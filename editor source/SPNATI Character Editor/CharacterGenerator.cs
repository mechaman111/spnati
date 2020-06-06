using KisekaeImporter;
using KisekaeImporter.DataStructures.Kisekae;
using KisekaeImporter.ImageImport;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Manages creating the images for characters out of KisekaeCodes
	/// </summary>
	public static class CharacterGenerator
	{
		private static int _currentConverter = -1;
		private static IKisekaeConverter _converter;
		private static BackgroundQueue _workerQueue = new BackgroundQueue();
		private static bool _warmed;

		public static void SetConverter(int converter)
		{
			if (_currentConverter != converter)
			{
				_warmed = false;
				_currentConverter = converter;
				switch (converter)
				{
					case 1:
						_converter = new KisekaeOnlineConverter();
						break;
					case 2:
						_converter = new KisekaeConverter(false);
						break;
					default:
						_converter = new KisekaeConverter(true);
						break;
				}
			}
		}

		public static void DiscardRequests(object requester)
		{
			_workerQueue.ChangePriority(requester, 9999);
		}

		private static void CleanImage(KisekaeCode code, ISkin character, bool skipPreprocessing)
		{
			if (!skipPreprocessing)
			{
				var poseComponent = code.GetComponent<KisekaePose>();
				if (poseComponent != null)
				{
					int offset = 410 - poseComponent.Placement.X;

					poseComponent.Placement.X = 410;

					for (int i = 1; i < code.Models.Length; i++)
					{
						KisekaePose modelPose = code.Models[i]?.GetComponent<KisekaePose>();
						if (modelPose != null)
						{
							modelPose.Placement.X += offset;
						}
					}
					code.Scene?.ShiftX(offset);
				}
			}

			List<KisekaeCode> codes = new List<KisekaeCode>();
			codes.Add(code);
			ImportUnrecognizedAssets(character, codes);
		}

		public static bool ImportUnrecognizedAssets(ISkin character, List<KisekaeCode> codes)
		{
			bool cancelled = false;
			List<string> unknownUrls = new List<string>();
			string imagesDir = character.GetAttachmentsDirectory();

			List<KisekaeChunk> chunks = new List<KisekaeChunk>();
			foreach (KisekaeCode code in codes)
			{
				for (int i = 0; i < code.Models.Length; i++)
				{
					if (code.Models[i] != null)
					{
						chunks.Add(code.Models[i]);
					}
				}
				if (code.Scene != null)
				{
					chunks.Add(code.Scene);
				}
			}

			foreach (KisekaeChunk chunk in chunks)
			{
				unknownUrls.Clear();
				foreach (string url in chunk.Assets)
				{
					//search for any images that aren't in the character's images folder
					string filename = Path.GetFileName(url);
					string realFile = Path.Combine(imagesDir, filename);
					if (!Directory.Exists(imagesDir))
					{
						Directory.CreateDirectory(imagesDir);
					}
					if (!File.Exists(realFile))
					{
						unknownUrls.Add(filename);
					}
				}

				if (unknownUrls.Count > 0)
				{
					PropImporter importer = new PropImporter();
					importer.SetData(unknownUrls, imagesDir);
					cancelled = (importer.ShowDialog() == System.Windows.Forms.DialogResult.Cancel);
				}
				if (chunk.Assets.Count > 0)
				{
					string kklDir = Path.GetDirectoryName(Config.KisekaeDirectory);
					if (string.IsNullOrEmpty(kklDir))
					{
						KisekaeSetup setup = new Forms.KisekaeSetup();
						if (setup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							kklDir = Path.GetDirectoryName(Config.KisekaeDirectory);
						}
						else
						{
							continue;
						}
					}
					kklDir = Path.Combine(kklDir, "images");
					if (!Directory.Exists(kklDir))
					{
						Directory.CreateDirectory(kklDir);
					}
					UpdateAssets(imagesDir, kklDir);
					chunk.ReplaceAssetPaths("images");
				}
			}
			return !cancelled;
		}

		/// <summary>
		/// Copies files from character's attachments folder to an images subdirectory of kkl
		/// </summary>
		/// <param name="charDirectory"></param>
		private static void UpdateAssets(string charDirectory, string kklDirectory)
		{
			foreach (string file in Directory.EnumerateFiles(charDirectory))
			{
				string filename = Path.GetFileName(file);
				string imagePath = Path.Combine(kklDirectory, filename);
				if (File.Exists(imagePath))
				{
					//If the kkl folder is newer, don't overwrite it.
					FileInfo fi = new FileInfo(imagePath);
					FileInfo fromFi = new FileInfo(file);
					if (fromFi.LastWriteTimeUtc <= fi.LastWriteTimeUtc)
					{
						continue;
					}
				}
				try
				{
					File.Copy(file, imagePath, true);
				}
				catch (Exception e)
				{
					ErrorLog.LogError(e.Message);
				}
			}
		}

		public static async Task<Image> GetCroppedImage(KisekaeCode code, Rect crop, ISkin character, Dictionary<string, string> extraData, bool skipPreprocessing)
		{
			//reset some scene vars
			CleanImage(code, character, skipPreprocessing);
			ImageMetadata data = new ImageMetadata("raw", code.ToString())
			{
				CropInfo = crop,
				ExtraData = extraData
			};
			if (!_warmed)
			{
				_converter.WarmUp();
				_warmed = true;
			}
			return await _workerQueue.QueueTask(() => { return _converter.Generate(data, true, false); }, 1, null, 0);
		}

		public static async Task<Image> GetRawImage(KisekaeCode code, ISkin character, Dictionary<string, string> extraData, bool skipPreprocessing)
		{
			//reset some scene vars
			CleanImage(code, character, skipPreprocessing);
			ImageMetadata data = new ImageMetadata("raw", code.ToString());
			data.SkipPreprocessing = skipPreprocessing;
			data.ExtraData = extraData;
			if (!_warmed)
			{
				_converter.WarmUp();
				_warmed = true;
			}
			return await _workerQueue.QueueTask(() => { return _converter.Generate(data, false, false); }, 1, null, 0);
		}
	}

	public class TaggedImage
	{
		public int ReferenceCount;
		public Image Image;
		public int Hash;
		public Func<Image> RestoreFunction;

		public TaggedImage(Image img, int hash, Func<Image> restoreFunction)
		{
			ReferenceCount = 1;
			Image = img;
			Hash = hash;
			RestoreFunction = restoreFunction;
		}

		public void AddReference()
		{
			ReferenceCount++;
			if (Image == null && RestoreFunction != null)
			{
				Image = RestoreFunction();
			}
		}

		public void ReleaseReference()
		{
			ReferenceCount--;
			if (ReferenceCount <= 0 && Image != null && RestoreFunction != null)
			{
				Image.Dispose();
				Image = null;
			}
		}
	}

	/// <summary>
	/// Interface for converting a Kisekae code into an image
	/// </summary>
	public interface IKisekaeConverter
	{
		Image Generate(ImageMetadata metadata, bool crop, bool allowCache);
		void WarmUp();
	}
}

using KisekaeImporter;
using KisekaeImporter.DataStructures.Kisekae;
using KisekaeImporter.ImageImport;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Manages creating the images for characters out of KisekaeCodes
	/// </summary>
	public static class CharacterGenerator
	{
		private static IKisekaeConverter _converter = new KisekaeConverter();
		private static BackgroundQueue _workerQueue = new BackgroundQueue();

		public static void SetConverter(IKisekaeConverter converter)
		{
			_converter = converter;
		}

		public static void DiscardRequests(object requester)
		{
			_workerQueue.ChangePriority(requester, 9999);
		}

		private static void CleanImage(KisekaeCode code, Character character)
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

			List<string> unknownUrls = new List<string>();
			string imagesDir = character.GetAttachmentsDirectory();

			List<KisekaeChunk> chunks = new List<KisekaeChunk>();
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
					importer.ShowDialog();
				}
				chunk.ReplaceAssetPaths(imagesDir);
			}
		}

		public static async Task<Image> GetCroppedImage(KisekaeCode code, Rect crop, Character character)
		{
			//reset some scene vars
			CleanImage(code, character);
			ImageMetadata data = new ImageMetadata("raw", code.ToString())
			{
				CropInfo = crop
			};
			return await _workerQueue.QueueTask(() => { return _converter.Generate(data, true, false); }, 1, null, 0);
		}

		public static async Task<Image> GetRawImage(KisekaeCode code, Character character)
		{
			//reset some scene vars
			CleanImage(code, character);
			ImageMetadata data = new ImageMetadata("raw", code.ToString());
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
	}
}

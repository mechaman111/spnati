using KisekaeImporter.RemoteClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KisekaeImporter.ImageImport
{
	/// <summary>
	/// Interfaces with Kisekae to produce images
	/// </summary>
	public sealed class ImageImporter: IDisposable
	{
		public const int ImageXOffset = 700;

		private const string VersionSetup33 = "33***bc410.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]ua1.0.0.0_ub_uc7.0.30_ud7.0";
		private const string VersionSetup36 = "36***bc410.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0_ub_u0_v0_uc7.0.30_ud7.0";
		private const string VersionSetup40 = "40***bc410.500.0.0.1*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v0_uc7.2.24_ud7.8";
		private const string VersionSetup54 = "54***bc410.500.0.0.1.0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v0_uc7.2.24_ud7.8";
		private const string VersionSetup68 = "68***bc410.500.0.0.1.0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_e00_uf0.3.0.0_ue_ub_u0_v00_uc7.2.30_ud7.8";
		private const string VersionSetup83 = "83***bc410.500.0.0.1.0_f00*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_e00_uf0.3.0.0_ue_ub_u0_v00_uc7.2.30_ud7.8";

		private const string DefaultVersionSetup = VersionSetup68;

		/// <summary>
		/// Number of seconds to wait for an image
		/// </summary>
		private const int RetryTimeLimit = 10000;
		/// <summary>
		/// Frequency in ms to check for the image
		/// </summary>
		private const int RetryInterval = 200;
		/// <summary>
		/// Number of attempts to try to find the file
		/// </summary>
		private const int RetryLimit = RetryTimeLimit / RetryInterval;

		/// <summary>
		/// appdata folder for kkl.exe
		/// </summary>
		private string KklAppData
		{
			get
			{
				string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "kkl", "Local Store");
				return directory;
			}
		}

		private string SetupFileName
		{
			get { return Path.Combine(KklAppData, "scene_setup_file.txt"); }
		}

		private string SetupImageFileName
		{
			get { return Path.Combine(KklAppData, "scene_setup_file..png"); }
		}

		private string GetSetupString(string rawData)
		{
			if (rawData.Length < 2)
				return DefaultVersionSetup;
			string version = rawData.Substring(0, 2);
			switch (version)
			{
				case "33":
					return VersionSetup33;
				case "36":
					return VersionSetup36;
				case "40":
					return VersionSetup40;
				case "54":
					return VersionSetup54;
				case "68":
					return VersionSetup68;
				case "83":
					return VersionSetup83;
				default:
					return DefaultVersionSetup;
			}
		}

		private Client _client;

		public ImageImporter(bool allowRemoteControl)
		{
			_client = new Client();
			if (allowRemoteControl)
			{
				_client.Connect();
			}
		}

		public Image ImportSingleImage(ImageMetadata image)
		{
			//SetupForImport();
			return Import(image);
		}

		/// <summary>
		/// Reimports the last code
		/// </summary>
		/// <returns></returns>
		public Image Reimport(Dictionary<string, string> extraData)
		{
			return ImportCode("54**", extraData); //import an empty scene so that nothing changes but we get a file outputted
		}

		private void SetupForImport()
		{
			try
			{
				File.WriteAllText(SetupFileName, GetSetupString(""));
			}
			catch
			{

			}
		}

		private Image Import(ImageMetadata image)
		{
			const string DefaultVersion = "68**";

			string baseFile = Path.Combine(KklAppData, image.ImageKey);

			string data = image.Data;
			if (!string.IsNullOrEmpty(data))
			{
				if (!image.SkipPreprocessing)
				{
					KisekaeCode importCode = new KisekaeCode(image.Data);
					string sceneSetup = GetSetupString(data);
					KisekaeCode code = new KisekaeCode(sceneSetup, true);
					KisekaeScene scene = code.Scene?.GetComponent<KisekaeScene>();
					int x = 0;
					int y = 0;
					int zoom = 0;
					if (scene != null)
					{
						x = scene.Camera.X;
						y = scene.Camera.Y;
						zoom = scene.Camera.Zoom;
					}
					code.MergeIn(importCode, false, false);
					if (scene != null)
					{
						scene.Camera.X = x;
						scene.Camera.Y = y;
						scene.Camera.Zoom = zoom;
					}
					data = code.ToString();
				}
			}

			if (!image.StartsWithVersion())
			{
				data = DefaultVersion + image.Data;
			}

			return ImportCode(data, image.ExtraData);
		}

		private Image ImportCode(string data, Dictionary<string, string> extraData)
		{
			try
			{
				List<string> lines = new List<string>();
				if (extraData != null)
				{
					foreach (KeyValuePair<string, string> kvp in extraData)
					{
						string[] subpieces = kvp.Key.Split('/');
						foreach (string subpiece in subpieces)
						{
							if (float.TryParse(kvp.Value, out float v))
							{
								lines.Add($"{subpiece}={Math.Floor(v / 100.0f * 255)}");
							}
						}
					}
				}
				lines.Add(data);

				if (_client.Connected && lines.Count <= 1) //KKL104 seems really bad with transparencies, so use the old import method with them
				{
					//use remote protocol

					//ostensibly transparencies can be prepended to the code with new lines, but this doesn't appear to work
					//string code = string.Join("\n", lines).Replace("\\\\", "\\");
					string code = data.Replace("\\\\", "\\");

					//so instead, send transparency commands
					foreach (KeyValuePair<string, string> kvp in extraData)
					{
						string[] subpieces = kvp.Key.Split('/');
						foreach (string subpiece in subpieces)
						{
							if (float.TryParse(kvp.Value, out float v))
							{
								int amount = (int)Math.Floor(v / 100.0f * 255);
								ServerRequest partRequest = new ServerRequest("alpha_direct", "op", "set", "character", "0", "path", subpiece, "alpha", amount.ToString());
								Task<ServerResponse> alphaTask = _client.SendCommand(partRequest);
								alphaTask.Wait();
								ServerResponse r = alphaTask.Result;
								if (!r.IsSuccess)
								{
									return null;
								}
							}
						}
					}

					ServerRequest request = new ServerRequest("import", "code", code.Replace("\\", "\\\\"));
					Task<ServerResponse> task = _client.SendCommand(request);
					task.Wait();
					ServerResponse response = task.Result;
					if (response.IsSuccess)
					{
						if (extraData.Count > 0)
						{
							//reset transparencies
							ServerRequest partRequest = new ServerRequest("alpha_direct", "op", "reset_all");
							_client.SendCommand(partRequest).Wait();
						}

						return (response as ImageResponse)?.Image;
					}
					else
					{
						//maybe they restarted KKL, so try to connect again for next time
						_client.Connect();
					}
				}

				//fall back to the text file method

				string baseFile = Path.Combine(KklAppData, "zzReimport");
				string dataFileName = baseFile + ".txt";
				string[] imageFileNames = new string[] { baseFile + "..png", baseFile + ".png" }; //Different versions expect different names

				//Conversion can fail if the image already exists
				foreach (string file in imageFileNames)
				{
					File.Delete(file);
				}

				//Write the image data where kkl can find it
				File.WriteAllLines(dataFileName, lines);

				//Wait for KKL to pick it up and create an image
				Image result = null;
				Image tmp = WaitForImage(imageFileNames);
				if (tmp != null)
				{
					result = new Bitmap(tmp); //free up the file so it can be deleted later
					tmp.Dispose();
				}
				return result;
			}
			catch
			{

			}
			return null;
		}

		/// <summary>
		/// Waits for kisekae to generate the image in the local cache
		/// </summary>
		/// <param name="possibleFileNames"></param>
		/// <returns></returns>
		private Image WaitForImage(string[] possibleFileNames)
		{
			bool found = false;
			int triesLeft = RetryLimit;
			while (triesLeft > 0)
			{
				for (int i = 0; i < possibleFileNames.Length; i++)
				{
					string filename = possibleFileNames[i];
					if (File.Exists(filename))
					{
						try
						{
							using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
							{
								using (Image src = Image.FromStream(fs))
								{
									return new Bitmap(src);
								}
							}
						}
						catch
						{
							if (!found)
							{
								triesLeft = RetryLimit;
								found = true;
							}
						}
					}
				}
				triesLeft--;
				Thread.Sleep(RetryInterval);
			}

			//Failed
			return null;
		}

		/// <summary>
		/// Crops a source image to the given size. Transparent areas will be added if it doesn't fit
		/// </summary>
		/// <param name="key">File name</param>
		/// <param name="srcImage">Source image</param>
		/// <param name="cropRegion">Cropping region</param>
		/// <returns>Copy of the source image cropped as appropriate</returns>
		public Image Crop(Image srcImage, Rect cropBounds)
		{
			if (srcImage == null)
				return null;
			Rectangle cropRegion = new Rectangle(cropBounds.Left, cropBounds.Top, cropBounds.Right - cropBounds.Left, cropBounds.Bottom - cropBounds.Top);

			Image img = new Bitmap(cropRegion.Width, cropRegion.Height);
			Graphics g = Graphics.FromImage(img);

			g.DrawImage(srcImage, new Rectangle(0, 0, img.Width, img.Height), cropRegion, GraphicsUnit.Pixel);
			g.Dispose();
			return img;
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}

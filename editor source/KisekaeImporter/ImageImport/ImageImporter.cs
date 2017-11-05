using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace KisekaeImporter.ImageImport
{
	/// <summary>
	/// Interfaces with Kisekae to produce images
	/// </summary>
	public class ImageImporter
	{
		private PoseList _poseList = new PoseList();

		public const int ImageXOffset = 700;

		private const string VersionSetup33 = "33***bc410.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]ua1.0.0.0_ub_uc7.0.30_ud7.0";
		private const string VersionSetup36 = "36***bc410.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0_ub_u0_v0_uc7.0.30_ud7.0";
		private const string VersionSetup40 = "40***bc410.500.0.0.1*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v0_uc7.2.24_ud7.8";
		//47**ba54_bb7.1_bc496.500.0.0.1_bd17_be180
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

		public void AddPose(string key, string rawData)
		{
			_poseList.Poses.Add(new ImageMetadata(key, rawData));
		}

		private string GetSetupString()
		{
			//Parse out the version from the first image
			if (_poseList.Poses.Count == 0)
				return VersionSetup33; //Default version

			return GetSetupString(_poseList.Poses[0].Data);
		}

		private string GetSetupString(string rawData)
		{
			if (rawData.Length < 2)
				return VersionSetup33;
			string version = rawData.Substring(0, 2);
			switch (version)
			{
				case "36":
					return VersionSetup36;
				case "40":
					return VersionSetup40;
				default:
					return VersionSetup33;
			}
		}

		public Image ImportSingleImage(ImageMetadata image)
		{
			SetupForImport();
			return Import(image);
		}

		private void SetupForImport()
		{
			try
			{
				File.WriteAllText(SetupFileName, GetSetupString());
			}
			catch
			{
			
			}
		}

		private Image Import(ImageMetadata image)
		{
			const string DefaultVersion = "33**";

			string baseFile = Path.Combine(KklAppData, image.ImageKey);
			string dataFileName = baseFile + ".txt";
			string[] imageFileNames = new string[] { baseFile + "..png", baseFile + ".png" }; //Different versions expect different names

			string data = image.Data;
			if (!image.StartsWithVersion())
			{
				data = DefaultVersion + image.Data;
			}

			try
			{
				//Conversion can fail if the image already exists
				foreach (string file in imageFileNames)
				{
					File.Delete(file);
				}

				//Write the image data where kkl can find it
				File.WriteAllText(dataFileName, data);

				//Wait for KKL to pick it up and create an image
				Image result = WaitForImage(imageFileNames);
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
							return new Bitmap(filename);
						}
						catch
						{
							Thread.Sleep(RetryInterval); //Sometimes bad timing can cause Out of Memory exceptions, so give it a little more time to settle
							return new Bitmap(filename);
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
			return img;
		}
	}
}

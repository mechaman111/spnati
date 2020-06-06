using KisekaeImporter.ImageImport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public class KisekaeConverter : IKisekaeConverter
	{
		private ImageImporter _importer;
		private System.Timers.Timer _timer;

		private Dictionary<int, Image> _rawCache = new Dictionary<int, Image>();

		public bool AllowRemoteControl;

		public KisekaeConverter(bool allowRemoteControl)
		{
			AllowRemoteControl = allowRemoteControl;
			_timer = new System.Timers.Timer(10000);
			_timer.Elapsed += _timer_Elapsed;
			_timer.Start();
		}

		public void WarmUp()
		{
			CheckKKL();

			try
			{
				string path = Config.KisekaeDirectory;
				string enableServerPath = Path.Combine(Path.GetDirectoryName(path), "enable_server");
				if (!string.IsNullOrEmpty(path) && !File.Exists(enableServerPath))
				{
					File.Create(enableServerPath);
				}
			}
			catch { }

			_importer = new ImageImporter(AllowRemoteControl);
		}
		
		private void CheckKKL()
		{
			if (!string.IsNullOrEmpty(Config.KisekaeDirectory))
			{
				//see what version of kkl is running and complain if it doesn't match
				try
				{
					Process[] p = Process.GetProcessesByName("kkl");
					if (p.Length > 0)
					{
						string file = p[0].MainModule.FileName;
						if (string.Compare(file, Config.KisekaeDirectory, true) != 0)
						{
							if (MessageBox.Show($"Looks like you're running a different version of kkl.exe than the editor is configured to use, which will cause image attachments to fail.\r\n\r\nDo you want me to switch the editor to use {file}?", "Character Editor", MessageBoxButtons.YesNo) == DialogResult.Yes)
							{
								Config.KisekaeDirectory = file;
							}
						}
					}
				}
				catch { }
			}

			//TODO: Add enable_server file
		}

		private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			_timer.Stop();
			//clear the raw cache
			lock (_rawCache)
			{
				foreach (KeyValuePair<int, Image> kvp in _rawCache)
				{
					kvp.Value.Dispose();
				}
				_rawCache.Clear();
			}
			_timer.Start();
		}

		public Image Generate(ImageMetadata metadata, bool crop, bool allowCache)
		{
			if (!metadata.Data.Contains("_ae"))
			{
				metadata.Data += "_ae1.3.3.0.0";
			}
			int hash = metadata.Data.GetHashCode();

			Rect cropInfo = metadata.CropInfo;
			cropInfo.Left += ImageImporter.ImageXOffset;
			cropInfo.Right += ImageImporter.ImageXOffset;

			lock (_rawCache)
			{
				Image raw;
				if (allowCache && _rawCache.TryGetValue(hash, out raw))
				{
					if (crop)
					{
						//this code has already been imported in this run, so just need to crop it
						return _importer.Crop(raw, cropInfo);
					}
					else return raw;
				}

				int tries = 2;
				while (tries > 0)
				{
					try
					{
						using (Image img = _importer.ImportSingleImage(metadata))
						{
							//copy the image and cache the uncropped data so future requests for the same code with a different cropping can just crop
							Bitmap copy = new Bitmap(img.Width, img.Height);
							Graphics g = Graphics.FromImage(copy);
							g.DrawImage(img, 0, 0);
							g.Dispose();
							if (allowCache)
							{
								_rawCache[hash] = copy;
							}

							if (crop)
							{
								return _importer.Crop(img, cropInfo);
							}
							else
							{
								return copy;
							}
						}
					}
					catch (Exception)
					{
						GC.Collect();
						tries--;
					}
				}
				return null;
			}
			//throw new Exception("Too many exceptions");
		}
	}
}

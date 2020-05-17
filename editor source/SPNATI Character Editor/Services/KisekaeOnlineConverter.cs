using Desktop;
using KisekaeImporter;
using KisekaeImporter.DataStructures.Kisekae;
using KisekaeImporter.ImageImport;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SPNATI_Character_Editor
{
	public class KisekaeOnlineConverter : IKisekaeConverter
	{
		private ImageImporter _importer = new ImageImporter();
		private Timer _timer;
		private HttpClient _httpClient = new HttpClient();
		private Dictionary<int, Image> _rawCache = new Dictionary<int, Image>();

		public KisekaeOnlineConverter()
		{
			_timer = new Timer(10000);
			_timer.Elapsed += _timer_Elapsed;
			_timer.Start();
		}

		public void WarmUp()
		{
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
			}
			return RequestImage(metadata, hash, allowCache, crop, cropInfo).Result;
		}

		private async Task UploadAttachment(string path)
		{
			string file = Path.Combine(Path.GetDirectoryName(Config.KisekaeDirectory), path);
			if (File.Exists(file))
			{
				MultipartFormDataContent requestContent = new MultipartFormDataContent();
				using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
				{
					requestContent.Add(new StreamContent(fs), "file", Path.GetFileName(path));
					try
					{
						await this._httpClient.PostAsync("https://spnati.faraway-vision.io/kkl/upload_attachment", requestContent);
					}
					catch (Exception e)
					{
						ErrorLog.LogError(e.Message);
					}
				}
				requestContent = null;
			}
		}

		private async Task<Image> RequestImage(ImageMetadata metadata, int hash, bool allowCache, bool crop, Rect cropInfo)
		{
			KisekaeCode code = new KisekaeCode(metadata.Data);
			if (code.Scene != null)
			{
				foreach (string asset in code.Scene.Assets)
				{
					await UploadAttachment(asset);
				}
			}
			for (int i = 0; i < code.Models.Length; i++)
			{
				KisekaeModel model = code.Models[i];
				if (model != null)
				{
					foreach (string asset in model.Assets)
					{
						await UploadAttachment(asset);
					}
				}
				model = null;
			}
			ImportPayload payload = new ImportPayload(metadata);
			string json = Json.Serialize(payload);
			json = json.Replace('\\', '/');
			try
			{
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.PostAsync("https://spnati.faraway-vision.io/kkl/import", content);
				StreamContent cnt = response.Content as StreamContent;
				Stream stream = await cnt.ReadAsStreamAsync();
				using (Image img = new Bitmap(stream))
				{
					Bitmap copy = new Bitmap(2000, 1500);
					Graphics g = Graphics.FromImage(copy);
					g.DrawImage(img, copy.Width / 2 - img.Width / 2, 0);
					g.Dispose();
					if (allowCache)
					{
						Dictionary<int, Image> obj = _rawCache;
						lock (obj)
						{
							_rawCache[hash] = copy;
						}
						obj = null;
					}
					if (crop)
					{
						return _importer.Crop(img, cropInfo);
					}
					return copy;
				}
			}
			catch (Exception e)
			{
				ErrorLog.LogError(e.Message);
			}
			return null;
		}
	}
}

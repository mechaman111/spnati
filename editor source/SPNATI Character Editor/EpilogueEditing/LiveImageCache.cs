using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SPNATI_Character_Editor
{
	public static class LiveImageCache
	{
		private static Dictionary<string, Bitmap> _images = new Dictionary<string, Bitmap>();

		public static void Clear()
		{
			foreach (Bitmap img in _images.Values)
			{
				img.Dispose();
			}
			_images.Clear();
		}

		public static Bitmap Get(string src)
		{
			if (string.IsNullOrEmpty(src)) { return null; }
			Bitmap img = null;
			if (_images.TryGetValue(src, out img))
			{
				return img;
			}
			string path = GetImagePath(src);
			if (!File.Exists(path))
			{
				return null;
			}
			try
			{
				using (Bitmap temp = new Bitmap(path))
				{
					img = new Bitmap(temp);
					_images[src] = img;
				}
			}
			catch { }
			return img;
		}

		private static string GetImagePath(string path)
		{
			return Path.Combine(Config.SpnatiDirectory, "opponents", path);
		}
	}
}

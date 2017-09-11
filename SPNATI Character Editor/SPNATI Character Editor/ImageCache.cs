using System.Collections.Generic;
using System.Drawing;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Loads images as necessary to avoid holding a bajillion in memory
	/// </summary>
	public static class ImageCache
	{
		private static Dictionary<string, Image> _cache = new Dictionary<string, Image>();

		public static void Clear()
		{
			_cache.Clear();
		}

		public static Image Get(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return null;
			Image img = null;
			if (!_cache.TryGetValue(filename, out img))
			{
				try
				{
					using (var temp = new Bitmap(filename))
					{
						//Load an image and then create a new one from it so that we can dispose the original image and unlock the file, which
						//will allow us to replace the file with a new one when importing new images
						img = new Bitmap(temp);
					}
				}
				catch { }
			}
			return img;
		}

		public static void Release(string filename)
		{
			Image img = null;
			if (_cache.TryGetValue(filename, out img))
			{
				_cache.Remove(filename);
				img.Dispose();
				img = null;
			}
		}

		public static void Set(string filename, Image image)
		{
			_cache[filename] = image;
		}
	}
}

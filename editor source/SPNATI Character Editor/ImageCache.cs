using System;
using System.Collections.Generic;
using System.Drawing;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Loads images as necessary to avoid holding a bajillion in memory.
	/// Also manages unloading images to prevent running out of GDI resources which cause Kisekae imports to fail.
	/// </summary>
	public static class ImageCache
	{
		private static Dictionary<string, ImageAsset> _cache = new Dictionary<string, ImageAsset>();

		private class ImageAsset : IDisposable
		{
			public ImageReference Reference;
			public int Count;

			public ImageAsset(ImageReference reference)
			{
				Reference = reference;
				Count = 1;
			}

			public void Dispose()
			{
				Reference?.Dispose();
			}
		}

		/// <summary>
		/// Obtains a reference to an image. The image will remain in memory for as long as it has at least one reference
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static ImageReference Get(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return null;
			ImageAsset reference = null;
			if (!_cache.TryGetValue(filename, out reference))
			{
				try
				{
					if (filename.EndsWith(".gif"))
					{
						Image image = Image.FromFile(filename);
						ImageReference imageRef = new ImageReference(filename, image);
						reference = new ImageAsset(imageRef);
						_cache[filename] = reference;
					}
					else
					{
						using (var temp = new Bitmap(filename))
						{
							//Load an image and then create a new one from it so that we can dispose the original image and unlock the file, which
							//will allow us to replace the file with a new one when importing new images
							Image image = new Bitmap(temp);
							ImageReference imageRef = new ImageReference(filename, image);
							reference = new ImageAsset(imageRef);
							_cache[filename] = reference;
						}
					}
				}
				catch { }
			}
			else
			{
				reference.Count++;
			}
			return reference?.Reference;
		}

		/// <summary>
		/// Releases an image reference. When all references are lost, the image is unloaded
		/// </summary>
		/// <param name="filename"></param>
		public static void Release(string filename)
		{
			ImageAsset reference = null;
			if (_cache.TryGetValue(filename, out reference))
			{
				reference.Count--;
				if (reference.Count <= 0)
				{
					_cache.Remove(filename);
					reference.Dispose();
				}
			}
		}
	}

	public class ImageReference : IDisposable
	{
		public string FileName;
		public Image Image;

		public ImageReference(string filename, Image image)
		{
			FileName = filename;
			Image = image;
		}

		public override string ToString()
		{
			return FileName;
		}

		public void Replace(Image newImage)
		{
			Image = newImage;
		}

		public void Dispose()
		{
			Image?.Dispose();
			Image = null;
		}
	}
}

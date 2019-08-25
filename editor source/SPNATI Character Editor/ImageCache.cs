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
		public const string PreviewImage = "***preview***";

		private static Dictionary<string, ImageAsset> _cache = new Dictionary<string, ImageAsset>();

		private class ImageAsset : IDisposable
		{
			public ImageReference Reference;
			public int Count;
			public Image Image
			{
				get { return Reference.Image; }
			}

			public ImageAsset(ImageReference reference)
			{
				Reference = reference;
				Count = 1;
			}

			public void Dispose()
			{
				Reference?.Dispose();
			}

			public void Replace(Image newImage)
			{
				Reference.Replace(newImage);
			}
		}

		public static void Clear()
		{
			_cache.Clear();
		}

		public static int GetReferenceCount(string filename)
		{
			ImageAsset reference = _cache.Get(filename);
			if (reference != null)
			{
				return reference.Count;
			}
			return 0;
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

		/// <summary>
		/// Adds an image that didn't come from disk to the cache and sets its reference count to 0
		/// </summary>
		/// <param name="key"></param>
		/// <param name="image"></param>
		public static void Add(string key, Image image)
		{
			ImageAsset reference = null;
			if (!_cache.TryGetValue(key, out reference))
			{
				reference = new ImageAsset(new ImageReference(key, image));
				_cache[key] = reference;
				reference.Count = 0;
			}
			else
			{
				reference.Count++;
			}
		}

		/// <summary>
		/// Replaces a cached image THAT CAME FROM DISK. Current references need to act on the ReplaceImage desktop message to actually use it, however
		/// </summary>
		/// <param name="key"></param>
		/// <param name="newImage"></param>
		public static void Replace(string key, Image newImage)
		{
			ImageAsset reference = _cache.Get(key);
			if (reference != null)
			{
				//dispose the old image since nobody should still be holding onto a reference. If they are, naughty naughty
				reference.Dispose();
				reference.Replace(newImage);
			}
			else
			{
				newImage.Dispose(); //nobody needs the new image right away, so don't keep it around
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

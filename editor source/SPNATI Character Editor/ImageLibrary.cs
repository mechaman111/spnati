using Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Holds images for a particular character
	/// </summary>
	public class ImageLibrary
	{
		private static Dictionary<Character, ImageLibrary> _libraries = new Dictionary<Character, ImageLibrary>();

		public const string PreviewImage = "***preview***";

		public static ImageLibrary Get(Character character)
		{
			return _libraries.GetOrAddDefault(character, () =>
			{
				ImageLibrary lib = new ImageLibrary();
				lib.Load(character);
				return lib;
			});
		}

		private Dictionary<int, List<CharacterImage>> _stages = new Dictionary<int, List<CharacterImage>>();
		private List<CharacterImage> _allImages = new List<CharacterImage>();
		private Dictionary<string, CharacterImage> _miniImages = new Dictionary<string, CharacterImage>();

		/// <summary>
		/// Loads metadata for all images in the given folder
		/// </summary>
		/// <param name="folder"></param>
		private void Load(Character character)
		{
			_stages.Clear();
			_allImages.Clear();
			string dir = Config.GetRootDirectory(character);
			string[] extensions = { ".png", ".gif" };
			foreach (string file in Directory.EnumerateFiles(dir, "*.*")
				.Where(s => extensions.Any(ext => ext == Path.GetExtension(s))))
			{
				string name = Path.GetFileNameWithoutExtension(file);
				Add(file, name);
			}
		}

		/// <summary>
		/// Adds an image
		/// </summary>
		/// <param name="file"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public CharacterImage Add(string file, string name)
		{
			CharacterImage image = new CharacterImage(name, file);
			_allImages.Add(image);

			if (file != PreviewImage)
			{
				int stage = -1;
				if (char.IsNumber(image.Name[0]))
				{
					int hyphen = image.Name.IndexOf('-', 1);
					if (hyphen > 0)
					{
						stage = int.Parse(image.Name.Substring(0, hyphen));
					}
				}
				if (stage < 0)
					image.IsGeneric = true;
				List<CharacterImage> list;
				if (!_stages.TryGetValue(stage, out list))
				{
					list = new List<CharacterImage>();
					_stages[stage] = list;
				}
				list.Add(image);
			}
			return image;
		}

		/// <summary>
		/// Finds an image with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public CharacterImage Find(string name)
		{
			string shortName = Path.GetFileNameWithoutExtension(name);
			return _allImages.Find(img => img.Name == name || img.Name == shortName);
		}

		/// <summary>
		/// Gets (generating if need be), a resized version of an image
		/// </summary>
		/// <param name="name"></param>
		/// <param name="height">Height in pixels to use for the image</param>
		/// <returns></returns>
		public CharacterImage GetMini(string name, int height)
		{
			CharacterImage existing = _miniImages.Get(name);
			if (existing != null && existing.Disposed)
			{
				_miniImages.Remove(name); //Image is stale, so throw it away
				existing = null;
			}
			if (existing == null)
			{
				CharacterImage img = Find(name);
				if (img == null)
				{
					return null;
				}
				else
				{
					Image fullSize = img.GetImage();
					float aspect = (float)fullSize.Width / fullSize.Height;
					int width = (int)(aspect * height);
					Bitmap mini = new Bitmap(fullSize, new Size(width, height));
					img.ReleaseImage();
					string key = "*MINI*" + name;
					existing = new CharacterImage(name, key);
					_miniImages[name] = existing;
					ImageCache.Add(key, mini);
				}
			}
			return existing;
		}

		/// <summary>
		/// Enumerates all images with the given stage's prefix
		/// </summary>
		/// <param name="stage">Stage to get images for.</param>
		/// <returns></returns>
		public IEnumerable<CharacterImage> GetImages(int stage)
		{
			List<CharacterImage> list;
			if (_stages.TryGetValue(stage, out list))
			{
				return list;
			}
			return new List<CharacterImage>();
		}

		/// <summary>
		/// Replaces an existing image. Reliant on any references to update themselves
		/// </summary>
		/// <param name="path"></param>
		/// <param name="newImage"></param>
		public CharacterImage Replace(string path, Image newImage)
		{
			CharacterImage reference = Find(path);
			if (reference != null && reference.ReferenceCount > 0)
			{
				ImageReplacementArgs args = new ImageReplacementArgs()
				{
					Reference = reference,
					NewImage = newImage
				};
				Shell.Instance.PostOffice.SendMessage(DesktopMessages.ReplaceImage, args);

				ImageCache.Replace(reference.FileName, newImage);
				return reference;
			}
			else
			{
				ImageCache.Add(path, newImage);
				return Add(path, Path.GetFileNameWithoutExtension(path));
			}
		}
	}

	public class ImageReplacementArgs
	{
		public CharacterImage Reference { get; set; }
		public Image NewImage { get; set; }
	}

	public class CharacterImage
	{
		public bool Disposed { get; private set; }

		public string FileName;
		public string FileExtension;
		public string Name;
		public string DefaultName { get; set; }
		/// <summary>
		/// Indicates this image has no prefix
		/// </summary>
		public bool IsGeneric;

		public int ReferenceCount { get { return ImageCache.GetReferenceCount(FileName); } }

		public CharacterImage(string name, string filename)
		{
			Name = name;
			FileName = filename;
			FileExtension = Path.GetExtension(filename);
			DefaultName = DialogueLine.GetDefaultImage(Name);
		}

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Gets the real image associated with a filename.
		/// EVERY call to this should be paired with a Release() when done.
		/// If it'll be holding the reference for some time, it should also listen to the ReplaceImage desktop message and swap out the old image
		/// </summary>
		public Image GetImage()
		{
			Disposed = false;
			return ImageCache.Get(FileName);
		}

		/// <summary>
		/// Releases this image from memory
		/// </summary>
		public void ReleaseImage()
		{
			Disposed = true;
			ImageCache.Release(FileName);
		}
	}
}

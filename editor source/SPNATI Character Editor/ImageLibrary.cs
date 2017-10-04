using System.Collections.Generic;
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
		private Dictionary<int, List<CharacterImage>> _stages = new Dictionary<int, List<CharacterImage>>();
		private List<CharacterImage> _allImages = new List<CharacterImage>();
		private string _folder;

		/// <summary>
		/// Loads metadata for all images in the given folder
		/// </summary>
		/// <param name="folder"></param>
		public void Load(string folder)
		{
			_folder = folder;
			_stages.Clear();
			_allImages.Clear();
			string dir = Config.GetRootDirectory(folder);
			string[] extenstions = { ".png", ".gif" };
			foreach (string file in Directory.EnumerateFiles(dir, "*.*")
				.Where(s => extenstions.Any(ext => ext == Path.GetExtension(s))))
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
		/// Updates an image
		/// </summary>
		/// <param name="name"></param>
		/// <param name="image"></param>
		public void Update(string name, Image image)
		{
			CharacterImage img = Find(name);
			if (img == null)
				img = Add(Path.Combine(Config.GetRootDirectory(_folder), name + ".png"), name);
			img.Image = image;
		}

		/// <summary>
		/// Enumerates through all images for a character
		/// </summary>
		/// <returns></returns>
		public IEnumerable<CharacterImage> GetImages()
		{
			return _allImages;
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
	}

	public class CharacterImage
	{
		public Image Image
		{
			get
			{
				return ImageCache.Get(FileName);
			}
			set
			{
				ImageCache.Set(FileName, value);
			}
		}
		public string FileName;
		public string FileExtension;
		public string Name;
		public string DefaultName { get; set; }
		/// <summary>
		/// Indicates this image has no prefix
		/// </summary>
		public bool IsGeneric;

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
	}
}

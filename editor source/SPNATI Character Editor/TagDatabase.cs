using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Holds information about character tags
	/// </summary>
	public static class TagDatabase
	{
		private static Dictionary<string, int> _tags = new Dictionary<string, int>();
		public static TagDictionary Dictionary { get; private set; }

		public static void Load()
		{
			Dictionary = Serialization.ImportTags();
		}

		/// <summary>
		/// Adds an instance of a tag
		/// </summary>
		/// <param name="tag"></param>
		public static void AddTag(string tag, bool increment = true)
		{
			Dictionary.AddIfNew(tag);
			tag = tag.ToLowerInvariant();
			int current = 0;
			_tags.TryGetValue(tag, out current);
			if (increment || current == 0)
			{
				current++;
			}
			_tags[tag] = current;
		}

		/// <summary>
		/// Gets a tag by its value
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static Tag GetTag(string tag)
		{
			return Dictionary.GetTag(tag);
		}

		/// <summary>
		/// Checks whether a tag of the given key exists
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool TagExists(string key)
		{
			return _tags.ContainsKey(key.ToLowerInvariant());
		}

		/// <summary>
		/// Enumerates through the available tags
		/// </summary>
		public static IEnumerable<Tag> Tags
		{
			get
			{
				foreach (var kvp in _tags)
				{
					yield return new Tag() { Value = kvp.Key, Count = kvp.Value };
				}
			}
		}

		/// <summary>
		/// Converts a ToString format to the tag it belongs to
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static string StringToTag(string tag)
		{
			int split = tag.LastIndexOf(" (");
			if (split == -1)
				return tag;
			string name = tag.Substring(0, split);
			return name;
		}
	}
}

using Desktop;
using System;
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

		private static DualKeyDictionary<string, string, List<Character>> _groups = new DualKeyDictionary<string, string, List<Character>>();
		private static DualKeyDictionary<Character, string, List<string>> _characterGroups = new DualKeyDictionary<Character, string, List<string>>();

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
			if (string.IsNullOrEmpty(key)) { return false; }
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

		public static void CacheGroup(Tag tag, Character character)
		{
			if (string.IsNullOrEmpty(tag.Group)) { return; }
			List<Character> list = _groups.Get(tag.Group, tag.Key);
			List<string> tags = _characterGroups.Get(character, tag.Group);
			if (tags == null)
			{
				tags = new List<string>();
				_characterGroups.Set(character, tag.Group, tags);
			}
			tags.Add(tag.Key);
			if (list == null)
			{
				list = new List<Character>();
				_groups.Set(tag.Group, tag.Key, list);
			}
			list.Add(character);
		}

		public static Tuple<string, List<Character>> GetSmallestGroup(string group, Character character)
		{
			List<string> tags = _characterGroups.Get(character, group);
			if (tags == null) { return null; }
			int min = int.MaxValue;
			string minTag = null;
			List<Character> minList = null;
			Dictionary<string, List<Character>> output = new Dictionary<string, List<Character>>();
			Dictionary<string, List<Character>> result;
			_groups.TryGetValue(group, out result);
			foreach (string tag in tags)
			{
				List<Character> list = result.Get(tag);
				if (list != null && list.Count > 1 && (min == 0 || min > list.Count))
				{
					min = list.Count;
					minTag = tag;
					minList = list;
				}
			}
			if (minTag == null)
			{
				return null;
			}
			return new Tuple<string, List<Character>>(minTag, minList);
		}
	}
}

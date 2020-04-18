using Desktop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Holds information about character tags
	/// </summary>
	public static class TagDatabase
	{
		private static Dictionary<string, int> _tags = new Dictionary<string, int>();
		public static TagDictionary Dictionary { get; private set; }

		private static DualKeyDictionary<string, string, List<string>> _groups = new DualKeyDictionary<string, string, List<string>>();
		private static DualKeyDictionary<string, string, List<string>> _characterGroups = new DualKeyDictionary<string, string, List<string>>();

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
			List<string> list = _groups.Get(tag.Group, tag.Key);
			List<string> tags = _characterGroups.Get(character.FolderName, tag.Group);
			if (tags == null)
			{
				tags = new List<string>();
				_characterGroups.Set(character.FolderName, tag.Group, tags);
			}
			tags.Add(tag.Key);
			if (list == null)
			{
				list = new List<string>();
				_groups.Set(tag.Group, tag.Key, list);
			}
			list.Add(character.FolderName);
		}

		/// <summary>
		/// Gets all groups a character that has at least one other character
		/// </summary>
		/// <param name="group"></param>
		/// <param name="character"></param>
		/// <param name="max">Max amount of characters in the group to consider</param>
		/// <returns></returns>
		public static List<Tuple<string, List<Character>>> GetGroups(string group, Character character, int max)
		{
			List<Tuple<string, List<Character>>> output = new List<Tuple<string, List<Character>>>();
			List<string> tags = _characterGroups.Get(character.FolderName, group);
			if (tags == null) { return output; }

			Dictionary<string, List<string>> result;
			_groups.TryGetValue(group, out result);
			foreach (string tag in tags)
			{
				List<string> list = result.Get(tag);
				if (list != null && list.Count > 1 && list.Count <= max)
				{
					List<Character> finalList = new List<Character>();
					foreach (string key in list)
					{
						finalList.Add(CharacterDatabase.Get(key));
					}
					output.Add(new Tuple<string, List<Character>>(tag, finalList));
				}
			}
			return output;
		}

		public static Tuple<string, List<Character>> GetSmallestGroup(string group, Character character)
		{
			List<Tuple<string, List<Character>>> groups = GetGroups(group, character, 10);
			if (groups.Count == 0)
			{
				return null;
			}
			Tuple<string, List<Character>> min = null;
			foreach (Tuple<string, List<Character>> grp in groups)
			{
				if (min == null || grp.Item2.Count < min.Item2.Count)
				{
					min = grp;
				}
			}
			return min;
		}
	}
}

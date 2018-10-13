using System.Collections.Generic;
using System.IO;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Tracks information across all characters, even those not actively being edited
	/// </summary>
	public static class CharacterDatabase
	{
		public static List<Character> Characters = new List<Character>();

		/// <summary>
		/// Loads characters from a listing
		/// </summary>
		/// <param name="listing"></param>
		public static void Load()
		{
			string[] dirs = new string[] { Path.Combine(Config.GameDirectory, "opponents") };
			for (int d = 0; d < dirs.Length; d++)
			{
				string dir = dirs[d];
				foreach (string key in Directory.EnumerateDirectories(dir))
				{
					Character character = Serialization.ImportCharacter(key);
					if (character != null)
					{
						Characters.Add(character);
						for (int i = 0; i < character.Tags.Count; i++)
						{
							string tag = character.Tags[i].ToLowerInvariant();
							character.Tags[i] = tag;
							if (!string.IsNullOrEmpty(tag))
								TagDatabase.AddTag(tag);
						}
						if (!character.Tags.Contains(key))
						{
							TagDatabase.AddTag(character.DisplayName);
						}
					}
				}
			}
			Characters.Sort((c1, c2) => { return c1.FolderName.CompareTo(c2.FolderName); });
		}

		public static Character Get(string folderName)
		{
			return Characters.Find(ch => ch.FolderName == folderName);
		}

		public static bool Exists(string folderName)
		{
			return Characters.Exists(ch => ch.FolderName == folderName);
		}

		public static void Set(string folderName, Character character)
		{
			for (int i = 0; i < Characters.Count; i++)
			{
				if (Characters[i].FolderName == folderName)
				{
					Characters.RemoveAt(i);
					Characters.Insert(i, character);
					return;
				}
			}
			Characters.Add(character);
		}
	}
}

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
			string[] dirs = new string[] { Path.Combine(Config.GameDirectory, "opponents"),
											Path.Combine(Config.GameDirectory, "saves", "offline_opponents"),
											Path.Combine(Config.GameDirectory, "saves", "incomplete_opponents") };
			CharacterSource[] sources = new CharacterSource[] { CharacterSource.Main, CharacterSource.Offline, CharacterSource.Incomplete };
			for (int d = 0; d < dirs.Length; d++)
			{
				string dir = dirs[d];
				foreach (string key in Directory.EnumerateDirectories(dir))
				{
					Character character = Serialization.ImportCharacter(key, sources[d]);
					if (character != null)
					{
						Characters.Add(character);
						for (int i = 0; i < character.Tags.Count; i++)
						{
							string tag = character.Tags[i].ToLowerInvariant();
							character.Tags[i] = tag;
							TagDatabase.AddTag(tag);
						}
					}
				}
			}
		}

		public static Character Get(string folderName)
		{
			Character c = Characters.Find(ch => ch.FolderName == folderName);
			if (c != null)
				c.PrepareForEdit();
			return c;
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

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
			string dir = Path.Combine(Config.GameDirectory, "opponents");
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
						TagDatabase.AddTag(tag);
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

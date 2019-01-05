using Desktop;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Tracks information across all characters, even those not actively being edited
	/// </summary>
	public static class CharacterDatabase
	{
		public static IEnumerable<Character> Characters
		{
			get { return _characters; }
		}

		public static IEnumerable<Costume> Skins
		{
			get { return _reskins.Values; }
		}

		private static List<Character> _characters = new List<Character>();
		private static Dictionary<string, Character> _characterMap = new Dictionary<string, Character>();
		private static Dictionary<Character, CharacterEditorData> _editorData = new Dictionary<Character, CharacterEditorData>();
		private static Dictionary<string, Costume> _reskins = new Dictionary<string, Costume>();

		public static int Count
		{
			get { return _characters.Count; }
		}

		public static void Add(Character character)
		{
			_characters.Add(character);
			_characterMap[character.FolderName] = character;
		}

		public static Character GetRandom()
		{
			return _characters.GetRandom();
		}

		public static Character Get(string folderName)
		{
			return _characterMap.Get(folderName);
		}

		public static bool Exists(string folderName)
		{
			return _characterMap.ContainsKey(folderName);
		}

		public static void Set(string folderName, Character character)
		{
			_characterMap[folderName] = character;
			for (int i = 0; i < _characters.Count; i++)
			{
				if (_characters[i].FolderName == folderName)
				{
					_characters.RemoveAt(i);
					_characters.Insert(i, character);
					return;
				}
			}
			_characters.Add(character);
		}

		public static void AddEditorData(Character character, CharacterEditorData data)
		{
			data = data ?? new CharacterEditorData();
			data.LinkOwner(character);
			_editorData[character] = data;
		}

		public static CharacterEditorData GetEditorData(Character character)
		{
			return _editorData.GetOrAddDefault(character, () => new CharacterEditorData() { Owner = character.FolderName });
		}

		/// <summary>
		/// Record select filter for keeping out humans
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		public static bool FilterHuman(IRecord record)
		{
			return record.Key != "human";
		}

		public static void AddSkin(Costume skin)
		{
			_reskins[skin.Folder] = skin;
		}

		public static Costume GetSkin(string folder)
		{
			return _reskins.Get(folder);
		}
	}
}

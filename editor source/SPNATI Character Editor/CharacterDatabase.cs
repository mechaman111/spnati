using Desktop;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

		/// <summary>
		/// Returns only characters that aren't being filtered out
		/// </summary>
		public static IEnumerable<Character> FilteredCharacters
		{
			get
			{
				return Characters.Where(record =>
				{
					string status = Listing.Instance.GetCharacterStatus(record.Key);
					return !Config.StatusFilters.Contains(status);
				});
			}
		}

		public static IEnumerable<Costume> Skins
		{
			get { return _reskins.Values; }
		}

		private static List<Character> _characters = new List<Character>();
		private static Dictionary<string, Character> _characterMap = new Dictionary<string, Character>();
		private static Dictionary<string, Character> _idMap = new Dictionary<string, Character>();
		private static Dictionary<Character, CharacterEditorData> _editorData = new Dictionary<Character, CharacterEditorData>();
		private static Dictionary<string, Costume> _reskins = new Dictionary<string, Costume>();
		private static Dictionary<ISkin, PoseMatrix> _poseMatrices = new Dictionary<ISkin, PoseMatrix>();

		public static List<string> FailedCharacters = new List<string>();

		//If true, Get() will return a placeholder dummy for folders that physically exist but the character wasn't loaded yet. Only needed during initial load
		public static bool UsePlaceholders = false;

		public static int Count
		{
			get { return _characters.Count; }
		}

		public static void Clear()
		{
			_characters.Clear();
			_characterMap.Clear();
			_idMap.Clear();
			_editorData.Clear();
			_reskins.Clear();
		}

		public static void Add(Character character)
		{
			Character old;
			if (_characterMap.TryGetValue(character.FolderName, out old))
			{
				_characters.Remove(old);
			}
			_characters.Add(character);
			_characterMap[character.FolderName] = character;
			_idMap[GetId(character)] = character;
		}

		public static string GetId(Character character)
		{
			return GetId(character.FolderName);
		}
		public static string GetId(string id)
		{
			return Regex.Replace(id, @"\W", "");
		}

		public static Character GetRandom()
		{
			return _characters.GetRandom();
		}

		public static Character Get(string folderName)
		{
			Character character = _characterMap.Get(folderName);
			if (character == null && UsePlaceholders)
			{
				string path = Config.GetRootDirectory(folderName);
				if (Directory.Exists(path))
				{
					character = new PlaceholderCharacter();
					character.FolderName = folderName;
					_characterMap[folderName] = character;
				}
			}
			return character;
		}

		public static Character GetById(string id)
		{
			return _idMap.Get(id);
		}

		public static bool Exists(string folderName)
		{
			return _characterMap.ContainsKey(folderName);
		}

		public static void Set(string folderName, Character character)
		{
			_characterMap[folderName] = character;
			_idMap[GetId(character)] = character;
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
			if (character.Markers.IsValueCreated)
			{
				character.Markers.Value.Merge(data.Markers);
			}
		}

		public static CharacterEditorData GetEditorData(Character character)
		{
			if (character == null) { return new CharacterEditorData(); }
			return _editorData.GetOrAddDefault(character, () =>
			{
				CharacterEditorData data = new CharacterEditorData()
				{
					Owner = character.FolderName
				};
				data.LinkOwner(character);
				return data;
			});
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

		/// <summary>
		/// Record select filter for keeping out the default
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		public static bool FilterDefaultCostume(IRecord record)
		{
			return record.Key != "default";
		}

		public static void AddSkin(Costume skin)
		{
			_reskins[skin.Folder] = skin;
		}

		public static Costume GetSkin(string folder)
		{
			return _reskins.Get(folder);
		}

		public static CachedCharacter LoadFromCache(string path, out bool stale)
		{
			stale = false;
			string folderName = Path.GetFileName(path);
			string cachePath = Path.Combine(Config.AppDataDirectory, folderName, "cached.xml");
			CachedCharacter cachedCharacter = null;
			if (File.Exists(cachePath))
			{
				try
				{
					string behaviorPath = Path.Combine(Config.GetRootDirectory(folderName), "behaviour.xml");
					string editorPath = Path.Combine(Config.GetRootDirectory(folderName), "editor.xml");
					DateTime lastChange = File.GetLastWriteTimeUtc(behaviorPath);
					DateTime cacheDate = File.GetLastWriteTimeUtc(cachePath);
					bool editorNewer = false;
					if (File.Exists(editorPath) && cacheDate < File.GetLastWriteTimeUtc(editorPath))
					{
						editorNewer = true;
					}
					stale = cacheDate < lastChange || editorNewer;
					cachedCharacter = Serialization.ImportXml<CachedCharacter>(cachePath);
					if (cachedCharacter != null)
					{
						if (cachedCharacter.CacheVersion < CachedCharacter.CurrentVersion)
						{
							stale = true;
						}
					}
				}
				catch
				{
					cachedCharacter = null;
				}
			}

			if (cachedCharacter != null && !stale)
			{
				CharacterEditorData data = Serialization.ImportEditorData(folderName);
				AddEditorData(cachedCharacter, data);
			}

			return cachedCharacter;
		}

		/// <summary>
		/// Builds the cached version of a character
		/// </summary>
		/// <param name="folderName"></param>
		/// <param name="oldCache">If provided, a diff will be generated between the old and new</param>
		public static CachedCharacter CacheCharacter(string folderName, CachedCharacter oldCache)
		{
			string behaviorPath = Path.Combine(Config.GetRootDirectory(folderName), "behaviour.xml");
			if (File.Exists(behaviorPath))
			{
				using (Character realCharacter = Serialization.ImportCharacter(folderName))
				{
					if (realCharacter != null)
					{
						CachedCharacter cached = CacheCharacter(realCharacter);
						GlobalCache.CreateDiff(oldCache, cached);
						return cached;
					}
				}
			}
			return null;
		}
		/// <summary>
		/// Builds the cached version of a character
		/// </summary>
		/// <param name="character"></param>
		public static CachedCharacter CacheCharacter(Character character)
		{
			CachedCharacter c = new CachedCharacter(character);

			string outputDir = Path.Combine(Config.AppDataDirectory, character.FolderName);
			if (!Directory.Exists(outputDir))
			{
				Directory.CreateDirectory(outputDir);
			}
			string cachePath = Path.Combine(outputDir, "cached.xml");
			Serialization.ExportXml(c, cachePath);

			return c;
		}

		/// <summary>
		/// Loads all characters from their behaviours, but doesn't process them for full editing
		/// </summary>
		public static void LoadAll()
		{
			for (int i = 0; i < _characters.Count; i++)
			{
				if (!_characters[i].IsFullyLoaded)
				{
					Load(_characters[i].FolderName, i);
				}
			}
		}

		/// <summary>
		/// Loads all characters from their behaviours, but doesn't process them for full editing
		/// </summary>
		public static IEnumerator<Character> LoadAllIncrementally()
		{
			for (int i = 0; i < _characters.Count; i++)
			{
				if (!_characters[i].IsFullyLoaded)
				{
					yield return _characters[i];
					Load(_characters[i].FolderName, i);
				}
			}
		}

		/// <summary>
		/// Gets whether all characters have been loaded from their behaviors
		/// </summary>
		public static bool AllLoaded
		{
			get
			{
				for (int i = 0; i < _characters.Count; i++)
				{
					if (!_characters[i].IsFullyLoaded)
					{
						return false;
					}
				}
				return true;
			}
		}
		public static int UnloadedCount
		{
			get
			{
				int sum = 0;
				for (int i = 0; i < _characters.Count; i++)
				{
					if (!_characters[i].IsFullyLoaded)
					{
						sum++;
					}
				}
				return sum;
			}
		}

		/// <summary>
		/// Loads in a full character file, replacing the cached stub
		/// </summary>
		/// <param name="folderName"></param>
		/// <returns></returns>
		public static Character Load(string folderName, int index = -1)
		{
			Character c = Serialization.ImportCharacter(folderName);
			_characterMap[folderName] = c;
			if (index == -1)
			{
				for (int i = 0; i < _characters.Count; i++)
				{
					if (_characters[i].FolderName == folderName)
					{
						_characters[i] = c;
						break;
					}
				}
			}
			else
			{
				_characters[index] = c;
			}

			c.Metadata.AlternateSkins.ForEach(set =>
			{
				foreach (SkinLink link in set.Skins)
				{
					Costume skin = GetSkin(link.Folder);
					if (skin != null)
					{
						skin.Character = c;
						link.Costume = skin;
						skin.Link = link;
					}
				}
			});

			RecordLookup.ReplaceRecent(c);

			return c;
		}

		public static MarkerData LoadMarkerData(Character character)
		{
			string folderName = character.FolderName;
			MarkerData markers = new MarkerData();
			MarkerData markerXml = Serialization.ImportMarkerData(folderName);
			if (markerXml != null)
			{
				markers.Merge(markerXml);
			}
			CharacterEditorData editorData = GetEditorData(character);
			if (editorData != null)
			{
				markers.Merge(editorData.Markers);
			}
			return markers;
		}

		public static void AddPoseMatrix(ISkin skin, PoseMatrix matrix)
		{
			matrix.Character = skin;
			_poseMatrices[skin] = matrix;
		}

		public static PoseMatrix GetPoseMatrix(ISkin skin)
		{
			return GetPoseMatrix(skin, true);
		}

		/// <summary>
		/// Gets the pose matrix for a character/costume
		/// </summary>
		/// <param name="skin"></param>
		/// <returns></returns>
		public static PoseMatrix GetPoseMatrix(ISkin skin, bool loadFromDisk)
		{
			PoseMatrix matrix;
			if (!_poseMatrices.TryGetValue(skin, out matrix) && loadFromDisk)
			{
				//try to load it
				string filename = Path.Combine(skin.GetDirectory(), "poses.xml");
				if (File.Exists(filename))
				{
					matrix = Serialization.ImportXml<PoseMatrix>(filename);
				}
				if (matrix == null)
				{
					//create a new one
					matrix = new PoseMatrix();
				}

				//cache it
				AddPoseMatrix(skin, matrix);
			}

			return matrix;
		}
	}
}

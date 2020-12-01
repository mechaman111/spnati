using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Manages serialization of various objects to/from disk
	/// </summary>
	public static class Serialization
	{
		public static bool ExportListing(Listing listing, string name)
		{
			string dir = Path.Combine(Config.GetString(Settings.GameDirectory), "opponents");
			string filename = Path.Combine(dir, name);
			XmlSerializer serializer = new XmlSerializer(typeof(Listing), "");
			XmlWriter writer = null;
			try
			{
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.IndentChars = "\t";
				settings.Indent = true;
				settings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
				writer = XmlWriter.Create(filename, settings);
				serializer.Serialize(writer, listing);
				writer.Close();
			}
			catch (IOException e)
			{
				if (writer != null)
				{
					writer.Close();
				}
				MessageBox.Show(e.Message);
				return false;
			}
			return true;
		}
		
		/// <summary>
		/// Generates a character's xml files
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public static bool ExportCharacter(Character character)
		{
			character.PrepareForEdit();
			string dir = Config.GetRootDirectory(character);
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			bool backupEnabled = Config.BackupEnabled;
			if (backupEnabled)
			{
				CleanUpBackups(character);
			}

			string timestamp = GetTimeStamp();
			bool success = BackupAndExportXml(character, character, "behaviour", timestamp) &&
				BackupAndExportXml(character, character.Metadata, "meta", timestamp) &&
				BackupAndExportXml(character, CharacterDatabase.GetEditorData(character), "editor", timestamp) &&
				BackupAndExportXml(character, character.Collectibles, "collectibles", timestamp) &&
				CharacterHistory.Save(character);

			if (success && !string.IsNullOrEmpty(character.StyleSheetName))
			{
				CharacterStyleSheetSerializer.Save(character, character.Styles);
			}
			PoseMatrix matrix = CharacterDatabase.GetPoseMatrix(character, false);
			if (success)
			{
				if (matrix != null)
				{
					BackupAndExportXml(character, matrix, "poses", timestamp);
				}
				else
				{
					//if a poses file exists but hasn't been touched this session, copy it over
					BackupFile(character, "poses", timestamp);
				}
			}

			// clean up old files
			DeleteFile(character, "markers.xml");
			DeleteFile(character, "behaviour.edit.bak");
			DeleteFile(character, "meta.edit.bak");
			DeleteFile(character, "markers.edit.bak");
			DeleteFile(character, "editor.edit.bak");

			if (success)
			{
				character.IsDirty = false;
			}
			return success;
		}

		private static void CleanUpBackups(Character character)
		{
			string dir = Config.GetBackupDirectory(character);
			DateTime now = DateTime.Now;
			int minAge = Config.BackupPeriod;
			int maxAge = Config.BackupLifeTime * 1440;
			string oldestRecentFile = null;
			string oldestFileSuffix = null;
			double oldestAge = 0.0;
			if (Directory.Exists(dir))
			{
				List<string> obsoleteFiles = Directory.EnumerateFiles(dir, "*.bak", SearchOption.TopDirectoryOnly).Where(delegate (string file)
				{
					DateTime writeTime = File.GetLastWriteTime(file);
					TimeSpan age = now - writeTime;
					bool result = false;
					if (age.TotalMinutes > maxAge || age.TotalMinutes < minAge)
					{
						if (age.TotalMinutes < minAge && age.TotalMinutes > oldestAge)
						{
							oldestRecentFile = file;
							oldestAge = age.TotalMinutes;
						}
						result = true;
					}
					return result;
				}).ToList();
				if (oldestRecentFile != null)
				{
					oldestFileSuffix = Path.GetFileName(oldestRecentFile);
					oldestFileSuffix = oldestFileSuffix.Substring(oldestFileSuffix.IndexOf('-'));
				}
				foreach (string file in obsoleteFiles)
				{
					if (oldestFileSuffix == null || !file.EndsWith(oldestFileSuffix))
					{
						try
						{
							File.Delete(file);
						}
						catch (Exception e)
						{
							ErrorLog.LogError(e.Message);
						}
					}
				}
			}
		}

		private static void DeleteFile(Character character, string file)
		{
			string filePath = Path.Combine(Config.GetRootDirectory(character), file);
			if (File.Exists(filePath))
			{
				try
				{
					File.Delete(filePath);
				}
				catch { }
			}
		}

		private static string GetTimeStamp()
		{
			return DateTime.Now.ToString("yyyyMMddHHmmss");
		}

		private static bool BackupAndExportXml(Character character, object data, string name, string timestamp)
		{
			if (data == null) { return false; }
			string dir = Config.GetRootDirectory(character);
			string filename = Path.Combine(dir, name + ".xml");
			if (ExportXml(data, filename))
			{
				bool backupEnabled = Config.BackupEnabled;
				if (backupEnabled)
				{
					BackupFile(character, name, timestamp);
				}
				return true;
			}
			return false;
		}

		private static void BackupFile(Character character, string name, string timestamp)
		{
			string dir = Config.GetRootDirectory(character);
			string filename = Path.Combine(dir, name + ".xml");
			if (!File.Exists(filename))
			{
				return;
			}
			string backup = Config.GetBackupDirectory(character);
			if (!Directory.Exists(backup))
			{
				Directory.CreateDirectory(backup);
			}
			string backupFilename = Path.Combine(backup, $"{name}-{timestamp}.bak");
			try
			{
				if (File.Exists(backupFilename))
				{
					File.Delete(backupFilename);
				}

				File.Copy(filename, backupFilename);
			}
			catch { }
		}

		public static Listing ImportListing(string name)
		{
			string dir = Path.Combine(Config.GetString(Settings.GameDirectory), "opponents");
			string filename = Path.Combine(dir, name);
			if (!File.Exists(filename))
			{
				return new Listing();
			}
			TextReader reader = null;
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Listing), "");
				reader = new StreamReader(filename);
				Listing listing = serializer.Deserialize(reader) as Listing;
				return listing;
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		public static Character ImportCharacter(string folderName)
		{
			string folder = Config.GetRootDirectory(folderName);
			if (!Directory.Exists(folder))
				return null;

			string filename = Path.Combine(folder, "behaviour.xml");
			if (!File.Exists(filename))
			{
				return null;
			}
			Character character = ImportXml<Character>(filename);
			if (character == null)
			{
				return null;
			}

			if (string.IsNullOrEmpty(character.Version))
			{
				string contents = File.ReadAllText(filename);
				int editorIndex = contents.IndexOf("Character Editor");
				if (editorIndex >= 0)
				{
					character.Source = EditorSource.CharacterEditor;
					int atIndex = contents.IndexOf(" at ", editorIndex);
					if (atIndex >= 0)
					{
						int start = editorIndex + "Character Editor ".Length;
						int length = atIndex - start;
						if (length < 10)
						{
							string version = contents.Substring(start, length);
							character.Version = version;
						}
						character.Source = EditorSource.CharacterEditor;
					}
					else
					{
						character.Source = EditorSource.Other;
					}
				}
				else
				{
					int makeIndex = contents.IndexOf("make_xml.py version ");
					if (makeIndex >= 0)
					{
						character.Source = EditorSource.MakeXml;
					}
					else
					{
						character.Source = EditorSource.Other;
					}
				}
			}
			else
			{
				character.Source = EditorSource.CharacterEditor;
			}

			character.FolderName = Path.GetFileName(folderName);

			Metadata metadata = ImportMetadata(folderName);
			if (metadata == null)
				character.Metadata = new Metadata(character);
			else character.Metadata = metadata;

			CollectibleData collectibles = ImportCollectibles(folderName);
			if (collectibles != null)
			{
				character.Collectibles = collectibles;
			}

			CharacterEditorData editorData = ImportEditorData(folderName);
			CharacterDatabase.AddEditorData(character, editorData);

			return character;
		}

		/// <summary>
		/// Reverts a character to older versions of its files
		/// </summary>
		/// <param name="timestamp"></param>
		/// <returns></returns>
		public static Character RecoverCharacter(Character character, string timestamp)
		{
			string folder = Config.GetBackupDirectory(character);
			if (!Directory.Exists(folder))
			{
				return character;
			}
			Character recoveredCharacter = ImportXml<Character>(Path.Combine(folder, $"behaviour-{timestamp}.bak"));
			if (recoveredCharacter == null)
			{
				return character;
			}
			recoveredCharacter.FolderName = character.FolderName;

			Metadata meta = ImportXml<Metadata>(Path.Combine(folder, $"meta-{timestamp}.bak"));
			recoveredCharacter.Metadata = meta ?? character.Metadata;

			string markerFile = Path.Combine(folder, $"markers-{timestamp}.bak");
			if (File.Exists(markerFile))
			{
				MarkerData markers = ImportXml<MarkerData>(markerFile);
				if (markers != null)
				{
					recoveredCharacter.Markers = new Lazy<MarkerData>(() => markers);
				}
			}

			CharacterDatabase.Set(character.FolderName, recoveredCharacter);
			string editorFile = Path.Combine(folder, $"editor-{timestamp}.bak");
			if (File.Exists(editorFile))
			{
				CharacterEditorData editorData = ImportXml<CharacterEditorData>(editorFile);
				if (editorData != null)
				{
					CharacterDatabase.AddEditorData(recoveredCharacter, editorData);
				}
			}

			string posesFile = Path.Combine(folder, $"poses-{timestamp}.bak");
			if (File.Exists(posesFile))
			{
				PoseMatrix matrix = ImportXml<PoseMatrix>(posesFile);
				if (matrix != null)
				{
					CharacterDatabase.AddPoseMatrix(recoveredCharacter, matrix);
				}
			}

			return recoveredCharacter;
		}

		/// <summary>
		/// Imports an XML file
		/// </summary>
		/// <typeparam name="T">Type of data to ipmort</typeparam>
		/// <param name="filename">Filename to import</param>
		/// <returns>An object of type T or null if it failed</returns>
		public static T ImportXml<T>(string filename)
		{
			TextReader reader = null;
			try
			{
				//XML files can contain HTML-encoding characters which aren't recognized in real XML, so the file is preprocessed to decode these
				//before passing through the actual serializer
				string text = File.ReadAllText(filename);
				text = XMLHelper.Decode(text);

				//Also, italics are a funky case since they use invalid XML. Encode these to make the serializer happy, and then they will be switched back to the "bad" format in the character's OnAfterDeserialize
				text = XMLHelper.EncodeEntityReferences(text);

				//Now do the actual deserialization
				reader = new StringReader(text);
				XmlSerializer serializer = new XmlSerializer(typeof(T), "");
				T result = (T)serializer.Deserialize(reader);

				IHookSerialization hook = result as IHookSerialization;
				if (hook != null)
				{
					hook.OnAfterDeserialize(filename);
				}

				return result;
			}
			catch (Exception e)
			{
				ErrorLog.LogError("Error importing " + filename + ": " + e.Message + ": " + e.InnerException?.Message);
				return default(T);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		/// <summary>
		/// Exports an XML file
		/// </summary>
		/// <typeparam name="T">Type of data</typeparam>
		/// <param name="data">Data to serialize</param>
		/// <param name="filename">File name</param>
		/// <returns>True if successful</returns>
		public static bool ExportXml<T>(T data, string filename)
		{
			TextWriter writer = null;
			try
			{
				SpnatiXmlSerializer test = new SpnatiXmlSerializer();
				test.Serialize(filename, data);
				return true;
			}
			catch (IOException e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
				}
			}
		}

		private static Metadata ImportMetadata(string folderName)
		{
			string folder = Config.GetRootDirectory(folderName);
			if (!Directory.Exists(folder))
				return null;

			string filename = Path.Combine(folder, "meta.xml");
			if (!File.Exists(filename))
			{
				return null;
			}

			return ImportXml<Metadata>(filename);
		}

		public static TagList ImportTriggers()
		{
			string path = Path.Combine(Config.SpnatiDirectory, "opponents");
			string filename = Path.Combine(path, "dialogue_tags.xml");
			if (!File.Exists(filename))
			{
				//use the old location for backwards compatibility, though no one should ever need it except for me jumping across branches
				filename = "dialogue_tags.xml";
			}
			if (File.Exists(filename))
			{
				TextReader reader = null;
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(TagList), "");
					reader = new StreamReader(filename);
					TagList tagList = serializer.Deserialize(reader) as TagList;
					return tagList;
				}
				finally
				{
					if (reader != null)
						reader.Close();
				}
			}
			return null;
		}

		public static BackgroundList ImportBackgrounds()
		{
			string filename = Path.Combine(Config.SpnatiDirectory, "backgrounds.xml");
			if (File.Exists(filename))
			{
				TextReader reader = null;
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(BackgroundList), "");
					reader = new StreamReader(filename);
					return serializer.Deserialize(reader) as BackgroundList;
				}
				finally
				{
					if (reader != null)
					{
						reader.Close();
					}
				}
			}
			return null;
		}

		public static MarkerData ImportMarkerData(string folderName)
		{
			string folder = Config.GetRootDirectory(folderName);
			if (!Directory.Exists(folder))
				return null;

			string filename = Path.Combine(folder, "markers.xml");
			if (!File.Exists(filename))
			{
				return null;
			}

			return ImportXml<MarkerData>(filename);
		}

		public static CharacterEditorData ImportEditorData(string folderName)
		{
			string folder = Config.GetRootDirectory(folderName);
			if (!Directory.Exists(folder))
				return null;

			string filename = Path.Combine(folder, "editor.xml");
			if (!File.Exists(filename))
			{
				return null;
			}

			return ImportXml<CharacterEditorData>(filename);
		}

		public static CollectibleData ImportCollectibles(string folderName)
		{
			string folder = Config.GetRootDirectory(folderName);
			if (!Directory.Exists(folder))
				return null;

			string filename = Path.Combine(folder, "collectibles.xml");
			if (!File.Exists(filename))
			{
				return null;
			}

			return ImportXml<CollectibleData>(filename);
		}

		public static TagDictionary ImportTags()
		{
			string path = Path.Combine(Config.SpnatiDirectory, "opponents");
			string filename = Path.Combine(path, "tag_dictionary.xml");
			if (!File.Exists(filename))
			{
				//use the old location for backwards compatibility
				filename = Path.Combine(Config.ExecutableDirectory, "tag_dictionary.xml");
			}

			if (File.Exists(filename))
			{
				TextReader reader = null;
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(TagDictionary), "");
					reader = new StreamReader(filename);
					TagDictionary tagList = serializer.Deserialize(reader) as TagDictionary;
					tagList.CacheData();
					return tagList;
				}
				finally
				{
					if (reader != null)
						reader.Close();
				}
			}
			return null;
		}

		public static Costume ImportSkin(string folderName)
		{
			string folder = Config.GetRootDirectory(folderName);
			if (!Directory.Exists(folder))
				return null;

			string filename = Path.Combine(folder, "costume.xml");
			if (!File.Exists(filename))
			{
				return null;
			}
			Costume skin = ImportXml<Costume>(filename);
			return skin;
		}

		public static bool ExportSkin(Costume skin)
		{
			string folder = skin.Folder;
			if (string.IsNullOrEmpty(folder))
			{
				return false;
			}
			string dir = Path.Combine(Config.SpnatiDirectory, folder);
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			bool success = ExportXml(skin, Path.Combine(dir, "costume.xml"));
			PoseMatrix matrix = CharacterDatabase.GetPoseMatrix(skin, false);
			if (success)
			{
				if (matrix != null)
				{
					success = ExportXml(matrix, Path.Combine(dir, "poses.xml"));
				}
			}
			return success;
		}

		public static SpnatiConfig ImportConfig()
		{
			string dir = Config.GetString(Settings.GameDirectory);
			string filename = Path.Combine(dir, "config.xml");
			TextReader reader = null;
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(SpnatiConfig), "");
				reader = new StreamReader(filename);
				SpnatiConfig config = serializer.Deserialize(reader) as SpnatiConfig;
				return config;
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		public static bool ExportConfig()
		{
			string dir = Config.GetString(Settings.GameDirectory);
			string filename = Path.Combine(dir, "config.xml");
			XmlSerializer serializer = new XmlSerializer(typeof(SpnatiConfig), "");
			XmlWriter writer = null;
			try
			{
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.IndentChars = "\t";
				settings.Indent = true;
				settings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
				writer = XmlWriter.Create(filename, settings);
				serializer.Serialize(writer, SpnatiConfig.Instance);
				writer.Close();
			}
			catch (IOException e)
			{
				if (writer != null)
				{
					writer.Close();
				}
				MessageBox.Show(e.Message);
				return false;
			}
			return true;
		}

		/// <summary>
		/// Imports an xml file into an object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="file"></param>
		/// <returns></returns>
		public static T Import<T>(string file) where T : class
		{
			TextReader reader = null;
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T), "");
				reader = new StreamReader(file);
				T obj = serializer.Deserialize(reader) as T;
				return obj;
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

	}

	public interface IHookSerialization
	{
		void OnBeforeSerialize();
		void OnAfterDeserialize(string source);
	}
}

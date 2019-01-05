using SPNATI_Character_Editor.IO;
using System;
using System.IO;
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
		public static bool ExportListing(Listing listing)
		{
			string dir = Path.Combine(Config.GetString(Settings.GameDirectory), "opponents");
			string filename = Path.Combine(dir, "listing.xml");
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

				//Manually clean up the file to put the comments back. Doing this instead of a custom serializer since I'm lazy
				string contents = File.ReadAllText(filename);
				contents = contents.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
				int index = contents.IndexOf(">");
				contents = contents.Substring(0, index + 1) + "\r\n<!--\r\n This file contains listings for all of the opponents in the game.\r\n It is used to compile the opponents on the select screen.\r\n -->\r\n" + contents.Substring(index + 1);
				contents = contents.Replace("\t<individuals>", "\r\n\t<!-- Individual Listings -->\r\n\t<individuals>");
				contents = contents.Replace("\t<groups>", "\r\n\t<!-- Group Listings -->\r\n\t<groups>");
				File.WriteAllText(filename, contents);
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

		public static bool ExportCharacter(Character character)
		{
			string dir = Config.GetRootDirectory(character);
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			bool success = BackupAndExportXml(character, character, "behaviour") &&
				BackupAndExportXml(character, character.Metadata, "meta") &&
				BackupAndExportXml(character, character.Markers, "markers") &&
				BackupAndExportXml(character, CharacterDatabase.GetEditorData(character), "editor");
			return success;
		}

		private static bool BackupAndExportXml(Character character, object data, string name)
		{
			if (data == null) { return false; }
			string dir = Config.GetRootDirectory(character);
			string filename = Path.Combine(dir, name + ".xml");
			string backup = Path.Combine(dir, name + ".edit.bak");

			//Backup the existing file every 12 hours
			if (File.Exists(filename) && (!File.Exists(backup) || (DateTime.Now - File.GetLastWriteTime(backup)).TotalHours >= 12))
			{
				File.Delete(backup);
				File.Copy(filename, backup);
			}

			return ExportXml(data, filename);
		}

		public static Listing ImportListing()
		{
			string dir = Path.Combine(Config.GetString(Settings.GameDirectory), "opponents");
			string filename = Path.Combine(dir, "listing.xml");
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

			character.FolderName = Path.GetFileName(folderName);

			Metadata metadata = ImportMetadata(folderName);
			if (metadata == null)
				character.Metadata = new Metadata(character);
			else character.Metadata = metadata;

			MarkerData markers = ImportMarkerData(folderName); //should move this to the editor data at some point, but it would render markers.xml obsolete
			if (markers != null)
			{
				character.Markers.Merge(markers);
			}

			CharacterEditorData editorData = ImportEditorData(folderName);
			CharacterDatabase.AddEditorData(character, editorData);

			return character;
		}

		/// <summary>
		/// Imports an XML file
		/// </summary>
		/// <typeparam name="T">Type of data to ipmort</typeparam>
		/// <param name="filename">Filename to import</param>
		/// <returns>An object of type T or null if it failed</returns>
		private static T ImportXml<T>(string filename)
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
					hook.OnAfterDeserialize();
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
		private static bool ExportXml<T>(T data, string filename)
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

		private static MarkerData ImportMarkerData(string folderName)
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

		private static CharacterEditorData ImportEditorData(string folderName)
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

		public static TagDictionary ImportTags()
		{
			string filename = "tag_dictionary.xml";
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
			return success;
		}
	}

	public interface IHookSerialization
	{
		void OnBeforeSerialize();
		void OnAfterDeserialize();
	}
}

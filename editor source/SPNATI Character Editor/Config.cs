using System;
using System.Collections.Generic;
using System.IO;

namespace SPNATI_Character_Editor
{
	public static class Config
	{
		public const string Version = "v2.3";

		/// <summary>
		/// Gets the SPNATI directory
		/// </summary>
		public static string GameDirectory { get; set; }

		/// <summary>
		/// Gets the location where kkl.exe is found
		/// </summary>
		public static string KisekaeDirectory { get; set; }

		public static bool DisplayImages { get; set; }

		/// <summary>
		/// Gets the folder path for the last character edited
		/// </summary>
		public static string LastCharacter { get; set; }

		static Config()
		{
			string filename = Path.Combine(AppDataDirectory, "settings.ini");
			if (File.Exists(filename))
			{
				ReadSettings(filename);
			}
		}

		private static void ReadSettings(string file)
		{
			string[] lines = File.ReadAllLines(file);
			try
			{
				GameDirectory = lines[0];
				LastCharacter = lines[1];
				DisplayImages = true; // lines[2] == "1";
			}
			catch
			{
				DisplayImages = true;
			}
		}

		public static void Save()
		{
			string dataDir = AppDataDirectory;
			string filename = Path.Combine(dataDir, "settings.ini");
			if (!Directory.Exists(dataDir))
			{
				Directory.CreateDirectory(dataDir);
			}

			List<string> lines = new List<string>();
			lines.Add(GameDirectory);
			lines.Add(LastCharacter);
			lines.Add(DisplayImages ? "1" : "0");
			File.WriteAllLines(filename, lines);
		}

		public static string AppDataDirectory
		{
			get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SPNATI"); }
		}

		/// <summary>
		/// Retrieves the root directory for a character
		/// </summary>
		public static string GetRootDirectory(Character character)
		{
			if (GameDirectory == null || character == null || string.IsNullOrEmpty(character.FolderName))
				return "";
			return GetRootDirectory(character.FolderName);
		}

		/// <summary>
		/// Retrieves the full directory name for a folder
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public static string GetRootDirectory(string folder)
		{
			if (GameDirectory == null || folder == null)
				return "";
			return Path.Combine(GameDirectory, "opponents", folder);
		}
	}
}

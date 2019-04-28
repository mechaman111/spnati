using System;

namespace SPNATI_Character_Editor.Analyzers
{
	public class VersionAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "Version"; }
		}

		public string Name
		{
			get { return "Editor Version"; }
		}

		public string FullName
		{
			get { return "Created in editor"; }
		}

		public string ParentKey
		{
			get { return ""; }
		}

		public string[] GetValues()
		{
			string[] values = new string[Config.VersionHistory.Length + 2];
			values[0] = "make_xml.py";
			values[1] = "other";
			for (int i = 0; i < Config.VersionHistory.Length; i++)
			{
				values[i + 2] = Config.VersionHistory[i];
			}
			return values;
		}

		public Type GetValueType()
		{
			return typeof(int);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			if (value == "make_xml.py")
			{
				return character.Source == EditorSource.MakeXml;
			}
			else if (value == "other")
			{
				return character.Source == EditorSource.Other;
			}
			int targetVersion = -1;
			int characterVersion = -1;
			for (int i = 0; i < Config.VersionHistory.Length; i++)
			{
				string version = Config.VersionHistory[i];
				if (version == value)
				{
					targetVersion = i;
				}
				if (version == character.Version)
				{
					characterVersion = i;
				}
			}
			if (character.Source != EditorSource.CharacterEditor)
			{
				return false;
			}
			switch (op)
			{
				case "!=":
					return targetVersion != characterVersion;
				case ">":
					return characterVersion > targetVersion;
				case ">=":
					return characterVersion >= targetVersion;
				case "<":
					return characterVersion < targetVersion;
				case "<=":
					return characterVersion <= targetVersion;
				default:
					return characterVersion == targetVersion;
			}
		}
	}
}

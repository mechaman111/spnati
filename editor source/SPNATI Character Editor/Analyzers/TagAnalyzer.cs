using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Analyzers
{
	public class TagAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "TagUsage"; }
		}

		public string Name
		{
			get	{ return "Tags"; }
		}

		public string FullName
		{
			get { return "Tag"; }
		}

		public string ParentKey
		{
			get { return ""; }
		}

		public string[] GetValues()
		{
			List<string> tags = new List<string>();
			foreach (Tag record in TagDatabase.Tags)
			{
				tags.Add(record.Key);
			}
			return tags.ToArray();
		}

		public Type GetValueType()
		{
			return typeof(string);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			foreach (CharacterTag tag in character.Tags)
			{
				if (StringOperations.Matches(tag.Tag, op, value))
				{
					return true;
				}
			}
			return false;
		}
	}
}

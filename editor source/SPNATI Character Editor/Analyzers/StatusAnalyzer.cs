using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor.Analyzers
{
	public class StatusAnalyzer : IDataAnalyzer
	{
		public string Key
		{
			get { return "Status"; }
		}

		public string Name
		{
			get { return "Listing Status"; }
		}

		public string FullName
		{
			get { return "Listing Status"; }
		}

		public string ParentKey
		{
			get { return ""; }
		}

		public string[] GetValues()
		{
			string[] values = new string[] {
				"online",
				"offline",
				"incomplete",
				"testing",
				"unlisted"
			};
			return values;
		}

		public Type GetValueType()
		{
			return typeof(string);
		}

		public bool MeetsCriteria(Character character, string op, string value)
		{
			string status = "";
			switch (Listing.Instance.GetCharacterStatus(character.FolderName))
			{
				case OpponentStatus.Incomplete:
					status = "incomplete";
					break;
				case OpponentStatus.Main:
					status = "online";
					break;
				case OpponentStatus.Offline:
					status = "offline";
					break;
				case OpponentStatus.Testing:
					status = "testing";
					break;
				case OpponentStatus.Unlisted:
					status = "unlisted";
					break;
			}
			return StringOperations.Matches(status, op, value);
		}
	}
}

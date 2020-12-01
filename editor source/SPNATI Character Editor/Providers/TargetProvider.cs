using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class TargetProvider : IRecordProvider<TargetId>
	{
		private Case _context;

		public bool AllowsNew
		{
			get
			{
				return true;
			}
		}
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent
		{
			get
			{
				return false;
			}
		}

		public IRecord Create(string key)
		{
			return new TargetId(key, key, "Characters", "");
		}

		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}

		public ListViewItem FormatItem(IRecord record)
		{
			TargetId target = record as TargetId;
			return new ListViewItem(new string[] { target.Name, target.Key, target.Description });
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Target Select";
			info.Columns = new string[] { "Name", "Folder", "Description" };
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			if ("self".StartsWith(text))
			{
				list.Add(new TargetId("self", "Self", "Targeted", "Looks at this character's information"));
			}
			if ("target".StartsWith(text))
			{
				list.Add(new TargetId("target", "Target", "Targeted", "Looks at the current case's target's information if there is one"));
			}

			if (_context != null)
			{
				foreach (TargetCondition filter in _context.Conditions)
				{
					if (!string.IsNullOrEmpty(filter.Variable))
					{
						list.Add(new TargetId(filter.Variable, filter.Variable, "Filters", "Custom filter variable"));
					}
				}
			}

			foreach (Character record in CharacterDatabase.Characters)
			{
				string id = CharacterDatabase.GetId(record);
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text) || id.ToLower().Contains(text))
				{
					//partial match
					list.Add(new TargetId(CharacterDatabase.GetId(record), record.Name, "Characters", "Looks at " + record.FirstName + "'s information if they are in the game"));
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			_context = context as Case;
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort((record1, record2) => record1.CompareTo(record2));
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}

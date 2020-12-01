using Desktop;
using SPNATI_Character_Editor.DataStructures;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class CaseGroupProvider : IRecordProvider<CaseGroup>
	{
		public bool AllowsNew
		{
			get { return false; }
		}
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent
		{
			get { return false; }
		}

		public IRecord Create(string key)
		{
			throw new NotImplementedException();
		}

		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}

		public ListViewItem FormatItem(IRecord record)
		{
			return new ListViewItem(record.Name);
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (CaseGroup record in CaseDefinitionDatabase.Groups)
			{
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name" };
			info.ColumnWidths = new int[] { -2 };
			info.Caption = "Choose a Group";
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}

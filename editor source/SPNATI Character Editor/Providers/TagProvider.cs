using Desktop;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class TagProvider : IRecordProvider<Tag>
	{
		public bool AllowsNew
		{
			get { return true; }
		}
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent
		{
			get { return false; }
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (Tag record in TagDatabase.Dictionary.Tags)
			{
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Select a Tag";
			info.Columns = new string[] { "Name", "Value", "Description" };
		}
		
		public ListViewItem FormatItem(IRecord record)
		{
			Tag tag = record as Tag;
			return new ListViewItem(new string[] { tag.DisplayName, tag.Value, tag.Description });
		}

		public IRecord Create(string key)
		{
			return TagDatabase.Dictionary.AddTag(key);
		}

		public void Delete(IRecord record)
		{
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}

		public void SetContext(object context)
		{
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}

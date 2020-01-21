using Desktop.Skinning;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop.Providers
{
	public class SkinProvider : IRecordProvider<Skin>
	{
		public bool AllowsNew
		{
			get { return true; }
		}

		public bool TrackRecent
		{
			get { return false; }
		}

		public IRecord Create(string key)
		{
			Skin skin = new Skin();
			skin.Name = key;
			SkinManager.Instance.AvailableSkins.Add(skin);
			SkinManager.Instance.AvailableSkins.Sort();
			return skin;
		}

		public void Delete(IRecord record)
		{
			
		}

		public ListViewItem FormatItem(IRecord record)
		{
			ListViewItem item = new ListViewItem(record.Name, record.Group);
			return item;
		}

		public string[] GetColumns()
		{
			return new string[] { "Name", "Group" };
		}

		public virtual int[] GetColumnWidths()
		{
			return null;
		}

		public string GetLookupCaption()
		{
			return "Select Skin";
		}

		public List<IRecord> GetRecords(string text)
		{
			SkinManager skinManager = SkinManager.Instance;
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (Skin record in SkinManager.Instance.AvailableSkins)
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

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}

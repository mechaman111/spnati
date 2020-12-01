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
		public bool AllowsDelete { get { return false; } }

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

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name", "Group" };
			info.Caption = "Select Skin";
		}

		public ListViewItem FormatItem(IRecord record)
		{
			ListViewItem item = new ListViewItem(record.Name, record.Group);
			return item;
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			SkinManager skinManager = SkinManager.Instance;
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (Skin record in skinManager.AvailableSkins)
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

		public virtual bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}

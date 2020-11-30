using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace SPNATI_Character_Editor.Controls.Reference
{
	public partial class BackgroundGuide : UserControl
	{
		public static List<BackgroundReferenceRecord> Records = new List<BackgroundReferenceRecord>();

		public BackgroundGuide()
		{
			InitializeComponent();

			//cache backgrounds
			if (Records.Count == 0)
			{
				foreach (Background bkg in BackgroundDatabase.Backgrounds)
				{
					Records.Add(new BackgroundReferenceRecord(bkg));
				}
			}

			recValue.RecordType = typeof(BackgroundReferenceRecord);
		}

		private void recValue_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			BackgroundReferenceRecord record = recValue.Record as BackgroundReferenceRecord;
			grid.Rows.Clear();
			if (record != null)
			{
				foreach (Tuple<string, string> kvp in record.Values)
				{
					grid.Rows.Add(new object[] { kvp.Item1, kvp.Item2 });
				}
			}
		}
	}

	public class BackgroundReferenceRecord : IRecord
	{
		public BackgroundReferenceRecord(Background bkg)
		{
			Key = bkg.Id;
			foreach (XmlElement elem in bkg.Elements)
			{
				string key = elem.Name;
				string value = elem.InnerText;
				if (key == "name")
				{
					Name = value;
				}
				else if(!BackgroundDatabase.IsExcluded(key))
				{
					Values.Add(new Tuple<string, string>(key, value));
				}
			}
		}

		public string Name { get; set; }

		public string Key { get; set; }

		public string Group { get { return ""; } }

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public string ToLookupString()
		{
			return Name;
		}

		public List<Tuple<string, string>> Values = new List<Tuple<string, string>>();

		public override string ToString()
		{
			return Name;
		}
	}

	public class BackgroundReferenceProvider : IRecordProvider<BackgroundReferenceRecord>
	{
		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent { get { return false; } }

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

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Choose a Background";
			info.Columns = new string[] { "Name", "Value" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			return new ListViewItem(new string[] { record.Key, record.Name });
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (BackgroundReferenceRecord record in BackgroundGuide.Records)
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

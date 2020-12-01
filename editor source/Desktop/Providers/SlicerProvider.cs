using Desktop.Reporting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop.Providers
{
	public class SlicerDefinition : IRecord
	{
		public string Key { get { return Name; } set { Name = value; } }
		public string Name { get; set; }
		public string Group { get; set; }
		public string Description { get; set; }
		private Func<IDataSlicer> _creationFunc;

		public SlicerDefinition(string name, string description, Func<IDataSlicer> creator)
		{
			Name = name;
			Description = description;
			_creationFunc = creator;
		}

		public IDataSlicer CreateInstance(object context)
		{
			IDataSlicer slicer = _creationFunc();
			slicer.SetContext(context);
			return slicer;
		}

		public string ToLookupString()
		{
			return Name;
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}
	}

	public class SlicerProvider : IRecordProvider<SlicerDefinition>
	{
		private static List<SlicerDefinition> _slicers = new List<SlicerDefinition>();

		public static void AddSlicer(string name, string description, Func<IDataSlicer> creator)
		{
			SlicerDefinition def = new SlicerDefinition(name, description, creator);
			_slicers.Add(def);
		}

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

		public ListViewItem FormatItem(IRecord record)
		{
			SlicerDefinition slicer = record as SlicerDefinition;
			ListViewItem item = new ListViewItem(new string[] { record.Name, slicer.Description });
			return item;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name", "Description" };
			info.Caption = "Slice on Data";
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (SlicerDefinition record in _slicers)
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

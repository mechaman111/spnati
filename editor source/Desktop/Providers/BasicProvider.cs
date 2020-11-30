using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop
{
	public static class BasicRecordStash
	{
		private static Dictionary<Type, BasicRecord> _records = new Dictionary<Type, BasicRecord>();

		public static void Stash(BasicRecord record)
		{
			_records[record.GetType()] = record;
		}

		public static BasicRecord Retrieve<T>() where T : BasicRecord
		{
			BasicRecord record;
			if (!_records.TryGetValue(typeof(T), out record))
			{
				record = Activator.CreateInstance<T>() as BasicRecord;
				_records[record.GetType()] = record;
			}
			return record;
		}
	}

	public class BasicProvider<T> : IRecordProvider<T> where T : BasicRecord
	{
		public string GetLookupCaption()
		{
			return "Record Lookup";
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name" };
			info.Caption = "Record Lookup";
		}

		public IRecord Create(string key)
		{
			throw new NotImplementedException();
		}

		public void Delete(IRecord record)
		{
		}

		public void Sort(List<IRecord> record)
		{
			record.Sort();
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			BasicRecord record = BasicRecordStash.Retrieve<T>();
			record.Key = record.Key ?? text;
			record.Name = record.Name ?? text;
			List<IRecord> records = new List<IRecord>();
			records.Add(record);
			return records;
		}

		public bool AllowsNew
		{
			get { return false; }
		}
		public bool AllowsDelete
		{
			get { return false; }
		}

		public bool TrackRecent
		{
			get { return false; }
		}

		public ListViewItem FormatItem(IRecord record)
		{
			return new ListViewItem(record.ToLookupString());
		}

		public void SetContext(object context) { }

		public virtual bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}

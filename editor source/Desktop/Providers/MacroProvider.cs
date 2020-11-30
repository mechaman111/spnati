using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop.Providers
{
	public class MacroProvider : IRecordProvider<Macro>
	{
		private static DualKeyDictionary<Type, string, Macro> _macros = new DualKeyDictionary<Type, string, Macro>();

		private Type _filterType;
		public bool AllowsNew { get { return true; } }
		public bool AllowsDelete { get { return false; } }

		public IRecord Create(string key)
		{
			if (_filterType == null)
			{
				throw new Exception("Cannot create a Macro without a context");
			}
			int suffix = 0;
			string prefix = key;
			Dictionary<string, Macro> macros = _macros[_filterType];
			if (macros != null)
			{
				while (macros.ContainsKey(key))
				{
					key = prefix + ++suffix;
				}
			}
			Macro macro = new Macro();
			macro.Name = key;
			_macros.Set(_filterType, key, macro);
			return macro;
		}
		public void Delete(IRecord record) { }

		public void Remove(Type type, Macro macro)
		{
			_macros.Remove(type, macro.Name);
		}

		public void Add(Type type, Macro macro)
		{
			_macros.Set(type, macro.Name, macro);
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name" };
			info.ColumnWidths = new int[] { -2 };
			info.Caption = "Select a Macro";
		}

		public ListViewItem FormatItem(IRecord record)
		{
			Macro data = record as Macro;
			return new ListViewItem(new string[] { data.Name });
		}

		public bool TrackRecent
		{
			get { return false; }
		}

		public Macro Get(Type type, string name)
		{
			return _macros.Get(type, name);
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (KeyValuePair<Type, Dictionary<string, Macro>> kvp1 in _macros)
			{
				if (_filterType == null || kvp1.Key == _filterType)
				{
					foreach (KeyValuePair<string, Macro> kvp2 in kvp1.Value)
					{
						Macro record = kvp2.Value;
						if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
						{
							//partial match
							list.Add(record);
						}
					}
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			_filterType = (Type)context;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.Providers
{
	public class PropertyProvider : IRecordProvider<PropertyRecord>
	{
		private static DualKeyDictionary<Type, string, PropertyRecord> _editControls = new DualKeyDictionary<Type, string, PropertyRecord>();

		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }

		public IRecord Create(string key) { throw new NotImplementedException(); }
		public void Delete(IRecord record) { }

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (KeyValuePair<Type, Dictionary<string, PropertyRecord>> kvp1 in _editControls)
			{
				foreach (KeyValuePair<string, PropertyRecord> kvp2 in kvp1.Value)
				{
					PropertyRecord record = kvp2.Value;
					if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
					{
						//partial match
						list.Add(record);
					}
				}
			}
			return list;
		}

		public static IEnumerable<PropertyRecord> GetEditControls(Type dataType)
		{
			Dictionary<string, PropertyRecord> editControls;
			if (_editControls.TryGetValue(dataType, out editControls))
			{
				return editControls.Values;
			}
			return Enumerable.Empty<PropertyRecord>();
		}

		/// <summary>
		/// Finds all the edit controls for members of the given type
		/// </summary>
		/// <param name="dataType"></param>
		public static void BuildControlMap(Type dataType)
		{
			if (_editControls.ContainsPrimaryKey(dataType)) { return; }
			foreach (MemberInfo mi in dataType.GetMembers(BindingFlags.Public | BindingFlags.Instance))
			{
				EditControlAttribute attr = mi.GetCustomAttribute<EditControlAttribute>();
				if (attr != null)
				{
					_editControls.Set(dataType, mi.Name, new PropertyRecord(dataType, mi, attr));
				}
			}
		}

		public ListViewItem FormatItem(IRecord record)
		{
			PropertyRecord data = record as PropertyRecord;
			return new ListViewItem(new string[] { data.Name, data.Description });
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Select a Property";
			info.Columns = new string[] { "Type", "Description" };
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}

		public void SetContext(object context) { }

		public bool TrackRecent
		{
			get { return false; }
		}

		public virtual bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}
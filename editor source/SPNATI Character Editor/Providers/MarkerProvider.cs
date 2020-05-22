using Desktop;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class MarkerProvider : IRecordProvider<Marker>
	{
		private Character _character;
		private static List<Marker> _placeholders = new List<Marker>();

		public bool AllowsNew { get { return true; } }

		public bool TrackRecent
		{
			get { return false; }
		}

		public void SetContext(object context)
		{
			_character = context as Character;
		}

		public IRecord Create(string key)
		{
			Marker marker = new Marker();
			marker.Key = key;
			if (key.StartsWith("$"))
			{
				_placeholders.Add(marker);
			}
			else
			{
				if (_character != null)
				{
					if (_character.Markers.Value.Contains(key))
					{
						return _character.Markers.Value.Get(key);
					}
					_character.Markers.Value.Add(marker);
				}
			}
			return marker;
		}

		public void Delete(IRecord record) { }

		public ListViewItem FormatItem(IRecord record)
		{
			Marker marker = record as Marker;
			return new ListViewItem(new string[] { marker.Name, marker.Description });
		}

		public string[] GetColumns()
		{
			return new string[] { "Name", "Description" };
		}

		public int[] GetColumnWidths()
		{
			return null;
		}

		public string GetLookupCaption()
		{
			return "Select a Marker";
		}
		public List<IRecord> GetRecords(string text)
		{
			text = text.ToLower();
			var list = new List<IRecord>();
			foreach (Marker m in _placeholders)
			{
				if (m.Key.ToLower().Contains(text) || m.Name.ToLower().Contains(text))
				{
					list.Add(m);
				}
			}
			if (_character == null) { return list; }

			foreach (Marker record in _character.Markers.Value.Values)
			{
				if (string.IsNullOrEmpty(record.Key))
				{
					continue;
				}
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}

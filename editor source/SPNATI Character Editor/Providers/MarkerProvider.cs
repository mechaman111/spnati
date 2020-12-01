using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class MarkerProvider : IRecordProvider<Marker>
	{
		private Character _character;
		private static List<Marker> _placeholders = new List<Marker>();
		private bool _excludeCharacter = false;

		public bool AllowsNew { get { return true; } }
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent
		{
			get { return false; }
		}

		public void SetContext(object context)
		{
			_excludeCharacter = false;
			_character = null;
			if (context is Character)
			{
				_character = context as Character;
			}
			else if (context is Tuple<Character, bool>)
			{
				Tuple<Character, bool> c = context as Tuple<Character, bool>;
				_character = c.Item1;
				_excludeCharacter = c.Item2;
			}
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

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Select a Marker";
			info.Columns = new string[] { "Name", "Description" };
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
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

			if (_excludeCharacter)
			{
				HashSet<string> otherMarkers = new HashSet<string>();
				foreach (Character c in CharacterDatabase.FilteredCharacters)
				{
					if (c != _character)
					{
						foreach (Marker m in c.Markers.Value.Values)
						{
							if (m.Scope == MarkerScope.Public && !otherMarkers.Contains(m.Name) && !string.IsNullOrEmpty(m.Name))
							{
								otherMarkers.Add(m.Name);
								if (m.Key.ToLower().Contains(text) || m.Name.ToLower().Contains(text))
								{
									//partial match
									list.Add(m);
								}
							}							
						}
					}
				}
			}
			else
			{
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

using Desktop;
using SPNATI_Character_Editor.DataStructures;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class CollectibleProvider : IRecordProvider<Collectible>
	{
		private Character _character;

		public bool AllowsNew { get { return true; } }
		public bool AllowsDelete { get { return false; } }

		private static ObservableCollection<Collectible> _genericCollectibles = new ObservableCollection<Collectible>();

		public void SetContext(object context)
		{
			_character = context as Character;
		}

		public bool TrackRecent
		{
			get { return false; }
		}

		public IRecord Create(string key)
		{
			Collectible collectible = new Collectible();
			collectible.Key = key;
			collectible.Title = key;
			if (_character != null)
			{
				_character.Collectibles.Add(collectible);
			}
			else
			{
				_genericCollectibles.Add(collectible);
			}
			return collectible;
		}

		public void Delete(IRecord record)
		{
			if (_character != null)
			{
				Collectible collectible = record as Collectible;
				_character.Collectibles.Remove(collectible);
			}
		}

		public ListViewItem FormatItem(IRecord record)
		{
			Collectible collectible = record as Collectible;
			return new ListViewItem(new string[] { collectible.Id, collectible.Name, collectible.Subtitle });
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Select a Collectible";
			info.Columns = new string[] { "ID", "Title", "Subtitle" };
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			ObservableCollection<Collectible> source = _genericCollectibles;
			var list = new List<IRecord>();

			if (_character != null)
			{
				source = _character.Collectibles.Collectibles;
			}

			foreach (Collectible record in source)
			{
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

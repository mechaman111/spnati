using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class CharacterProvider : IRecordProvider<Character>
	{
		public string GetLookupCaption()
		{
			return "Character Select";
		}

		public bool AllowsNew
		{
			get	{ return true; }
		}

		public IRecord Create(string key)
		{
			Character c = new Character();
			c.FirstName = key;
			c.Label = key;
			c.FolderName = key;

			//Add in some barebones data to be at the minimal functional level
			c.Wardrobe.Add(new Clothing() { FormalName = "Final Layer", GenericName = "final layer", Position = "lower", Type = "important" });
			c.Wardrobe.Add(new Clothing() { FormalName = "First Layer", GenericName = "first layer", Position = "upper", Type = "important" });
			c.Behavior.EnsureDefaults(c);

			Serialization.ExportCharacter(c);
			CharacterDatabase.Add(c);

			//Add to the listing under testing status
			c.Metadata.Enabled = true;
			Listing.Instance.Characters.Add(new Opponent(c.FolderName, OpponentStatus.Testing));
			Serialization.ExportListing(Listing.Instance);

			return c;
		}

		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}

		public List<IRecord> GetRecords(string text)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (Character record in CharacterDatabase.Characters)
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
			list.Sort((record1, record2) => record1.CompareTo(record2));	
		}

		public string[] GetColumns()
		{
			return new string[] { "Name", "Folder" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			return new ListViewItem(new string[] { record.Name, record.Key });
		}

		public void SetContext(object context)
		{
		}
	}
}

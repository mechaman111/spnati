using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class CostumeProvider : IRecordProvider<Costume>
	{
		public bool AllowsNew
		{
			get { return true; }
		}
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent
		{
			get { return true; }
		}

		public IRecord Create(string key)
		{
			key = key.ToLower();
			Costume skin = new Costume();
			skin.Id = key;

			Character owner = null;
			//try to identify the owner automatically
			string[] pieces = key.Split('_');
			foreach (string piece in pieces)
			{
				owner = CharacterDatabase.Get(piece);
				if (owner != null)
				{
					break;
				}
			}

			string searchText = owner?.FolderName ?? "";
			owner = RecordLookup.DoLookup(typeof(Character), searchText, false, CharacterDatabase.FilterHuman, true, skin) as Character;
			if (owner == null)
			{
				return null;
			}
			if (!owner.IsFullyLoaded)
			{
				owner = CharacterDatabase.Load(owner.FolderName);
			}

			//force the key to fit a certain pattern
			if (!key.StartsWith(owner.FolderName + "_"))
			{
				key = key.Replace(owner.FolderName, "").Trim('_');
				key = owner.FolderName + "_" + key;
				skin.Id = key;
				MessageBox.Show($"This outfit has been renamed to {key}.");
			}

			string folder = $"opponents/reskins/{key}/";
			skin.Folders.Add(new StageSpecificValue(0, folder));

			skin.Tags.Add(new SkinTag("alternative_skin"));
			TagDatabase.AddTag(skin.Id);

			//Link up basic information with the source character
			skin.LinkCharacter(owner);

			skin.Labels.Add(new StageSpecificValue(0, owner.Label));

			//Duplicate the wardrobe
			foreach (Clothing item in owner.Wardrobe)
			{
				skin.Wardrobe.Add(item.Copy());
			}

			Serialization.ExportSkin(skin);
			Serialization.ExportCharacter(owner);
			CharacterDatabase.AddSkin(skin);

			return skin;
		}

		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (Costume record in CharacterDatabase.Skins)
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
			list.Sort((record1, record2) =>
			{
				Costume c1 = record1 as Costume;
				Costume c2 = record2 as Costume;
				Character chr1 = c1.Character;
				Character chr2 = c2.Character;
				int compare = 0;
				if (chr1 != null && chr2 == null)
				{
					compare = -1;
				}
				else if (chr1 == null && chr2 != null)
				{
					compare = 1;
				}
				else if (chr1 != null && chr2 != null)
				{
					compare = chr1.CompareTo(chr2);
				}
				if (compare == 0)
				{
					compare = c1.Name.CompareTo(c2.Name);
				}
				return compare;
			});
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Skin Select";
			info.Columns = new string[] { "Name", "ID", "Character", "Folder" };
		}

		public int[] GetColumnWidths()
		{
			return null;
		}

		public ListViewItem FormatItem(IRecord record)
		{
			Costume skin = record as Costume;
			return new ListViewItem(new string[] { skin.Name, skin.Id, skin.Character?.ToString(), skin.Folder });
		}

		public void SetContext(object context)
		{
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}

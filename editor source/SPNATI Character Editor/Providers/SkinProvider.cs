using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class SkinProvider : IRecordProvider<Costume>
	{
		public string GetLookupCaption()
		{
			return "Skin Select";
		}

		public bool AllowsNew
		{
			get { return true; }
		}

		public IRecord Create(string key)
		{
			Costume skin = new Costume();
			skin.Id = key;

			IRecord record = RecordLookup.DoLookup(typeof(Character), "", false, CharacterDatabase.FilterHuman, skin);
			if (record == null)
			{
				return null;
			}

			string folder = $"opponents/reskins/{key}/";
			skin.Folders.Add(new StageSpecificValue(0, folder));

			skin.Tags.Add(new SkinTag("alternative_skin"));
			TagDatabase.AddTag(skin.Id);

			//Link up basic information with the source character
			Character owner = record as Character;
			SkinLink link = null;
			AlternateSkin existing = owner.Metadata.AlternateSkins.Find(s =>
			{
				return s.Skins.Find(l =>
				{
					if (l.Folder == folder)
					{
						link = l;
						return true;
					}
					return false;
				}) != null;
			});
			if (link == null)
			{
				if (owner.Metadata.AlternateSkins.Count == 0)
				{
					owner.Metadata.AlternateSkins.Add(new AlternateSkin());
				}
				link = new SkinLink()
				{
					Folder = folder,
					Name = key, //can be renamed later
				};
				owner.Metadata.AlternateSkins[0].Skins.Add(link);
			}

			skin.Labels.Add(new StageSpecificValue(0, owner.Label));
			skin.Character = owner;
			link.Costume = skin;
			skin.Link = link;

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

		public List<IRecord> GetRecords(string text)
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
			list.Sort((record1, record2) => record1.CompareTo(record2));
		}

		public string[] GetColumns()
		{
			return new string[] { "Name", "Folder" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			Costume skin = record as Costume;
			return new ListViewItem(new string[] { skin.Id, skin.Folder });
		}

		public void SetContext(object context)
		{
		}
	}
}

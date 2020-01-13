using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class CharacterProvider : IRecordProvider<Character>
	{
		private Costume _skinContext;
		private Character _characterContext;

		public string GetLookupCaption()
		{
			if (_skinContext != null)
			{
				return "Choose a character for which the reskin belongs";
			}
			return "Character Select";
		}

		public bool AllowsNew
		{
			get { return true; }
		}

		public bool TrackRecent
		{
			get { return true; }
		}

		public IRecord Create(string key)
		{
			Character c = new Character();
			c.FirstName = key;
			c.Label = key;
			c.FolderName = key.ToLower();
			c.IsNew = true;

			if (_characterContext == null)
			{
				//adding a new character
				c.Metadata.Writer = Config.UserName;

				//Add in some barebones data to be at the minimal functional level
				c.Wardrobe.Add(new Clothing() { GenericName = "", Name = "final layer", Position = "lower", Type = "important" });
				c.Wardrobe.Add(new Clothing() { GenericName = "", Name = "first layer", Position = "upper", Type = "important" });
				c.Behavior.EnsureDefaults(c);

				c.Intelligence.Add(new StageSpecificValue(0, "average"));

				Serialization.ExportCharacter(c);
				CharacterDatabase.Add(c);

				//Add to the listing under testing status
				Listing.Instance.Characters.Add(new Opponent(c.FolderName, OpponentStatus.Testing));
				Serialization.ExportListing(Listing.Instance);
			}
			else
			{
				//see if the character actually exists already and use that one instead
				Character existing = CharacterDatabase.Get(key);
				if (existing != null)
				{
					return existing;
				}
				//otherwise, make a placeholder for this session
				CharacterDatabase.Add(c);
			}
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
			return new string[] { "Name", "Folder", "Last Update", "Source" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			OpponentStatus status = Listing.Instance.GetCharacterStatus(record.Key);
			Character c = record as Character;
			string lastUpdate = GetTimeSince(c.LastUpdate, DateTime.Now);
			return new ListViewItem(new string[] { record.Name, record.Key, lastUpdate, status == OpponentStatus.Testing || status == OpponentStatus.Main ? "" : status.ToString() });
		}

		public void SetContext(object context)
		{
			_characterContext = context as Character;
			_skinContext = context as Costume;
		}

		public static string GetTimeSince(DateTime date, DateTime since)
		{
			TimeSpan diff = date - since;
			if (date <= since)
			{
				if (diff.Days <= -7)
				{
					int days = -diff.Days;
					int weeks = days / 7;
					int months = weeks / 4;
					if (months >= 12)
					{
						return "Over a year ago";
					}
					else if (months == 0)
					{
						return $"{weeks} {(weeks == 1 ? "week" : "weeks")} ago";
					}
					return $"{months} {(months == 1 ? "month" : "months")} ago";
				}
				else
				{
					if (diff.Days < 0)
					{
						return $"{-diff.Days} {(diff.Days == -1 ? "day" : "days")} ago";
					}
					else
					{
						if (diff.Hours < 0)
						{
							return $"{-diff.Hours} {(diff.Hours == -1 ? "hour" : "hours")} ago";
						}
						else
						{
							if (diff.Minutes >= 0)
							{
								return "Just now";
							}
							return $"{-diff.Minutes} {(diff.Minutes == -1 ? "minute" : "minutes")} ago";
						}
					}
				}
			}
			return "In the future";
		}
	}
}

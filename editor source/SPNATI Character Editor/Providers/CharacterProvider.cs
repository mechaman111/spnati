using Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class CharacterProvider : IRecordProvider<Character>
	{
		private Costume _skinContext;
		private Character _characterContext;

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

		public void SetFormatInfo(LookupFormat info)
		{
			info.Columns = new string[] { "Name", "Folder", "Last Update", "Writer", "Status" };
			if (_skinContext != null)
			{
				info.Caption = "Choose a character for which the reskin belongs";
			}
			info.Caption = "Character Select";
			info.ExtraFields.Add("Writer:");
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			string writer = "";
			List<IRecord> list = new List<IRecord>();
			if (args.ExtraText != null && args.ExtraText.Length > 0)
			{
				writer = args.ExtraText[0]?.ToLower() ?? "";
			}
			foreach (Character record in CharacterDatabase.Characters)
			{
				Character c = record as Character;
				if (!string.IsNullOrEmpty(writer))
				{
					string actualWriter = c.Metadata.Writer?.ToLower() ?? "";
					if (!actualWriter.Contains(writer))
					{
						continue;
					}
				}
				if (NameMatches(c.Name, text) ||
					NameMatches(c.FolderName, text) ||
					c.Labels.Any(l => NameMatches(l.Value, text)))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		private bool NameMatches(string name, string expected)
		{
			return (name?.ToLower() ?? "").Contains(expected);
		}

		public bool FilterFromUI(IRecord record)
		{
			string status = Listing.Instance.GetCharacterStatus(record.Key);
			return Config.StatusFilters.Contains(status);
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort((record1, record2) => record1.CompareTo(record2));
		}

		public ListViewItem FormatItem(IRecord record)
		{
			string status = Listing.Instance.GetCharacterStatus(record.Key) ?? "";
			Character c = record as Character;
			DateTime updateTime = DateTimeOffset.FromUnixTimeMilliseconds(c.Metadata.LastUpdate).DateTime.ToLocalTime();
			string lastUpdate = GetTimeSince(updateTime, DateTime.Now);
			return new ListViewItem(new string[] { record.Name, record.Key, lastUpdate, c.Metadata.Writer, status == OpponentStatus.Testing || status == OpponentStatus.Main ? "" : status });
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
						if (date.Year < 2000)
						{
							return "";
						}
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

using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class FilterTypeProvider : IRecordProvider<FilterType>
	{
		private static List<FilterType> _types;
		private Case _context;

		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }
		public bool TrackRecent { get { return false; } }

		static FilterTypeProvider()
		{
			_types = new List<FilterType>()
			{
				new FilterType("any", "Anyone", "General",  "Match zero or more of anyone in the game")
				{
					CanSpecifyRange = true,
					CanSpecifyCharacter = false,
				},
				new FilterType("self", "Self", "General", "Looks at this character's information")
				{
					CanSpecifyRange = false,
					CanSpecifyCharacter = false,
				},
				new FilterType("target", "Target", "General", "Looks at the case's target's information, if there is a target")
				{
					CanSpecifyRange = false,
					CanSpecifyCharacter = true,
					RequiresTarget = true,
				},
				new FilterType("opp", "Opponent", "General", "Match zero or more characters other than the speaker")
				{
					CanSpecifyRange = true,
					CanSpecifyCharacter = true,
				},
				new FilterType("other", "Also Playing", "General", "Match zero or more characters who are neither the current target nor the speaker")
				{
					CanSpecifyRange = true,
					CanSpecifyCharacter = true,
				},
				new FilterType("winner", "Winner", "General", "Look at the winner of the most recent hand")
				{
					CanSpecifyRange = false,
					CanSpecifyCharacter = true,
				},
			};
		}

		public IRecord Create(string key)
		{
			throw new NotImplementedException();
		}
		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}

		public ListViewItem FormatItem(IRecord record)
		{
			FilterType target = record as FilterType;
			return new ListViewItem(new string[] { target.Name, target.Key, target.Description });
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Match Condition On";
			info.Columns = new string[] { "Name", "Key", "Description" };
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (FilterType type in _types)
			{
				if (type.RequiresTarget)
				{
					if (_context != null)
					{
						TriggerDefinition trigger = TriggerDatabase.GetTrigger(_context.Tag);
						if (trigger != null)
						{
							if (!trigger.HasTarget)
							{
								continue;
							}
						}
					}
				}
				if (type.Key.Contains(text) || type.Name.ToLower().Contains(text))
				{
					list.Add(type);
				}
			}

			foreach (Character record in CharacterDatabase.Characters)
			{
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
				{
					//partial match
					list.Add(new FilterType(record.Key, record.Name, "Specific", "Looks at " + record.FirstName + "'s information if they are in the game")
					{
						CanSpecifyCharacter = false,
						CanSpecifyRange = false,
						IsCharacter = true,
					});
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			_context = context as Case;
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort((record1, record2) =>
			{
				FilterType f1 = record1 as FilterType;
				FilterType f2 = record2 as FilterType;
				int compare = f1.Group.CompareTo(f2.Group);
				if (compare == 0)
				{
					compare = record1.CompareTo(record2);
				}
				return compare;
			});
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}

	public class FilterType : IRecord
	{
		public string Group { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public bool CanSpecifyCharacter;
		public bool CanSpecifyRange;
		public bool RequiresTarget;
		public bool IsCharacter;

		public FilterType(string key, string name, string group, string description)
		{
			Group = group;
			Key = key;
			Name = name;
			Description = description;
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}

		public string ToLookupString()
		{
			return Name;
		}
	}
}

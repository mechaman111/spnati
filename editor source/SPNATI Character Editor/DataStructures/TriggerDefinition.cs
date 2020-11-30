using Desktop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Case tag metadata
	/// </summary>
	public class TriggerDefinition : IRecord
	{
		public const string StartTrigger = "-";

		[XmlAttribute("tag")]
		public string Tag;

		[XmlAttribute("optional")]
		public bool Optional;

		[XmlAttribute("start")]
		public int StartStage;

		[XmlAttribute("end")]
		public int EndStage;

		[XmlAttribute("label")]
		public string Label;

		[XmlAttribute("safeLabel")]
		public string SafeLabel;

		[XmlAttribute("description")]
		public string HelpText;

		[XmlArray("vars")]
		public List<string> AvailableVariables = new List<string>();

		[XmlArray("bulk")]
		public List<string> BulkPairs = new List<string>();

		[XmlElement("defaultImage")]
		public string DefaultImage;

		[XmlElement("defaultText")]
		public string DefaultText;

		[XmlAttribute("hasTarget")]
		public bool HasTarget;

		[XmlAttribute("color")]
		public int ColorScheme
		{
			get;
			set;
		}

		/// <summary>
		/// Generic trigger that satisfies this as a default
		/// </summary>
		[XmlElement("generic")]
		public string GenericTrigger;

		/// <summary>
		/// Triggers that are considered having their default met if this trigger has a default
		/// </summary>
		public List<string> LinkedTriggers = new List<string>();

		/// <summary>
		/// This trigger occurs a maximum of one time per stage for a character
		/// </summary>
		[XmlAttribute("oncePerStage")]
		public bool OncePerStage;

		/// <summary>
		/// What character gender this trigger is associated with
		/// </summary>
		[XmlAttribute("gender")]
		public string Gender;

		/// <summary>
		/// What character "size" this trigger is associated with
		/// </summary>
		[XmlAttribute("size")]
		public string Size;

		[XmlIgnore]
		public bool Unrecognized { get; set; }

		public string Name
		{
			get { return Label; }
		}

		public string Key
		{
			get { return Tag; }
			set { Tag = value; }
		}

		string IRecord.Group
		{
			get { return ""; }
		}

		/// <summary>
		/// Used by the editor to group tags that correspond to the same phase (ex. must_strip_normal and must_strip_winning)
		/// </summary>
		[XmlAttribute("relatedGroup")]
		public int RelatedGroup;

		#region Formatting help for txt export
		[XmlAttribute("group")]
		/// <summary>
		/// Group to place within the text export
		/// </summary>
		public int Group;

		[XmlAttribute("order")]
		/// <summary>
		/// Order within the group
		/// </summary>
		public int GroupOrder;

		[XmlAttribute("spacer")]
		/// <summary>
		/// If true, an empty line should be placed before this line in the export
		/// </summary>
		public bool Spacer;

		[XmlAttribute("previousStage")]
		/// <summary>
		/// If true, the case will be grouped with the stage before it
		/// </summary>
		public bool GroupWithPreviousStage;

		[XmlAttribute("noPrefix")]
		/// <summary>
		/// If true, the stage prefix should be left off
		/// </summary>
		public bool NoPrefix;
		#endregion

		public TriggerDefinition()
		{
		}

		public TriggerDefinition(string tag, string name)
		{
			Tag = tag;
			Label = name;
		}

		public override string ToString()
		{
			return Config.SafeMode ? SafeLabel ?? Label : Label;
		}

		public string ToLookupString()
		{
			return ToString();
		}

		public int CompareTo(IRecord other)
		{
			return TriggerDatabase.Compare(Key, other.Key);
		}
	}

	[XmlRoot("tags")]
	public class TagList
	{
		[XmlArray("triggers")]
		[XmlArrayItem("trigger")]
		public List<TriggerDefinition> Tags = new List<TriggerDefinition>();

		[XmlArray("groups")]
		[XmlArrayItem("textGroup")]
		public List<TextGroup> Groups = new List<TextGroup>();
	}

	public class TextGroup
	{
		[XmlAttribute("id")]
		public int Id;
		[XmlAttribute("name")]
		public string Name;
		[XmlAttribute("safeName")]
		public string SafeName;
		[XmlElement("description")]
		public string Description;
		[XmlAttribute("future")]
		public bool AppliesToNextStage;
	}

	public static class TriggerDatabase
	{
		private static Dictionary<string, TriggerDefinition> _triggers = new Dictionary<string, TriggerDefinition>();
		private static List<string> _tagOrder = new List<string>();

		private static Dictionary<int, TextGroup> _groups = new Dictionary<int, TextGroup>();

		public static Func<string, Character, int, bool> FakeUsedInStage;

		public static List<string> GetTags()
		{
			List<string> values = new List<string>();
			foreach (var trigger in _triggers.Values)
				values.Add(trigger.Tag);
			return values;
		}

		public static void Clear()
		{
			_triggers.Clear();
			_tagOrder.Clear();
			_groups.Clear();
		}

		public static IEnumerable<TextGroup> Groups
		{
			get
			{
				return _groups.Values;
			}
		}

		public static List<TriggerDefinition> Triggers
		{
			get
			{
				List<TriggerDefinition> triggers = new List<TriggerDefinition>();
				triggers.AddRange(_triggers.Values);
				return triggers;
			}
		}

		public static void Load()
		{
			TriggerDefinition start = new TriggerDefinition(TriggerDefinition.StartTrigger, "Start Game")
			{
				StartStage = -1,
				EndStage = -1
			};
			AddTrigger(start);

			TagList list = Serialization.ImportTriggers();
			if (list != null)
			{
				foreach (var trigger in list.Tags)
				{
					AddTrigger(trigger);
				}
				foreach (var group in list.Groups)
				{
					AddGroup(group);
				}
				foreach (TriggerDefinition trigger in _triggers.Values)
				{
					if (!string.IsNullOrEmpty(trigger.GenericTrigger))
					{
						TriggerDefinition generic = GetTrigger(trigger.GenericTrigger);
						generic.LinkedTriggers.Add(trigger.Tag);
					}
				}
			}
		}

		public static int Compare(string tag1, string tag2)
		{
			TriggerDefinition t1 = GetTrigger(tag1);
			TriggerDefinition t2 = GetTrigger(tag2);
			int compare = t1.Group.CompareTo(t2.Group);
			if (compare == 0)
			{
				compare = t1.GroupOrder.CompareTo(t2.GroupOrder);
			}
			if (compare == 0)
			{
				compare = tag1.CompareTo(tag2);
			}
			return compare;
		}

		public static void AddGroup(TextGroup group)
		{
			_groups[group.Id] = group;
		}

		public static void AddTrigger(TriggerDefinition t)
		{
			_triggers[t.Tag] = t;
			_tagOrder.Add(t.Tag);
		}

		public static TriggerDefinition GetTrigger(string tag)
		{
			if (string.IsNullOrEmpty(tag))
				return null;
			TriggerDefinition trigger;
			if (!_triggers.TryGetValue(tag, out trigger))
			{
				return new TriggerDefinition()
				{
					Tag = tag,
					Unrecognized = true
				};
			}
			return trigger;
		}

		/// <summary>
		/// Gets whether a variable can be used with a certain tag.
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="variable"></param>
		/// <returns></returns>
		public static bool IsVariableAvailable(string tag, string variable)
		{
			TriggerDefinition trigger;
			string[] pieces = variable.ToLower().Split('.');
			string varName = pieces[0];
			if (varName.ToLower() == "clothes")
				varName = "clothing";
			Variable v = VariableDatabase.Get(varName);
			if (v != null && v.IsGlobal)
				return true;
			if (_triggers.TryGetValue(tag, out trigger))
			{
				return trigger.AvailableVariables.Contains(varName);
			}
			return false;
		}

		public static string GetLabel(string tag)
		{
			TriggerDefinition trigger;
			if (_triggers.TryGetValue(tag, out trigger))
			{
				return trigger.Label;
			}
			return tag;
		}

		public static string GetHelpText(string tag)
		{
			TriggerDefinition trigger;
			if (_triggers.TryGetValue(tag, out trigger))
			{
				return trigger.HelpText;
			}
			return "";
		}

		/// <summary>
		/// Converts a character-specific stage to a general stage. This is useful for using a global index for post-loss stages even though they vary per character.
		/// </summary>
		/// <param name="character"></param>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static int ToStandardStage(Character character, int stage)
		{
			int layers = character.Layers;
			if (stage == 0 || layers == Clothing.MaxLayers)
				return stage;
			return ShiftStage(character, stage);
		}

		/// <summary>
		/// Upshifts stages so that it's based off of MaxLayers
		/// </summary>
		/// <param name="character"></param>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static int ShiftStage(Character character, int stage)
		{
			int layers = character.Layers;
			//Shift forwards so MaxLayers is the final stage before losing
			int shiftAmount = Clothing.MaxLayers - layers;
			return stage + shiftAmount;
		}

		/// <summary>
		/// Converts a character-specific stage into the format make_xml.py expects
		/// </summary>
		/// <param name="character"></param>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static int ToFlatFileStage(Character character, int stage)
		{
			int layers = character.Layers;
			if (stage >= layers)
			{
				stage -= (layers + Clothing.ExtraStages);
			}
			return stage;
		}

		/// <summary>
		/// Gets whether a certain trigger gets used while the player is in a particular stage
		/// </summary>
		/// <param name="character"></param>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static bool UsedInStage(string tag, Character character, int stage)
		{
			if (FakeUsedInStage != null)
				return FakeUsedInStage(tag, character, stage);

			TriggerDefinition trigger;
			if (string.IsNullOrEmpty(tag))
				return false;
			if (!_triggers.TryGetValue(tag, out trigger))
				return false;
			int standardStage = ToStandardStage(character, stage);
			return standardStage >= trigger.StartStage && standardStage <= trigger.EndStage;
		}

		/// <summary>
		/// Gets the description for an text export group
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetGroupDescription(int id)
		{
			TextGroup group;
			if (_groups.TryGetValue(id, out group))
				return group.Description;
			return "";
		}

		/// <summary>
		/// Gets the short name for a text export group
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetGroupName(int id)
		{
			TextGroup group;
			if (_groups.TryGetValue(id, out group))
			{
				if (Config.SafeMode)
				{
					return group.SafeName ?? group.Name;
				}
				return group.Name;
			}
			return "";
		}

		public static bool GroupAppliesToNextStage(int id)
		{
			TextGroup group;
			if (_groups.TryGetValue(id, out group))
				return group.AppliesToNextStage;
			return false;
		}

		/// <summary>
		/// Gets whether two tags are referring to the same phase, just under different conditions (ex. must_strip_winning vs must_strip_losing)
		/// </summary>
		/// <param name="tag1"></param>
		/// <param name="tag2"></param>
		/// <returns></returns>
		public static bool AreRelated(string tag1, string tag2)
		{
			if (tag1 == tag2) { return true; }
			TriggerDefinition t1 = GetTrigger(tag1);
			TriggerDefinition t2 = GetTrigger(tag2);
			return t1.RelatedGroup == t2.RelatedGroup && t2.RelatedGroup > 0;
		}
	}

	public class TriggerProvider : IRecordProvider<TriggerDefinition>
	{
		public bool AllowsNew { get { return false; } }
		public bool AllowsDelete { get { return false; } }

		public bool TrackRecent { get { return false; } }

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
			ListViewItem item = new ListViewItem(new string[] { record.Key, record.Name });
			return item;
		}

		public void SetFormatInfo(LookupFormat info)
		{
			info.Caption = "Select a Case Type";
			info.Columns = new string[] { "Tag", "Label" };
		}

		public List<IRecord> GetRecords(string text, LookupArgs args)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (TriggerDefinition record in TriggerDatabase.Triggers)
			{
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
			
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort((r1, r2) =>
			{
				TriggerDefinition t1 = r1 as TriggerDefinition;
				TriggerDefinition t2 = r2 as TriggerDefinition;
				return TriggerDatabase.Compare(t1.Tag, t2.Tag);
			});
		}

		public bool FilterFromUI(IRecord record)
		{
			return false;
		}
	}
}

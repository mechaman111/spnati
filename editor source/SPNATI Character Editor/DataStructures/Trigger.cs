using SPNATI_Character_Editor.Activities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Case tag metadata
	/// </summary>
	public class Trigger
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

		public Trigger()
		{
		}

		public Trigger(string tag, string name)
		{
			Tag = tag;
			Label = name;
		}

		public override string ToString()
		{
			return Label;
		}
	}

	[XmlRoot("tags")]
	public class TagList
	{
		[XmlArray("triggers")]
		[XmlArrayItem("trigger")]
		public List<Trigger> Tags = new List<Trigger>();

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
		[XmlElement("description")]
		public string Description;
		[XmlAttribute("future")]
		public bool AppliesToNextStage;
	}

	public static class TriggerDatabase
	{
		private static Dictionary<string, Trigger> _triggers = new Dictionary<string, Trigger>();
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

		public static List<Trigger> Triggers
		{
			get
			{
				List<Trigger> triggers = new List<Trigger>();
				triggers.AddRange(_triggers.Values);
				return triggers;
			}
		}

		public static void Load()
		{
			Trigger start = new Trigger(Trigger.StartTrigger, "Start Game")
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
			}
		}

		public static int Compare(string tag1, string tag2)
		{
			int index1 = _tagOrder.IndexOf(tag1);
			int index2 = _tagOrder.IndexOf(tag2);
			return index1.CompareTo(index2);
		}

		public static void AddGroup(TextGroup group)
		{
			_groups[group.Id] = group;
		}

		public static void AddTrigger(Trigger t)
		{
			_triggers[t.Tag] = t;
			_tagOrder.Add(t.Tag);
		}

		public static Trigger GetTrigger(string tag)
		{
			if (string.IsNullOrEmpty(tag))
				return null;
			Trigger trigger;
			if (!_triggers.TryGetValue(tag, out trigger))
			{
				return new Trigger()
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
			Trigger trigger;
			string varName = variable.ToLower();
			if (varName.ToLower() == "clothes")
				varName = "clothing";
			Variable v = VariableDatabase.Get(varName);
			if (v != null && v.IsGlobal)
				return true;
			if (_triggers.TryGetValue(tag, out trigger))
			{
				return trigger.AvailableVariables.Contains(variable.ToLower());
			}
			return false;
		}

		public static string GetLabel(string tag)
		{
			Trigger trigger;
			if (_triggers.TryGetValue(tag, out trigger))
			{
				return trigger.Label;
			}
			return tag;
		}

		public static string GetHelpText(string tag)
		{
			Trigger trigger;
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
			if (stage == 0 || layers == 8)
				return stage;
			return ShiftStage(character, stage);
		}

		/// <summary>
		/// Upshifts stages so that it's based off of 8 layers
		/// </summary>
		/// <param name="character"></param>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static int ShiftStage(Character character, int stage)
		{
			int layers = character.Layers;
			//Shift forwards so 8 is the final stage before losing
			int shiftAmount = 8 - layers;
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

			Trigger trigger;
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
				return group.Name;
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
			Trigger t1 = GetTrigger(tag1);
			Trigger t2 = GetTrigger(tag2);
			return t1.RelatedGroup == t2.RelatedGroup && t2.RelatedGroup > 0;
		}
	}
}

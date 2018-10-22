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

		#region Formatting help for xml export
		[XmlAttribute("xmlGroup")]
		/// <summary>
		/// Group for sorting in the xml output
		/// </summary>
		/// <remarks>This order comes from observing Python's hashing, since make_xml.py just puts tags into a dictionary and prints them out in whatever order that ends up being</remarks>
		public int XmlGroup;

		[XmlAttribute("xmlOrder")]
		/// <summary>
		/// Order within group for sorting
		/// </summary>
		public int XmlOrder;
		#endregion

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
			if (variable.ToLower() == "player")
				return true;
			if (variable.ToLower() == "clothes")
				variable = "clothing";
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
		/// Gets the tag opposite another one. Ex. female_removing_minor <-> stripping
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static string GetOppositeTag(string tag, Character character, int stage)
		{
			//TODO: Move this into dialogue_tags.xml instead of hardcoding
			if (tag == "must_strip_winning" || tag == "must_strip_normal" || tag == "must_strip_losing")
			{
				return character.Gender + "_must_strip";
			}
			else if (tag == "stripping")
			{
				Clothing layer = character.Wardrobe[stage];
				string type = layer.Type;
				if (type == "extra")
					type = "accessory";
				else if (type == "important")
				{
					if (layer.Position == "upper")
					{
						return character.Gender + "_chest_will_be_visible";
					}
					else
					{
						return character.Gender + "_crotch_will_be_visible";
					}
				}
				return character.Gender + "_removing_" + type;
			}
			else if (tag == "stripped")
			{
				Clothing layer = character.Wardrobe[stage - 1];
				string type = layer.Type;
				if (type == "extra")
					type = "accessory";
				else if (type == "important")
				{
					if (layer.Position == "upper")
					{
						if (character.Gender == "male")
						{
							return "male_chest_is_visible";
						}
						else
						{
							return "female_" + character.Size + "_chest_is_visible";
						}
					}
					else
					{
						if (character.Gender == "male")
						{
							return "male_" + character.Size + "_crotch_is_visible";
						}
						else
						{
							return "female_chest_is_visible";
						}
					}
				}
				return character.Gender + "_removed_" + type;
			}
			else if (tag == "must_masturbate_first" || tag == "must_masturbate")
			{
				return character.Gender + "_must_masturbate";
			}
			else if (tag == "start_masturbating")
			{
				return character.Gender + "_start_masturbating";
			}
			else if (tag == "masturbating" || tag == "heavy_masturbating")
			{
				return character.Gender + "_masturbating";
			}
			else if (tag == "finished_masturbating")
			{
				return character.Gender + "_finished_masturbating";
			}
			else if (tag.Contains("_must_strip"))
			{
				return "must_strip_normal";
			}
			else if (tag.Contains("_removing_") || tag.Contains("_will_be_visible"))
			{
				return "stripping";
			}
			else if (tag.Contains("_removed_") || tag.Contains("_is_visible"))
			{
				return "stripped";
			}
			else if (tag.EndsWith("male_must_masturbate"))
			{
				return "must_masturbate";
			}
			else if (tag.EndsWith("male_start_masturbating"))
			{
				return "start_masturbating";
			}
			else if (tag.EndsWith("male_masturbating"))
			{
				return "masturbating";
			}
			else if (tag.EndsWith("male_finished_masturbating"))
			{
				return "finished_masturbating";
			}
			else if (tag == "game_over_defeat")
			{
				return "game_over_victory";
			}
			else if (tag == "game_over_victory")
			{
				return "game_over_defeat";
			}

			return null; //there is no opposite
		}

		/// <summary>
		/// Gets the game phase corresponding to a tag
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static GamePhase GetPhase(string tag)
		{
			if (tag == "swap_cards")
				return GamePhase.ExchangingCards;
			else if (tag == "must_strip_winning" || tag == "must_strip_normal" || tag == "must_strip_losing")
				return GamePhase.BeforeLoss;
			else if (tag.EndsWith("must_strip"))
				return GamePhase.BeforeLoss;
			else if (tag.Contains("_removing_") || tag.Contains("_will_be_visible") || tag == "stripping")
				return GamePhase.DuringLoss;
			else if (tag.Contains("_removed_") || tag.Contains("_is_visible") || tag == "stripped")
				return GamePhase.AfterLoss;
			else if (tag.Contains("must_masturbate"))
				return GamePhase.BeforeLoss;
			else if (tag.Contains("start_masturbating"))
				return GamePhase.DuringLoss;
			else if (tag == "heavy_masturbating")
				return GamePhase.HeavyMasturbating;
			else if (tag.Contains("finishing"))
				return GamePhase.Finishing;
			else if (tag.Contains("finished"))
				return GamePhase.Finished;
			else if (tag == "good_hand")
				return GamePhase.GoodHand;
			else if (tag == "bad_hand")
				return GamePhase.BadHand;
			else if (tag == "okay_hand")
				return GamePhase.OkayHand;
			else if (tag == "masturbating" || tag == "male_masturbating" || tag == "female_masturbating")
				return GamePhase.Masturbating;
			else if (tag.Contains("game_over"))
				return GamePhase.GameOver;

			return GamePhase.Start;
		}
	}
}

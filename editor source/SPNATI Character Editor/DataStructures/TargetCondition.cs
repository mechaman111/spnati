using Desktop;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using Newtonsoft.Json;
using SPNATI_Character_Editor.Controls.EditControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class TargetCondition : BindableObject
	{
		[DefaultValue("")]
		[XmlAttribute("count")]
		[JsonProperty("count")]
		/// <summary>
		/// Number of characters needing the filter tag
		/// </summary>
		public string Count
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[RecordSelect(DisplayName = "Tag", GroupName = "Metadata", GroupOrder = 10, Description = "Character has the given tag", RecordType = typeof(Tag), UseAutoComplete = true)]
		[XmlAttribute("filter")]
		[JsonProperty("filter")]
		/// <summary>
		/// Tag to condition on
		/// </summary>
		public string FilterTag
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[ComboBox(DisplayName = "Gender", GroupName = "Metadata", GroupOrder = 5, Description = "Character's gender", Options = new string[] { "female", "male" })]
		[JsonProperty("gender")]
		[XmlAttribute("gender")]
		public string Gender
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[Status(DisplayName = "Status", GroupName = "Status", GroupOrder = 10, Description = "Current status of the characters to match")]
		[XmlAttribute("status")]
		[JsonProperty("status")]
		public string Status
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("role")]
		[JsonProperty("role")]
		public string Role
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("character")]
		[JsonProperty("character")]
		public string Character
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[StageSelect(DisplayName = "Stage", GroupName = "Status", GroupOrder = 40, Description = "Character is within a range of stages", BoundProperties = new string[] { "Character", "Role" })]
		[XmlAttribute("stage")]
		[JsonProperty("stage")]
		public string Stage
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("var")]
		[JsonProperty("var")]
		public string Variable
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[NumericRange(DisplayName = "Remaining Layers", GroupName = "Status", GroupOrder = 50, Description = "Number of layers the character has left")]
		[DefaultValue("")]
		[XmlAttribute("layers")]
		[JsonProperty("layers")]
		public string Layers
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[NumericRange(DisplayName = "Starting Layers", GroupName = "Metadata", GroupOrder = 55, Description = "Number of layers the character started with")]
		[DefaultValue("")]
		[XmlAttribute("startingLayers")]
		[JsonProperty("startingLayers")]
		public string StartingLayers
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[NumericRange(DisplayName = "Time in Stage", GroupName = "Status", GroupOrder = 60, Description = "Number of rounds since the last time this character lost a hand")]
		[DefaultValue("")]
		[XmlAttribute("timeInStage")]
		[JsonProperty("timeInStage")]
		public string TimeInStage
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[ComboBox(DisplayName = "Has Hand", GroupName = "Game", GroupOrder = 65, Description = "Character has a particular poker hand",
					Options = new string[] { "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush" })]
		[DefaultValue("")]
		[XmlAttribute("hasHand")]
		[JsonProperty("hasHand")]
		public string Hand
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[NumericRange(DisplayName = "Consecutive Losses", GroupName = "Game", GroupOrder = 63, Description = "Number of hands the character has lost in a row")]
		[DefaultValue("")]
		[XmlAttribute("consecutiveLosses")]
		[JsonProperty("consecutiveLosses")]
		public string ConsecutiveLosses
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[MarkerCondition(DisplayName = "Said Marker", GroupName = "Dialogue", GroupOrder = 45, Description = "Character has said a marker", ShowPrivate = false, BoundProperties = new string[] { "Character" })]
		[DefaultValue("")]
		[XmlAttribute("saidMarker")]
		[JsonProperty("saidMarker")]
		public string SaidMarker
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[MarkerCondition(DisplayName = "Saying Marker", GroupName = "Dialogue", GroupOrder = 46, Description = "Character is saying a marker", ShowPrivate = false, BoundProperties = new string[] { "Character" })]
		[DefaultValue("")]
		[XmlAttribute("sayingMarker")]
		[JsonProperty("sayingMarker")]
		public string SayingMarker
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Marker(DisplayName = "Not Said Marker", GroupName = "Dialogue", GroupOrder = 47, Description = "Character has not said a marker", ShowPrivate = false, BoundProperties = new string[] { "Character" })]
		[DefaultValue("")]
		[XmlAttribute("notSaidMarker")]
		[JsonProperty("notSaidMarker")]
		public string NotSaidMarker
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[Text(DisplayName = "Saying Text", GroupName = "Dialogue", GroupOrder = 48, Description = "Character is saying some text at this very moment")]
		[DefaultValue("")]
		[XmlAttribute("saying")]
		[JsonProperty("saying")]
		public string Saying
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		[PoseMatch(DisplayName = "Pose", GroupName = "Dialogue", GroupOrder = 50, Description ="Character is using a particular pose")]
		[DefaultValue("")]
		[XmlAttribute("pose")]
		[JsonProperty("pose")]
		public string Pose
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public static readonly KeyValuePair<string, string>[] StatusTypes = {
			new KeyValuePair<string, string>(null, ""),
			new KeyValuePair<string, string>("lost_some", "Lost something"),
			new KeyValuePair<string, string>("mostly_clothed", "Lost only accessories"),
			new KeyValuePair<string, string>("decent", "Still covered by major articles"),
			new KeyValuePair<string, string>("exposed", "Chest and/or crotch visible"),
			new KeyValuePair<string, string>("chest_visible", "Chest visible"),
			new KeyValuePair<string, string>("crotch_visible", "Crotch visible"),
			new KeyValuePair<string, string>("topless", "Topless (not naked)"),
			new KeyValuePair<string, string>("bottomless", "Bottomless (not naked)"),
			new KeyValuePair<string, string>("naked", "Naked (fully exposed)"),
			new KeyValuePair<string, string>("lost_all", "Lost all layers"),
			new KeyValuePair<string, string>("alive", "Still in the game"),
			new KeyValuePair<string, string>("masturbating", "Masturbating"),
			new KeyValuePair<string, string>("finished", "Finished masturbating")
		};

		public static readonly KeyValuePair<string, string>[] SafeStatusTypes = {
			new KeyValuePair<string, string>(null, ""),
			new KeyValuePair<string, string>("lost_some", "Lost something"),
			new KeyValuePair<string, string>("mostly_clothed", "Lost only accessories"),
			new KeyValuePair<string, string>("decent", "Still covered by major articles"),
			new KeyValuePair<string, string>("exposed", "Bottom and/or top visible"),
			new KeyValuePair<string, string>("chest_visible", "Top visible"),
			new KeyValuePair<string, string>("crotch_visible", "Bottom visible"),
			new KeyValuePair<string, string>("topless", "No top"),
			new KeyValuePair<string, string>("bottomless", "No bottom"),
			new KeyValuePair<string, string>("naked", "No top or bottom"),
			new KeyValuePair<string, string>("lost_all", "Lost all layers"),
			new KeyValuePair<string, string>("alive", "Still in the game"),
			new KeyValuePair<string, string>("masturbating", "Forfeiting"),
			new KeyValuePair<string, string>("finished", "Finished forfeit")
		};

		public TargetCondition()
		{
		}

		public TargetCondition(string tag, string gender, string status, string count)
		{
			FilterTag = tag;
			Gender = gender;
			Status = status;
			Count = count;
		}

		public TargetCondition(string serializedData, string count)
		{
			Count = count;

			string[] parts = serializedData.Split('&');
			foreach (string part in parts)
			{
				if (part.Contains(";"))
				{
					string[] pieces = part.Split(new char[] { ';' }, 2);
					if (pieces.Length == 2)
					{
						string key = pieces[0];
						string value = pieces[1];
						switch (key)
						{
							case "var":
								Variable = value;
								break;
							case "stage":
								Stage = value;
								break;
							case "character":
								Character = value;
								break;
							case "role":
								Role = value;
								break;
							case "saying":
								Saying = value;
								break;
							case "sayingmarker":
								SayingMarker = value;
								break;
							case "saidmarker":
								SaidMarker = value;
								break;
							case "notsaidmarker":
								NotSaidMarker = value;
								break;
							case "timeinstage":
								TimeInStage = value;
								break;
							case "losses":
								ConsecutiveLosses = value;
								break;
							case "layers":
								Layers = value;
								break;
							case "startinglayers":
								StartingLayers = value;
								break;
							case "hashand":
								Hand = value;
								break;
							case "pose":
								Pose = value;
								break;
						}
					}
				}
				else
				{
					if (part == "male" || part == "female")
					{
						Gender = part;
					}
					else if (part != "" && Array.Exists(StatusTypes, t => t.Key == part || "not_" + t.Key == part))
					{
						Status = part;
					}
					else
					{
						FilterTag = part;
					}
				}
			}
		}

		public override bool Equals(object obj)
		{
			TargetCondition other = obj as TargetCondition;
			if (other == null)
			{
				return false;
			}
			return FilterTag == other.FilterTag &&
				(Count ?? "") == (other.Count ?? "") &&
				Status == other.Status &&
				Gender == other.Gender &&
				Hand == other.Hand &&
				Role == other.Role &&
				Character == other.Character &&
				Stage == other.Stage &&
				TimeInStage == other.TimeInStage &&
				Layers == other.Layers &&
				StartingLayers == other.StartingLayers &&
				SaidMarker == other.SaidMarker &&
				NotSaidMarker == other.NotSaidMarker &&
				SayingMarker == other.SayingMarker &&
				Saying == other.Saying &&
				ConsecutiveLosses == other.ConsecutiveLosses &&
				Pose == other.Pose &&
				(Variable ?? "") == (other.Variable ?? "");
		}

		public override int GetHashCode()
		{
			int hash = (FilterTag ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Gender ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Status ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Count ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Hand ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Role ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Character ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Stage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TimeInStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Layers ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (StartingLayers ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (SaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (NotSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (SayingMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Saying ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (ConsecutiveLosses ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Variable ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Pose ?? string.Empty).GetHashCode();
			return hash;
		}

		public void ClearEmptyValues()
		{
			if (FilterTag == "")
				FilterTag = null;
			if (Gender == "")
				Gender = null;
			if (Status == "")
				Status = null;
		}

		public TargetCondition Copy()
		{
			TargetCondition copy = new TargetCondition();
			CopyPropertiesInto(copy);
			return copy;
		}

		public string Serialize()
		{
			List<string> parts = new List<string>();
			if (!string.IsNullOrEmpty(Status))
			{
				parts.Add(Status);
			}
			if (!string.IsNullOrEmpty(Gender))
			{
				parts.Add(Gender);
			}
			if (!string.IsNullOrEmpty(FilterTag))
			{
				parts.Add(FilterTag);
			}
			if (!string.IsNullOrEmpty(Role))
			{
				parts.Add("role;" + Role);
			}
			if (!string.IsNullOrEmpty(Character))
			{
				parts.Add("character;" + Character);
			}
			if (!string.IsNullOrEmpty(Stage))
			{
				parts.Add("stage;" + Stage);
			}
			if (!string.IsNullOrEmpty(Variable))
			{
				parts.Add("var;" + Character);
			}
			if (!string.IsNullOrEmpty(Layers))
			{
				parts.Add("layers;" + Layers);
			}
			if (!string.IsNullOrEmpty(StartingLayers))
			{
				parts.Add("startinglayers;" + StartingLayers);
			}
			if (!string.IsNullOrEmpty(TimeInStage))
			{
				parts.Add("timeinstage;" + TimeInStage);
			}
			if (!string.IsNullOrEmpty(ConsecutiveLosses))
			{
				parts.Add("losses;" + ConsecutiveLosses);
			}
			if (!string.IsNullOrEmpty(Hand))
			{
				parts.Add("hashand;" + Hand);
			}
			if (!string.IsNullOrEmpty(SaidMarker))
			{
				parts.Add("saidmarker;" + SaidMarker);
			}
			if (!string.IsNullOrEmpty(NotSaidMarker))
			{
				parts.Add("notsaidmarker;" + NotSaidMarker);
			}
			if (!string.IsNullOrEmpty(SayingMarker))
			{
				parts.Add("sayingmarker;" + SayingMarker);
			}
			if (!string.IsNullOrEmpty(Saying))
			{
				parts.Add("saying;" + Saying);
			}
			if (!string.IsNullOrEmpty(Pose))
			{
				parts.Add("pose;" + Pose);
			}
			string data = string.Format("count-{1}:{0}", Count, string.Join("&", parts));
			return data;
		}

		public override string ToString()
		{
			return ToString(false);
		}

		public string ToString(bool excludeTarget)
		{
			string str = GUIHelper.RangeToString(Count);
			if (!HasAdvancedConditions)
			{
				if (!string.IsNullOrEmpty(str))
				{
					str += " players";
				}
				if (!excludeTarget)
				{
					string roleStr = null;
					if (!string.IsNullOrEmpty(Role))
					{
						switch (Role)
						{
							case "target":
								str += " target";
								break;
							case "opp":
								str += " opposing";
								break;
							case "other":
								str += " other";
								break;
							case "winner":
								str += " winner";
								break;
						}
						roleStr = Role;
					}
					if (!string.IsNullOrEmpty(Character) && !excludeTarget)
					{
						str += $": {Character}";
					}
				}
			}
			else
			{
				string roleStr = null;
				if (!string.IsNullOrEmpty(Role))
				{
					switch (Role)
					{
						case "target":
							str += " target";
							break;
						case "opp":
							str += " opposing";
							break;
						case "other":
							str += " other";
							break;
						case "winner":
							str += " winner";
							break;
					}
					roleStr = Role;
				}
				if (!string.IsNullOrEmpty(Character) && !excludeTarget)
				{
					str += $": {Character}";
				}
				if (Status != null)
				{
					string status = Status.Replace("_", " ");
					if (Config.SafeMode)
					{
						if (status == "naked")
						{
							status = "final layer";
						}
						else if (status == "topless")
						{
							status = "no top";
						}
						else if (status == "bottomless")
						{
							status = "no bottom";
						}
						else if (status == "masturbating")
						{
							status = "forfeiting";
						}
					}
					str += " " + status;
				}
				if (!string.IsNullOrEmpty(Gender))
				{
					str += " " + Gender + (FilterTag != null ? "" : "s");
				}
				if (!string.IsNullOrEmpty(Stage))
				{
					str += $" stage: {Stage}";
				}
				if (!string.IsNullOrEmpty(Hand))
				{
					str += $" hand {Hand}";
				}
				if (!string.IsNullOrEmpty(TimeInStage))
				{
					str += $" after {TimeInStage} rounds in stage";
				}
				if (!string.IsNullOrEmpty(Layers))
				{
					str += $" layers left: {Layers}";
				}
				if (!string.IsNullOrEmpty(StartingLayers))
				{
					str += $" starting layers: {StartingLayers}";
				}
				if (FilterTag != null)
				{
					str += " " + FilterTag;
				}
				if (!string.IsNullOrEmpty(ConsecutiveLosses))
				{
					str += $" {ConsecutiveLosses} losses in row";
				}
				if (!string.IsNullOrEmpty(SaidMarker))
				{
					str += $" said {SaidMarker}";
				}
				if (!string.IsNullOrEmpty(NotSaidMarker))
				{
					str += $" not said {NotSaidMarker}";
				}
				if (!string.IsNullOrEmpty(SayingMarker))
				{
					str += $" saying {SayingMarker}";
				}
				if (!string.IsNullOrEmpty(Saying))
				{
					str += $" saying text {Saying}";
				}
				if (!string.IsNullOrEmpty(Pose))
				{
					str += $" using pose {Pose}";
				}
				if (!string.IsNullOrEmpty(Variable))
				{
					str += $" => {Variable}";
				}
				if (str.Trim() == roleStr)
				{
					return "";
				}
			}
			return str.Trim();
		}

		public int GetPriority()
		{
			int priority = 0;
			if (Role == "self")
			{
				if (!string.IsNullOrEmpty(FilterTag))
				{
					priority += 0;
				}
				if (!string.IsNullOrEmpty(Status))
				{
					priority += 20;
				}
				if (!string.IsNullOrEmpty(ConsecutiveLosses))
				{
					priority += 60;
				}
				if (!string.IsNullOrEmpty(TimeInStage))
				{
					priority += 8;
				}
				if (!string.IsNullOrEmpty(Hand))
				{
					priority += 20;
				}
				if (!string.IsNullOrEmpty(Gender))
				{
					priority += 5;
				}
			}
			else if (Role == "target")
			{
				if (!string.IsNullOrEmpty(Character))
				{
					priority += 300;
				}
				if (!string.IsNullOrEmpty(FilterTag))
				{
					priority += 150;
				}
				if (!string.IsNullOrEmpty(Stage))
				{
					priority += 80;
				}
				if (!string.IsNullOrEmpty(Status))
				{
					priority += 70;
				}
				if (!string.IsNullOrEmpty(Layers))
				{
					priority += 40;
				}
				if (!string.IsNullOrEmpty(StartingLayers))
				{
					priority += 40;
				}
				if (!string.IsNullOrEmpty(ConsecutiveLosses))
				{
					priority += 60;
				}
				if (!string.IsNullOrEmpty(TimeInStage))
				{
					priority += 25;
				}
				if (!string.IsNullOrEmpty(Hand))
				{
					priority += 30;
				}
				if (!string.IsNullOrEmpty(Gender))
				{
					priority += 5;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(Character))
				{
					priority += 100;
				}
				if (!string.IsNullOrEmpty(FilterTag))
				{
					priority += 10;
				}
				if (!string.IsNullOrEmpty(Stage))
				{
					priority += 40;
				}
				if (!string.IsNullOrEmpty(Status))
				{
					priority += 5;
				}
				if (!string.IsNullOrEmpty(Layers))
				{
					priority += 20;
				}
				if (!string.IsNullOrEmpty(StartingLayers))
				{
					priority += 20;
				}
				if (!string.IsNullOrEmpty(ConsecutiveLosses))
				{
					priority += 30;
				}
				if (!string.IsNullOrEmpty(TimeInStage))
				{
					priority += 15;
				}
				if (!string.IsNullOrEmpty(Hand))
				{
					priority += 15;
				}
				if (!string.IsNullOrEmpty(Gender))
				{
					priority += 5;
				}
			}
			if (!string.IsNullOrEmpty(SaidMarker))
			{
				priority += 1;
			}
			if (!string.IsNullOrEmpty(NotSaidMarker))
			{
				priority += 1;
			}
			if (!string.IsNullOrEmpty(SayingMarker))
			{
				priority += 1;
			}
			if (!string.IsNullOrEmpty(Saying))
			{
				priority += 1;
			}
			if (!string.IsNullOrEmpty(Pose))
			{
				priority += 1;
			}

			if (Role == "winner")
			{
				priority = (int)Math.Truncate(priority * 1.5f);
			}

			return priority;
		}

		[JsonIgnore]
		public bool IsEmpty
		{
			get
			{
				return !HasAdvancedConditions && string.IsNullOrEmpty(Count) && string.IsNullOrEmpty(Character) && string.IsNullOrEmpty(Variable);
			}
		}

		[JsonIgnore]
		public bool HasAdvancedConditions
		{
			get
			{
				return !string.IsNullOrEmpty(Status) ||
					!string.IsNullOrEmpty(Stage) ||
					!string.IsNullOrEmpty(TimeInStage) ||
					!string.IsNullOrEmpty(ConsecutiveLosses) ||
					!string.IsNullOrEmpty(SaidMarker) ||
					!string.IsNullOrEmpty(SayingMarker) ||
					!string.IsNullOrEmpty(NotSaidMarker) ||
					!string.IsNullOrEmpty(Saying) ||
					!string.IsNullOrEmpty(Hand) ||
					!string.IsNullOrEmpty(Layers) ||
					!string.IsNullOrEmpty(StartingLayers) ||
					!string.IsNullOrEmpty(Gender) ||
					!string.IsNullOrEmpty(FilterTag) ||
					!string.IsNullOrEmpty(Status) ||
					!string.IsNullOrEmpty(Pose);
			}
		}

		public string FormatVariable(string variable)
		{
			variable = variable.Replace("~", "");
			variable = variable.Trim();
			return variable;
		}
	}

	public class FilterRole : BasicRecord
	{
		public string Description;

		public FilterRole(string id, string name, string description)
		{
			Key = id;
			Name = name;
			Description = description;
		}
	}

	[Flags]
	public enum TargetType
	{
		None = 0,
		DirectTarget = 1,
		Filter = 2,
		All = 3,
	}
}

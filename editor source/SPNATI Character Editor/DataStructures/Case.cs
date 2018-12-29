using Desktop;
using Desktop.CommonControls.PropertyControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	/// <remarks>
	/// For simplicity in serialization, fields are listed in the order that make_xml.py generates them, so that the two methods for generating xml files produce mostly equivalent output
	/// </remarks>
	public class Case : IComparable<Case>
	{
		private static long s_globalId;

		[XmlAttribute("tag")]
		public string Tag;

		/// <summary>
		/// Unique case identifier. Unused by the game, but important for the editor
		/// </summary>
		[DefaultValue(0)]
		[XmlAttribute("id")]
		public int Id;

		[RecordSelect(DisplayName = "Target", GroupName = "Target", GroupOrder = 0, Description = "Character performing the action", RecordType = typeof(Character), RecordFilter = "FilterTargetByCase")]
		[XmlAttribute("target")]
		public string Target;

		[RecordSelect(DisplayName = "Target Tag", GroupName = "Target", GroupOrder = 1, Description = "Target has a certain tag", RecordType = typeof(Tag), AllowCreate = true)]
		[XmlAttribute("filter")]
		public string Filter;

		[Boolean(DisplayName = "Hidden", GroupName = "Self", GroupOrder = 99, Description = "This case will evaluate and set markers when conditions are met, but the lines will not actually be displayed on screen", AutoCheck = true)]
		[XmlAttribute("hidden")]
		public string Hidden;

		[StageSelect(DisplayName = "Target Stage", GroupName = "Target", GroupOrder = 2, Description = "Target is currently within a range of stages", BoundProperties = new string[] { "Target" }, FilterStagesToTarget = true)]
		[XmlAttribute("targetStage")]
		public string TargetStage;

		[NumericRange(DisplayName = "Target Layers", GroupName = "Target", GroupOrder = 9, Description = "Number of layers the target has left")]
		[XmlAttribute("targetLayers")]
		public string TargetLayers;

		[NumericRange(DisplayName = "Target Starting Layers", GroupName = "Target", GroupOrder = 10, Description = "Number of layers the target started with")]
		[XmlAttribute("targetStartingLayers")]
		public string TargetStartingLayers;

		[Status(DisplayName = "Target Status", GroupName = "Target", GroupOrder = 8, Description = "Target's current clothing status")]
		[XmlAttribute("targetStatus")]
		public string TargetStatus;

		[RecordSelect(DisplayName = "Also Playing", GroupName = "Also Playing", GroupOrder = 0, Description = "Character that is playing but not performing the current action", RecordType = typeof(Character))]
		[XmlAttribute("alsoPlaying")]
		public string AlsoPlaying;

		[StageSelect(DisplayName = "Also Playing Stage", GroupName = "Also Playing", GroupOrder = 1, Description = "Character in Also Playing is currently within a range of stages", BoundProperties = new string[] { "AlsoPlaying" }, FilterStagesToTarget = false)]
		[XmlAttribute("alsoPlayingStage")]
		public string AlsoPlayingStage;

		[ComboBox(DisplayName = "Also Playing Hand", GroupName = "Also Playing", GroupOrder = 6, Description = "Character in Also Playing has a particular poker hand",
			Options = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush" })]
		[XmlAttribute("alsoPlayingHand")]
		public string AlsoPlayingHand;

		[ComboBox(DisplayName = "Target Hand", GroupName = "Target", GroupOrder = 7, Description = "Target has a particular poker hand",
			Options = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush" })]
		[XmlAttribute("oppHand")]
		public string TargetHand;

		[ComboBox(DisplayName = "Has Hand", GroupName = "Self", GroupOrder = 5, Description = "Character has a particular poker hand",
			Options = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush" })]
		[XmlAttribute("hasHand")]
		public string HasHand;

		[PlayerRange(DisplayName = "Total Males", GroupName = "Table", GroupOrder = 2, Description = "Number of males playing (including this character and the player)")]
		[XmlAttribute("totalMales")]
		public string TotalMales;

		[PlayerRange(DisplayName = "Total Females", GroupName = "Table", GroupOrder = 1, Description = "Number of females playing (including this character and the player)")]
		[XmlAttribute("totalFemales")]
		public string TotalFemales;

		[NumericRange(DisplayName = "Target Time in Stage", GroupName = "Target", GroupOrder = 6, Description = "Number of rounds since the last time the target lost a hand")]
		[XmlAttribute("targetTimeInStage")]
		public string TargetTimeInStage;

		[NumericRange(DisplayName = "Also Playing Time in Stage", GroupName = "Also Playing", GroupOrder = 5, Description = "Number of rounds since the last time the Also Playing player lost a hand")]
		[XmlAttribute("alsoPlayingTimeInStage")]
		public string AlsoPlayingTimeInStage;

		[NumericRange(DisplayName = "Time in Stage", GroupName = "Self", GroupOrder = 4, Description = "Number of rounds since the last time this player lost a hand")]
		[XmlAttribute("timeInStage")]
		public string TimeInStage;

		[NumericRange(DisplayName = "Consecutive Losses", GroupName = "Game", GroupOrder = 0, Description = "Number of hands the target player (or this player) has lost in a row")]
		[XmlAttribute("consecutiveLosses")]
		public string ConsecutiveLosses;

		[PlayerRange(DisplayName = "Total Playing", GroupName = "Table", GroupOrder = 3, Description = "Number of players still in the game")]
		[XmlAttribute("totalAlive")]
		public string TotalPlaying;

		[PlayerRange(DisplayName = "Total Exposed", GroupName = "Table", GroupOrder = 4, Description = "Number of players who have exposed either their chest or crotch")]
		[XmlAttribute("totalExposed")]
		public string TotalExposed;

		[PlayerRange(DisplayName = "Total Naked", GroupName = "Table", GroupOrder = 5, Description = "Number of players who have lost all their clothing, but might still be playing")]
		[XmlAttribute("totalNaked")]
		public string TotalNaked;

		[PlayerRange(DisplayName = "Total Masturbating", GroupName = "Table", GroupOrder = 6, Description = "Number of players who are currently masturbating")]
		[XmlAttribute("totalMasturbating")]
		public string TotalMasturbating;

		[PlayerRange(DisplayName = "Total Finished", GroupName = "Table", GroupOrder = 7, Description = "Number of players who finished masturbating and completely out of the game")]
		[XmlAttribute("totalFinished")]
		public string TotalFinished;

		[NumericRange(DisplayName = "Total Rounds", GroupName = "Game", GroupOrder = 1, Description = "Number of rounds since the game began")]
		[XmlAttribute("totalRounds")]
		public string TotalRounds;

		[MarkerCondition(DisplayName = "Said Marker", GroupName = "Self", GroupOrder = 0, Description = "Character has said a marker", ShowPrivate = true)]
		[XmlAttribute("saidMarker")]
		public string SaidMarker;

		[Marker(DisplayName = "Not Said Marker", GroupName = "Self", GroupOrder = 1, Description = "Character has not said a marker", ShowPrivate = true)]
		[XmlAttribute("notSaidMarker")]
		public string NotSaidMarker;

		[MarkerCondition(DisplayName = "Also Playing Said Marker", GroupName = "Also Playing", GroupOrder = 2, Description = "Another player has said a marker", ShowPrivate = false, BoundProperties = new string[] { "AlsoPlaying" })]
		[XmlAttribute("alsoPlayingSaidMarker")]
		public string AlsoPlayingSaidMarker;

		[Marker(DisplayName = "Also Playing Not Said Marker", GroupName = "Also Playing", GroupOrder = 3, Description = "Another player has not said a marker", ShowPrivate = false, BoundProperties = new string[] { "AlsoPlaying" })]
		[XmlAttribute("alsoPlayingNotSaidMarker")]
		public string AlsoPlayingNotSaidMarker;

		[MarkerCondition(DisplayName = "Also Playing Saying Marker", GroupName = "Also Playing", GroupOrder = 4, Description = "Another player is saying a marker at this very moment", ShowPrivate = false, BoundProperties = new string[] { "AlsoPlaying" })]
		[XmlAttribute("alsoPlayingSayingMarker")]
		public string AlsoPlayingSayingMarker;

		[MarkerCondition(DisplayName = "Target Said Marker", GroupName = "Target", GroupOrder = 3, Description = "Target has said a marker", ShowPrivate = false, BoundProperties = new string[] { "Target" })]
		[XmlAttribute("targetSaidMarker")]
		public string TargetSaidMarker;

		[Marker(DisplayName = "Target Not Said Marker", GroupName = "Target", GroupOrder = 4, Description = "Target has not said a marker", ShowPrivate = false, BoundProperties = new string[] { "Target" })]
		[XmlAttribute("targetNotSaidMarker")]
		public string TargetNotSaidMarker;

		[MarkerCondition(DisplayName = "Target Saying Marker", GroupName = "Target", GroupOrder = 5, Description = "Target is saying a marker at this very moment", ShowPrivate = false, BoundProperties = new string[] { "Target" })]
		[XmlAttribute("targetSayingMarker")]
		public string TargetSayingMarker;

		[XmlAttribute("priority")]
		public string CustomPriority;

		[XmlIgnore]
		public string TargetStatusType
		{
			get
			{
				if (!string.IsNullOrEmpty(TargetStatus) && TargetStatus.StartsWith("not_"))
				{
					return TargetStatus.Substring(4);
				}
				else
				{
					return TargetStatus;
				}
			}
			set
			{
				if (!string.IsNullOrEmpty(TargetStatus) && TargetStatus.StartsWith("not_"))
				{
					TargetStatus = "not_" + value;
				}
				else
				{
					TargetStatus = value;
				}
			}
		}

		[XmlIgnore]
		public bool NegateTargetStatus
		{
			get
			{
				return (!string.IsNullOrEmpty(TargetStatus) && TargetStatus.StartsWith("not_"));
			}
			set
			{
				TargetStatus = TargetStatusType != null ? (value ? "not_" : "") + TargetStatusType : null;
			}
		}

		[Filter(DisplayName = "Filter (+)", GroupName = "Table", GroupOrder = 0, Description = "Filter based on table conditions. Multiple can be added")]
		[XmlElement("condition")]
		public List<TargetCondition> Conditions;

		[Expression(DisplayName = "Variable Test (+)", GroupName = "Game", GroupOrder = 5, Description = "Tests the value of a variable. Multiple can be added")]
		[XmlElement("test")]
		public List<ExpressionTest> Expressions;

		[XmlElement("state")]
		public List<DialogueLine> Lines;

		/// <summary>
		/// Used for consistently sorting two identical cases
		/// </summary>
		private long _globalId;

		/// <summary>
		/// Stages this case appears in
		/// </summary>
		[XmlIgnore]
		public List<int> Stages = new List<int>();

		/// <summary>
		/// Whether this case is considered the "default" for its tag
		/// </summary>
		[XmlIgnore]
		public bool IsDefault;

		public Case()
		{
			_globalId = s_globalId++;
			Lines = new List<DialogueLine>();
			Conditions = new List<TargetCondition>();
			Expressions = new List<ExpressionTest>();
		}

		public Case(string tag) : this()
		{
			Tag = tag;
		}

		public override string ToString()
		{
			string result = TriggerDatabase.GetLabel(Tag);
			if (!string.IsNullOrEmpty(CustomPriority))
			{
				result = "*" + result;
			}
			if (HasFilters)
			{
				if (!string.IsNullOrEmpty(Target))
				{
					result += string.Format(" (target={0})", Target);
				}
				if (!string.IsNullOrEmpty(TargetStage))
				{
					result += string.Format(" (target stage={0})", TargetStage);
				}
				if (!string.IsNullOrEmpty(TargetStatus))
				{
					result += string.Format(" (target {0})", TargetStatus.Replace("_", " "));
				}
				if (!string.IsNullOrEmpty(TargetHand))
				{
					result += string.Format(" (target hand={0})", TargetHand);
				}
				if (!string.IsNullOrEmpty(TargetTimeInStage))
				{
					result += string.Format(" (after {0} rounds in stage)", TargetTimeInStage);
				}
				if (!string.IsNullOrEmpty(TargetLayers))
				{
					result += $" (layers remaining={TargetLayers})";
				}
				if (!string.IsNullOrEmpty(Filter))
				{
					result += string.Format(" (filter={0})", Filter);
				}
				if (!string.IsNullOrEmpty(AlsoPlaying))
				{
					result += string.Format(" (playing w/{0})", AlsoPlaying);
				}
				if (!string.IsNullOrEmpty(AlsoPlayingStage))
				{
					result += string.Format(" (playing w/stage={0})", AlsoPlayingStage);
				}
				if (!string.IsNullOrEmpty(AlsoPlayingTimeInStage))
				{
					result += string.Format(" (after {0} rounds in stage)", AlsoPlayingTimeInStage);
				}
				if (!string.IsNullOrEmpty(HasHand))
				{
					result += string.Format(" (hand={0})", HasHand);
				}
				if (Conditions.Count > 0)
				{
					result += string.Format(" ({0})", string.Join(",", Conditions));
				}
				if (Expressions.Count > 0)
				{
					result += $" ({string.Join(",", Expressions)})";
				}
				if (!string.IsNullOrEmpty(TotalFemales))
				{
					result += string.Format(" ({0} females)", TotalFemales);
				}
				if (!string.IsNullOrEmpty(TotalMales))
				{
					result += string.Format(" ({0} males)", TotalMales);
				}
				if (!string.IsNullOrEmpty(TotalRounds))
				{
					result += string.Format(" ({0} overall rounds)", TotalRounds);
				}
				if (!string.IsNullOrEmpty(TimeInStage))
				{
					result += string.Format(" (after {0} rounds in own stage)", TimeInStage);
				}
				if (!string.IsNullOrEmpty(ConsecutiveLosses))
				{
					result += string.Format(" ({0} losses in a row)", ConsecutiveLosses);
				}
				if (!string.IsNullOrEmpty(TotalPlaying))
				{
					result += string.Format(" ({0} playing)", TotalPlaying);
				}
				if (!string.IsNullOrEmpty(TotalExposed))
				{
					result += string.Format(" ({0} exposed)", TotalExposed);
				}
				if (!string.IsNullOrEmpty(TotalNaked))
				{
					result += string.Format(" ({0} naked)", TotalNaked);
				}
				if (!string.IsNullOrEmpty(TotalMasturbating))
				{
					result += string.Format(" ({0} finishing)", TotalMasturbating);
				}
				if (!string.IsNullOrEmpty(TotalFinished))
				{
					result += string.Format(" ({0} finished)", TotalFinished);
				}
				if (!string.IsNullOrEmpty(SaidMarker))
				{
					result += string.Format(" (said {0})", SaidMarker);
				}
				if (!string.IsNullOrEmpty(NotSaidMarker))
				{
					result += string.Format(" (not said {0})", NotSaidMarker);
				}
				if (!string.IsNullOrEmpty(TargetSaidMarker))
				{
					result += string.Format(" (target said {0})", TargetSaidMarker);
				}
				if (!string.IsNullOrEmpty(TargetNotSaidMarker))
				{
					result += string.Format(" (target not said {0})", TargetNotSaidMarker);
				}
				if (!string.IsNullOrEmpty(TargetSayingMarker))
				{
					result += string.Format(" (target saying {0})", TargetSayingMarker);
				}
				if (!string.IsNullOrEmpty(AlsoPlayingSaidMarker))
				{
					result += string.Format(" (other said {0})", AlsoPlayingSaidMarker);
				}
				if (!string.IsNullOrEmpty(AlsoPlayingNotSaidMarker))
				{
					result += string.Format(" (other not said {0})", AlsoPlayingNotSaidMarker);
				}
				if (!string.IsNullOrEmpty(AlsoPlayingSayingMarker))
				{
					result += string.Format(" (other saying {0})", AlsoPlayingSayingMarker);
				}
			}
			int priority = GetPriority();
			if (priority > 0)
			{
				result += string.Format(" (priority={0})", priority);
			}
			return result;
		}

		/// <summary>
		/// Copies the case except stages and lines
		/// </summary>
		/// <returns></returns>
		public Case CopyConditions()
		{
			Case copy = new Case();
			foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				if (field.FieldType == typeof(string))
				{
					field.SetValue(copy, field.GetValue(this));
				}
			}

			//Since it's just a shallow collection, need to break references to objects
			copy.Conditions = new List<TargetCondition>();
			foreach (TargetCondition condition in Conditions)
			{
				copy.Conditions.Add(condition.Copy());
			}

			copy.Expressions = new List<ExpressionTest>();
			foreach (ExpressionTest test in Expressions)
			{
				copy.Expressions.Add(test.Copy());
			}
			return copy;
		}

		/// <summary>
		/// Copies the case including dialogue and conditions but NOT stages
		/// </summary>
		/// <returns></returns>
		public Case Copy()
		{
			Case copy = CopyConditions();
			for (int i = 0; i < Lines.Count; i++)
			{
				copy.Lines.Add(Lines[i].Copy());
			}
			return copy;
		}

		public void ClearEmptyValues()
		{
			foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				if (field.FieldType == typeof(string) && (string)field.GetValue(this) == "")
				{
					field.SetValue(this, null);
				}
			}

			foreach (var condition in Conditions)
			{
				condition.ClearEmptyValues();
			}
		}

		/// <summary>
		/// Gets the priority for targeted dialogue used by the game
		/// </summary>
		/// <returns></returns>
		public int GetPriority()
		{
			int totalPriority = 0;

			if (!string.IsNullOrEmpty(CustomPriority))
			{
				int priority;
				if (int.TryParse(CustomPriority, out priority))
				{
					return priority;
				}
			}

			if (!string.IsNullOrEmpty(Target))
				totalPriority += 300;

			if (!string.IsNullOrEmpty(Filter))
				totalPriority += 150;

			if (!string.IsNullOrEmpty(TargetStage))
				totalPriority += 80;

			if (!string.IsNullOrEmpty(TargetStatus))
				totalPriority += 70;

			if (!string.IsNullOrEmpty(TargetLayers))
				totalPriority += 40;

			if (!string.IsNullOrEmpty(TargetStartingLayers))
				totalPriority += 40;

			if (!string.IsNullOrEmpty(ConsecutiveLosses))
				totalPriority += 60;

			if (!string.IsNullOrEmpty(TargetHand))
				totalPriority += 30;

			if (!string.IsNullOrEmpty(TargetTimeInStage))
				totalPriority += 25;

			if (!string.IsNullOrEmpty(TargetSaidMarker))
				totalPriority += 1;
			if (!string.IsNullOrEmpty(TargetNotSaidMarker))
				totalPriority += 1;
			if (!string.IsNullOrEmpty(TargetSayingMarker))
				totalPriority += 1;

			if (!string.IsNullOrEmpty(HasHand))
				totalPriority += 20;

			if (!string.IsNullOrEmpty(AlsoPlaying))
			{
				totalPriority += 100;

				if (!string.IsNullOrEmpty(AlsoPlayingStage))
					totalPriority += 40;
				if (!string.IsNullOrEmpty(AlsoPlayingTimeInStage))
					totalPriority += 15;
				if (!string.IsNullOrEmpty(AlsoPlayingHand))
					totalPriority += 5;
				if (!string.IsNullOrEmpty(AlsoPlayingSaidMarker))
					totalPriority += 1;
				if (!string.IsNullOrEmpty(AlsoPlayingNotSaidMarker))
					totalPriority += 1;
				if (!string.IsNullOrEmpty(AlsoPlayingSayingMarker))
					totalPriority += 1;
			}

			if (!string.IsNullOrEmpty(TimeInStage))
				totalPriority += 8;

			if (!string.IsNullOrEmpty(SaidMarker))
				totalPriority += 1;
			if (!string.IsNullOrEmpty(NotSaidMarker))
				totalPriority += 1;

			totalPriority += Conditions.Sum(c => (c.Filter != null ? 10 : 0) + (c.Gender != null ? 5 : 0) + (c.Status != null ? 5 : 0));
			totalPriority += Expressions.Count * 50;

			if (!string.IsNullOrEmpty(TotalMales))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalFemales))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalPlaying))
				totalPriority += 3;
			if (!string.IsNullOrEmpty(TotalExposed))
				totalPriority += 4;
			if (!string.IsNullOrEmpty(TotalNaked))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalMasturbating))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalFinished))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalRounds))
				totalPriority += 10;

			return totalPriority;
		}

		/// <summary>
		/// Gets whether this case has the same stages as another, but not necessarily other conditions
		/// </summary>
		/// <param name="other"></param>
		/// <param name="matchAll">If true, all stages must match. If false, only one needs to</param>
		/// <returns></returns>
		public bool MatchesStages(Case other, bool matchAll)
		{
			if (!matchAll)
			{
				for (int i = 0; i < Stages.Count; i++)
				{
					if (other.Stages.Contains(Stages[i]))
					{
						return true;
					}
				}
				return false;
			}

			if (other.Stages.Count != Stages.Count)
			{
				return false;
			}

			for (int i = 0; i < other.Stages.Count; i++)
			{
				if (other.Stages[i] != Stages[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Gets whether this case matches the conditions+tag of another, but not necessarily the lines or stages
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool MatchesConditions(Case other)
		{
			if (other == this)
				return true;
			if (Tag != other.Tag)
				return false;
			bool sameFilters = Target == other.Target &&
				TargetHand == other.TargetHand &&
				TargetStage == other.TargetStage &&
				TargetTimeInStage == other.TargetTimeInStage &&
				TargetLayers == other.TargetLayers && 
				TargetStatus == other.TargetStatus &&
				TargetStartingLayers == other.TargetStartingLayers &&
				AlsoPlaying == other.AlsoPlaying &&
				AlsoPlayingHand == other.AlsoPlayingHand &&
				AlsoPlayingStage == other.AlsoPlayingStage &&
				AlsoPlayingTimeInStage == other.AlsoPlayingTimeInStage &&
				HasHand == other.HasHand &&
				Filter == other.Filter &&
				TimeInStage == other.TimeInStage &&
				TotalFemales == other.TotalFemales &&
				TotalMales == other.TotalMales &&
				TotalPlaying == other.TotalPlaying &&
				TotalExposed == other.TotalExposed &&
				TotalMasturbating == other.TotalMasturbating &&
				TotalNaked == other.TotalNaked &&
				TotalFinished == other.TotalFinished &&
				TotalRounds == other.TotalRounds &&
				SaidMarker == other.SaidMarker &&
				NotSaidMarker == other.NotSaidMarker &&
				TargetSaidMarker == other.TargetSaidMarker &&
				TargetNotSaidMarker == other.TargetNotSaidMarker &&
				TargetSayingMarker == other.TargetSayingMarker &&
				AlsoPlayingSaidMarker == other.AlsoPlayingSaidMarker &&
				AlsoPlayingNotSaidMarker == other.AlsoPlayingNotSaidMarker &&
				AlsoPlayingSayingMarker == other.AlsoPlayingSayingMarker &&
				ConsecutiveLosses == other.ConsecutiveLosses &&
				Hidden == other.Hidden;
			if (!sameFilters)
				return false;

			if (other.Conditions.Count != Conditions.Count)
				return false;
			for (int i = 0; i < Conditions.Count; i++)
			{
				TargetCondition c1 = Conditions[i];
				TargetCondition c2 = other.Conditions[i];
				if (c1.Filter != c2.Filter || c1.Count != c2.Count || c1.Status != c2.Status || c1.Gender != c2.Gender)
					return false;
			}
			if (other.Expressions.Count != Expressions.Count)
				return false;
			for(int i = 0;i < Expressions.Count; i++)
			{
				if (!Expressions[i].Equals(other.Expressions[i]))
				{
					return false;
				}
			}

			return true;
		}

		public bool HasFilters
		{
			get
			{
				return !string.IsNullOrEmpty(Target) ||
				  !string.IsNullOrEmpty(TargetHand) ||
				  !string.IsNullOrEmpty(TargetStage) ||
				  !string.IsNullOrEmpty(TargetStatus) ||
				  !string.IsNullOrEmpty(TargetLayers) ||
				  !string.IsNullOrEmpty(TargetStartingLayers) ||
				  !string.IsNullOrEmpty(Filter) ||
				  !string.IsNullOrEmpty(AlsoPlayingStage) ||
				  !string.IsNullOrEmpty(AlsoPlaying) ||
				  !string.IsNullOrEmpty(AlsoPlayingHand) ||
				  !string.IsNullOrEmpty(HasHand) ||
				  !string.IsNullOrEmpty(TotalFemales) ||
				  !string.IsNullOrEmpty(TotalMales) ||
				  !string.IsNullOrEmpty(ConsecutiveLosses) ||
				  !string.IsNullOrEmpty(TargetTimeInStage) ||
				  !string.IsNullOrEmpty(AlsoPlayingTimeInStage) ||
				  !string.IsNullOrEmpty(TimeInStage) ||
				  !string.IsNullOrEmpty(TotalPlaying) ||
				  !string.IsNullOrEmpty(TotalExposed) ||
				  !string.IsNullOrEmpty(TotalNaked) ||
				  !string.IsNullOrEmpty(TotalMasturbating) ||
				  !string.IsNullOrEmpty(TotalFinished) ||
				  !string.IsNullOrEmpty(TotalRounds) ||
				  !string.IsNullOrEmpty(TargetSaidMarker) ||
				  !string.IsNullOrEmpty(TargetNotSaidMarker) ||
				  !string.IsNullOrEmpty(TargetSayingMarker) ||
				  !string.IsNullOrEmpty(AlsoPlayingSaidMarker) ||
				  !string.IsNullOrEmpty(AlsoPlayingNotSaidMarker) ||
				  !string.IsNullOrEmpty(SaidMarker) ||
				  !string.IsNullOrEmpty(NotSaidMarker) ||
				  !string.IsNullOrEmpty(AlsoPlayingSayingMarker) ||
				  !string.IsNullOrEmpty(Hidden) ||
				  Conditions.Count > 0 ||
				  Expressions.Count > 0;
			}
		}

		/// <summary>
		/// Gets whether this case has any targeted dialogue towards other players
		/// </summary>
		public bool HasTargetedConditions
		{
			get
			{
				return !string.IsNullOrEmpty(Target) ||
					 !string.IsNullOrEmpty(TargetHand) ||
					 !string.IsNullOrEmpty(TargetStage) ||
					 !string.IsNullOrEmpty(TargetLayers) ||
					 !string.IsNullOrEmpty(TargetStartingLayers) ||
					 !string.IsNullOrEmpty(TargetStatus) ||
					 !string.IsNullOrEmpty(Filter) ||
					 !string.IsNullOrEmpty(AlsoPlayingStage) ||
					 !string.IsNullOrEmpty(AlsoPlaying) ||
					 !string.IsNullOrEmpty(AlsoPlayingHand) ||
					 !string.IsNullOrEmpty(HasHand) ||
					 !string.IsNullOrEmpty(ConsecutiveLosses) ||
					 !string.IsNullOrEmpty(TargetTimeInStage) ||
					 !string.IsNullOrEmpty(AlsoPlayingTimeInStage) ||
					 !string.IsNullOrEmpty(TargetSaidMarker) ||
					 !string.IsNullOrEmpty(TargetNotSaidMarker) ||
					 !string.IsNullOrEmpty(TargetSayingMarker) ||
					 !string.IsNullOrEmpty(AlsoPlayingSaidMarker) ||
					 !string.IsNullOrEmpty(AlsoPlayingNotSaidMarker) ||
					 !string.IsNullOrEmpty(AlsoPlayingSayingMarker);
			}
		}

		/// <summary>
		/// Gets whether this case has any targeted dialogue that is based on game state
		/// </summary>
		public bool HasStageConditions
		{
			get
			{
				return !string.IsNullOrEmpty(TimeInStage) ||
					!string.IsNullOrEmpty(TotalRounds) ||
					!string.IsNullOrEmpty(SaidMarker) ||
					!string.IsNullOrEmpty(NotSaidMarker) ||
					!string.IsNullOrEmpty(TotalPlaying) ||
					!string.IsNullOrEmpty(TotalExposed) ||
					!string.IsNullOrEmpty(TotalNaked) ||
					!string.IsNullOrEmpty(TotalMasturbating) ||
					!string.IsNullOrEmpty(TotalFinished) ||
					!string.IsNullOrEmpty(TotalRounds) ||
					!string.IsNullOrEmpty(TotalFemales) ||
					!string.IsNullOrEmpty(TotalMales) ||
					Conditions.Count > 0 ||
					Expressions.Count > 0;
			}
		}

		/// <summary>
		/// Gets a unique hash for this combination of conditions
		/// </summary>
		/// <returns></returns>
		public int GetCode()
		{
			int hash = Tag.GetHashCode();
			hash = (hash * 397) ^ (Target ?? "").GetHashCode();
			hash = (hash * 397) ^ (TargetHand ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetStatus ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetLayers ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetStartingLayers ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlaying ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingHand ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalFemales ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalMales ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (HasHand ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Filter ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetTimeInStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingTimeInStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TimeInStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (ConsecutiveLosses ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalExposed ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalPlaying ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalNaked ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalMasturbating ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalFinished ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalRounds ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (SaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (NotSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetNotSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetSayingMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingNotSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingSayingMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (CustomPriority ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Hidden ?? string.Empty).GetHashCode();
			foreach (var condition in Conditions)
			{
				hash = (hash * 397) ^ (condition.Filter ?? string.Empty).GetHashCode();
				hash = (hash * 397) ^ (condition.Gender ?? string.Empty).GetHashCode();
				hash = (hash * 397) ^ (condition.Status ?? string.Empty).GetHashCode();
				hash = (hash * 397) ^ condition.Count.GetHashCode();
			}
			foreach (ExpressionTest expr in Expressions)
			{
				hash = (hash * 397) ^ expr.GetHashCode();
			}
			return hash;
		}

		public int CompareTo(Case other)
		{
			int comparison = Tag.CompareTo(other.Tag);
			if (comparison == 0)
				comparison = other.GetPriority().CompareTo(GetPriority());
			if (comparison == 0)
				return _globalId.CompareTo(other._globalId);
			else return comparison;
		}

		/// <summary>
		/// Gets the also playing stage range
		/// </summary>
		/// <param name="min">Min stage</param>
		/// <param name="max">Max stage</param>
		public void SplitAlsoPlayingStage(out string min, out string max)
		{
			min = AlsoPlayingStage;
			max = null;
			if (string.IsNullOrEmpty(AlsoPlayingStage))
			{
				return;
			}
			string[] pieces = AlsoPlayingStage.Split('-');
			min = pieces[0];
			if (pieces.Length > 1)
				max = pieces[1];
		}

		/// <summary>
		/// Converts a min and max range into a single AlsoPlayingStage value
		/// </summary>
		public void SetAlsoPlayingStage(string min, string max)
		{
			if (string.IsNullOrEmpty(min))
			{
				AlsoPlayingStage = null;
			}
			AlsoPlayingStage = min;
			if (!string.IsNullOrEmpty(max) && min != max)
			{
				AlsoPlayingStage += "-" + max;
			}
		}

		/// <summary>
		/// Gets the target stage range
		/// </summary>
		/// <param name="min">Min stage</param>
		/// <param name="max">Max stage</param>
		public void SplitTargetStage(out string min, out string max)
		{
			min = TargetStage;
			max = null;
			if (string.IsNullOrEmpty(TargetStage))
			{
				return;
			}
			string[] pieces = TargetStage.Split('-');
			min = pieces[0];
			if (pieces.Length > 1)
				max = pieces[1];
		}

		/// <summary>
		/// Converts a min and max range into a single TargetStage value
		/// </summary>
		public void SetTargetStage(string min, string max)
		{
			if (string.IsNullOrEmpty(min))
			{
				TargetStage = null;
			}
			TargetStage = min;
			if (!string.IsNullOrEmpty(max) && min != max)
			{
				TargetStage += "-" + max;
			}
		}

		/// <summary>
		/// Converts a string to a range
		/// </summary>
		/// <param name="range">Range in the format D or D-D</param>
		/// <param name="min">Minimum value of the range</param>
		/// <param name="max">Maximum value of the range</param>
		public static void ToRange(string range, out int min, out int max)
		{
			if (string.IsNullOrEmpty(range))
			{
				min = 0;
				max = 0;
				return;
			}
			string[] pieces = range.Split('-');
			int.TryParse(pieces[0], out min);
			if (pieces.Length > 1)
				int.TryParse(pieces[1], out max);
			else max = min;
		}

		/// <summary>
		/// Creates a response to a situation.
		/// </summary>
		/// <param name="speaker"></param>
		/// <param name="responder"></param>
		/// <returns></returns>
		public Case CreateResponse(Character speaker, Character responder)
		{
			//no way to respond to hidden cases, since they never display
			if (Hidden == "1") { return null; }

			Case response = new Case();

			response.Tag = GetResponseTag(speaker, responder);

			bool caseIsTargetable = TriggerDatabase.GetTrigger(Tag).HasTarget;
			bool responseIsTargetable = TriggerDatabase.GetTrigger(response.Tag).HasTarget;
			bool hasTarget = HasTarget();
			bool targetingResponder = (Target == responder.FolderName);
			bool hasAlsoPlaying = HasAlsoPlaying();
			bool alsoPlayingIsResponder = (AlsoPlaying == responder.FolderName);

			if (response.Tag == "-") //this is deprecated anyway
			{
				return null;
			}
			if ((caseIsTargetable && hasAlsoPlaying && !alsoPlayingIsResponder) || (!responseIsTargetable && !hasTarget && hasAlsoPlaying && !alsoPlayingIsResponder))
			{
				//for cases where AlsoPlaying is already in use, shift that character into a filter target condition
				TargetCondition condition = new TargetCondition()
				{
					Count = "1",
					Filter = AlsoPlaying
				};
				response.Conditions.Add(condition);
				hasAlsoPlaying = false; //free this up for the responder to go into
			}

			if (!caseIsTargetable && responseIsTargetable && !hasTarget && !hasAlsoPlaying)
			{
				CopySelfIntoTarget(response, speaker);
			}
			else if (!caseIsTargetable && responseIsTargetable && !hasTarget && alsoPlayingIsResponder)
			{
				CopySelfIntoTarget(response, speaker);
				CopyAlsoPlayingIntoSelf(response, responder);
			}
			else if (!caseIsTargetable && responseIsTargetable && !hasTarget && hasAlsoPlaying && !alsoPlayingIsResponder)
			{
				CopySelfIntoTarget(response, speaker);
				CopyAlsoPlaying(response);
			}
			else if ((caseIsTargetable || !responseIsTargetable) && !hasTarget && !alsoPlayingIsResponder)
			{
				CopySelfIntoAlsoPlaying(response, speaker);
			}
			else if ((caseIsTargetable || !responseIsTargetable) && !hasTarget && alsoPlayingIsResponder)
			{
				CopySelfIntoAlsoPlaying(response, speaker);
				CopyAlsoPlayingIntoSelf(response, responder);
			}
			else if (caseIsTargetable && hasTarget && !targetingResponder && !hasAlsoPlaying)
			{
				CopyTarget(response);
				CopySelfIntoAlsoPlaying(response, speaker);
			}
			else if (caseIsTargetable && hasTarget && !hasAlsoPlaying && targetingResponder)
			{
				CopyTargetIntoSelf(response, responder);
				CopySelfIntoAlsoPlaying(response, speaker);
			}
			else if (caseIsTargetable && hasTarget && !targetingResponder && alsoPlayingIsResponder)
			{
				CopyAlsoPlayingIntoSelf(response, responder);
				CopyTarget(response);
				CopySelfIntoAlsoPlaying(response, speaker);
			}
			else
			{
				return null; //if I computed the truth table correctly, this should never happen
			}

			//if not stages have been set, apply it to all
			Trigger trigger = TriggerDatabase.GetTrigger(response.Tag);
			if (response.Stages.Count == 0)
			{
				for (int i = 0; i < responder.Layers + Clothing.ExtraStages; i++)
				{
					int standardStage = TriggerDatabase.ToStandardStage(responder, i);
					if (standardStage >= trigger.StartStage && standardStage <= trigger.EndStage)
					{
						response.Stages.Add(i);
					}
				}
			}

			//misc conditions are always the same
			response.Conditions.AddRange(Conditions);
			response.Expressions.AddRange(Expressions);
			response.ConsecutiveLosses = ConsecutiveLosses;
			response.TotalFemales = TotalFemales;
			response.TotalMales = TotalMales;
			response.TotalPlaying = TotalPlaying;
			response.TotalExposed = TotalExposed;
			response.TotalNaked = TotalNaked;
			response.TotalMasturbating = TotalMasturbating;
			response.TotalFinished = TotalFinished;
			response.TotalRounds = TotalRounds;

			return response;
		}

		/// <summary>
		/// Gets whether any Target-related fields are set
		/// </summary>
		private bool HasTarget()
		{
			return !string.IsNullOrEmpty(Target) ||
				!string.IsNullOrEmpty(TargetHand) ||
				!string.IsNullOrEmpty(TargetLayers) ||
				!string.IsNullOrEmpty(TargetSaidMarker) ||
				!string.IsNullOrEmpty(TargetNotSaidMarker) ||
				!string.IsNullOrEmpty(TargetSayingMarker) ||
				!string.IsNullOrEmpty(TargetStage) ||
				!string.IsNullOrEmpty(TargetStartingLayers) ||
				!string.IsNullOrEmpty(TargetStatus) ||
				!string.IsNullOrEmpty(TargetTimeInStage);
		}

		/// <summary>
		/// Gets whether any AlsoPlaying-related fields are set
		/// </summary>
		/// <returns></returns>
		private bool HasAlsoPlaying()
		{
			return !string.IsNullOrEmpty(AlsoPlaying); //also playing only works if a specific target is given, so no need to check the other values
		}

		/// <summary>
		/// Copies target properties into another case's target
		/// </summary>
		/// <param name="other"></param>
		private void CopyTarget(Case other)
		{
			other.Target = Target;
			other.TargetHand = TargetHand;
			other.TargetLayers = TargetLayers;
			other.TargetSaidMarker = TargetSaidMarker;
			other.TargetNotSaidMarker = TargetNotSaidMarker;
			other.TargetSayingMarker = TargetSayingMarker;
			other.TargetStage = TargetStage;
			other.TargetStartingLayers = TargetStartingLayers;
			other.TargetStatus = TargetStatus;
			other.TargetStatusType = TargetStatusType;
			other.TargetTimeInStage = TargetTimeInStage;
		}

		/// <summary>
		/// Copies Target properties into another case's self properties
		/// </summary>
		/// <param name="other"></param>
		private void CopyTargetIntoSelf(Case other, Character responder)
		{
			if (!string.IsNullOrEmpty(TargetStage))
			{
				int min, max;
				ToRange(TargetStage, out min, out max);
				for (int i = min; i <= max; i++)
				{
					other.Stages.Add(i);
				}
			}
			else
			{
				bool removing = Tag.Contains("will_be_visible") || Tag.Contains("removing_");
				bool removed = Tag.Contains("is_visible") || Tag.Contains("removed_");
				string type = Tag.Contains("accessory") ? "extra" : Tag.Contains("minor") ? "minor" : Tag.Contains("major") ? "major" : "important";
				string position = Tag.Contains("chest") ? "upper" : "lower";

				//all stages
				Trigger trigger = TriggerDatabase.GetTrigger(other.Tag);
				for (int i = 0; i < responder.Layers + Clothing.ExtraStages; i++)
				{
					int standardStage = TriggerDatabase.ToStandardStage(responder, i);
					if (standardStage >= trigger.StartStage && standardStage <= trigger.EndStage)
					{
						//filter out stages that aren't possible
						if (removing || removed)
						{
							if (i > responder.Layers)
							{
								//no finishing or finished
								continue;
							}
							else
							{
								//make sure item matches targeted type
								Clothing c = responder.Wardrobe[responder.Layers - i - (removed ? 0 : 1)];
								if (c.Type != type)
								{
									continue;
								}
								if (type == "important" && position != c.Position)
								{
									continue;
								}
							}
						}

						other.Stages.Add(i);
					}
				}
			}
			other.HasHand = TargetHand;
			other.SaidMarker = TargetSaidMarker;
			other.NotSaidMarker = TargetNotSaidMarker;
			other.TimeInStage = TargetTimeInStage;
		}

		/// <summary>
		/// Copies AlsoPlaying properties into another case's AlsoPlaying
		/// </summary>
		/// <param name="other"></param>
		private void CopyAlsoPlaying(Case other)
		{
			other.AlsoPlaying = AlsoPlaying;
			other.AlsoPlayingHand = AlsoPlayingHand;
			other.AlsoPlayingNotSaidMarker = AlsoPlayingNotSaidMarker;
			other.AlsoPlayingSaidMarker = AlsoPlayingSaidMarker;
			other.AlsoPlayingStage = AlsoPlayingStage;
			other.AlsoPlayingTimeInStage = AlsoPlayingTimeInStage;
			other.AlsoPlayingSayingMarker = AlsoPlayingSayingMarker;
		}

		/// <summary>
		/// Copies this tag's self-targeting properties into another's AlsoPlaying
		/// </summary>
		/// <param name="other"></param>
		private void CopySelfIntoAlsoPlaying(Case other, Character speaker)
		{
			string speakerStageRange = null;
			int min = 0;
			int max = 0;
			if (Stages.Count > 0)
			{
				min = Stages.Min(stage => stage);
				max = Stages.Max(stage => stage);
			}
			Trigger trigger = TriggerDatabase.GetTrigger(Tag);
			if (TriggerDatabase.ToStandardStage(speaker, min) == trigger.StartStage && TriggerDatabase.ToStandardStage(speaker, max) == trigger.EndStage)
			{
				speakerStageRange = null;
			}
			else if (max != min)
			{
				speakerStageRange = min + "-" + max;
			}
			else
			{
				speakerStageRange = min.ToString();
			}

			other.AlsoPlaying = speaker.FolderName;
			other.AlsoPlayingStage = speakerStageRange;
			other.AlsoPlayingTimeInStage = TimeInStage;
			other.AlsoPlayingHand = HasHand;
			other.AlsoPlayingNotSaidMarker = NotSaidMarker;
			other.AlsoPlayingSaidMarker = SaidMarker;

			//If all lines set the same marker, use that marker in targetSayingMarker
			string marker = null;
			foreach (DialogueLine line in Lines)
			{
				if (line.Marker != null)
				{
					if (marker == null)
					{
						marker = line.Marker;
					}
					else if (marker != line.Marker)
					{
						marker = null;
						break;
					}
				}
				else if (marker != null)
				{
					marker = null;
					break;
				}
			}
			if (!string.IsNullOrEmpty(marker))
			{
				if (marker.StartsWith("+") || marker.StartsWith("-"))
				{
					marker = marker.Substring(1);
				}
				other.AlsoPlayingSayingMarker = marker;
			}
		}

		/// <summary>
		/// Copies this tag's AlsoPlaying properties into another's self-properties
		/// </summary>
		/// <param name="other"></param>
		private void CopyAlsoPlayingIntoSelf(Case other, Character responder)
		{
			if (!string.IsNullOrEmpty(AlsoPlayingStage))
			{
				int min, max;
				ToRange(AlsoPlayingStage, out min, out max);
				for (int i = min; i <= max; i++)
				{
					other.Stages.Add(i);
				}
			}
			else
			{
				//all stages
				Trigger trigger = TriggerDatabase.GetTrigger(other.Tag);
				for (int i = 0; i < responder.Layers + Clothing.ExtraStages; i++)
				{
					int stage = TriggerDatabase.ToStandardStage(responder, i);
					if (stage >= trigger.StartStage && stage <= trigger.EndStage)
					{
						other.Stages.Add(i);
					}
				}
			}
			other.TimeInStage = AlsoPlayingTimeInStage;
			other.HasHand = AlsoPlayingHand;
			other.NotSaidMarker = AlsoPlayingNotSaidMarker;
			other.SaidMarker = AlsoPlayingSaidMarker;
		}

		/// <summary>
		/// Copies this tag's self-targeting properties into another's Target
		/// </summary>
		/// <param name="other"></param>
		private void CopySelfIntoTarget(Case other, Character speaker)
		{
			string speakerStageRange = null;
			int min = 0;
			int max = 0;
			if (Stages.Count > 0)
			{
				min = Stages.Min(stage => stage);
				max = Stages.Max(stage => stage);
			}
			Trigger trigger = TriggerDatabase.GetTrigger(Tag);
			if (TriggerDatabase.ToStandardStage(speaker, min) == trigger.StartStage && TriggerDatabase.ToStandardStage(speaker, max) == trigger.EndStage)
			{
				speakerStageRange = null;
			}
			else if (max != min)
			{
				speakerStageRange = min + "-" + max;
			}
			else
			{
				speakerStageRange = min.ToString();
			}

			other.Target = speaker.FolderName;
			other.TargetStage = speakerStageRange;
			other.TargetTimeInStage = TimeInStage;
			other.TargetHand = HasHand;
			other.TargetNotSaidMarker = NotSaidMarker;
			other.TargetSaidMarker = SaidMarker;

			//If all lines set the same marker, use that marker in targetSayingMarker
			string marker = null;
			foreach (DialogueLine line in Lines)
			{
				if (line.Marker != null)
				{
					if (marker == null)
					{
						marker = line.Marker;
					}
					else if (marker != line.Marker)
					{
						marker = null;
						break;
					}
				}
				else if (marker != null)
				{
					marker = null;
					break;
				}
			}
			if (!string.IsNullOrEmpty(marker))
			{
				other.TargetSayingMarker = marker;
			}
		}

		/// <summary>
		/// Translates a layer type and position to a word used in a trigger
		/// </summary>
		/// <remarks>This really doesn't belong in the Case class</remarks>
		/// <param name="speaker"></param>
		/// <param name="stage"></param>
		/// <returns></returns>
		private string GetLayerType(Character speaker, int stage)
		{
			Clothing layer = speaker.Wardrobe[speaker.Layers - stage - 1];
			if (layer.Type == "extra")
			{
				return "accessory";
			}
			else if (layer.Type == "important")
			{
				if (layer.Position == "lower")
				{
					return "crotch";
				}
				else if (layer.Position == "upper")
				{
					return "chest";
				}
			}
			return layer.Type;
		}

		/// <summary>
		/// Gets the tag needed for a responder to target this case.
		/// </summary>
		/// <param name="speaker"></param>
		/// <param name="responder"></param>
		/// <returns></returns>
		public string GetResponseTag(Character speaker, Character responder)
		{
			string gender = speaker.Gender;

			//First handle tags where the speaker is actively doing something, since these are the easiest to handle
			if (Tag == "stripping")
			{
				int stage = Stages.Min(s => s);
				string layer = GetLayerType(speaker, stage);
				if (layer == "major" || layer == "minor" || layer == "accessory")
				{
					return $"{gender}_removing_{layer}";
				}
				else
				{
					return $"{gender}_{layer}_will_be_visible";
				}
			}
			else if (Tag == "stripped")
			{
				int stage = Stages.Min(s => s);
				string layer = GetLayerType(speaker, stage - 1);
				if (layer == "major" || layer == "minor" || layer == "accessory")
				{
					return $"{gender}_removed_{layer}";
				}
				else
				{
					if (gender == "female" && layer == "chest" || gender == "male" && layer == "crotch")
					{
						return $"{gender}_{speaker.Size}_{layer}_is_visible";
					}
					else
					{
						return $"{gender}_{layer}_is_visible";
					}
				}
			}
			else if (Tag == "must_masturbate_first")
			{
				return $"{gender}_must_masturbate";
			}
			else if (Tag == "must_masturbate" || Tag == "start_masturbating" || Tag == "masturbating" || Tag == "finished_masturbating")
			{
				return $"{gender}_{Tag}";
			}
			else if (Tag.StartsWith("must_strip"))
			{
				return $"{gender}_must_strip";
			}
			else if (Tag == "game_over_victory")
			{
				return "game_over_defeat";
			}

			string tag = Tag;
			if (tag.StartsWith("female_"))
			{
				tag = tag.Substring(7);
			}
			else if (tag.StartsWith("male_"))
			{
				tag = tag.Substring(5);
			}

			if (Target == responder.FolderName)
			{
				//addressing responder directly, meaning the responder must be doing some action
				if (tag.Contains("will_be_visible") || tag.Contains("removing"))
				{
					return "stripping";
				}
				else if (tag.Contains("is_visible") || tag.Contains("removed"))
				{
					return "stripped";
				}
				else if (tag == "must_strip")
				{
					return "must_strip";
				}
				else if (tag.Contains("masturbat"))
				{
					return tag;
				}
			}

			if (tag == "good_hand" || tag == "okay_hand" || tag == "bad_hand")
			{
				return "hand";
			}

			return Tag; //if nothing above applied, the speaker is reacting to some event unrelated to the responder, so the responder can target the same thing
		}

		public static List<int> GetTargetStage(string reactionTag, Character target)
		{
			List<int> stages = new List<int>();
			//limit the stages for certain tags
			if (reactionTag.EndsWith("crotch_is_visible"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "important" && l.Position == "lower");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer);
				}
			}
			else if (reactionTag.EndsWith("crotch_will_be_visible"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "important" && l.Position == "lower");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer - 1);
				}
			}
			else if (reactionTag.EndsWith("chest_is_visible"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "important" && l.Position == "upper");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer);
				}
			}
			else if (reactionTag.EndsWith("chest_will_be_visible"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "important" && l.Position == "upper");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer - 1);
				}
			}
			else if (reactionTag.EndsWith("removing_accessory"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "extra");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer - 1);
				}
			}
			else if (reactionTag.EndsWith("removed_accessory"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "extra");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer);
				}
			}
			else if (reactionTag.EndsWith("removing_major"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "major");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer - 1);
				}
			}
			else if (reactionTag.EndsWith("removed_major"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "major");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer);
				}
			}
			else if (reactionTag.EndsWith("removing_minor"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "minor");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer - 1);
				}
			}
			else if (reactionTag.EndsWith("removed_minor"))
			{
				int layer = target.Wardrobe.FindIndex(l => l.Type == "minor");
				if (layer >= 0)
				{
					stages.Add(target.Layers - layer);
				}
			}
			else if (reactionTag.EndsWith("must_masturbate"))
			{
				stages.Add(target.Layers);
			}
			else if (reactionTag.EndsWith("start_masturbating"))
			{
				stages.Add(target.Layers);
			}
			else if (reactionTag.Contains("finished"))
			{
				stages.Add(target.Layers + 2);
			}
			else if (reactionTag.Contains("finishing"))
			{
				stages.Add(target.Layers + 1);
			}
			else if (reactionTag.EndsWith("masturbating"))
			{
				stages.Add(target.Layers + 1);
			}

			return stages;
		}

		/// <summary>
		/// Gets a list of cases that could potentially match up with the given source tag. This could be lines that the sourceCase is responding to, or lines that are responding to the sourceCase
		/// </summary>
		/// <remarks>This assumes the sourceCase is targeting the other character, either directly or through a tag</remarks>
		/// <param name="sourceCase">Case for which to get matches</param>
		/// <param name="speaker">The character speaking this case</param>
		/// <param name="other">Speaker of the matching cases</param>
		/// <returns></returns>
		public static List<Case> GetMatchingCases(Case sourceCase, Character speaker, Character other)
		{
			List<Case> cases = new List<Case>();

			//first create a response that targets it exactly
			Case response = sourceCase.CreateResponse(speaker, other);
			if (response == null)
			{
				//if that failed, the case is too specific to respond to exactly, so create a matching case with the bare minimum of requirements
				response = new Case(sourceCase.GetResponseTag(speaker, other));
			}

			//now get a list of cases that fit the bare minimum requirements of that response (i.e. the tag and stages)
			List<Case> possibleCases = other.Behavior.GetWorkingCases().Where(c => TriggerDatabase.AreRelated(c.Tag, response.Tag) && c.Stages.Min() >= response.Stages.Min() && c.Stages.Max() <= response.Stages.Max()).ToList();

			//now start eliminating cases that aren't possible (or at least very unlikely) based on the source's conditions
			foreach (Case c in possibleCases)
			{
				if (!string.IsNullOrEmpty(c.AlsoPlaying) && c.AlsoPlaying != sourceCase.AlsoPlaying)
				{
					continue; //conflict; they're checking for different also playings. Okay, it's not technically a conflict, but it's highly unlikely they want to respond to this case
				}
				if (!string.IsNullOrEmpty(sourceCase.TargetSaidMarker) && c.NotSaidMarker == sourceCase.TargetSaidMarker)
				{
					continue; //if they're looking for a marker and the other is looking for not having the marker, then it's a clear conflict of interests
				}
				if (!string.IsNullOrEmpty(c.SaidMarker) && !string.IsNullOrEmpty(response.TargetSaidMarker) && response.TargetSaidMarker != c.SaidMarker)
				{
					continue; //Other is checking for a marker that the source isn't, which makes it unlikely since otherwise they should be targeting the marker too
				}
				if (c.HasHand != sourceCase.TargetHand)
				{
					continue;
				}
				if (c.TimeInStage != sourceCase.TargetTimeInStage)
				{
					continue;
				}
				if (!string.IsNullOrEmpty(c.Filter) && !speaker.Tags.Contains(c.Filter))
				{
					continue;
				}
				if (!string.IsNullOrEmpty(c.Target) && c.Target != speaker.FolderName && c.Target != sourceCase.Target)
				{
					continue;
				}
				if (!c.MatchesTableConditions(sourceCase))
				{
					continue;
				}

				cases.Add(c);
			}

			cases.Sort((c1, c2) => { return c2.GetPriority().CompareTo(c1.GetPriority()); });
			return cases;
		}

		/// <summary>
		/// Gets whether the table conditions match between two cases
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool MatchesTableConditions(Case other)
		{
			bool match = other.TotalExposed == TotalExposed &&
				other.TotalFemales == TotalFemales &&
				other.TotalFinished == TotalFinished &&
				other.TotalMales == TotalMales &&
				other.TotalMasturbating == TotalMasturbating &&
				other.TotalNaked == TotalNaked &&
				other.TotalPlaying == TotalPlaying &&
				other.TotalRounds == TotalRounds &&
				other.ConsecutiveLosses == ConsecutiveLosses;
			return match;
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		private bool FilterTargetByCase(IRecord record)
		{
			Character character = record as Character;
			Trigger trigger = TriggerDatabase.GetTrigger(Tag);

			if (character.Key == "human")
			{
				return true;
			}

			if (trigger.Size != character.Size)
			{
				return false;
			}

			if (trigger.Gender == null || trigger.Gender == character.Gender)
			{
				return true;
			}
			return false;
		}
	}

	public class TargetCondition
	{
		[XmlAttribute("count")]
		/// <summary>
		/// Number of characters needing the filter tag
		/// </summary>
		public string Count;

		[XmlAttribute("filter")]
		/// <summary>
		/// Tag to condition on
		/// </summary>
		public string Filter;

		[XmlAttribute("gender")]
		public string Gender;

		[XmlAttribute("status")]
		public string Status;

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

		[XmlIgnore]
		public string StatusType
		{
			get
			{
				if (!string.IsNullOrEmpty(Status) && Status.StartsWith("not_"))
				{
					return Status.Substring(4);
				}
				else
				{
					return Status;
				}
			}
			set
			{
				if (!string.IsNullOrEmpty(Status) && Status.StartsWith("not_"))
				{
					Status = "not_" + value;
				}
				else
				{
					Status = value;
				}
			}
		}

		[XmlIgnore]
		public bool NegateStatus
		{
			get
			{
				return (!string.IsNullOrEmpty(Status) && Status.StartsWith("not_"));
			}
			set
			{
				if (string.IsNullOrEmpty(StatusType))
				{
					Status = null;
				}
				else
				{
					Status = StatusType != null ? (value ? "not_" : "") + StatusType : null;
				}
			}
		}

		public TargetCondition()
		{
		}

		public TargetCondition(string tag, string gender, string status, string count)
		{
			Filter = tag;
			Gender = gender;
			Status = status;
			Count = count;
		}

		public TargetCondition(string tag, string gender, string status, bool negateStatus, string count)
		{
			Filter = tag;
			Gender = gender;
			StatusType = status;
			NegateStatus = negateStatus;
			Count = count;
		}

		public void ClearEmptyValues()
		{
			if (Filter == "")
				Filter = null;
			if (Gender == "")
				Gender = null;
			if (Status == "")
				Status = null;
		}

		public TargetCondition Copy()
		{
			TargetCondition copy = new TargetCondition(Filter, Gender, Status, NegateStatus, Count);
			return copy;
		}

		public override string ToString()
		{
			string str = Count;
			if (Filter == null && Status == null && Gender == null)
			{
				str += " players";
			}
			else
			{
				if (Status != null)
				{
					str += " " + Status.Replace("_", " ");
				}
				if (!string.IsNullOrEmpty(Gender))
				{
					str += " " + Gender + (Filter != null ? "" : "s");
				}
				if (Filter != null)
				{
					str += " " + Filter;
				}

			}
			return str;
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

	public class ExpressionTest
	{
		[XmlAttribute("expr")]
		public string Expression;

		[XmlAttribute("value")]
		public string Value;

		public ExpressionTest() { }

		public ExpressionTest(string expr, string value)
		{
			Expression = expr;
			Value = value;
		}

		public ExpressionTest Copy()
		{
			return new ExpressionTest(Expression, Value);
		}

		public override bool Equals(object obj)
		{
			ExpressionTest other = obj as ExpressionTest;
			if (other == null) { return false; }
			return Expression.Equals(other) && Value.Equals(other);
		}

		public override int GetHashCode()
		{
			int hash = Expression.GetHashCode();
			hash = (hash * 397) ^ (Value ?? "").GetHashCode();
			return hash;
		}

		public override string ToString()
		{
			return $"{Expression}={Value}";
		}
	}

	public static class Extensions
	{
		public static int ToInt(this string value)
		{
			if (value == null)
				return 0;
			string[] pieces = value.Split('-');
			string val = pieces[0];
			int result;
			int.TryParse(val, out result);
			return result;
		}
	}
}

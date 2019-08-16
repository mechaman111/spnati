using Desktop;
using Desktop.CommonControls;
using Desktop.CommonControls.PropertyControls;
using Desktop.DataStructures;
using Desktop.Reporting;
using Newtonsoft.Json;
using SPNATI_Character_Editor.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Case : BindableObject, IComparable<Case>, ISliceable
	{
		private static long s_globalId;

		private string _tag;
		[XmlOrder(0)]
		[XmlAttribute("tag")]
		[JsonProperty("tag")]
		public string Tag
		{
			get { return _tag; }
			set { if (_tag != value) { _tag = value; NotifyPropertyChanged(nameof(Tag)); } }
		}

		[DefaultValue(0)]
		[XmlOrder(1)]
		[XmlAttribute("set")]
		public int TriggerSet;

		/// <summary>
		/// Unique case identifier in the format Stage-Id. Unused by the game, but important for the editor
		/// </summary>
		[DefaultValue(null)]
		[XmlOrder(2)]
		[XmlAttribute("id")]
		public string StageId;

		/// <summary>
		/// ID for a working case. Like the 2nd piece of StageId
		/// </summary>
		[XmlIgnore]
		public int Id;

		/// <summary>
		/// Only used with ImportEdits
		/// </summary>
		[JsonProperty("stage")]
		[XmlIgnore]
		public string Stage;

		private int _oneShotId;
		/// <summary>
		/// Case will only play once
		/// </summary>
		[OneShot(OneShotMode.Case, DisplayName = "Play Once", GroupName = "Self", GroupOrder = 98, Description = "This call will only play once per game.")]
		[DefaultValue(0)]
		[XmlOrder(10)]
		[JsonProperty("oneShotId")]
		[XmlAttribute("oneShotId")]
		public int OneShotId
		{
			get { return _oneShotId; }
			set { if (_oneShotId != value) { _oneShotId = value; NotifyPropertyChanged(nameof(OneShotId)); } }
		}

		private string _target;
		[RecordSelect(DisplayName = "Target", GroupName = "Target", GroupOrder = 0, Description = "Character performing the action", RecordType = typeof(Character), RecordFilter = "FilterTargetByCase", AllowCreate = true)]
		[XmlOrder(20)]
		[XmlAttribute("target")]
		[JsonProperty("target")]
		public string Target
		{
			get { return _target; }
			set { if (_target != value) { _target = value; NotifyPropertyChanged(nameof(Target)); } }
		}

		private string _filter;
		[RecordSelect(DisplayName = "Target Tag", GroupName = "Target", GroupOrder = 1, Description = "Target has a certain tag", RecordType = typeof(Tag), AllowCreate = true)]
		[XmlOrder(30)]
		[XmlAttribute("filter")]
		[JsonProperty("filter")]
		public string Filter
		{
			get { return _filter; }
			set { if (_filter != value) { _filter = value; NotifyPropertyChanged(nameof(Filter)); } }
		}

		private string _hidden;
		[Boolean(DisplayName = "Hidden", GroupName = "Self", GroupOrder = 99, Description = "This case will evaluate and set markers when conditions are met, but the lines will not actually be displayed on screen", AutoCheck = true)]
		[XmlOrder(40)]
		[XmlAttribute("hidden")]
		[JsonProperty("hidden")]
		public string Hidden
		{
			get { return _hidden; }
			set { if (_hidden != value) { _hidden = value; NotifyPropertyChanged(nameof(Hidden)); } }
		}

		private string _targetStage;
		[StageSelect(DisplayName = "Target Stage", GroupName = "Target", GroupOrder = 2, Description = "Target is currently within a range of stages", BoundProperties = new string[] { "Target" }, FilterStagesToTarget = true, SkinVariable = "~target.costume~")]
		[XmlOrder(50)]
		[XmlAttribute("targetStage")]
		[JsonProperty("targetStage")]
		public string TargetStage
		{
			get { return _targetStage; }
			set { if (_targetStage != value) { _targetStage = value; NotifyPropertyChanged(nameof(TargetStage)); } }
		}

		private string _targetLayers;
		[NumericRange(DisplayName = "Target Layers", GroupName = "Target", GroupOrder = 9, Description = "Number of layers the target has left")]
		[XmlOrder(60)]
		[XmlAttribute("targetLayers")]
		[JsonProperty("targetLayers")]
		public string TargetLayers
		{
			get { return _targetLayers; }
			set { if (_targetLayers != value) { _targetLayers = value; NotifyPropertyChanged(nameof(TargetLayers)); } }
		}

		private string _targetStartingLayers;
		[NumericRange(DisplayName = "Target Starting Layers", GroupName = "Target", GroupOrder = 10, Description = "Number of layers the target started with")]
		[XmlOrder(70)]
		[XmlAttribute("targetStartingLayers")]
		[JsonProperty("targetStartingLayers")]
		public string TargetStartingLayers
		{
			get { return _targetStartingLayers; }
			set { if (_targetStartingLayers != value) { _targetStartingLayers = value; NotifyPropertyChanged(nameof(TargetStartingLayers)); } }
		}

		private string _targetStatus;
		[Status(DisplayName = "Target Status", GroupName = "Target", GroupOrder = 8, Description = "Target's current clothing status")]
		[XmlOrder(80)]
		[XmlAttribute("targetStatus")]
		[JsonProperty("targetStatus")]
		public string TargetStatus
		{
			get { return _targetStatus; }
			set { if (_targetStatus != value) { _targetStatus = value; NotifyPropertyChanged(nameof(TargetStatus)); } }
		}

		private string _alsoPlaying;
		[RecordSelect(DisplayName = "Also Playing", GroupName = "Also Playing", GroupOrder = 0, Description = "Character that is playing but not performing the current action", RecordType = typeof(Character), AllowCreate = true)]
		[XmlOrder(90)]
		[XmlAttribute("alsoPlaying")]
		[JsonProperty("alsoPlaying")]
		public string AlsoPlaying
		{
			get { return _alsoPlaying; }
			set { if (_alsoPlaying != value) { _alsoPlaying = value; NotifyPropertyChanged(nameof(AlsoPlaying)); } }
		}

		private string _alsoPlayingStage;
		[StageSelect(DisplayName = "Also Playing Stage", GroupName = "Also Playing", GroupOrder = 1, Description = "Character in Also Playing is currently within a range of stages", BoundProperties = new string[] { "AlsoPlaying" }, FilterStagesToTarget = false, SkinVariable = "~_.costume~")]
		[XmlOrder(100)]
		[XmlAttribute("alsoPlayingStage")]
		[JsonProperty("alsoPlayingStage")]
		public string AlsoPlayingStage
		{
			get { return _alsoPlayingStage; }
			set { if (_alsoPlayingStage != value) { _alsoPlayingStage = value; NotifyPropertyChanged(nameof(AlsoPlayingStage)); } }
		}

		private string _alsoPlayingHand;
		[ComboBox(DisplayName = "Also Playing Hand", GroupName = "Also Playing", GroupOrder = 6, Description = "Character in Also Playing has a particular poker hand",
			Options = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush" })]
		[XmlOrder(110)]
		[XmlAttribute("alsoPlayingHand")]
		[JsonProperty("alsoPlayingHand")]
		public string AlsoPlayingHand
		{
			get { return _alsoPlayingHand; }
			set { if (_alsoPlayingHand != value) { _alsoPlayingHand = value; NotifyPropertyChanged(nameof(AlsoPlayingHand)); } }
		}

		private string _targetHand;
		[ComboBox(DisplayName = "Target Hand", GroupName = "Target", GroupOrder = 7, Description = "Target has a particular poker hand",
			Options = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush" })]
		[XmlOrder(120)]
		[XmlAttribute("oppHand")]
		[JsonProperty("oppHand")]
		public string TargetHand
		{
			get { return _targetHand; }
			set { if (_targetHand != value) { _targetHand = value; NotifyPropertyChanged(nameof(TargetHand)); } }
		}

		private string _hasHand;
		[ComboBox(DisplayName = "Has Hand", GroupName = "Self", GroupOrder = 5, Description = "Character has a particular poker hand",
			Options = new string[] { "Nothing", "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Royal Flush" })]
		[XmlOrder(130)]
		[XmlAttribute("hasHand")]
		[JsonProperty("hasHand")]
		public string HasHand
		{
			get { return _hasHand; }
			set { if (_hasHand != value) { _hasHand = value; NotifyPropertyChanged(nameof(HasHand)); } }
		}

		private string _totalMales;
		[NumericRange(DisplayName = "Total Males", GroupName = "Table", GroupOrder = 2, Description = "Number of males playing (including this character and the player)", Minimum = 0, Maximum = 5)]
		[XmlOrder(140)]
		[XmlAttribute("totalMales")]
		[JsonProperty("totalMales")]
		public string TotalMales
		{
			get { return _totalMales; }
			set { if (_totalMales != value) { _totalMales = value; NotifyPropertyChanged(nameof(TotalMales)); } }
		}

		private string _totalFemales;
		[NumericRange(DisplayName = "Total Females", GroupName = "Table", GroupOrder = 1, Description = "Number of females playing (including this character and the player)", Minimum = 0, Maximum = 5)]
		[XmlOrder(150)]
		[XmlAttribute("totalFemales")]
		[JsonProperty("totalFemales")]
		public string TotalFemales
		{
			get { return _totalFemales; }
			set { if (_totalFemales != value) { _totalFemales = value; NotifyPropertyChanged(nameof(TotalFemales)); } }
		}

		private string _targetTimeInStage;
		[NumericRange(DisplayName = "Target Time in Stage", GroupName = "Target", GroupOrder = 6, Description = "Number of rounds since the last time the target lost a hand")]
		[XmlOrder(160)]
		[XmlAttribute("targetTimeInStage")]
		[JsonProperty("targetTimeInStage")]
		public string TargetTimeInStage
		{
			get { return _targetTimeInStage; }
			set { if (_targetTimeInStage != value) { _targetTimeInStage = value; NotifyPropertyChanged(nameof(TargetTimeInStage)); } }
		}

		private string _alsoPlayingTimeInStage;
		[NumericRange(DisplayName = "Also Playing Time in Stage", GroupName = "Also Playing", GroupOrder = 5, Description = "Number of rounds since the last time the Also Playing player lost a hand")]
		[XmlOrder(170)]
		[XmlAttribute("alsoPlayingTimeInStage")]
		[JsonProperty("alsoPlayingTimeInStage")]
		public string AlsoPlayingTimeInStage
		{
			get { return _alsoPlayingTimeInStage; }
			set { if (_alsoPlayingTimeInStage != value) { _alsoPlayingTimeInStage = value; NotifyPropertyChanged(nameof(AlsoPlayingTimeInStage)); } }
		}

		private string _timeInStage;
		[NumericRange(DisplayName = "Time in Stage", GroupName = "Self", GroupOrder = 4, Description = "Number of rounds since the last time this player lost a hand")]
		[XmlOrder(180)]
		[XmlAttribute("timeInStage")]
		[JsonProperty("timeInStage")]
		public string TimeInStage
		{
			get { return _timeInStage; }
			set { if (_timeInStage != value) { _timeInStage = value; NotifyPropertyChanged(nameof(TimeInStage)); } }
		}

		private string _consecutiveLosses;
		[NumericRange(DisplayName = "Consecutive Losses", GroupName = "Game", GroupOrder = 0, Description = "Number of hands the target player (or this player) has lost in a row")]
		[XmlOrder(190)]
		[XmlAttribute("consecutiveLosses")]
		[JsonProperty("consecutiveLosses")]
		public string ConsecutiveLosses
		{
			get { return _consecutiveLosses; }
			set { if (_consecutiveLosses != value) { _consecutiveLosses = value; NotifyPropertyChanged(nameof(ConsecutiveLosses)); } }
		}

		private string _totalPlaying;
		[NumericRange(DisplayName = "Total Playing", GroupName = "Table", GroupOrder = 3, Description = "Number of players still in the game", Minimum = 0, Maximum = 5)]
		[XmlOrder(200)]
		[XmlAttribute("totalAlive")]
		[JsonProperty("totalAlive")]
		public string TotalPlaying
		{
			get { return _totalPlaying; }
			set { if (_totalPlaying != value) { _totalPlaying = value; NotifyPropertyChanged(nameof(TotalPlaying)); } }
		}

		private string _totalExposed;
		[NumericRange(DisplayName = "Total Exposed", GroupName = "Table", GroupOrder = 4, Description = "Number of players who have exposed either their chest or crotch", Minimum = 0, Maximum = 5)]
		[XmlOrder(210)]
		[XmlAttribute("totalExposed")]
		[JsonProperty("totalExposed")]
		public string TotalExposed
		{
			get { return _totalExposed; }
			set { if (_totalExposed != value) { _totalExposed = value; NotifyPropertyChanged(nameof(TotalExposed)); } }
		}

		private string _totalNaked;
		[NumericRange(DisplayName = "Total Naked", GroupName = "Table", GroupOrder = 5, Description = "Number of players who have lost all their clothing, but might still be playing", Minimum = 0, Maximum = 5)]
		[XmlOrder(220)]
		[XmlAttribute("totalNaked")]
		[JsonProperty("totalNaked")]
		public string TotalNaked
		{
			get { return _totalNaked; }
			set { if (_totalNaked != value) { _totalNaked = value; NotifyPropertyChanged(nameof(TotalNaked)); } }
		}

		private string _totalMasturbating;
		[NumericRange(DisplayName = "Total Masturbating", GroupName = "Table", GroupOrder = 6, Description = "Number of players who are currently masturbating", Minimum = 0, Maximum = 5)]
		[XmlOrder(230)]
		[XmlAttribute("totalMasturbating")]
		[JsonProperty("totalMasturbating")]
		public string TotalMasturbating
		{
			get { return _totalMasturbating; }
			set { if (_totalMasturbating != value) { _totalMasturbating = value; NotifyPropertyChanged(nameof(TotalMasturbating)); } }
		}

		private string _totalFinished;
		[NumericRange(DisplayName = "Total Finished", GroupName = "Table", GroupOrder = 7, Description = "Number of players who finished masturbating and completely out of the game", Minimum = 0, Maximum = 5)]
		[XmlOrder(240)]
		[XmlAttribute("totalFinished")]
		[JsonProperty("totalFinished")]
		public string TotalFinished
		{
			get { return _totalFinished; }
			set { if (_totalFinished != value) { _totalFinished = value; NotifyPropertyChanged(nameof(TotalFinished)); } }
		}

		private string _totalRounds;
		[NumericRange(DisplayName = "Total Rounds", GroupName = "Game", GroupOrder = 1, Description = "Number of rounds since the game began")]
		[XmlOrder(250)]
		[XmlAttribute("totalRounds")]
		[JsonProperty("totalRounds")]
		public string TotalRounds
		{
			get { return _totalRounds; }
			set { if (_totalRounds != value) { _totalRounds = value; NotifyPropertyChanged(nameof(TotalRounds)); } }
		}

		private string _saidMarker;
		[MarkerCondition(DisplayName = "Said Marker", GroupName = "Self", GroupOrder = 0, Description = "Character has said a marker", ShowPrivate = true)]
		[XmlOrder(260)]
		[XmlAttribute("saidMarker")]
		[JsonProperty("saidMarker")]
		public string SaidMarker
		{
			get { return _saidMarker; }
			set { if (_saidMarker != value) { _saidMarker = value; NotifyPropertyChanged(nameof(SaidMarker)); } }
		}

		private string _notSaidMarker;
		[Marker(DisplayName = "Not Said Marker", GroupName = "Self", GroupOrder = 1, Description = "Character has not said a marker", ShowPrivate = true)]
		[XmlOrder(270)]
		[XmlAttribute("notSaidMarker")]
		[JsonProperty("notSaidMarker")]
		public string NotSaidMarker
		{
			get { return _notSaidMarker; }
			set { if (_notSaidMarker != value) { _notSaidMarker = value; NotifyPropertyChanged(nameof(NotSaidMarker)); } }
		}

		private string _alsoPlayingSaidMarker;
		[MarkerCondition(DisplayName = "Also Playing Said Marker", GroupName = "Also Playing", GroupOrder = 2, Description = "Another player has said a marker", ShowPrivate = false, BoundProperties = new string[] { "AlsoPlaying" })]
		[XmlOrder(280)]
		[XmlAttribute("alsoPlayingSaidMarker")]
		[JsonProperty("alsoPlayingSaidMarker")]
		public string AlsoPlayingSaidMarker
		{
			get { return _alsoPlayingSaidMarker; }
			set { if (_alsoPlayingSaidMarker != value) { _alsoPlayingSaidMarker = value; NotifyPropertyChanged(nameof(AlsoPlayingSaidMarker)); } }
		}

		private string _alsoPlayingNotSaidMarker;
		[Marker(DisplayName = "Also Playing Not Said Marker", GroupName = "Also Playing", GroupOrder = 3, Description = "Another player has not said a marker", ShowPrivate = false, BoundProperties = new string[] { "AlsoPlaying" })]
		[XmlOrder(290)]
		[XmlAttribute("alsoPlayingNotSaidMarker")]
		[JsonProperty("alsoPlayingNotSaidMarker")]
		public string AlsoPlayingNotSaidMarker
		{
			get { return _alsoPlayingNotSaidMarker; }
			set { if (_alsoPlayingNotSaidMarker != value) { _alsoPlayingNotSaidMarker = value; NotifyPropertyChanged(nameof(AlsoPlayingNotSaidMarker)); } }
		}

		private string _alsoPlayingSayingMarker;
		[MarkerCondition(DisplayName = "Also Playing Saying Marker", GroupName = "Also Playing", GroupOrder = 4, Description = "Another player is saying a marker at this very moment", ShowPrivate = false, BoundProperties = new string[] { "AlsoPlaying" })]
		[XmlOrder(300)]
		[XmlAttribute("alsoPlayingSayingMarker")]
		[JsonProperty("alsoPlayingSayingMarker")]
		public string AlsoPlayingSayingMarker
		{
			get { return _alsoPlayingSayingMarker; }
			set { if (_alsoPlayingSayingMarker != value) { _alsoPlayingSayingMarker = value; NotifyPropertyChanged(nameof(AlsoPlayingSayingMarker)); } }
		}

		private string _alsoPlayingSaying;
		[Text(DisplayName = "Also Playing Saying Text", GroupName = "Also Playing", GroupOrder = 5, Description = "Another player is saying some text at this very moment")]
		[XmlOrder(310)]
		[XmlAttribute("alsoPlayingSaying")]
		[JsonProperty("alsoPlayingSaying")]
		public string AlsoPlayingSaying
		{
			get { return _alsoPlayingSaying; }
			set { if (_alsoPlayingSaying != value) { _alsoPlayingSaying = value; NotifyPropertyChanged(nameof(AlsoPlayingSaying)); } }
		}

		private string _targetSaidMarker;
		[MarkerCondition(DisplayName = "Target Said Marker", GroupName = "Target", GroupOrder = 3, Description = "Target has said a marker", ShowPrivate = false, BoundProperties = new string[] { "Target" })]
		[XmlOrder(320)]
		[XmlAttribute("targetSaidMarker")]
		[JsonProperty("targetSaidMarker")]
		public string TargetSaidMarker
		{
			get { return _targetSaidMarker; }
			set { if (_targetSaidMarker != value) { _targetSaidMarker = value; NotifyPropertyChanged(nameof(TargetSaidMarker)); } }
		}

		private string _targetNotSaidMarker;
		[Marker(DisplayName = "Target Not Said Marker", GroupName = "Target", GroupOrder = 4, Description = "Target has not said a marker", ShowPrivate = false, BoundProperties = new string[] { "Target" })]
		[XmlOrder(330)]
		[XmlAttribute("targetNotSaidMarker")]
		[JsonProperty("targetNotSaidMarker")]
		public string TargetNotSaidMarker
		{
			get { return _targetNotSaidMarker; }
			set { if (_targetNotSaidMarker != value) { _targetNotSaidMarker = value; NotifyPropertyChanged(nameof(TargetNotSaidMarker)); } }
		}

		private string _targetSayingMarker;
		[MarkerCondition(DisplayName = "Target Saying Marker", GroupName = "Target", GroupOrder = 5, Description = "Target is saying a marker at this very moment", ShowPrivate = false, BoundProperties = new string[] { "Target" })]
		[XmlOrder(340)]
		[XmlAttribute("targetSayingMarker")]
		[JsonProperty("targetSayingMarker")]
		public string TargetSayingMarker
		{
			get { return _targetSayingMarker; }
			set { if (_targetSayingMarker != value) { _targetSayingMarker = value; NotifyPropertyChanged(nameof(TargetSayingMarker)); } }
		}

		private string _targetSaying;
		[Text(DisplayName = "Target Saying Text", GroupName = "Target", GroupOrder = 6, Description = "Target is saying some text at this very moment")]
		[XmlOrder(350)]
		[XmlAttribute("targetSaying")]
		[JsonProperty("targetSaying")]
		public string TargetSaying
		{
			get { return _targetSaying; }
			set { if (_targetSaying != value) { _targetSaying = value; NotifyPropertyChanged(nameof(TargetSaying)); } }
		}

		private string _priority;
		[XmlOrder(360)]
		[XmlAttribute("priority")]
		[JsonProperty("priority")]
		public string CustomPriority
		{
			get { return _priority; }
			set { if (_priority != value) { _priority = value; NotifyPropertyChanged(nameof(CustomPriority)); } }
		}

		private string _addCharacterTags;
		[XmlOrder(370)]
		[XmlAttribute("addCharacterTags")]
		[JsonProperty("addCharacterTags")]
		public string AddCharacterTags
		{
			get { return _addCharacterTags; }
			set { if (_addCharacterTags != value) { _addCharacterTags = value; NotifyPropertyChanged(nameof(AddCharacterTags)); } }
		}

		private string _removeCharacterTags;
		[XmlOrder(380)]
		[XmlAttribute("removeCharacterTags")]
		[JsonProperty("removeCharacterTags")]
		public string RemoveCharacterTags
		{
			get { return _removeCharacterTags; }
			set { if (_removeCharacterTags != value) { _removeCharacterTags = value; NotifyPropertyChanged(nameof(RemoveCharacterTags)); } }
		}

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
		[XmlOrder(390)]
		[XmlElement("condition")]
		[JsonProperty("counters")]
		public ObservableCollection<TargetCondition> Conditions
		{
			get { return Get<ObservableCollection<TargetCondition>>(); }
			set { Set(value); }
		}

		[Expression(DisplayName = "Variable Test (+)", GroupName = "Game", GroupOrder = 5, Description = "Tests the value of a variable. Multiple can be added", BoundProperties = new string[] { "Target", "AlsoPlaying" })]
		[XmlOrder(400)]
		[XmlElement("test")]
		[JsonProperty("tests")]
		public ObservableCollection<ExpressionTest> Expressions
		{
			get { return Get<ObservableCollection<ExpressionTest>>(); }
			set { Set(value); }
		}

		[JsonProperty("lines")]
		[XmlOrder(410)]
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
		public ObservableCollection<int> Stages
		{
			get { return Get<ObservableCollection<int>>(); }
			set { Set(value); }
		}

		[DefaultValue("")]
		[XmlAttribute("stage")]
		public string StageRange
		{
			get
			{
				return GUIHelper.ListToString(Stages);
			}
			set
			{
				Stages = GUIHelper.StringToList(value);
			}
		}

		/// <summary>
		/// Whether this case is considered the "default" for its tag
		/// </summary>
		[XmlIgnore]
		public bool IsDefault;

		public Case()
		{
			_globalId = s_globalId++;
			Lines = new List<DialogueLine>();
			Stages = new ObservableCollection<int>();
			Conditions = new ObservableCollection<TargetCondition>();
			Expressions = new ObservableCollection<ExpressionTest>();
			AlternativeConditions = new ObservableCollection<Case>();
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
			if (HasConditions)
			{
				result += " " + ToConditionsString(false);
			}
			if (string.IsNullOrEmpty(Hidden))
			{
				int priority = GetPriority();
				if (priority > 0)
				{
					result += string.Format(" (priority={0})", priority);
				}
			}
			return result;
		}

		public string ToConditionsString(bool excludeTarget)
		{
			List<string> results = new List<string>();
			foreach (Case alternate in AlternativeConditions)
			{
				results.Add(alternate.ToConditionsString(excludeTarget));
			}
			List<string> result = new List<string>();
			if (!string.IsNullOrEmpty(Target) && !excludeTarget)
			{
				result.Add(string.Format("(target={0})", Target));
			}
			if (!string.IsNullOrEmpty(TargetStage))
			{
				result.Add(string.Format("(target stage={0})", TargetStage));
			}
			if (!string.IsNullOrEmpty(TargetStatus))
			{
				result.Add(string.Format("(target {0})", TargetStatus.Replace("_", " ")));
			}
			if (!string.IsNullOrEmpty(TargetHand))
			{
				result.Add(string.Format("(target hand={0})", TargetHand));
			}
			if (!string.IsNullOrEmpty(TargetTimeInStage))
			{
				result.Add(string.Format("(after {0} rounds in stage)", GUIHelper.RangeToString(TargetTimeInStage)));
			}
			if (!string.IsNullOrEmpty(TargetLayers))
			{
				result.Add($"(layers remaining={TargetLayers})");
			}
			if (!string.IsNullOrEmpty(Filter))
			{
				result.Add(string.Format("(filter={0})", Filter));
			}
			if (!string.IsNullOrEmpty(AlsoPlaying) && (!excludeTarget || !string.IsNullOrEmpty(Target)))
			{
				result.Add(string.Format("(playing w/{0})", AlsoPlaying));
			}
			if (!string.IsNullOrEmpty(AlsoPlayingStage))
			{
				result.Add(string.Format("(playing w/stage={0})", AlsoPlayingStage));
			}
			if (!string.IsNullOrEmpty(AlsoPlayingTimeInStage))
			{
				result.Add(string.Format("(after {0} rounds in stage)", GUIHelper.RangeToString(AlsoPlayingTimeInStage)));
			}
			if (!string.IsNullOrEmpty(AlsoPlayingHand))
			{
				result.Add(string.Format("(playing w/hand={0})", AlsoPlayingHand));
			}
			if (!string.IsNullOrEmpty(HasHand))
			{
				result.Add(string.Format("(hand={0})", HasHand));
			}
			if (Conditions.Count > 0)
			{
				result.Add(string.Format("({0})", string.Join(",", Conditions)));
			}
			if (!string.IsNullOrEmpty(TotalFemales))
			{
				result.Add(string.Format("({0} females)", GUIHelper.RangeToString(TotalFemales)));
			}
			if (!string.IsNullOrEmpty(TotalMales))
			{
				result.Add(string.Format("({0} males)", GUIHelper.RangeToString(TotalMales)));
			}
			if (!string.IsNullOrEmpty(TotalRounds))
			{
				result.Add(string.Format("({0} overall rounds)", GUIHelper.RangeToString(TotalRounds)));
			}
			if (!string.IsNullOrEmpty(TimeInStage))
			{
				result.Add(string.Format("(after {0} rounds in own stage)", GUIHelper.RangeToString(TimeInStage)));
			}
			if (!string.IsNullOrEmpty(ConsecutiveLosses))
			{
				result.Add(string.Format("({0} losses in a row)", GUIHelper.RangeToString(ConsecutiveLosses)));
			}
			if (!string.IsNullOrEmpty(TotalPlaying))
			{
				result.Add(string.Format("({0} playing)", GUIHelper.RangeToString(TotalPlaying)));
			}
			if (!string.IsNullOrEmpty(TotalExposed))
			{
				result.Add(string.Format("({0} exposed)", GUIHelper.RangeToString(TotalExposed)));
			}
			if (!string.IsNullOrEmpty(TotalNaked))
			{
				result.Add(string.Format("({0} naked)", GUIHelper.RangeToString(TotalNaked)));
			}
			if (!string.IsNullOrEmpty(TotalMasturbating))
			{
				result.Add(string.Format("({0} finishing)", GUIHelper.RangeToString(TotalMasturbating)));
			}
			if (!string.IsNullOrEmpty(TotalFinished))
			{
				result.Add(string.Format("({0} finished)", GUIHelper.RangeToString(TotalFinished)));
			}
			if (!string.IsNullOrEmpty(SaidMarker))
			{
				result.Add(string.Format("(said {0})", SaidMarker));
			}
			if (!string.IsNullOrEmpty(NotSaidMarker))
			{
				result.Add(string.Format("(not said {0})", NotSaidMarker));
			}
			if (!string.IsNullOrEmpty(TargetSaidMarker))
			{
				result.Add(string.Format("(target said {0})", TargetSaidMarker));
			}
			if (!string.IsNullOrEmpty(TargetNotSaidMarker))
			{
				result.Add(string.Format("(target not said {0})", TargetNotSaidMarker));
			}
			if (!string.IsNullOrEmpty(TargetSayingMarker))
			{
				result.Add(string.Format("(target saying {0})", TargetSayingMarker));
			}
			if (!string.IsNullOrEmpty(TargetSaying))
			{
				result.Add(string.Format("(target saying \"{0}\")", TargetSaying));
			}
			if (!string.IsNullOrEmpty(AlsoPlayingSaidMarker))
			{
				result.Add(string.Format("(other said {0})", AlsoPlayingSaidMarker));
			}
			if (!string.IsNullOrEmpty(AlsoPlayingNotSaidMarker))
			{
				result.Add(string.Format("(other not said {0})", AlsoPlayingNotSaidMarker));
			}
			if (!string.IsNullOrEmpty(AlsoPlayingSayingMarker))
			{
				result.Add(string.Format("(other saying {0})", AlsoPlayingSayingMarker));
			}
			if (!string.IsNullOrEmpty(AlsoPlayingSaying))
			{
				result.Add(string.Format("(other saying \"{0}\")", AlsoPlayingSaying));
			}
			if (Expressions.Count > 0)
			{
				result.Add($"({string.Join(",", Expressions)})");
			}
			if (OneShotId > 0)
			{
				result.Add("(play once)");
			}
			results.Add(string.Join(" ", result));
			string conditions = string.Join(" OR ", results);
			if (AlternativeConditions.Count > 0)
			{
				conditions = "*" + conditions;
			}
			return conditions;
		}

		/// <summary>
		/// Copies the case except stages and lines
		/// </summary>
		/// <returns></returns>
		public Case CopyConditions()
		{
			Case copy = new Case();
			foreach (MemberInfo field in this.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance))
			{
				if (field.Name == "StageRange")
				{
					continue;
				}
				if (field.MemberType == MemberTypes.Field || field.MemberType == MemberTypes.Property)
				{
					if (field.GetDataType() == typeof(string) || field.GetDataType() == typeof(int))
					{
						field.SetValue(copy, field.GetValue(this));
					}
				}
			}

			//Since it's just a shallow collection, need to break references to objects
			copy.Conditions = new ObservableCollection<TargetCondition>();
			foreach (TargetCondition condition in Conditions)
			{
				copy.Conditions.Add(condition.Copy());
			}

			copy.Expressions = new ObservableCollection<ExpressionTest>();
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
			copy.AlternativeConditions = new ObservableCollection<Case>();
			foreach (Case alternate in AlternativeConditions)
			{
				copy.AlternativeConditions.Add(alternate.Copy());
			}
			return copy;
		}

		public void ClearEmptyValues()
		{
			foreach (MemberInfo field in this.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance))
			{
				if (field.Name == "StageRange")
				{
					continue;
				}
				if (field.MemberType == MemberTypes.Field || field.MemberType == MemberTypes.Property)
				{
					if (field.GetDataType() == typeof(string) && (string)field.GetValue(this) == "")
					{
						field.SetValue(this, null);
					}
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
			if (!string.IsNullOrEmpty(Hidden))
			{
				return int.MinValue;
			}
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
			if (!string.IsNullOrEmpty(TargetSaying))
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
				if (!string.IsNullOrEmpty(AlsoPlayingSaying))
					totalPriority += 1;
			}

			if (!string.IsNullOrEmpty(TimeInStage))
				totalPriority += 8;

			if (!string.IsNullOrEmpty(SaidMarker))
				totalPriority += 1;
			if (!string.IsNullOrEmpty(NotSaidMarker))
				totalPriority += 1;

			totalPriority += Conditions.Sum(c => c.GetPriority());
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
		/// Gets whether the lines in this case are identical to another
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool MatchesLines(Case other)
		{
			if (Lines.Count != other.Lines.Count)
			{
				return false;
			}
			for (int i = 0; i < Lines.Count; i++)
			{
				if (Lines[i].GetHashCode() != other.Lines[i].GetHashCode())
				{
					return false;
				}
			}
			return true;
		}

		private int _conditionHash;
		protected override void OnPropertyChanged(string propName)
		{
			_conditionHash = 0;
		}

		private int GetConditionHash()
		{
			if (_conditionHash > 0)
			{
				return _conditionHash;
			}
			int hash = (Target ?? "").GetHashCode();
			hash = (hash * 397) ^ (TargetHand ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetTimeInStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetStatus ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetLayers ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetStartingLayers ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlaying ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingHand ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingTimeInStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (HasHand ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Filter ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TimeInStage ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalFemales ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalMales ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalPlaying ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalExposed ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalMasturbating ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalNaked ?? string.Empty).GetHashCode();
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
			hash = (hash * 397) ^ (ConsecutiveLosses ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetSaying ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingSaying ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AddCharacterTags ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (RemoveCharacterTags ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (CustomPriority ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (Hidden ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (OneShotId > 0 ? OneShotId : -1);
			_conditionHash = hash;
			return hash;
		}

		/// <summary>
		/// Gets whether this case matches another in everything but conditions
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool MatchesNonConditions(Case other)
		{
			if (other == this)
				return true;
			if (Tag != other.Tag)
				return false;

			if (other.Lines.Count != Lines.Count)
			{
				return false;
			}
			if (other.GetLineCode() != GetLineCode())
			{
				return false;
			}
			if (other.RemoveCharacterTags != RemoveCharacterTags ||
				other.AddCharacterTags != AddCharacterTags ||
				other.CustomPriority != CustomPriority)
			{
				return false;
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

			bool sameFilters = (GetConditionHash() == other.GetConditionHash());
			if (!sameFilters)
				return false;

			if (other.Conditions.Count != Conditions.Count)
				return false;
			for (int i = 0; i < Conditions.Count; i++)
			{
				TargetCondition c1 = Conditions[i];
				TargetCondition c2 = other.Conditions[i];
				if (!c1.Equals(c2))
					return false;
			}
			if (other.Expressions.Count != Expressions.Count)
				return false;
			for (int i = 0; i < Expressions.Count; i++)
			{
				if (!Expressions[i].Equals(other.Expressions[i]))
				{
					return false;
				}
			}

			return true;
		}

		public bool HasConditions
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
				  !string.IsNullOrEmpty(TargetSaying) ||
				  !string.IsNullOrEmpty(AlsoPlayingSaying) ||
				  !string.IsNullOrEmpty(Hidden) ||
				  Conditions.Count > 0 ||
				  Expressions.Count > 0;
			}
		}

		/// <summary>
		/// Gets whether this case has any Filters
		/// </summary>
		public bool HasFilters
		{
			get
			{
				if (!string.IsNullOrEmpty(Filter))
				{
					if (CharacterDatabase.GetById(Filter) == null)
					{
						return true;
					}
				}
				return Conditions.Any(c =>
				{
					bool result = false;
					if (!string.IsNullOrEmpty(c.FilterId))
					{
						Character character = CharacterDatabase.GetById(c.FilterId);
						result = (character == null);
					}
					return result;
				});
			}
		}

		/// <summary>
		/// Gets whether this case has any direct targeted dialogue towards other players
		/// </summary>
		public bool HasTargetedConditions
		{
			get
			{
				bool targeted = !string.IsNullOrEmpty(Target) || !string.IsNullOrEmpty(AlsoPlaying) || (!string.IsNullOrEmpty(Filter) && CharacterDatabase.GetById(Filter) != null);
				if (!targeted)
				{
					foreach (TargetCondition condition in Conditions)
					{
						if (!string.IsNullOrEmpty(condition.FilterId))
						{
							targeted = true;
							break;
						}
					}
				}
				return targeted;
			}
		}

		[XmlIgnore]
		public ObservableCollection<Case> AlternativeConditions
		{
			get { return Get<ObservableCollection<Case>>(); }
			set { Set(value); }
		}

		public IEnumerable<Case> GetConditionSets()
		{
			yield return this;
			foreach (Case alternate in AlternativeConditions)
			{
				yield return alternate;
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
					!string.IsNullOrEmpty(ConsecutiveLosses) ||
					!string.IsNullOrEmpty(HasHand) ||
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
			hash = (hash * 397) ^ GetConditionHash();
			foreach (var condition in Conditions)
			{
				hash = (hash * 397) ^ condition.GetHashCode();
			}
			foreach (ExpressionTest expr in Expressions)
			{
				hash = (hash * 397) ^ expr.GetHashCode();
			}
			return hash;
		}

		/// <summary>
		/// Returns a unique hash for this combination of lines
		/// </summary>
		/// <returns></returns>
		public int GetLineCode()
		{
			int hash = 0;
			foreach (DialogueLine line in Lines)
			{
				hash = (hash * 397) ^ line.GetHashCode();
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
			if (response.Tag == null)
			{
				return null;
			}

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
					FilterTag = AlsoPlaying
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

			string otherId = CharacterDatabase.GetId(responder);
			foreach (ExpressionTest test in Expressions)
			{
				if (test.GetTarget() == otherId)
				{
					ExpressionTest copy = test.Copy();
					copy.ChangeTarget("self");
					response.Expressions.Add(copy);
				}
			}

			//if no stages have been set, apply it to all
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(response.Tag);
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
			foreach (TargetCondition cond in Conditions)
			{
				if (cond.FilterTag != responder.FolderName)
				{
					response.Conditions.Add(cond);
				}
			}
			foreach (ExpressionTest test in Expressions)
			{
				if (!test.RefersTo(speaker, speaker, Target) && !test.RefersTo(responder, speaker, Target))
				{
					response.Expressions.Add(test);
				}
			}
			response.ConsecutiveLosses = ConsecutiveLosses;
			response.TotalFemales = TotalFemales;
			response.TotalMales = TotalMales;
			response.TotalPlaying = TotalPlaying;
			response.TotalExposed = TotalExposed;
			response.TotalNaked = TotalNaked;
			response.TotalMasturbating = TotalMasturbating;
			response.TotalFinished = TotalFinished;
			response.TotalRounds = TotalRounds;

			//special cases
			if (Tag == "must_masturbate_first")
			{
				response.TotalMasturbating = "0";
				response.TotalFinished = "0";
			}

			foreach (Case alternate in AlternativeConditions)
			{
				Case alternateResponse = alternate.CreateResponse(speaker, responder);
				response.AlternativeConditions.Add(alternateResponse);
			}

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
			other.TargetSaying = TargetSaying;
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
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(other.Tag);
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
			foreach (ExpressionTest test in Expressions)
			{
				if (test.GetTarget() == "target")
				{
					ExpressionTest copy = test.Copy();
					copy.ChangeTarget("self");
					other.Expressions.Add(copy);
				}
			}
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
			other.AlsoPlayingSaying = AlsoPlayingSaying;
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
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(Tag);
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

			//If all lines set the same marker, use that marker in alsoPlayingSayingMarker
			if (Lines.Count > 0)
			{
				string marker = Lines[0].Marker;
				for (int l = 1; l < Lines.Count; l++)
				{
					DialogueLine line = Lines[l];
					if (line.Marker != marker)
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
					if (other.AlsoPlayingNotSaidMarker == marker)
					{
						//if they had a not said marker for the same thing, clear that
						other.AlsoPlayingNotSaidMarker = null;
					}
				}
			}

			string id = CharacterDatabase.GetId(speaker);
			foreach (ExpressionTest test in Expressions)
			{
				if (test.GetTarget() == "self")
				{
					ExpressionTest copy = test.Copy();
					copy.ChangeTarget(id);
					other.Expressions.Add(copy);
				}
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
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(other.Tag);
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
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(Tag);
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

			if (other.Tag.Contains("crotch") || other.Tag.Contains("chest"))
			{
				//if there is only one important for these layers, don't both including a targetStage
				string position = (other.Tag.Contains("crotch") ? "lower" : "upper");
				int layerCount = speaker.Wardrobe.Count(c => c.Position == position && c.Type == "important");
				if (layerCount == 1)
				{
					speakerStageRange = null;
				}
			}

			other.Target = speaker.FolderName;
			other.TargetStage = speakerStageRange;
			other.TargetTimeInStage = TimeInStage;
			other.TargetHand = HasHand;
			other.TargetNotSaidMarker = NotSaidMarker;
			other.TargetSaidMarker = SaidMarker;

			//If all lines set the same marker, use that marker in alsoPlayingSayingMarker
			if (Lines.Count > 0)
			{
				string marker = Lines[0].Marker;
				for (int l = 1; l < Lines.Count; l++)
				{
					DialogueLine line = Lines[l];
					if (line.Marker != marker)
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
					other.TargetSayingMarker = marker;
					if (other.TargetNotSaidMarker == marker)
					{
						//if they had a not said marker for the same thing, clear that
						other.TargetNotSaidMarker = null;
					}
				}
			}

			foreach (ExpressionTest test in Expressions)
			{
				if (test.GetTarget() == "self")
				{
					ExpressionTest copy = test.Copy();
					copy.ChangeTarget("target");
					other.Expressions.Add(copy);
				}
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
			string layerType = layer.Type;
			if (layer.Type == "major" && (string.IsNullOrEmpty(layer.Position) || layer.Position == "upper" || layer.Position == "lower"))
			{
				//if this is the last major and there are no importants, treat as important
				bool foundImportant = false;
				for (int l = speaker.Layers - stage - 2; l >= 0; l--)
				{
					Clothing other = speaker.Wardrobe[l];
					if ((other.Type == "important" || other.Type == "major") && (other.Position == "both" || other.Position == layer.Position))
					{
						foundImportant = true;
						break;
					}
				}
				if (!foundImportant)
				{
					layerType = "important";
				}
			}
			if (layerType == "extra")
			{
				return "accessory";
			}
			else if (layerType == "important")
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
			return layerType;
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
					if (speaker.Metadata.CrossGender)
					{
						return "opponent_stripping";
					}
					return $"{gender}_removing_{layer}";
				}
				else
				{
					if (speaker.Metadata.CrossGender)
					{
						return $"opponent_{layer}_will_be_visible";
					}
					return $"{gender}_{layer}_will_be_visible";
				}
			}
			else if (Tag == "stripped")
			{
				int stage = Stages.Min(s => s);
				string layer = GetLayerType(speaker, stage - 1);
				if (layer == "major" || layer == "minor" || layer == "accessory")
				{
					if (speaker.Metadata.CrossGender)
					{
						return "opponent_stripped";
					}
					return $"{gender}_removed_{layer}";
				}
				else
				{
					if (speaker.Metadata.CrossGender)
					{
						return $"opponent_{layer}_is_visible";
					}
					else if (gender == "female" && layer == "chest" || gender == "male" && layer == "crotch")
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
				if (speaker.Metadata.CrossGender)
				{
					return "opponent_lost";
				}
				return $"{gender}_must_masturbate";
			}
			else if (Tag == "must_masturbate")
			{
				if (speaker.Metadata.CrossGender)
				{
					return "opponent_lost";
				}
				return $"{gender}_must_masturbate";
			}
			else if (Tag == "start_masturbating" || Tag == "masturbating" || Tag == "finished_masturbating" || Tag == "heavy_masturbating")
			{
				if (speaker.Metadata.CrossGender)
				{
					return $"opponent_{Tag}";
				}
				return $"{gender}_{Tag}";
			}
			else if (Tag.StartsWith("must_strip"))
			{
				if (speaker.Metadata.CrossGender)
				{
					return "opponent_lost";
				}
				return $"{gender}_must_strip";
			}
			else if (Tag == "game_over_victory")
			{
				return "game_over_defeat";
			}
			else if (Tag == "after_masturbating")
			{
				return "hand";
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
			else if (tag.StartsWith("opponent_"))
			{
				tag = tag.Substring(9);
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
				else if (tag == "opponent_lost")
				{
					return "must_strip";
				}
				else if (tag == "opponent_stripping")
				{
					return "stripping";
				}
				else if (tag == "opponent_stripped")
				{
					return "stripped";
				}
				else if (tag == "game_over_defeat")
				{
					return "game_over_victory";
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
				if (response == null)
				{
					//if that failed, there is no way to respond to this tag
					return cases;
				}
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
				if (!string.IsNullOrEmpty(c.Filter) && speaker.Tags.Find(t => t.Tag == c.Filter) == null)
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
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(Tag);

			if (character.Key == "human")
			{
				return true;
			}

			if (trigger.Size != null && trigger.Size != character.Size)
			{
				return false;
			}

			if (trigger.Gender == null || trigger.Gender == character.Gender || character.Metadata.CrossGender)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets whether at least one line touches a collectible
		/// </summary>
		public bool HasCollectible
		{
			get
			{
				foreach (DialogueLine line in Lines)
				{
					if (!string.IsNullOrEmpty(line.CollectibleId))
					{
						return true;
					}
				}
				return false;
			}
		}

		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
			if (!string.IsNullOrEmpty(Stage))
			{
				Tuple<int, int> interval = GUIHelper.ToInterval(Stage);
				for (int i = interval.Item1; i <= interval.Item2; i++)
				{
					Stages.Add(i);
				}
			}
		}

		/// <summary>
		/// Gets a list of markers that this case assumes are being said
		/// </summary>
		/// <returns></returns>
		public List<string> GetMarkers()
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(SaidMarker) && (!SaidMarker.Contains("!=") || SaidMarker.EndsWith("!=0")) && !SaidMarker.EndsWith("==0"))
			{
				int splitIndex = SaidMarker.IndexOfAny(new char[] { '=', '>', '<', '!' });
				if (splitIndex > 0)
				{
					list.Add(SaidMarker.Substring(0, splitIndex));
				}
				else
				{
					list.Add(SaidMarker);
				}
			}
			foreach (ExpressionTest test in Expressions)
			{
				if (test.Expression.StartsWith("~marker.") || test.Expression.StartsWith("~self.marker."))
				{
					if (test.Operator != "!=" || (test.Operator == "!=" && test.Value == "0"))
					{
						int dot = test.Expression.LastIndexOf('.');
						if (dot >= 0)
						{
							string marker = test.Expression.Substring(dot + 1, test.Expression.Length - (dot + 2));
							list.Add(marker);
						}
					}
				}
			}
			return list;
		}

		public int GetSliceCount()
		{
			return Lines.Count;
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

using System;
using System.Collections.Generic;
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

		[XmlAttribute("alsoPlaying")]
		public string AlsoPlaying;

		[XmlAttribute("alsoPlayingHand")]
		public string AlsoPlayingHand;

		[XmlAttribute("alsoPlayingStage")]
		public string AlsoPlayingStage;

		[XmlAttribute("hasHand")]
		public string HasHand;

		[XmlAttribute("filter")]
		public string Filter;

		[XmlAttribute("tag")]
		public string Tag;

		[XmlAttribute("totalNaked")]
		public string TotalNaked;

		[XmlAttribute("totalMasturbating")]
		public string TotalFinishing;

		[XmlAttribute("totalFinished")]
		public string TotalFinished;

		[XmlAttribute("totalExposed")]
		public string TotalExposed;

		[XmlAttribute("totalAlive")]
		public string TotalPlaying;

		[XmlAttribute("timeInStage")]
		public string TimeInStage;

		[XmlAttribute("targetTimeInStage")]
		public string TargetTimeInStage;

		[XmlAttribute("target")]
		public string Target;

		[XmlAttribute("targetStage")]
		public string TargetStage;

		[XmlAttribute("targetLayers")]
		public string TargetLayers;

		[XmlAttribute("targetStatus")]
		public string TargetStatus;

		[XmlAttribute("targetSaidMarker")]
		public string TargetSaidMarker;

		[XmlAttribute("targetNotSaidMarker")]
		public string TargetNotSaidMarker;

		[XmlAttribute("notSaidMarker")]
		public string NotSaidMarker;

		[XmlAttribute("priority")]
		public string CustomPriority;

		[XmlAttribute("saidMarker")]
		public string SaidMarker;

		[XmlAttribute("oppHand")]
		public string TargetHand;

		[XmlAttribute("totalFemales")]
		public string TotalFemales;

		[XmlAttribute("totalMales")]
		public string TotalMales;

		[XmlAttribute("totalRounds")]
		public string TotalRounds;

		[XmlAttribute("consecutiveLosses")]
		public string ConsecutiveLosses;

		[XmlAttribute("alsoPlayingTimeInStage")]
		public string AlsoPlayingTimeInStage;

		[XmlAttribute("alsoPlayingSaidMarker")]
		public string AlsoPlayingSaidMarker;

		[XmlAttribute("alsoPlayingNotSaidMarker")]
		public string AlsoPlayingNotSaidMarker;

		[XmlElement("condition")]
		public List<TargetCondition> Conditions;

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
		}

		public Case(string tag)
		{
			_globalId = s_globalId++;
			Lines = new List<DialogueLine>();
			Conditions = new List<TargetCondition>();
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
				if (!string.IsNullOrEmpty(TargetHand))
				{
					result += string.Format(" (target hand={0})", TargetHand);
				}
				if (!string.IsNullOrEmpty(TargetTimeInStage))
				{
					result += string.Format(" (after {0} rounds in stage)", TargetTimeInStage);
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
				if (!string.IsNullOrEmpty(TotalFinishing))
				{
					result += string.Format(" ({0} finishing)", TotalFinishing);
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
				if (!string.IsNullOrEmpty(AlsoPlayingSaidMarker))
				{
					result += string.Format(" (other said {0})", AlsoPlayingSaidMarker);
				}
				if (!string.IsNullOrEmpty(AlsoPlayingNotSaidMarker))
				{
					result += string.Format(" (other not said {0})", AlsoPlayingNotSaidMarker);
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
			}

			if (!string.IsNullOrEmpty(TimeInStage))
				totalPriority += 8;

			if (!string.IsNullOrEmpty(SaidMarker))
				totalPriority += 1;
			if (!string.IsNullOrEmpty(NotSaidMarker))
				totalPriority += 1;

			totalPriority += Conditions.Count * 10;

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
			if (!string.IsNullOrEmpty(TotalFinishing))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalFinished))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalRounds))
				totalPriority += 10;

			return totalPriority;
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
				TotalFinishing == other.TotalFinishing &&
				TotalNaked == other.TotalNaked &&
				TotalFinished == other.TotalFinished &&
				TotalRounds == other.TotalRounds &&
				SaidMarker == other.SaidMarker &&
				NotSaidMarker == other.NotSaidMarker &&
				TargetSaidMarker == other.TargetSaidMarker &&
				TargetNotSaidMarker == other.TargetNotSaidMarker &&
				AlsoPlayingSaidMarker == other.AlsoPlayingSaidMarker &&
				AlsoPlayingNotSaidMarker == other.AlsoPlayingNotSaidMarker &&
				ConsecutiveLosses == other.ConsecutiveLosses;
			if (!sameFilters)
				return false;

			if (other.Conditions.Count != Conditions.Count)
				return false;
			for (int i = 0; i < Conditions.Count; i++)
			{
				TargetCondition c1 = Conditions[i];
				TargetCondition c2 = other.Conditions[i];
				if (c1.Filter != c2.Filter || c1.Count != c2.Count)
					return false;
			}

			return true;
		}

		/// <summary>
		/// Gets whether this case looks like another even if they aren't the same instance
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool LooksLike(Case other)
		{
			if (!MatchesConditions(other))
				return false;

			if (Lines.Count != other.Lines.Count)
				return false;

			//Compare lines
			for (int i = 0; i < Lines.Count; i++)
			{
				DialogueLine line1 = Lines[i];
				DialogueLine line2 = other.Lines[i];
				string image1 = DialogueLine.GetDefaultImage(line1.Image);
				string image2 = DialogueLine.GetDefaultImage(line2.Image);
				if (image1 != image2 || line1.Text != line2.Text)
					return false;
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
				  !string.IsNullOrEmpty(TotalFinishing) ||
				  !string.IsNullOrEmpty(TotalFinished) ||
				  !string.IsNullOrEmpty(TotalRounds) ||
				  !string.IsNullOrEmpty(TargetSaidMarker) ||
				  !string.IsNullOrEmpty(TargetNotSaidMarker) ||
				  !string.IsNullOrEmpty(AlsoPlayingSaidMarker) ||
				  !string.IsNullOrEmpty(AlsoPlayingNotSaidMarker) ||
				  !string.IsNullOrEmpty(SaidMarker) ||
				  !string.IsNullOrEmpty(NotSaidMarker) ||
				  Conditions.Count > 0;
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
					 !string.IsNullOrEmpty(AlsoPlayingSaidMarker) ||
					 !string.IsNullOrEmpty(AlsoPlayingNotSaidMarker);
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
					!string.IsNullOrEmpty(TotalFinishing) ||
					!string.IsNullOrEmpty(TotalFinished) ||
					!string.IsNullOrEmpty(TotalRounds) ||
					!string.IsNullOrEmpty(TotalFemales) ||
					!string.IsNullOrEmpty(TotalMales) ||
					Conditions.Count > 0;
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
			hash = (hash * 397) ^ (TotalFinishing ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalFinished ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TotalRounds ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (SaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (NotSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (TargetNotSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (AlsoPlayingNotSaidMarker ?? string.Empty).GetHashCode();
			hash = (hash * 397) ^ (CustomPriority ?? string.Empty).GetHashCode();
			foreach (var condition in Conditions)
			{
				hash = (hash * 397) ^ (condition.Filter ?? string.Empty).GetHashCode();
				hash = (hash * 397) ^ condition.Count.GetHashCode();
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
		/// Creates a working case that is a direct response to this one. Assumes that this case has a target
		/// </summary>
		/// <param name="speaker">The character speaking the lines from this case that should be responded to</param>
		/// <param name="responder">The character who will say the response that is being created</param>
		/// <returns></returns>
		public Case CreateResponse(Character speaker, Character responder)
		{
			Case response = new Case();
			int minStage, maxStage;

			string speakerStageRange = null;
			int min = Stages.Min(stage => stage);
			int max = Stages.Max(stage => stage);
			if (max != min)
				speakerStageRange = min + "-" + max;
			else speakerStageRange = min.ToString();
			//iF it applies to all stages, leave it blank
			if (min == 0 && max == speaker.Layers + Clothing.ExtraStages - 1)
				speakerStageRange = null;

			string gender = (Tag.StartsWith("male_") ? "male" : Tag.StartsWith("female_") ? "female" : null);
			if (Target == responder.FolderName || responder.Tags.Exists(t => t == Filter))
			{
				//Responder is the target

				if (AlsoPlaying != null || (gender != null && gender != responder.Gender))
				{
					//If the responder is the target, but there was also an AlsoPlaying, it's impossible to respond directly to this case since 
					//you can't use two AlsoPlayings
					return null;
				}

				if (!string.IsNullOrEmpty(TargetStage))
				{
					ToRange(TargetStage, out minStage, out maxStage);
					for (int stage = minStage; stage <= maxStage; stage++)
					{
						response.Stages.Add(stage);
					}
				}
				else
				{
					List<int> targetStages = GetTargetStage(Tag, responder);
					if (targetStages.Count == 0)
					{
						//If not limited to a stage, use all stages by default
						for (int i = 0; i < responder.Layers + Clothing.ExtraStages; i++)
						{
							response.Stages.Add(i);
						}
					}
					else
					{
						response.Stages.AddRange(targetStages);
					}
				}

				//Figure out the corresponding responder tag
				response.Tag = TriggerDatabase.GetOppositeTag(Tag, speaker, response.Stages[0]);
				if (response.Tag == null)
				{
					return null; //There is no way to respond to this tag
				}

				//Since the response won't have a target, put the speaker in alsoPlaying
				response.AlsoPlaying = speaker.FolderName;
				response.AlsoPlayingStage = speakerStageRange;
				response.AlsoPlayingTimeInStage = TimeInStage;
				response.AlsoPlayingHand = HasHand;
				response.AlsoPlayingNotSaidMarker = NotSaidMarker;
				response.AlsoPlayingSaidMarker = SaidMarker;

				//Match speaker's target conditions with the responder
				response.TimeInStage = TargetTimeInStage;
				response.HasHand = TargetHand;
				response.SaidMarker = TargetSaidMarker;
				response.NotSaidMarker = TargetNotSaidMarker;

			}
			else if (AlsoPlaying == responder.FolderName)
			{
				//Responder is also playing
				if (!string.IsNullOrEmpty(AlsoPlayingStage))
				{
					ToRange(AlsoPlayingStage, out minStage, out maxStage);
					for (int stage = minStage; stage <= maxStage; stage++)
					{
						response.Stages.Add(stage);
					}
				}
				else
				{
					//If not limited to a stage, use all stages by default
					for (int i = 0; i < responder.Layers + Clothing.ExtraStages; i++)
					{
						response.Stages.Add(i);
					}
				}

				Trigger trigger = TriggerDatabase.GetTrigger(Tag);
				if (trigger.HasTarget)
				{
					//someone else is the target, so use the same tag as the speaker
					response.Tag = Tag;
					response.Target = Target;
					response.TargetStage = TargetStage;
					response.TargetTimeInStage = TargetTimeInStage;
					response.TargetHand = TargetHand;
					response.Filter = Filter;
					response.TargetSaidMarker = TargetSaidMarker;
					response.TargetNotSaidMarker = TargetNotSaidMarker;

					//Speaker goes into AlsoPlaying
					response.AlsoPlaying = speaker.FolderName;
					response.AlsoPlayingHand = HasHand;
					response.AlsoPlayingNotSaidMarker = NotSaidMarker;
					response.AlsoPlayingSaidMarker = SaidMarker;
					response.AlsoPlayingStage = speakerStageRange;
					response.AlsoPlayingTimeInStage = TimeInStage;
				}
				else
				{
					//speaker is the target, so use the opposite tag
					response.Tag = TriggerDatabase.GetOppositeTag(Tag, speaker, min);
					response.Target = speaker.FolderName;
					response.TargetStage = speakerStageRange;
					response.TargetTimeInStage = TimeInStage;
					response.TargetHand = HasHand;
					response.TargetSaidMarker = SaidMarker;
					response.TargetNotSaidMarker = NotSaidMarker;
				}

				//Match speaker's also playing conditions with the responder
				response.TimeInStage = AlsoPlayingTimeInStage;
				response.HasHand = AlsoPlayingHand;
				response.SaidMarker = AlsoPlayingSaidMarker;
				response.NotSaidMarker = AlsoPlayingNotSaidMarker;
			}

			//Misc conditions are always the same
			response.Conditions.AddRange(Conditions);
			response.ConsecutiveLosses = ConsecutiveLosses;
			response.TotalFemales = TotalFemales;
			response.TotalMales = TotalMales;
			response.TotalPlaying = TotalPlaying;
			response.TotalExposed = TotalExposed;
			response.TotalNaked = TotalNaked;
			response.TotalFinishing = TotalFinishing;
			response.TotalFinished = TotalFinished;
			response.TotalRounds = TotalRounds;

			return response;
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

		[XmlIgnore]
		public string StatusType
		{
			get
			{
				if (!String.IsNullOrEmpty(Status) && Status[0] == '!')
				{
					return Status.Substring(1);
				}
				else
				{
					return Status;
				}
			}
			set
			{
				if (!String.IsNullOrEmpty(Status) && Status[0] == '!')
				{
					Status = "!" + value;
				}
				else
				{
					Status = value;
				}
			}
		}

		[XmlIgnore]
		public bool NegateStatus {
			get
			{
				return (!String.IsNullOrEmpty(Status) && Status[0] == '!');
			}
			set
			{
				Status = StatusType != null ? (value ? "!" : "") + StatusType : null;
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
					str += " " + Status.Replace("!", "not ");
				}
				if (Gender != null)
				{
					str += " " + Gender + "s";
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

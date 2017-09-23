using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Case : IComparable<Case>
	{
		private static long s_globalId;

		[XmlAttribute("alsoPlaying")]
		public string AlsoPlaying;

		[XmlAttribute("alsoPlayingHand")]
		public string AlsoPlayingHand;

		[XmlAttribute("alsoPlayingStage")]
		public string AlsoPlayingStage;

		[XmlAttribute("oppHand")]
		public string TargetHand;

		[XmlAttribute("hasHand")]
		public string HasHand;

		[XmlAttribute("filter")]
		public string Filter;

		[XmlAttribute("tag")]
		public string Tag;

		[XmlAttribute("target")]
		public string Target;

		[XmlAttribute("targetStage")]
		public string TargetStage;

		[XmlAttribute("totalMales")]
		public string TotalMales;

		[XmlAttribute("totalFemales")]
		public string TotalFemales;

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
			if (HasFilters)
			{
				if (!string.IsNullOrEmpty(Target))
				{
					result += string.Format(" (target={0})", Target);
				}
				else if (!string.IsNullOrEmpty(TargetStage))
				{
					result += string.Format(" (target stage={0})", TargetStage);
				}
				else if (!string.IsNullOrEmpty(TargetHand))
				{
					result += string.Format(" (target hand={0})", TargetHand);
				}
				else if (!string.IsNullOrEmpty(Filter))
				{
					result += string.Format(" (filter={0})", Filter);
				}
				else if (!string.IsNullOrEmpty(AlsoPlaying))
				{
					result += string.Format(" (playing w/{0})", AlsoPlaying);
				}
				else if (!string.IsNullOrEmpty(HasHand))
				{
					result += string.Format(" (hand={0})", HasHand);
				}
				else if (Conditions.Count > 0)
				{
					result += string.Format(" ({0})", string.Join(",", Conditions));
				}
				else if (!string.IsNullOrEmpty(TotalFemales))
				{
					result += string.Format(" ({0} females)", TotalFemales);
				}
				else if (!string.IsNullOrEmpty(TotalMales))
				{
					result += string.Format(" ({0} males)", TotalMales);
				}
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
			copy.Tag = Tag;
			copy.Target = Target;
			copy.TargetHand = TargetHand;
			copy.TargetStage = TargetStage;
			copy.AlsoPlaying = AlsoPlaying;
			copy.AlsoPlayingHand = AlsoPlayingHand;
			copy.AlsoPlayingStage = AlsoPlayingStage;
			copy.Filter = Filter;
			copy.HasHand = HasHand;
			copy.TotalFemales = TotalFemales;
			copy.TotalMales = TotalMales;
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
			if (Target == "")
				Target = null;
			if (TargetHand == "")
				TargetHand = null;
			if (TargetStage == "")
				TargetStage = null;
			if (AlsoPlaying == "")
				AlsoPlaying = null;
			if (AlsoPlayingHand == "")
				AlsoPlayingHand = null;
			if (AlsoPlayingStage == "")
				AlsoPlayingStage = null;
			if (Filter == "")
				Filter = null;
			if (HasHand == "")
				HasHand = null;
			if (TotalFemales == "")
				TotalFemales = null;
			if (TotalMales == "")
				TotalMales = null;
		}

		/// <summary>
		/// Gets the priority for targeted dialogue used by the game
		/// </summary>
		/// <returns></returns>
		public int GetPriority()
		{
			int totalPriority = 0;
			if (!string.IsNullOrEmpty(Target))
				totalPriority += 300;

			if (!string.IsNullOrEmpty(Filter))
				totalPriority += 150;

			if (!string.IsNullOrEmpty(TargetStage))
				totalPriority += 80;

			if (!string.IsNullOrEmpty(TargetHand))
				totalPriority += 30;

			if (!string.IsNullOrEmpty(HasHand))
				totalPriority += 20;

			if (!string.IsNullOrEmpty(AlsoPlaying))
			{
				totalPriority += 100;

				if (!string.IsNullOrEmpty(AlsoPlayingStage))
					totalPriority += 40;
				if (!string.IsNullOrEmpty(AlsoPlayingHand))
					totalPriority += 5;
			}

			totalPriority += Conditions.Count * 10;

			if (!string.IsNullOrEmpty(TotalMales))
				totalPriority += 5;
			if (!string.IsNullOrEmpty(TotalFemales))
				totalPriority += 5;

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
				AlsoPlaying == other.AlsoPlaying &&
				AlsoPlayingHand == other.AlsoPlayingHand &&
				AlsoPlayingStage == other.AlsoPlayingStage &&
				HasHand == other.HasHand &&
				Filter == other.Filter &&
				TotalFemales == other.TotalFemales &&
				TotalMales == other.TotalMales;
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
			foreach (var condition in Conditions)
			{
				hash = (hash * 397) ^ condition.GetHashCode();
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
	}

	public class TargetCondition
	{
		[XmlAttribute("filter")]
		/// <summary>
		/// Tag to condition on
		/// </summary>
		public string Filter;

		[XmlAttribute("count")]
		/// <summary>
		/// Number of characters needing the filter tag
		/// </summary>
		public int Count;

		public TargetCondition()
		{
		}

		public TargetCondition(string filter, int count)
		{
			Filter = filter;
			Count = count;
		}

		public TargetCondition Copy()
		{
			TargetCondition copy = new TargetCondition(Filter, Count);
			return copy;
		}

		public override string ToString()
		{
			return string.Format("{0}={1}", Filter, Count);
		}
	}
}

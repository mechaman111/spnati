using Desktop;
using Desktop.CommonControls;
using System.Reflection;

namespace SPNATI_Character_Editor.Analyzers
{
	public abstract class CaseConditionPropertyAnalyzer : BooleanAnalyzer
	{
		public abstract string PropertyName { get; }

		public override string Key { get { return "CaseCondition" + PropertyName; } }
		public override string Name { get { return "Uses " + PropertyName; } }
		public override string FullName { get { return "Dialogue - Uses " + PropertyName; } }

		public override string ParentKey
		{
			get { return "Dialogue>Conditions"; }
		}

		public override bool GetValue(Character character)
		{
			MemberInfo mi = PropertyTypeInfo.GetMemberInfo(typeof(Case), PropertyName);
			
			foreach (Case theCase in character.Behavior.EnumerateSourceCases())
			{
				string value = mi.GetValue(theCase)?.ToString();
				if (!string.IsNullOrEmpty(value))
				{
					return true;
				}
			}
			
			return false;
		}
	}
	public class ConditionHasHandAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "HasHand"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Self"; } }
	}
	public class ConditionHiddenAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "Hidden"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Self"; } }
	}
	public class ConditionTargetStageAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetStage"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionTargetLayersAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetLayers"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionTargetStartingLayersAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetStartingLayers"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionTargetStatusAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetStatus"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionTargetHandAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetHand"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionAlsoAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AlsoPlaying"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Also Playing"; } }
	}
	public class ConditionAlsoStageAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AlsoPlayingStage"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Also Playing"; } }
	}
	public class ConditionAlsoHasHandAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AlsoPlayingHand"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Also Playing"; } }
	}
	public class ConditionTotalMalesAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TotalMales"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionTotalFemalesAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TotalFemales"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionTimeInStageAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TimeInStage"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Self"; } }
	}
	public class ConditionLossesAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "ConsecutiveLosses"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionTotalPlayingAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TotalPlaying"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionTotalExposedAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TotalExposed"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionTotalNakedAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TotalNaked"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionTotalFinishingAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TotalMasturbating"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionTotalFinishedAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TotalFinished"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Table"; } }
	}
	public class ConditionSaidMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "SaidMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Self"; } }
	}
	public class ConditionNotSaidMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "NotSaidMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Self"; } }
	}
	public class ConditionAlsoSaidMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AlsoPlayingSaidMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Also Playing"; } }
	}
	public class ConditionAlsoNotSaidMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AlsoPlayingNotSaidMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Also Playing"; } }
	}
	public class ConditionAlsoSayingMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AlsoPlayingSayingMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Also Playing"; } }
	}
	public class ConditionAlsoSayingAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AlsoPlayingSaying"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Also Playing"; } }
	}
	public class ConditionTargetSaidMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetSaidMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionTargetNotSaidMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetNotSaidMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionTargetSayingMarkerAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetSayingMarker"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionTargetSayingAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "TargetSaying"; } }
		public override string ParentKey { get { return "Dialogue>Conditions>Target"; } }
	}
	public class ConditionPriorityAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "CustomPriority"; } }
		public override string ParentKey { get { return "Dialogue>Conditions"; } }
	}
	public class ConditionAddTagsAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "AddCharacterTags"; } }
		public override string ParentKey { get { return "Dialogue>Conditions"; } }
	}
	public class ConditionRemoveTagsAnalyzer : CaseConditionPropertyAnalyzer
	{
		public override string PropertyName { get { return "RemoveCharacterTags"; } }
		public override string ParentKey { get { return "Dialogue>Conditions"; } }
	}
	public class CaseFilterAnalyzer : BooleanAnalyzer
	{
		public override string Key { get { return "Filters"; } }
		public override string Name { get { return "Uses Filters"; } }
		public override string FullName { get { return "Dialogue - Uses Filters"; } }

		public override string ParentKey
		{
			get { return "Dialogue>Conditions"; }
		}

		public override bool GetValue(Character character)
		{
			foreach (Case theCase in character.Behavior.EnumerateSourceCases())
			{
				if (theCase.HasFilters)
				{
					return true;
				}
			}
			return false;
		}
	}
	public class CaseVariableAnalyzer : BooleanAnalyzer
	{
		public override string Key { get { return "Variable"; } }
		public override string Name { get { return "Uses Variable Tests"; } }
		public override string FullName { get { return "Dialogue - Uses Variable Tests"; } }

		public override string ParentKey
		{
			get { return "Dialogue>Conditions"; }
		}

		public override bool GetValue(Character character)
		{
			foreach (Case theCase in character.Behavior.EnumerateSourceCases())
			{
				if (theCase.Expressions.Count > 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}

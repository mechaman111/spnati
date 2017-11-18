using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 21)]
	public class MarkerUsageBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Markers (Consumed)";
		}

		public override string GetTitle()
		{
			return "Markers Referenced by Other Characters (Top 30 Characters)";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			Dictionary<string, HashSet<string>> markers = new Dictionary<string, HashSet<string>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				var stages = c.Behavior.Stages;
				foreach (var stage in stages)
				{
					foreach (var stageCase in stage.Cases)
					{
						TrackMarker(markers, stageCase.Target, stageCase.TargetSaidMarker);
						TrackMarker(markers, stageCase.Target, stageCase.TargetNotSaidMarker);
						TrackMarker(markers, stageCase.AlsoPlaying, stageCase.AlsoPlayingNotSaidMarker);
						TrackMarker(markers, stageCase.AlsoPlaying, stageCase.AlsoPlayingSaidMarker);
					}
				}
			}
			foreach (var kvp in markers)
			{
				data.Add(new Tuple<Character, int>(CharacterDatabase.Get(kvp.Key), kvp.Value.Count));
			}
			return data;
		}

		private static void TrackMarker(Dictionary<string, HashSet<string>> markers, string target, string marker)
		{
			if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(marker))
				return;
			HashSet<string> set;
			if (!markers.TryGetValue(target, out set))
			{
				set = new HashSet<string>();
				markers[target] = set;
			}
			if (!set.Contains(marker))
			{
				//first time this character has used the marker
				set.Add(marker);
			}
		}
	}
}

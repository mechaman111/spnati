using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 4)]
	public class TargetedCharacterBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Targets (Outgoing)";
		}

		public override string GetTitle()
		{
			return "Unique Players Targeted By This Character";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				HashSet<string> targets = new HashSet<string>();
				foreach (var stageCase in c.Behavior.EnumerateSourceCases())
				{
					foreach (string target in stageCase.GetTargets())
					{
						targets.Add(target);
					}
				}
				int count = targets.Count;
				if (count > 0)
				{
					data.Add(new Tuple<Character, int>(c, count));
				}
			}
			return data;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 20)]
	public class MarkerBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Markers (Produced)";
		}

		public override string GetTitle()
		{
			return "Markers Available for Other Characters";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.Markers.Value.Values.Count(m => m.Scope == MarkerScope.Public);
				if (count > 0)
				{
					data.Add(new Tuple<Character, int>(c, count));
				}
			}
			return data;
		}
	}
}

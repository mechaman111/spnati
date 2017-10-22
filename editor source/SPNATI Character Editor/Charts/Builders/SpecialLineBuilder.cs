using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 1)]
	public class SpecialLineBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Lines (Special)";
		}

		public override string GetTitle()
		{
			return "Game State Dependent Line Count (Top 30 Characters)";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.GetSpecialLineCount();
				if (count > 0)
				{
					data.Add(new Tuple<Character, int>(c, count));
				}
			}
			return data;
		}
	}
}

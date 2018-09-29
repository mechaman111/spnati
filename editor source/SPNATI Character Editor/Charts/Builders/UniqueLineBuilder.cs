using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 0)]
	public class UniqueLineBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Lines (Total)";
		}

		public override string GetTitle()
		{
			return "Total Unique Line Count";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.GetUniqueLineCount();
				if (count > 0)
				{
					data.Add(new Tuple<Character, int>(c, count));
				}
			}
			return data;
		}
	}
}

using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 0)]
	public class GenericLineBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Lines (Generic)";
		}

		public override string GetTitle()
		{
			return "Generic Line Count (Top 30 Characters)";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.GetGenericLineCount();
				if (count > 0)
				{
					data.Add(new Tuple<Character, int>(c, count));
				}
			}
			return data;
		}
	}
}

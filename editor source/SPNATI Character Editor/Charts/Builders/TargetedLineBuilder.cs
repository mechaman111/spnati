using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 1)]
	public class TargetedLineBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Lines (Targeted)";
		}

		public override string GetTitle()
		{
			return "Targeted Line Count";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.GetTargetedLineCount();
				if (count > 0)
				{
					data.Add(new Tuple<Character, int>(c, count));
				}
			}
			return data;
		}
	}

	[Chart(ChartType.Bar, 1)]
	public class FilteredLineBuilder : GenderedBuilder
	{
		public override string GetLabel()
		{
			return "Lines (Filtered)";
		}

		public override string GetTitle()
		{
			return "Filtered (by Tag) Line Count";
		}

		protected override List<Tuple<Character, int>> GetData()
		{
			var data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.GetFilterLineCount();
				if (count > 0)
				{
					data.Add(new Tuple<Character, int>(c, count));
				}
			}
			return data;
		}
	}
}

using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.StackedBar, 2)]
	public class SpokenToBuilder : IChartDataBuilder
	{
		private List<Tuple<Character, int, int>> _data;
		private const string ViewAll = "All";
		private const string ViewDirect = "Direct";
		private const string ViewTags = "Tags";

		public string GetLabel()
		{
			return "Lines (Spoken to)";
		}

		public void GenerateData()
		{
			List<Tuple<Character, int, int>> counts = new List<Tuple<Character, int, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = 0;
				int tagCount = 0;
				foreach (Character c2 in CharacterDatabase.Characters)
				{
					int byTag;
					count += c2.GetCharacterUsage(c, out byTag);
					tagCount += byTag;
				}
				if (count + tagCount > 0)
				{
					counts.Add(new Tuple<Character, int, int>(c, count, tagCount));
				}
			}

			_data = counts;
		}

		public List<List<ChartData>> GetSeries(string view)
		{
			List<List<ChartData>> data = new List<List<ChartData>>();
			List<ChartData> series0 = new List<ChartData>();
			List<ChartData> series1 = new List<ChartData>();

			data.Add(series0);
			data.Add(series1);

			switch (view)
			{
				case ViewAll:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item2 + t2.Item3).CompareTo(t1.Item2 + t1.Item3);
					});
					break;
				case ViewDirect:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item2).CompareTo(t1.Item2);
					});
					break;
				case ViewTags:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item3).CompareTo(t1.Item3);
					});
					break;
			}

			for (int i = 0; i < _data.Count; i++)
			{
				var item = _data[i];
				switch (view)
				{
					case ViewAll:
						series0.Add(new ChartData(item.Item1.Label, item.Item2));
						series1.Add(new ChartData(item.Item1.Label, item.Item3));
						break;
					case ViewDirect:
						series0.Add(new ChartData(item.Item1.Label, item.Item2));
						break;
					case ViewTags:
						series1.Add(new ChartData(item.Item1.Label, item.Item3));
						break;
				}
			}

			return data;
		}

		public string GetTitle()
		{
			return "Unique Lines Spoken To a Character";
		}

		public string[] GetViews()
		{
			return new string[] { ViewAll, ViewDirect, ViewTags };
		}
	}
}

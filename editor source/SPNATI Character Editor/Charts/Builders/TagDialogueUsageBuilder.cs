using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 6)]
	public class TagDialogueUsageBuilder : IChartDataBuilder
	{
		private const string ViewAll = "Both Genders";
		private const string ViewFemale = "Female";
		private const string ViewMale = "Male";

		private List<Tuple<string, int>> _data;

		public string GetLabel()
		{
			return "Tags (Usage in Dialogue)";
		}

		public string GetTitle()
		{
			return "Number of Lines Targeting a Tag";
		}

		public void GenerateData()
		{
			//Lazy approach of just regenerating the whole data set with each series
		}

		public List<List<ChartData>> GetSeries(string view)
		{
			_data = new List<Tuple<string, int>>();
			string targetGender = view == ViewAll ? "" : view.ToLower();
			foreach (Tag tag in TagDatabase.Tags)
			{
				int count = 0;
				foreach (Character c in CharacterDatabase.Characters)
				{
					count += c.GetTagUsage(tag.Value, targetGender);
				}
				if (count > 0)
				{
					_data.Add(new Tuple<string, int>(tag.Value, count));
				}
			}
			_data.Sort((t1, t2) =>
			{
				int compare = t2.Item2.CompareTo(t1.Item2);
				if (compare == 0)
				{
					compare = t1.Item1.CompareTo(t2.Item1);
				}
				return compare;
			});

			List<List<ChartData>> series = new List<List<Builders.ChartData>>();
			List<ChartData> chartData = new List<ChartData>();
			series.Add(chartData);

			for (int i = 0; i < _data.Count; i++)
			{
				var item = _data[i];
				chartData.Add(new ChartData(item.Item1, item.Item2));
			}

			return series;
		}

		public string[] GetViews()
		{
			return new string[] { ViewAll, ViewFemale, ViewMale };
		}
	}
}

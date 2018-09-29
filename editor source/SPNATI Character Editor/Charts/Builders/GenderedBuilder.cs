using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	public abstract class GenderedBuilder : IChartDataBuilder
	{
		private static readonly string ViewAll = "Both Genders";
		private static readonly string ViewFemale = "Female";
		private static readonly string ViewMale = "Male";

		private List<Tuple<Character, int>> _data;

		public abstract string GetLabel();
		public abstract string GetTitle();

		public void GenerateData()
		{
			_data = GetData();
			_data.Sort((t1, t2) =>
			{
				return t2.Item2.CompareTo(t1.Item2);
			});
		}
		protected abstract List<Tuple<Character, int>> GetData();

		public List<List<ChartData>> GetSeries(string view)
		{
			List<List<ChartData>> series = new List<List<Builders.ChartData>>();
			List<ChartData> chartData = new List<ChartData>();
			series.Add(chartData);

			List<Tuple<Character, int>> filteredData = new List<Tuple<Character, int>>();
			if (view == ViewAll)
			{
				filteredData = _data;
			}
			else
			{
				view = view.ToLower();
				foreach (var item in _data)
				{
					if (item.Item1.Gender == view)
					{
						filteredData.Add(item);
					}
				}
			}

			for (int i = 0; i < filteredData.Count; i++)
			{
				var item = filteredData[i];
				chartData.Add(new ChartData(item.Item1.Label, item.Item2));
			}

			return series;
		}

		public string[] GetViews()
		{
			return new string[] { ViewAll, ViewFemale, ViewMale };
		}
	}
}

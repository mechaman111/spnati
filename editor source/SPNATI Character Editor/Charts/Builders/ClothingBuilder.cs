using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	/// <summary>
	/// Charts custom Pose Maker poses
	/// </summary>
	[Chart(ChartType.Bar, 30)]
	public class ClothingBuilder : IChartDataBuilder
	{
		private List<Tuple<string, int>> _data;

		public string GetLabel()
		{
			return "Clothing Frequency";
		}

		public void GenerateData()
		{
			_data = new List<Tuple<string, int>>();
			foreach (string item in ClothingDatabase.Items.Values)
			{
				int count = ClothingDatabase.Items.GetCount(item);
				_data.Add(new Tuple<string, int>(item, count));
			}
			_data.Sort((d1, d2) =>
			{
				int compare = d2.Item2.CompareTo(d1.Item2);
				if (compare == 0)
				{
					compare = d1.Item1.CompareTo(d2.Item1);
				}
				return compare;
			});
		}

		public List<List<ChartData>> GetSeries(string view)
		{
			List<List<ChartData>> data = new List<List<ChartData>>();
			List<ChartData> series0 = new List<ChartData>();

			data.Add(series0);
			for (int i = 0; i < _data.Count; i++)
			{
				var item = _data[i];
				series0.Add(new ChartData(item.Item1, item.Item2));
			}

			return data;
		}

		public string GetTitle()
		{
			return "Clothing Frequency";
		}

		public string[] GetViews()
		{
			return new string[] { "All" };
		}
	}
}

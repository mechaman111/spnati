using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	/// <summary>
	/// Charts collectible counts
	/// </summary>
	[Chart(ChartType.Bar, 13)]
	public class CollectibleBuilder : IChartDataBuilder
	{
		private List<Tuple<Character, int>> _data;

		public string GetLabel()
		{
			return "Collectibles";
		}

		public void GenerateData()
		{
			_data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				if (c.FolderName == "human" || c.Collectibles.Count == 0) { continue; }
				_data.Add(new Tuple<Character, int>(c, c.Collectibles.Count));
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
				series0.Add(new ChartData(item.Item1.Label, item.Item2));
			}

			return data;
		}

		public string GetTitle()
		{
			return "Collectibles Per Character";
		}

		public string[] GetViews()
		{
			return new string[] { "All" };
		}
	}
}

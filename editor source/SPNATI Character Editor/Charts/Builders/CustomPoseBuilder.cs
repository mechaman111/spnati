using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	/// <summary>
	/// Charts custom Pose Maker poses
	/// </summary>
	[Chart(ChartType.Bar, 13)]
	public class CustomPoseBuilder : IChartDataBuilder
	{
		private List<Tuple<Character, int>> _data;

		public string GetLabel()
		{
			return "Custom Poses";
		}

		public void GenerateData()
		{
			_data = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				if (c.FolderName == "human") { continue; }
				_data.Add(new Tuple<Character, int>(c, c.CustomPoses.Count));
			}
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
			return "Custom Pose Usage";
		}

		public string[] GetViews()
		{
			return new string[] { "All" };
		}
	}
}

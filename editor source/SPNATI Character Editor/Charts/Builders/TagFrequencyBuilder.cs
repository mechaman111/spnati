using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts.Builders
{
	[Chart(ChartType.Bar, 5)]
	public class TagFrequencyBuilder : IChartDataBuilder
	{
		private const string ViewAll = "Both Genders";
		private const string ViewFemale = "Female";
		private const string ViewMale = "Male";

		public string GetLabel()
		{
			return "Tags";
		}

		public string GetTitle()
		{
			return "Most Common Tags (used by more than 1 character)";
		}

		public void GenerateData()
		{
			//Data will be populated in GetSeries
		}

		public List<List<ChartData>> GetSeries(string view)
		{
			List<Tag> tags = new List<Tag>();
			foreach (var tag in TagDatabase.Tags)
			{
				if (view == ViewAll)
				{
					if (tag.Count > 1)
						tags.Add(tag);
				}
				else
				{
					string gender = view.ToLower();
					int count = 0;
					foreach (Character c in CharacterDatabase.Characters)
					{
						if (c.Gender == gender && c.Tags.Find(t => t.Tag == tag.Value) != null)
						{
							count++;
						}
					}
					if (count > 1)
					{
						Tag t = new Tag() { Value = tag.Value, Count = count };
						tags.Add(t);
					}
				}
			}
			tags.Sort((t1, t2) =>
			{
				return t2.Count.CompareTo(t1.Count);
			});

			List<List<ChartData>> series = new List<List<Builders.ChartData>>();
			List<ChartData> chartData = new List<ChartData>();
			series.Add(chartData);

			for (int i = 0; i < tags.Count; i++)
			{
				var item = tags[i];
				chartData.Add(new ChartData(item.Value, item.Count));
			}

			return series;
		}

		public string[] GetViews()
		{
			return new string[] { ViewAll, ViewFemale, ViewMale };
		}
	}
}

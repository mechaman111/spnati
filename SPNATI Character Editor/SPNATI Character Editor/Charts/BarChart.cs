using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Charts
{
	/// <summary>
	/// Generic bar graph
	/// </summary>
	[Chart("Lines (Generic)", 0, "GenerateUniqueLines")]
	[Chart("Lines (Targeted)", 1, "GenerateTargetedLines")]
	[Chart("Tags", 5, "GenerateTags")]
	[Chart("Tags (Usage in Dialogue)", 6, "GenerateTagTargets")]
	public partial class BarChart : UserControl
	{
		public BarChart()
		{
			InitializeComponent();
		}

		public void GenerateTargetedLines()
		{
			chart.Titles[0].Text = "Targeted Line Count (Top 30 Characters)";
			List<Tuple<Character, int>> counts = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.GetTargetedLineCount();
				if (count > 0)
				{
					counts.Add(new Tuple<Character, int>(c, count));
				}
			}
			counts.Sort((t1, t2) =>
			{
				return t2.Item2.CompareTo(t1.Item2);
			});

			var points = chart.Series[0].Points;
			for (int i = 0; i < 30 && i < counts.Count; i++)
			{
				var item = counts[i];
				points.AddXY(item.Item1.Label, item.Item2);
			}
		}

		public void GenerateUniqueLines()
		{
			chart.Titles[0].Text = "Generic Line Count (Top 30 Characters)";
			List<Tuple<Character, int>> counts = new List<Tuple<Character, int>>();
			foreach (Character c in CharacterDatabase.Characters)
			{
				int count = c.GetGenericLineCount();
				if (count > 0)
				{
					counts.Add(new Tuple<Character, int>(c, count));
				}
			}
			counts.Sort((t1, t2) =>
			{
				return t2.Item2.CompareTo(t1.Item2);
			});

			var points = chart.Series[0].Points;
			for (int i = 0; i < 30 && i < counts.Count; i++)
			{
				var item = counts[i];
				points.AddXY(item.Item1.Label, item.Item2);
			}
		}

		public void GenerateTags()
		{
			chart.Titles[0].Text = "Most Common Tags (used by more than 1 character)";
			List<Tag> tags = new List<Tag>();
			foreach (var tag in TagDatabase.Tags)
			{
				if (tag.Count > 1)
					tags.Add(tag);
			}
			tags.Sort((t1, t2) =>
			{
				return t2.Count.CompareTo(t1.Count);
			});

			var points = chart.Series[0].Points;
			for (int i = 0; i < tags.Count; i++)
			{
				var item = tags[i];
				points.AddXY(item.Value, item.Count);
			}
		}

		public void GenerateTagTargets()
		{
			chart.Titles[0].Text = "Number of Lines Targeting a Tag";
			List<Tuple<string, int>> counts = new List<Tuple<string, int>>();
			foreach (Tag tag in TagDatabase.Tags)
			{
				int count = 0;
				foreach (Character c in CharacterDatabase.Characters)
				{
					count += c.GetTagUsage(tag.Value);
				}
				if (count > 0)
				{
					counts.Add(new Tuple<string, int>(tag.Value, count));
				}
			}
			counts.Sort((t1, t2) =>
			{
				return t2.Item2.CompareTo(t1.Item2);
			});

			var points = chart.Series[0].Points;
			for (int i = 0; i < counts.Count; i++)
			{
				var item = counts[i];
				points.AddXY(item.Item1, item.Item2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Charts
{
	/// <summary>
	/// Stacked bar graph for lines spoken to a character
	/// </summary>
	[Chart("Lines (Spoken to)", 2, "GenerateSpokenToLines")]
	public partial class SpokenToChart : UserControl
	{
		private List<Tuple<Character, int, int>> _data;
		private GraphView _view = GraphView.All;

		public SpokenToChart()
		{
			InitializeComponent();
		}

		public void GenerateSpokenToLines()
		{
			chart.Titles[0].Text = "Unique Lines Spoken To a Character (Top 30 Characters)";
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
			UpdateGraph();
		}

		private void UpdateGraph()
		{
			switch (_view)
			{
				case GraphView.All:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item2 + t2.Item3).CompareTo(t1.Item2 + t1.Item3);
					});
					break;
				case GraphView.Direct:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item2).CompareTo(t1.Item2);
					});
					break;
				case GraphView.Tags:
					_data.Sort((t1, t2) =>
					{
						return (t2.Item3).CompareTo(t1.Item3);
					});
					break;
			}
			

			var points = chart.Series[0].Points;
			var points2 = chart.Series[1].Points;
			points.Clear();
			points2.Clear();
			for (int i = 0; i < 30 && i < _data.Count; i++)
			{
				var item = _data[i];
				switch (_view)
				{
					case GraphView.All:
						points.AddXY(item.Item1.Label, item.Item2);
						points2.AddXY(item.Item1.Label, item.Item3);
						break;
					case GraphView.Direct:
						points.AddXY(item.Item1.Label, item.Item2);
						break;
					case GraphView.Tags:
						points2.AddXY(item.Item1.Label, item.Item3);
						break;
				}
			}
		}

		private void ChangeView(object sender, EventArgs e)
		{
			if (radAll.Checked)
				_view = GraphView.All;
			else if (radDirect.Checked)
				_view = GraphView.Direct;
			else if (radTags.Checked)
				_view = GraphView.Tags;
			UpdateGraph();

		}

		private enum GraphView
		{
			All,
			Direct,
			Tags
		}
	}
}

using SPNATI_Character_Editor.Charts.Builders;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Charts
{
	[ChartControl(ChartType.StackedBar)]
	/// <summary>
	/// Stacked bar graph for lines spoken to a character
	/// </summary>
	public partial class StackedBarChart : SkinnedChart, IChartControl
	{
		public StackedBarChart()
		{
			InitializeComponent();
			AddChart(chart);
		}

		public void SetTitle(string title)
		{
			chart.Titles[0].Text = title;
		}

		public void SetData(IChartDataBuilder builder, string view)
		{
			foreach (var s in chart.Series)
			{
				s.Points.Clear();
			}
			List<List<ChartData>> series = builder.GetSeries(view);
			for (int i = 0; i < chart.Series.Count && i < series.Count; i++)
			{
				var points = chart.Series[i].Points;
				var dataPoints = series[i];
				foreach (var point in dataPoints)
				{
					points.AddXY(point.X, point.Y);
				}
			}
			chart.ChartAreas[0].Axes[0].ScaleView.Zoom(0, 29.5);
		}
	}
}

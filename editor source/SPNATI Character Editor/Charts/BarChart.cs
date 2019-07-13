using SPNATI_Character_Editor.Charts.Builders;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Charts
{
	[ChartControl(ChartType.Bar)]
	/// <summary>
	/// Generic bar graph
	/// </summary>
	public partial class BarChart : SkinnedChart, IChartControl
	{
		public BarChart()
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
			List<List<ChartData>> series = builder.GetSeries(view);
			for (int i = 0; i < chart.Series.Count && i < series.Count; i++)
			{
				var points = chart.Series[i].Points;
				var dataPoints = series[i];

				points.Clear();
				foreach (var point in dataPoints)
				{
					points.AddXY(point.X, Math.Round(point.Y, 2));
				}
			}
			chart.ChartAreas[0].Axes[0].ScaleView.Zoom(0, 29.5);
		}
	}
}

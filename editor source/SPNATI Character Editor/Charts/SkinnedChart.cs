using Desktop.Skinning;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SPNATI_Character_Editor.Charts
{
	public class SkinnedChart : UserControl, ISkinControl
	{
		private List<Chart> _charts = new List<Chart>();

		public void AddChart(Chart chart)
		{
			_charts.Add(chart);
			ApplySkin(SkinManager.Instance.CurrentSkin, chart);
		}

		public void OnUpdateSkin(Skin skin)
		{
			foreach (Chart chart in _charts)
			{
				ApplySkin(skin, chart);
			}
		}

		private void ApplySkin(Skin skin, Chart chart)
		{
			chart.BackColor = skin.Background.Normal;
			chart.ForeColor = skin.Background.ForeColor;
			foreach (ChartArea area in chart.ChartAreas)
			{
				area.BackColor = skin.FieldBackColor;
				foreach (Axis axis in area.Axes)
				{
					axis.TitleForeColor = skin.Background.ForeColor;
					axis.LabelStyle.ForeColor = skin.Background.ForeColor;
				}
			}
			foreach (Legend legend in chart.Legends)
			{
				legend.BackColor = skin.Background.Normal;
				legend.ForeColor = skin.Background.ForeColor;
			}
			foreach (Title title in chart.Titles)
			{
				title.BackColor = skin.Background.Normal;
				title.ForeColor = skin.Background.ForeColor;
			}
			foreach (Series series in chart.Series)
			{
				series.LabelForeColor = skin.Background.ForeColor;
			}
		}
	}
}

using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 800, DelayRun = true, Caption = "Pose Usage")]
	public partial class PoseUsageGraph : Activity
	{
		private Character _character;

		public PoseUsageGraph()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
		}

		public override string Caption
		{
			get { return "Pose Usage"; }
		}

		protected override void OnActivate()
		{
			RefreshGraph();
		}

		private void RefreshGraph()
		{
			HashSet<int> checkedStages = new HashSet<int>();
			foreach (Control ctl in flowStages.Controls)
			{
				CheckBox box = ctl as CheckBox;
				if (box.Checked)
				{
					int stage = (int)ctl.Tag;
					checkedStages.Add(stage);
				}
			}

			flowStages.Controls.Clear();
			graph.Series.Clear();

			//pose name, stage, count
			DualKeyDictionary<string, int, int> data = new DualKeyDictionary<string, int, int>();

			for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
			{
				string stageName = _character.LayerToStageName(i).DisplayName;
				Series series = graph.Series.Add(stageName);
				series.ChartType = SeriesChartType.StackedColumn;

				CheckBox box = new CheckBox();
				flowStages.Controls.Add(box);
				box.Tag = i;
				box.Text = stageName;
				box.Checked = (checkedStages.Count == 0 || checkedStages.Contains(i));
				box.CheckedChanged += Box_CheckedChanged;
			}

			foreach (Case workingCase in _character.Behavior.GetWorkingCases())
			{
				foreach (DialogueLine line in workingCase.Lines)
				{
					string image = line.Image;
					if (string.IsNullOrEmpty(image)) { continue; }
					for (int stage = 0; stage < _character.Layers + Clothing.ExtraStages; stage++)
					{
						int count = data.Get(image, stage);
						if (workingCase.Stages.Contains(stage))
						{
							count++;
						}
						data.Set(image, stage, count);
					}
				}
			}
			List<Tuple<string, int, int>> results = new List<Tuple<string, int, int>>();
			foreach (KeyValuePair<string, Dictionary<int, int>> kvp1 in data)
			{
				foreach (KeyValuePair<int, int> kvp2 in kvp1.Value)
				{
					results.Add(new Tuple<string, int, int>(kvp1.Key, kvp2.Key, kvp2.Value));
				}
			}
			results.Sort((a, b) =>
			{
				return a.Item1.CompareTo(b.Item1);
			});
			foreach (Tuple<string, int, int> result in results)
			{
				Series series = graph.Series[result.Item2];
				DataPointCollection points = series.Points;
				DataPoint pt = points[points.AddXY(result.Item1, result.Item3)];
				pt.ToolTip = result.Item1;
			}
		}

		private void Box_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox box = sender as CheckBox;
			int stage = (int)box.Tag;
			graph.Series[stage].Enabled = box.Checked;
		}

		protected override void OnSkinChanged(Skin skin)
		{
			base.OnSkinChanged(skin);
			graph.BackColor = skin.Background.Normal;
			graph.ForeColor = skin.Background.ForeColor;
			foreach (ChartArea area in graph.ChartAreas)
			{
				area.BackColor = skin.FieldBackColor;
				foreach (Axis axis in area.Axes)
				{
					axis.TitleForeColor = skin.Background.ForeColor;
					axis.LabelStyle.ForeColor = skin.Background.ForeColor;
				}
			}
			foreach (Legend legend in graph.Legends)
			{
				legend.BackColor = skin.Background.Normal;
				legend.ForeColor = skin.Background.ForeColor;
			}
			foreach (Title title in graph.Titles)
			{
				title.BackColor = skin.Background.Normal;
				title.ForeColor = skin.Background.ForeColor;
			}
			foreach (Series series in graph.Series)
			{
				series.LabelForeColor = skin.Background.ForeColor;
			}
		}
	}
}

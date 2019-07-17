using Desktop;
using SPNATI_Character_Editor.Charts;
using SPNATI_Character_Editor.Charts.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Desktop.Skinning;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(ChartRecord), 0)]
	public partial class ChartHost : Activity
	{
		private Dictionary<ChartType, Type> _chartControlTypes = new Dictionary<ChartType, Type>();
		private HashSet<ChartItem> _loadedCharts = new HashSet<ChartItem>();
		private Control _currentGraph;
		private IChartControl _currentChartControl;
		private IChartDataBuilder _currentBuilder;
		private string[] _availableViews;
		private string _currentView;

		public ChartHost()
		{
			InitializeComponent();
		}

		protected override void OnInitialize()
		{
			FindCharts();
		}

		/// <summary>
		/// Loads the chart list by scanning through the assembly
		/// </summary>
		private void FindCharts()
		{
			List<ChartItem> items = new List<ChartItem>();
			Assembly assembly = Assembly.GetExecutingAssembly();
			foreach (var type in assembly.GetTypes())
			{
				ChartControlAttribute controlAttr = type.GetCustomAttribute<ChartControlAttribute>();
				if (controlAttr != null)
				{
					_chartControlTypes[controlAttr.ChartType] = type;
				}

				foreach (ChartAttribute chartAttr in type.GetCustomAttributes<ChartAttribute>())
				{
					var item = new ChartItem(chartAttr, type);
					if (item.ChartDataBuilder != null)
						items.Add(item);
				}
			}
			items.Sort();
			lstGraph.DataSource = items;
		}

		private void lstGraph_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChartItem item = lstGraph.SelectedItem as ChartItem;
			if (!_loadedCharts.Contains(item))
			{
				Type controlType;
				if (!_chartControlTypes.TryGetValue(item.ChartType, out controlType))
					return;
				Control ctl = Activator.CreateInstance(controlType) as Control;
				IChartControl chartControl = ctl as IChartControl;
				if (chartControl != null)
				{
					item.Control = ctl;
					ctl.Dock = DockStyle.Fill;
					chartContainer.Controls.Add(ctl);
					Cursor.Current = Cursors.WaitCursor;
					chartControl.SetTitle(item.ChartDataBuilder.GetTitle());
					item.ChartDataBuilder.GenerateData();
					Cursor.Current = Cursors.Default;
				}
				else return;
				_loadedCharts.Add(item);
			}
			Control graph = item.Control;
			if (_currentGraph != null)
			{
				_currentGraph.Hide();
			}
			_currentBuilder = item.ChartDataBuilder;
			_currentChartControl = graph as IChartControl;
			PopulateViews();
			ShowView();
			graph.Show();
			_currentGraph = graph;
		}

		private void PopulateViews()
		{
			_availableViews = _currentBuilder.GetViews();
			foreach (Control ctl in viewPanel.Controls)
			{
				RadioButton button = ctl as RadioButton;
				if (button != null)
					button.CheckedChanged -= ViewChanged;
			}
			_currentView = _availableViews[0];
			viewPanel.Controls.Clear();
			for (int i = 0; i < _availableViews.Length; i++)
			{
				RadioButton button = new SkinnedRadioButton();
				viewPanel.Controls.Add(button);
				button.Text = _availableViews[i];
				if (i == 0)
					button.Checked = true;
				button.CheckedChanged += ViewChanged;
			}
		}

		private void ViewChanged(object sender, EventArgs e)
		{
			RadioButton button = sender as RadioButton;
			if (button.Checked)
			{
				_currentView = button.Text;
				ShowView();
			}
		}

		private void ShowView()
		{
			if (_currentChartControl == null)
				return;
			Cursor.Current = Cursors.WaitCursor;
			_currentChartControl.SetData(_currentBuilder, _currentView);
			Cursor.Current = Cursors.Default;
		}

		private class ChartItem : IComparable<ChartItem>
		{
			public IChartDataBuilder ChartDataBuilder;
			/// <summary>
			/// Sort order
			/// </summary>
			public int Order;
			/// <summary>
			/// Type of chart
			/// </summary>
			public ChartType ChartType;
			/// <summary>
			/// Control, if it has been generated
			/// </summary>
			public Control Control;

			public ChartItem(ChartAttribute attribute, Type type)
			{
				ChartDataBuilder = Activator.CreateInstance(type) as IChartDataBuilder;
				Order = attribute.Order;
				ChartType = attribute.ChartType;
			}

			public override string ToString()
			{
				return ChartDataBuilder?.GetLabel() ?? "Unknown";
			}

			public int CompareTo(ChartItem other)
			{
				return Order.CompareTo(other.Order);
			}
		}

		protected override void OnSkinChanged(Skin skin)
		{
			base.OnSkinChanged(skin);
		}
	}

	public class ChartRecord : BasicRecord
	{
		public ChartRecord()
		{
			Name = "Charts";
			Key = "Charts";
		}
	}

	public class ChartProvider : BasicProvider<ChartRecord>
	{
	}
}

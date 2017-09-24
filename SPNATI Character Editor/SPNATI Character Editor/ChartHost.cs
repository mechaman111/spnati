using SPNATI_Character_Editor.Charts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class ChartHost : Form
	{
		private HashSet<ChartItem> _loadedCharts = new HashSet<ChartItem>();
		private Control _currentGraph;

		public ChartHost()
		{
			InitializeComponent();
		}

		private void ChartHost_Load(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;
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
				foreach (ChartAttribute chartAttr in type.GetCustomAttributes<ChartAttribute>())
				{
					items.Add(new ChartItem(chartAttr, type));
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
				Control ctl = Activator.CreateInstance(item.ChartType) as Control;
				if (ctl != null)
				{
					item.Control = ctl;
					ctl.Dock = DockStyle.Fill;
					splitContainer1.Panel2.Controls.Add(ctl);
					if (item.Generator != null)
					{
						Cursor.Current = Cursors.WaitCursor;
						item.Generator.Invoke(ctl, null);
						Cursor.Current = Cursors.Default;
					}
				}
				_loadedCharts.Add(item);
			}
			Control graph = item.Control;
			if (_currentGraph != null)
			{
				_currentGraph.Hide();
			}
			graph.Show();
			_currentGraph = graph;
		}

		private class ChartItem : IComparable<ChartItem>
		{
			/// <summary>
			/// Chart's label
			/// </summary>
			public string Label;
			/// <summary>
			/// Sort order
			/// </summary>
			public int Order;
			/// <summary>
			/// Function generator
			/// </summary>
			public MethodInfo Generator;
			/// <summary>
			/// Type of chart
			/// </summary>
			public Type ChartType;
			/// <summary>
			/// Control, if it has been generated
			/// </summary>
			public Control Control;

			public ChartItem(ChartAttribute attribute, Type chartType)
			{
				Label = attribute.Label;
				Order = attribute.Order;
				Generator = chartType.GetMethod(attribute.GenerationFunction, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				ChartType = chartType;
			}

			public override string ToString()
			{
				return Label;
			}

			public int CompareTo(ChartItem other)
			{
				return Order.CompareTo(other.Order);
			}
		}
	}
}

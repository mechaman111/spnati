namespace SPNATI_Character_Editor.Activities
{
	partial class PoseUsageGraph
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.flowStages = new System.Windows.Forms.FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
			this.SuspendLayout();
			// 
			// graph
			// 
			this.graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea1.AxisX.MajorGrid.Enabled = false;
			chartArea1.AxisX.ScaleView.Size = 8D;
			chartArea1.AxisY.MajorGrid.Interval = 0D;
			chartArea1.AxisY.MinorGrid.Interval = 5D;
			chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Silver;
			chartArea1.Name = "ChartArea1";
			this.graph.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.graph.Legends.Add(legend1);
			this.graph.Location = new System.Drawing.Point(3, 64);
			this.graph.Name = "graph";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
			series2.Legend = "Legend1";
			series2.Name = "Series2";
			series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
			this.graph.Series.Add(series1);
			this.graph.Series.Add(series2);
			this.graph.Size = new System.Drawing.Size(1015, 596);
			this.graph.TabIndex = 0;
			this.graph.Text = "chart1";
			// 
			// flowStages
			// 
			this.flowStages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowStages.Location = new System.Drawing.Point(3, 3);
			this.flowStages.Name = "flowStages";
			this.flowStages.Size = new System.Drawing.Size(1015, 55);
			this.flowStages.TabIndex = 1;
			// 
			// PoseUsageGraph
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.flowStages);
			this.Controls.Add(this.graph);
			this.Name = "PoseUsageGraph";
			this.Size = new System.Drawing.Size(1021, 660);
			((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart graph;
		private System.Windows.Forms.FlowLayoutPanel flowStages;
	}
}

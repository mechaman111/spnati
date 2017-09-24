namespace SPNATI_Character_Editor.Charts
{
	partial class SpokenToChart
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
			System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
			this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.label1 = new System.Windows.Forms.Label();
			this.radAll = new System.Windows.Forms.RadioButton();
			this.radDirect = new System.Windows.Forms.RadioButton();
			this.radTags = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
			this.SuspendLayout();
			// 
			// chart
			// 
			this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea1.AxisX.Interval = 1D;
			chartArea1.AxisX.IsReversed = true;
			chartArea1.AxisX.MajorGrid.Enabled = false;
			chartArea1.AxisX.MajorTickMark.Enabled = false;
			chartArea1.Name = "ChartArea1";
			this.chart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chart.Legends.Add(legend1);
			this.chart.Location = new System.Drawing.Point(0, 26);
			this.chart.Name = "chart";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
			series1.IsValueShownAsLabel = true;
			series1.Legend = "Legend1";
			series1.LegendText = "Targeted Directly";
			series1.Name = "Series1";
			series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
			series1.YValuesPerPoint = 2;
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
			series2.IsValueShownAsLabel = true;
			series2.Legend = "Legend1";
			series2.LegendText = "Targeted by Tag";
			series2.Name = "Series2";
			this.chart.Series.Add(series1);
			this.chart.Series.Add(series2);
			this.chart.Size = new System.Drawing.Size(621, 402);
			this.chart.TabIndex = 0;
			this.chart.Text = "chart1";
			title1.Name = "Title1";
			title1.Text = "Lines of Dialogue Targeted Towards Other Characters";
			this.chart.Titles.Add(title1);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "View:";
			// 
			// radAll
			// 
			this.radAll.AutoSize = true;
			this.radAll.Checked = true;
			this.radAll.Location = new System.Drawing.Point(42, 3);
			this.radAll.Name = "radAll";
			this.radAll.Size = new System.Drawing.Size(36, 17);
			this.radAll.TabIndex = 2;
			this.radAll.TabStop = true;
			this.radAll.Text = "All";
			this.radAll.UseVisualStyleBackColor = true;
			this.radAll.CheckedChanged += new System.EventHandler(this.ChangeView);
			// 
			// radDirect
			// 
			this.radDirect.AutoSize = true;
			this.radDirect.Location = new System.Drawing.Point(84, 3);
			this.radDirect.Name = "radDirect";
			this.radDirect.Size = new System.Drawing.Size(77, 17);
			this.radDirect.TabIndex = 3;
			this.radDirect.TabStop = true;
			this.radDirect.Text = "Only Direct";
			this.radDirect.UseVisualStyleBackColor = true;
			this.radDirect.CheckedChanged += new System.EventHandler(this.ChangeView);
			// 
			// radTags
			// 
			this.radTags.AutoSize = true;
			this.radTags.Location = new System.Drawing.Point(167, 3);
			this.radTags.Name = "radTags";
			this.radTags.Size = new System.Drawing.Size(73, 17);
			this.radTags.TabIndex = 4;
			this.radTags.TabStop = true;
			this.radTags.Text = "Only Tags";
			this.radTags.UseVisualStyleBackColor = true;
			this.radTags.CheckedChanged += new System.EventHandler(this.ChangeView);
			// 
			// SpokenToChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.radTags);
			this.Controls.Add(this.radDirect);
			this.Controls.Add(this.radAll);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chart);
			this.Name = "SpokenToChart";
			this.Size = new System.Drawing.Size(621, 428);
			((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radAll;
		private System.Windows.Forms.RadioButton radDirect;
		private System.Windows.Forms.RadioButton radTags;
	}
}

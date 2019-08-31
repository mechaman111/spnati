namespace SPNATI_Character_Editor.Activities
{
	partial class Dashboard
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.colLeft = new System.Windows.Forms.SplitContainer();
			this.grpChecklist = new Desktop.Skinning.SkinnedGroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grpRequirements = new Desktop.Skinning.SkinnedGroupBox();
			this.tableRequirements = new System.Windows.Forms.TableLayoutPanel();
			this.barSize = new Desktop.CommonControls.RadialGauge();
			this.barUnique = new Desktop.CommonControls.RadialGauge();
			this.barFilters = new Desktop.CommonControls.RadialGauge();
			this.barTargets = new Desktop.CommonControls.RadialGauge();
			this.barLines = new Desktop.CommonControls.RadialGauge();
			this.colRight = new System.Windows.Forms.SplitContainer();
			this.grpHistory = new Desktop.Skinning.SkinnedGroupBox();
			this.graphLines = new Desktop.CommonControls.LineGraph();
			this.grpPartners = new Desktop.Skinning.SkinnedGroupBox();
			this.cmdPreviousGraph = new Desktop.Skinning.SkinnedIcon();
			this.cmdNextGraph = new Desktop.Skinning.SkinnedIcon();
			this.lblLines = new Desktop.Skinning.SkinnedLabel();
			this.graphPartners = new Desktop.CommonControls.Graphs.StackedBarGraph();
			this.tooltipPartners = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colLeft)).BeginInit();
			this.colLeft.Panel1.SuspendLayout();
			this.colLeft.Panel2.SuspendLayout();
			this.colLeft.SuspendLayout();
			this.grpChecklist.SuspendLayout();
			this.grpRequirements.SuspendLayout();
			this.tableRequirements.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colRight)).BeginInit();
			this.colRight.Panel1.SuspendLayout();
			this.colRight.Panel2.SuspendLayout();
			this.colRight.SuspendLayout();
			this.grpHistory.SuspendLayout();
			this.grpPartners.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(6);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.colLeft);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.colRight);
			this.splitContainer1.Size = new System.Drawing.Size(817, 548);
			this.splitContainer1.SplitterDistance = 397;
			this.splitContainer1.TabIndex = 1;
			// 
			// colLeft
			// 
			this.colLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.colLeft.Location = new System.Drawing.Point(0, 0);
			this.colLeft.Name = "colLeft";
			this.colLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// colLeft.Panel1
			// 
			this.colLeft.Panel1.Controls.Add(this.grpChecklist);
			this.colLeft.Panel1.Padding = new System.Windows.Forms.Padding(6);
			// 
			// colLeft.Panel2
			// 
			this.colLeft.Panel2.Controls.Add(this.grpRequirements);
			this.colLeft.Panel2.Padding = new System.Windows.Forms.Padding(6);
			this.colLeft.Size = new System.Drawing.Size(397, 548);
			this.colLeft.SplitterDistance = 304;
			this.colLeft.TabIndex = 0;
			// 
			// grpChecklist
			// 
			this.grpChecklist.Controls.Add(this.label1);
			this.grpChecklist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpChecklist.Location = new System.Drawing.Point(6, 6);
			this.grpChecklist.Name = "grpChecklist";
			this.grpChecklist.Size = new System.Drawing.Size(385, 292);
			this.grpChecklist.TabIndex = 0;
			this.grpChecklist.TabStop = false;
			this.grpChecklist.Text = "Checklist";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// grpRequirements
			// 
			this.grpRequirements.Controls.Add(this.tableRequirements);
			this.grpRequirements.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpRequirements.Location = new System.Drawing.Point(6, 6);
			this.grpRequirements.Name = "grpRequirements";
			this.grpRequirements.Size = new System.Drawing.Size(385, 228);
			this.grpRequirements.TabIndex = 0;
			this.grpRequirements.TabStop = false;
			this.grpRequirements.Text = "Basic Requirements";
			// 
			// tableRequirements
			// 
			this.tableRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableRequirements.ColumnCount = 3;
			this.tableRequirements.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
			this.tableRequirements.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
			this.tableRequirements.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
			this.tableRequirements.Controls.Add(this.barSize, 1, 1);
			this.tableRequirements.Controls.Add(this.barUnique, 2, 0);
			this.tableRequirements.Controls.Add(this.barFilters, 0, 1);
			this.tableRequirements.Controls.Add(this.barTargets, 1, 0);
			this.tableRequirements.Controls.Add(this.barLines, 0, 0);
			this.tableRequirements.Location = new System.Drawing.Point(6, 25);
			this.tableRequirements.Name = "tableRequirements";
			this.tableRequirements.RowCount = 2;
			this.tableRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableRequirements.Size = new System.Drawing.Size(373, 197);
			this.tableRequirements.TabIndex = 1;
			// 
			// barSize
			// 
			this.barSize.CapacityMode = true;
			this.barSize.Caption = "File Space";
			this.barSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barSize.InvertCapacityColors = false;
			this.barSize.Location = new System.Drawing.Point(127, 101);
			this.barSize.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.barSize.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.barSize.Name = "barSize";
			this.barSize.ShowPercentage = false;
			this.barSize.Size = new System.Drawing.Size(118, 93);
			this.barSize.TabIndex = 4;
			this.barSize.Unit = "mb";
			this.barSize.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// barUnique
			// 
			this.barUnique.CapacityMode = true;
			this.barUnique.Caption = "Unique Targets";
			this.barUnique.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barUnique.InvertCapacityColors = true;
			this.barUnique.Location = new System.Drawing.Point(251, 3);
			this.barUnique.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.barUnique.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.barUnique.Name = "barUnique";
			this.barUnique.ShowPercentage = false;
			this.barUnique.Size = new System.Drawing.Size(119, 92);
			this.barUnique.TabIndex = 3;
			this.barUnique.Unit = "characters";
			this.barUnique.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// barFilters
			// 
			this.barFilters.CapacityMode = true;
			this.barFilters.Caption = "Filtered Lines";
			this.barFilters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barFilters.InvertCapacityColors = true;
			this.barFilters.Location = new System.Drawing.Point(3, 101);
			this.barFilters.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.barFilters.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.barFilters.Name = "barFilters";
			this.barFilters.ShowPercentage = false;
			this.barFilters.Size = new System.Drawing.Size(118, 93);
			this.barFilters.TabIndex = 2;
			this.barFilters.Unit = "lines";
			this.barFilters.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// barTargets
			// 
			this.barTargets.CapacityMode = true;
			this.barTargets.Caption = "Targeted Lines";
			this.barTargets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barTargets.InvertCapacityColors = true;
			this.barTargets.Location = new System.Drawing.Point(127, 3);
			this.barTargets.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.barTargets.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.barTargets.Name = "barTargets";
			this.barTargets.ShowPercentage = false;
			this.barTargets.Size = new System.Drawing.Size(118, 92);
			this.barTargets.TabIndex = 1;
			this.barTargets.Unit = "lines";
			this.barTargets.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// barLines
			// 
			this.barLines.CapacityMode = true;
			this.barLines.Caption = "Total Lines";
			this.barLines.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barLines.InvertCapacityColors = true;
			this.barLines.Location = new System.Drawing.Point(3, 3);
			this.barLines.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.barLines.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.barLines.Name = "barLines";
			this.barLines.ShowPercentage = false;
			this.barLines.Size = new System.Drawing.Size(118, 92);
			this.barLines.TabIndex = 0;
			this.barLines.Unit = "lines";
			this.barLines.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// colRight
			// 
			this.colRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.colRight.Location = new System.Drawing.Point(0, 0);
			this.colRight.Name = "colRight";
			this.colRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// colRight.Panel1
			// 
			this.colRight.Panel1.Controls.Add(this.grpHistory);
			this.colRight.Panel1.Padding = new System.Windows.Forms.Padding(6);
			// 
			// colRight.Panel2
			// 
			this.colRight.Panel2.Controls.Add(this.grpPartners);
			this.colRight.Panel2.Padding = new System.Windows.Forms.Padding(6);
			this.colRight.Size = new System.Drawing.Size(416, 548);
			this.colRight.SplitterDistance = 289;
			this.colRight.TabIndex = 0;
			// 
			// grpHistory
			// 
			this.grpHistory.Controls.Add(this.graphLines);
			this.grpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpHistory.Location = new System.Drawing.Point(6, 6);
			this.grpHistory.Name = "grpHistory";
			this.grpHistory.Size = new System.Drawing.Size(404, 277);
			this.grpHistory.TabIndex = 1;
			this.grpHistory.TabStop = false;
			this.grpHistory.Text = "Lines Written";
			// 
			// graphLines
			// 
			this.graphLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graphLines.Location = new System.Drawing.Point(6, 24);
			this.graphLines.MaxTicks = 5;
			this.graphLines.Name = "graphLines";
			this.graphLines.Size = new System.Drawing.Size(392, 247);
			this.graphLines.TabIndex = 0;
			// 
			// grpPartners
			// 
			this.grpPartners.Controls.Add(this.cmdPreviousGraph);
			this.grpPartners.Controls.Add(this.cmdNextGraph);
			this.grpPartners.Controls.Add(this.lblLines);
			this.grpPartners.Controls.Add(this.graphPartners);
			this.grpPartners.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpPartners.Location = new System.Drawing.Point(6, 6);
			this.grpPartners.Name = "grpPartners";
			this.grpPartners.Size = new System.Drawing.Size(404, 243);
			this.grpPartners.TabIndex = 1;
			this.grpPartners.TabStop = false;
			this.grpPartners.Text = "Franchise Partners";
			// 
			// cmdPreviousGraph
			// 
			this.cmdPreviousGraph.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdPreviousGraph.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdPreviousGraph.Flat = false;
			this.cmdPreviousGraph.Image = global::SPNATI_Character_Editor.Properties.Resources.PreviousFrame;
			this.cmdPreviousGraph.Location = new System.Drawing.Point(9, 25);
			this.cmdPreviousGraph.Name = "cmdPreviousGraph";
			this.cmdPreviousGraph.Size = new System.Drawing.Size(16, 16);
			this.cmdPreviousGraph.TabIndex = 3;
			this.tooltipPartners.SetToolTip(this.cmdPreviousGraph, "Previous Graph");
			this.cmdPreviousGraph.UseVisualStyleBackColor = true;
			this.cmdPreviousGraph.Click += new System.EventHandler(this.cmdPreviousGraph_Click);
			// 
			// cmdNextGraph
			// 
			this.cmdNextGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNextGraph.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdNextGraph.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdNextGraph.Flat = false;
			this.cmdNextGraph.Image = global::SPNATI_Character_Editor.Properties.Resources.NextFrame;
			this.cmdNextGraph.Location = new System.Drawing.Point(382, 25);
			this.cmdNextGraph.Name = "cmdNextGraph";
			this.cmdNextGraph.Size = new System.Drawing.Size(16, 16);
			this.cmdNextGraph.TabIndex = 2;
			this.tooltipPartners.SetToolTip(this.cmdNextGraph, "Next Graph");
			this.cmdNextGraph.UseVisualStyleBackColor = true;
			this.cmdNextGraph.Click += new System.EventHandler(this.cmdNextGraph_Click);
			// 
			// lblLines
			// 
			this.lblLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblLines.ForeColor = System.Drawing.Color.Blue;
			this.lblLines.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblLines.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblLines.Location = new System.Drawing.Point(6, 25);
			this.lblLines.Name = "lblLines";
			this.lblLines.Size = new System.Drawing.Size(395, 16);
			this.lblLines.TabIndex = 1;
			this.lblLines.Text = "Lines";
			this.lblLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// graphPartners
			// 
			this.graphPartners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graphPartners.Location = new System.Drawing.Point(6, 44);
			this.graphPartners.Name = "graphPartners";
			this.graphPartners.ShowLegend = true;
			this.graphPartners.ShowTotals = true;
			this.graphPartners.Size = new System.Drawing.Size(392, 190);
			this.graphPartners.TabIndex = 0;
			// 
			// Dashboard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "Dashboard";
			this.Size = new System.Drawing.Size(817, 548);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.colLeft.Panel1.ResumeLayout(false);
			this.colLeft.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.colLeft)).EndInit();
			this.colLeft.ResumeLayout(false);
			this.grpChecklist.ResumeLayout(false);
			this.grpChecklist.PerformLayout();
			this.grpRequirements.ResumeLayout(false);
			this.tableRequirements.ResumeLayout(false);
			this.colRight.Panel1.ResumeLayout(false);
			this.colRight.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.colRight)).EndInit();
			this.colRight.ResumeLayout(false);
			this.grpHistory.ResumeLayout(false);
			this.grpPartners.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox grpChecklist;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer colLeft;
		private Desktop.Skinning.SkinnedGroupBox grpRequirements;
		private System.Windows.Forms.TableLayoutPanel tableRequirements;
		private Desktop.CommonControls.RadialGauge barLines;
		private Desktop.CommonControls.RadialGauge barUnique;
		private Desktop.CommonControls.RadialGauge barFilters;
		private Desktop.CommonControls.RadialGauge barTargets;
		private Desktop.CommonControls.RadialGauge barSize;
		private System.Windows.Forms.SplitContainer colRight;
		private Desktop.Skinning.SkinnedGroupBox grpHistory;
		private Desktop.CommonControls.LineGraph graphLines;
		private Desktop.Skinning.SkinnedGroupBox grpPartners;
		private Desktop.CommonControls.Graphs.StackedBarGraph graphPartners;
		private Desktop.Skinning.SkinnedLabel lblLines;
		private Desktop.Skinning.SkinnedIcon cmdPreviousGraph;
		private Desktop.Skinning.SkinnedIcon cmdNextGraph;
		private System.Windows.Forms.ToolTip tooltipPartners;
	}
}

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	partial class SponsorshipWidget
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
			this.grpRequirements = new Desktop.Skinning.SkinnedGroupBox();
			this.tableRequirements = new System.Windows.Forms.TableLayoutPanel();
			this.barSize = new Desktop.CommonControls.RadialGauge();
			this.barUnique = new Desktop.CommonControls.RadialGauge();
			this.barFilters = new Desktop.CommonControls.RadialGauge();
			this.barTargets = new Desktop.CommonControls.RadialGauge();
			this.barLines = new Desktop.CommonControls.RadialGauge();
			this.barCollectibles = new Desktop.CommonControls.RadialGauge();
			this.grpRequirements.SuspendLayout();
			this.tableRequirements.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpRequirements
			// 
			this.grpRequirements.BackColor = System.Drawing.Color.White;
			this.grpRequirements.Controls.Add(this.tableRequirements);
			this.grpRequirements.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpRequirements.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpRequirements.Image = null;
			this.grpRequirements.Location = new System.Drawing.Point(0, 0);
			this.grpRequirements.Name = "grpRequirements";
			this.grpRequirements.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpRequirements.ShowIndicatorBar = false;
			this.grpRequirements.Size = new System.Drawing.Size(487, 334);
			this.grpRequirements.TabIndex = 1;
			this.grpRequirements.TabStop = false;
			this.grpRequirements.Text = "Sponsorship";
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
			this.tableRequirements.Controls.Add(this.barCollectibles, 2, 1);
			this.tableRequirements.Location = new System.Drawing.Point(6, 25);
			this.tableRequirements.Name = "tableRequirements";
			this.tableRequirements.RowCount = 2;
			this.tableRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableRequirements.Size = new System.Drawing.Size(475, 303);
			this.tableRequirements.TabIndex = 1;
			// 
			// barSize
			// 
			this.barSize.CapacityMode = true;
			this.barSize.Caption = "File Space";
			this.barSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barSize.HighlightOverCapacity = false;
			this.barSize.InvertCapacityColors = false;
			this.barSize.Location = new System.Drawing.Point(161, 154);
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
			this.barSize.Size = new System.Drawing.Size(152, 146);
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
			this.barUnique.HighlightOverCapacity = false;
			this.barUnique.InvertCapacityColors = true;
			this.barUnique.Location = new System.Drawing.Point(319, 3);
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
			this.barUnique.Size = new System.Drawing.Size(153, 145);
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
			this.barFilters.HighlightOverCapacity = false;
			this.barFilters.InvertCapacityColors = true;
			this.barFilters.Location = new System.Drawing.Point(3, 154);
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
			this.barFilters.Size = new System.Drawing.Size(152, 146);
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
			this.barTargets.HighlightOverCapacity = false;
			this.barTargets.InvertCapacityColors = true;
			this.barTargets.Location = new System.Drawing.Point(161, 3);
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
			this.barTargets.Size = new System.Drawing.Size(152, 145);
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
			this.barLines.HighlightOverCapacity = false;
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
			this.barLines.Size = new System.Drawing.Size(152, 145);
			this.barLines.TabIndex = 0;
			this.barLines.Unit = "lines";
			this.barLines.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// barCollectibles
			// 
			this.barCollectibles.CapacityMode = true;
			this.barCollectibles.Caption = "Allowed Collectibles";
			this.barCollectibles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.barCollectibles.HighlightOverCapacity = true;
			this.barCollectibles.InvertCapacityColors = false;
			this.barCollectibles.Location = new System.Drawing.Point(319, 154);
			this.barCollectibles.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.barCollectibles.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.barCollectibles.Name = "barCollectibles";
			this.barCollectibles.ShowPercentage = false;
			this.barCollectibles.Size = new System.Drawing.Size(153, 146);
			this.barCollectibles.TabIndex = 0;
			this.barCollectibles.Unit = "collectibles";
			this.barCollectibles.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// SponsorshipWidget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpRequirements);
			this.Name = "SponsorshipWidget";
			this.Size = new System.Drawing.Size(487, 334);
			this.grpRequirements.ResumeLayout(false);
			this.tableRequirements.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox grpRequirements;
		private System.Windows.Forms.TableLayoutPanel tableRequirements;
		private Desktop.CommonControls.RadialGauge barSize;
		private Desktop.CommonControls.RadialGauge barUnique;
		private Desktop.CommonControls.RadialGauge barFilters;
		private Desktop.CommonControls.RadialGauge barTargets;
		private Desktop.CommonControls.RadialGauge barLines;
		private Desktop.CommonControls.RadialGauge barCollectibles;
	}
}

namespace SPNATI_Character_Editor.Controls.Dashboards
{
	partial class ComparisonWidget
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
			this.grpPartners = new Desktop.Skinning.SkinnedGroupBox();
			this.cboGroups = new Desktop.Skinning.SkinnedComboBox();
			this.cmdPreviousGraph = new Desktop.Skinning.SkinnedIcon();
			this.cmdNextGraph = new Desktop.Skinning.SkinnedIcon();
			this.lblLines = new Desktop.Skinning.SkinnedLabel();
			this.graphPartners = new Desktop.CommonControls.Graphs.StackedBarGraph();
			this.grpPartners.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpPartners
			// 
			this.grpPartners.BackColor = System.Drawing.Color.White;
			this.grpPartners.Controls.Add(this.cboGroups);
			this.grpPartners.Controls.Add(this.cmdPreviousGraph);
			this.grpPartners.Controls.Add(this.cmdNextGraph);
			this.grpPartners.Controls.Add(this.lblLines);
			this.grpPartners.Controls.Add(this.graphPartners);
			this.grpPartners.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpPartners.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpPartners.Image = null;
			this.grpPartners.Location = new System.Drawing.Point(0, 0);
			this.grpPartners.Name = "grpPartners";
			this.grpPartners.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpPartners.ShowIndicatorBar = false;
			this.grpPartners.Size = new System.Drawing.Size(427, 318);
			this.grpPartners.TabIndex = 2;
			this.grpPartners.TabStop = false;
			this.grpPartners.Text = "Franchise Partners";
			// 
			// cboGroups
			// 
			this.cboGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboGroups.BackColor = System.Drawing.Color.White;
			this.cboGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGroups.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboGroups.KeyMember = null;
			this.cboGroups.Location = new System.Drawing.Point(240, 3);
			this.cboGroups.Name = "cboGroups";
			this.cboGroups.SelectedIndex = -1;
			this.cboGroups.SelectedItem = null;
			this.cboGroups.Size = new System.Drawing.Size(181, 23);
			this.cboGroups.Sorted = false;
			this.cboGroups.TabIndex = 4;
			this.cboGroups.SelectedIndexChanged += new System.EventHandler(this.cboGroups_SelectedIndexChanged);
			// 
			// cmdPreviousGraph
			// 
			this.cmdPreviousGraph.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdPreviousGraph.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdPreviousGraph.Flat = false;
			this.cmdPreviousGraph.Image = global::SPNATI_Character_Editor.Properties.Resources.PreviousFrame;
			this.cmdPreviousGraph.Location = new System.Drawing.Point(9, 29);
			this.cmdPreviousGraph.Name = "cmdPreviousGraph";
			this.cmdPreviousGraph.Size = new System.Drawing.Size(16, 16);
			this.cmdPreviousGraph.TabIndex = 3;
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
			this.cmdNextGraph.Location = new System.Drawing.Point(405, 29);
			this.cmdNextGraph.Name = "cmdNextGraph";
			this.cmdNextGraph.Size = new System.Drawing.Size(16, 16);
			this.cmdNextGraph.TabIndex = 2;
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
			this.lblLines.Location = new System.Drawing.Point(6, 29);
			this.lblLines.Name = "lblLines";
			this.lblLines.Size = new System.Drawing.Size(418, 16);
			this.lblLines.TabIndex = 1;
			this.lblLines.Text = "Lines";
			this.lblLines.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// graphPartners
			// 
			this.graphPartners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graphPartners.HorizontalOrientation = false;
			this.graphPartners.Location = new System.Drawing.Point(6, 47);
			this.graphPartners.Name = "graphPartners";
			this.graphPartners.ShowLegend = true;
			this.graphPartners.ShowTotals = true;
			this.graphPartners.Size = new System.Drawing.Size(415, 262);
			this.graphPartners.TabIndex = 0;
			// 
			// ComparisonWidget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpPartners);
			this.Name = "ComparisonWidget";
			this.Size = new System.Drawing.Size(427, 318);
			this.grpPartners.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox grpPartners;
		private Desktop.Skinning.SkinnedIcon cmdPreviousGraph;
		private Desktop.Skinning.SkinnedIcon cmdNextGraph;
		private Desktop.Skinning.SkinnedLabel lblLines;
		private Desktop.CommonControls.Graphs.StackedBarGraph graphPartners;
		private Desktop.Skinning.SkinnedComboBox cboGroups;
	}
}

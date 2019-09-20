namespace SPNATI_Character_Editor.Controls.Dashboards
{
	partial class TargetWidget
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
			this.grpWidget = new Desktop.Skinning.SkinnedGroupBox();
			this.lblName = new Desktop.Skinning.SkinnedLabel();
			this.graphLines = new Desktop.CommonControls.Graphs.StackedBarGraph();
			this.linkGo = new Desktop.CommonControls.ActivityLink();
			this.grpWidget.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpWidget
			// 
			this.grpWidget.BackColor = System.Drawing.Color.White;
			this.grpWidget.Controls.Add(this.linkGo);
			this.grpWidget.Controls.Add(this.lblName);
			this.grpWidget.Controls.Add(this.graphLines);
			this.grpWidget.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpWidget.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpWidget.Image = null;
			this.grpWidget.Location = new System.Drawing.Point(0, 0);
			this.grpWidget.Name = "grpWidget";
			this.grpWidget.Size = new System.Drawing.Size(396, 316);
			this.grpWidget.TabIndex = 1;
			this.grpWidget.TabStop = false;
			this.grpWidget.Text = "Targeting";
			// 
			// lblName
			// 
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblName.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblName.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblName.Location = new System.Drawing.Point(6, 26);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(384, 13);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "Most Targeted Opponents";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// graphLines
			// 
			this.graphLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graphLines.HorizontalOrientation = false;
			this.graphLines.Location = new System.Drawing.Point(6, 42);
			this.graphLines.Name = "graphLines";
			this.graphLines.ShowLegend = false;
			this.graphLines.ShowTotals = true;
			this.graphLines.Size = new System.Drawing.Size(384, 268);
			this.graphLines.TabIndex = 0;
			// 
			// linkGo
			// 
			this.linkGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkGo.LaunchHandler = null;
			this.linkGo.LaunchParameters = null;
			this.linkGo.Location = new System.Drawing.Point(357, 3);
			this.linkGo.Name = "linkGo";
			this.linkGo.Size = new System.Drawing.Size(33, 24);
			this.linkGo.TabIndex = 2;
			this.linkGo.Visible = false;
			// 
			// TargetWidget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpWidget);
			this.Name = "TargetWidget";
			this.Size = new System.Drawing.Size(396, 316);
			this.grpWidget.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox grpWidget;
		private Desktop.Skinning.SkinnedLabel lblName;
		private Desktop.CommonControls.Graphs.StackedBarGraph graphLines;
		private Desktop.CommonControls.ActivityLink linkGo;
	}
}

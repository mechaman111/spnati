namespace SPNATI_Character_Editor.Controls.Dashboards
{
	partial class LineHistoryWidget
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
			this.grpHistory = new Desktop.Skinning.SkinnedGroupBox();
			this.graphLines = new Desktop.CommonControls.LineGraph();
			this.grpHistory.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpHistory
			// 
			this.grpHistory.Controls.Add(this.graphLines);
			this.grpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpHistory.Location = new System.Drawing.Point(0, 0);
			this.grpHistory.Name = "grpHistory";
			this.grpHistory.Size = new System.Drawing.Size(430, 354);
			this.grpHistory.TabIndex = 2;
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
			this.graphLines.Size = new System.Drawing.Size(418, 324);
			this.graphLines.TabIndex = 0;
			// 
			// LineHistoryWidget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpHistory);
			this.Name = "LineHistoryWidget";
			this.Size = new System.Drawing.Size(430, 354);
			this.grpHistory.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox grpHistory;
		private Desktop.CommonControls.LineGraph graphLines;
	}
}

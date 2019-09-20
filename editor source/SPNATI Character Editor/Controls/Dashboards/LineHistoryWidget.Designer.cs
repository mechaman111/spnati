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
			this.cmdGoals = new Desktop.Skinning.SkinnedButton();
			this.lblGoalMet = new Desktop.Skinning.SkinnedLabel();
			this.grpHistory.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpHistory
			// 
			this.grpHistory.BackColor = System.Drawing.Color.White;
			this.grpHistory.Controls.Add(this.lblGoalMet);
			this.grpHistory.Controls.Add(this.cmdGoals);
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
			// cmdGoals
			// 
			this.cmdGoals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGoals.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdGoals.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdGoals.Flat = true;
			this.cmdGoals.ForeColor = System.Drawing.Color.Blue;
			this.cmdGoals.Image = global::SPNATI_Character_Editor.Properties.Resources.Settings;
			this.cmdGoals.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdGoals.Location = new System.Drawing.Point(323, 3);
			this.cmdGoals.Name = "cmdGoals";
			this.cmdGoals.Size = new System.Drawing.Size(101, 23);
			this.cmdGoals.TabIndex = 2;
			this.cmdGoals.Text = "Set Goals";
			this.cmdGoals.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.cmdGoals.UseVisualStyleBackColor = true;
			this.cmdGoals.Click += new System.EventHandler(this.cmdGoals_Click);
			// 
			// lblGoalMet
			// 
			this.lblGoalMet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblGoalMet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblGoalMet.ForeColor = System.Drawing.Color.Green;
			this.lblGoalMet.Highlight = Desktop.Skinning.SkinnedHighlight.Good;
			this.lblGoalMet.Image = global::SPNATI_Character_Editor.Properties.Resources.Checkmark;
			this.lblGoalMet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblGoalMet.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblGoalMet.Location = new System.Drawing.Point(205, 2);
			this.lblGoalMet.Name = "lblGoalMet";
			this.lblGoalMet.Size = new System.Drawing.Size(112, 23);
			this.lblGoalMet.TabIndex = 3;
			this.lblGoalMet.Text = "Today\'s goals met!";
			this.lblGoalMet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblGoalMet.Visible = false;
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
		private Desktop.Skinning.SkinnedButton cmdGoals;
		private Desktop.Skinning.SkinnedLabel lblGoalMet;
	}
}

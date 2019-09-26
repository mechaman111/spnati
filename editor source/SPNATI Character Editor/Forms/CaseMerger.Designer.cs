namespace SPNATI_Character_Editor.Forms
{
	partial class CaseMerger
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lstDupes = new Desktop.Skinning.SkinnedListBox();
			this.lblExists = new Desktop.Skinning.SkinnedLabel();
			this.grpCases = new Desktop.Skinning.SkinnedGroupBox();
			this.cmdMerge = new Desktop.Skinning.SkinnedButton();
			this.cmdSkip = new Desktop.Skinning.SkinnedButton();
			this.caseControl1 = new SPNATI_Character_Editor.Controls.CaseControl();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.grpCases.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstDupes
			// 
			this.lstDupes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstDupes.BackColor = System.Drawing.Color.White;
			this.lstDupes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstDupes.ForeColor = System.Drawing.Color.Black;
			this.lstDupes.FormattingEnabled = true;
			this.lstDupes.Location = new System.Drawing.Point(9, 41);
			this.lstDupes.Name = "lstDupes";
			this.lstDupes.Size = new System.Drawing.Size(165, 511);
			this.lstDupes.TabIndex = 1;
			// 
			// lblExists
			// 
			this.lblExists.AutoSize = true;
			this.lblExists.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblExists.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblExists.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblExists.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblExists.Location = new System.Drawing.Point(6, 25);
			this.lblExists.Name = "lblExists";
			this.lblExists.Size = new System.Drawing.Size(48, 13);
			this.lblExists.TabIndex = 3;
			this.lblExists.Text = "Exists in:";
			// 
			// grpCases
			// 
			this.grpCases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpCases.BackColor = System.Drawing.Color.White;
			this.grpCases.Controls.Add(this.cmdMerge);
			this.grpCases.Controls.Add(this.cmdSkip);
			this.grpCases.Controls.Add(this.caseControl1);
			this.grpCases.Controls.Add(this.lstDupes);
			this.grpCases.Controls.Add(this.lblExists);
			this.grpCases.Location = new System.Drawing.Point(3, 32);
			this.grpCases.Name = "grpCases";
			this.grpCases.Size = new System.Drawing.Size(864, 567);
			this.grpCases.TabIndex = 4;
			this.grpCases.TabStop = false;
			this.grpCases.Text = "Case X of Y";
			// 
			// cmdMerge
			// 
			this.cmdMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdMerge.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdMerge.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdMerge.Flat = true;
			this.cmdMerge.ForeColor = System.Drawing.Color.Blue;
			this.cmdMerge.Image = global::SPNATI_Character_Editor.Properties.Resources.Checkmark;
			this.cmdMerge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdMerge.Location = new System.Drawing.Point(780, 6);
			this.cmdMerge.Name = "cmdMerge";
			this.cmdMerge.Size = new System.Drawing.Size(75, 23);
			this.cmdMerge.TabIndex = 5;
			this.cmdMerge.Text = "Merge";
			this.cmdMerge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.cmdMerge, "Merge the cases into one and advance to the next");
			this.cmdMerge.UseVisualStyleBackColor = true;
			this.cmdMerge.Click += new System.EventHandler(this.cmdMerge_Click);
			// 
			// cmdSkip
			// 
			this.cmdSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSkip.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdSkip.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdSkip.Flat = true;
			this.cmdSkip.ForeColor = System.Drawing.Color.Blue;
			this.cmdSkip.Image = global::SPNATI_Character_Editor.Properties.Resources.Undo;
			this.cmdSkip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdSkip.Location = new System.Drawing.Point(699, 6);
			this.cmdSkip.Name = "cmdSkip";
			this.cmdSkip.Size = new System.Drawing.Size(75, 23);
			this.cmdSkip.TabIndex = 4;
			this.cmdSkip.Text = "Skip";
			this.toolTip1.SetToolTip(this.cmdSkip, "Leave these cases as is and advance to the next");
			this.cmdSkip.UseVisualStyleBackColor = true;
			this.cmdSkip.Click += new System.EventHandler(this.cmdSkip_Click);
			// 
			// caseControl1
			// 
			this.caseControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.caseControl1.Location = new System.Drawing.Point(180, 41);
			this.caseControl1.Name = "caseControl1";
			this.caseControl1.Size = new System.Drawing.Size(678, 512);
			this.caseControl1.TabIndex = 2;
			// 
			// CaseMerger
			// 
			this.AcceptButton = this.cmdMerge;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(870, 603);
			this.Controls.Add(this.grpCases);
			this.Name = "CaseMerger";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Case Merger Utility";
			this.grpCases.ResumeLayout(false);
			this.grpCases.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.Skinning.SkinnedListBox lstDupes;
		private Controls.CaseControl caseControl1;
		private Desktop.Skinning.SkinnedLabel lblExists;
		private Desktop.Skinning.SkinnedGroupBox grpCases;
		private Desktop.Skinning.SkinnedButton cmdMerge;
		private Desktop.Skinning.SkinnedButton cmdSkip;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
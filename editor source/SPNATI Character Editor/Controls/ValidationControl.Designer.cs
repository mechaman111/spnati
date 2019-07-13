namespace SPNATI_Character_Editor.Controls
{
	partial class ValidationControl
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
			this.lstFilters = new Desktop.Skinning.SkinnedListBox();
			this.lstCharacters = new Desktop.Skinning.SkinnedListBox();
			this.lstWarnings = new Desktop.Skinning.SkinnedListBox();
			this.pnlValid = new Desktop.Skinning.SkinnedPanel();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.pnlWarnings = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdGoTo = new Desktop.Skinning.SkinnedButton();
			this.cmdCopy = new Desktop.Skinning.SkinnedButton();
			this.cmdCopyAll = new Desktop.Skinning.SkinnedButton();
			this.pnlProgress = new Desktop.Skinning.SkinnedPanel();
			this.lblProgress = new Desktop.Skinning.SkinnedLabel();
			this.progressBar = new Desktop.Skinning.SkinnedProgressBar();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox3 = new Desktop.Skinning.SkinnedGroupBox();
			this.pnlValid.SuspendLayout();
			this.pnlWarnings.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.pnlProgress.SuspendLayout();
			this.skinnedGroupBox1.SuspendLayout();
			this.skinnedGroupBox2.SuspendLayout();
			this.skinnedGroupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstFilters
			// 
			this.lstFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstFilters.BackColor = System.Drawing.Color.White;
			this.lstFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstFilters.ForeColor = System.Drawing.Color.Black;
			this.lstFilters.FormattingEnabled = true;
			this.lstFilters.Location = new System.Drawing.Point(6, 24);
			this.lstFilters.Name = "lstFilters";
			this.lstFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lstFilters.Size = new System.Drawing.Size(200, 108);
			this.lstFilters.TabIndex = 12;
			this.lstFilters.SelectedIndexChanged += new System.EventHandler(this.lstFilters_SelectedIndexChanged);
			// 
			// lstCharacters
			// 
			this.lstCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCharacters.BackColor = System.Drawing.Color.White;
			this.lstCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstCharacters.ForeColor = System.Drawing.Color.Black;
			this.lstCharacters.FormattingEnabled = true;
			this.lstCharacters.Location = new System.Drawing.Point(6, 23);
			this.lstCharacters.Name = "lstCharacters";
			this.lstCharacters.Size = new System.Drawing.Size(200, 433);
			this.lstCharacters.TabIndex = 8;
			this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);
			// 
			// lstWarnings
			// 
			this.lstWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstWarnings.BackColor = System.Drawing.Color.White;
			this.lstWarnings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstWarnings.ForeColor = System.Drawing.Color.Black;
			this.lstWarnings.HorizontalScrollbar = true;
			this.lstWarnings.Location = new System.Drawing.Point(0, 0);
			this.lstWarnings.Name = "lstWarnings";
			this.lstWarnings.Size = new System.Drawing.Size(837, 563);
			this.lstWarnings.TabIndex = 9;
			this.lstWarnings.SelectedIndexChanged += new System.EventHandler(this.lstWarnings_SelectedIndexChanged);
			this.lstWarnings.DoubleClick += new System.EventHandler(this.lstWarnings_DoubleClick);
			// 
			// pnlValid
			// 
			this.pnlValid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlValid.Controls.Add(this.label4);
			this.pnlValid.Location = new System.Drawing.Point(6, 25);
			this.pnlValid.Name = "pnlValid";
			this.pnlValid.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.pnlValid.Size = new System.Drawing.Size(837, 597);
			this.pnlValid.TabIndex = 14;
			this.pnlValid.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlValid.Visible = false;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.Color.Green;
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label4.Location = new System.Drawing.Point(3, 209);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(831, 173);
			this.label4.TabIndex = 0;
			this.label4.Text = "Everything Checks Out!";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlWarnings
			// 
			this.pnlWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlWarnings.Controls.Add(this.skinnedPanel1);
			this.pnlWarnings.Controls.Add(this.lstWarnings);
			this.pnlWarnings.Location = new System.Drawing.Point(6, 25);
			this.pnlWarnings.Name = "pnlWarnings";
			this.pnlWarnings.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlWarnings.Size = new System.Drawing.Size(837, 597);
			this.pnlWarnings.TabIndex = 14;
			this.pnlWarnings.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdGoTo);
			this.skinnedPanel1.Controls.Add(this.cmdCopy);
			this.skinnedPanel1.Controls.Add(this.cmdCopyAll);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 565);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedPanel1.Size = new System.Drawing.Size(837, 32);
			this.skinnedPanel1.TabIndex = 13;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdGoTo
			// 
			this.cmdGoTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGoTo.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdGoTo.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdGoTo.Flat = false;
			this.cmdGoTo.Location = new System.Drawing.Point(681, 5);
			this.cmdGoTo.Name = "cmdGoTo";
			this.cmdGoTo.Size = new System.Drawing.Size(153, 23);
			this.cmdGoTo.TabIndex = 10;
			this.cmdGoTo.Text = "Go to Warning";
			this.toolTip1.SetToolTip(this.cmdGoTo, "Jumps to the line causing the selected warning");
			this.cmdGoTo.UseVisualStyleBackColor = true;
			this.cmdGoTo.Click += new System.EventHandler(this.cmdGoTo_Click);
			// 
			// cmdCopy
			// 
			this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCopy.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCopy.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCopy.Flat = false;
			this.cmdCopy.ForeColor = System.Drawing.Color.Blue;
			this.cmdCopy.Location = new System.Drawing.Point(3, 5);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new System.Drawing.Size(176, 23);
			this.cmdCopy.TabIndex = 11;
			this.cmdCopy.Text = "Copy to Clipboard";
			this.toolTip1.SetToolTip(this.cmdCopy, "Copies all validation warnings for this character to the clipboard");
			this.cmdCopy.UseVisualStyleBackColor = true;
			this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
			// 
			// cmdCopyAll
			// 
			this.cmdCopyAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCopyAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCopyAll.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCopyAll.Flat = false;
			this.cmdCopyAll.ForeColor = System.Drawing.Color.Blue;
			this.cmdCopyAll.Location = new System.Drawing.Point(183, 5);
			this.cmdCopyAll.Name = "cmdCopyAll";
			this.cmdCopyAll.Size = new System.Drawing.Size(176, 23);
			this.cmdCopyAll.TabIndex = 12;
			this.cmdCopyAll.Text = "Copy All to Clipboard";
			this.toolTip1.SetToolTip(this.cmdCopyAll, "Copies all validation warnings for every character to the clipboard");
			this.cmdCopyAll.UseVisualStyleBackColor = true;
			this.cmdCopyAll.Click += new System.EventHandler(this.cmdCopyAll_Click);
			// 
			// pnlProgress
			// 
			this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlProgress.Controls.Add(this.lblProgress);
			this.pnlProgress.Controls.Add(this.progressBar);
			this.pnlProgress.Location = new System.Drawing.Point(6, 25);
			this.pnlProgress.Name = "pnlProgress";
			this.pnlProgress.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.pnlProgress.Size = new System.Drawing.Size(837, 597);
			this.pnlProgress.TabIndex = 1;
			this.pnlProgress.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlProgress.Visible = false;
			// 
			// lblProgress
			// 
			this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblProgress.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblProgress.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblProgress.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblProgress.Location = new System.Drawing.Point(3, 260);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(831, 50);
			this.lblProgress.TabIndex = 3;
			this.lblProgress.Text = "Loading...";
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(282, 313);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(264, 23);
			this.progressBar.TabIndex = 2;
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.skinnedGroupBox1.Controls.Add(this.lstCharacters);
			this.skinnedGroupBox1.Location = new System.Drawing.Point(6, 5);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.Size = new System.Drawing.Size(212, 475);
			this.skinnedGroupBox1.TabIndex = 13;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Characters";
			// 
			// skinnedGroupBox2
			// 
			this.skinnedGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.skinnedGroupBox2.Controls.Add(this.lstFilters);
			this.skinnedGroupBox2.Location = new System.Drawing.Point(6, 486);
			this.skinnedGroupBox2.Name = "skinnedGroupBox2";
			this.skinnedGroupBox2.Size = new System.Drawing.Size(212, 147);
			this.skinnedGroupBox2.TabIndex = 9;
			this.skinnedGroupBox2.TabStop = false;
			this.skinnedGroupBox2.Text = "Filters";
			// 
			// skinnedGroupBox3
			// 
			this.skinnedGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox3.Controls.Add(this.pnlWarnings);
			this.skinnedGroupBox3.Controls.Add(this.pnlValid);
			this.skinnedGroupBox3.Controls.Add(this.pnlProgress);
			this.skinnedGroupBox3.Location = new System.Drawing.Point(224, 5);
			this.skinnedGroupBox3.Name = "skinnedGroupBox3";
			this.skinnedGroupBox3.Size = new System.Drawing.Size(849, 628);
			this.skinnedGroupBox3.TabIndex = 13;
			this.skinnedGroupBox3.TabStop = false;
			this.skinnedGroupBox3.Text = "Warnings";
			// 
			// ValidationControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedGroupBox3);
			this.Controls.Add(this.skinnedGroupBox2);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Name = "ValidationControl";
			this.Size = new System.Drawing.Size(1076, 636);
			this.pnlValid.ResumeLayout(false);
			this.pnlWarnings.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.pnlProgress.ResumeLayout(false);
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox2.ResumeLayout(false);
			this.skinnedGroupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.Skinning.SkinnedListBox lstFilters;
		private Desktop.Skinning.SkinnedListBox lstCharacters;
		private Desktop.Skinning.SkinnedListBox lstWarnings;
		private Desktop.Skinning.SkinnedPanel pnlWarnings;
		private Desktop.Skinning.SkinnedPanel pnlValid;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedPanel pnlProgress;
		private Desktop.Skinning.SkinnedLabel lblProgress;
		private Desktop.Skinning.SkinnedProgressBar progressBar;
		private Desktop.Skinning.SkinnedButton cmdGoTo;
		private Desktop.Skinning.SkinnedButton cmdCopy;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedButton cmdCopyAll;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox2;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox3;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}

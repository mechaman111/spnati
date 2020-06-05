namespace SPNATI_Character_Editor
{
	partial class FindReplace
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
			this.tabs = new Desktop.Skinning.SkinnedTabControl();
			this.tabFind = new System.Windows.Forms.TabPage();
			this.tabReplace = new System.Windows.Forms.TabPage();
			this.lblFind = new Desktop.Skinning.SkinnedLabel();
			this.txtFind = new Desktop.Skinning.SkinnedTextBox();
			this.chkMatchCase = new Desktop.Skinning.SkinnedCheckBox();
			this.chkWholeWords = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdFind = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.txtReplace = new Desktop.Skinning.SkinnedTextBox();
			this.lblReplace = new Desktop.Skinning.SkinnedLabel();
			this.cmdReplace = new Desktop.Skinning.SkinnedButton();
			this.cmdReplaceAll = new Desktop.Skinning.SkinnedButton();
			this.focusTimer = new System.Windows.Forms.Timer(this.components);
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.strip = new Desktop.Skinning.SkinnedTabStrip();
			this.chkMarkers = new Desktop.Skinning.SkinnedCheckBox();
			this.tabs.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabFind);
			this.tabs.Controls.Add(this.tabReplace);
			this.tabs.Location = new System.Drawing.Point(2, 27);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(390, 26);
			this.tabs.TabIndex = 20;
			this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
			// 
			// tabFind
			// 
			this.tabFind.BackColor = System.Drawing.Color.White;
			this.tabFind.ForeColor = System.Drawing.Color.Black;
			this.tabFind.Location = new System.Drawing.Point(4, 22);
			this.tabFind.Name = "tabFind";
			this.tabFind.Padding = new System.Windows.Forms.Padding(3);
			this.tabFind.Size = new System.Drawing.Size(382, 0);
			this.tabFind.TabIndex = 0;
			this.tabFind.Text = "Find";
			// 
			// tabReplace
			// 
			this.tabReplace.BackColor = System.Drawing.Color.White;
			this.tabReplace.ForeColor = System.Drawing.Color.Black;
			this.tabReplace.Location = new System.Drawing.Point(4, 22);
			this.tabReplace.Name = "tabReplace";
			this.tabReplace.Padding = new System.Windows.Forms.Padding(3);
			this.tabReplace.Size = new System.Drawing.Size(382, 0);
			this.tabReplace.TabIndex = 1;
			this.tabReplace.Text = "Replace";
			// 
			// lblFind
			// 
			this.lblFind.AutoSize = true;
			this.lblFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblFind.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFind.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblFind.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblFind.Location = new System.Drawing.Point(12, 58);
			this.lblFind.Name = "lblFind";
			this.lblFind.Size = new System.Drawing.Size(56, 13);
			this.lblFind.TabIndex = 0;
			this.lblFind.Text = "Fi&nd what:";
			// 
			// txtFind
			// 
			this.txtFind.BackColor = System.Drawing.Color.White;
			this.txtFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtFind.ForeColor = System.Drawing.Color.Black;
			this.txtFind.Location = new System.Drawing.Point(90, 55);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(196, 20);
			this.txtFind.TabIndex = 1;
			// 
			// chkMatchCase
			// 
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkMatchCase.Location = new System.Drawing.Point(15, 107);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(82, 17);
			this.chkMatchCase.TabIndex = 4;
			this.chkMatchCase.Text = "Matc&h case";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			// 
			// chkWholeWords
			// 
			this.chkWholeWords.AutoSize = true;
			this.chkWholeWords.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkWholeWords.Location = new System.Drawing.Point(15, 130);
			this.chkWholeWords.Name = "chkWholeWords";
			this.chkWholeWords.Size = new System.Drawing.Size(130, 17);
			this.chkWholeWords.TabIndex = 5;
			this.chkWholeWords.Text = "Find whole words onl&y";
			this.chkWholeWords.UseVisualStyleBackColor = true;
			// 
			// cmdFind
			// 
			this.cmdFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdFind.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdFind.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdFind.Flat = false;
			this.cmdFind.Location = new System.Drawing.Point(292, 53);
			this.cmdFind.Name = "cmdFind";
			this.cmdFind.Size = new System.Drawing.Size(95, 23);
			this.cmdFind.TabIndex = 50;
			this.cmdFind.Text = "&Find Next";
			this.cmdFind.UseVisualStyleBackColor = true;
			this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
			this.cmdFind.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnRestoreFocus);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(320, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 53;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// txtReplace
			// 
			this.txtReplace.BackColor = System.Drawing.Color.White;
			this.txtReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtReplace.ForeColor = System.Drawing.Color.Black;
			this.txtReplace.Location = new System.Drawing.Point(90, 81);
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(196, 20);
			this.txtReplace.TabIndex = 3;
			// 
			// lblReplace
			// 
			this.lblReplace.AutoSize = true;
			this.lblReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblReplace.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblReplace.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblReplace.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblReplace.Location = new System.Drawing.Point(12, 84);
			this.lblReplace.Name = "lblReplace";
			this.lblReplace.Size = new System.Drawing.Size(72, 13);
			this.lblReplace.TabIndex = 2;
			this.lblReplace.Text = "Replace w&ith:";
			// 
			// cmdReplace
			// 
			this.cmdReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdReplace.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdReplace.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdReplace.Flat = false;
			this.cmdReplace.Location = new System.Drawing.Point(292, 81);
			this.cmdReplace.Name = "cmdReplace";
			this.cmdReplace.Size = new System.Drawing.Size(95, 23);
			this.cmdReplace.TabIndex = 51;
			this.cmdReplace.Text = "&Replace";
			this.cmdReplace.UseVisualStyleBackColor = true;
			this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
			this.cmdReplace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnRestoreFocus);
			// 
			// cmdReplaceAll
			// 
			this.cmdReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdReplaceAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdReplaceAll.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdReplaceAll.Flat = false;
			this.cmdReplaceAll.Location = new System.Drawing.Point(292, 110);
			this.cmdReplaceAll.Name = "cmdReplaceAll";
			this.cmdReplaceAll.Size = new System.Drawing.Size(95, 23);
			this.cmdReplaceAll.TabIndex = 52;
			this.cmdReplaceAll.Text = "Replace &All";
			this.cmdReplaceAll.UseVisualStyleBackColor = true;
			this.cmdReplaceAll.Click += new System.EventHandler(this.cmdReplaceAll_Click);
			this.cmdReplaceAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnRestoreFocus);
			// 
			// focusTimer
			// 
			this.focusTimer.Tick += new System.EventHandler(this.focusTimer_Tick);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 153);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(399, 30);
			this.skinnedPanel1.TabIndex = 54;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// strip
			// 
			this.strip.AddCaption = null;
			this.strip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.strip.DecorationText = null;
			this.strip.Location = new System.Drawing.Point(1, 25);
			this.strip.Margin = new System.Windows.Forms.Padding(0);
			this.strip.Name = "strip";
			this.strip.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.strip.ShowAddButton = false;
			this.strip.ShowCloseButton = false;
			this.strip.Size = new System.Drawing.Size(397, 23);
			this.strip.StartMargin = 5;
			this.strip.TabControl = this.tabs;
			this.strip.TabIndex = 55;
			this.strip.TabMargin = 5;
			this.strip.TabPadding = 20;
			this.strip.TabSize = -1;
			this.strip.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.strip.Text = "skinnedTabStrip1";
			this.strip.Vertical = false;
			// 
			// chkMarkers
			// 
			this.chkMarkers.AutoSize = true;
			this.chkMarkers.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkMarkers.Location = new System.Drawing.Point(150, 107);
			this.chkMarkers.Name = "chkMarkers";
			this.chkMarkers.Size = new System.Drawing.Size(100, 17);
			this.chkMarkers.TabIndex = 56;
			this.chkMarkers.Text = "Search markers";
			this.chkMarkers.UseVisualStyleBackColor = true;
			this.chkMarkers.Visible = false;
			this.chkMarkers.CheckedChanged += new System.EventHandler(this.chkMarkers_CheckedChanged);
			// 
			// FindReplace
			// 
			this.AcceptButton = this.cmdFind;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(399, 183);
			this.Controls.Add(this.chkMarkers);
			this.Controls.Add(this.strip);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.cmdReplaceAll);
			this.Controls.Add(this.cmdReplace);
			this.Controls.Add(this.lblReplace);
			this.Controls.Add(this.txtReplace);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.cmdFind);
			this.Controls.Add(this.txtFind);
			this.Controls.Add(this.chkWholeWords);
			this.Controls.Add(this.lblFind);
			this.Controls.Add(this.chkMatchCase);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindReplace";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Sizable = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find and Replace";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindReplace_FormClosing);
			this.Shown += new System.EventHandler(this.FindReplace_Shown);
			this.tabs.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedTabControl tabs;
		private System.Windows.Forms.TabPage tabFind;
		private System.Windows.Forms.TabPage tabReplace;
		private Desktop.Skinning.SkinnedTextBox txtFind;
		private Desktop.Skinning.SkinnedLabel lblFind;
		private Desktop.Skinning.SkinnedCheckBox chkWholeWords;
		private Desktop.Skinning.SkinnedCheckBox chkMatchCase;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdFind;
		private Desktop.Skinning.SkinnedTextBox txtReplace;
		private Desktop.Skinning.SkinnedLabel lblReplace;
		private Desktop.Skinning.SkinnedButton cmdReplace;
		private Desktop.Skinning.SkinnedButton cmdReplaceAll;
		private System.Windows.Forms.Timer focusTimer;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedTabStrip strip;
		private Desktop.Skinning.SkinnedCheckBox chkMarkers;
	}
}
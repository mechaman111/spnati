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
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabFind = new System.Windows.Forms.TabPage();
			this.tabReplace = new System.Windows.Forms.TabPage();
			this.lblFind = new System.Windows.Forms.Label();
			this.txtFind = new System.Windows.Forms.TextBox();
			this.chkMatchCase = new System.Windows.Forms.CheckBox();
			this.chkWholeWords = new System.Windows.Forms.CheckBox();
			this.cmdFind = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.txtReplace = new System.Windows.Forms.TextBox();
			this.lblReplace = new System.Windows.Forms.Label();
			this.cmdReplace = new System.Windows.Forms.Button();
			this.cmdReplaceAll = new System.Windows.Forms.Button();
			this.focusTimer = new System.Windows.Forms.Timer(this.components);
			this.tabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabFind);
			this.tabs.Controls.Add(this.tabReplace);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(399, 26);
			this.tabs.TabIndex = 20;
			this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
			// 
			// tabFind
			// 
			this.tabFind.Location = new System.Drawing.Point(4, 22);
			this.tabFind.Name = "tabFind";
			this.tabFind.Padding = new System.Windows.Forms.Padding(3);
			this.tabFind.Size = new System.Drawing.Size(391, 0);
			this.tabFind.TabIndex = 0;
			this.tabFind.Text = "Find";
			this.tabFind.UseVisualStyleBackColor = true;
			// 
			// tabReplace
			// 
			this.tabReplace.Location = new System.Drawing.Point(4, 22);
			this.tabReplace.Name = "tabReplace";
			this.tabReplace.Padding = new System.Windows.Forms.Padding(3);
			this.tabReplace.Size = new System.Drawing.Size(391, 0);
			this.tabReplace.TabIndex = 1;
			this.tabReplace.Text = "Replace";
			this.tabReplace.UseVisualStyleBackColor = true;
			// 
			// lblFind
			// 
			this.lblFind.AutoSize = true;
			this.lblFind.Location = new System.Drawing.Point(12, 31);
			this.lblFind.Name = "lblFind";
			this.lblFind.Size = new System.Drawing.Size(56, 13);
			this.lblFind.TabIndex = 0;
			this.lblFind.Text = "Fi&nd what:";
			// 
			// txtFind
			// 
			this.txtFind.Location = new System.Drawing.Point(90, 28);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(203, 20);
			this.txtFind.TabIndex = 1;
			// 
			// chkMatchCase
			// 
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.Location = new System.Drawing.Point(15, 80);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(82, 17);
			this.chkMatchCase.TabIndex = 4;
			this.chkMatchCase.Text = "Matc&h case";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			// 
			// chkWholeWords
			// 
			this.chkWholeWords.AutoSize = true;
			this.chkWholeWords.Location = new System.Drawing.Point(15, 103);
			this.chkWholeWords.Name = "chkWholeWords";
			this.chkWholeWords.Size = new System.Drawing.Size(130, 17);
			this.chkWholeWords.TabIndex = 5;
			this.chkWholeWords.Text = "Find whole words onl&y";
			this.chkWholeWords.UseVisualStyleBackColor = true;
			// 
			// cmdFind
			// 
			this.cmdFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdFind.Location = new System.Drawing.Point(312, 26);
			this.cmdFind.Name = "cmdFind";
			this.cmdFind.Size = new System.Drawing.Size(75, 23);
			this.cmdFind.TabIndex = 50;
			this.cmdFind.Text = "&Find Next";
			this.cmdFind.UseVisualStyleBackColor = true;
			this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
			this.cmdFind.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnRestoreFocus);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(312, 113);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 53;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// txtReplace
			// 
			this.txtReplace.Location = new System.Drawing.Point(90, 54);
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(203, 20);
			this.txtReplace.TabIndex = 3;
			// 
			// lblReplace
			// 
			this.lblReplace.AutoSize = true;
			this.lblReplace.Location = new System.Drawing.Point(12, 57);
			this.lblReplace.Name = "lblReplace";
			this.lblReplace.Size = new System.Drawing.Size(72, 13);
			this.lblReplace.TabIndex = 2;
			this.lblReplace.Text = "Replace w&ith:";
			// 
			// cmdReplace
			// 
			this.cmdReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdReplace.Location = new System.Drawing.Point(312, 55);
			this.cmdReplace.Name = "cmdReplace";
			this.cmdReplace.Size = new System.Drawing.Size(75, 23);
			this.cmdReplace.TabIndex = 51;
			this.cmdReplace.Text = "&Replace";
			this.cmdReplace.UseVisualStyleBackColor = true;
			this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
			this.cmdReplace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnRestoreFocus);
			// 
			// cmdReplaceAll
			// 
			this.cmdReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdReplaceAll.Location = new System.Drawing.Point(312, 84);
			this.cmdReplaceAll.Name = "cmdReplaceAll";
			this.cmdReplaceAll.Size = new System.Drawing.Size(75, 23);
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
			// FindReplace
			// 
			this.AcceptButton = this.cmdFind;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(399, 148);
			this.Controls.Add(this.cmdReplaceAll);
			this.Controls.Add(this.cmdReplace);
			this.Controls.Add(this.lblReplace);
			this.Controls.Add(this.txtReplace);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.cmdFind);
			this.Controls.Add(this.txtFind);
			this.Controls.Add(this.chkWholeWords);
			this.Controls.Add(this.lblFind);
			this.Controls.Add(this.chkMatchCase);
			this.Name = "FindReplace";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find and Replace";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindReplace_FormClosing);
			this.Shown += new System.EventHandler(this.FindReplace_Shown);
			this.tabs.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabFind;
		private System.Windows.Forms.TabPage tabReplace;
		private System.Windows.Forms.TextBox txtFind;
		private System.Windows.Forms.Label lblFind;
		private System.Windows.Forms.CheckBox chkWholeWords;
		private System.Windows.Forms.CheckBox chkMatchCase;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdFind;
		private System.Windows.Forms.TextBox txtReplace;
		private System.Windows.Forms.Label lblReplace;
		private System.Windows.Forms.Button cmdReplace;
		private System.Windows.Forms.Button cmdReplaceAll;
		private System.Windows.Forms.Timer focusTimer;
	}
}
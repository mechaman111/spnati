namespace SPNATI_Character_Editor.Controls
{
	partial class LineImportControl
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
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.grpImports = new Desktop.Skinning.SkinnedGroupBox();
			this.caseEditor = new SPNATI_Character_Editor.Controls.CaseControl();
			this.lstCases = new Desktop.Skinning.SkinnedListBox();
			this.txtCode = new Desktop.Skinning.SkinnedTextBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.grpImports.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdImport
			// 
			this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImport.Flat = false;
			this.cmdImport.Location = new System.Drawing.Point(1073, 20);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(75, 23);
			this.cmdImport.TabIndex = 10;
			this.cmdImport.Text = "Import";
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(3, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Code:";
			// 
			// grpImports
			// 
			this.grpImports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpImports.BackColor = System.Drawing.Color.White;
			this.grpImports.Controls.Add(this.caseEditor);
			this.grpImports.Controls.Add(this.lstCases);
			this.grpImports.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpImports.Image = null;
			this.grpImports.Location = new System.Drawing.Point(9, 83);
			this.grpImports.Name = "grpImports";
			this.grpImports.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpImports.ShowIndicatorBar = false;
			this.grpImports.Size = new System.Drawing.Size(1145, 594);
			this.grpImports.TabIndex = 11;
			this.grpImports.TabStop = false;
			this.grpImports.Text = "Imported Cases";
			// 
			// caseEditor
			// 
			this.caseEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.caseEditor.Enabled = false;
			this.caseEditor.Location = new System.Drawing.Point(196, 19);
			this.caseEditor.Name = "caseEditor";
			this.caseEditor.Size = new System.Drawing.Size(943, 569);
			this.caseEditor.TabIndex = 1;
			this.caseEditor.Visible = false;
			this.caseEditor.HighlightRow += new System.EventHandler<int>(this.caseEditor_HighlightRow);
			// 
			// lstCases
			// 
			this.lstCases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstCases.BackColor = System.Drawing.Color.White;
			this.lstCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstCases.ForeColor = System.Drawing.Color.Black;
			this.lstCases.FormattingEnabled = true;
			this.lstCases.Location = new System.Drawing.Point(11, 24);
			this.lstCases.Name = "lstCases";
			this.lstCases.Size = new System.Drawing.Size(179, 563);
			this.lstCases.TabIndex = 0;
			this.lstCases.SelectedIndexChanged += new System.EventHandler(this.lstCases_SelectedIndexChanged);
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCode.BackColor = System.Drawing.Color.White;
			this.txtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtCode.ForeColor = System.Drawing.Color.Black;
			this.txtCode.Location = new System.Drawing.Point(47, 22);
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(1020, 55);
			this.txtCode.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(2, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(570, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Paste a code from the game\'s Export Dev Mode Edits popup and click Import to load" +
    " those lines into the character\'s file.";
			// 
			// LineImportControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdImport);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.grpImports);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.label1);
			this.Name = "LineImportControl";
			this.Size = new System.Drawing.Size(1157, 680);
			this.grpImports.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedButton cmdImport;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedGroupBox grpImports;
		private CaseControl caseEditor;
		private Desktop.Skinning.SkinnedListBox lstCases;
		private Desktop.Skinning.SkinnedTextBox txtCode;
		private Desktop.Skinning.SkinnedLabel label1;
	}
}

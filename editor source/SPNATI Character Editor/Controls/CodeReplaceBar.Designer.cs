namespace SPNATI_Character_Editor.Controls
{
	partial class CodeReplaceBar
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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cboSearch = new Desktop.Skinning.SkinnedComboBox();
			this.cmdReplace = new Desktop.Skinning.SkinnedButton();
			this.txtReplace = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.txtFind = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.cmdClose = new Desktop.Skinning.SkinnedIcon();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.SuspendLayout();
			// 
			// cboSearch
			// 
			this.cboSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboSearch.BackColor = System.Drawing.Color.White;
			this.cboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSearch.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboSearch.KeyMember = null;
			this.cboSearch.Location = new System.Drawing.Point(514, 1);
			this.cboSearch.Name = "cboSearch";
			this.cboSearch.SelectedIndex = -1;
			this.cboSearch.SelectedItem = null;
			this.cboSearch.Size = new System.Drawing.Size(104, 21);
			this.cboSearch.Sorted = false;
			this.cboSearch.TabIndex = 4;
			this.cboSearch.Text = "cboType";
			// 
			// cmdReplace
			// 
			this.cmdReplace.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdReplace.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdReplace.Flat = false;
			this.cmdReplace.ForeColor = System.Drawing.Color.Blue;
			this.cmdReplace.Location = new System.Drawing.Point(624, 0);
			this.cmdReplace.Name = "cmdReplace";
			this.cmdReplace.Size = new System.Drawing.Size(106, 23);
			this.cmdReplace.TabIndex = 5;
			this.cmdReplace.Text = "Replace &All";
			this.cmdReplace.UseVisualStyleBackColor = true;
			this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
			// 
			// txtReplace
			// 
			this.txtReplace.BackColor = System.Drawing.Color.White;
			this.txtReplace.ForeColor = System.Drawing.Color.Black;
			this.txtReplace.Location = new System.Drawing.Point(304, 2);
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.Size = new System.Drawing.Size(188, 20);
			this.txtReplace.TabIndex = 3;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(231, 5);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(72, 13);
			this.skinnedLabel2.TabIndex = 2;
			this.skinnedLabel2.Text = "Replace with:";
			// 
			// txtFind
			// 
			this.txtFind.BackColor = System.Drawing.Color.White;
			this.txtFind.ForeColor = System.Drawing.Color.Black;
			this.txtFind.Location = new System.Drawing.Point(37, 2);
			this.txtFind.Name = "txtFind";
			this.txtFind.Size = new System.Drawing.Size(188, 20);
			this.txtFind.TabIndex = 1;
			this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(3, 5);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(31, 13);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Text:";
			// 
			// cmdClose
			// 
			this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdClose.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdClose.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdClose.Flat = false;
			this.cmdClose.Image = global::SPNATI_Character_Editor.Properties.Resources.Delete;
			this.cmdClose.Location = new System.Drawing.Point(775, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(21, 23);
			this.cmdClose.TabIndex = 6;
			this.cmdClose.Text = "Close";
			this.toolTip1.SetToolTip(this.cmdClose, "Close Replace");
			this.cmdClose.UseVisualStyleBackColor = true;
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(498, 5);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(15, 13);
			this.skinnedLabel3.TabIndex = 11;
			this.skinnedLabel3.Text = "in";
			// 
			// CodeReplaceBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboSearch);
			this.Controls.Add(this.cmdReplace);
			this.Controls.Add(this.txtReplace);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.txtFind);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.skinnedLabel3);
			this.Name = "CodeReplaceBar";
			this.Size = new System.Drawing.Size(799, 23);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedIcon cmdClose;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedTextBox txtFind;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedTextBox txtReplace;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedButton cmdReplace;
		private Desktop.Skinning.SkinnedComboBox cboSearch;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
	}
}

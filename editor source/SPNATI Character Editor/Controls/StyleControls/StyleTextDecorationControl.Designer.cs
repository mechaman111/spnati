namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleTextDecorationControl
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
			this.chkUnder = new Desktop.Skinning.SkinnedCheckBox();
			this.chkOver = new Desktop.Skinning.SkinnedCheckBox();
			this.chkStrike = new Desktop.Skinning.SkinnedCheckBox();
			this.cboStyle = new Desktop.Skinning.SkinnedComboBox();
			this.fldColor = new Desktop.CommonControls.ColorField();
			this.SuspendLayout();
			// 
			// chkUnder
			// 
			this.chkUnder.AutoSize = true;
			this.chkUnder.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkUnder.Location = new System.Drawing.Point(3, 2);
			this.chkUnder.Name = "chkUnder";
			this.chkUnder.Size = new System.Drawing.Size(71, 17);
			this.chkUnder.TabIndex = 0;
			this.chkUnder.Text = "Underline";
			this.chkUnder.UseVisualStyleBackColor = true;
			// 
			// chkOver
			// 
			this.chkOver.AutoSize = true;
			this.chkOver.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkOver.Location = new System.Drawing.Point(69, 2);
			this.chkOver.Name = "chkOver";
			this.chkOver.Size = new System.Drawing.Size(65, 17);
			this.chkOver.TabIndex = 1;
			this.chkOver.Text = "Overline";
			this.chkOver.UseVisualStyleBackColor = true;
			// 
			// chkStrike
			// 
			this.chkStrike.AutoSize = true;
			this.chkStrike.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkStrike.Location = new System.Drawing.Point(128, 2);
			this.chkStrike.Name = "chkStrike";
			this.chkStrike.Size = new System.Drawing.Size(89, 17);
			this.chkStrike.TabIndex = 2;
			this.chkStrike.Text = "Strikethrough";
			this.chkStrike.UseVisualStyleBackColor = true;
			// 
			// cboStyle
			// 
			this.cboStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboStyle.BackColor = System.Drawing.Color.White;
			this.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStyle.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboStyle.Location = new System.Drawing.Point(213, 0);
			this.cboStyle.Name = "cboStyle";
			this.cboStyle.SelectedIndex = -1;
			this.cboStyle.SelectedItem = null;
			this.cboStyle.Size = new System.Drawing.Size(79, 21);
			this.cboStyle.Sorted = false;
			this.cboStyle.TabIndex = 3;
			this.cboStyle.Text = "skinnedComboBox1";
			this.cboStyle.Visible = false;
			// 
			// fldColor
			// 
			this.fldColor.BackColor = System.Drawing.SystemColors.Control;
			this.fldColor.Color = System.Drawing.SystemColors.Control;
			this.fldColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.fldColor.Location = new System.Drawing.Point(296, 0);
			this.fldColor.Name = "fldColor";
			this.fldColor.Size = new System.Drawing.Size(48, 21);
			this.fldColor.TabIndex = 4;
			this.fldColor.UseVisualStyleBackColor = true;
			this.fldColor.Visible = false;
			// 
			// StyleTextDecorationControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fldColor);
			this.Controls.Add(this.cboStyle);
			this.Controls.Add(this.chkStrike);
			this.Controls.Add(this.chkOver);
			this.Controls.Add(this.chkUnder);
			this.Name = "StyleTextDecorationControl";
			this.Size = new System.Drawing.Size(394, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedCheckBox chkUnder;
		private Desktop.Skinning.SkinnedCheckBox chkOver;
		private Desktop.Skinning.SkinnedCheckBox chkStrike;
		private Desktop.Skinning.SkinnedComboBox cboStyle;
		private Desktop.CommonControls.ColorField fldColor;
	}
}

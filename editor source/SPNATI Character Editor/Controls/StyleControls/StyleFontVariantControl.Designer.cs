namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleFontVariantControl
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
			this.cboVariant = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// cboVariant
			// 
			this.cboVariant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboVariant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboVariant.BackColor = System.Drawing.Color.White;
			this.cboVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboVariant.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboVariant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboVariant.Location = new System.Drawing.Point(3, 0);
			this.cboVariant.Name = "cboVariant";
			this.cboVariant.SelectedIndex = -1;
			this.cboVariant.SelectedItem = null;
			this.cboVariant.Size = new System.Drawing.Size(139, 21);
			this.cboVariant.Sorted = false;
			this.cboVariant.TabIndex = 0;
			// 
			// StyleFontVariantControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboVariant);
			this.Name = "StyleFontVariantControl";
			this.Size = new System.Drawing.Size(163, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboVariant;
	}
}

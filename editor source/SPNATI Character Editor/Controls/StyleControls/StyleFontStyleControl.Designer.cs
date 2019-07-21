namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleFontStyleControl
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
			this.radNormal = new Desktop.Skinning.SkinnedRadioButton();
			this.radItalic = new Desktop.Skinning.SkinnedRadioButton();
			this.SuspendLayout();
			// 
			// radNormal
			// 
			this.radNormal.AutoSize = true;
			this.radNormal.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radNormal.Location = new System.Drawing.Point(3, 1);
			this.radNormal.Name = "radNormal";
			this.radNormal.Size = new System.Drawing.Size(58, 17);
			this.radNormal.TabIndex = 0;
			this.radNormal.TabStop = true;
			this.radNormal.Text = "Normal";
			this.radNormal.UseVisualStyleBackColor = true;
			// 
			// radItalic
			// 
			this.radItalic.AutoSize = true;
			this.radItalic.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radItalic.Location = new System.Drawing.Point(67, 1);
			this.radItalic.Name = "radItalic";
			this.radItalic.Size = new System.Drawing.Size(47, 17);
			this.radItalic.TabIndex = 1;
			this.radItalic.TabStop = true;
			this.radItalic.Text = "Italic";
			this.radItalic.UseVisualStyleBackColor = true;
			// 
			// StyleFontStyleControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.radItalic);
			this.Controls.Add(this.radNormal);
			this.Name = "StyleFontStyleControl";
			this.Size = new System.Drawing.Size(205, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedRadioButton radNormal;
		private Desktop.Skinning.SkinnedRadioButton radItalic;
	}
}

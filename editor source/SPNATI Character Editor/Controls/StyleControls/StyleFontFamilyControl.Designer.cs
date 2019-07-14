namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleFontFamilyControl
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
			this.lblFont = new Desktop.Skinning.SkinnedLinkLabel();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.SuspendLayout();
			// 
			// lblFont
			// 
			this.lblFont.AutoSize = true;
			this.lblFont.Location = new System.Drawing.Point(3, 4);
			this.lblFont.Name = "lblFont";
			this.lblFont.Size = new System.Drawing.Size(28, 13);
			this.lblFont.TabIndex = 0;
			this.lblFont.TabStop = true;
			this.lblFont.Text = "Font";
			this.lblFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFont_LinkClicked);
			// 
			// fontDialog1
			// 
			this.fontDialog1.ShowEffects = false;
			// 
			// StyleFontFamilyControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblFont);
			this.Name = "StyleFontFamilyControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLinkLabel lblFont;
		private System.Windows.Forms.FontDialog fontDialog1;
	}
}

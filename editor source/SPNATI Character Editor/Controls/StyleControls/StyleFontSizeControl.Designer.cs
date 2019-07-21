namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleFontSizeControl
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
			this.valSize = new Desktop.Skinning.SkinnedNumericUpDown();
			this.radPt = new Desktop.Skinning.SkinnedRadioButton();
			this.radPx = new Desktop.Skinning.SkinnedRadioButton();
			((System.ComponentModel.ISupportInitialize)(this.valSize)).BeginInit();
			this.SuspendLayout();
			// 
			// valSize
			// 
			this.valSize.BackColor = System.Drawing.Color.White;
			this.valSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valSize.ForeColor = System.Drawing.Color.Black;
			this.valSize.Location = new System.Drawing.Point(3, 1);
			this.valSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valSize.Name = "valSize";
			this.valSize.Size = new System.Drawing.Size(48, 20);
			this.valSize.TabIndex = 0;
			this.valSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// radPt
			// 
			this.radPt.AutoSize = true;
			this.radPt.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radPt.Location = new System.Drawing.Point(54, 2);
			this.radPt.Name = "radPt";
			this.radPt.Size = new System.Drawing.Size(34, 17);
			this.radPt.TabIndex = 1;
			this.radPt.TabStop = true;
			this.radPt.Text = "pt";
			this.radPt.UseVisualStyleBackColor = true;
			// 
			// radPx
			// 
			this.radPx.AutoSize = true;
			this.radPx.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radPx.Location = new System.Drawing.Point(91, 2);
			this.radPx.Name = "radPx";
			this.radPx.Size = new System.Drawing.Size(36, 17);
			this.radPx.TabIndex = 2;
			this.radPx.TabStop = true;
			this.radPx.Text = "px";
			this.radPx.UseVisualStyleBackColor = true;
			// 
			// StyleFontSizeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.radPx);
			this.Controls.Add(this.radPt);
			this.Controls.Add(this.valSize);
			this.Name = "StyleFontSizeControl";
			this.Size = new System.Drawing.Size(150, 21);
			((System.ComponentModel.ISupportInitialize)(this.valSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedNumericUpDown valSize;
		private Desktop.Skinning.SkinnedRadioButton radPt;
		private Desktop.Skinning.SkinnedRadioButton radPx;
	}
}

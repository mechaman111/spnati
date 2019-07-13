namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleTextShadowControl
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
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.valHoriz = new Desktop.Skinning.SkinnedNumericUpDown();
			this.valVert = new Desktop.Skinning.SkinnedNumericUpDown();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.valBlur = new Desktop.Skinning.SkinnedNumericUpDown();
			this.fldColor = new Desktop.CommonControls.ColorField();
			((System.ComponentModel.ISupportInitialize)(this.valHoriz)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valVert)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valBlur)).BeginInit();
			this.SuspendLayout();
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel1.Location = new System.Drawing.Point(3, 3);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(38, 13);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Offset:";
			// 
			// valHoriz
			// 
			this.valHoriz.BackColor = System.Drawing.Color.White;
			this.valHoriz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valHoriz.ForeColor = System.Drawing.Color.Black;
			this.valHoriz.Location = new System.Drawing.Point(41, 1);
			this.valHoriz.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
			this.valHoriz.Name = "valHoriz";
			this.valHoriz.Size = new System.Drawing.Size(37, 20);
			this.valHoriz.TabIndex = 1;
			// 
			// valVert
			// 
			this.valVert.BackColor = System.Drawing.Color.White;
			this.valVert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valVert.ForeColor = System.Drawing.Color.Black;
			this.valVert.Location = new System.Drawing.Point(84, 1);
			this.valVert.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
			this.valVert.Name = "valVert";
			this.valVert.Size = new System.Drawing.Size(37, 20);
			this.valVert.TabIndex = 2;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel2.Location = new System.Drawing.Point(124, 3);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(28, 13);
			this.skinnedLabel2.TabIndex = 3;
			this.skinnedLabel2.Text = "Blur:";
			// 
			// valBlur
			// 
			this.valBlur.BackColor = System.Drawing.Color.White;
			this.valBlur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valBlur.ForeColor = System.Drawing.Color.Black;
			this.valBlur.Location = new System.Drawing.Point(152, 1);
			this.valBlur.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
			this.valBlur.Name = "valBlur";
			this.valBlur.Size = new System.Drawing.Size(37, 20);
			this.valBlur.TabIndex = 4;
			// 
			// fldColor
			// 
			this.fldColor.BackColor = System.Drawing.SystemColors.Control;
			this.fldColor.Color = System.Drawing.SystemColors.Control;
			this.fldColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.fldColor.Location = new System.Drawing.Point(195, 0);
			this.fldColor.Name = "fldColor";
			this.fldColor.Size = new System.Drawing.Size(43, 21);
			this.fldColor.TabIndex = 5;
			this.fldColor.UseVisualStyleBackColor = true;
			// 
			// StyleTextShadowControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fldColor);
			this.Controls.Add(this.valBlur);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.valVert);
			this.Controls.Add(this.valHoriz);
			this.Controls.Add(this.skinnedLabel1);
			this.Name = "StyleTextShadowControl";
			this.Size = new System.Drawing.Size(369, 21);
			((System.ComponentModel.ISupportInitialize)(this.valHoriz)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valVert)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valBlur)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedNumericUpDown valHoriz;
		private Desktop.Skinning.SkinnedNumericUpDown valVert;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedNumericUpDown valBlur;
		private Desktop.CommonControls.ColorField fldColor;
	}
}

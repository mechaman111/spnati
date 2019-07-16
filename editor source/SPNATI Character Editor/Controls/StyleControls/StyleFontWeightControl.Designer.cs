namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleFontWeightControl
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
			this.radBold = new Desktop.Skinning.SkinnedRadioButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.valWeight = new Desktop.Skinning.SkinnedNumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valWeight)).BeginInit();
			this.SuspendLayout();
			// 
			// radNormal
			// 
			this.radNormal.AutoSize = true;
			this.radNormal.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radNormal.Location = new System.Drawing.Point(3, 2);
			this.radNormal.Name = "radNormal";
			this.radNormal.Size = new System.Drawing.Size(58, 17);
			this.radNormal.TabIndex = 0;
			this.radNormal.TabStop = true;
			this.radNormal.Text = "Normal";
			this.radNormal.UseVisualStyleBackColor = true;
			// 
			// radBold
			// 
			this.radBold.AutoSize = true;
			this.radBold.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radBold.Location = new System.Drawing.Point(67, 2);
			this.radBold.Name = "radBold";
			this.radBold.Size = new System.Drawing.Size(46, 17);
			this.radBold.TabIndex = 1;
			this.radBold.TabStop = true;
			this.radBold.Text = "Bold";
			this.radBold.UseVisualStyleBackColor = true;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel1.Location = new System.Drawing.Point(120, 4);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(44, 13);
			this.skinnedLabel1.TabIndex = 2;
			this.skinnedLabel1.Text = "Weight:";
			// 
			// valWeight
			// 
			this.valWeight.BackColor = System.Drawing.Color.White;
			this.valWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valWeight.ForeColor = System.Drawing.Color.Black;
			this.valWeight.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.valWeight.Location = new System.Drawing.Point(170, 1);
			this.valWeight.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
			this.valWeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.valWeight.Name = "valWeight";
			this.valWeight.Size = new System.Drawing.Size(57, 20);
			this.valWeight.TabIndex = 3;
			this.valWeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// StyleFontWeightControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valWeight);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.radBold);
			this.Controls.Add(this.radNormal);
			this.Name = "StyleFontWeightControl";
			this.Size = new System.Drawing.Size(263, 21);
			((System.ComponentModel.ISupportInitialize)(this.valWeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedRadioButton radNormal;
		private Desktop.Skinning.SkinnedRadioButton radBold;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedNumericUpDown valWeight;
	}
}

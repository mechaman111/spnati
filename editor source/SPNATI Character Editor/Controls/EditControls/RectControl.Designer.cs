namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class RectControl
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
			this.lblLeft = new Desktop.Skinning.SkinnedLabel();
			this.valLeft = new Desktop.Skinning.SkinnedNumericUpDown();
			this.valTop = new Desktop.Skinning.SkinnedNumericUpDown();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.valWidth = new Desktop.Skinning.SkinnedNumericUpDown();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.valHeight = new Desktop.Skinning.SkinnedNumericUpDown();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			((System.ComponentModel.ISupportInitialize)(this.valLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// lblLeft
			// 
			this.lblLeft.AutoSize = true;
			this.lblLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblLeft.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblLeft.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblLeft.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblLeft.Location = new System.Drawing.Point(4, 2);
			this.lblLeft.Name = "lblLeft";
			this.lblLeft.Size = new System.Drawing.Size(28, 13);
			this.lblLeft.TabIndex = 0;
			this.lblLeft.Text = "Left:";
			// 
			// valLeft
			// 
			this.valLeft.BackColor = System.Drawing.Color.White;
			this.valLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valLeft.ForeColor = System.Drawing.Color.Black;
			this.valLeft.Location = new System.Drawing.Point(33, 0);
			this.valLeft.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.valLeft.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
			this.valLeft.Name = "valLeft";
			this.valLeft.Size = new System.Drawing.Size(52, 20);
			this.valLeft.TabIndex = 1;
			// 
			// valTop
			// 
			this.valTop.BackColor = System.Drawing.Color.White;
			this.valTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valTop.ForeColor = System.Drawing.Color.Black;
			this.valTop.Location = new System.Drawing.Point(119, 0);
			this.valTop.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.valTop.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
			this.valTop.Name = "valTop";
			this.valTop.Size = new System.Drawing.Size(52, 20);
			this.valTop.TabIndex = 3;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(88, 2);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(29, 13);
			this.skinnedLabel1.TabIndex = 2;
			this.skinnedLabel1.Text = "Top:";
			// 
			// valWidth
			// 
			this.valWidth.BackColor = System.Drawing.Color.White;
			this.valWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valWidth.ForeColor = System.Drawing.Color.Black;
			this.valWidth.Location = new System.Drawing.Point(214, 0);
			this.valWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.valWidth.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
			this.valWidth.Name = "valWidth";
			this.valWidth.Size = new System.Drawing.Size(52, 20);
			this.valWidth.TabIndex = 5;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(174, 2);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(38, 13);
			this.skinnedLabel2.TabIndex = 4;
			this.skinnedLabel2.Text = "Width:";
			// 
			// valHeight
			// 
			this.valHeight.BackColor = System.Drawing.Color.White;
			this.valHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valHeight.ForeColor = System.Drawing.Color.Black;
			this.valHeight.Location = new System.Drawing.Point(313, 0);
			this.valHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.valHeight.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
			this.valHeight.Name = "valHeight";
			this.valHeight.Size = new System.Drawing.Size(52, 20);
			this.valHeight.TabIndex = 7;
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(269, 2);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(41, 13);
			this.skinnedLabel3.TabIndex = 6;
			this.skinnedLabel3.Text = "Height:";
			// 
			// RectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valHeight);
			this.Controls.Add(this.skinnedLabel3);
			this.Controls.Add(this.valWidth);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.valTop);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.valLeft);
			this.Controls.Add(this.lblLeft);
			this.Name = "RectControl";
			this.Size = new System.Drawing.Size(654, 20);
			((System.ComponentModel.ISupportInitialize)(this.valLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valHeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblLeft;
		private Desktop.Skinning.SkinnedNumericUpDown valLeft;
		private Desktop.Skinning.SkinnedNumericUpDown valTop;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedNumericUpDown valWidth;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedNumericUpDown valHeight;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
	}
}

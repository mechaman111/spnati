namespace SPNATI_Character_Editor
{
	partial class NumericRangeControl
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
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.valFrom = new Desktop.Skinning.SkinnedNumericUpDown();
			this.valTo = new Desktop.Skinning.SkinnedNumericUpDown();
			this.chkUpper = new Desktop.Skinning.SkinnedCheckBox();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(83, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(19, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "to:";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(-2, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "From:";
			// 
			// valFrom
			// 
			this.valFrom.BackColor = System.Drawing.Color.White;
			this.valFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valFrom.ForeColor = System.Drawing.Color.Black;
			this.valFrom.Location = new System.Drawing.Point(37, 1);
			this.valFrom.Name = "valFrom";
			this.valFrom.Size = new System.Drawing.Size(43, 20);
			this.valFrom.TabIndex = 7;
			// 
			// valTo
			// 
			this.valTo.BackColor = System.Drawing.Color.White;
			this.valTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valTo.ForeColor = System.Drawing.Color.Black;
			this.valTo.Location = new System.Drawing.Point(106, 1);
			this.valTo.Name = "valTo";
			this.valTo.Size = new System.Drawing.Size(41, 20);
			this.valTo.TabIndex = 8;
			// 
			// chkUpper
			// 
			this.chkUpper.AutoSize = true;
			this.chkUpper.Location = new System.Drawing.Point(153, 2);
			this.chkUpper.Name = "chkUpper";
			this.chkUpper.Size = new System.Drawing.Size(103, 17);
			this.chkUpper.TabIndex = 9;
			this.chkUpper.Text = "No upper bound";
			this.chkUpper.UseVisualStyleBackColor = true;
			// 
			// NumericRangeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkUpper);
			this.Controls.Add(this.valTo);
			this.Controls.Add(this.valFrom);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "NumericRangeControl";
			this.Size = new System.Drawing.Size(576, 21);
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedNumericUpDown valFrom;
		private Desktop.Skinning.SkinnedNumericUpDown valTo;
		private Desktop.Skinning.SkinnedCheckBox chkUpper;
	}
}

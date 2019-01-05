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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.valFrom = new System.Windows.Forms.NumericUpDown();
			this.valTo = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
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
			this.label1.Location = new System.Drawing.Point(-2, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "From:";
			// 
			// valFrom
			// 
			this.valFrom.Location = new System.Drawing.Point(37, 1);
			this.valFrom.Name = "valFrom";
			this.valFrom.Size = new System.Drawing.Size(43, 20);
			this.valFrom.TabIndex = 7;
			// 
			// valTo
			// 
			this.valTo.Location = new System.Drawing.Point(106, 1);
			this.valTo.Name = "valTo";
			this.valTo.Size = new System.Drawing.Size(41, 20);
			this.valTo.TabIndex = 8;
			// 
			// NumericRangeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown valFrom;
		private System.Windows.Forms.NumericUpDown valTo;
	}
}

namespace Desktop.CommonControls
{
	partial class Slider
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
			this.lblLabel = new System.Windows.Forms.Label();
			this.trackbar = new System.Windows.Forms.TrackBar();
			this.lblValue = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackbar)).BeginInit();
			this.SuspendLayout();
			// 
			// lblLabel
			// 
			this.lblLabel.AutoSize = true;
			this.lblLabel.Location = new System.Drawing.Point(3, 8);
			this.lblLabel.Name = "lblLabel";
			this.lblLabel.Size = new System.Drawing.Size(36, 13);
			this.lblLabel.TabIndex = 0;
			this.lblLabel.Text = "Label:";
			// 
			// trackbar
			// 
			this.trackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackbar.LargeChange = 10;
			this.trackbar.Location = new System.Drawing.Point(72, 3);
			this.trackbar.Maximum = 100;
			this.trackbar.Name = "trackbar";
			this.trackbar.Size = new System.Drawing.Size(324, 45);
			this.trackbar.TabIndex = 1;
			this.trackbar.TickFrequency = 10;
			this.trackbar.ValueChanged += new System.EventHandler(this.trackbar_ValueChanged);
			// 
			// lblValue
			// 
			this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblValue.AutoSize = true;
			this.lblValue.Location = new System.Drawing.Point(396, 11);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(13, 13);
			this.lblValue.TabIndex = 2;
			this.lblValue.Text = "0";
			// 
			// Slider
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblValue);
			this.Controls.Add(this.trackbar);
			this.Controls.Add(this.lblLabel);
			this.Name = "Slider";
			this.Size = new System.Drawing.Size(424, 37);
			((System.ComponentModel.ISupportInitialize)(this.trackbar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblLabel;
		private System.Windows.Forms.TrackBar trackbar;
		private System.Windows.Forms.Label lblValue;
	}
}

namespace SPNATI_Character_Editor.Controls
{
	partial class PartTransparencySlider
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
			this.lblName = new Desktop.Skinning.SkinnedLabel();
			this.track = new Desktop.Skinning.SkinnedSlider();
			this.valValue = new Desktop.Skinning.SkinnedNumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.track)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valValue)).BeginInit();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblName.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblName.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblName.Location = new System.Drawing.Point(3, 7);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(58, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Part name:";
			// 
			// track
			// 
			this.track.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.track.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.track.Increment = 10;
			this.track.Location = new System.Drawing.Point(110, -1);
			this.track.Maximum = 100;
			this.track.Minimum = 0;
			this.track.Name = "track";
			this.track.Size = new System.Drawing.Size(144, 31);
			this.track.TabIndex = 1;
			this.track.TickFrequency = 25;
			this.track.Value = 0;
			// 
			// valValue
			// 
			this.valValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valValue.BackColor = System.Drawing.Color.White;
			this.valValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valValue.ForeColor = System.Drawing.Color.Black;
			this.valValue.Location = new System.Drawing.Point(260, 4);
			this.valValue.Name = "valValue";
			this.valValue.Size = new System.Drawing.Size(47, 20);
			this.valValue.TabIndex = 2;
			// 
			// PartTransparencySlider
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valValue);
			this.Controls.Add(this.track);
			this.Controls.Add(this.lblName);
			this.Name = "PartTransparencySlider";
			this.Size = new System.Drawing.Size(310, 30);
			((System.ComponentModel.ISupportInitialize)(this.track)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblName;
		private Desktop.Skinning.SkinnedSlider track;
		private Desktop.Skinning.SkinnedNumericUpDown valValue;
	}
}

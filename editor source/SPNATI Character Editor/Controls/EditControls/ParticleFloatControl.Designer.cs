namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class ParticleFloatControl
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
			this.valFrom = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.valTo = new Desktop.Skinning.SkinnedNumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).BeginInit();
			this.SuspendLayout();
			// 
			// valFrom
			// 
			this.valFrom.BackColor = System.Drawing.Color.White;
			this.valFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valFrom.ForeColor = System.Drawing.Color.Black;
			this.valFrom.Location = new System.Drawing.Point(0, 0);
			this.valFrom.Name = "valFrom";
			this.valFrom.Size = new System.Drawing.Size(53, 20);
			this.valFrom.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(56, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "to:";
			// 
			// valTo
			// 
			this.valTo.BackColor = System.Drawing.Color.White;
			this.valTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valTo.ForeColor = System.Drawing.Color.Black;
			this.valTo.Location = new System.Drawing.Point(77, 0);
			this.valTo.Name = "valTo";
			this.valTo.Size = new System.Drawing.Size(53, 20);
			this.valTo.TabIndex = 2;
			// 
			// ParticleFloatControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valTo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.valFrom);
			this.Name = "ParticleFloatControl";
			this.Size = new System.Drawing.Size(543, 20);
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedNumericUpDown valFrom;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedNumericUpDown valTo;
	}
}

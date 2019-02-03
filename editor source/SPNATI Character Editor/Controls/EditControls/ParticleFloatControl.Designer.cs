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
			this.valFrom = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.valTo = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).BeginInit();
			this.SuspendLayout();
			// 
			// valFrom
			// 
			this.valFrom.Location = new System.Drawing.Point(0, 0);
			this.valFrom.Name = "valFrom";
			this.valFrom.Size = new System.Drawing.Size(53, 20);
			this.valFrom.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(56, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "to:";
			// 
			// valTo
			// 
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

		private System.Windows.Forms.NumericUpDown valFrom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown valTo;
	}
}

namespace Desktop.CommonControls.PropertyControls
{
	partial class NumericControl
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
			this.valValue = new Desktop.Skinning.SkinnedNumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valValue)).BeginInit();
			this.SuspendLayout();
			// 
			// valValue
			// 
			this.valValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.valValue.Location = new System.Drawing.Point(0, 0);
			this.valValue.Name = "valValue";
			this.valValue.Size = new System.Drawing.Size(576, 20);
			this.valValue.TabIndex = 1;
			// 
			// NumericControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valValue);
			this.Name = "NumericControl";
			this.Size = new System.Drawing.Size(576, 20);
			((System.ComponentModel.ISupportInitialize)(this.valValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedNumericUpDown valValue;
	}
}

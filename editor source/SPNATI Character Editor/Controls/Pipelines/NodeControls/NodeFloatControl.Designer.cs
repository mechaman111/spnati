namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodeFloatControl
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
			this.valValue.BackColor = System.Drawing.Color.White;
			this.valValue.DecimalPlaces = 2;
			this.valValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.valValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valValue.ForeColor = System.Drawing.Color.Black;
			this.valValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.valValue.Location = new System.Drawing.Point(0, 0);
			this.valValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
			this.valValue.Name = "valValue";
			this.valValue.Size = new System.Drawing.Size(150, 20);
			this.valValue.TabIndex = 1;
			// 
			// NodeFloatControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valValue);
			this.Name = "NodeFloatControl";
			this.Size = new System.Drawing.Size(150, 21);
			((System.ComponentModel.ISupportInitialize)(this.valValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedNumericUpDown valValue;
	}
}

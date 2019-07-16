namespace Desktop.CommonControls.PropertyControls
{
	partial class BooleanControl
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
			this.chkEnabled = new Desktop.Skinning.SkinnedCheckBox();
			this.SuspendLayout();
			// 
			// chkEnabled
			// 
			this.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkEnabled.AutoSize = true;
			this.chkEnabled.Location = new System.Drawing.Point(0, 4);
			this.chkEnabled.Name = "chkEnabled";
			this.chkEnabled.Size = new System.Drawing.Size(15, 14);
			this.chkEnabled.TabIndex = 0;
			this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkEnabled.UseVisualStyleBackColor = true;
			// 
			// BooleanControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkEnabled);
			this.Name = "BooleanControl";
			this.Size = new System.Drawing.Size(150, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedCheckBox chkEnabled;
	}
}

namespace SPNATI_Character_Editor
{
	partial class OneShotControl
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
			this.chkOneShot = new Desktop.Skinning.SkinnedCheckBox();
			this.SuspendLayout();
			// 
			// chkOneShot
			// 
			this.chkOneShot.AutoSize = true;
			this.chkOneShot.Location = new System.Drawing.Point(0, 4);
			this.chkOneShot.Name = "chkOneShot";
			this.chkOneShot.Size = new System.Drawing.Size(15, 14);
			this.chkOneShot.TabIndex = 0;
			this.chkOneShot.UseVisualStyleBackColor = true;
			// 
			// OneShotControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkOneShot);
			this.Name = "OneShotControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedCheckBox chkOneShot;
	}
}

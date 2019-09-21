namespace SPNATI_Character_Editor.Activities
{
	partial class CharacterLoader
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
			this.lblProgress = new Desktop.Skinning.SkinnedLabel();
			this.progressBar = new Desktop.Skinning.SkinnedProgressBar();
			this.SuspendLayout();
			// 
			// lblProgress
			// 
			this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblProgress.ForeColor = System.Drawing.Color.Blue;
			this.lblProgress.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblProgress.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblProgress.Location = new System.Drawing.Point(59, 280);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(856, 50);
			this.lblProgress.TabIndex = 3;
			this.lblProgress.Text = "Loading...";
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(345, 333);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(276, 23);
			this.progressBar.TabIndex = 2;
			// 
			// CharacterLoader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.progressBar);
			this.Name = "CharacterLoader";
			this.Size = new System.Drawing.Size(975, 637);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblProgress;
		private Desktop.Skinning.SkinnedProgressBar progressBar;
	}
}

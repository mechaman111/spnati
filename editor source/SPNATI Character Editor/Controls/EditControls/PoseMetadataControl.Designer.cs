namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class PoseMetadataControl
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
			this.cmdSet = new Desktop.Skinning.SkinnedButton();
			this.SuspendLayout();
			// 
			// cmdSet
			// 
			this.cmdSet.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdSet.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cmdSet.Flat = false;
			this.cmdSet.Location = new System.Drawing.Point(0, 0);
			this.cmdSet.Name = "cmdSet";
			this.cmdSet.Size = new System.Drawing.Size(75, 21);
			this.cmdSet.TabIndex = 0;
			this.cmdSet.Text = "Set...";
			this.cmdSet.UseVisualStyleBackColor = true;
			this.cmdSet.Click += new System.EventHandler(this.cmdSet_Click);
			// 
			// PoseMetadataControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdSet);
			this.Name = "PoseMetadataControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedButton cmdSet;
	}
}

namespace SPNATI_Character_Editor.Controls
{
	partial class MarkerOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerOptions));
			this.chkPersistent = new Desktop.Skinning.SkinnedCheckBox();
			this.gridMarkers = new SPNATI_Character_Editor.Controls.MarkerControl();
			this.SuspendLayout();
			// 
			// chkPersistent
			// 
			this.chkPersistent.AutoSize = true;
			this.chkPersistent.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkPersistent.Location = new System.Drawing.Point(3, 141);
			this.chkPersistent.Name = "chkPersistent";
			this.chkPersistent.Size = new System.Drawing.Size(72, 17);
			this.chkPersistent.TabIndex = 3;
			this.chkPersistent.Text = "Persistent";
			this.chkPersistent.UseVisualStyleBackColor = true;
			// 
			// gridMarkers
			// 
			this.gridMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMarkers.Location = new System.Drawing.Point(0, 0);
			this.gridMarkers.Name = "gridMarkers";
			this.gridMarkers.ShowWhen = false;
			this.gridMarkers.Size = new System.Drawing.Size(470, 135);
			this.gridMarkers.TabIndex = 6;
			// 
			// MarkerOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridMarkers);
			this.Controls.Add(this.chkPersistent);
			this.Name = "MarkerOptions";
			this.Size = new System.Drawing.Size(470, 163);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Desktop.Skinning.SkinnedCheckBox chkPersistent;
		private MarkerControl gridMarkers;
	}
}

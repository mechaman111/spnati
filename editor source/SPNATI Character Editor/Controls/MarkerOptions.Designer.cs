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
			this.gridMarkers = new SPNATI_Character_Editor.Controls.MarkerControl();
			this.SuspendLayout();
			// 
			// gridMarkers
			// 
			this.gridMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMarkers.Location = new System.Drawing.Point(0, 0);
			this.gridMarkers.Name = "gridMarkers";
			this.gridMarkers.ShowWhen = false;
			this.gridMarkers.Size = new System.Drawing.Size(470, 160);
			this.gridMarkers.TabIndex = 6;
			// 
			// MarkerOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridMarkers);
			this.Name = "MarkerOptions";
			this.Size = new System.Drawing.Size(470, 163);
			this.ResumeLayout(false);

		}

		#endregion
		private MarkerControl gridMarkers;
	}
}

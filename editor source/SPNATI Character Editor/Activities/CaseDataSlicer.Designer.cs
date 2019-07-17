namespace SPNATI_Character_Editor.Activities
{
	partial class CaseDataSlicer
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
			this.ctlSlicer = new Desktop.CommonControls.DataSlicerControl();
			this.SuspendLayout();
			// 
			// ctlSlicer
			// 
			this.ctlSlicer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlSlicer.Location = new System.Drawing.Point(0, 0);
			this.ctlSlicer.Name = "ctlSlicer";
			this.ctlSlicer.Size = new System.Drawing.Size(999, 652);
			this.ctlSlicer.TabIndex = 0;
			// 
			// CaseDataSlicer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ctlSlicer);
			this.Name = "CaseDataSlicer";
			this.Size = new System.Drawing.Size(999, 652);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.DataSlicerControl ctlSlicer;
	}
}

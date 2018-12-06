namespace SPNATI_Character_Editor.Activities
{
	partial class MarkerEditor
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
			this.grid = new SPNATI_Character_Editor.Controls.MarkerGrid();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.AllowPrivate = true;
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.Location = new System.Drawing.Point(0, 0);
			this.grid.Name = "grid";
			this.grid.ReadOnly = false;
			this.grid.Size = new System.Drawing.Size(967, 682);
			this.grid.TabIndex = 0;
			// 
			// MarkerEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grid);
			this.Name = "MarkerEditor";
			this.Size = new System.Drawing.Size(967, 682);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.MarkerGrid grid;
	}
}

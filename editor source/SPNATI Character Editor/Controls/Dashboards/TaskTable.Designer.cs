namespace SPNATI_Character_Editor.Controls
{
	partial class TaskTable
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
			this.pnlRecords = new Desktop.CommonControls.DBPanel();
			this.SuspendLayout();
			// 
			// pnlRecords
			// 
			this.pnlRecords.AutoScroll = true;
			this.pnlRecords.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRecords.Location = new System.Drawing.Point(0, 0);
			this.pnlRecords.Name = "pnlRecords";
			this.pnlRecords.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlRecords.Size = new System.Drawing.Size(323, 172);
			this.pnlRecords.TabIndex = 0;
			this.pnlRecords.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// TaskTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlRecords);
			this.Name = "TaskTable";
			this.Size = new System.Drawing.Size(323, 172);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.DBPanel pnlRecords;
	}
}

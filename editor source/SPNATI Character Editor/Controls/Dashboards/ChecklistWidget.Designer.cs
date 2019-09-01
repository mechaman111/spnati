namespace SPNATI_Character_Editor.Controls.Dashboards
{
	partial class ChecklistWidget
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
			this.grpChecklist = new Desktop.Skinning.SkinnedGroupBox();
			this.tasks = new SPNATI_Character_Editor.Controls.TaskTable();
			this.grpChecklist.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpChecklist
			// 
			this.grpChecklist.BackColor = System.Drawing.Color.White;
			this.grpChecklist.Controls.Add(this.tasks);
			this.grpChecklist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpChecklist.Location = new System.Drawing.Point(0, 0);
			this.grpChecklist.Name = "grpChecklist";
			this.grpChecklist.Size = new System.Drawing.Size(366, 330);
			this.grpChecklist.TabIndex = 1;
			this.grpChecklist.TabStop = false;
			this.grpChecklist.Text = "Checklist";
			// 
			// tasks
			// 
			this.tasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tasks.Location = new System.Drawing.Point(6, 24);
			this.tasks.Name = "tasks";
			this.tasks.Size = new System.Drawing.Size(354, 300);
			this.tasks.TabIndex = 0;
			// 
			// ChecklistWidget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpChecklist);
			this.Name = "ChecklistWidget";
			this.Size = new System.Drawing.Size(366, 330);
			this.grpChecklist.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox grpChecklist;
		private TaskTable tasks;
	}
}

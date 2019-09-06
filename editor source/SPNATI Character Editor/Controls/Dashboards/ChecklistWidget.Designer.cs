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
			this.pnlGood = new Desktop.Skinning.SkinnedPanel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.tasks = new SPNATI_Character_Editor.Controls.TaskTable();
			this.grpChecklist.SuspendLayout();
			this.pnlGood.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpChecklist
			// 
			this.grpChecklist.BackColor = System.Drawing.Color.White;
			this.grpChecklist.Controls.Add(this.pnlGood);
			this.grpChecklist.Controls.Add(this.tasks);
			this.grpChecklist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpChecklist.Location = new System.Drawing.Point(0, 0);
			this.grpChecklist.Name = "grpChecklist";
			this.grpChecklist.Size = new System.Drawing.Size(366, 330);
			this.grpChecklist.TabIndex = 1;
			this.grpChecklist.TabStop = false;
			this.grpChecklist.Text = "Checklist";
			// 
			// pnlGood
			// 
			this.pnlGood.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlGood.Controls.Add(this.skinnedLabel2);
			this.pnlGood.Controls.Add(this.skinnedLabel1);
			this.pnlGood.Location = new System.Drawing.Point(6, 24);
			this.pnlGood.Name = "pnlGood";
			this.pnlGood.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.pnlGood.Size = new System.Drawing.Size(354, 300);
			this.pnlGood.TabIndex = 1;
			this.pnlGood.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlGood.Visible = false;
			this.pnlGood.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGood_Paint);
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(125, 260);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(107, 13);
			this.skinnedLabel2.TabIndex = 1;
			this.skinnedLabel2.Text = "No outstanding tasks";
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Segoe UI", 28F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.Green;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Finished;
			this.skinnedLabel1.Location = new System.Drawing.Point(75, 209);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(208, 51);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Way to Go!";
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
			this.pnlGood.ResumeLayout(false);
			this.pnlGood.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox grpChecklist;
		private TaskTable tasks;
		private Desktop.Skinning.SkinnedPanel pnlGood;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
	}
}

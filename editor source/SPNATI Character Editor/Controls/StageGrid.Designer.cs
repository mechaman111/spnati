namespace SPNATI_Character_Editor.Controls
{
	partial class StageGrid
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
			this.panel = new Desktop.CommonControls.SelectablePanel();
			this.lblTitle = new Desktop.Skinning.SkinnedLabel();
			this.chkSelectAll = new Desktop.Skinning.SkinnedCheckBox();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Controls.Add(this.lblTitle);
			this.panel.Controls.Add(this.chkSelectAll);
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.panel.Size = new System.Drawing.Size(307, 232);
			this.panel.TabIndex = 3;
			this.panel.TabSide = Desktop.Skinning.TabSide.None;
			this.panel.TabStop = true;
			this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
			this.panel.MouseLeave += new System.EventHandler(this.panel_MouseLeave);
			this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblTitle.ForeColor = System.Drawing.Color.Blue;
			this.lblTitle.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblTitle.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblTitle.Location = new System.Drawing.Point(-2, -3);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(55, 21);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Stages";
			this.lblTitle.Visible = false;
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkSelectAll.Location = new System.Drawing.Point(0, 19);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(15, 14);
			this.chkSelectAll.TabIndex = 1;
			this.chkSelectAll.UseVisualStyleBackColor = true;
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// StageGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.panel);
			this.Name = "StageGrid";
			this.Size = new System.Drawing.Size(307, 232);
			this.panel.ResumeLayout(false);
			this.panel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.SelectablePanel panel;
		private Desktop.Skinning.SkinnedCheckBox chkSelectAll;
		private Desktop.Skinning.SkinnedLabel lblTitle;
	}
}

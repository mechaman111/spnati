namespace SPNATI_Character_Editor.Controls
{
	partial class TagGrid
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
			this.components = new System.ComponentModel.Container();
			this.panel = new Desktop.CommonControls.SelectablePanel();
			this.lblGroup = new Desktop.Skinning.SkinnedLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.panel.Size = new System.Drawing.Size(307, 232);
			this.panel.TabIndex = 3;
			this.panel.TabStop = true;
			this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
			this.panel.MouseLeave += new System.EventHandler(this.panel_MouseLeave);
			this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
			// 
			// lblGroup
			// 
			this.lblGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGroup.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblGroup.Location = new System.Drawing.Point(3, 0);
			this.lblGroup.Name = "lblGroup";
			this.lblGroup.Size = new System.Drawing.Size(100, 66);
			this.lblGroup.TabIndex = 4;
			this.lblGroup.Text = "Group Name";
			this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TagGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.lblGroup);
			this.Controls.Add(this.panel);
			this.Name = "TagGrid";
			this.Size = new System.Drawing.Size(307, 232);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.SelectablePanel panel;
		private Desktop.Skinning.SkinnedLabel lblGroup;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

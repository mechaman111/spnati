namespace SPNATI_Character_Editor.Controls
{
	partial class StageImageGrid
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
			this.panel.TabSide = Desktop.Skinning.TabSide.None;
			this.panel.TabStop = true;
			this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
			this.panel.MouseLeave += new System.EventHandler(this.panel_MouseLeave);
			this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
			// 
			// StageImageGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.panel);
			this.Name = "StageImageGrid";
			this.Size = new System.Drawing.Size(307, 232);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.SelectablePanel panel;
	}
}

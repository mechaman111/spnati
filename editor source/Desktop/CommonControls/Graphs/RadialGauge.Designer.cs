namespace Desktop.CommonControls
{
	partial class RadialGauge
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

				_pen.Dispose();
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
			this.panel = new Desktop.CommonControls.DBPanel();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.MaximumSize = new System.Drawing.Size(0, 150);
			this.panel.Name = "panel";
			this.panel.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.panel.Size = new System.Drawing.Size(150, 100);
			this.panel.TabIndex = 0;
			this.panel.TabSide = Desktop.Skinning.TabSide.None;
			this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.RadialGauge_Paint);
			// 
			// RadialGauge
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel);
			this.Name = "RadialGauge";
			this.Size = new System.Drawing.Size(150, 100);
			this.Load += new System.EventHandler(this.RadialGauge_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private DBPanel panel;
	}
}

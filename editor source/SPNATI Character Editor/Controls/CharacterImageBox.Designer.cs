namespace SPNATI_Character_Editor.Controls
{
	partial class CharacterImageBox
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
			this.canvas = new Desktop.CommonControls.DBPanel();
			this.tmrTick = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.canvas.Size = new System.Drawing.Size(197, 493);
			this.canvas.TabIndex = 1;
			this.canvas.TabSide = Desktop.Skinning.TabSide.None;
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
			// 
			// tmrTick
			// 
			this.tmrTick.Interval = 30;
			this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
			// 
			// CharacterImageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.canvas);
			this.Name = "CharacterImageBox";
			this.Size = new System.Drawing.Size(197, 493);
			this.Resize += new System.EventHandler(this.CharacterImageBox_Resize);
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.CommonControls.DBPanel canvas;
		private System.Windows.Forms.Timer tmrTick;
	}
}

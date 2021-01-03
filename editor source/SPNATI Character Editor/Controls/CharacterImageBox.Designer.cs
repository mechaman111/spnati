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
			this.txtPreview = new System.Windows.Forms.RichTextBox();
			this.canvas.SuspendLayout();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.Controls.Add(this.txtPreview);
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
			// txtPreview
			// 
			this.txtPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPreview.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPreview.Location = new System.Drawing.Point(31, 65);
			this.txtPreview.Name = "txtPreview";
			this.txtPreview.ReadOnly = true;
			this.txtPreview.Size = new System.Drawing.Size(100, 96);
			this.txtPreview.TabIndex = 0;
			this.txtPreview.TabStop = false;
			this.txtPreview.Text = "";
			this.txtPreview.Visible = false;
			this.txtPreview.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.txtPreview_ContentsResized);
			// 
			// CharacterImageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.canvas);
			this.Name = "CharacterImageBox";
			this.Size = new System.Drawing.Size(197, 493);
			this.Resize += new System.EventHandler(this.CharacterImageBox_Resize);
			this.canvas.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.CommonControls.DBPanel canvas;
		private System.Windows.Forms.Timer tmrTick;
		private System.Windows.Forms.RichTextBox txtPreview;
	}
}

namespace SPNATI_Character_Editor.EpilogueEditor
{
	partial class LiveCanvas
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
			this.canvas = new Desktop.CommonControls.SelectablePanel();
			this.canvasStrip = new System.Windows.Forms.ToolStrip();
			this.cmdFit = new System.Windows.Forms.ToolStripButton();
			this.tsZoomOut = new System.Windows.Forms.ToolStripButton();
			this.tsZoomIn = new System.Windows.Forms.ToolStripButton();
			this.tsZoom = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdMarkers = new System.Windows.Forms.ToolStripButton();
			this.tsFilter = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsRecord = new System.Windows.Forms.ToolStripButton();
			this.tsRight = new System.Windows.Forms.ToolStrip();
			this.tsBackColor = new System.Windows.Forms.ToolStripButton();
			this.tsHelp = new System.Windows.Forms.ToolStripButton();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.canvasStrip.SuspendLayout();
			this.tsRight.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.canvas.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.canvas.Location = new System.Drawing.Point(0, 25);
			this.canvas.Margin = new System.Windows.Forms.Padding(0);
			this.canvas.Name = "canvas";
			this.canvas.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.canvas.Size = new System.Drawing.Size(851, 589);
			this.canvas.TabIndex = 0;
			this.canvas.TabSide = Desktop.Skinning.TabSide.None;
			this.canvas.TabStop = true;
			this.canvas.Click += new System.EventHandler(this.canvas_Click);
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
			this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
			this.canvas.Resize += new System.EventHandler(this.canvas_Resize);
			// 
			// canvasStrip
			// 
			this.canvasStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.canvasStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.canvasStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdFit,
            this.tsZoomOut,
            this.tsZoomIn,
            this.tsZoom,
            this.toolStripSeparator1,
            this.cmdMarkers,
            this.tsFilter,
            this.toolStripSeparator2,
            this.tsRecord});
			this.canvasStrip.Location = new System.Drawing.Point(0, 0);
			this.canvasStrip.Name = "canvasStrip";
			this.canvasStrip.Size = new System.Drawing.Size(207, 25);
			this.canvasStrip.TabIndex = 19;
			this.canvasStrip.Tag = "PrimaryLight";
			// 
			// cmdFit
			// 
			this.cmdFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdFit.Image = global::SPNATI_Character_Editor.Properties.Resources.FitToScreen;
			this.cmdFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdFit.Name = "cmdFit";
			this.cmdFit.Size = new System.Drawing.Size(23, 22);
			this.cmdFit.Text = "Fit to Screen";
			this.cmdFit.ToolTipText = "Recenter scene and adjust zoom to fit the screen";
			this.cmdFit.Click += new System.EventHandler(this.cmdFit_Click);
			// 
			// tsZoomOut
			// 
			this.tsZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsZoomOut.Image = global::SPNATI_Character_Editor.Properties.Resources.ZoomOut;
			this.tsZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsZoomOut.Name = "tsZoomOut";
			this.tsZoomOut.Size = new System.Drawing.Size(23, 22);
			this.tsZoomOut.Text = "Zoom Out";
			this.tsZoomOut.Click += new System.EventHandler(this.tsZoomOut_Click);
			// 
			// tsZoomIn
			// 
			this.tsZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsZoomIn.Image = global::SPNATI_Character_Editor.Properties.Resources.ZoomIn;
			this.tsZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsZoomIn.Name = "tsZoomIn";
			this.tsZoomIn.Size = new System.Drawing.Size(23, 22);
			this.tsZoomIn.Text = "Zoom In";
			this.tsZoomIn.Click += new System.EventHandler(this.tsZoomIn_Click);
			// 
			// tsZoom
			// 
			this.tsZoom.Font = new System.Drawing.Font("Segoe UI", 7F);
			this.tsZoom.Name = "tsZoom";
			this.tsZoom.Size = new System.Drawing.Size(15, 22);
			this.tsZoom.Text = "1x";
			this.tsZoom.Click += new System.EventHandler(this.tsZoom_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// cmdMarkers
			// 
			this.cmdMarkers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.cmdMarkers.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdMarkers.Name = "cmdMarkers";
			this.cmdMarkers.Size = new System.Drawing.Size(62, 22);
			this.cmdMarkers.Text = "Markers...";
			this.cmdMarkers.Click += new System.EventHandler(this.cmdMarkers_Click);
			// 
			// tsFilter
			// 
			this.tsFilter.CheckOnClick = true;
			this.tsFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsFilter.Image = global::SPNATI_Character_Editor.Properties.Resources.Filter;
			this.tsFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsFilter.Name = "tsFilter";
			this.tsFilter.Size = new System.Drawing.Size(23, 22);
			this.tsFilter.Text = "Toggle marker conditionals";
			this.tsFilter.ToolTipText = "Toggle marker conditionals";
			this.tsFilter.Click += new System.EventHandler(this.tsFilter_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsRecord
			// 
			this.tsRecord.CheckOnClick = true;
			this.tsRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRecord.Image = global::SPNATI_Character_Editor.Properties.Resources.Record;
			this.tsRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRecord.Name = "tsRecord";
			this.tsRecord.Size = new System.Drawing.Size(23, 22);
			this.tsRecord.Text = "Live record in playback mode";
			this.tsRecord.Click += new System.EventHandler(this.tsRecord_Click);
			// 
			// tsRight
			// 
			this.tsRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tsRight.Dock = System.Windows.Forms.DockStyle.None;
			this.tsRight.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBackColor,
            this.tsHelp});
			this.tsRight.Location = new System.Drawing.Point(802, 0);
			this.tsRight.Name = "tsRight";
			this.tsRight.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.tsRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsRight.Size = new System.Drawing.Size(49, 25);
			this.tsRight.TabIndex = 20;
			this.tsRight.Tag = "PrimaryLight";
			// 
			// tsBackColor
			// 
			this.tsBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsBackColor.Image = global::SPNATI_Character_Editor.Properties.Resources.ColorPalette;
			this.tsBackColor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBackColor.Name = "tsBackColor";
			this.tsBackColor.Size = new System.Drawing.Size(23, 22);
			this.tsBackColor.Text = "Background Color";
			this.tsBackColor.ToolTipText = "Set background color";
			this.tsBackColor.Click += new System.EventHandler(this.tsBackColor_Click);
			// 
			// tsHelp
			// 
			this.tsHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsHelp.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.tsHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsHelp.Name = "tsHelp";
			this.tsHelp.Size = new System.Drawing.Size(23, 22);
			this.tsHelp.Text = "Help";
			this.tsHelp.ToolTipText = "Show Help...";
			this.tsHelp.Click += new System.EventHandler(this.tsHelp_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel1.Controls.Add(this.tsRight);
			this.skinnedPanel1.Controls.Add(this.canvasStrip);
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 0);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.skinnedPanel1.Size = new System.Drawing.Size(851, 25);
			this.skinnedPanel1.TabIndex = 21;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// LiveCanvas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.canvas);
			this.Name = "LiveCanvas";
			this.Size = new System.Drawing.Size(851, 614);
			this.canvasStrip.ResumeLayout(false);
			this.canvasStrip.PerformLayout();
			this.tsRight.ResumeLayout(false);
			this.tsRight.PerformLayout();
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.SelectablePanel canvas;
		private System.Windows.Forms.ToolStrip canvasStrip;
		private System.Windows.Forms.ToolStripButton cmdFit;
		private System.Windows.Forms.ToolStripButton cmdMarkers;
		private System.Windows.Forms.ToolStripButton tsZoomIn;
		private System.Windows.Forms.ToolStripButton tsZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel tsZoom;
		private System.Windows.Forms.ToolStrip tsRight;
		private System.Windows.Forms.ToolStripButton tsHelp;
		private System.Windows.Forms.ToolStripButton tsRecord;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsFilter;
		private System.Windows.Forms.ToolStripButton tsBackColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}

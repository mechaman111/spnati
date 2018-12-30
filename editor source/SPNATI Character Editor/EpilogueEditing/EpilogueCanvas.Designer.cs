namespace SPNATI_Character_Editor.Controls
{
	partial class EpilogueCanvas
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.propertyTable = new Desktop.CommonControls.PropertyTable();
			this.canvasStrip = new System.Windows.Forms.ToolStrip();
			this.cmdLock = new System.Windows.Forms.ToolStripButton();
			this.cmdFit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdPlayDirective = new System.Windows.Forms.ToolStripButton();
			this.cmdPlay = new System.Windows.Forms.ToolStripButton();
			this.canvas = new Desktop.CommonControls.SelectablePanel();
			this.label1 = new System.Windows.Forms.Label();
			this.lblZoom = new System.Windows.Forms.Label();
			this.sliderZoom = new System.Windows.Forms.TrackBar();
			this.lblCoord = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tmrPlay = new System.Windows.Forms.Timer(this.components);
			this.treeScenes = new SPNATI_Character_Editor.Controls.SceneTree();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.canvasStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.canvasStrip);
			this.splitContainer1.Panel2.Controls.Add(this.canvas);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.lblZoom);
			this.splitContainer1.Panel2.Controls.Add(this.sliderZoom);
			this.splitContainer1.Size = new System.Drawing.Size(1101, 516);
			this.splitContainer1.SplitterDistance = 297;
			this.splitContainer1.TabIndex = 10;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.treeScenes);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.propertyTable);
			this.splitContainer2.Size = new System.Drawing.Size(293, 512);
			this.splitContainer2.SplitterDistance = 302;
			this.splitContainer2.TabIndex = 0;
			// 
			// propertyTable
			// 
			this.propertyTable.AllowDelete = false;
			this.propertyTable.AllowFavorites = false;
			this.propertyTable.AllowHelp = true;
			this.propertyTable.Data = null;
			this.propertyTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyTable.HideAddField = true;
			this.propertyTable.HideSpeedButtons = true;
			this.propertyTable.Location = new System.Drawing.Point(0, 0);
			this.propertyTable.Name = "propertyTable";
			this.propertyTable.PlaceholderText = "Add a property";
			this.propertyTable.RemoveCaption = "Remove";
			this.propertyTable.RowHeaderWidth = 85F;
			this.propertyTable.Size = new System.Drawing.Size(293, 206);
			this.propertyTable.Sorted = true;
			this.propertyTable.TabIndex = 0;
			this.propertyTable.UseAutoComplete = false;
			this.propertyTable.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.propertyTable_PropertyChanged);
			// 
			// canvasStrip
			// 
			this.canvasStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.canvasStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.canvasStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdLock,
            this.cmdFit,
            this.toolStripSeparator1,
            this.cmdPlayDirective,
            this.cmdPlay});
			this.canvasStrip.Location = new System.Drawing.Point(184, 0);
			this.canvasStrip.Name = "canvasStrip";
			this.canvasStrip.Size = new System.Drawing.Size(101, 25);
			this.canvasStrip.TabIndex = 0;
			// 
			// cmdLock
			// 
			this.cmdLock.CheckOnClick = true;
			this.cmdLock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdLock.Image = global::SPNATI_Character_Editor.Properties.Resources.VideoCamera;
			this.cmdLock.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdLock.Name = "cmdLock";
			this.cmdLock.Size = new System.Drawing.Size(23, 22);
			this.cmdLock.Text = "Lock";
			this.cmdLock.ToolTipText = "Lock to scene camera";
			this.cmdLock.Click += new System.EventHandler(this.cmdLock_Click);
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
			this.cmdFit.Click += new System.EventHandler(this.cmdRecenter_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// cmdPlayDirective
			// 
			this.cmdPlayDirective.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPlayDirective.Image = global::SPNATI_Character_Editor.Properties.Resources.Playback;
			this.cmdPlayDirective.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPlayDirective.Name = "cmdPlayDirective";
			this.cmdPlayDirective.Size = new System.Drawing.Size(23, 22);
			this.cmdPlayDirective.Text = "Play";
			this.cmdPlayDirective.ToolTipText = "Play selected animation";
			this.cmdPlayDirective.Click += new System.EventHandler(this.cmdPlayDirective_Click);
			// 
			// cmdPlay
			// 
			this.cmdPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPlay.Image = global::SPNATI_Character_Editor.Properties.Resources.Play;
			this.cmdPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPlay.Name = "cmdPlay";
			this.cmdPlay.Size = new System.Drawing.Size(23, 22);
			this.cmdPlay.Text = "Play";
			this.cmdPlay.ToolTipText = "Play scene";
			this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
			// 
			// canvas
			// 
			this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.canvas.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.canvas.Location = new System.Drawing.Point(-2, 28);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(795, 486);
			this.canvas.TabIndex = 0;
			this.canvas.TabStop = true;
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
			this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
			this.canvas.Resize += new System.EventHandler(this.canvas_Resize);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Zoom:";
			// 
			// lblZoom
			// 
			this.lblZoom.AutoSize = true;
			this.lblZoom.Location = new System.Drawing.Point(148, 5);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(33, 13);
			this.lblZoom.TabIndex = 13;
			this.lblZoom.Text = "1.00x";
			// 
			// sliderZoom
			// 
			this.sliderZoom.AutoSize = false;
			this.sliderZoom.LargeChange = 1;
			this.sliderZoom.Location = new System.Drawing.Point(46, 5);
			this.sliderZoom.Maximum = 11;
			this.sliderZoom.Name = "sliderZoom";
			this.sliderZoom.Size = new System.Drawing.Size(104, 19);
			this.sliderZoom.TabIndex = 12;
			this.sliderZoom.Value = 3;
			this.sliderZoom.ValueChanged += new System.EventHandler(this.SliderZoom_ValueChanged);
			// 
			// lblCoord
			// 
			this.lblCoord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCoord.Location = new System.Drawing.Point(923, 7);
			this.lblCoord.Name = "lblCoord";
			this.lblCoord.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCoord.Size = new System.Drawing.Size(175, 13);
			this.lblCoord.TabIndex = 10;
			this.lblCoord.Text = "(0,0)";
			this.lblCoord.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tmrPlay
			// 
			this.tmrPlay.Interval = 32;
			this.tmrPlay.Tick += new System.EventHandler(this.tmrPlay_Tick);
			// 
			// treeScenes
			// 
			this.treeScenes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeScenes.Enabled = false;
			this.treeScenes.Location = new System.Drawing.Point(0, 0);
			this.treeScenes.Name = "treeScenes";
			this.treeScenes.Size = new System.Drawing.Size(293, 302);
			this.treeScenes.TabIndex = 0;
			this.treeScenes.AfterSelect += new System.EventHandler<SPNATI_Character_Editor.Controls.SceneTreeEventArgs>(this.TreeScenes_AfterSelect);
			// 
			// EpilogueCanvas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblCoord);
			this.Controls.Add(this.splitContainer1);
			this.Enabled = false;
			this.Name = "EpilogueCanvas";
			this.Size = new System.Drawing.Size(1101, 516);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.canvasStrip.ResumeLayout(false);
			this.canvasStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.CommonControls.SelectablePanel canvas;
		private System.Windows.Forms.Label lblZoom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar sliderZoom;
		private System.Windows.Forms.Label lblCoord;
		private Desktop.CommonControls.PropertyTable propertyTable;
		private System.Windows.Forms.ToolTip toolTip1;
		private SceneTree treeScenes;
		private System.Windows.Forms.ToolStrip canvasStrip;
		private System.Windows.Forms.ToolStripButton cmdFit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton cmdPlay;
		private System.Windows.Forms.ToolStripButton cmdLock;
		private System.Windows.Forms.Timer tmrPlay;
		private System.Windows.Forms.ToolStripButton cmdPlayDirective;
	}
}

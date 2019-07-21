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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpilogueCanvas));
			this.splitContainer1 = new Desktop.Skinning.SkinnedSplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.propertyTable = new Desktop.CommonControls.PropertyTable();
			this.canvasStrip = new System.Windows.Forms.ToolStrip();
			this.cmdLock = new System.Windows.Forms.ToolStripButton();
			this.cmdFit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdToggleFade = new System.Windows.Forms.ToolStripButton();
			this.cmdMarkers = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdPlayDirective = new System.Windows.Forms.ToolStripButton();
			this.cmdPlay = new System.Windows.Forms.ToolStripButton();
			this.canvas = new Desktop.CommonControls.SelectablePanel();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.lblZoom = new Desktop.Skinning.SkinnedLabel();
			this.sliderZoom = new Desktop.Skinning.SkinnedSlider();
			this.lblCoord = new Desktop.Skinning.SkinnedLabel();
			this.tmrPlay = new System.Windows.Forms.Timer(this.components);
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
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
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
			this.splitContainer1.Panel2.Controls.Add(this.skinnedPanel1);
			this.splitContainer1.Panel2.Controls.Add(this.canvas);
			this.splitContainer1.Size = new System.Drawing.Size(1101, 516);
			this.splitContainer1.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.Primary;
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
			this.splitContainer2.Size = new System.Drawing.Size(295, 514);
			this.splitContainer2.SplitterDistance = 303;
			this.splitContainer2.TabIndex = 0;
			// 
			// propertyTable
			// 
			this.propertyTable.AllowDelete = false;
			this.propertyTable.AllowFavorites = false;
			this.propertyTable.AllowHelp = true;
			this.propertyTable.AllowMacros = false;
			this.propertyTable.BackColor = System.Drawing.Color.White;
			this.propertyTable.Data = null;
			this.propertyTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyTable.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.propertyTable.HideAddField = true;
			this.propertyTable.HideSpeedButtons = true;
			this.propertyTable.Location = new System.Drawing.Point(0, 0);
			this.propertyTable.ModifyingProperty = null;
			this.propertyTable.Name = "propertyTable";
			this.propertyTable.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.propertyTable.PlaceholderText = "Add a property";
			this.propertyTable.PreserveControls = true;
			this.propertyTable.PreviewData = null;
			this.propertyTable.RemoveCaption = "Remove";
			this.propertyTable.RowHeaderWidth = 85F;
			this.propertyTable.RunInitialAddEvents = false;
			this.propertyTable.Size = new System.Drawing.Size(295, 207);
			this.propertyTable.Sorted = true;
			this.propertyTable.TabIndex = 0;
			this.propertyTable.UndoManager = null;
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
            this.cmdToggleFade,
            this.cmdMarkers,
            this.toolStripSeparator2,
            this.cmdPlayDirective,
            this.cmdPlay});
			this.canvasStrip.Location = new System.Drawing.Point(184, 1);
			this.canvasStrip.Name = "canvasStrip";
			this.canvasStrip.Size = new System.Drawing.Size(192, 25);
			this.canvasStrip.TabIndex = 0;
			this.canvasStrip.Tag = "PrimaryLight";
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
			// cmdToggleFade
			// 
			this.cmdToggleFade.Checked = true;
			this.cmdToggleFade.CheckOnClick = true;
			this.cmdToggleFade.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cmdToggleFade.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdToggleFade.Image = global::SPNATI_Character_Editor.Properties.Resources.Fade;
			this.cmdToggleFade.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdToggleFade.Name = "cmdToggleFade";
			this.cmdToggleFade.Size = new System.Drawing.Size(23, 22);
			this.cmdToggleFade.Text = "Play";
			this.cmdToggleFade.ToolTipText = "Toggle fade overlay";
			this.cmdToggleFade.Click += new System.EventHandler(this.cmdToggleFade_Click);
			// 
			// cmdMarkers
			// 
			this.cmdMarkers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.cmdMarkers.Image = ((System.Drawing.Image)(resources.GetObject("cmdMarkers.Image")));
			this.cmdMarkers.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdMarkers.Name = "cmdMarkers";
			this.cmdMarkers.Size = new System.Drawing.Size(62, 22);
			this.cmdMarkers.Text = "Markers...";
			this.cmdMarkers.Click += new System.EventHandler(this.cmdMarkers_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
			this.canvas.Location = new System.Drawing.Point(0, 28);
			this.canvas.Name = "canvas";
			this.canvas.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.canvas.Size = new System.Drawing.Size(799, 486);
			this.canvas.TabIndex = 0;
			this.canvas.TabSide = Desktop.Skinning.TabSide.None;
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
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Zoom:";
			// 
			// lblZoom
			// 
			this.lblZoom.AutoSize = true;
			this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblZoom.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblZoom.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblZoom.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblZoom.Location = new System.Drawing.Point(148, 6);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(33, 13);
			this.lblZoom.TabIndex = 13;
			this.lblZoom.Text = "1.00x";
			// 
			// sliderZoom
			// 
			this.sliderZoom.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.sliderZoom.Increment = 1;
			this.sliderZoom.Location = new System.Drawing.Point(46, 4);
			this.sliderZoom.Maximum = 11;
			this.sliderZoom.Minimum = 0;
			this.sliderZoom.Name = "sliderZoom";
			this.sliderZoom.Size = new System.Drawing.Size(104, 19);
			this.sliderZoom.TabIndex = 12;
			this.sliderZoom.TickFrequency = 0;
			this.sliderZoom.Value = 3;
			this.sliderZoom.ValueChanged += new System.EventHandler(this.SliderZoom_ValueChanged);
			// 
			// lblCoord
			// 
			this.lblCoord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCoord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblCoord.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCoord.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblCoord.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblCoord.Location = new System.Drawing.Point(623, 5);
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
			// skinnedPanel1
			// 
			this.skinnedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel1.Controls.Add(this.lblCoord);
			this.skinnedPanel1.Controls.Add(this.canvasStrip);
			this.skinnedPanel1.Controls.Add(this.label1);
			this.skinnedPanel1.Controls.Add(this.sliderZoom);
			this.skinnedPanel1.Controls.Add(this.lblZoom);
			this.skinnedPanel1.Location = new System.Drawing.Point(-2, 0);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.skinnedPanel1.Size = new System.Drawing.Size(801, 28);
			this.skinnedPanel1.TabIndex = 14;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// treeScenes
			// 
			this.treeScenes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeScenes.Enabled = false;
			this.treeScenes.Location = new System.Drawing.Point(0, 0);
			this.treeScenes.Name = "treeScenes";
			this.treeScenes.Size = new System.Drawing.Size(295, 303);
			this.treeScenes.TabIndex = 0;
			this.treeScenes.AfterSelect += new System.EventHandler<SPNATI_Character_Editor.Controls.SceneTreeEventArgs>(this.TreeScenes_AfterSelect);
			// 
			// EpilogueCanvas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Enabled = false;
			this.Name = "EpilogueCanvas";
			this.Size = new System.Drawing.Size(1101, 516);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.canvasStrip.ResumeLayout(false);
			this.canvasStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sliderZoom)).EndInit();
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedSplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.CommonControls.SelectablePanel canvas;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel lblCoord;
		private Desktop.CommonControls.PropertyTable propertyTable;
		private SceneTree treeScenes;
		private System.Windows.Forms.ToolStrip canvasStrip;
		private System.Windows.Forms.ToolStripButton cmdFit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton cmdPlay;
		private System.Windows.Forms.ToolStripButton cmdLock;
		private System.Windows.Forms.Timer tmrPlay;
		private System.Windows.Forms.ToolStripButton cmdPlayDirective;
		private System.Windows.Forms.ToolStripButton cmdToggleFade;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private Desktop.Skinning.SkinnedLabel lblZoom;
		private Desktop.Skinning.SkinnedSlider sliderZoom;
		private System.Windows.Forms.ToolStripButton cmdMarkers;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}

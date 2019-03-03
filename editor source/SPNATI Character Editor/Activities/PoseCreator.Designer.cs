namespace SPNATI_Character_Editor.Activities
{
	partial class PoseCreator
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoseCreator));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.lblDragger = new System.Windows.Forms.Label();
			this.lstPoses = new Desktop.CommonControls.DBTreeView();
			this.tsPoseList = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.lblCoord = new System.Windows.Forms.Label();
			this.canvasStrip = new System.Windows.Forms.ToolStrip();
			this.canvas = new Desktop.CommonControls.SelectablePanel();
			this.tmrTick = new System.Windows.Forms.Timer(this.components);
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.tsAddDirective = new System.Windows.Forms.ToolStripSplitButton();
			this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addAnimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsAddKeyframe = new System.Windows.Forms.ToolStripButton();
			this.tsCut = new System.Windows.Forms.ToolStripButton();
			this.tsCopy = new System.Windows.Forms.ToolStripButton();
			this.tsPaste = new System.Windows.Forms.ToolStripButton();
			this.tsDuplicate = new System.Windows.Forms.ToolStripButton();
			this.tsCollapseAll = new System.Windows.Forms.ToolStripButton();
			this.tsExpandAll = new System.Windows.Forms.ToolStripButton();
			this.cmdFit = new System.Windows.Forms.ToolStripButton();
			this.openFileDialog1 = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			this.cmdMarkers = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tsPoseList.SuspendLayout();
			this.canvasStrip.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.lblCoord);
			this.splitContainer1.Panel2.Controls.Add(this.canvasStrip);
			this.splitContainer1.Panel2.Controls.Add(this.canvas);
			this.splitContainer1.Size = new System.Drawing.Size(998, 674);
			this.splitContainer1.SplitterDistance = 252;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.lblDragger);
			this.splitContainer2.Panel1.Controls.Add(this.lstPoses);
			this.splitContainer2.Panel1.Controls.Add(this.tsPoseList);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.table);
			this.splitContainer2.Size = new System.Drawing.Size(252, 674);
			this.splitContainer2.SplitterDistance = 211;
			this.splitContainer2.TabIndex = 2;
			// 
			// lblDragger
			// 
			this.lblDragger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDragger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDragger.Location = new System.Drawing.Point(5, 102);
			this.lblDragger.Name = "lblDragger";
			this.lblDragger.Size = new System.Drawing.Size(220, 2);
			this.lblDragger.TabIndex = 4;
			this.lblDragger.Visible = false;
			// 
			// lstPoses
			// 
			this.lstPoses.AllowDrop = true;
			this.lstPoses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstPoses.HideSelection = false;
			this.lstPoses.Location = new System.Drawing.Point(0, 25);
			this.lstPoses.Name = "lstPoses";
			this.lstPoses.Size = new System.Drawing.Size(248, 182);
			this.lstPoses.TabIndex = 2;
			this.lstPoses.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstPoses_ItemDrag);
			this.lstPoses.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.lstPoses_AfterSelect);
			this.lstPoses.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstPoses_DragDrop);
			this.lstPoses.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstPoses_DragEnter);
			this.lstPoses.DragOver += new System.Windows.Forms.DragEventHandler(this.lstPoses_DragOver);
			this.lstPoses.DragLeave += new System.EventHandler(this.lstPoses_DragLeave);
			this.lstPoses.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.lstPoses_QueryContinueDrag);
			this.lstPoses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPoses_KeyDown);
			// 
			// tsPoseList
			// 
			this.tsPoseList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsPoseList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsRemove,
            this.tsAddDirective,
            this.tsAddKeyframe,
            this.toolStripSeparator1,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsDuplicate,
            this.toolStripSeparator2,
            this.tsCollapseAll,
            this.tsExpandAll});
			this.tsPoseList.Location = new System.Drawing.Point(0, 0);
			this.tsPoseList.Name = "tsPoseList";
			this.tsPoseList.Size = new System.Drawing.Size(248, 25);
			this.tsPoseList.TabIndex = 1;
			this.tsPoseList.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// table
			// 
			this.table.AllowDelete = false;
			this.table.AllowFavorites = false;
			this.table.AllowHelp = false;
			this.table.Data = null;
			this.table.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table.HideAddField = true;
			this.table.HideSpeedButtons = true;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.Name = "table";
			this.table.PlaceholderText = null;
			this.table.RemoveCaption = "Remove";
			this.table.RowHeaderWidth = 85F;
			this.table.Size = new System.Drawing.Size(248, 455);
			this.table.Sorted = true;
			this.table.TabIndex = 0;
			this.table.UseAutoComplete = true;
			this.table.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.table_PropertyChanged);
			// 
			// lblCoord
			// 
			this.lblCoord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCoord.Location = new System.Drawing.Point(560, 0);
			this.lblCoord.Name = "lblCoord";
			this.lblCoord.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCoord.Size = new System.Drawing.Size(175, 13);
			this.lblCoord.TabIndex = 15;
			this.lblCoord.Text = "(0,0)";
			this.lblCoord.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// canvasStrip
			// 
			this.canvasStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.canvasStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.canvasStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdFit,
            this.cmdMarkers});
			this.canvasStrip.Location = new System.Drawing.Point(0, 0);
			this.canvasStrip.Name = "canvasStrip";
			this.canvasStrip.Size = new System.Drawing.Size(119, 25);
			this.canvasStrip.TabIndex = 14;
			// 
			// canvas
			// 
			this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.canvas.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.canvas.Location = new System.Drawing.Point(0, 28);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(738, 642);
			this.canvas.TabIndex = 0;
			this.canvas.TabStop = true;
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
			this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
			// 
			// tmrTick
			// 
			this.tmrTick.Interval = 30;
			this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
			// 
			// tsAdd
			// 
			this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(23, 22);
			this.tsAdd.Text = "Add Pose";
			this.tsAdd.ToolTipText = "Add sprite-based pose";
			this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
			// 
			// tsRemove
			// 
			this.tsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(23, 22);
			this.tsRemove.Text = "Remove pose";
			this.tsRemove.ToolTipText = "Remove pose";
			this.tsRemove.Click += new System.EventHandler(this.tsRemove_Click);
			// 
			// tsAddDirective
			// 
			this.tsAddDirective.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddDirective.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpriteToolStripMenuItem,
            this.addAnimationToolStripMenuItem});
			this.tsAddDirective.Image = global::SPNATI_Character_Editor.Properties.Resources.AddChildNode;
			this.tsAddDirective.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddDirective.Name = "tsAddDirective";
			this.tsAddDirective.Size = new System.Drawing.Size(32, 22);
			this.tsAddDirective.Text = "Add";
			this.tsAddDirective.ToolTipText = "Add a new directive to the selected scene";
			// 
			// addSpriteToolStripMenuItem
			// 
			this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
			this.addSpriteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.addSpriteToolStripMenuItem.Tag = "sprite";
			this.addSpriteToolStripMenuItem.Text = "Add Sprite";
			this.addSpriteToolStripMenuItem.ToolTipText = "Add a sprite to the scene";
			this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.addSpriteToolStripMenuItem_Click);
			// 
			// addAnimationToolStripMenuItem
			// 
			this.addAnimationToolStripMenuItem.Name = "addAnimationToolStripMenuItem";
			this.addAnimationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.addAnimationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.addAnimationToolStripMenuItem.Tag = "remove";
			this.addAnimationToolStripMenuItem.Text = "Add Animation";
			this.addAnimationToolStripMenuItem.Click += new System.EventHandler(this.addAnimationToolStripMenuItem_Click);
			// 
			// tsAddKeyframe
			// 
			this.tsAddKeyframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddKeyframe.Image = global::SPNATI_Character_Editor.Properties.Resources.AddKeyframe;
			this.tsAddKeyframe.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddKeyframe.Name = "tsAddKeyframe";
			this.tsAddKeyframe.Size = new System.Drawing.Size(23, 22);
			this.tsAddKeyframe.Text = "Add Keyframe";
			this.tsAddKeyframe.Click += new System.EventHandler(this.tsAddKeyframe_Click);
			// 
			// tsCut
			// 
			this.tsCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCut.Image = global::SPNATI_Character_Editor.Properties.Resources.Cut;
			this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCut.Name = "tsCut";
			this.tsCut.Size = new System.Drawing.Size(23, 22);
			this.tsCut.Text = "Cut";
			this.tsCut.Click += new System.EventHandler(this.tsCut_Click);
			// 
			// tsCopy
			// 
			this.tsCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCopy.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopy.Name = "tsCopy";
			this.tsCopy.Size = new System.Drawing.Size(23, 22);
			this.tsCopy.Text = "Copy";
			this.tsCopy.Click += new System.EventHandler(this.tsCopy_Click);
			// 
			// tsPaste
			// 
			this.tsPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsPaste.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPaste.Name = "tsPaste";
			this.tsPaste.Size = new System.Drawing.Size(23, 22);
			this.tsPaste.Text = "Paste";
			this.tsPaste.Click += new System.EventHandler(this.tsPaste_Click);
			// 
			// tsDuplicate
			// 
			this.tsDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsDuplicate.Image = global::SPNATI_Character_Editor.Properties.Resources.Duplicate;
			this.tsDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDuplicate.Name = "tsDuplicate";
			this.tsDuplicate.Size = new System.Drawing.Size(23, 22);
			this.tsDuplicate.Text = "Duplicate";
			this.tsDuplicate.Click += new System.EventHandler(this.tsDuplicate_Click);
			// 
			// tsCollapseAll
			// 
			this.tsCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCollapseAll.Image = global::SPNATI_Character_Editor.Properties.Resources.CollapseAll;
			this.tsCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCollapseAll.Name = "tsCollapseAll";
			this.tsCollapseAll.Size = new System.Drawing.Size(23, 22);
			this.tsCollapseAll.Text = "Collapse all";
			this.tsCollapseAll.ToolTipText = "Collapse all";
			this.tsCollapseAll.Click += new System.EventHandler(this.tsCollapseAll_Click);
			// 
			// tsExpandAll
			// 
			this.tsExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsExpandAll.Image = global::SPNATI_Character_Editor.Properties.Resources.ExpandAll;
			this.tsExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsExpandAll.Name = "tsExpandAll";
			this.tsExpandAll.Size = new System.Drawing.Size(23, 20);
			this.tsExpandAll.Text = "Expand all";
			this.tsExpandAll.ToolTipText = "Expand all";
			this.tsExpandAll.Click += new System.EventHandler(this.tsExpandAll_Click);
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
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "";
			this.openFileDialog1.UseAbsolutePaths = false;
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
			// PoseCreator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "PoseCreator";
			this.Size = new System.Drawing.Size(998, 674);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tsPoseList.ResumeLayout(false);
			this.tsPoseList.PerformLayout();
			this.canvasStrip.ResumeLayout(false);
			this.canvasStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStrip tsPoseList;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.CommonControls.PropertyTable table;
		private Desktop.CommonControls.SelectablePanel canvas;
		private Controls.CharacterImageDialog openFileDialog1;
		private Desktop.CommonControls.DBTreeView lstPoses;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsCollapseAll;
		private System.Windows.Forms.ToolStripButton tsExpandAll;
		private System.Windows.Forms.ToolStripButton tsCut;
		private System.Windows.Forms.ToolStripButton tsCopy;
		private System.Windows.Forms.ToolStripButton tsPaste;
		private System.Windows.Forms.ToolStripButton tsDuplicate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Label lblDragger;
		private System.Windows.Forms.ToolStripSplitButton tsAddDirective;
		private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addAnimationToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton tsAddKeyframe;
		private System.Windows.Forms.Timer tmrTick;
		private System.Windows.Forms.ToolStrip canvasStrip;
		private System.Windows.Forms.ToolStripButton cmdFit;
		private System.Windows.Forms.Label lblCoord;
		private System.Windows.Forms.ToolStripButton cmdMarkers;
	}
}

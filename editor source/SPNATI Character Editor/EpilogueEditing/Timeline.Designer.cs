namespace SPNATI_Character_Editor.EpilogueEditor
{
	partial class Timeline
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
			this.tmrTick = new System.Windows.Forms.Timer(this.components);
			this.panelHeader = new Desktop.CommonControls.SelectablePanel();
			this.panelAxis = new Desktop.CommonControls.DBPanel();
			this.container = new Desktop.CommonControls.DBPanel();
			this.panel = new Desktop.CommonControls.SelectablePanel();
			this.tools = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.editMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tooltip = new System.Windows.Forms.ToolTip(this.components);
			this.tmrTooltip = new System.Windows.Forms.Timer(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.tsRedo = new System.Windows.Forms.ToolStripMenuItem();
			this.tsCut = new System.Windows.Forms.ToolStripMenuItem();
			this.tsCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.tsPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.tsDuplicate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.tsFirst = new System.Windows.Forms.ToolStripButton();
			this.tsPrevious = new System.Windows.Forms.ToolStripButton();
			this.tsPlay = new System.Windows.Forms.ToolStripSplitButton();
			this.playOnceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playLoopingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playOnceWithRepeatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsNext = new System.Windows.Forms.ToolStripButton();
			this.tsLast = new System.Windows.Forms.ToolStripButton();
			this.tsZoomOut = new System.Windows.Forms.ToolStripButton();
			this.tsZoomIn = new System.Windows.Forms.ToolStripButton();
			this.container.SuspendLayout();
			this.tools.SuspendLayout();
			this.editMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tmrTick
			// 
			this.tmrTick.Interval = 30;
			this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
			// 
			// panelHeader
			// 
			this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panelHeader.Location = new System.Drawing.Point(0, 25);
			this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
			this.panelHeader.Name = "panelHeader";
			this.panelHeader.Size = new System.Drawing.Size(180, 110);
			this.panelHeader.TabIndex = 4;
			this.panelHeader.TabStop = true;
			this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
			this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
			this.panelHeader.MouseLeave += new System.EventHandler(this.panelHeader_MouseLeave);
			this.panelHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseMove);
			// 
			// panelAxis
			// 
			this.panelAxis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelAxis.Location = new System.Drawing.Point(180, 0);
			this.panelAxis.Margin = new System.Windows.Forms.Padding(0);
			this.panelAxis.Name = "panelAxis";
			this.panelAxis.Size = new System.Drawing.Size(697, 25);
			this.panelAxis.TabIndex = 3;
			this.panelAxis.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAxis_Paint);
			this.panelAxis.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelAxis_MouseDown);
			this.panelAxis.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelAxis_MouseMove);
			this.panelAxis.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelAxis_MouseUp);
			// 
			// container
			// 
			this.container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.container.AutoScroll = true;
			this.container.Controls.Add(this.panel);
			this.container.Location = new System.Drawing.Point(180, 25);
			this.container.Margin = new System.Windows.Forms.Padding(0);
			this.container.Name = "container";
			this.container.Size = new System.Drawing.Size(697, 110);
			this.container.TabIndex = 3;
			this.container.Scroll += new System.Windows.Forms.ScrollEventHandler(this.container_Scroll);
			// 
			// panel
			// 
			this.panel.BackColor = System.Drawing.Color.LightGray;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Margin = new System.Windows.Forms.Padding(0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(697, 110);
			this.panel.TabIndex = 2;
			this.panel.TabStop = true;
			this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
			this.panel.MouseLeave += new System.EventHandler(this.panel_MouseLeave);
			this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
			this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
			this.panel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.panel_PreviewKeyDown);
			// 
			// tools
			// 
			this.tools.AutoSize = false;
			this.tools.Dock = System.Windows.Forms.DockStyle.None;
			this.tools.GripMargin = new System.Windows.Forms.Padding(0);
			this.tools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFirst,
            this.tsPrevious,
            this.tsPlay,
            this.tsNext,
            this.tsLast,
            this.toolStripSeparator3,
            this.tsZoomOut,
            this.tsZoomIn});
			this.tools.Location = new System.Drawing.Point(0, 0);
			this.tools.Name = "tools";
			this.tools.Padding = new System.Windows.Forms.Padding(0);
			this.tools.Size = new System.Drawing.Size(180, 25);
			this.tools.TabIndex = 6;
			this.tools.Text = "tools";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// editMenu
			// 
			this.editMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsUndo,
            this.tsRedo,
            this.toolStripSeparator2,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsDuplicate,
            this.tsDelete});
			this.editMenu.Name = "editMenu";
			this.editMenu.Size = new System.Drawing.Size(167, 164);
			this.editMenu.Opening += new System.ComponentModel.CancelEventHandler(this.editMenu_Opening);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(163, 6);
			this.toolStripSeparator2.Visible = false;
			// 
			// tmrTooltip
			// 
			this.tmrTooltip.Tick += new System.EventHandler(this.tmrTooltip_Tick);
			// 
			// contextMenu
			// 
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(61, 4);
			// 
			// tsUndo
			// 
			this.tsUndo.Enabled = false;
			this.tsUndo.Image = global::SPNATI_Character_Editor.Properties.Resources.Undo;
			this.tsUndo.Name = "tsUndo";
			this.tsUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.tsUndo.Size = new System.Drawing.Size(166, 22);
			this.tsUndo.Text = "Undo";
			this.tsUndo.Visible = false;
			this.tsUndo.Click += new System.EventHandler(this.tsUndo_Click);
			// 
			// tsRedo
			// 
			this.tsRedo.Enabled = false;
			this.tsRedo.Image = global::SPNATI_Character_Editor.Properties.Resources.Redo;
			this.tsRedo.Name = "tsRedo";
			this.tsRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.tsRedo.Size = new System.Drawing.Size(166, 22);
			this.tsRedo.Text = "Redo";
			this.tsRedo.Visible = false;
			this.tsRedo.Click += new System.EventHandler(this.tsRedo_Click);
			// 
			// tsCut
			// 
			this.tsCut.Enabled = false;
			this.tsCut.Image = global::SPNATI_Character_Editor.Properties.Resources.Cut;
			this.tsCut.Name = "tsCut";
			this.tsCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.tsCut.Size = new System.Drawing.Size(166, 22);
			this.tsCut.Text = "Cut";
			this.tsCut.Click += new System.EventHandler(this.tsCut_Click);
			// 
			// tsCopy
			// 
			this.tsCopy.Enabled = false;
			this.tsCopy.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.tsCopy.Name = "tsCopy";
			this.tsCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsCopy.Size = new System.Drawing.Size(166, 22);
			this.tsCopy.Text = "Copy";
			this.tsCopy.Click += new System.EventHandler(this.tsCopy_Click);
			// 
			// tsPaste
			// 
			this.tsPaste.Enabled = false;
			this.tsPaste.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.tsPaste.Name = "tsPaste";
			this.tsPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.tsPaste.Size = new System.Drawing.Size(166, 22);
			this.tsPaste.Text = "Paste";
			this.tsPaste.Click += new System.EventHandler(this.tsPaste_Click);
			// 
			// tsDuplicate
			// 
			this.tsDuplicate.Enabled = false;
			this.tsDuplicate.Image = global::SPNATI_Character_Editor.Properties.Resources.Duplicate;
			this.tsDuplicate.Name = "tsDuplicate";
			this.tsDuplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.tsDuplicate.Size = new System.Drawing.Size(166, 22);
			this.tsDuplicate.Text = "Duplicate";
			this.tsDuplicate.Click += new System.EventHandler(this.tsDuplicate_Click);
			// 
			// tsDelete
			// 
			this.tsDelete.Enabled = false;
			this.tsDelete.Image = global::SPNATI_Character_Editor.Properties.Resources.Delete;
			this.tsDelete.Name = "tsDelete";
			this.tsDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsDelete.Size = new System.Drawing.Size(166, 22);
			this.tsDelete.Text = "Delete";
			this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
			// 
			// tsFirst
			// 
			this.tsFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsFirst.Image = global::SPNATI_Character_Editor.Properties.Resources.FirstFrame;
			this.tsFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsFirst.Name = "tsFirst";
			this.tsFirst.Size = new System.Drawing.Size(23, 22);
			this.tsFirst.Text = "Jump to Start";
			this.tsFirst.Click += new System.EventHandler(this.tsFirst_Click);
			// 
			// tsPrevious
			// 
			this.tsPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsPrevious.Image = global::SPNATI_Character_Editor.Properties.Resources.PreviousFrame;
			this.tsPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPrevious.Name = "tsPrevious";
			this.tsPrevious.Size = new System.Drawing.Size(23, 22);
			this.tsPrevious.Text = "Jump to previous frame";
			this.tsPrevious.Click += new System.EventHandler(this.tsPrevious_Click);
			// 
			// tsPlay
			// 
			this.tsPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsPlay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playOnceToolStripMenuItem,
            this.playLoopingToolStripMenuItem,
            this.playOnceWithRepeatsToolStripMenuItem});
			this.tsPlay.Image = global::SPNATI_Character_Editor.Properties.Resources.TimelinePlayOnce;
			this.tsPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPlay.Name = "tsPlay";
			this.tsPlay.Size = new System.Drawing.Size(32, 22);
			this.tsPlay.Text = "Play";
			this.tsPlay.ButtonClick += new System.EventHandler(this.tsPlay_Click);
			// 
			// playOnceToolStripMenuItem
			// 
			this.playOnceToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.TimelinePlayOnce;
			this.playOnceToolStripMenuItem.Name = "playOnceToolStripMenuItem";
			this.playOnceToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.playOnceToolStripMenuItem.Text = "Play to End";
			this.playOnceToolStripMenuItem.Click += new System.EventHandler(this.playOnceToolStripMenuItem_Click);
			// 
			// playLoopingToolStripMenuItem
			// 
			this.playLoopingToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.TimelinePlay;
			this.playLoopingToolStripMenuItem.Name = "playLoopingToolStripMenuItem";
			this.playLoopingToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.playLoopingToolStripMenuItem.Text = "Play Repeating";
			this.playLoopingToolStripMenuItem.Click += new System.EventHandler(this.playLoopingToolStripMenuItem_Click);
			// 
			// playOnceWithRepeatsToolStripMenuItem
			// 
			this.playOnceWithRepeatsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.TimelinePlayLoops;
			this.playOnceWithRepeatsToolStripMenuItem.Name = "playOnceWithRepeatsToolStripMenuItem";
			this.playOnceWithRepeatsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.playOnceWithRepeatsToolStripMenuItem.Text = "Play Once With Loops";
			this.playOnceWithRepeatsToolStripMenuItem.Click += new System.EventHandler(this.playOnceWithRepeatsToolStripMenuItem_Click);
			// 
			// tsNext
			// 
			this.tsNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsNext.Image = global::SPNATI_Character_Editor.Properties.Resources.NextFrame;
			this.tsNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsNext.Name = "tsNext";
			this.tsNext.Size = new System.Drawing.Size(23, 22);
			this.tsNext.Text = "Jump to next frame";
			this.tsNext.Click += new System.EventHandler(this.tsNext_Click);
			// 
			// tsLast
			// 
			this.tsLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsLast.Image = global::SPNATI_Character_Editor.Properties.Resources.LastFrame;
			this.tsLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsLast.Name = "tsLast";
			this.tsLast.Size = new System.Drawing.Size(23, 22);
			this.tsLast.Text = "Jump to End";
			this.tsLast.Click += new System.EventHandler(this.tsLast_Click);
			// 
			// tsZoomOut
			// 
			this.tsZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsZoomOut.Image = global::SPNATI_Character_Editor.Properties.Resources.ZoomOut;
			this.tsZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsZoomOut.Name = "tsZoomOut";
			this.tsZoomOut.Size = new System.Drawing.Size(23, 22);
			this.tsZoomOut.Text = "Zoom out";
			this.tsZoomOut.Click += new System.EventHandler(this.tsZoomOut_Click);
			// 
			// tsZoomIn
			// 
			this.tsZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsZoomIn.Image = global::SPNATI_Character_Editor.Properties.Resources.ZoomIn;
			this.tsZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsZoomIn.Name = "tsZoomIn";
			this.tsZoomIn.Size = new System.Drawing.Size(23, 20);
			this.tsZoomIn.Text = "Zoom in";
			this.tsZoomIn.Click += new System.EventHandler(this.tsZoomIn_Click);
			// 
			// Timeline
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.editMenu;
			this.Controls.Add(this.container);
			this.Controls.Add(this.panelHeader);
			this.Controls.Add(this.tools);
			this.Controls.Add(this.panelAxis);
			this.Name = "Timeline";
			this.Size = new System.Drawing.Size(877, 135);
			this.Load += new System.EventHandler(this.Timeline_Load);
			this.Resize += new System.EventHandler(this.Timeline_Resize);
			this.container.ResumeLayout(false);
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			this.editMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.CommonControls.SelectablePanel panel;
		private Desktop.CommonControls.DBPanel container;
		private Desktop.CommonControls.DBPanel panelAxis;
		private Desktop.CommonControls.SelectablePanel panelHeader;
		private System.Windows.Forms.Timer tmrTick;
		private System.Windows.Forms.ToolStrip tools;
		private System.Windows.Forms.ToolStripSplitButton tsPlay;
		private System.Windows.Forms.ToolStripButton tsFirst;
		private System.Windows.Forms.ToolStripButton tsLast;
		private System.Windows.Forms.ToolStripMenuItem tsCut;
		private System.Windows.Forms.ToolStripMenuItem tsCopy;
		private System.Windows.Forms.ToolStripMenuItem tsPaste;
		private System.Windows.Forms.ToolStripMenuItem tsDuplicate;
		private System.Windows.Forms.ToolStripMenuItem tsDelete;
		private System.Windows.Forms.ToolStripMenuItem tsUndo;
		private System.Windows.Forms.ToolStripMenuItem tsRedo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsPrevious;
		private System.Windows.Forms.ToolStripButton tsNext;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolTip tooltip;
		private System.Windows.Forms.Timer tmrTooltip;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ContextMenuStrip editMenu;
		private System.Windows.Forms.ToolStripButton tsZoomOut;
		private System.Windows.Forms.ToolStripButton tsZoomIn;
		private System.Windows.Forms.ToolStripMenuItem playOnceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playLoopingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playOnceWithRepeatsToolStripMenuItem;
	}
}

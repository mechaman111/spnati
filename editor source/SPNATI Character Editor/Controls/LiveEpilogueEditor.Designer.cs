namespace SPNATI_Character_Editor.Controls
{
	partial class LiveEpilogueEditor
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
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.tsMainMenu = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.lstScenes = new Desktop.CommonControls.RefreshableListBox();
			this.tsToolbar = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.lblDataCaption = new Desktop.Skinning.SkinnedLabel();
			this.subTable = new Desktop.CommonControls.PropertyTable();
			this.lblSubTable = new Desktop.Skinning.SkinnedLabel();
			this.tmrRealtime = new System.Windows.Forms.Timer(this.components);
			this.tsAddSprite = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveSprite = new System.Windows.Forms.ToolStripButton();
			this.tsAddTransition = new System.Windows.Forms.ToolStripButton();
			this.tsMoveUp = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.tsAdd = new System.Windows.Forms.ToolStripDropDownButton();
			this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addSpeechBubbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addEmitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addWaitForInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.tsRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsAddKeyframe = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveKeyframe = new System.Windows.Forms.ToolStripButton();
			this.tsAddEndFrame = new System.Windows.Forms.ToolStripButton();
			this.tsTypeNormal = new System.Windows.Forms.ToolStripButton();
			this.tsTypeSplit = new System.Windows.Forms.ToolStripButton();
			this.tsTypeBegin = new System.Windows.Forms.ToolStripButton();
			this.timeline = new SPNATI_Character_Editor.EpilogueEditor.Timeline();
			this.canvas = new SPNATI_Character_Editor.EpilogueEditor.LiveCanvas();
			this.openFileDialog1 = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tsMainMenu.SuspendLayout();
			this.tsToolbar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1131, 675);
			this.splitContainer1.SplitterDistance = 175;
			this.splitContainer1.TabIndex = 5;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tsMainMenu);
			this.splitContainer3.Panel1.Controls.Add(this.lstScenes);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.tsToolbar);
			this.splitContainer3.Panel2.Controls.Add(this.timeline);
			this.splitContainer3.Size = new System.Drawing.Size(1131, 175);
			this.splitContainer3.SplitterDistance = 165;
			this.splitContainer3.TabIndex = 0;
			// 
			// tsMainMenu
			// 
			this.tsMainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsAddSprite,
            this.tsRemoveSprite,
            this.tsAddTransition,
            this.toolStripSeparator3,
            this.tsMoveUp,
            this.toolStripButton1});
			this.tsMainMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMainMenu.Name = "tsMainMenu";
			this.tsMainMenu.Size = new System.Drawing.Size(165, 25);
			this.tsMainMenu.TabIndex = 7;
			this.tsMainMenu.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
			this.toolStripLabel1.Text = "Scenes";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// lstScenes
			// 
			this.lstScenes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstScenes.BackColor = System.Drawing.Color.White;
			this.lstScenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstScenes.ForeColor = System.Drawing.Color.Black;
			this.lstScenes.FormattingEnabled = true;
			this.lstScenes.IntegralHeight = false;
			this.lstScenes.Location = new System.Drawing.Point(0, 25);
			this.lstScenes.Margin = new System.Windows.Forms.Padding(0);
			this.lstScenes.Name = "lstScenes";
			this.lstScenes.Size = new System.Drawing.Size(165, 150);
			this.lstScenes.TabIndex = 8;
			this.lstScenes.SelectedIndexChanged += new System.EventHandler(this.lstScenes_SelectedIndexChanged);
			// 
			// tsToolbar
			// 
			this.tsToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsRemove,
            this.toolStripSeparator1,
            this.tsRefresh,
            this.toolStripSeparator2,
            this.tsAddKeyframe,
            this.tsRemoveKeyframe,
            this.tsAddEndFrame,
            this.toolStripSeparator4,
            this.tsTypeNormal,
            this.tsTypeSplit,
            this.tsTypeBegin});
			this.tsToolbar.Location = new System.Drawing.Point(0, 0);
			this.tsToolbar.Name = "tsToolbar";
			this.tsToolbar.Size = new System.Drawing.Size(962, 25);
			this.tsToolbar.TabIndex = 8;
			this.tsToolbar.Text = "toolStrip1";
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
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.canvas);
			this.splitContainer2.Size = new System.Drawing.Size(1131, 496);
			this.splitContainer2.SplitterDistance = 376;
			this.splitContainer2.TabIndex = 0;
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.table);
			this.splitContainer4.Panel1.Controls.Add(this.lblDataCaption);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.subTable);
			this.splitContainer4.Panel2.Controls.Add(this.lblSubTable);
			this.splitContainer4.Size = new System.Drawing.Size(376, 496);
			this.splitContainer4.SplitterDistance = 248;
			this.splitContainer4.TabIndex = 8;
			// 
			// table
			// 
			this.table.AllowDelete = false;
			this.table.AllowFavorites = false;
			this.table.AllowHelp = true;
			this.table.AllowMacros = false;
			this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.table.BackColor = System.Drawing.Color.White;
			this.table.Data = null;
			this.table.Enabled = false;
			this.table.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.table.HideAddField = true;
			this.table.HideSpeedButtons = true;
			this.table.Location = new System.Drawing.Point(3, 25);
			this.table.Margin = new System.Windows.Forms.Padding(0);
			this.table.ModifyingProperty = null;
			this.table.Name = "table";
			this.table.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.table.PlaceholderText = null;
			this.table.PreserveControls = true;
			this.table.PreviewData = null;
			this.table.RemoveCaption = "Remove";
			this.table.RowHeaderWidth = 0F;
			this.table.RunInitialAddEvents = false;
			this.table.Size = new System.Drawing.Size(370, 223);
			this.table.Sorted = true;
			this.table.TabIndex = 6;
			this.table.UndoManager = null;
			this.table.UseAutoComplete = true;
			// 
			// lblDataCaption
			// 
			this.lblDataCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDataCaption.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblDataCaption.ForeColor = System.Drawing.Color.Blue;
			this.lblDataCaption.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.lblDataCaption.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblDataCaption.Location = new System.Drawing.Point(3, 0);
			this.lblDataCaption.Name = "lblDataCaption";
			this.lblDataCaption.Size = new System.Drawing.Size(370, 25);
			this.lblDataCaption.TabIndex = 7;
			this.lblDataCaption.Text = "Data";
			this.lblDataCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// subTable
			// 
			this.subTable.AllowDelete = false;
			this.subTable.AllowFavorites = false;
			this.subTable.AllowHelp = true;
			this.subTable.AllowMacros = false;
			this.subTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.subTable.BackColor = System.Drawing.Color.White;
			this.subTable.Data = null;
			this.subTable.Enabled = false;
			this.subTable.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.subTable.HideAddField = true;
			this.subTable.HideSpeedButtons = true;
			this.subTable.Location = new System.Drawing.Point(3, 23);
			this.subTable.Margin = new System.Windows.Forms.Padding(0);
			this.subTable.ModifyingProperty = null;
			this.subTable.Name = "subTable";
			this.subTable.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.subTable.PlaceholderText = null;
			this.subTable.PreserveControls = true;
			this.subTable.PreviewData = null;
			this.subTable.RemoveCaption = "Remove";
			this.subTable.RowHeaderWidth = 0F;
			this.subTable.RunInitialAddEvents = false;
			this.subTable.Size = new System.Drawing.Size(370, 223);
			this.subTable.Sorted = true;
			this.subTable.TabIndex = 8;
			this.subTable.UndoManager = null;
			this.subTable.UseAutoComplete = true;
			// 
			// lblSubTable
			// 
			this.lblSubTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSubTable.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblSubTable.ForeColor = System.Drawing.Color.Blue;
			this.lblSubTable.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.lblSubTable.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblSubTable.Location = new System.Drawing.Point(3, -2);
			this.lblSubTable.Name = "lblSubTable";
			this.lblSubTable.Size = new System.Drawing.Size(370, 25);
			this.lblSubTable.TabIndex = 9;
			this.lblSubTable.Text = "Data";
			this.lblSubTable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// tmrRealtime
			// 
			this.tmrRealtime.Interval = 30;
			this.tmrRealtime.Tick += new System.EventHandler(this.tmrRealtime_Tick);
			// 
			// tsAddSprite
			// 
			this.tsAddSprite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddSprite.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAddSprite.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddSprite.Name = "tsAddSprite";
			this.tsAddSprite.Size = new System.Drawing.Size(23, 22);
			this.tsAddSprite.Text = "Add Scene";
			this.tsAddSprite.Click += new System.EventHandler(this.tsAddSprite_Click);
			// 
			// tsRemoveSprite
			// 
			this.tsRemoveSprite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveSprite.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemoveSprite.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveSprite.Name = "tsRemoveSprite";
			this.tsRemoveSprite.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveSprite.Text = "Remove Scene";
			this.tsRemoveSprite.Click += new System.EventHandler(this.tsRemoveSprite_Click);
			// 
			// tsAddTransition
			// 
			this.tsAddTransition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddTransition.Image = global::SPNATI_Character_Editor.Properties.Resources.AddTransition;
			this.tsAddTransition.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddTransition.Name = "tsAddTransition";
			this.tsAddTransition.Size = new System.Drawing.Size(23, 22);
			this.tsAddTransition.Text = "Add Transition";
			this.tsAddTransition.Click += new System.EventHandler(this.tsAddTransition_Click);
			// 
			// tsMoveUp
			// 
			this.tsMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsMoveUp.Image = global::SPNATI_Character_Editor.Properties.Resources.UpArrow;
			this.tsMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsMoveUp.Name = "tsMoveUp";
			this.tsMoveUp.Size = new System.Drawing.Size(23, 22);
			this.tsMoveUp.Text = "Move Up";
			this.tsMoveUp.Click += new System.EventHandler(this.tsMoveUp_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::SPNATI_Character_Editor.Properties.Resources.DownArrow;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "Move Down";
			this.toolStripButton1.Click += new System.EventHandler(this.tsMoveDown_Click);
			// 
			// tsAdd
			// 
			this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpriteToolStripMenuItem,
            this.addSpeechBubbleToolStripMenuItem,
            this.addEmitterToolStripMenuItem,
            this.addWaitForInputToolStripMenuItem});
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(29, 22);
			this.tsAdd.Text = "Add Object";
			// 
			// addSpriteToolStripMenuItem
			// 
			this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
			this.addSpriteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.addSpriteToolStripMenuItem.Text = "Add Sprite";
			this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.addSpriteToolStripMenuItem_Click);
			// 
			// addSpeechBubbleToolStripMenuItem
			// 
			this.addSpeechBubbleToolStripMenuItem.Name = "addSpeechBubbleToolStripMenuItem";
			this.addSpeechBubbleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.addSpeechBubbleToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.addSpeechBubbleToolStripMenuItem.Text = "Add Speech Bubble";
			this.addSpeechBubbleToolStripMenuItem.Click += new System.EventHandler(this.addSpeechBubbleToolStripMenuItem_Click);
			// 
			// addEmitterToolStripMenuItem
			// 
			this.addEmitterToolStripMenuItem.Name = "addEmitterToolStripMenuItem";
			this.addEmitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.addEmitterToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.addEmitterToolStripMenuItem.Text = "Add Emitter";
			this.addEmitterToolStripMenuItem.Click += new System.EventHandler(this.addEmitterToolStripMenuItem_Click);
			// 
			// addWaitForInputToolStripMenuItem
			// 
			this.addWaitForInputToolStripMenuItem.Name = "addWaitForInputToolStripMenuItem";
			this.addWaitForInputToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
			this.addWaitForInputToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.addWaitForInputToolStripMenuItem.Text = "Add Pause";
			this.addWaitForInputToolStripMenuItem.Click += new System.EventHandler(this.addWaitForInputToolStripMenuItem_Click);
			// 
			// tsRemove
			// 
			this.tsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(23, 22);
			this.tsRemove.Text = "Remove Object";
			this.tsRemove.Click += new System.EventHandler(this.tsRemove_Click);
			// 
			// tsRefresh
			// 
			this.tsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRefresh.Image = global::SPNATI_Character_Editor.Properties.Resources.Refresh;
			this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRefresh.Name = "tsRefresh";
			this.tsRefresh.Size = new System.Drawing.Size(23, 22);
			this.tsRefresh.Text = "Refresh assets";
			this.tsRefresh.ToolTipText = "Reload sprites from files";
			this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
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
			// tsRemoveKeyframe
			// 
			this.tsRemoveKeyframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveKeyframe.Image = global::SPNATI_Character_Editor.Properties.Resources.RemoveKeyframe;
			this.tsRemoveKeyframe.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveKeyframe.Name = "tsRemoveKeyframe";
			this.tsRemoveKeyframe.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveKeyframe.Text = "Remove Keyframe";
			this.tsRemoveKeyframe.Click += new System.EventHandler(this.tsRemoveKeyframe_Click);
			// 
			// tsAddEndFrame
			// 
			this.tsAddEndFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddEndFrame.Image = global::SPNATI_Character_Editor.Properties.Resources.CopyKeyFrame;
			this.tsAddEndFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddEndFrame.Name = "tsAddEndFrame";
			this.tsAddEndFrame.Size = new System.Drawing.Size(23, 22);
			this.tsAddEndFrame.Text = "Copy first keyframe to end";
			this.tsAddEndFrame.Click += new System.EventHandler(this.tsAddEndFrame_Click);
			// 
			// tsTypeNormal
			// 
			this.tsTypeNormal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsTypeNormal.Image = global::SPNATI_Character_Editor.Properties.Resources.KeyTypeNormal;
			this.tsTypeNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsTypeNormal.Name = "tsTypeNormal";
			this.tsTypeNormal.Size = new System.Drawing.Size(23, 22);
			this.tsTypeNormal.Text = "Normal keyframe";
			this.tsTypeNormal.Click += new System.EventHandler(this.tsTypeNormal_Click);
			// 
			// tsTypeSplit
			// 
			this.tsTypeSplit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsTypeSplit.Image = global::SPNATI_Character_Editor.Properties.Resources.KeyTypeSplit;
			this.tsTypeSplit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsTypeSplit.Name = "tsTypeSplit";
			this.tsTypeSplit.Size = new System.Drawing.Size(23, 22);
			this.tsTypeSplit.Text = "Split keyframe";
			this.tsTypeSplit.Click += new System.EventHandler(this.tsTypeSplit_Click);
			// 
			// tsTypeBegin
			// 
			this.tsTypeBegin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsTypeBegin.Image = global::SPNATI_Character_Editor.Properties.Resources.KeyTypeBegin;
			this.tsTypeBegin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsTypeBegin.Name = "tsTypeBegin";
			this.tsTypeBegin.Size = new System.Drawing.Size(23, 22);
			this.tsTypeBegin.Text = "Begin keyframe";
			this.tsTypeBegin.Click += new System.EventHandler(this.tsTypeBegin_Click);
			// 
			// timeline
			// 
			this.timeline.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.timeline.CommandHistory = null;
			this.timeline.CurrentTime = 0F;
			this.timeline.ElapsedTime = 0F;
			this.timeline.Enabled = false;
			this.timeline.Location = new System.Drawing.Point(0, 25);
			this.timeline.Name = "timeline";
			this.timeline.PauseOnBreaks = false;
			this.timeline.PlaybackAwaitingInput = false;
			this.timeline.PlaybackTime = 0F;
			this.timeline.Size = new System.Drawing.Size(962, 150);
			this.timeline.TabIndex = 1;
			// 
			// canvas
			// 
			this.canvas.AllowZoom = true;
			this.canvas.CustomDraw = false;
			this.canvas.DisallowEdit = false;
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Enabled = false;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(751, 496);
			this.canvas.TabIndex = 0;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "";
			this.openFileDialog1.IncludeOpponents = false;
			this.openFileDialog1.UseAbsolutePaths = false;
			// 
			// LiveEpilogueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "LiveEpilogueEditor";
			this.Size = new System.Drawing.Size(1131, 675);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.tsMainMenu.ResumeLayout(false);
			this.tsMainMenu.PerformLayout();
			this.tsToolbar.ResumeLayout(false);
			this.tsToolbar.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private SPNATI_Character_Editor.EpilogueEditor.LiveCanvas canvas;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.CommonControls.RefreshableListBox lstScenes;
		private System.Windows.Forms.ToolStrip tsMainMenu;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripButton tsAddSprite;
		private System.Windows.Forms.ToolStripButton tsRemoveSprite;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private Desktop.CommonControls.PropertyTable table;
		private SPNATI_Character_Editor.EpilogueEditor.Timeline timeline;
		private System.Windows.Forms.ToolStrip tsToolbar;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private System.Windows.Forms.ToolStripDropDownButton tsAdd;
		private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addSpeechBubbleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addEmitterToolStripMenuItem;
		private CharacterImageDialog openFileDialog1;
		private System.Windows.Forms.Timer tmrRealtime;
		private Desktop.Skinning.SkinnedLabel lblDataCaption;
		private System.Windows.Forms.ToolStripMenuItem addWaitForInputToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsAddKeyframe;
		private System.Windows.Forms.ToolStripButton tsRemoveKeyframe;
		private System.Windows.Forms.ToolStripButton tsAddEndFrame;
		private System.Windows.Forms.ToolStripButton tsAddTransition;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsMoveUp;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private Desktop.CommonControls.PropertyTable subTable;
		private Desktop.Skinning.SkinnedLabel lblSubTable;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton tsTypeNormal;
		private System.Windows.Forms.ToolStripButton tsTypeSplit;
		private System.Windows.Forms.ToolStripButton tsTypeBegin;
	}
}

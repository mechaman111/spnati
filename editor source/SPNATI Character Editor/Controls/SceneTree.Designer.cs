namespace SPNATI_Character_Editor.Controls
{
	partial class SceneTree
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
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAddScene = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.tsAddTransition = new System.Windows.Forms.ToolStripButton();
			this.tsAddDirective = new System.Windows.Forms.ToolStripSplitButton();
			this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.addEmitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.emitParticleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.addSpeechBubbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeSpeechBubbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearSpeechBubblesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.moveSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveZoomCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopAnimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.fadeEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jumpToSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.waitForAnimationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.waitForInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.userPromptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsAddKeyframe = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsUp = new System.Windows.Forms.ToolStripButton();
			this.tsDown = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsCut = new System.Windows.Forms.ToolStripButton();
			this.tsCopy = new System.Windows.Forms.ToolStripButton();
			this.tsPaste = new System.Windows.Forms.ToolStripButton();
			this.tsDuplicate = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsLock = new System.Windows.Forms.ToolStripButton();
			this.tsCollapse = new System.Windows.Forms.ToolStripButton();
			this.tsExpandAll = new System.Windows.Forms.ToolStripButton();
			this.lblDragger = new System.Windows.Forms.Label();
			this.treeScenes = new Desktop.CommonControls.DBTreeView();
			this.openFileDialog = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip1);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.lblDragger);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.treeScenes);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(357, 380);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(357, 405);
			this.toolStripContainer1.TabIndex = 4;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddScene,
            this.tsRemove,
            this.tsAddTransition,
            this.tsAddDirective,
            this.tsAddKeyframe,
            this.toolStripSeparator1,
            this.tsUp,
            this.tsDown,
            this.toolStripSeparator5,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsDuplicate,
            this.toolStripSeparator7,
            this.tsLock,
            this.tsCollapse,
            this.tsExpandAll});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(357, 25);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsAddScene
			// 
			this.tsAddScene.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddScene.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAddScene.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddScene.Name = "tsAddScene";
			this.tsAddScene.Size = new System.Drawing.Size(23, 22);
			this.tsAddScene.Text = "Add Scene";
			this.tsAddScene.ToolTipText = "Add a new scene";
			this.tsAddScene.Click += new System.EventHandler(this.TsAdd_ButtonClick);
			// 
			// tsRemove
			// 
			this.tsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(23, 22);
			this.tsRemove.Text = "Remove";
			this.tsRemove.ToolTipText = "Remove selected node";
			this.tsRemove.Click += new System.EventHandler(this.TsRemove_Click);
			// 
			// tsAddTransition
			// 
			this.tsAddTransition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddTransition.Image = global::SPNATI_Character_Editor.Properties.Resources.AddTransition;
			this.tsAddTransition.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddTransition.Name = "tsAddTransition";
			this.tsAddTransition.Size = new System.Drawing.Size(23, 22);
			this.tsAddTransition.Text = "Add Transition";
			this.tsAddTransition.ToolTipText = "Add Scene Transition";
			this.tsAddTransition.Click += new System.EventHandler(this.tsAddTransition_Click);
			// 
			// tsAddDirective
			// 
			this.tsAddDirective.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddDirective.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpriteToolStripMenuItem,
            this.removeObjectToolStripMenuItem,
            this.toolStripSeparator6,
            this.addEmitterToolStripMenuItem,
            this.emitParticleToolStripMenuItem,
            this.toolStripSeparator8,
            this.addSpeechBubbleToolStripMenuItem,
            this.removeSpeechBubbleToolStripMenuItem,
            this.clearSpeechBubblesToolStripMenuItem,
            this.toolStripSeparator2,
            this.moveSpriteToolStripMenuItem,
            this.moveZoomCameraToolStripMenuItem,
            this.stopAnimationToolStripMenuItem,
            this.toolStripSeparator4,
            this.fadeEffectToolStripMenuItem,
            this.jumpToSceneToolStripMenuItem,
            this.toolStripSeparator3,
            this.waitForAnimationsToolStripMenuItem,
            this.waitForInputToolStripMenuItem,
            this.userPromptToolStripMenuItem});
			this.tsAddDirective.Image = global::SPNATI_Character_Editor.Properties.Resources.AddChildNode;
			this.tsAddDirective.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddDirective.Name = "tsAddDirective";
			this.tsAddDirective.Size = new System.Drawing.Size(32, 22);
			this.tsAddDirective.Text = "Add";
			this.tsAddDirective.ToolTipText = "Add a new directive to the selected scene";
			this.tsAddDirective.ButtonClick += new System.EventHandler(this.TsAddDirective_ButtonClick);
			// 
			// addSpriteToolStripMenuItem
			// 
			this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
			this.addSpriteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.addSpriteToolStripMenuItem.Tag = "sprite";
			this.addSpriteToolStripMenuItem.Text = "Add Sprite";
			this.addSpriteToolStripMenuItem.ToolTipText = "Add a sprite to the scene";
			this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// removeObjectToolStripMenuItem
			// 
			this.removeObjectToolStripMenuItem.Name = "removeObjectToolStripMenuItem";
			this.removeObjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.removeObjectToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.removeObjectToolStripMenuItem.Tag = "remove";
			this.removeObjectToolStripMenuItem.Text = "Remove Sprite/Emitter";
			this.removeObjectToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(274, 6);
			// 
			// addEmitterToolStripMenuItem
			// 
			this.addEmitterToolStripMenuItem.Name = "addEmitterToolStripMenuItem";
			this.addEmitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.addEmitterToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.addEmitterToolStripMenuItem.Tag = "emitter";
			this.addEmitterToolStripMenuItem.Text = "Add Emitter";
			this.addEmitterToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// emitParticleToolStripMenuItem
			// 
			this.emitParticleToolStripMenuItem.Name = "emitParticleToolStripMenuItem";
			this.emitParticleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
			this.emitParticleToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.emitParticleToolStripMenuItem.Tag = "emit";
			this.emitParticleToolStripMenuItem.Text = "Emit Particle";
			this.emitParticleToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(274, 6);
			// 
			// addSpeechBubbleToolStripMenuItem
			// 
			this.addSpeechBubbleToolStripMenuItem.Name = "addSpeechBubbleToolStripMenuItem";
			this.addSpeechBubbleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
			this.addSpeechBubbleToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.addSpeechBubbleToolStripMenuItem.Tag = "text";
			this.addSpeechBubbleToolStripMenuItem.Text = "Add Speech Bubble";
			this.addSpeechBubbleToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// removeSpeechBubbleToolStripMenuItem
			// 
			this.removeSpeechBubbleToolStripMenuItem.Name = "removeSpeechBubbleToolStripMenuItem";
			this.removeSpeechBubbleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
			this.removeSpeechBubbleToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.removeSpeechBubbleToolStripMenuItem.Tag = "clear";
			this.removeSpeechBubbleToolStripMenuItem.Text = "Remove Speech Bubble";
			this.removeSpeechBubbleToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// clearSpeechBubblesToolStripMenuItem
			// 
			this.clearSpeechBubblesToolStripMenuItem.Name = "clearSpeechBubblesToolStripMenuItem";
			this.clearSpeechBubblesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D7)));
			this.clearSpeechBubblesToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.clearSpeechBubblesToolStripMenuItem.Tag = "clear-all";
			this.clearSpeechBubblesToolStripMenuItem.Text = "Clear Speech Bubbles";
			this.clearSpeechBubblesToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(274, 6);
			// 
			// moveSpriteToolStripMenuItem
			// 
			this.moveSpriteToolStripMenuItem.Name = "moveSpriteToolStripMenuItem";
			this.moveSpriteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D8)));
			this.moveSpriteToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.moveSpriteToolStripMenuItem.Tag = "move";
			this.moveSpriteToolStripMenuItem.Text = "Animate Sprite/Emitter";
			this.moveSpriteToolStripMenuItem.ToolTipText = "Transform a sprite on screen";
			this.moveSpriteToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// moveZoomCameraToolStripMenuItem
			// 
			this.moveZoomCameraToolStripMenuItem.Name = "moveZoomCameraToolStripMenuItem";
			this.moveZoomCameraToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D9)));
			this.moveZoomCameraToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.moveZoomCameraToolStripMenuItem.Tag = "camera";
			this.moveZoomCameraToolStripMenuItem.Text = "Move/Zoom Camera";
			this.moveZoomCameraToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// stopAnimationToolStripMenuItem
			// 
			this.stopAnimationToolStripMenuItem.Name = "stopAnimationToolStripMenuItem";
			this.stopAnimationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
			this.stopAnimationToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.stopAnimationToolStripMenuItem.Tag = "stop";
			this.stopAnimationToolStripMenuItem.Text = "Stop Animation";
			this.stopAnimationToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(274, 6);
			// 
			// fadeEffectToolStripMenuItem
			// 
			this.fadeEffectToolStripMenuItem.Name = "fadeEffectToolStripMenuItem";
			this.fadeEffectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D1)));
			this.fadeEffectToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.fadeEffectToolStripMenuItem.Tag = "fade";
			this.fadeEffectToolStripMenuItem.Text = "Fade Effect";
			this.fadeEffectToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// jumpToSceneToolStripMenuItem
			// 
			this.jumpToSceneToolStripMenuItem.Name = "jumpToSceneToolStripMenuItem";
			this.jumpToSceneToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D2)));
			this.jumpToSceneToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.jumpToSceneToolStripMenuItem.Tag = "jump";
			this.jumpToSceneToolStripMenuItem.Text = "Jump to Scene";
			this.jumpToSceneToolStripMenuItem.Visible = false;
			this.jumpToSceneToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(274, 6);
			// 
			// waitForAnimationsToolStripMenuItem
			// 
			this.waitForAnimationsToolStripMenuItem.Name = "waitForAnimationsToolStripMenuItem";
			this.waitForAnimationsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Space)));
			this.waitForAnimationsToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.waitForAnimationsToolStripMenuItem.Tag = "wait";
			this.waitForAnimationsToolStripMenuItem.Text = "Wait for Animations";
			this.waitForAnimationsToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// waitForInputToolStripMenuItem
			// 
			this.waitForInputToolStripMenuItem.Name = "waitForInputToolStripMenuItem";
			this.waitForInputToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
			this.waitForInputToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.waitForInputToolStripMenuItem.Tag = "pause";
			this.waitForInputToolStripMenuItem.Text = "Wait For Input";
			this.waitForInputToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// userPromptToolStripMenuItem
			// 
			this.userPromptToolStripMenuItem.Name = "userPromptToolStripMenuItem";
			this.userPromptToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)));
			this.userPromptToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
			this.userPromptToolStripMenuItem.Tag = "prompt";
			this.userPromptToolStripMenuItem.Text = "User Prompt";
			this.userPromptToolStripMenuItem.Visible = false;
			this.userPromptToolStripMenuItem.Click += new System.EventHandler(this.AddDirectiveToolstripItem_Click);
			// 
			// tsAddKeyframe
			// 
			this.tsAddKeyframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddKeyframe.Image = global::SPNATI_Character_Editor.Properties.Resources.AddKeyframe;
			this.tsAddKeyframe.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddKeyframe.Name = "tsAddKeyframe";
			this.tsAddKeyframe.Size = new System.Drawing.Size(23, 22);
			this.tsAddKeyframe.Text = "Add Keyframe";
			this.tsAddKeyframe.Click += new System.EventHandler(this.TsAddKeyframe_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsUp
			// 
			this.tsUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsUp.Image = global::SPNATI_Character_Editor.Properties.Resources.UpArrow;
			this.tsUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsUp.Name = "tsUp";
			this.tsUp.Size = new System.Drawing.Size(23, 22);
			this.tsUp.Text = "Up";
			this.tsUp.ToolTipText = "Move selected item up";
			this.tsUp.Click += new System.EventHandler(this.TsUp_Click);
			// 
			// tsDown
			// 
			this.tsDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsDown.Image = global::SPNATI_Character_Editor.Properties.Resources.DownArrow;
			this.tsDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDown.Name = "tsDown";
			this.tsDown.Size = new System.Drawing.Size(23, 22);
			this.tsDown.Text = "Up";
			this.tsDown.ToolTipText = "Move selected item down";
			this.tsDown.Click += new System.EventHandler(this.TsDown_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			// 
			// tsLock
			// 
			this.tsLock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsLock.Image = global::SPNATI_Character_Editor.Properties.Resources.Lock;
			this.tsLock.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsLock.Name = "tsLock";
			this.tsLock.Size = new System.Drawing.Size(23, 22);
			this.tsLock.Text = "Lock changes in canvas";
			this.tsLock.Click += new System.EventHandler(this.tsLock_Click);
			// 
			// tsCollapse
			// 
			this.tsCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCollapse.Image = global::SPNATI_Character_Editor.Properties.Resources.CollapseAll;
			this.tsCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCollapse.Name = "tsCollapse";
			this.tsCollapse.Size = new System.Drawing.Size(23, 22);
			this.tsCollapse.Text = "Collapse all";
			this.tsCollapse.Click += new System.EventHandler(this.tsCollapse_Click);
			// 
			// tsExpandAll
			// 
			this.tsExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsExpandAll.Image = global::SPNATI_Character_Editor.Properties.Resources.ExpandAll;
			this.tsExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsExpandAll.Name = "tsExpandAll";
			this.tsExpandAll.Size = new System.Drawing.Size(23, 20);
			this.tsExpandAll.Text = "Expand all";
			this.tsExpandAll.Click += new System.EventHandler(this.tsExpandAll_Click);
			// 
			// lblDragger
			// 
			this.lblDragger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDragger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDragger.Location = new System.Drawing.Point(6, 119);
			this.lblDragger.Name = "lblDragger";
			this.lblDragger.Size = new System.Drawing.Size(328, 2);
			this.lblDragger.TabIndex = 3;
			this.lblDragger.Visible = false;
			// 
			// treeScenes
			// 
			this.treeScenes.AllowDrop = true;
			this.treeScenes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeScenes.HideSelection = false;
			this.treeScenes.Location = new System.Drawing.Point(0, 25);
			this.treeScenes.Name = "treeScenes";
			this.treeScenes.Size = new System.Drawing.Size(357, 355);
			this.treeScenes.TabIndex = 2;
			this.treeScenes.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeScenes_ItemDrag);
			this.treeScenes.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeScenes_DragDrop);
			this.treeScenes.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeScenes_DragEnter);
			this.treeScenes.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeScenes_DragOver);
			this.treeScenes.DragLeave += new System.EventHandler(this.TreeScenes_DragLeave);
			this.treeScenes.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.TreeScenes_QueryContinueDrag);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "";
			this.openFileDialog.IncludeOpponents = false;
			this.openFileDialog.UseAbsolutePaths = false;
			// 
			// SceneTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Enabled = false;
			this.Name = "SceneTree";
			this.Size = new System.Drawing.Size(357, 405);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.DBTreeView treeScenes;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsAddScene;
		private System.Windows.Forms.ToolStripSplitButton tsAddDirective;
		private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveSpriteToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsUp;
		private System.Windows.Forms.ToolStripButton tsDown;
		private System.Windows.Forms.Label lblDragger;
		private System.Windows.Forms.ToolStripMenuItem moveZoomCameraToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem waitForAnimationsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopAnimationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem waitForInputToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addSpeechBubbleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeSpeechBubbleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearSpeechBubblesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem fadeEffectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsAddKeyframe;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton tsCut;
		private System.Windows.Forms.ToolStripButton tsCopy;
		private System.Windows.Forms.ToolStripButton tsPaste;
		private System.Windows.Forms.ToolStripButton tsDuplicate;
		private System.Windows.Forms.ToolStripButton tsAddTransition;
		private System.Windows.Forms.ToolStripMenuItem addEmitterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeObjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripButton tsLock;
		private System.Windows.Forms.ToolStripMenuItem emitParticleToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private CharacterImageDialog openFileDialog;
		private System.Windows.Forms.ToolStripMenuItem jumpToSceneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem userPromptToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton tsCollapse;
		private System.Windows.Forms.ToolStripButton tsExpandAll;
	}
}

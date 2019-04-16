namespace SPNATI_Character_Editor.EpilogueEditor
{
	partial class PoseEditor
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
			this.tsMainMenu = new System.Windows.Forms.ToolStrip();
			this.tsAddSprite = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveSprite = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsAddKeyframe = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveKeyframe = new System.Windows.Forms.ToolStripButton();
			this.tsAddEndFrame = new System.Windows.Forms.ToolStripButton();
			this.timeline = new SPNATI_Character_Editor.EpilogueEditor.Timeline();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.lstPoses = new Desktop.CommonControls.RefreshableListBox();
			this.tsPoses = new System.Windows.Forms.ToolStrip();
			this.tsAddPose = new System.Windows.Forms.ToolStripButton();
			this.tsRemovePose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsCut = new System.Windows.Forms.ToolStripButton();
			this.tsCopy = new System.Windows.Forms.ToolStripButton();
			this.tsPaste = new System.Windows.Forms.ToolStripButton();
			this.tsDuplicate = new System.Windows.Forms.ToolStripButton();
			this.lblDataCaption = new System.Windows.Forms.Label();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.canvas = new SPNATI_Character_Editor.EpilogueEditor.LiveCanvas();
			this.openFileDialog1 = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tsMainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tsPoses.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
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
			this.splitContainer1.Panel2.Controls.Add(this.canvas);
			this.splitContainer1.Size = new System.Drawing.Size(1038, 687);
			this.splitContainer1.SplitterDistance = 503;
			this.splitContainer1.TabIndex = 0;
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
			this.splitContainer2.Panel1.Controls.Add(this.tsMainMenu);
			this.splitContainer2.Panel1.Controls.Add(this.timeline);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer2.Size = new System.Drawing.Size(503, 687);
			this.splitContainer2.SplitterDistance = 399;
			this.splitContainer2.TabIndex = 0;
			// 
			// tsMainMenu
			// 
			this.tsMainMenu.Enabled = false;
			this.tsMainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddSprite,
            this.tsRemoveSprite,
            this.toolStripSeparator2,
            this.tsAddKeyframe,
            this.tsRemoveKeyframe,
            this.tsAddEndFrame});
			this.tsMainMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMainMenu.Name = "tsMainMenu";
			this.tsMainMenu.Size = new System.Drawing.Size(503, 25);
			this.tsMainMenu.TabIndex = 2;
			this.tsMainMenu.Text = "toolStrip1";
			// 
			// tsAddSprite
			// 
			this.tsAddSprite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddSprite.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAddSprite.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddSprite.Name = "tsAddSprite";
			this.tsAddSprite.Size = new System.Drawing.Size(23, 22);
			this.tsAddSprite.Text = "Add Sprite";
			this.tsAddSprite.Click += new System.EventHandler(this.AddSprite_Click);
			// 
			// tsRemoveSprite
			// 
			this.tsRemoveSprite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveSprite.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemoveSprite.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveSprite.Name = "tsRemoveSprite";
			this.tsRemoveSprite.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveSprite.Text = "Remove Sprite";
			this.tsRemoveSprite.Click += new System.EventHandler(this.RemoveSprite_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsAddKeyframe
			// 
			this.tsAddKeyframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddKeyframe.Image = global::SPNATI_Character_Editor.Properties.Resources.AddKeyframe;
			this.tsAddKeyframe.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddKeyframe.Name = "tsAddKeyframe";
			this.tsAddKeyframe.Size = new System.Drawing.Size(23, 22);
			this.tsAddKeyframe.Text = "Add Keyframe";
			this.tsAddKeyframe.Click += new System.EventHandler(this.AddKeyframe_Click);
			// 
			// tsRemoveKeyframe
			// 
			this.tsRemoveKeyframe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveKeyframe.Image = global::SPNATI_Character_Editor.Properties.Resources.RemoveKeyframe;
			this.tsRemoveKeyframe.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveKeyframe.Name = "tsRemoveKeyframe";
			this.tsRemoveKeyframe.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveKeyframe.Text = "Remove Keyframe";
			this.tsRemoveKeyframe.Click += new System.EventHandler(this.RemoveKeyframe_Click);
			// 
			// tsAddEndFrame
			// 
			this.tsAddEndFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddEndFrame.Image = global::SPNATI_Character_Editor.Properties.Resources.CopyKeyFrame;
			this.tsAddEndFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddEndFrame.Name = "tsAddEndFrame";
			this.tsAddEndFrame.Size = new System.Drawing.Size(23, 22);
			this.tsAddEndFrame.Text = "Copy first keyframe to end";
			this.tsAddEndFrame.Click += new System.EventHandler(this.CopyFirstFrame_Click);
			// 
			// timeline
			// 
			this.timeline.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.timeline.CommandHistory = null;
			this.timeline.CurrentTime = 0F;
			this.timeline.Enabled = false;
			this.timeline.Location = new System.Drawing.Point(0, 25);
			this.timeline.Margin = new System.Windows.Forms.Padding(0);
			this.timeline.Name = "timeline";
			this.timeline.PlaybackTime = 0F;
			this.timeline.Size = new System.Drawing.Size(503, 374);
			this.timeline.TabIndex = 1;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.lstPoses);
			this.splitContainer3.Panel1.Controls.Add(this.tsPoses);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.lblDataCaption);
			this.splitContainer3.Panel2.Controls.Add(this.table);
			this.splitContainer3.Size = new System.Drawing.Size(503, 284);
			this.splitContainer3.SplitterDistance = 167;
			this.splitContainer3.TabIndex = 6;
			// 
			// lstPoses
			// 
			this.lstPoses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstPoses.FormattingEnabled = true;
			this.lstPoses.Location = new System.Drawing.Point(0, 25);
			this.lstPoses.Name = "lstPoses";
			this.lstPoses.Size = new System.Drawing.Size(167, 259);
			this.lstPoses.TabIndex = 1;
			this.lstPoses.SelectedIndexChanged += new System.EventHandler(this.lstPoses_SelectedIndexChanged);
			this.lstPoses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPoses_KeyDown);
			// 
			// tsPoses
			// 
			this.tsPoses.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsPoses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddPose,
            this.tsRemovePose,
            this.toolStripSeparator1,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsDuplicate});
			this.tsPoses.Location = new System.Drawing.Point(0, 0);
			this.tsPoses.Name = "tsPoses";
			this.tsPoses.Size = new System.Drawing.Size(167, 25);
			this.tsPoses.TabIndex = 0;
			// 
			// tsAddPose
			// 
			this.tsAddPose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddPose.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAddPose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddPose.Name = "tsAddPose";
			this.tsAddPose.Size = new System.Drawing.Size(23, 22);
			this.tsAddPose.Text = "Add Pose";
			this.tsAddPose.Click += new System.EventHandler(this.tsAddPose_Click);
			// 
			// tsRemovePose
			// 
			this.tsRemovePose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemovePose.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemovePose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemovePose.Name = "tsRemovePose";
			this.tsRemovePose.Size = new System.Drawing.Size(23, 22);
			this.tsRemovePose.Text = "Remove Pose";
			this.tsRemovePose.Click += new System.EventHandler(this.tsRemovePose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsCut
			// 
			this.tsCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCut.Image = global::SPNATI_Character_Editor.Properties.Resources.Cut;
			this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCut.Name = "tsCut";
			this.tsCut.Size = new System.Drawing.Size(23, 22);
			this.tsCut.Text = "Cut Pose";
			this.tsCut.Click += new System.EventHandler(this.tsCut_Click);
			// 
			// tsCopy
			// 
			this.tsCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCopy.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopy.Name = "tsCopy";
			this.tsCopy.Size = new System.Drawing.Size(23, 22);
			this.tsCopy.Text = "Copy Pose";
			this.tsCopy.Click += new System.EventHandler(this.tsCopy_Click);
			// 
			// tsPaste
			// 
			this.tsPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsPaste.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPaste.Name = "tsPaste";
			this.tsPaste.Size = new System.Drawing.Size(23, 22);
			this.tsPaste.Text = "Paste Pose";
			this.tsPaste.Click += new System.EventHandler(this.tsPaste_Click);
			// 
			// tsDuplicate
			// 
			this.tsDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsDuplicate.Image = global::SPNATI_Character_Editor.Properties.Resources.Duplicate;
			this.tsDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDuplicate.Name = "tsDuplicate";
			this.tsDuplicate.Size = new System.Drawing.Size(23, 22);
			this.tsDuplicate.Text = "Duplicate Pose";
			this.tsDuplicate.Click += new System.EventHandler(this.tsDuplicate_Click);
			// 
			// lblDataCaption
			// 
			this.lblDataCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDataCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDataCaption.Location = new System.Drawing.Point(-1, -1);
			this.lblDataCaption.Name = "lblDataCaption";
			this.lblDataCaption.Size = new System.Drawing.Size(334, 26);
			this.lblDataCaption.TabIndex = 6;
			this.lblDataCaption.Text = "Data";
			this.lblDataCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// table
			// 
			this.table.AllowDelete = false;
			this.table.AllowFavorites = false;
			this.table.AllowHelp = false;
			this.table.AllowMacros = false;
			this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.table.Data = null;
			this.table.Enabled = false;
			this.table.HideAddField = true;
			this.table.HideSpeedButtons = true;
			this.table.Location = new System.Drawing.Point(0, 25);
			this.table.Name = "table";
			this.table.PlaceholderText = null;
			this.table.PreserveControls = true;
			this.table.PreviewData = null;
			this.table.RemoveCaption = "Remove";
			this.table.RowHeaderWidth = 0F;
			this.table.RunInitialAddEvents = false;
			this.table.Size = new System.Drawing.Size(332, 259);
			this.table.Sorted = true;
			this.table.TabIndex = 5;
			this.table.UndoManager = null;
			this.table.UseAutoComplete = true;
			// 
			// canvas
			// 
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Enabled = false;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Margin = new System.Windows.Forms.Padding(0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(531, 687);
			this.canvas.TabIndex = 19;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "";
			this.openFileDialog1.UseAbsolutePaths = false;
			// 
			// PoseEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "PoseEditor";
			this.Size = new System.Drawing.Size(1038, 687);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tsMainMenu.ResumeLayout(false);
			this.tsMainMenu.PerformLayout();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.tsPoses.ResumeLayout(false);
			this.tsPoses.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.CommonControls.PropertyTable table;
		private Timeline timeline;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.ToolStrip tsPoses;
		private Desktop.CommonControls.RefreshableListBox lstPoses;
		private LiveCanvas canvas;
		private System.Windows.Forms.Label lblDataCaption;
		private System.Windows.Forms.ToolStripButton tsAddPose;
		private System.Windows.Forms.ToolStripButton tsRemovePose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsCut;
		private System.Windows.Forms.ToolStripButton tsCopy;
		private System.Windows.Forms.ToolStripButton tsPaste;
		private System.Windows.Forms.ToolStripButton tsDuplicate;
		private System.Windows.Forms.ToolStrip tsMainMenu;
		private System.Windows.Forms.ToolStripButton tsAddSprite;
		private System.Windows.Forms.ToolStripButton tsRemoveSprite;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsAddKeyframe;
		private System.Windows.Forms.ToolStripButton tsRemoveKeyframe;
		private System.Windows.Forms.ToolStripButton tsAddEndFrame;
		private Controls.CharacterImageDialog openFileDialog1;
	}
}

namespace SPNATI_Character_Editor.Controls.Pipelines
{
	partial class GraphEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEditor));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAddNode = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveNode = new System.Windows.Forms.ToolStripButton();
			this.tsSaveAs = new System.Windows.Forms.ToolStripButton();
			this.panel = new Desktop.Skinning.SkinnedPanel();
			this.tmrPreview = new System.Windows.Forms.Timer(this.components);
			this.skinnedSplitContainer1 = new Desktop.Skinning.SkinnedSplitContainer();
			this.grpPreview = new Desktop.Skinning.SkinnedGroupBox();
			this.picPreview = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer1)).BeginInit();
			this.skinnedSplitContainer1.Panel1.SuspendLayout();
			this.skinnedSplitContainer1.Panel2.SuspendLayout();
			this.skinnedSplitContainer1.SuspendLayout();
			this.grpPreview.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddNode,
            this.tsRemoveNode,
            this.tsSaveAs});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(779, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "tsToolbar";
			// 
			// tsAddNode
			// 
			this.tsAddNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddNode.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddNode.Name = "tsAddNode";
			this.tsAddNode.Size = new System.Drawing.Size(23, 22);
			this.tsAddNode.Text = "Add Node";
			this.tsAddNode.Click += new System.EventHandler(this.tsAddNode_Click);
			// 
			// tsRemoveNode
			// 
			this.tsRemoveNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveNode.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemoveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveNode.Name = "tsRemoveNode";
			this.tsRemoveNode.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveNode.Text = "Remove node";
			this.tsRemoveNode.Click += new System.EventHandler(this.tsRemoveNode_Click);
			// 
			// tsSaveAs
			// 
			this.tsSaveAs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveAs.Image")));
			this.tsSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsSaveAs.Name = "tsSaveAs";
			this.tsSaveAs.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsSaveAs.Size = new System.Drawing.Size(60, 22);
			this.tsSaveAs.Text = "Save As...";
			this.tsSaveAs.Click += new System.EventHandler(this.tsSaveAs_Click);
			// 
			// panel
			// 
			this.panel.AutoScroll = true;
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.panel.Size = new System.Drawing.Size(526, 464);
			this.panel.TabIndex = 1;
			this.panel.TabSide = Desktop.Skinning.TabSide.None;
			this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
			this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
			this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
			this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
			// 
			// tmrPreview
			// 
			this.tmrPreview.Tick += new System.EventHandler(this.tmrPreview_Tick);
			// 
			// skinnedSplitContainer1
			// 
			this.skinnedSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinnedSplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.skinnedSplitContainer1.Location = new System.Drawing.Point(0, 25);
			this.skinnedSplitContainer1.Name = "skinnedSplitContainer1";
			// 
			// skinnedSplitContainer1.Panel1
			// 
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.panel);
			// 
			// skinnedSplitContainer1.Panel2
			// 
			this.skinnedSplitContainer1.Panel2.Controls.Add(this.grpPreview);
			this.skinnedSplitContainer1.Size = new System.Drawing.Size(779, 464);
			this.skinnedSplitContainer1.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedSplitContainer1.SplitterDistance = 526;
			this.skinnedSplitContainer1.TabIndex = 3;
			// 
			// grpPreview
			// 
			this.grpPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpPreview.BackColor = System.Drawing.Color.White;
			this.grpPreview.Controls.Add(this.picPreview);
			this.grpPreview.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpPreview.Image = null;
			this.grpPreview.Location = new System.Drawing.Point(3, 2);
			this.grpPreview.Name = "grpPreview";
			this.grpPreview.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpPreview.ShowIndicatorBar = false;
			this.grpPreview.Size = new System.Drawing.Size(243, 459);
			this.grpPreview.TabIndex = 0;
			this.grpPreview.TabStop = false;
			this.grpPreview.Text = "Preview";
			// 
			// picPreview
			// 
			this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picPreview.Location = new System.Drawing.Point(6, 25);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(231, 428);
			this.picPreview.TabIndex = 0;
			// 
			// GraphEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedSplitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "GraphEditor";
			this.Size = new System.Drawing.Size(779, 489);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.skinnedSplitContainer1.Panel1.ResumeLayout(false);
			this.skinnedSplitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer1)).EndInit();
			this.skinnedSplitContainer1.ResumeLayout(false);
			this.grpPreview.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsAddNode;
		private Desktop.Skinning.SkinnedPanel panel;
		private System.Windows.Forms.Timer tmrPreview;
		private System.Windows.Forms.ToolStripButton tsRemoveNode;
		private Desktop.Skinning.SkinnedSplitContainer skinnedSplitContainer1;
		private Desktop.Skinning.SkinnedGroupBox grpPreview;
		private CharacterImageBox picPreview;
		private System.Windows.Forms.ToolStripButton tsSaveAs;
	}
}

namespace Desktop.CommonControls
{
	partial class DataSlicerControl
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
				_labelBrush.Dispose();
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
			this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
			this.skinnedSplitContainer1 = new Desktop.Skinning.SkinnedSplitContainer();
			this.skinnedSplitContainer2 = new Desktop.Skinning.SkinnedSplitContainer();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAddSlice = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveSlice = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsDown = new System.Windows.Forms.ToolStripButton();
			this.tsUp = new System.Windows.Forms.ToolStripButton();
			this.lstSlices = new Desktop.Skinning.SkinnedListBox();
			this.grpProperties = new Desktop.Skinning.SkinnedGroupBox();
			this.pnlSlice = new System.Windows.Forms.Panel();
			this.skinnedGroupBox3 = new Desktop.Skinning.SkinnedGroupBox();
			this.graph = new Desktop.Skinning.SkinnedPanel();
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer1)).BeginInit();
			this.skinnedSplitContainer1.Panel1.SuspendLayout();
			this.skinnedSplitContainer1.Panel2.SuspendLayout();
			this.skinnedSplitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer2)).BeginInit();
			this.skinnedSplitContainer2.Panel1.SuspendLayout();
			this.skinnedSplitContainer2.Panel2.SuspendLayout();
			this.skinnedSplitContainer2.SuspendLayout();
			this.skinnedGroupBox1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.grpProperties.SuspendLayout();
			this.skinnedGroupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tmrRefresh
			// 
			this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
			// 
			// skinnedSplitContainer1
			// 
			this.skinnedSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinnedSplitContainer1.Location = new System.Drawing.Point(0, 0);
			this.skinnedSplitContainer1.Name = "skinnedSplitContainer1";
			// 
			// skinnedSplitContainer1.Panel1
			// 
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.skinnedSplitContainer2);
			// 
			// skinnedSplitContainer1.Panel2
			// 
			this.skinnedSplitContainer1.Panel2.Controls.Add(this.skinnedGroupBox3);
			this.skinnedSplitContainer1.Size = new System.Drawing.Size(1004, 668);
			this.skinnedSplitContainer1.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedSplitContainer1.SplitterDistance = 220;
			this.skinnedSplitContainer1.TabIndex = 0;
			// 
			// skinnedSplitContainer2
			// 
			this.skinnedSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinnedSplitContainer2.Location = new System.Drawing.Point(0, 0);
			this.skinnedSplitContainer2.Name = "skinnedSplitContainer2";
			this.skinnedSplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// skinnedSplitContainer2.Panel1
			// 
			this.skinnedSplitContainer2.Panel1.Controls.Add(this.skinnedGroupBox1);
			// 
			// skinnedSplitContainer2.Panel2
			// 
			this.skinnedSplitContainer2.Panel2.Controls.Add(this.grpProperties);
			this.skinnedSplitContainer2.Size = new System.Drawing.Size(220, 668);
			this.skinnedSplitContainer2.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedSplitContainer2.SplitterDistance = 249;
			this.skinnedSplitContainer2.TabIndex = 0;
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.Controls.Add(this.toolStrip1);
			this.skinnedGroupBox1.Controls.Add(this.lstSlices);
			this.skinnedGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinnedGroupBox1.Location = new System.Drawing.Point(0, 0);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.Size = new System.Drawing.Size(220, 249);
			this.skinnedGroupBox1.TabIndex = 1;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Slices";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddSlice,
            this.tsRemoveSlice,
            this.toolStripSeparator1,
            this.tsDown,
            this.tsUp});
			this.toolStrip1.Location = new System.Drawing.Point(6, 25);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(208, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Tag = "Surface";
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsAddSlice
			// 
			this.tsAddSlice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddSlice.Image = global::Desktop.Properties.Resources.Add;
			this.tsAddSlice.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddSlice.Name = "tsAddSlice";
			this.tsAddSlice.Size = new System.Drawing.Size(23, 22);
			this.tsAddSlice.Text = "Add Slice";
			this.tsAddSlice.Click += new System.EventHandler(this.tsAddSlice_Click);
			// 
			// tsRemoveSlice
			// 
			this.tsRemoveSlice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveSlice.Image = global::Desktop.Properties.Resources.Remove;
			this.tsRemoveSlice.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveSlice.Name = "tsRemoveSlice";
			this.tsRemoveSlice.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveSlice.Text = "Remove Slice";
			this.tsRemoveSlice.Click += new System.EventHandler(this.tsRemoveSlice_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsDown
			// 
			this.tsDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsDown.Image = global::Desktop.Properties.Resources.DownArrow;
			this.tsDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDown.Name = "tsDown";
			this.tsDown.Size = new System.Drawing.Size(23, 22);
			this.tsDown.Text = "Move Down";
			this.tsDown.Click += new System.EventHandler(this.tsDown_Click);
			// 
			// tsUp
			// 
			this.tsUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsUp.Image = global::Desktop.Properties.Resources.UpArrow;
			this.tsUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsUp.Name = "tsUp";
			this.tsUp.Size = new System.Drawing.Size(23, 22);
			this.tsUp.Text = "Move Up";
			this.tsUp.Click += new System.EventHandler(this.tsUp_Click);
			// 
			// lstSlices
			// 
			this.lstSlices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstSlices.BackColor = System.Drawing.Color.White;
			this.lstSlices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstSlices.ForeColor = System.Drawing.Color.Black;
			this.lstSlices.FormattingEnabled = true;
			this.lstSlices.IntegralHeight = false;
			this.lstSlices.Location = new System.Drawing.Point(6, 53);
			this.lstSlices.Name = "lstSlices";
			this.lstSlices.Size = new System.Drawing.Size(208, 190);
			this.lstSlices.TabIndex = 0;
			this.lstSlices.SelectedIndexChanged += new System.EventHandler(this.lstSlices_SelectedIndexChanged);
			// 
			// grpProperties
			// 
			this.grpProperties.Controls.Add(this.pnlSlice);
			this.grpProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpProperties.Location = new System.Drawing.Point(0, 0);
			this.grpProperties.Name = "grpProperties";
			this.grpProperties.Size = new System.Drawing.Size(220, 415);
			this.grpProperties.TabIndex = 0;
			this.grpProperties.TabStop = false;
			this.grpProperties.Text = "Slice Properties";
			// 
			// pnlSlice
			// 
			this.pnlSlice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSlice.Location = new System.Drawing.Point(6, 27);
			this.pnlSlice.Name = "pnlSlice";
			this.pnlSlice.Size = new System.Drawing.Size(208, 380);
			this.pnlSlice.TabIndex = 0;
			// 
			// skinnedGroupBox3
			// 
			this.skinnedGroupBox3.Controls.Add(this.graph);
			this.skinnedGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinnedGroupBox3.Location = new System.Drawing.Point(0, 0);
			this.skinnedGroupBox3.Name = "skinnedGroupBox3";
			this.skinnedGroupBox3.Size = new System.Drawing.Size(780, 668);
			this.skinnedGroupBox3.TabIndex = 1;
			this.skinnedGroupBox3.TabStop = false;
			this.skinnedGroupBox3.Text = "Data";
			// 
			// graph
			// 
			this.graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.graph.Location = new System.Drawing.Point(6, 22);
			this.graph.Name = "graph";
			this.graph.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.graph.Size = new System.Drawing.Size(768, 640);
			this.graph.TabIndex = 0;
			this.graph.TabSide = Desktop.Skinning.TabSide.None;
			this.graph.Paint += new System.Windows.Forms.PaintEventHandler(this.graph_Paint);
			// 
			// DataSlicerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedSplitContainer1);
			this.Name = "DataSlicerControl";
			this.Size = new System.Drawing.Size(1004, 668);
			this.skinnedSplitContainer1.Panel1.ResumeLayout(false);
			this.skinnedSplitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer1)).EndInit();
			this.skinnedSplitContainer1.ResumeLayout(false);
			this.skinnedSplitContainer2.Panel1.ResumeLayout(false);
			this.skinnedSplitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer2)).EndInit();
			this.skinnedSplitContainer2.ResumeLayout(false);
			this.skinnedGroupBox1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.grpProperties.ResumeLayout(false);
			this.skinnedGroupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Skinning.SkinnedSplitContainer skinnedSplitContainer1;
		private Skinning.SkinnedSplitContainer skinnedSplitContainer2;
		private Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Skinning.SkinnedListBox lstSlices;
		private Skinning.SkinnedGroupBox grpProperties;
		private System.Windows.Forms.Panel pnlSlice;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsAddSlice;
		private Skinning.SkinnedGroupBox skinnedGroupBox3;
		private Desktop.Skinning.SkinnedPanel graph;
		private System.Windows.Forms.Timer tmrRefresh;
		private System.Windows.Forms.ToolStripButton tsRemoveSlice;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsDown;
		private System.Windows.Forms.ToolStripButton tsUp;
	}
}

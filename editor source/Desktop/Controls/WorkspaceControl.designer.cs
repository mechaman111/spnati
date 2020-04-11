namespace Desktop
{
	partial class WorkspaceControl
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
			this.splitContainer1 = new Desktop.Skinning.SkinnedSplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.grpBanner = new Desktop.Skinning.SkinnedGroupBox();
			this.tabActivities = new Desktop.Skinning.SkinnedTabControl();
			this.stripActivities = new Desktop.Skinning.SkinnedTabStrip();
			this.sidebar = new Desktop.CommonControls.DBPanel();
			this.stripSidebar = new Desktop.Skinning.SkinnedTabStrip();
			this.tabSidebarActivities = new Desktop.Skinning.SkinnedTabControl();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.sidebar.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.stripActivities);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.sidebar);
			this.splitContainer1.Size = new System.Drawing.Size(903, 544);
			this.splitContainer1.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.splitContainer1.SplitterDistance = 621;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(100, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.grpBanner);
			this.splitContainer2.Panel1Collapsed = true;
			this.splitContainer2.Panel1MinSize = 20;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.tabActivities);
			this.splitContainer2.Size = new System.Drawing.Size(522, 544);
			this.splitContainer2.SplitterDistance = 25;
			this.splitContainer2.TabIndex = 2;
			// 
			// grpBanner
			// 
			this.grpBanner.BackColor = System.Drawing.Color.White;
			this.grpBanner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpBanner.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpBanner.Image = null;
			this.grpBanner.Location = new System.Drawing.Point(0, 0);
			this.grpBanner.Name = "grpBanner";
			this.grpBanner.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.grpBanner.ShowIndicatorBar = false;
			this.grpBanner.Size = new System.Drawing.Size(150, 25);
			this.grpBanner.TabIndex = 0;
			this.grpBanner.TabStop = false;
			// 
			// tabActivities
			// 
			this.tabActivities.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabActivities.ItemSize = new System.Drawing.Size(25, 100);
			this.tabActivities.Location = new System.Drawing.Point(0, 0);
			this.tabActivities.Margin = new System.Windows.Forms.Padding(0);
			this.tabActivities.Multiline = true;
			this.tabActivities.Name = "tabActivities";
			this.tabActivities.SelectedIndex = 0;
			this.tabActivities.Size = new System.Drawing.Size(521, 544);
			this.tabActivities.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabActivities.TabIndex = 0;
			this.tabActivities.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.OnSelectingTab);
			// 
			// stripActivities
			// 
			this.stripActivities.AddCaption = null;
			this.stripActivities.DecorationText = null;
			this.stripActivities.Dock = System.Windows.Forms.DockStyle.Left;
			this.stripActivities.Location = new System.Drawing.Point(0, 0);
			this.stripActivities.Margin = new System.Windows.Forms.Padding(0);
			this.stripActivities.Name = "stripActivities";
			this.stripActivities.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.stripActivities.ShowAddButton = false;
			this.stripActivities.ShowCloseButton = false;
			this.stripActivities.Size = new System.Drawing.Size(100, 544);
			this.stripActivities.StartMargin = 10;
			this.stripActivities.TabControl = this.tabActivities;
			this.stripActivities.TabIndex = 1;
			this.stripActivities.TabMargin = 1;
			this.stripActivities.TabPadding = 20;
			this.stripActivities.TabSize = 25;
			this.stripActivities.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripActivities.Text = "skinnedTabStrip1";
			this.stripActivities.Vertical = true;
			// 
			// sidebar
			// 
			this.sidebar.Controls.Add(this.stripSidebar);
			this.sidebar.Controls.Add(this.tabSidebarActivities);
			this.sidebar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sidebar.Location = new System.Drawing.Point(0, 0);
			this.sidebar.Name = "sidebar";
			this.sidebar.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.sidebar.Size = new System.Drawing.Size(278, 544);
			this.sidebar.TabIndex = 0;
			this.sidebar.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// stripSidebar
			// 
			this.stripSidebar.AddCaption = null;
			this.stripSidebar.DecorationText = null;
			this.stripSidebar.Dock = System.Windows.Forms.DockStyle.Top;
			this.stripSidebar.Location = new System.Drawing.Point(0, 0);
			this.stripSidebar.Margin = new System.Windows.Forms.Padding(0);
			this.stripSidebar.Name = "stripSidebar";
			this.stripSidebar.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripSidebar.ShowAddButton = false;
			this.stripSidebar.ShowCloseButton = false;
			this.stripSidebar.Size = new System.Drawing.Size(278, 23);
			this.stripSidebar.StartMargin = 20;
			this.stripSidebar.TabControl = this.tabSidebarActivities;
			this.stripSidebar.TabIndex = 1;
			this.stripSidebar.TabMargin = 5;
			this.stripSidebar.TabPadding = 20;
			this.stripSidebar.TabSize = -1;
			this.stripSidebar.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripSidebar.Vertical = false;
			// 
			// tabSidebarActivities
			// 
			this.tabSidebarActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabSidebarActivities.Location = new System.Drawing.Point(0, 23);
			this.tabSidebarActivities.Margin = new System.Windows.Forms.Padding(0);
			this.tabSidebarActivities.Name = "tabSidebarActivities";
			this.tabSidebarActivities.SelectedIndex = 0;
			this.tabSidebarActivities.Size = new System.Drawing.Size(278, 521);
			this.tabSidebarActivities.TabIndex = 0;
			this.tabSidebarActivities.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.OnSelectingTab);
			// 
			// WorkspaceControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "WorkspaceControl";
			this.Size = new System.Drawing.Size(903, 544);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.sidebar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedTabControl tabActivities;
		private Desktop.Skinning.SkinnedSplitContainer splitContainer1;
		private CommonControls.DBPanel sidebar;
		private Desktop.Skinning.SkinnedTabControl tabSidebarActivities;
		private Skinning.SkinnedTabStrip stripActivities;
		private Skinning.SkinnedTabStrip stripSidebar;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Skinning.SkinnedGroupBox grpBanner;
	}
}

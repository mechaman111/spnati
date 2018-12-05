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
			this.tabActivities = new System.Windows.Forms.TabControl();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.sidebar = new Desktop.CommonControls.DBPanel();
			this.tabSidebarActivities = new System.Windows.Forms.TabControl();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.sidebar.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabActivities
			// 
			this.tabActivities.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabActivities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabActivities.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabActivities.ItemSize = new System.Drawing.Size(25, 100);
			this.tabActivities.Location = new System.Drawing.Point(0, 0);
			this.tabActivities.Multiline = true;
			this.tabActivities.Name = "tabActivities";
			this.tabActivities.SelectedIndex = 0;
			this.tabActivities.Size = new System.Drawing.Size(617, 540);
			this.tabActivities.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabActivities.TabIndex = 0;
			this.tabActivities.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabActivities_DrawItem);
			this.tabActivities.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.OnSelectingTab);
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
			this.splitContainer1.Panel1.Controls.Add(this.tabActivities);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.sidebar);
			this.splitContainer1.Size = new System.Drawing.Size(903, 544);
			this.splitContainer1.SplitterDistance = 621;
			this.splitContainer1.TabIndex = 1;
			// 
			// sidebar
			// 
			this.sidebar.Controls.Add(this.tabSidebarActivities);
			this.sidebar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sidebar.Location = new System.Drawing.Point(0, 0);
			this.sidebar.Name = "sidebar";
			this.sidebar.Size = new System.Drawing.Size(274, 540);
			this.sidebar.TabIndex = 0;
			// 
			// tabSidebarActivities
			// 
			this.tabSidebarActivities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabSidebarActivities.Location = new System.Drawing.Point(0, 0);
			this.tabSidebarActivities.Name = "tabSidebarActivities";
			this.tabSidebarActivities.SelectedIndex = 0;
			this.tabSidebarActivities.Size = new System.Drawing.Size(274, 540);
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
			this.sidebar.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabActivities;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private CommonControls.DBPanel sidebar;
		private System.Windows.Forms.TabControl tabSidebarActivities;
	}
}

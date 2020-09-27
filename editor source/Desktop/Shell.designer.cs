namespace Desktop
{
	partial class Shell
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
			if (disposing)
			{
				if (_messenger != null)
				{
					_messenger.Dispose();
				}
				if (_toaster != null)
				{
					_toaster.Dispose();
				}
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolbar = new System.Windows.Forms.MenuStrip();
			this.tabWorkspaces = new Desktop.Skinning.SkinnedTabControl();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsSubAction = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.tmrAutoTick = new System.Windows.Forms.Timer(this.components);
			this.stripActivities = new Desktop.Skinning.SkinnedTabStrip();
			this.actionStrip = new System.Windows.Forms.MenuStrip();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolbar
			// 
			this.toolbar.Dock = System.Windows.Forms.DockStyle.None;
			this.toolbar.Location = new System.Drawing.Point(27, 1);
			this.toolbar.Name = "toolbar";
			this.toolbar.Size = new System.Drawing.Size(202, 24);
			this.toolbar.TabIndex = 0;
			this.toolbar.Tag = "PrimaryDark";
			this.toolbar.Text = "menuStrip1";
			// 
			// tabWorkspaces
			// 
			this.tabWorkspaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabWorkspaces.Location = new System.Drawing.Point(1, 55);
			this.tabWorkspaces.Margin = new System.Windows.Forms.Padding(0);
			this.tabWorkspaces.Name = "tabWorkspaces";
			this.tabWorkspaces.Padding = new System.Drawing.Point(21, 3);
			this.tabWorkspaces.SelectedIndex = 0;
			this.tabWorkspaces.Size = new System.Drawing.Size(1302, 706);
			this.tabWorkspaces.TabIndex = 2;
			this.tabWorkspaces.Visible = false;
			this.tabWorkspaces.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabWorkspaces_Selecting);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 16;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statusStrip1.AutoSize = false;
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.tsSubAction,
            this.tsVersion});
			this.statusStrip1.Location = new System.Drawing.Point(1, 761);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1302, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(1122, 17);
			this.lblStatus.Spring = true;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tsSubAction
			// 
			this.tsSubAction.Name = "tsSubAction";
			this.tsSubAction.Size = new System.Drawing.Size(0, 17);
			this.tsSubAction.Click += new System.EventHandler(this.tsSubAction_Click);
			// 
			// tsVersion
			// 
			this.tsVersion.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsVersion.Name = "tsVersion";
			this.tsVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsVersion.Size = new System.Drawing.Size(27, 17);
			this.tsVersion.Text = "v1.0";
			this.tsVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.tsVersion.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
			this.tsVersion.Click += new System.EventHandler(this.tsVersion_Click);
			// 
			// tmrAutoTick
			// 
			this.tmrAutoTick.Tick += new System.EventHandler(this.tmrAutoTick_Tick);
			// 
			// stripActivities
			// 
			this.stripActivities.AddCaption = null;
			this.stripActivities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stripActivities.DecorationText = null;
			this.stripActivities.Location = new System.Drawing.Point(1, 25);
			this.stripActivities.Margin = new System.Windows.Forms.Padding(0);
			this.stripActivities.Name = "stripActivities";
			this.stripActivities.PanelType = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.stripActivities.ShowAddButton = false;
			this.stripActivities.ShowCloseButton = true;
			this.stripActivities.Size = new System.Drawing.Size(1302, 30);
			this.stripActivities.StartMargin = 103;
			this.stripActivities.TabControl = this.tabWorkspaces;
			this.stripActivities.TabIndex = 4;
			this.stripActivities.TabMargin = 5;
			this.stripActivities.TabPadding = 20;
			this.stripActivities.TabSize = 100;
			this.stripActivities.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripActivities.Text = "skinnedTabStrip1";
			this.stripActivities.Vertical = false;
			this.stripActivities.CloseButtonClicked += new System.EventHandler(this.stripActivities_CloseButtonClicked);
			// 
			// actionStrip
			// 
			this.actionStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.actionStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.actionStrip.Location = new System.Drawing.Point(1009, 1);
			this.actionStrip.Name = "actionStrip";
			this.actionStrip.ShowItemToolTips = true;
			this.actionStrip.Size = new System.Drawing.Size(202, 24);
			this.actionStrip.TabIndex = 5;
			this.actionStrip.Tag = "PrimaryDark";
			// 
			// Shell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1304, 784);
			this.Controls.Add(this.actionStrip);
			this.Controls.Add(this.stripActivities);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabWorkspaces);
			this.Controls.Add(this.toolbar);
			this.KeyPreview = true;
			this.MainMenuStrip = this.toolbar;
			this.Name = "Shell";
			this.ShowTitleBar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shell";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Shell_FormClosing);
			this.Load += new System.EventHandler(this.Shell_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Shell_KeyDown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip toolbar;
		private Desktop.Skinning.SkinnedTabControl tabWorkspaces;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Timer tmrAutoTick;
		private Skinning.SkinnedTabStrip stripActivities;
		private System.Windows.Forms.MenuStrip actionStrip;
		private System.Windows.Forms.ToolStripStatusLabel tsVersion;
		private System.Windows.Forms.ToolStripStatusLabel tsSubAction;
	}
}


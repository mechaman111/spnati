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
			if (disposing && (components != null))
			{
				components.Dispose();
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
			this.tabWorkspaces = new System.Windows.Forms.TabControl();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tmrAutoTick = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolbar
			// 
			this.toolbar.Location = new System.Drawing.Point(0, 0);
			this.toolbar.Name = "toolbar";
			this.toolbar.Size = new System.Drawing.Size(1304, 24);
			this.toolbar.TabIndex = 0;
			this.toolbar.Text = "menuStrip1";
			// 
			// tabWorkspaces
			// 
			this.tabWorkspaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabWorkspaces.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabWorkspaces.Location = new System.Drawing.Point(0, 27);
			this.tabWorkspaces.Name = "tabWorkspaces";
			this.tabWorkspaces.Padding = new System.Drawing.Point(21, 3);
			this.tabWorkspaces.SelectedIndex = 0;
			this.tabWorkspaces.Size = new System.Drawing.Size(1304, 732);
			this.tabWorkspaces.TabIndex = 2;
			this.tabWorkspaces.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabWorkspaces_DrawItem);
			this.tabWorkspaces.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabWorkspaces_Selecting);
			this.tabWorkspaces.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabWorkspaces_MouseClick);
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
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 762);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1304, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(39, 17);
			this.lblStatus.Text = "Status";
			// 
			// tmrAutoTick
			// 
			this.tmrAutoTick.Tick += new System.EventHandler(this.tmrAutoTick_Tick);
			// 
			// Shell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1304, 784);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabWorkspaces);
			this.Controls.Add(this.toolbar);
			this.KeyPreview = true;
			this.MainMenuStrip = this.toolbar;
			this.Name = "Shell";
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
		private System.Windows.Forms.TabControl tabWorkspaces;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Timer tmrAutoTick;
	}
}


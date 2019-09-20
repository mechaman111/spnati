namespace Desktop.CommonControls
{
	partial class PropertyTable
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
			this.shortcuts = new System.Windows.Forms.MenuStrip();
			this.focusOnAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSpeedButtons = new Desktop.Skinning.SkinnedMenuStrip();
			this.recAdd = new Desktop.CommonControls.RecordField();
			this.pnlRecords = new Desktop.CommonControls.DBPanel();
			this.shortcuts.SuspendLayout();
			this.SuspendLayout();
			// 
			// shortcuts
			// 
			this.shortcuts.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.shortcuts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.focusOnAdd});
			this.shortcuts.Location = new System.Drawing.Point(0, 126);
			this.shortcuts.Name = "shortcuts";
			this.shortcuts.Size = new System.Drawing.Size(863, 24);
			this.shortcuts.TabIndex = 3;
			this.shortcuts.Text = "menuStrip1";
			this.shortcuts.Visible = false;
			// 
			// focusOnAdd
			// 
			this.focusOnAdd.Name = "focusOnAdd";
			this.focusOnAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
			this.focusOnAdd.Size = new System.Drawing.Size(78, 20);
			this.focusOnAdd.Text = "Add record";
			this.focusOnAdd.Click += new System.EventHandler(this.focusOnAdd_Click);
			// 
			// menuSpeedButtons
			// 
			this.menuSpeedButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.menuSpeedButtons.AutoSize = false;
			this.menuSpeedButtons.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.menuSpeedButtons.Dock = System.Windows.Forms.DockStyle.None;
			this.menuSpeedButtons.Location = new System.Drawing.Point(142, 0);
			this.menuSpeedButtons.Name = "menuSpeedButtons";
			this.menuSpeedButtons.Size = new System.Drawing.Size(592, 24);
			this.menuSpeedButtons.TabIndex = 4;
			this.menuSpeedButtons.Tag = "Surface";
			// 
			// recAdd
			// 
			this.recAdd.AllowCreate = false;
			this.recAdd.Location = new System.Drawing.Point(3, 3);
			this.recAdd.Name = "recAdd";
			this.recAdd.PlaceholderText = null;
			this.recAdd.Record = null;
			this.recAdd.RecordContext = null;
			this.recAdd.RecordFilter = null;
			this.recAdd.RecordKey = null;
			this.recAdd.RecordType = null;
			this.recAdd.Size = new System.Drawing.Size(138, 20);
			this.recAdd.TabIndex = 0;
			this.recAdd.UseAutoComplete = false;
			this.recAdd.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recAdd_RecordChanged);
			// 
			// pnlRecords
			// 
			this.pnlRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRecords.AutoScroll = true;
			this.pnlRecords.Location = new System.Drawing.Point(3, 27);
			this.pnlRecords.Name = "pnlRecords";
			this.pnlRecords.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlRecords.Size = new System.Drawing.Size(731, 120);
			this.pnlRecords.TabIndex = 2;
			this.pnlRecords.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// PropertyTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.menuSpeedButtons);
			this.Controls.Add(this.recAdd);
			this.Controls.Add(this.pnlRecords);
			this.Controls.Add(this.shortcuts);
			this.Name = "PropertyTable";
			this.Size = new System.Drawing.Size(734, 150);
			this.shortcuts.ResumeLayout(false);
			this.shortcuts.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private RecordField recAdd;
		private DBPanel pnlRecords;
		private System.Windows.Forms.MenuStrip shortcuts;
		private System.Windows.Forms.ToolStripMenuItem focusOnAdd;
		private Desktop.Skinning.SkinnedMenuStrip menuSpeedButtons;
	}
}

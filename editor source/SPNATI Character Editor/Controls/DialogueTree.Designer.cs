namespace SPNATI_Character_Editor.Controls
{
	partial class DialogueTree
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.recTreeTarget = new Desktop.CommonControls.RecordField();
			this.tstrDialogue = new System.Windows.Forms.ToolStrip();
			this.tsbtnAddDialogue = new System.Windows.Forms.ToolStripSplitButton();
			this.triggerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsbtnSplit = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsbtnRemoveDialogue = new System.Windows.Forms.ToolStripButton();
			this.treeDialogue = new System.Windows.Forms.TreeView();
			this.cboTreeFilter = new System.Windows.Forms.ComboBox();
			this.label33 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.tmrDelete = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.cboView = new System.Windows.Forms.ComboBox();
			this.tstrDialogue.SuspendLayout();
			this.SuspendLayout();
			// 
			// recTreeTarget
			// 
			this.recTreeTarget.AllowCreate = false;
			this.recTreeTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.recTreeTarget.Location = new System.Drawing.Point(55, 604);
			this.recTreeTarget.Name = "recTreeTarget";
			this.recTreeTarget.PlaceholderText = null;
			this.recTreeTarget.Record = null;
			this.recTreeTarget.RecordContext = null;
			this.recTreeTarget.RecordKey = null;
			this.recTreeTarget.RecordType = null;
			this.recTreeTarget.Size = new System.Drawing.Size(278, 20);
			this.recTreeTarget.TabIndex = 45;
			this.recTreeTarget.UseAutoComplete = true;
			this.recTreeTarget.RecordChanged += new System.EventHandler<Desktop.IRecord>(this.recTreeTarget_RecordChanged);
			// 
			// tstrDialogue
			// 
			this.tstrDialogue.AutoSize = false;
			this.tstrDialogue.BackColor = System.Drawing.SystemColors.Control;
			this.tstrDialogue.CanOverflow = false;
			this.tstrDialogue.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tstrDialogue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddDialogue,
            this.tsbtnSplit,
            this.tsbtnRemoveDialogue});
			this.tstrDialogue.Location = new System.Drawing.Point(0, 0);
			this.tstrDialogue.Name = "tstrDialogue";
			this.tstrDialogue.Padding = new System.Windows.Forms.Padding(2, 2, 3, 2);
			this.tstrDialogue.Size = new System.Drawing.Size(336, 35);
			this.tstrDialogue.TabIndex = 40;
			this.tstrDialogue.Text = "toolStrip1";
			// 
			// tsbtnAddDialogue
			// 
			this.tsbtnAddDialogue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnAddDialogue.DropDown = this.triggerMenu;
			this.tsbtnAddDialogue.Name = "tsbtnAddDialogue";
			this.tsbtnAddDialogue.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.tsbtnAddDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tsbtnAddDialogue.Size = new System.Drawing.Size(51, 28);
			this.tsbtnAddDialogue.Text = "Add";
			this.tsbtnAddDialogue.ButtonClick += new System.EventHandler(this.tsbtnAddDialogue_ButtonClick);
			// 
			// triggerMenu
			// 
			this.triggerMenu.Name = "triggerMenu";
			this.triggerMenu.OwnerItem = this.tsbtnAddDialogue;
			this.triggerMenu.ShowImageMargin = false;
			this.triggerMenu.Size = new System.Drawing.Size(36, 4);
			// 
			// tsbtnSplit
			// 
			this.tsbtnSplit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnSplit.Name = "tsbtnSplit";
			this.tsbtnSplit.Padding = new System.Windows.Forms.Padding(3);
			this.tsbtnSplit.Size = new System.Drawing.Size(85, 28);
			this.tsbtnSplit.Text = "Copy Tools";
			// 
			// tsbtnRemoveDialogue
			// 
			this.tsbtnRemoveDialogue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbtnRemoveDialogue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnRemoveDialogue.Name = "tsbtnRemoveDialogue";
			this.tsbtnRemoveDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tsbtnRemoveDialogue.Size = new System.Drawing.Size(60, 28);
			this.tsbtnRemoveDialogue.Text = "Remove";
			this.tsbtnRemoveDialogue.Click += new System.EventHandler(this.tsmiRemove_Click);
			// 
			// treeDialogue
			// 
			this.treeDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeDialogue.HideSelection = false;
			this.treeDialogue.Location = new System.Drawing.Point(3, 59);
			this.treeDialogue.Name = "treeDialogue";
			this.treeDialogue.Size = new System.Drawing.Size(330, 512);
			this.treeDialogue.TabIndex = 41;
			this.treeDialogue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeDialogue_KeyDown);
			// 
			// cboTreeFilter
			// 
			this.cboTreeFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTreeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTreeFilter.FormattingEnabled = true;
			this.cboTreeFilter.Items.AddRange(new object[] {
            "All Lines",
            "Non-Targeted Only",
            "Targeted Only"});
			this.cboTreeFilter.Location = new System.Drawing.Point(55, 577);
			this.cboTreeFilter.Name = "cboTreeFilter";
			this.cboTreeFilter.Size = new System.Drawing.Size(278, 21);
			this.cboTreeFilter.TabIndex = 44;
			this.cboTreeFilter.SelectedIndexChanged += new System.EventHandler(this.cboTreeFilter_SelectedIndexChanged);
			// 
			// label33
			// 
			this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label33.AutoSize = true;
			this.label33.ForeColor = System.Drawing.Color.Blue;
			this.label33.Location = new System.Drawing.Point(3, 640);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(134, 13);
			this.label33.TabIndex = 42;
			this.label33.Text = "Blue: Contains default lines";
			// 
			// label35
			// 
			this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label35.AutoSize = true;
			this.label35.ForeColor = System.Drawing.Color.Green;
			this.label35.Location = new System.Drawing.Point(3, 627);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(128, 13);
			this.label35.TabIndex = 43;
			this.label35.Text = "Green: Targeted dialogue";
			// 
			// label40
			// 
			this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label40.AutoSize = true;
			this.label40.Location = new System.Drawing.Point(3, 607);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(41, 13);
			this.label40.TabIndex = 47;
			this.label40.Text = "Target:";
			// 
			// label39
			// 
			this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label39.AutoSize = true;
			this.label39.Location = new System.Drawing.Point(3, 580);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(32, 13);
			this.label39.TabIndex = 46;
			this.label39.Text = "Filter:";
			// 
			// tmrDelete
			// 
			this.tmrDelete.Interval = 1;
			this.tmrDelete.Tick += new System.EventHandler(this.tmrDelete_Tick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 49;
			this.label1.Text = "View:";
			// 
			// cboView
			// 
			this.cboView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboView.FormattingEnabled = true;
			this.cboView.Items.AddRange(new object[] {
            "By Stage",
            "By Case"});
			this.cboView.Location = new System.Drawing.Point(55, 32);
			this.cboView.Name = "cboView";
			this.cboView.Size = new System.Drawing.Size(278, 21);
			this.cboView.TabIndex = 48;
			// 
			// DialogueTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboView);
			this.Controls.Add(this.label40);
			this.Controls.Add(this.label39);
			this.Controls.Add(this.recTreeTarget);
			this.Controls.Add(this.tstrDialogue);
			this.Controls.Add(this.treeDialogue);
			this.Controls.Add(this.cboTreeFilter);
			this.Controls.Add(this.label33);
			this.Controls.Add(this.label35);
			this.Name = "DialogueTree";
			this.Size = new System.Drawing.Size(336, 653);
			this.tstrDialogue.ResumeLayout(false);
			this.tstrDialogue.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recTreeTarget;
		private System.Windows.Forms.ToolStrip tstrDialogue;
		private System.Windows.Forms.ToolStripSplitButton tsbtnAddDialogue;
		private System.Windows.Forms.ToolStripDropDownButton tsbtnSplit;
		private System.Windows.Forms.ToolStripButton tsbtnRemoveDialogue;
		private System.Windows.Forms.TreeView treeDialogue;
		private System.Windows.Forms.ComboBox cboTreeFilter;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.ContextMenuStrip triggerMenu;
		private System.Windows.Forms.Timer tmrDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboView;
	}
}

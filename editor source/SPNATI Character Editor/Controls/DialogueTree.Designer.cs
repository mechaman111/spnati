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
			this.tstrDialogue = new System.Windows.Forms.ToolStrip();
			this.tsbtnAddDialogue = new System.Windows.Forms.ToolStripSplitButton();
			this.triggerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsRecipe = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSplit = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsbtnRemoveDialogue = new System.Windows.Forms.ToolStripButton();
			this.tsConfig = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsHide = new System.Windows.Forms.ToolStripMenuItem();
			this.tsUnhide = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsShowHidden = new System.Windows.Forms.ToolStripMenuItem();
			this.tsCollapse = new System.Windows.Forms.ToolStripButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdLegend = new Desktop.Skinning.SkinnedIcon();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cboView = new Desktop.Skinning.SkinnedComboBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.lstDialogue = new Desktop.CommonControls.AccordionListView();
			this.chkHideTargeted = new Desktop.Skinning.SkinnedCheckBox();
			this.lblTag = new Desktop.Skinning.SkinnedLabel();
			this.recTag = new Desktop.CommonControls.RecordField();
			this.label40 = new Desktop.Skinning.SkinnedLabel();
			this.recTreeTarget = new Desktop.CommonControls.RecordField();
			this.tsExpandAll = new System.Windows.Forms.ToolStripButton();
			this.tstrDialogue.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tstrDialogue
			// 
			this.tstrDialogue.AutoSize = false;
			this.tstrDialogue.CanOverflow = false;
			this.tstrDialogue.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tstrDialogue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddDialogue,
            this.tsRecipe,
            this.toolStripSeparator3,
            this.tsRefresh,
            this.toolStripSeparator2,
            this.tsbtnSplit,
            this.tsbtnRemoveDialogue,
            this.tsConfig,
            this.tsCollapse,
            this.tsExpandAll});
			this.tstrDialogue.Location = new System.Drawing.Point(0, 0);
			this.tstrDialogue.Name = "tstrDialogue";
			this.tstrDialogue.Padding = new System.Windows.Forms.Padding(2, 2, 3, 2);
			this.tstrDialogue.Size = new System.Drawing.Size(336, 30);
			this.tstrDialogue.TabIndex = 40;
			this.tstrDialogue.Text = "toolStrip1";
			// 
			// tsbtnAddDialogue
			// 
			this.tsbtnAddDialogue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAddDialogue.DropDown = this.triggerMenu;
			this.tsbtnAddDialogue.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsbtnAddDialogue.Name = "tsbtnAddDialogue";
			this.tsbtnAddDialogue.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.tsbtnAddDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tsbtnAddDialogue.Size = new System.Drawing.Size(38, 23);
			this.tsbtnAddDialogue.Text = "Add";
			this.tsbtnAddDialogue.ButtonClick += new System.EventHandler(this.tsbtnAddDialogue_ButtonClick);
			// 
			// triggerMenu
			// 
			this.triggerMenu.Name = "triggerMenu";
			this.triggerMenu.OwnerItem = this.tsbtnAddDialogue;
			this.triggerMenu.ShowImageMargin = false;
			this.triggerMenu.Size = new System.Drawing.Size(36, 4);
			this.triggerMenu.Opening += new System.ComponentModel.CancelEventHandler(this.triggerMenu_Opening);
			// 
			// tsRecipe
			// 
			this.tsRecipe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRecipe.Image = global::SPNATI_Character_Editor.Properties.Resources.Recipe;
			this.tsRecipe.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRecipe.Name = "tsRecipe";
			this.tsRecipe.Size = new System.Drawing.Size(23, 23);
			this.tsRecipe.Text = "Use Recipe";
			this.tsRecipe.Click += new System.EventHandler(this.tsRecipe_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
			// 
			// tsRefresh
			// 
			this.tsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRefresh.Image = global::SPNATI_Character_Editor.Properties.Resources.Sort;
			this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRefresh.Name = "tsRefresh";
			this.tsRefresh.Size = new System.Drawing.Size(23, 23);
			this.tsRefresh.Text = "Update sorting";
			this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
			// 
			// tsbtnSplit
			// 
			this.tsbtnSplit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnSplit.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.tsbtnSplit.Name = "tsbtnSplit";
			this.tsbtnSplit.Padding = new System.Windows.Forms.Padding(3);
			this.tsbtnSplit.Size = new System.Drawing.Size(35, 23);
			this.tsbtnSplit.Text = "Copy";
			// 
			// tsbtnRemoveDialogue
			// 
			this.tsbtnRemoveDialogue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbtnRemoveDialogue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnRemoveDialogue.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsbtnRemoveDialogue.Name = "tsbtnRemoveDialogue";
			this.tsbtnRemoveDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tsbtnRemoveDialogue.Size = new System.Drawing.Size(26, 23);
			this.tsbtnRemoveDialogue.Text = "Remove";
			this.tsbtnRemoveDialogue.Click += new System.EventHandler(this.tsmiRemove_Click);
			// 
			// tsConfig
			// 
			this.tsConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsHide,
            this.tsUnhide,
            this.toolStripSeparator1,
            this.tsShowHidden});
			this.tsConfig.Image = global::SPNATI_Character_Editor.Properties.Resources.Settings;
			this.tsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsConfig.Name = "tsConfig";
			this.tsConfig.Size = new System.Drawing.Size(29, 23);
			this.tsConfig.Text = "Config";
			this.tsConfig.DropDownOpening += new System.EventHandler(this.tsConfig_DropDownOpening);
			// 
			// tsHide
			// 
			this.tsHide.Name = "tsHide";
			this.tsHide.Size = new System.Drawing.Size(178, 22);
			this.tsHide.Text = "Hide Case";
			this.tsHide.Click += new System.EventHandler(this.tsHide_Click);
			// 
			// tsUnhide
			// 
			this.tsUnhide.Name = "tsUnhide";
			this.tsUnhide.Size = new System.Drawing.Size(178, 22);
			this.tsUnhide.Text = "Unhide Case";
			this.tsUnhide.Click += new System.EventHandler(this.tsUnhide_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
			// 
			// tsShowHidden
			// 
			this.tsShowHidden.CheckOnClick = true;
			this.tsShowHidden.Name = "tsShowHidden";
			this.tsShowHidden.Size = new System.Drawing.Size(178, 22);
			this.tsShowHidden.Text = "Show Hidden Cases";
			this.tsShowHidden.Click += new System.EventHandler(this.tsShowHidden_Click);
			// 
			// tsCollapse
			// 
			this.tsCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCollapse.Image = global::SPNATI_Character_Editor.Properties.Resources.CollapseAll;
			this.tsCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCollapse.Name = "tsCollapse";
			this.tsCollapse.Size = new System.Drawing.Size(23, 23);
			this.tsCollapse.Text = "Collapse all";
			this.tsCollapse.Click += new System.EventHandler(this.tsCollapse_Click);
			// 
			// cmdLegend
			// 
			this.cmdLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdLegend.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdLegend.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdLegend.Flat = false;
			this.cmdLegend.FlatAppearance.BorderSize = 0;
			this.cmdLegend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdLegend.Image = global::SPNATI_Character_Editor.Properties.Resources.Legend;
			this.cmdLegend.Location = new System.Drawing.Point(317, 580);
			this.cmdLegend.Name = "cmdLegend";
			this.cmdLegend.Size = new System.Drawing.Size(16, 16);
			this.cmdLegend.TabIndex = 53;
			this.toolTip1.SetToolTip(this.cmdLegend, "See Color Legend");
			this.cmdLegend.UseVisualStyleBackColor = true;
			this.cmdLegend.Click += new System.EventHandler(this.cmdLegend_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel1.Controls.Add(this.cboView);
			this.skinnedPanel1.Controls.Add(this.label1);
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 27);
			this.skinnedPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.skinnedPanel1.Size = new System.Drawing.Size(336, 26);
			this.skinnedPanel1.TabIndex = 54;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cboView
			// 
			this.cboView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboView.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboView.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboView.BackColor = System.Drawing.Color.White;
			this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboView.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cboView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboView.FormattingEnabled = true;
			this.cboView.Location = new System.Drawing.Point(55, 2);
			this.cboView.Name = "cboView";
			this.cboView.SelectedIndex = -1;
			this.cboView.SelectedItem = null;
			this.cboView.Size = new System.Drawing.Size(278, 21);
			this.cboView.Sorted = false;
			this.cboView.TabIndex = 48;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.PrimaryLight;
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 49;
			this.label1.Text = "View:";
			// 
			// lstDialogue
			// 
			this.lstDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstDialogue.DataSource = null;
			this.lstDialogue.Location = new System.Drawing.Point(3, 56);
			this.lstDialogue.Name = "lstDialogue";
			this.lstDialogue.SelectedItem = null;
			this.lstDialogue.ShowIndicators = true;
			this.lstDialogue.Size = new System.Drawing.Size(330, 519);
			this.lstDialogue.TabIndex = 52;
			this.lstDialogue.SelectedIndexChanged += new System.EventHandler<System.EventArgs>(this.lstDialogue_SelectedIndexChanged);
			this.lstDialogue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstDialogue_KeyDown);
			// 
			// chkHideTargeted
			// 
			this.chkHideTargeted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkHideTargeted.AutoSize = true;
			this.chkHideTargeted.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkHideTargeted.Location = new System.Drawing.Point(3, 581);
			this.chkHideTargeted.Name = "chkHideTargeted";
			this.chkHideTargeted.Size = new System.Drawing.Size(139, 17);
			this.chkHideTargeted.TabIndex = 42;
			this.chkHideTargeted.Text = "Hide Targeted Dialogue";
			this.chkHideTargeted.UseVisualStyleBackColor = true;
			this.chkHideTargeted.CheckedChanged += new System.EventHandler(this.chkHideTargeted_CheckedChanged);
			// 
			// lblTag
			// 
			this.lblTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblTag.AutoSize = true;
			this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblTag.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTag.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblTag.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblTag.Location = new System.Drawing.Point(3, 631);
			this.lblTag.Name = "lblTag";
			this.lblTag.Size = new System.Drawing.Size(50, 13);
			this.lblTag.TabIndex = 51;
			this.lblTag.Text = "Filter tag:";
			// 
			// recTag
			// 
			this.recTag.AllowCreate = false;
			this.recTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.recTag.Location = new System.Drawing.Point(71, 630);
			this.recTag.Name = "recTag";
			this.recTag.PlaceholderText = null;
			this.recTag.Record = null;
			this.recTag.RecordContext = null;
			this.recTag.RecordFilter = null;
			this.recTag.RecordKey = null;
			this.recTag.RecordType = null;
			this.recTag.Size = new System.Drawing.Size(262, 20);
			this.recTag.TabIndex = 44;
			this.recTag.UseAutoComplete = true;
			this.recTag.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recTag_RecordChanged);
			// 
			// label40
			// 
			this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label40.AutoSize = true;
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label40.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label40.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label40.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label40.Location = new System.Drawing.Point(3, 608);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(62, 13);
			this.label40.TabIndex = 47;
			this.label40.Text = "Filter target:";
			// 
			// recTreeTarget
			// 
			this.recTreeTarget.AllowCreate = false;
			this.recTreeTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.recTreeTarget.Location = new System.Drawing.Point(71, 604);
			this.recTreeTarget.Name = "recTreeTarget";
			this.recTreeTarget.PlaceholderText = null;
			this.recTreeTarget.Record = null;
			this.recTreeTarget.RecordContext = null;
			this.recTreeTarget.RecordFilter = null;
			this.recTreeTarget.RecordKey = null;
			this.recTreeTarget.RecordType = null;
			this.recTreeTarget.Size = new System.Drawing.Size(262, 20);
			this.recTreeTarget.TabIndex = 43;
			this.recTreeTarget.UseAutoComplete = true;
			this.recTreeTarget.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recTreeTarget_RecordChanged);
			// 
			// tsExpandAll
			// 
			this.tsExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsExpandAll.Image = global::SPNATI_Character_Editor.Properties.Resources.ExpandAll;
			this.tsExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsExpandAll.Name = "tsExpandAll";
			this.tsExpandAll.Size = new System.Drawing.Size(23, 23);
			this.tsExpandAll.Text = "Expand all";
			this.tsExpandAll.Click += new System.EventHandler(this.tsExpandAll_Click);
			// 
			// DialogueTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.cmdLegend);
			this.Controls.Add(this.lstDialogue);
			this.Controls.Add(this.chkHideTargeted);
			this.Controls.Add(this.lblTag);
			this.Controls.Add(this.recTag);
			this.Controls.Add(this.label40);
			this.Controls.Add(this.recTreeTarget);
			this.Controls.Add(this.tstrDialogue);
			this.Name = "DialogueTree";
			this.Size = new System.Drawing.Size(336, 653);
			this.Load += new System.EventHandler(this.DialogueTree_Load);
			this.tstrDialogue.ResumeLayout(false);
			this.tstrDialogue.PerformLayout();
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recTreeTarget;
		private System.Windows.Forms.ToolStrip tstrDialogue;
		private System.Windows.Forms.ToolStripSplitButton tsbtnAddDialogue;
		private System.Windows.Forms.ToolStripDropDownButton tsbtnSplit;
		private System.Windows.Forms.ToolStripButton tsbtnRemoveDialogue;
		private Desktop.Skinning.SkinnedLabel label40;
		private System.Windows.Forms.ContextMenuStrip triggerMenu;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedComboBox cboView;
		private System.Windows.Forms.ToolStripDropDownButton tsConfig;
		private System.Windows.Forms.ToolStripMenuItem tsUnhide;
		private System.Windows.Forms.ToolStripMenuItem tsHide;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsShowHidden;
		private Desktop.CommonControls.RecordField recTag;
		private Desktop.Skinning.SkinnedLabel lblTag;
		private Desktop.Skinning.SkinnedCheckBox chkHideTargeted;
		private Desktop.CommonControls.AccordionListView lstDialogue;
		private Desktop.Skinning.SkinnedIcon cmdLegend;
		private System.Windows.Forms.ToolStripButton tsRecipe;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripButton tsRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsCollapse;
		private System.Windows.Forms.ToolStripButton tsExpandAll;
	}
}

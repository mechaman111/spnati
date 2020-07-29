namespace SPNATI_Character_Editor.Activities
{
	partial class CaseTemplateEditor
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tabs = new Desktop.Skinning.SkinnedTabControl();
			this.tabTemplates = new System.Windows.Forms.TabPage();
			this.splitTemplate = new Desktop.Skinning.SkinnedSplitContainer();
			this.tsTemplates = new System.Windows.Forms.ToolStrip();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.lstTemplates = new Desktop.Skinning.SkinnedListBox();
			this.txtLabel = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.grpLines = new Desktop.Skinning.SkinnedGroupBox();
			this.gridLines = new Desktop.Skinning.SkinnedDataGridView();
			this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.grpConditions = new Desktop.Skinning.SkinnedGroupBox();
			this.tableConditions = new Desktop.CommonControls.PropertyTable();
			this.cboTag = new Desktop.Skinning.SkinnedComboBox();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.recGroup = new Desktop.CommonControls.RecordField();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.tabGroups = new System.Windows.Forms.TabPage();
			this.splitGroup = new Desktop.Skinning.SkinnedSplitContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAddGroup = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveGroup = new System.Windows.Forms.ToolStripButton();
			this.lstGroups = new Desktop.Skinning.SkinnedListBox();
			this.txtGroupLabel = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel4 = new Desktop.Skinning.SkinnedLabel();
			this.tabStrip = new Desktop.Skinning.SkinnedTabStrip();
			this.tabs.SuspendLayout();
			this.tabTemplates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitTemplate)).BeginInit();
			this.splitTemplate.Panel1.SuspendLayout();
			this.splitTemplate.Panel2.SuspendLayout();
			this.splitTemplate.SuspendLayout();
			this.tsTemplates.SuspendLayout();
			this.grpLines.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
			this.grpConditions.SuspendLayout();
			this.tabGroups.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitGroup)).BeginInit();
			this.splitGroup.Panel1.SuspendLayout();
			this.splitGroup.Panel2.SuspendLayout();
			this.splitGroup.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabTemplates);
			this.tabs.Controls.Add(this.tabGroups);
			this.tabs.Location = new System.Drawing.Point(3, 26);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(1085, 599);
			this.tabs.TabIndex = 0;
			this.tabs.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabs_Deselecting);
			// 
			// tabTemplates
			// 
			this.tabTemplates.BackColor = System.Drawing.Color.White;
			this.tabTemplates.Controls.Add(this.splitTemplate);
			this.tabTemplates.ForeColor = System.Drawing.Color.Black;
			this.tabTemplates.Location = new System.Drawing.Point(4, 22);
			this.tabTemplates.Name = "tabTemplates";
			this.tabTemplates.Padding = new System.Windows.Forms.Padding(3);
			this.tabTemplates.Size = new System.Drawing.Size(1077, 573);
			this.tabTemplates.TabIndex = 0;
			this.tabTemplates.Text = "Templates";
			// 
			// splitTemplate
			// 
			this.splitTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitTemplate.Location = new System.Drawing.Point(3, 3);
			this.splitTemplate.Name = "splitTemplate";
			// 
			// splitTemplate.Panel1
			// 
			this.splitTemplate.Panel1.Controls.Add(this.tsTemplates);
			this.splitTemplate.Panel1.Controls.Add(this.lstTemplates);
			// 
			// splitTemplate.Panel2
			// 
			this.splitTemplate.Panel2.Controls.Add(this.txtLabel);
			this.splitTemplate.Panel2.Controls.Add(this.skinnedLabel3);
			this.splitTemplate.Panel2.Controls.Add(this.grpLines);
			this.splitTemplate.Panel2.Controls.Add(this.grpConditions);
			this.splitTemplate.Panel2.Controls.Add(this.cboTag);
			this.splitTemplate.Panel2.Controls.Add(this.skinnedLabel2);
			this.splitTemplate.Panel2.Controls.Add(this.recGroup);
			this.splitTemplate.Panel2.Controls.Add(this.skinnedLabel1);
			this.splitTemplate.Size = new System.Drawing.Size(1071, 567);
			this.splitTemplate.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.splitTemplate.SplitterDistance = 206;
			this.splitTemplate.TabIndex = 0;
			// 
			// tsTemplates
			// 
			this.tsTemplates.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsTemplates.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsRemove});
			this.tsTemplates.Location = new System.Drawing.Point(0, 0);
			this.tsTemplates.Name = "tsTemplates";
			this.tsTemplates.Size = new System.Drawing.Size(206, 25);
			this.tsTemplates.TabIndex = 1;
			this.tsTemplates.Tag = "Background";
			this.tsTemplates.Text = "toolStrip1";
			// 
			// tsAdd
			// 
			this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(23, 22);
			this.tsAdd.Text = "Add";
			this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
			// 
			// tsRemove
			// 
			this.tsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(23, 22);
			this.tsRemove.Text = "Remove";
			this.tsRemove.Click += new System.EventHandler(this.tsRemove_Click);
			// 
			// lstTemplates
			// 
			this.lstTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstTemplates.BackColor = System.Drawing.Color.White;
			this.lstTemplates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstTemplates.ForeColor = System.Drawing.Color.Black;
			this.lstTemplates.FormattingEnabled = true;
			this.lstTemplates.Location = new System.Drawing.Point(3, 28);
			this.lstTemplates.Name = "lstTemplates";
			this.lstTemplates.Size = new System.Drawing.Size(200, 537);
			this.lstTemplates.TabIndex = 2;
			this.lstTemplates.SelectedIndexChanged += new System.EventHandler(this.lstTemplates_SelectedIndexChanged);
			// 
			// txtLabel
			// 
			this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLabel.BackColor = System.Drawing.Color.White;
			this.txtLabel.ForeColor = System.Drawing.Color.Black;
			this.txtLabel.Location = new System.Drawing.Point(299, 6);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(320, 20);
			this.txtLabel.TabIndex = 11;
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(257, 9);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(36, 13);
			this.skinnedLabel3.TabIndex = 15;
			this.skinnedLabel3.Text = "Label:";
			// 
			// grpLines
			// 
			this.grpLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpLines.BackColor = System.Drawing.Color.White;
			this.grpLines.Controls.Add(this.gridLines);
			this.grpLines.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpLines.Image = null;
			this.grpLines.Location = new System.Drawing.Point(6, 462);
			this.grpLines.Name = "grpLines";
			this.grpLines.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpLines.ShowIndicatorBar = false;
			this.grpLines.Size = new System.Drawing.Size(849, 100);
			this.grpLines.TabIndex = 14;
			this.grpLines.TabStop = false;
			this.grpLines.Text = "Default Lines";
			// 
			// gridLines
			// 
			this.gridLines.AllowUserToDeleteRows = false;
			this.gridLines.AllowUserToResizeColumns = false;
			this.gridLines.AllowUserToResizeRows = false;
			this.gridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLines.BackgroundColor = System.Drawing.Color.White;
			this.gridLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridLines.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridLines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.gridLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridLines.ColumnHeadersVisible = false;
			this.gridLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColText});
			this.gridLines.Data = null;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridLines.DefaultCellStyle = dataGridViewCellStyle5;
			this.gridLines.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridLines.EnableHeadersVisualStyles = false;
			this.gridLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridLines.GridColor = System.Drawing.Color.LightGray;
			this.gridLines.Location = new System.Drawing.Point(6, 24);
			this.gridLines.Name = "gridLines";
			this.gridLines.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridLines.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.gridLines.RowHeadersVisible = false;
			this.gridLines.Size = new System.Drawing.Size(837, 70);
			this.gridLines.TabIndex = 0;
			// 
			// ColText
			// 
			this.ColText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColText.HeaderText = "Text";
			this.ColText.Name = "ColText";
			// 
			// grpConditions
			// 
			this.grpConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpConditions.BackColor = System.Drawing.Color.White;
			this.grpConditions.Controls.Add(this.tableConditions);
			this.grpConditions.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpConditions.Image = null;
			this.grpConditions.Location = new System.Drawing.Point(6, 34);
			this.grpConditions.Name = "grpConditions";
			this.grpConditions.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpConditions.ShowIndicatorBar = false;
			this.grpConditions.Size = new System.Drawing.Size(849, 422);
			this.grpConditions.TabIndex = 12;
			this.grpConditions.TabStop = false;
			this.grpConditions.Text = "Conditions";
			// 
			// tableConditions
			// 
			this.tableConditions.AllowDelete = true;
			this.tableConditions.AllowFavorites = false;
			this.tableConditions.AllowHelp = false;
			this.tableConditions.AllowMacros = false;
			this.tableConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableConditions.BackColor = System.Drawing.Color.White;
			this.tableConditions.Data = null;
			this.tableConditions.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableConditions.HideAddField = false;
			this.tableConditions.HideSpeedButtons = false;
			this.tableConditions.Location = new System.Drawing.Point(6, 23);
			this.tableConditions.ModifyingProperty = null;
			this.tableConditions.Name = "tableConditions";
			this.tableConditions.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableConditions.PlaceholderText = "Add a condition...";
			this.tableConditions.PreserveControls = false;
			this.tableConditions.PreviewData = null;
			this.tableConditions.RemoveCaption = "Remove";
			this.tableConditions.RowHeaderWidth = 0F;
			this.tableConditions.RunInitialAddEvents = true;
			this.tableConditions.Size = new System.Drawing.Size(837, 393);
			this.tableConditions.Sorted = false;
			this.tableConditions.TabIndex = 13;
			this.tableConditions.UndoManager = null;
			this.tableConditions.UseAutoComplete = true;
			// 
			// cboTag
			// 
			this.cboTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboTag.BackColor = System.Drawing.Color.White;
			this.cboTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTag.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboTag.KeyMember = null;
			this.cboTag.Location = new System.Drawing.Point(45, 5);
			this.cboTag.Name = "cboTag";
			this.cboTag.SelectedIndex = -1;
			this.cboTag.SelectedItem = null;
			this.cboTag.Size = new System.Drawing.Size(206, 23);
			this.cboTag.Sorted = false;
			this.cboTag.TabIndex = 10;
			this.cboTag.Text = "skinnedComboBox1";
			this.cboTag.SelectedIndexChanged += new System.EventHandler(this.cboTag_SelectedIndexChanged);
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(5, 9);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(34, 13);
			this.skinnedLabel2.TabIndex = 3;
			this.skinnedLabel2.Text = "Type:";
			// 
			// recGroup
			// 
			this.recGroup.AllowCreate = false;
			this.recGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.recGroup.Location = new System.Drawing.Point(670, 5);
			this.recGroup.Name = "recGroup";
			this.recGroup.PlaceholderText = null;
			this.recGroup.Record = null;
			this.recGroup.RecordContext = null;
			this.recGroup.RecordFilter = null;
			this.recGroup.RecordKey = null;
			this.recGroup.RecordType = null;
			this.recGroup.Size = new System.Drawing.Size(185, 20);
			this.recGroup.TabIndex = 12;
			this.recGroup.UseAutoComplete = false;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(625, 9);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(39, 13);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Group:";
			// 
			// tabGroups
			// 
			this.tabGroups.BackColor = System.Drawing.Color.White;
			this.tabGroups.Controls.Add(this.splitGroup);
			this.tabGroups.ForeColor = System.Drawing.Color.Black;
			this.tabGroups.Location = new System.Drawing.Point(4, 22);
			this.tabGroups.Name = "tabGroups";
			this.tabGroups.Padding = new System.Windows.Forms.Padding(3);
			this.tabGroups.Size = new System.Drawing.Size(1077, 573);
			this.tabGroups.TabIndex = 1;
			this.tabGroups.Text = "Groups";
			// 
			// splitGroup
			// 
			this.splitGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitGroup.Location = new System.Drawing.Point(3, 3);
			this.splitGroup.Name = "splitGroup";
			// 
			// splitGroup.Panel1
			// 
			this.splitGroup.Panel1.Controls.Add(this.toolStrip1);
			this.splitGroup.Panel1.Controls.Add(this.lstGroups);
			// 
			// splitGroup.Panel2
			// 
			this.splitGroup.Panel2.Controls.Add(this.txtGroupLabel);
			this.splitGroup.Panel2.Controls.Add(this.skinnedLabel4);
			this.splitGroup.Size = new System.Drawing.Size(1071, 567);
			this.splitGroup.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.splitGroup.SplitterDistance = 205;
			this.splitGroup.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddGroup,
            this.tsRemoveGroup});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(205, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Tag = "Background";
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsAddGroup
			// 
			this.tsAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddGroup.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddGroup.Name = "tsAddGroup";
			this.tsAddGroup.Size = new System.Drawing.Size(23, 22);
			this.tsAddGroup.Text = "Add";
			this.tsAddGroup.Click += new System.EventHandler(this.tsAddGroup_Click);
			// 
			// tsRemoveGroup
			// 
			this.tsRemoveGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveGroup.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemoveGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveGroup.Name = "tsRemoveGroup";
			this.tsRemoveGroup.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveGroup.Text = "Remove";
			this.tsRemoveGroup.Click += new System.EventHandler(this.tsRemoveGroup_Click);
			// 
			// lstGroups
			// 
			this.lstGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstGroups.BackColor = System.Drawing.Color.White;
			this.lstGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstGroups.ForeColor = System.Drawing.Color.Black;
			this.lstGroups.FormattingEnabled = true;
			this.lstGroups.Location = new System.Drawing.Point(1, 29);
			this.lstGroups.Name = "lstGroups";
			this.lstGroups.Size = new System.Drawing.Size(201, 524);
			this.lstGroups.TabIndex = 2;
			this.lstGroups.SelectedIndexChanged += new System.EventHandler(this.lstGroups_SelectedIndexChanged);
			// 
			// txtGroupLabel
			// 
			this.txtGroupLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGroupLabel.BackColor = System.Drawing.Color.White;
			this.txtGroupLabel.ForeColor = System.Drawing.Color.Black;
			this.txtGroupLabel.Location = new System.Drawing.Point(48, 5);
			this.txtGroupLabel.Name = "txtGroupLabel";
			this.txtGroupLabel.Size = new System.Drawing.Size(314, 20);
			this.txtGroupLabel.TabIndex = 16;
			// 
			// skinnedLabel4
			// 
			this.skinnedLabel4.AutoSize = true;
			this.skinnedLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel4.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel4.Location = new System.Drawing.Point(6, 8);
			this.skinnedLabel4.Name = "skinnedLabel4";
			this.skinnedLabel4.Size = new System.Drawing.Size(36, 13);
			this.skinnedLabel4.TabIndex = 17;
			this.skinnedLabel4.Text = "Label:";
			// 
			// tabStrip
			// 
			this.tabStrip.AddCaption = null;
			this.tabStrip.DecorationText = null;
			this.tabStrip.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabStrip.Location = new System.Drawing.Point(0, 0);
			this.tabStrip.Margin = new System.Windows.Forms.Padding(0);
			this.tabStrip.Name = "tabStrip";
			this.tabStrip.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.tabStrip.ShowAddButton = false;
			this.tabStrip.ShowCloseButton = false;
			this.tabStrip.Size = new System.Drawing.Size(1091, 23);
			this.tabStrip.StartMargin = 5;
			this.tabStrip.TabControl = this.tabs;
			this.tabStrip.TabIndex = 1;
			this.tabStrip.TabMargin = 5;
			this.tabStrip.TabPadding = 20;
			this.tabStrip.TabSize = 100;
			this.tabStrip.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tabStrip.Text = "skinnedTabStrip1";
			this.tabStrip.Vertical = false;
			// 
			// CaseTemplateEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabStrip);
			this.Controls.Add(this.tabs);
			this.Name = "CaseTemplateEditor";
			this.Size = new System.Drawing.Size(1091, 625);
			this.tabs.ResumeLayout(false);
			this.tabTemplates.ResumeLayout(false);
			this.splitTemplate.Panel1.ResumeLayout(false);
			this.splitTemplate.Panel1.PerformLayout();
			this.splitTemplate.Panel2.ResumeLayout(false);
			this.splitTemplate.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitTemplate)).EndInit();
			this.splitTemplate.ResumeLayout(false);
			this.tsTemplates.ResumeLayout(false);
			this.tsTemplates.PerformLayout();
			this.grpLines.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
			this.grpConditions.ResumeLayout(false);
			this.tabGroups.ResumeLayout(false);
			this.splitGroup.Panel1.ResumeLayout(false);
			this.splitGroup.Panel1.PerformLayout();
			this.splitGroup.Panel2.ResumeLayout(false);
			this.splitGroup.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitGroup)).EndInit();
			this.splitGroup.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedTabControl tabs;
		private System.Windows.Forms.TabPage tabTemplates;
		private System.Windows.Forms.TabPage tabGroups;
		private Desktop.Skinning.SkinnedTabStrip tabStrip;
		private Desktop.Skinning.SkinnedSplitContainer splitTemplate;
		private System.Windows.Forms.ToolStrip tsTemplates;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private Desktop.Skinning.SkinnedListBox lstTemplates;
		private Desktop.Skinning.SkinnedSplitContainer splitGroup;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsAddGroup;
		private System.Windows.Forms.ToolStripButton tsRemoveGroup;
		private Desktop.Skinning.SkinnedListBox lstGroups;
		private Desktop.CommonControls.RecordField recGroup;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedComboBox cboTag;
		private Desktop.Skinning.SkinnedGroupBox grpConditions;
		private Desktop.CommonControls.PropertyTable tableConditions;
		private Desktop.Skinning.SkinnedGroupBox grpLines;
		private Desktop.Skinning.SkinnedTextBox txtLabel;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
		private Desktop.Skinning.SkinnedTextBox txtGroupLabel;
		private Desktop.Skinning.SkinnedLabel skinnedLabel4;
		private Desktop.Skinning.SkinnedDataGridView gridLines;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColText;
	}
}

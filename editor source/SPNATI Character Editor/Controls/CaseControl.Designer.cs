namespace SPNATI_Character_Editor.Controls
{
	partial class CaseControl
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
			this.splitCase = new System.Windows.Forms.SplitContainer();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.tabCase = new Desktop.Skinning.SkinnedTabControl();
			this.tabConditions = new System.Windows.Forms.TabPage();
			this.chkPlayOnce = new Desktop.Skinning.SkinnedCheckBox();
			this.stripConditions = new Desktop.Skinning.SkinnedTabStrip();
			this.tabsConditions = new Desktop.Skinning.SkinnedTabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.skinnedPanel2 = new Desktop.Skinning.SkinnedPanel();
			this.tableConditions = new Desktop.CommonControls.PropertyTable();
			this.valPriority = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label73 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.txtFolder = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.cmdColorCode = new System.Windows.Forms.Button();
			this.txtLabel = new Desktop.Skinning.SkinnedTextBox();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.label34 = new Desktop.Skinning.SkinnedLabel();
			this.cboCaseTags = new Desktop.Skinning.SkinnedComboBox();
			this.lblHelpText = new Desktop.Skinning.SkinnedLabel();
			this.groupBox3 = new Desktop.Skinning.SkinnedGroupBox();
			this.gridStages = new SPNATI_Character_Editor.Controls.StageGrid();
			this.chkBackground = new Desktop.Skinning.SkinnedCheckBox();
			this.tabTags = new System.Windows.Forms.TabPage();
			this.lstRemoveTags = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.lstAddTags = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.stripCase = new Desktop.Skinning.SkinnedTabStrip();
			this.containerDialogue = new Desktop.Skinning.SkinnedPanel();
			this.tabs = new Desktop.Skinning.SkinnedTabControl();
			this.tabDialogue = new System.Windows.Forms.TabPage();
			this.lblAvailableVars = new Desktop.Skinning.SkinnedLabel();
			this.cmdCopyAll = new Desktop.Skinning.SkinnedButton();
			this.cmdPasteAll = new Desktop.Skinning.SkinnedButton();
			this.gridDialogue = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.tabNotes = new System.Windows.Forms.TabPage();
			this.txtNotes = new Desktop.Skinning.SkinnedTextBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.stripTabs = new Desktop.Skinning.SkinnedTabStrip();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitCase)).BeginInit();
			this.splitCase.Panel1.SuspendLayout();
			this.splitCase.Panel2.SuspendLayout();
			this.splitCase.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.tabCase.SuspendLayout();
			this.tabConditions.SuspendLayout();
			this.tabsConditions.SuspendLayout();
			this.skinnedPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valPriority)).BeginInit();
			this.skinnedGroupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabTags.SuspendLayout();
			this.containerDialogue.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabDialogue.SuspendLayout();
			this.tabNotes.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitCase
			// 
			this.splitCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitCase.Location = new System.Drawing.Point(0, 0);
			this.splitCase.Name = "splitCase";
			this.splitCase.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitCase.Panel1
			// 
			this.splitCase.Panel1.Controls.Add(this.skinnedPanel1);
			this.splitCase.Panel1.Controls.Add(this.stripCase);
			// 
			// splitCase.Panel2
			// 
			this.splitCase.Panel2.Controls.Add(this.containerDialogue);
			this.splitCase.Panel2.Controls.Add(this.stripTabs);
			this.splitCase.Size = new System.Drawing.Size(670, 680);
			this.splitCase.SplitterDistance = 386;
			this.splitCase.TabIndex = 62;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel1.Controls.Add(this.tabCase);
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 28);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.skinnedPanel1.Size = new System.Drawing.Size(670, 358);
			this.skinnedPanel1.TabIndex = 98;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.Top;
			// 
			// tabCase
			// 
			this.tabCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabCase.Controls.Add(this.tabConditions);
			this.tabCase.Controls.Add(this.tabTags);
			this.tabCase.Location = new System.Drawing.Point(1, 0);
			this.tabCase.Name = "tabCase";
			this.tabCase.SelectedIndex = 0;
			this.tabCase.Size = new System.Drawing.Size(665, 356);
			this.tabCase.TabIndex = 96;
			// 
			// tabConditions
			// 
			this.tabConditions.BackColor = System.Drawing.Color.White;
			this.tabConditions.Controls.Add(this.chkPlayOnce);
			this.tabConditions.Controls.Add(this.stripConditions);
			this.tabConditions.Controls.Add(this.skinnedPanel2);
			this.tabConditions.Controls.Add(this.valPriority);
			this.tabConditions.Controls.Add(this.label73);
			this.tabConditions.Controls.Add(this.skinnedGroupBox1);
			this.tabConditions.Controls.Add(this.groupBox3);
			this.tabConditions.Controls.Add(this.chkBackground);
			this.tabConditions.ForeColor = System.Drawing.Color.Black;
			this.tabConditions.Location = new System.Drawing.Point(4, 22);
			this.tabConditions.Name = "tabConditions";
			this.tabConditions.Padding = new System.Windows.Forms.Padding(3);
			this.tabConditions.Size = new System.Drawing.Size(657, 330);
			this.tabConditions.TabIndex = 0;
			this.tabConditions.Text = "General";
			// 
			// chkPlayOnce
			// 
			this.chkPlayOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkPlayOnce.AutoSize = true;
			this.chkPlayOnce.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkPlayOnce.Location = new System.Drawing.Point(392, 128);
			this.chkPlayOnce.Name = "chkPlayOnce";
			this.chkPlayOnce.Size = new System.Drawing.Size(75, 17);
			this.chkPlayOnce.TabIndex = 63;
			this.chkPlayOnce.Text = "Play Once";
			this.toolTip1.SetToolTip(this.chkPlayOnce, "Only play a single line from this case once per game");
			this.chkPlayOnce.UseVisualStyleBackColor = true;
			// 
			// stripConditions
			// 
			this.stripConditions.AddCaption = "AND";
			this.stripConditions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stripConditions.DecorationText = null;
			this.stripConditions.Location = new System.Drawing.Point(6, 122);
			this.stripConditions.Margin = new System.Windows.Forms.Padding(0);
			this.stripConditions.Name = "stripConditions";
			this.stripConditions.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripConditions.ShowAddButton = true;
			this.stripConditions.ShowCloseButton = false;
			this.stripConditions.Size = new System.Drawing.Size(383, 28);
			this.stripConditions.StartMargin = 5;
			this.stripConditions.TabControl = this.tabsConditions;
			this.stripConditions.TabIndex = 61;
			this.stripConditions.TabMargin = 5;
			this.stripConditions.TabPadding = 10;
			this.stripConditions.TabSize = -1;
			this.stripConditions.TabType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.stripConditions.Text = "skinnedTabStrip1";
			this.stripConditions.Vertical = false;
			this.stripConditions.CloseButtonClicked += new System.EventHandler(this.stripConditions_CloseButtonClicked);
			this.stripConditions.AddButtonClicked += new System.EventHandler(this.stripConditions_AddButtonClicked);
			// 
			// tabsConditions
			// 
			this.tabsConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabsConditions.Controls.Add(this.tabPage1);
			this.tabsConditions.Location = new System.Drawing.Point(32767, 15);
			this.tabsConditions.Margin = new System.Windows.Forms.Padding(1);
			this.tabsConditions.Name = "tabsConditions";
			this.tabsConditions.SelectedIndex = 0;
			this.tabsConditions.Size = new System.Drawing.Size(50, 50);
			this.tabsConditions.TabIndex = 62;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.White;
			this.tabPage1.ForeColor = System.Drawing.Color.Black;
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(42, 24);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Conditions";
			// 
			// skinnedPanel2
			// 
			this.skinnedPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel2.Controls.Add(this.tabsConditions);
			this.skinnedPanel2.Controls.Add(this.tableConditions);
			this.skinnedPanel2.Location = new System.Drawing.Point(5, 149);
			this.skinnedPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.skinnedPanel2.Name = "skinnedPanel2";
			this.skinnedPanel2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedPanel2.Size = new System.Drawing.Size(649, 178);
			this.skinnedPanel2.TabIndex = 32;
			this.skinnedPanel2.TabSide = Desktop.Skinning.TabSide.Top;
			// 
			// tableConditions
			// 
			this.tableConditions.AllowDelete = true;
			this.tableConditions.AllowFavorites = true;
			this.tableConditions.AllowHelp = false;
			this.tableConditions.AllowMacros = true;
			this.tableConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableConditions.BackColor = System.Drawing.Color.White;
			this.tableConditions.Data = null;
			this.tableConditions.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableConditions.HideAddField = true;
			this.tableConditions.HideSpeedButtons = false;
			this.tableConditions.Location = new System.Drawing.Point(1, 2);
			this.tableConditions.Margin = new System.Windows.Forms.Padding(1);
			this.tableConditions.ModifyingProperty = null;
			this.tableConditions.Name = "tableConditions";
			this.tableConditions.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableConditions.PlaceholderText = "Add a condition";
			this.tableConditions.PreserveControls = false;
			this.tableConditions.PreviewData = null;
			this.tableConditions.RemoveCaption = "Remove condition";
			this.tableConditions.RowHeaderWidth = 0F;
			this.tableConditions.RunInitialAddEvents = true;
			this.tableConditions.Size = new System.Drawing.Size(647, 175);
			this.tableConditions.Sorted = false;
			this.tableConditions.TabIndex = 31;
			this.tableConditions.UndoManager = null;
			this.tableConditions.UseAutoComplete = true;
			this.tableConditions.EditingMacro += new System.EventHandler<Desktop.CommonControls.MacroArgs>(this.tableConditions_EditingMacro);
			this.tableConditions.MacroChanged += new System.EventHandler<Desktop.CommonControls.MacroArgs>(this.tableConditions_MacroChanged);
			// 
			// valPriority
			// 
			this.valPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valPriority.BackColor = System.Drawing.Color.White;
			this.valPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valPriority.ForeColor = System.Drawing.Color.Black;
			this.valPriority.Location = new System.Drawing.Point(599, 127);
			this.valPriority.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.valPriority.Name = "valPriority";
			this.valPriority.Size = new System.Drawing.Size(54, 20);
			this.valPriority.TabIndex = 7;
			// 
			// label73
			// 
			this.label73.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label73.AutoSize = true;
			this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label73.ForeColor = System.Drawing.Color.Black;
			this.label73.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label73.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label73.Location = new System.Drawing.Point(552, 129);
			this.label73.Name = "label73";
			this.label73.Size = new System.Drawing.Size(41, 13);
			this.label73.TabIndex = 60;
			this.label73.Text = "Priority:";
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox1.Controls.Add(this.txtFolder);
			this.skinnedGroupBox1.Controls.Add(this.skinnedLabel1);
			this.skinnedGroupBox1.Controls.Add(this.cmdColorCode);
			this.skinnedGroupBox1.Controls.Add(this.txtLabel);
			this.skinnedGroupBox1.Controls.Add(this.label4);
			this.skinnedGroupBox1.Controls.Add(this.label34);
			this.skinnedGroupBox1.Controls.Add(this.cboCaseTags);
			this.skinnedGroupBox1.Controls.Add(this.lblHelpText);
			this.skinnedGroupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox1.Image = null;
			this.skinnedGroupBox1.Location = new System.Drawing.Point(478, 6);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox1.ShowIndicatorBar = false;
			this.skinnedGroupBox1.Size = new System.Drawing.Size(175, 113);
			this.skinnedGroupBox1.TabIndex = 39;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Case";
			// 
			// txtFolder
			// 
			this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFolder.BackColor = System.Drawing.Color.White;
			this.txtFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtFolder.ForeColor = System.Drawing.Color.Black;
			this.txtFolder.Location = new System.Drawing.Point(46, 64);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(123, 20);
			this.txtFolder.TabIndex = 70;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel1.Location = new System.Drawing.Point(6, 67);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(39, 13);
			this.skinnedLabel1.TabIndex = 69;
			this.skinnedLabel1.Text = "Folder:";
			// 
			// cmdColorCode
			// 
			this.cmdColorCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdColorCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdColorCode.Location = new System.Drawing.Point(146, 85);
			this.cmdColorCode.Name = "cmdColorCode";
			this.cmdColorCode.Size = new System.Drawing.Size(23, 23);
			this.cmdColorCode.TabIndex = 68;
			this.toolTip1.SetToolTip(this.cmdColorCode, "Set label color");
			this.cmdColorCode.UseVisualStyleBackColor = true;
			this.cmdColorCode.Click += new System.EventHandler(this.cmdColorCode_Click);
			// 
			// txtLabel
			// 
			this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLabel.BackColor = System.Drawing.Color.White;
			this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtLabel.ForeColor = System.Drawing.Color.Black;
			this.txtLabel.Location = new System.Drawing.Point(46, 87);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(94, 20);
			this.txtLabel.TabIndex = 67;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label4.Location = new System.Drawing.Point(6, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(36, 13);
			this.label4.TabIndex = 66;
			this.label4.Text = "Label:";
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label34.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label34.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label34.Location = new System.Drawing.Point(6, 24);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(34, 13);
			this.label34.TabIndex = 63;
			this.label34.Text = "Type:";
			// 
			// cboCaseTags
			// 
			this.cboCaseTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboCaseTags.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboCaseTags.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboCaseTags.BackColor = System.Drawing.Color.White;
			this.cboCaseTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCaseTags.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboCaseTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboCaseTags.FormattingEnabled = true;
			this.cboCaseTags.KeyMember = null;
			this.cboCaseTags.Location = new System.Drawing.Point(46, 21);
			this.cboCaseTags.Name = "cboCaseTags";
			this.cboCaseTags.SelectedIndex = -1;
			this.cboCaseTags.SelectedItem = null;
			this.cboCaseTags.Size = new System.Drawing.Size(123, 21);
			this.cboCaseTags.Sorted = false;
			this.cboCaseTags.TabIndex = 64;
			this.cboCaseTags.SelectedIndexChanged += new System.EventHandler(this.cboCaseTags_SelectedIndexChanged);
			// 
			// lblHelpText
			// 
			this.lblHelpText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHelpText.AutoEllipsis = true;
			this.lblHelpText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblHelpText.ForeColor = System.Drawing.Color.Black;
			this.lblHelpText.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblHelpText.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblHelpText.Location = new System.Drawing.Point(6, 45);
			this.lblHelpText.Name = "lblHelpText";
			this.lblHelpText.Size = new System.Drawing.Size(163, 13);
			this.lblHelpText.TabIndex = 65;
			this.lblHelpText.Text = "Help Text";
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.Color.White;
			this.groupBox3.Controls.Add(this.gridStages);
			this.groupBox3.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.groupBox3.Image = null;
			this.groupBox3.Location = new System.Drawing.Point(6, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.groupBox3.ShowIndicatorBar = false;
			this.groupBox3.Size = new System.Drawing.Size(467, 113);
			this.groupBox3.TabIndex = 37;
			this.groupBox3.TabStop = false;
			// 
			// gridStages
			// 
			this.gridStages.AutoSize = true;
			this.gridStages.ColumnHeaderHeight = 72;
			this.gridStages.Location = new System.Drawing.Point(6, 4);
			this.gridStages.Name = "gridStages";
			this.gridStages.ShowSelectAll = true;
			this.gridStages.Size = new System.Drawing.Size(72, 102);
			this.gridStages.TabIndex = 37;
			// 
			// chkBackground
			// 
			this.chkBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkBackground.AutoSize = true;
			this.chkBackground.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkBackground.Location = new System.Drawing.Point(468, 128);
			this.chkBackground.Name = "chkBackground";
			this.chkBackground.Size = new System.Drawing.Size(84, 17);
			this.chkBackground.TabIndex = 62;
			this.chkBackground.Text = "Background";
			this.toolTip1.SetToolTip(this.chkBackground, "When conditions are met, runs this case in the background (setting markers, state" +
        ", etc.) without displaying to the player.");
			this.chkBackground.UseVisualStyleBackColor = true;
			// 
			// tabTags
			// 
			this.tabTags.BackColor = System.Drawing.Color.White;
			this.tabTags.Controls.Add(this.lstRemoveTags);
			this.tabTags.Controls.Add(this.lstAddTags);
			this.tabTags.Controls.Add(this.label3);
			this.tabTags.Controls.Add(this.label2);
			this.tabTags.ForeColor = System.Drawing.Color.Black;
			this.tabTags.Location = new System.Drawing.Point(4, 22);
			this.tabTags.Name = "tabTags";
			this.tabTags.Padding = new System.Windows.Forms.Padding(3);
			this.tabTags.Size = new System.Drawing.Size(657, 330);
			this.tabTags.TabIndex = 1;
			this.tabTags.Text = "Tags";
			// 
			// lstRemoveTags
			// 
			this.lstRemoveTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstRemoveTags.Location = new System.Drawing.Point(348, 19);
			this.lstRemoveTags.Name = "lstRemoveTags";
			this.lstRemoveTags.RecordContext = null;
			this.lstRemoveTags.RecordFilter = null;
			this.lstRemoveTags.RecordType = null;
			this.lstRemoveTags.SelectedItems = new string[0];
			this.lstRemoveTags.Size = new System.Drawing.Size(303, 305);
			this.lstRemoveTags.TabIndex = 3;
			// 
			// lstAddTags
			// 
			this.lstAddTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstAddTags.Location = new System.Drawing.Point(6, 19);
			this.lstAddTags.Name = "lstAddTags";
			this.lstAddTags.RecordContext = null;
			this.lstAddTags.RecordFilter = null;
			this.lstAddTags.RecordType = null;
			this.lstAddTags.SelectedItems = new string[0];
			this.lstAddTags.Size = new System.Drawing.Size(336, 305);
			this.lstAddTags.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(345, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(158, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Tags to Remove with This Case";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(137, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Tags to Add with This Case";
			// 
			// stripCase
			// 
			this.stripCase.AddCaption = null;
			this.stripCase.DecorationText = null;
			this.stripCase.Dock = System.Windows.Forms.DockStyle.Top;
			this.stripCase.Location = new System.Drawing.Point(0, 0);
			this.stripCase.Margin = new System.Windows.Forms.Padding(0);
			this.stripCase.Name = "stripCase";
			this.stripCase.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.stripCase.ShowAddButton = false;
			this.stripCase.ShowCloseButton = false;
			this.stripCase.Size = new System.Drawing.Size(670, 28);
			this.stripCase.StartMargin = 5;
			this.stripCase.TabControl = this.tabCase;
			this.stripCase.TabIndex = 97;
			this.stripCase.TabMargin = 5;
			this.stripCase.TabPadding = 10;
			this.stripCase.TabSize = -1;
			this.stripCase.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripCase.Vertical = false;
			// 
			// containerDialogue
			// 
			this.containerDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.containerDialogue.Controls.Add(this.tabs);
			this.containerDialogue.Location = new System.Drawing.Point(0, 23);
			this.containerDialogue.Name = "containerDialogue";
			this.containerDialogue.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.containerDialogue.Size = new System.Drawing.Size(670, 267);
			this.containerDialogue.TabIndex = 97;
			this.containerDialogue.TabSide = Desktop.Skinning.TabSide.Top;
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabDialogue);
			this.tabs.Controls.Add(this.tabNotes);
			this.tabs.Location = new System.Drawing.Point(1, 0);
			this.tabs.Margin = new System.Windows.Forms.Padding(0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(668, 266);
			this.tabs.TabIndex = 95;
			// 
			// tabDialogue
			// 
			this.tabDialogue.BackColor = System.Drawing.Color.White;
			this.tabDialogue.Controls.Add(this.lblAvailableVars);
			this.tabDialogue.Controls.Add(this.cmdCopyAll);
			this.tabDialogue.Controls.Add(this.cmdPasteAll);
			this.tabDialogue.Controls.Add(this.gridDialogue);
			this.tabDialogue.ForeColor = System.Drawing.Color.Black;
			this.tabDialogue.Location = new System.Drawing.Point(4, 22);
			this.tabDialogue.Name = "tabDialogue";
			this.tabDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tabDialogue.Size = new System.Drawing.Size(660, 240);
			this.tabDialogue.TabIndex = 0;
			this.tabDialogue.Text = "Dialogue";
			// 
			// lblAvailableVars
			// 
			this.lblAvailableVars.AutoSize = true;
			this.lblAvailableVars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblAvailableVars.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblAvailableVars.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.lblAvailableVars.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.lblAvailableVars.Location = new System.Drawing.Point(2, 8);
			this.lblAvailableVars.Name = "lblAvailableVars";
			this.lblAvailableVars.Size = new System.Drawing.Size(158, 13);
			this.lblAvailableVars.TabIndex = 32;
			this.lblAvailableVars.Text = "Hover to see available variables";
			// 
			// cmdCopyAll
			// 
			this.cmdCopyAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCopyAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCopyAll.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCopyAll.Flat = true;
			this.cmdCopyAll.ForeColor = System.Drawing.Color.Blue;
			this.cmdCopyAll.Location = new System.Drawing.Point(496, 3);
			this.cmdCopyAll.Name = "cmdCopyAll";
			this.cmdCopyAll.Size = new System.Drawing.Size(77, 23);
			this.cmdCopyAll.TabIndex = 39;
			this.cmdCopyAll.Text = "Copy All";
			this.cmdCopyAll.UseVisualStyleBackColor = true;
			this.cmdCopyAll.Click += new System.EventHandler(this.cmdCopyAll_Click);
			// 
			// cmdPasteAll
			// 
			this.cmdPasteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPasteAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdPasteAll.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdPasteAll.Flat = true;
			this.cmdPasteAll.ForeColor = System.Drawing.Color.Blue;
			this.cmdPasteAll.Location = new System.Drawing.Point(579, 3);
			this.cmdPasteAll.Name = "cmdPasteAll";
			this.cmdPasteAll.Size = new System.Drawing.Size(77, 23);
			this.cmdPasteAll.TabIndex = 40;
			this.cmdPasteAll.Text = "Paste All";
			this.cmdPasteAll.UseVisualStyleBackColor = true;
			this.cmdPasteAll.Click += new System.EventHandler(this.cmdPasteAll_Click);
			// 
			// gridDialogue
			// 
			this.gridDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridDialogue.Location = new System.Drawing.Point(3, 32);
			this.gridDialogue.Name = "gridDialogue";
			this.gridDialogue.ReadOnly = false;
			this.gridDialogue.Size = new System.Drawing.Size(651, 205);
			this.gridDialogue.TabIndex = 42;
			this.gridDialogue.KeyDown += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.gridDialogue_KeyDown);
			this.gridDialogue.HighlightRow += new System.EventHandler<int>(this.gridDialogue_HighlightRow);
			// 
			// tabNotes
			// 
			this.tabNotes.BackColor = System.Drawing.Color.White;
			this.tabNotes.Controls.Add(this.txtNotes);
			this.tabNotes.Controls.Add(this.label1);
			this.tabNotes.ForeColor = System.Drawing.Color.Black;
			this.tabNotes.Location = new System.Drawing.Point(4, 22);
			this.tabNotes.Name = "tabNotes";
			this.tabNotes.Padding = new System.Windows.Forms.Padding(3);
			this.tabNotes.Size = new System.Drawing.Size(660, 240);
			this.tabNotes.TabIndex = 1;
			this.tabNotes.Text = "Notes";
			// 
			// txtNotes
			// 
			this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNotes.BackColor = System.Drawing.Color.White;
			this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtNotes.ForeColor = System.Drawing.Color.Black;
			this.txtNotes.Location = new System.Drawing.Point(5, 19);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(651, 221);
			this.txtNotes.TabIndex = 1;
			this.txtNotes.Validated += new System.EventHandler(this.txtNotes_Validated);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(6, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(413, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Jot down any personal notes about this case here. These notes won\'t appear in gam" +
    "e.";
			// 
			// stripTabs
			// 
			this.stripTabs.AddCaption = null;
			this.stripTabs.DecorationText = null;
			this.stripTabs.Dock = System.Windows.Forms.DockStyle.Top;
			this.stripTabs.Location = new System.Drawing.Point(0, 0);
			this.stripTabs.Margin = new System.Windows.Forms.Padding(0);
			this.stripTabs.Name = "stripTabs";
			this.stripTabs.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripTabs.ShowAddButton = false;
			this.stripTabs.ShowCloseButton = false;
			this.stripTabs.Size = new System.Drawing.Size(670, 23);
			this.stripTabs.StartMargin = 5;
			this.stripTabs.TabControl = this.tabs;
			this.stripTabs.TabIndex = 96;
			this.stripTabs.TabMargin = 5;
			this.stripTabs.TabPadding = 10;
			this.stripTabs.TabSize = -1;
			this.stripTabs.TabType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.stripTabs.Vertical = false;
			// 
			// CaseControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitCase);
			this.Name = "CaseControl";
			this.Size = new System.Drawing.Size(670, 680);
			this.Load += new System.EventHandler(this.CaseControl_Load);
			this.splitCase.Panel1.ResumeLayout(false);
			this.splitCase.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitCase)).EndInit();
			this.splitCase.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.tabCase.ResumeLayout(false);
			this.tabConditions.ResumeLayout(false);
			this.tabConditions.PerformLayout();
			this.tabsConditions.ResumeLayout(false);
			this.skinnedPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.valPriority)).EndInit();
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabTags.ResumeLayout(false);
			this.tabTags.PerformLayout();
			this.containerDialogue.ResumeLayout(false);
			this.tabs.ResumeLayout(false);
			this.tabDialogue.ResumeLayout(false);
			this.tabDialogue.PerformLayout();
			this.tabNotes.ResumeLayout(false);
			this.tabNotes.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitCase;
		private Desktop.Skinning.SkinnedTabControl tabCase;
		private System.Windows.Forms.TabPage tabConditions;
		private Desktop.Skinning.SkinnedGroupBox groupBox3;
		private Desktop.Skinning.SkinnedNumericUpDown valPriority;
		private Desktop.Skinning.SkinnedLabel label73;
		private Desktop.CommonControls.PropertyTable tableConditions;
		private System.Windows.Forms.TabPage tabTags;
		private RecordSelectBox lstRemoveTags;
		private RecordSelectBox lstAddTags;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedTabControl tabs;
		private System.Windows.Forms.TabPage tabDialogue;
		private Desktop.Skinning.SkinnedLabel lblAvailableVars;
		private Desktop.Skinning.SkinnedButton cmdCopyAll;
		private Desktop.Skinning.SkinnedButton cmdPasteAll;
		private DialogueGrid gridDialogue;
		private System.Windows.Forms.TabPage tabNotes;
		private Desktop.Skinning.SkinnedTextBox txtNotes;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel lblHelpText;
		private Desktop.Skinning.SkinnedComboBox cboCaseTags;
		private Desktop.Skinning.SkinnedLabel label34;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedTextBox txtLabel;
		private StageGrid gridStages;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedTabStrip stripTabs;
		private Desktop.Skinning.SkinnedTabStrip stripCase;
		private Desktop.Skinning.SkinnedPanel containerDialogue;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private System.Windows.Forms.Button cmdColorCode;
		private Desktop.Skinning.SkinnedTextBox txtFolder;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedTabStrip stripConditions;
		private Desktop.Skinning.SkinnedTabControl tabsConditions;
		private System.Windows.Forms.TabPage tabPage1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel2;
		private Desktop.Skinning.SkinnedCheckBox chkBackground;
		private Desktop.Skinning.SkinnedCheckBox chkPlayOnce;
	}
}

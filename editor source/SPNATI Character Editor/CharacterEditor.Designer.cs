namespace SPNATI_Character_Editor
{
	partial class CharacterEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterEditor));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.importtxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exporttxtFileForPythonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.validatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.currentCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allCharactersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dialogueTesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.graphsToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.howToGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutCharacterEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.txtLabel = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.picPortrait = new System.Windows.Forms.PictureBox();
			this.treeDialogue = new System.Windows.Forms.TreeView();
			this.cmdAddStage = new System.Windows.Forms.Button();
			this.cmdAddLine = new System.Windows.Forms.Button();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.cboLineTarget = new System.Windows.Forms.ComboBox();
			this.cmdCopy = new System.Windows.Forms.Button();
			this.cmdInsert = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.cboLineFilter = new System.Windows.Forms.ComboBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabMetadata = new System.Windows.Forms.TabPage();
			this.gridAI = new System.Windows.Forms.DataGridView();
			this.ColAIStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDifficulty = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.cmdAddToListing = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.cboDefaultPic = new System.Windows.Forms.ComboBox();
			this.label24 = new System.Windows.Forms.Label();
			this.txtHeight = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.gridTags = new System.Windows.Forms.DataGridView();
			this.ColTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label19 = new System.Windows.Forms.Label();
			this.txtArtist = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.txtWriter = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.valRounds = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.cboSize = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.cboGender = new System.Windows.Forms.ComboBox();
			this.tabWardrobe = new System.Windows.Forms.TabPage();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblPositionHelp = new System.Windows.Forms.Label();
			this.lblTypeHelp = new System.Windows.Forms.Label();
			this.txtClothesProperName = new System.Windows.Forms.TextBox();
			this.cboClothesPosition = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.cboClothesType = new System.Windows.Forms.ComboBox();
			this.txtClothesLowerCase = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.cmdClothesDown = new System.Windows.Forms.Button();
			this.cmdClothesUp = new System.Windows.Forms.Button();
			this.cmdRemoveClothes = new System.Windows.Forms.Button();
			this.cmdAddClothes = new System.Windows.Forms.Button();
			this.lstClothes = new System.Windows.Forms.ListBox();
			this.tabImages = new System.Windows.Forms.TabPage();
			this.tabDialogue = new System.Windows.Forms.TabPage();
			this.splitDialogue = new System.Windows.Forms.SplitContainer();
			this.cboTreeTarget = new System.Windows.Forms.ComboBox();
			this.cmdAddDialogue = new System.Windows.Forms.Button();
			this.label40 = new System.Windows.Forms.Label();
			this.cboTreeFilter = new System.Windows.Forms.ComboBox();
			this.cmdRemoveDialogue = new System.Windows.Forms.Button();
			this.label39 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.cmdSplit = new System.Windows.Forms.Button();
			this.label35 = new System.Windows.Forms.Label();
			this.grpCase = new System.Windows.Forms.GroupBox();
			this.lblHelpText = new System.Windows.Forms.Label();
			this.cmdPasteAll = new System.Windows.Forms.Button();
			this.cmdCopyAll = new System.Windows.Forms.Button();
			this.cboCaseTags = new System.Windows.Forms.ComboBox();
			this.label34 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lblAvailableVars = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.flowStageChecks = new System.Windows.Forms.FlowLayoutPanel();
			this.grpConditions = new System.Windows.Forms.GroupBox();
			this.tabControlConditions = new System.Windows.Forms.TabControl();
			this.tabTarget = new System.Windows.Forms.TabPage();
			this.label21 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.cboTargetHand = new System.Windows.Forms.ComboBox();
			this.label29 = new System.Windows.Forms.Label();
			this.cboTargetStage = new System.Windows.Forms.ComboBox();
			this.tabOther = new System.Windows.Forms.TabPage();
			this.label36 = new System.Windows.Forms.Label();
			this.cboAlsoPlaying = new System.Windows.Forms.ComboBox();
			this.label27 = new System.Windows.Forms.Label();
			this.cboAlsoPlayingStage = new System.Windows.Forms.ComboBox();
			this.cboAlsoPlayingHand = new System.Windows.Forms.ComboBox();
			this.label30 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.tabConditions = new System.Windows.Forms.TabPage();
			this.gridFilters = new System.Windows.Forms.DataGridView();
			this.ColTagFilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColTagCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.label37 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.cboTotalMales = new System.Windows.Forms.ComboBox();
			this.cboTotalFemales = new System.Windows.Forms.ComboBox();
			this.cboOwnHand = new System.Windows.Forms.ComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.tabEndings = new System.Windows.Forms.TabPage();
			this.label4 = new System.Windows.Forms.Label();
			this.lblLinesOfDialogue = new System.Windows.Forms.Label();
			this.splitMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.separateThisStageIntoANewCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitAtPoint = new System.Windows.Forms.ToolStripMenuItem();
			this.splitAll = new System.Windows.Forms.ToolStripMenuItem();
			this.duplicateThisCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkReplaceToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label38 = new System.Windows.Forms.Label();
			this.cboAlsoPlayingMaxStage = new System.Windows.Forms.ComboBox();
			this.imageImporter = new SPNATI_Character_Editor.Controls.ImageManager();
			this.gridDialogue = new SPNATI_Character_Editor.KeyboardDataGridView();
			this.ColImage = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColSilent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.epilogueEditor = new SPNATI_Character_Editor.Controls.EpilogueEditor();
			this.cboTargetToStage = new System.Windows.Forms.ComboBox();
			this.label41 = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabMetadata.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridAI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTags)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valRounds)).BeginInit();
			this.tabWardrobe.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabImages.SuspendLayout();
			this.tabDialogue.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitDialogue)).BeginInit();
			this.splitDialogue.Panel1.SuspendLayout();
			this.splitDialogue.Panel2.SuspendLayout();
			this.splitDialogue.SuspendLayout();
			this.grpCase.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.grpConditions.SuspendLayout();
			this.tabControlConditions.SuspendLayout();
			this.tabTarget.SuspendLayout();
			this.tabOther.SuspendLayout();
			this.tabConditions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridFilters)).BeginInit();
			this.tabMisc.SuspendLayout();
			this.tabEndings.SuspendLayout();
			this.splitMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridDialogue)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.validatorToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1217, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.importtxtToolStripMenuItem,
            this.exporttxtFileForPythonToolStripMenuItem,
            this.toolStripSeparator2,
            this.setupToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.newToolStripMenuItem.Text = "New...";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.openToolStripMenuItem.Text = "Open...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(272, 6);
			// 
			// importtxtToolStripMenuItem
			// 
			this.importtxtToolStripMenuItem.Name = "importtxtToolStripMenuItem";
			this.importtxtToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.importtxtToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.importtxtToolStripMenuItem.Text = "Import .txt...";
			this.importtxtToolStripMenuItem.Click += new System.EventHandler(this.importtxtToolStripMenuItem_Click);
			// 
			// exporttxtFileForPythonToolStripMenuItem
			// 
			this.exporttxtFileForPythonToolStripMenuItem.Name = "exporttxtFileForPythonToolStripMenuItem";
			this.exporttxtFileForPythonToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.exporttxtFileForPythonToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.exporttxtFileForPythonToolStripMenuItem.Text = "Export .txt file for make_xml.py";
			this.exporttxtFileForPythonToolStripMenuItem.Click += new System.EventHandler(this.exporttxtFileForPythonToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(272, 6);
			// 
			// setupToolStripMenuItem
			// 
			this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
			this.setupToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.setupToolStripMenuItem.Text = "Setup...";
			this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(272, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.findToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.findToolStripMenuItem.Text = "Find";
			this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
			// 
			// replaceToolStripMenuItem
			// 
			this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
			this.replaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.replaceToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.replaceToolStripMenuItem.Text = "Replace";
			this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
			// 
			// validatorToolStripMenuItem
			// 
			this.validatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentCharacterToolStripMenuItem,
            this.allCharactersToolStripMenuItem});
			this.validatorToolStripMenuItem.Name = "validatorToolStripMenuItem";
			this.validatorToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.validatorToolStripMenuItem.Text = "Validate";
			// 
			// currentCharacterToolStripMenuItem
			// 
			this.currentCharacterToolStripMenuItem.Name = "currentCharacterToolStripMenuItem";
			this.currentCharacterToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.currentCharacterToolStripMenuItem.Text = "Current Character";
			this.currentCharacterToolStripMenuItem.Click += new System.EventHandler(this.currentCharacterToolStripMenuItem_Click);
			// 
			// allCharactersToolStripMenuItem
			// 
			this.allCharactersToolStripMenuItem.Name = "allCharactersToolStripMenuItem";
			this.allCharactersToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.allCharactersToolStripMenuItem.Text = "All Characters";
			this.allCharactersToolStripMenuItem.Click += new System.EventHandler(this.allCharactersToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dialogueTesterToolStripMenuItem,
            this.graphsToolStripItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.viewToolStripMenuItem.Text = "Tools";
			// 
			// dialogueTesterToolStripMenuItem
			// 
			this.dialogueTesterToolStripMenuItem.Name = "dialogueTesterToolStripMenuItem";
			this.dialogueTesterToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.dialogueTesterToolStripMenuItem.Text = "Dialogue Tester...";
			this.dialogueTesterToolStripMenuItem.Click += new System.EventHandler(this.dialogueTesterToolStripMenuItem_Click);
			// 
			// graphsToolStripItem
			// 
			this.graphsToolStripItem.Name = "graphsToolStripItem";
			this.graphsToolStripItem.Size = new System.Drawing.Size(164, 22);
			this.graphsToolStripItem.Text = "Charts...";
			this.graphsToolStripItem.Click += new System.EventHandler(this.graphsToolStripItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToGuideToolStripMenuItem,
            this.aboutCharacterEditorToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// howToGuideToolStripMenuItem
			// 
			this.howToGuideToolStripMenuItem.Name = "howToGuideToolStripMenuItem";
			this.howToGuideToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.howToGuideToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.howToGuideToolStripMenuItem.Text = "View Help";
			this.howToGuideToolStripMenuItem.Click += new System.EventHandler(this.howToGuideToolStripMenuItem_Click);
			// 
			// aboutCharacterEditorToolStripMenuItem
			// 
			this.aboutCharacterEditorToolStripMenuItem.Name = "aboutCharacterEditorToolStripMenuItem";
			this.aboutCharacterEditorToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.aboutCharacterEditorToolStripMenuItem.Text = "About Character Editor...";
			this.aboutCharacterEditorToolStripMenuItem.Click += new System.EventHandler(this.aboutCharacterEditorToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 748);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1217, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Label:";
			// 
			// txtLabel
			// 
			this.txtLabel.Location = new System.Drawing.Point(55, 25);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(100, 20);
			this.txtLabel.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "First name:";
			// 
			// txtFirstName
			// 
			this.txtFirstName.Location = new System.Drawing.Point(75, 8);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(100, 20);
			this.txtFirstName.TabIndex = 4;
			// 
			// txtLastName
			// 
			this.txtLastName.Location = new System.Drawing.Point(246, 8);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(100, 20);
			this.txtLastName.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(181, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Last name:";
			// 
			// picPortrait
			// 
			this.picPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.picPortrait.Location = new System.Drawing.Point(963, 177);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(242, 564);
			this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPortrait.TabIndex = 0;
			this.picPortrait.TabStop = false;
			// 
			// treeDialogue
			// 
			this.treeDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeDialogue.HideSelection = false;
			this.treeDialogue.Location = new System.Drawing.Point(3, 32);
			this.treeDialogue.Name = "treeDialogue";
			this.treeDialogue.Size = new System.Drawing.Size(248, 538);
			this.treeDialogue.TabIndex = 1;
			this.treeDialogue.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDialogue_AfterSelect);
			this.treeDialogue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeDialogue_KeyDown);
			// 
			// cmdAddStage
			// 
			this.cmdAddStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdAddStage.Location = new System.Drawing.Point(209, 894);
			this.cmdAddStage.Name = "cmdAddStage";
			this.cmdAddStage.Size = new System.Drawing.Size(75, 23);
			this.cmdAddStage.TabIndex = 2;
			this.cmdAddStage.Text = "Add Stage";
			this.cmdAddStage.UseVisualStyleBackColor = true;
			// 
			// cmdAddLine
			// 
			this.cmdAddLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdAddLine.Location = new System.Drawing.Point(290, 894);
			this.cmdAddLine.Name = "cmdAddLine";
			this.cmdAddLine.Size = new System.Drawing.Size(75, 23);
			this.cmdAddLine.TabIndex = 3;
			this.cmdAddLine.Text = "Add Line";
			this.cmdAddLine.UseVisualStyleBackColor = true;
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdDelete.Location = new System.Drawing.Point(371, 923);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(75, 23);
			this.cmdDelete.TabIndex = 6;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Player:";
			// 
			// cboLineTarget
			// 
			this.cboLineTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboLineTarget.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboLineTarget.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboLineTarget.FormattingEnabled = true;
			this.cboLineTarget.Location = new System.Drawing.Point(81, 19);
			this.cboLineTarget.Name = "cboLineTarget";
			this.cboLineTarget.Size = new System.Drawing.Size(544, 21);
			this.cboLineTarget.TabIndex = 0;
			this.cboLineTarget.SelectedIndexChanged += new System.EventHandler(this.cboLineTarget_SelectedIndexChanged);
			// 
			// cmdCopy
			// 
			this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCopy.Location = new System.Drawing.Point(209, 923);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new System.Drawing.Size(75, 23);
			this.cmdCopy.TabIndex = 4;
			this.cmdCopy.Text = "Copy";
			this.cmdCopy.UseVisualStyleBackColor = true;
			// 
			// cmdInsert
			// 
			this.cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdInsert.Location = new System.Drawing.Point(290, 923);
			this.cmdInsert.Name = "cmdInsert";
			this.cmdInsert.Size = new System.Drawing.Size(75, 23);
			this.cmdInsert.TabIndex = 5;
			this.cmdInsert.Text = "Insert";
			this.cmdInsert.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 103);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Tag:";
			// 
			// cboLineFilter
			// 
			this.cboLineFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboLineFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboLineFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboLineFilter.FormattingEnabled = true;
			this.cboLineFilter.Location = new System.Drawing.Point(81, 100);
			this.cboLineFilter.Name = "cboLineFilter";
			this.cboLineFilter.Size = new System.Drawing.Size(544, 21);
			this.cboLineFilter.TabIndex = 3;
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabMetadata);
			this.tabControl.Controls.Add(this.tabWardrobe);
			this.tabControl.Controls.Add(this.tabImages);
			this.tabControl.Controls.Add(this.tabDialogue);
			this.tabControl.Controls.Add(this.tabEndings);
			this.tabControl.Location = new System.Drawing.Point(12, 51);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(945, 694);
			this.tabControl.TabIndex = 9;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
			// 
			// tabMetadata
			// 
			this.tabMetadata.Controls.Add(this.gridAI);
			this.tabMetadata.Controls.Add(this.cmdAddToListing);
			this.tabMetadata.Controls.Add(this.label7);
			this.tabMetadata.Controls.Add(this.label22);
			this.tabMetadata.Controls.Add(this.txtDescription);
			this.tabMetadata.Controls.Add(this.cboDefaultPic);
			this.tabMetadata.Controls.Add(this.label24);
			this.tabMetadata.Controls.Add(this.txtHeight);
			this.tabMetadata.Controls.Add(this.txtLastName);
			this.tabMetadata.Controls.Add(this.label23);
			this.tabMetadata.Controls.Add(this.label3);
			this.tabMetadata.Controls.Add(this.label20);
			this.tabMetadata.Controls.Add(this.txtFirstName);
			this.tabMetadata.Controls.Add(this.label2);
			this.tabMetadata.Controls.Add(this.gridTags);
			this.tabMetadata.Controls.Add(this.label19);
			this.tabMetadata.Controls.Add(this.txtArtist);
			this.tabMetadata.Controls.Add(this.label18);
			this.tabMetadata.Controls.Add(this.txtWriter);
			this.tabMetadata.Controls.Add(this.label17);
			this.tabMetadata.Controls.Add(this.txtSource);
			this.tabMetadata.Controls.Add(this.valRounds);
			this.tabMetadata.Controls.Add(this.label12);
			this.tabMetadata.Controls.Add(this.cboSize);
			this.tabMetadata.Controls.Add(this.label10);
			this.tabMetadata.Controls.Add(this.label11);
			this.tabMetadata.Controls.Add(this.cboGender);
			this.tabMetadata.Location = new System.Drawing.Point(4, 22);
			this.tabMetadata.Name = "tabMetadata";
			this.tabMetadata.Padding = new System.Windows.Forms.Padding(3);
			this.tabMetadata.Size = new System.Drawing.Size(937, 668);
			this.tabMetadata.TabIndex = 0;
			this.tabMetadata.Text = "Metadata";
			this.tabMetadata.UseVisualStyleBackColor = true;
			// 
			// gridAI
			// 
			this.gridAI.AllowUserToResizeColumns = false;
			this.gridAI.AllowUserToResizeRows = false;
			this.gridAI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColAIStage,
            this.ColDifficulty});
			this.gridAI.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridAI.Location = new System.Drawing.Point(366, 140);
			this.gridAI.Name = "gridAI";
			this.gridAI.RowHeadersVisible = false;
			this.gridAI.Size = new System.Drawing.Size(212, 101);
			this.gridAI.TabIndex = 81;
			// 
			// ColAIStage
			// 
			this.ColAIStage.HeaderText = "Stage";
			this.ColAIStage.MinimumWidth = 50;
			this.ColAIStage.Name = "ColAIStage";
			this.ColAIStage.Width = 50;
			// 
			// ColDifficulty
			// 
			this.ColDifficulty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColDifficulty.HeaderText = "Intelligence";
			this.ColDifficulty.Items.AddRange(new object[] {
            "",
            "bad",
            "average",
            "good"});
			this.ColDifficulty.Name = "ColDifficulty";
			this.ColDifficulty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColDifficulty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// cmdAddToListing
			// 
			this.cmdAddToListing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAddToListing.Location = new System.Drawing.Point(830, 6);
			this.cmdAddToListing.Name = "cmdAddToListing";
			this.cmdAddToListing.Size = new System.Drawing.Size(101, 23);
			this.cmdAddToListing.TabIndex = 80;
			this.cmdAddToListing.Text = "Add to Listing";
			this.cmdAddToListing.UseVisualStyleBackColor = true;
			this.cmdAddToListing.Click += new System.EventHandler(this.cmdAddToListing_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(296, 144);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 28;
			this.label7.Text = "Intelligence:";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(6, 64);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(43, 13);
			this.label22.TabIndex = 12;
			this.label22.Text = "Portrait:";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(75, 247);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(503, 90);
			this.txtDescription.TabIndex = 16;
			// 
			// cboDefaultPic
			// 
			this.cboDefaultPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDefaultPic.FormattingEnabled = true;
			this.cboDefaultPic.Location = new System.Drawing.Point(75, 61);
			this.cboDefaultPic.Name = "cboDefaultPic";
			this.cboDefaultPic.Size = new System.Drawing.Size(156, 21);
			this.cboDefaultPic.TabIndex = 10;
			this.cboDefaultPic.SelectedIndexChanged += new System.EventHandler(this.cboDefaultPic_SelectedIndexChanged);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(6, 250);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(63, 13);
			this.label24.TabIndex = 26;
			this.label24.Text = "Description:";
			// 
			// txtHeight
			// 
			this.txtHeight.Location = new System.Drawing.Point(366, 114);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(62, 20);
			this.txtHeight.TabIndex = 14;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(296, 117);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(41, 13);
			this.label23.TabIndex = 24;
			this.label23.Text = "Height:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(6, 144);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(34, 13);
			this.label20.TabIndex = 23;
			this.label20.Text = "Tags:";
			// 
			// gridTags
			// 
			this.gridTags.AllowUserToResizeColumns = false;
			this.gridTags.AllowUserToResizeRows = false;
			this.gridTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridTags.ColumnHeadersVisible = false;
			this.gridTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTag});
			this.gridTags.Location = new System.Drawing.Point(75, 140);
			this.gridTags.Name = "gridTags";
			this.gridTags.RowHeadersVisible = false;
			this.gridTags.Size = new System.Drawing.Size(212, 101);
			this.gridTags.TabIndex = 15;
			// 
			// ColTag
			// 
			this.ColTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTag.HeaderText = "Tag";
			this.ColTag.Name = "ColTag";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(296, 91);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(33, 13);
			this.label19.TabIndex = 21;
			this.label19.Text = "Artist:";
			// 
			// txtArtist
			// 
			this.txtArtist.Location = new System.Drawing.Point(366, 88);
			this.txtArtist.Name = "txtArtist";
			this.txtArtist.Size = new System.Drawing.Size(212, 20);
			this.txtArtist.TabIndex = 12;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(6, 91);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(38, 13);
			this.label18.TabIndex = 19;
			this.label18.Text = "Writer:";
			// 
			// txtWriter
			// 
			this.txtWriter.Location = new System.Drawing.Point(75, 88);
			this.txtWriter.Name = "txtWriter";
			this.txtWriter.Size = new System.Drawing.Size(212, 20);
			this.txtWriter.TabIndex = 11;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(6, 117);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(37, 13);
			this.label17.TabIndex = 17;
			this.label17.Text = "Origin:";
			// 
			// txtSource
			// 
			this.txtSource.Location = new System.Drawing.Point(75, 114);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(212, 20);
			this.txtSource.TabIndex = 13;
			// 
			// valRounds
			// 
			this.valRounds.Location = new System.Drawing.Point(455, 35);
			this.valRounds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valRounds.Name = "valRounds";
			this.valRounds.Size = new System.Drawing.Size(59, 20);
			this.valRounds.TabIndex = 9;
			this.valRounds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(363, 37);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(86, 13);
			this.label12.TabIndex = 14;
			this.label12.Text = "Rounds to finish:";
			// 
			// cboSize
			// 
			this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSize.FormattingEnabled = true;
			this.cboSize.Items.AddRange(new object[] {
            "small",
            "medium",
            "large"});
			this.cboSize.Location = new System.Drawing.Point(246, 34);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(100, 21);
			this.cboSize.TabIndex = 7;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 37);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(45, 13);
			this.label10.TabIndex = 10;
			this.label10.Text = "Gender:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(181, 37);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(30, 13);
			this.label11.TabIndex = 12;
			this.label11.Text = "Size:";
			// 
			// cboGender
			// 
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FormattingEnabled = true;
			this.cboGender.Items.AddRange(new object[] {
            "female",
            "male"});
			this.cboGender.Location = new System.Drawing.Point(75, 34);
			this.cboGender.Name = "cboGender";
			this.cboGender.Size = new System.Drawing.Size(100, 21);
			this.cboGender.TabIndex = 6;
			// 
			// tabWardrobe
			// 
			this.tabWardrobe.Controls.Add(this.label9);
			this.tabWardrobe.Controls.Add(this.groupBox1);
			this.tabWardrobe.Controls.Add(this.cmdClothesDown);
			this.tabWardrobe.Controls.Add(this.cmdClothesUp);
			this.tabWardrobe.Controls.Add(this.cmdRemoveClothes);
			this.tabWardrobe.Controls.Add(this.cmdAddClothes);
			this.tabWardrobe.Controls.Add(this.lstClothes);
			this.tabWardrobe.Location = new System.Drawing.Point(4, 22);
			this.tabWardrobe.Name = "tabWardrobe";
			this.tabWardrobe.Padding = new System.Windows.Forms.Padding(3);
			this.tabWardrobe.Size = new System.Drawing.Size(937, 668);
			this.tabWardrobe.TabIndex = 2;
			this.tabWardrobe.Text = "Wardrobe";
			this.tabWardrobe.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(209, 7);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(267, 13);
			this.label9.TabIndex = 14;
			this.label9.Text = "Clothing is ordered from first layer to remove to last layer";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblPositionHelp);
			this.groupBox1.Controls.Add(this.lblTypeHelp);
			this.groupBox1.Controls.Add(this.txtClothesProperName);
			this.groupBox1.Controls.Add(this.cboClothesPosition);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.cboClothesType);
			this.groupBox1.Controls.Add(this.txtClothesLowerCase);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Location = new System.Drawing.Point(209, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(722, 127);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Edit Clothing";
			// 
			// lblPositionHelp
			// 
			this.lblPositionHelp.AutoSize = true;
			this.lblPositionHelp.Location = new System.Drawing.Point(217, 100);
			this.lblPositionHelp.Name = "lblPositionHelp";
			this.lblPositionHelp.Size = new System.Drawing.Size(16, 13);
			this.lblPositionHelp.TabIndex = 14;
			this.lblPositionHelp.Text = "   ";
			// 
			// lblTypeHelp
			// 
			this.lblTypeHelp.AutoSize = true;
			this.lblTypeHelp.Location = new System.Drawing.Point(217, 73);
			this.lblTypeHelp.Name = "lblTypeHelp";
			this.lblTypeHelp.Size = new System.Drawing.Size(16, 13);
			this.lblTypeHelp.TabIndex = 13;
			this.lblTypeHelp.Text = "   ";
			// 
			// txtClothesProperName
			// 
			this.txtClothesProperName.Location = new System.Drawing.Point(84, 19);
			this.txtClothesProperName.Name = "txtClothesProperName";
			this.txtClothesProperName.Size = new System.Drawing.Size(127, 20);
			this.txtClothesProperName.TabIndex = 6;
			this.txtClothesProperName.TextChanged += new System.EventHandler(this.txtClothesProperName_TextChanged);
			// 
			// cboClothesPosition
			// 
			this.cboClothesPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboClothesPosition.FormattingEnabled = true;
			this.cboClothesPosition.Items.AddRange(new object[] {
            "upper",
            "lower",
            "other"});
			this.cboClothesPosition.Location = new System.Drawing.Point(84, 97);
			this.cboClothesPosition.Name = "cboClothesPosition";
			this.cboClothesPosition.Size = new System.Drawing.Size(127, 21);
			this.cboClothesPosition.TabIndex = 12;
			this.cboClothesPosition.SelectedIndexChanged += new System.EventHandler(this.cboClothesPosition_SelectedIndexChanged);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(8, 22);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(70, 13);
			this.label13.TabIndex = 5;
			this.label13.Text = "Proper name:";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(8, 100);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(47, 13);
			this.label16.TabIndex = 11;
			this.label16.Text = "Position:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(8, 48);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(62, 13);
			this.label14.TabIndex = 7;
			this.label14.Text = "Lowercase:";
			// 
			// cboClothesType
			// 
			this.cboClothesType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboClothesType.FormattingEnabled = true;
			this.cboClothesType.Items.AddRange(new object[] {
            "important",
            "major",
            "minor",
            "extra"});
			this.cboClothesType.Location = new System.Drawing.Point(84, 70);
			this.cboClothesType.Name = "cboClothesType";
			this.cboClothesType.Size = new System.Drawing.Size(127, 21);
			this.cboClothesType.TabIndex = 10;
			this.cboClothesType.SelectedIndexChanged += new System.EventHandler(this.cboClothesType_SelectedIndexChanged);
			// 
			// txtClothesLowerCase
			// 
			this.txtClothesLowerCase.Location = new System.Drawing.Point(84, 45);
			this.txtClothesLowerCase.Name = "txtClothesLowerCase";
			this.txtClothesLowerCase.Size = new System.Drawing.Size(127, 20);
			this.txtClothesLowerCase.TabIndex = 8;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(8, 73);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(34, 13);
			this.label15.TabIndex = 9;
			this.label15.Text = "Type:";
			// 
			// cmdClothesDown
			// 
			this.cmdClothesDown.Location = new System.Drawing.Point(168, 46);
			this.cmdClothesDown.Name = "cmdClothesDown";
			this.cmdClothesDown.Size = new System.Drawing.Size(35, 33);
			this.cmdClothesDown.TabIndex = 4;
			this.cmdClothesDown.Text = "▼";
			this.cmdClothesDown.UseVisualStyleBackColor = true;
			this.cmdClothesDown.Click += new System.EventHandler(this.cmdClothesDown_Click);
			// 
			// cmdClothesUp
			// 
			this.cmdClothesUp.Location = new System.Drawing.Point(168, 7);
			this.cmdClothesUp.Name = "cmdClothesUp";
			this.cmdClothesUp.Size = new System.Drawing.Size(35, 33);
			this.cmdClothesUp.TabIndex = 3;
			this.cmdClothesUp.Text = "▲";
			this.cmdClothesUp.UseVisualStyleBackColor = true;
			this.cmdClothesUp.Click += new System.EventHandler(this.cmdClothesUp_Click);
			// 
			// cmdRemoveClothes
			// 
			this.cmdRemoveClothes.Location = new System.Drawing.Point(87, 213);
			this.cmdRemoveClothes.Name = "cmdRemoveClothes";
			this.cmdRemoveClothes.Size = new System.Drawing.Size(74, 23);
			this.cmdRemoveClothes.TabIndex = 2;
			this.cmdRemoveClothes.Text = "Remove";
			this.cmdRemoveClothes.UseVisualStyleBackColor = true;
			this.cmdRemoveClothes.Click += new System.EventHandler(this.cmdRemoveClothes_Click);
			// 
			// cmdAddClothes
			// 
			this.cmdAddClothes.Location = new System.Drawing.Point(7, 213);
			this.cmdAddClothes.Name = "cmdAddClothes";
			this.cmdAddClothes.Size = new System.Drawing.Size(74, 23);
			this.cmdAddClothes.TabIndex = 1;
			this.cmdAddClothes.Text = "Add";
			this.cmdAddClothes.UseVisualStyleBackColor = true;
			this.cmdAddClothes.Click += new System.EventHandler(this.cmdAddClothes_Click);
			// 
			// lstClothes
			// 
			this.lstClothes.FormattingEnabled = true;
			this.lstClothes.Location = new System.Drawing.Point(7, 7);
			this.lstClothes.Name = "lstClothes";
			this.lstClothes.Size = new System.Drawing.Size(154, 199);
			this.lstClothes.TabIndex = 0;
			this.lstClothes.SelectedIndexChanged += new System.EventHandler(this.lstClothes_SelectedIndexChanged);
			// 
			// tabImages
			// 
			this.tabImages.Controls.Add(this.imageImporter);
			this.tabImages.Location = new System.Drawing.Point(4, 22);
			this.tabImages.Name = "tabImages";
			this.tabImages.Padding = new System.Windows.Forms.Padding(3);
			this.tabImages.Size = new System.Drawing.Size(937, 668);
			this.tabImages.TabIndex = 3;
			this.tabImages.Text = "Images";
			this.tabImages.UseVisualStyleBackColor = true;
			// 
			// tabDialogue
			// 
			this.tabDialogue.Controls.Add(this.splitDialogue);
			this.tabDialogue.Controls.Add(this.cmdAddStage);
			this.tabDialogue.Controls.Add(this.cmdAddLine);
			this.tabDialogue.Controls.Add(this.cmdDelete);
			this.tabDialogue.Controls.Add(this.cmdInsert);
			this.tabDialogue.Controls.Add(this.cmdCopy);
			this.tabDialogue.Location = new System.Drawing.Point(4, 22);
			this.tabDialogue.Name = "tabDialogue";
			this.tabDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tabDialogue.Size = new System.Drawing.Size(937, 668);
			this.tabDialogue.TabIndex = 1;
			this.tabDialogue.Text = "Dialogue";
			this.tabDialogue.UseVisualStyleBackColor = true;
			// 
			// splitDialogue
			// 
			this.splitDialogue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitDialogue.Location = new System.Drawing.Point(3, 3);
			this.splitDialogue.Name = "splitDialogue";
			// 
			// splitDialogue.Panel1
			// 
			this.splitDialogue.Panel1.Controls.Add(this.cboTreeTarget);
			this.splitDialogue.Panel1.Controls.Add(this.cmdAddDialogue);
			this.splitDialogue.Panel1.Controls.Add(this.label40);
			this.splitDialogue.Panel1.Controls.Add(this.treeDialogue);
			this.splitDialogue.Panel1.Controls.Add(this.cboTreeFilter);
			this.splitDialogue.Panel1.Controls.Add(this.cmdRemoveDialogue);
			this.splitDialogue.Panel1.Controls.Add(this.label39);
			this.splitDialogue.Panel1.Controls.Add(this.label33);
			this.splitDialogue.Panel1.Controls.Add(this.cmdSplit);
			this.splitDialogue.Panel1.Controls.Add(this.label35);
			// 
			// splitDialogue.Panel2
			// 
			this.splitDialogue.Panel2.Controls.Add(this.grpCase);
			this.splitDialogue.Size = new System.Drawing.Size(931, 662);
			this.splitDialogue.SplitterDistance = 255;
			this.splitDialogue.TabIndex = 15;
			// 
			// cboTreeTarget
			// 
			this.cboTreeTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTreeTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTreeTarget.FormattingEnabled = true;
			this.cboTreeTarget.Location = new System.Drawing.Point(55, 603);
			this.cboTreeTarget.Name = "cboTreeTarget";
			this.cboTreeTarget.Size = new System.Drawing.Size(196, 21);
			this.cboTreeTarget.TabIndex = 39;
			this.cboTreeTarget.SelectedIndexChanged += new System.EventHandler(this.cboTreeTarget_SelectedIndexChanged);
			// 
			// cmdAddDialogue
			// 
			this.cmdAddDialogue.Location = new System.Drawing.Point(3, 3);
			this.cmdAddDialogue.Name = "cmdAddDialogue";
			this.cmdAddDialogue.Size = new System.Drawing.Size(54, 23);
			this.cmdAddDialogue.TabIndex = 29;
			this.cmdAddDialogue.Text = "Add";
			this.cmdAddDialogue.UseVisualStyleBackColor = true;
			this.cmdAddDialogue.Click += new System.EventHandler(this.cmdAddDialogue_Click);
			// 
			// label40
			// 
			this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label40.AutoSize = true;
			this.label40.Location = new System.Drawing.Point(3, 606);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(41, 13);
			this.label40.TabIndex = 38;
			this.label40.Text = "Target:";
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
			this.cboTreeFilter.Location = new System.Drawing.Point(55, 576);
			this.cboTreeFilter.Name = "cboTreeFilter";
			this.cboTreeFilter.Size = new System.Drawing.Size(196, 21);
			this.cboTreeFilter.TabIndex = 37;
			this.cboTreeFilter.SelectedIndexChanged += new System.EventHandler(this.cboTreeFilter_SelectedIndexChanged);
			// 
			// cmdRemoveDialogue
			// 
			this.cmdRemoveDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRemoveDialogue.Location = new System.Drawing.Point(193, 3);
			this.cmdRemoveDialogue.Name = "cmdRemoveDialogue";
			this.cmdRemoveDialogue.Size = new System.Drawing.Size(58, 23);
			this.cmdRemoveDialogue.TabIndex = 30;
			this.cmdRemoveDialogue.Text = "Remove";
			this.cmdRemoveDialogue.UseVisualStyleBackColor = true;
			this.cmdRemoveDialogue.Click += new System.EventHandler(this.cmdRemoveDialogue_Click);
			// 
			// label39
			// 
			this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label39.AutoSize = true;
			this.label39.Location = new System.Drawing.Point(3, 579);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(32, 13);
			this.label39.TabIndex = 36;
			this.label39.Text = "Filter:";
			// 
			// label33
			// 
			this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label33.AutoSize = true;
			this.label33.ForeColor = System.Drawing.Color.Blue;
			this.label33.Location = new System.Drawing.Point(6, 640);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(134, 13);
			this.label33.TabIndex = 31;
			this.label33.Text = "Blue: Contains default lines";
			// 
			// cmdSplit
			// 
			this.cmdSplit.Location = new System.Drawing.Point(63, 3);
			this.cmdSplit.Name = "cmdSplit";
			this.cmdSplit.Size = new System.Drawing.Size(84, 23);
			this.cmdSplit.TabIndex = 35;
			this.cmdSplit.Text = "Copy Tools...";
			this.cmdSplit.UseVisualStyleBackColor = true;
			this.cmdSplit.Click += new System.EventHandler(this.cmdSplit_Click);
			// 
			// label35
			// 
			this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label35.AutoSize = true;
			this.label35.ForeColor = System.Drawing.Color.Green;
			this.label35.Location = new System.Drawing.Point(6, 627);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(128, 13);
			this.label35.TabIndex = 34;
			this.label35.Text = "Green: Targeted dialogue";
			// 
			// grpCase
			// 
			this.grpCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpCase.Controls.Add(this.lblHelpText);
			this.grpCase.Controls.Add(this.cmdPasteAll);
			this.grpCase.Controls.Add(this.cmdCopyAll);
			this.grpCase.Controls.Add(this.cboCaseTags);
			this.grpCase.Controls.Add(this.label34);
			this.grpCase.Controls.Add(this.label8);
			this.grpCase.Controls.Add(this.lblAvailableVars);
			this.grpCase.Controls.Add(this.groupBox3);
			this.grpCase.Controls.Add(this.gridDialogue);
			this.grpCase.Controls.Add(this.grpConditions);
			this.grpCase.Location = new System.Drawing.Point(3, 0);
			this.grpCase.Name = "grpCase";
			this.grpCase.Size = new System.Drawing.Size(666, 659);
			this.grpCase.TabIndex = 28;
			this.grpCase.TabStop = false;
			this.grpCase.Text = "Edit Case";
			// 
			// lblHelpText
			// 
			this.lblHelpText.AutoSize = true;
			this.lblHelpText.Location = new System.Drawing.Point(222, 22);
			this.lblHelpText.Name = "lblHelpText";
			this.lblHelpText.Size = new System.Drawing.Size(53, 13);
			this.lblHelpText.TabIndex = 38;
			this.lblHelpText.Text = "Help Text";
			// 
			// cmdPasteAll
			// 
			this.cmdPasteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPasteAll.Location = new System.Drawing.Point(585, 385);
			this.cmdPasteAll.Name = "cmdPasteAll";
			this.cmdPasteAll.Size = new System.Drawing.Size(75, 23);
			this.cmdPasteAll.TabIndex = 40;
			this.cmdPasteAll.Text = "Paste All";
			this.cmdPasteAll.UseVisualStyleBackColor = true;
			this.cmdPasteAll.Click += new System.EventHandler(this.cmdPasteAll_Click);
			// 
			// cmdCopyAll
			// 
			this.cmdCopyAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCopyAll.Location = new System.Drawing.Point(504, 385);
			this.cmdCopyAll.Name = "cmdCopyAll";
			this.cmdCopyAll.Size = new System.Drawing.Size(75, 23);
			this.cmdCopyAll.TabIndex = 39;
			this.cmdCopyAll.Text = "Copy All";
			this.cmdCopyAll.UseVisualStyleBackColor = true;
			this.cmdCopyAll.Click += new System.EventHandler(this.cmdCopyAll_Click);
			// 
			// cboCaseTags
			// 
			this.cboCaseTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCaseTags.FormattingEnabled = true;
			this.cboCaseTags.Location = new System.Drawing.Point(45, 19);
			this.cboCaseTags.Name = "cboCaseTags";
			this.cboCaseTags.Size = new System.Drawing.Size(175, 21);
			this.cboCaseTags.TabIndex = 35;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(6, 22);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(34, 13);
			this.label34.TabIndex = 34;
			this.label34.Text = "Type:";
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 385);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(52, 13);
			this.label8.TabIndex = 33;
			this.label8.Text = "Dialogue:";
			// 
			// lblAvailableVars
			// 
			this.lblAvailableVars.AutoSize = true;
			this.lblAvailableVars.Location = new System.Drawing.Point(6, 398);
			this.lblAvailableVars.Name = "lblAvailableVars";
			this.lblAvailableVars.Size = new System.Drawing.Size(53, 13);
			this.lblAvailableVars.TabIndex = 32;
			this.lblAvailableVars.Text = "Variables:";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.chkSelectAll);
			this.groupBox3.Controls.Add(this.flowStageChecks);
			this.groupBox3.Location = new System.Drawing.Point(9, 46);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(651, 132);
			this.groupBox3.TabIndex = 37;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Applies to Stages";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.BackColor = System.Drawing.Color.White;
			this.chkSelectAll.Location = new System.Drawing.Point(578, -1);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
			this.chkSelectAll.TabIndex = 36;
			this.chkSelectAll.Text = "Select All";
			this.chkSelectAll.UseVisualStyleBackColor = false;
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// flowStageChecks
			// 
			this.flowStageChecks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowStageChecks.Location = new System.Drawing.Point(3, 16);
			this.flowStageChecks.Name = "flowStageChecks";
			this.flowStageChecks.Size = new System.Drawing.Size(645, 113);
			this.flowStageChecks.TabIndex = 0;
			// 
			// grpConditions
			// 
			this.grpConditions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpConditions.Controls.Add(this.tabControlConditions);
			this.grpConditions.Location = new System.Drawing.Point(9, 184);
			this.grpConditions.Name = "grpConditions";
			this.grpConditions.Size = new System.Drawing.Size(651, 198);
			this.grpConditions.TabIndex = 38;
			this.grpConditions.TabStop = false;
			this.grpConditions.Text = "Conditions";
			// 
			// tabControlConditions
			// 
			this.tabControlConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlConditions.Controls.Add(this.tabTarget);
			this.tabControlConditions.Controls.Add(this.tabOther);
			this.tabControlConditions.Controls.Add(this.tabConditions);
			this.tabControlConditions.Controls.Add(this.tabMisc);
			this.tabControlConditions.Location = new System.Drawing.Point(6, 19);
			this.tabControlConditions.Name = "tabControlConditions";
			this.tabControlConditions.SelectedIndex = 0;
			this.tabControlConditions.Size = new System.Drawing.Size(639, 173);
			this.tabControlConditions.TabIndex = 30;
			// 
			// tabTarget
			// 
			this.tabTarget.Controls.Add(this.cboTargetToStage);
			this.tabTarget.Controls.Add(this.label41);
			this.tabTarget.Controls.Add(this.label21);
			this.tabTarget.Controls.Add(this.label6);
			this.tabTarget.Controls.Add(this.label5);
			this.tabTarget.Controls.Add(this.label25);
			this.tabTarget.Controls.Add(this.cboTargetHand);
			this.tabTarget.Controls.Add(this.cboLineTarget);
			this.tabTarget.Controls.Add(this.label29);
			this.tabTarget.Controls.Add(this.cboTargetStage);
			this.tabTarget.Controls.Add(this.cboLineFilter);
			this.tabTarget.Location = new System.Drawing.Point(4, 22);
			this.tabTarget.Name = "tabTarget";
			this.tabTarget.Padding = new System.Windows.Forms.Padding(3);
			this.tabTarget.Size = new System.Drawing.Size(631, 147);
			this.tabTarget.TabIndex = 0;
			this.tabTarget.Text = "Target";
			this.tabTarget.UseVisualStyleBackColor = true;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(6, 3);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(308, 13);
			this.label21.TabIndex = 26;
			this.label21.Text = "Targets the player the action belongs to (ex. the player who lost)";
			// 
			// label25
			// 
			this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(6, 76);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(36, 13);
			this.label25.TabIndex = 16;
			this.label25.Text = "Hand:";
			// 
			// cboTargetHand
			// 
			this.cboTargetHand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTargetHand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTargetHand.FormattingEnabled = true;
			this.cboTargetHand.Items.AddRange(new object[] {
            "",
            "Nothing",
            "High Card",
            "One Pair",
            "Two Pair",
            "Three of a Kind",
            "Straight",
            "Flush",
            "Full House",
            "Four of a Kind",
            "Straight Flush",
            "Royal Flush"});
			this.cboTargetHand.Location = new System.Drawing.Point(81, 73);
			this.cboTargetHand.Name = "cboTargetHand";
			this.cboTargetHand.Size = new System.Drawing.Size(544, 21);
			this.cboTargetHand.TabIndex = 2;
			// 
			// label29
			// 
			this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(6, 49);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(49, 13);
			this.label29.TabIndex = 24;
			this.label29.Text = "At stage:";
			// 
			// cboTargetStage
			// 
			this.cboTargetStage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTargetStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTargetStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboTargetStage.FormattingEnabled = true;
			this.cboTargetStage.Location = new System.Drawing.Point(81, 46);
			this.cboTargetStage.Name = "cboTargetStage";
			this.cboTargetStage.Size = new System.Drawing.Size(242, 21);
			this.cboTargetStage.TabIndex = 1;
			// 
			// tabOther
			// 
			this.tabOther.Controls.Add(this.cboAlsoPlayingMaxStage);
			this.tabOther.Controls.Add(this.label38);
			this.tabOther.Controls.Add(this.label36);
			this.tabOther.Controls.Add(this.cboAlsoPlaying);
			this.tabOther.Controls.Add(this.label27);
			this.tabOther.Controls.Add(this.cboAlsoPlayingStage);
			this.tabOther.Controls.Add(this.cboAlsoPlayingHand);
			this.tabOther.Controls.Add(this.label30);
			this.tabOther.Controls.Add(this.label28);
			this.tabOther.Location = new System.Drawing.Point(4, 22);
			this.tabOther.Name = "tabOther";
			this.tabOther.Padding = new System.Windows.Forms.Padding(3);
			this.tabOther.Size = new System.Drawing.Size(631, 147);
			this.tabOther.TabIndex = 1;
			this.tabOther.Text = "Other Player";
			this.tabOther.UseVisualStyleBackColor = true;
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(6, 3);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(255, 13);
			this.label36.TabIndex = 28;
			this.label36.Text = "Targets a player other than the one taking the action";
			// 
			// cboAlsoPlaying
			// 
			this.cboAlsoPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboAlsoPlaying.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboAlsoPlaying.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboAlsoPlaying.FormattingEnabled = true;
			this.cboAlsoPlaying.Location = new System.Drawing.Point(81, 19);
			this.cboAlsoPlaying.Name = "cboAlsoPlaying";
			this.cboAlsoPlaying.Size = new System.Drawing.Size(544, 21);
			this.cboAlsoPlaying.TabIndex = 4;
			this.cboAlsoPlaying.SelectedIndexChanged += new System.EventHandler(this.cboAlsoPlaying_SelectedIndexChanged);
			// 
			// label27
			// 
			this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(6, 22);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(39, 13);
			this.label27.TabIndex = 20;
			this.label27.Text = "Player:";
			// 
			// cboAlsoPlayingStage
			// 
			this.cboAlsoPlayingStage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboAlsoPlayingStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboAlsoPlayingStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboAlsoPlayingStage.FormattingEnabled = true;
			this.cboAlsoPlayingStage.Location = new System.Drawing.Point(81, 46);
			this.cboAlsoPlayingStage.Name = "cboAlsoPlayingStage";
			this.cboAlsoPlayingStage.Size = new System.Drawing.Size(242, 21);
			this.cboAlsoPlayingStage.TabIndex = 5;
			// 
			// cboAlsoPlayingHand
			// 
			this.cboAlsoPlayingHand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboAlsoPlayingHand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAlsoPlayingHand.FormattingEnabled = true;
			this.cboAlsoPlayingHand.Items.AddRange(new object[] {
            "",
            "Nothing",
            "High Card",
            "One Pair",
            "Two Pair",
            "Three of a Kind",
            "Straight",
            "Flush",
            "Full House",
            "Four of a Kind",
            "Straight Flush",
            "Royal Flush"});
			this.cboAlsoPlayingHand.Location = new System.Drawing.Point(81, 73);
			this.cboAlsoPlayingHand.Name = "cboAlsoPlayingHand";
			this.cboAlsoPlayingHand.Size = new System.Drawing.Size(544, 21);
			this.cboAlsoPlayingHand.TabIndex = 7;
			// 
			// label30
			// 
			this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(6, 76);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(36, 13);
			this.label30.TabIndex = 26;
			this.label30.Text = "Hand:";
			// 
			// label28
			// 
			this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(6, 49);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(62, 13);
			this.label28.TabIndex = 22;
			this.label28.Text = "From stage:";
			// 
			// tabConditions
			// 
			this.tabConditions.Controls.Add(this.gridFilters);
			this.tabConditions.Location = new System.Drawing.Point(4, 22);
			this.tabConditions.Name = "tabConditions";
			this.tabConditions.Padding = new System.Windows.Forms.Padding(3);
			this.tabConditions.Size = new System.Drawing.Size(631, 147);
			this.tabConditions.TabIndex = 3;
			this.tabConditions.Text = "Tags";
			this.tabConditions.UseVisualStyleBackColor = true;
			// 
			// gridFilters
			// 
			this.gridFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTagFilter,
            this.ColTagCount});
			this.gridFilters.Location = new System.Drawing.Point(6, 6);
			this.gridFilters.Name = "gridFilters";
			this.gridFilters.Size = new System.Drawing.Size(619, 135);
			this.gridFilters.TabIndex = 0;
			// 
			// ColTagFilter
			// 
			this.ColTagFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTagFilter.HeaderText = "Filter on Tag";
			this.ColTagFilter.Name = "ColTagFilter";
			this.ColTagFilter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColTagFilter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// ColTagCount
			// 
			this.ColTagCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTagCount.HeaderText = "Total Playing with Tag";
			this.ColTagCount.Name = "ColTagCount";
			// 
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.label37);
			this.tabMisc.Controls.Add(this.label32);
			this.tabMisc.Controls.Add(this.label31);
			this.tabMisc.Controls.Add(this.cboTotalMales);
			this.tabMisc.Controls.Add(this.cboTotalFemales);
			this.tabMisc.Controls.Add(this.cboOwnHand);
			this.tabMisc.Controls.Add(this.label26);
			this.tabMisc.Location = new System.Drawing.Point(4, 22);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(631, 147);
			this.tabMisc.TabIndex = 2;
			this.tabMisc.Text = "Misc";
			this.tabMisc.UseVisualStyleBackColor = true;
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(6, 3);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(213, 13);
			this.label37.TabIndex = 24;
			this.label37.Text = "Conditions not targeting anyone in particular";
			// 
			// label32
			// 
			this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(6, 76);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(64, 13);
			this.label32.TabIndex = 23;
			this.label32.Text = "Total males:";
			// 
			// label31
			// 
			this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(6, 49);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(73, 13);
			this.label31.TabIndex = 22;
			this.label31.Text = "Total females:";
			// 
			// cboTotalMales
			// 
			this.cboTotalMales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTotalMales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTotalMales.FormattingEnabled = true;
			this.cboTotalMales.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboTotalMales.Location = new System.Drawing.Point(81, 73);
			this.cboTotalMales.Name = "cboTotalMales";
			this.cboTotalMales.Size = new System.Drawing.Size(544, 21);
			this.cboTotalMales.TabIndex = 9;
			// 
			// cboTotalFemales
			// 
			this.cboTotalFemales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTotalFemales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTotalFemales.FormattingEnabled = true;
			this.cboTotalFemales.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboTotalFemales.Location = new System.Drawing.Point(81, 46);
			this.cboTotalFemales.Name = "cboTotalFemales";
			this.cboTotalFemales.Size = new System.Drawing.Size(544, 21);
			this.cboTotalFemales.TabIndex = 8;
			// 
			// cboOwnHand
			// 
			this.cboOwnHand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboOwnHand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOwnHand.FormattingEnabled = true;
			this.cboOwnHand.Items.AddRange(new object[] {
            "",
            "Nothing",
            "High Card",
            "One Pair",
            "Two Pair",
            "Three of a Kind",
            "Straight",
            "Flush",
            "Full House",
            "Four of a Kind",
            "Straight Flush",
            "Royal Flush"});
			this.cboOwnHand.Location = new System.Drawing.Point(81, 19);
			this.cboOwnHand.Name = "cboOwnHand";
			this.cboOwnHand.Size = new System.Drawing.Size(544, 21);
			this.cboOwnHand.TabIndex = 7;
			// 
			// label26
			// 
			this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(6, 22);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(59, 13);
			this.label26.TabIndex = 18;
			this.label26.Text = "Own hand:";
			// 
			// tabEndings
			// 
			this.tabEndings.Controls.Add(this.epilogueEditor);
			this.tabEndings.Location = new System.Drawing.Point(4, 22);
			this.tabEndings.Name = "tabEndings";
			this.tabEndings.Padding = new System.Windows.Forms.Padding(3);
			this.tabEndings.Size = new System.Drawing.Size(937, 668);
			this.tabEndings.TabIndex = 4;
			this.tabEndings.Text = "Epilogue";
			this.tabEndings.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(960, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(123, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Unique lines of dialogue:";
			// 
			// lblLinesOfDialogue
			// 
			this.lblLinesOfDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLinesOfDialogue.AutoSize = true;
			this.lblLinesOfDialogue.Location = new System.Drawing.Point(1089, 30);
			this.lblLinesOfDialogue.Name = "lblLinesOfDialogue";
			this.lblLinesOfDialogue.Size = new System.Drawing.Size(13, 13);
			this.lblLinesOfDialogue.TabIndex = 14;
			this.lblLinesOfDialogue.Text = "0";
			// 
			// splitMenu
			// 
			this.splitMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.separateThisStageIntoANewCaseToolStripMenuItem,
            this.splitAtPoint,
            this.splitAll,
            this.duplicateThisCaseToolStripMenuItem,
            this.bulkReplaceToolToolStripMenuItem});
			this.splitMenu.Name = "splitMenu";
			this.splitMenu.ShowImageMargin = false;
			this.splitMenu.Size = new System.Drawing.Size(297, 114);
			// 
			// separateThisStageIntoANewCaseToolStripMenuItem
			// 
			this.separateThisStageIntoANewCaseToolStripMenuItem.Name = "separateThisStageIntoANewCaseToolStripMenuItem";
			this.separateThisStageIntoANewCaseToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
			this.separateThisStageIntoANewCaseToolStripMenuItem.Text = "Separate This Stage into a New Case";
			this.separateThisStageIntoANewCaseToolStripMenuItem.Click += new System.EventHandler(this.separateThisStageIntoANewCaseToolStripMenuItem_Click);
			// 
			// splitAtPoint
			// 
			this.splitAtPoint.Name = "splitAtPoint";
			this.splitAtPoint.Size = new System.Drawing.Size(296, 22);
			this.splitAtPoint.Text = "Separate This and Later Stages into a New Case";
			this.splitAtPoint.Click += new System.EventHandler(this.splitAtPoint_Click);
			// 
			// splitAll
			// 
			this.splitAll.Name = "splitAll";
			this.splitAll.Size = new System.Drawing.Size(296, 22);
			this.splitAll.Text = "Split This Case into Individual Stages";
			this.splitAll.Click += new System.EventHandler(this.splitAll_Click);
			// 
			// duplicateThisCaseToolStripMenuItem
			// 
			this.duplicateThisCaseToolStripMenuItem.Name = "duplicateThisCaseToolStripMenuItem";
			this.duplicateThisCaseToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
			this.duplicateThisCaseToolStripMenuItem.Text = "Duplicate this Case";
			this.duplicateThisCaseToolStripMenuItem.Click += new System.EventHandler(this.cmdDupe_Click);
			// 
			// bulkReplaceToolToolStripMenuItem
			// 
			this.bulkReplaceToolToolStripMenuItem.Name = "bulkReplaceToolToolStripMenuItem";
			this.bulkReplaceToolToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
			this.bulkReplaceToolToolStripMenuItem.Text = "Bulk Replace Tool...";
			this.bulkReplaceToolToolStripMenuItem.Click += new System.EventHandler(this.bulkReplaceToolToolStripMenuItem_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// label38
			// 
			this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label38.AutoSize = true;
			this.label38.Location = new System.Drawing.Point(344, 49);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(23, 13);
			this.label38.TabIndex = 29;
			this.label38.Text = "To:";
			// 
			// cboAlsoPlayingMaxStage
			// 
			this.cboAlsoPlayingMaxStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboAlsoPlayingMaxStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboAlsoPlayingMaxStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboAlsoPlayingMaxStage.FormattingEnabled = true;
			this.cboAlsoPlayingMaxStage.Location = new System.Drawing.Point(373, 46);
			this.cboAlsoPlayingMaxStage.Name = "cboAlsoPlayingMaxStage";
			this.cboAlsoPlayingMaxStage.Size = new System.Drawing.Size(252, 21);
			this.cboAlsoPlayingMaxStage.TabIndex = 6;
			// 
			// imageImporter
			// 
			this.imageImporter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageImporter.Location = new System.Drawing.Point(3, 3);
			this.imageImporter.Name = "imageImporter";
			this.imageImporter.Size = new System.Drawing.Size(931, 662);
			this.imageImporter.TabIndex = 0;
			// 
			// gridDialogue
			// 
			this.gridDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridDialogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDialogue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColImage,
            this.ColText,
            this.ColSilent});
			this.gridDialogue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridDialogue.Location = new System.Drawing.Point(5, 414);
			this.gridDialogue.MultiSelect = false;
			this.gridDialogue.Name = "gridDialogue";
			this.gridDialogue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gridDialogue.Size = new System.Drawing.Size(655, 239);
			this.gridDialogue.TabIndex = 41;
			this.gridDialogue.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellEnter);
			this.gridDialogue.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.gridDialogue_CellParsing);
			this.gridDialogue.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridDialogue_CellValidating);
			this.gridDialogue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellValueChanged);
			this.gridDialogue.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridDialogue_CurrentCellDirtyStateChanged);
			this.gridDialogue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridDialogue_KeyDown);
			// 
			// ColImage
			// 
			this.ColImage.HeaderText = "Image";
			this.ColImage.MaxDropDownItems = 20;
			this.ColImage.Name = "ColImage";
			this.ColImage.Width = 150;
			// 
			// ColText
			// 
			this.ColText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColText.HeaderText = "Text";
			this.ColText.Name = "ColText";
			// 
			// ColSilent
			// 
			this.ColSilent.HeaderText = "Silent";
			this.ColSilent.Name = "ColSilent";
			this.ColSilent.Width = 50;
			// 
			// epilogueEditor
			// 
			this.epilogueEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.epilogueEditor.Enabled = false;
			this.epilogueEditor.Location = new System.Drawing.Point(3, 3);
			this.epilogueEditor.Name = "epilogueEditor";
			this.epilogueEditor.Size = new System.Drawing.Size(931, 662);
			this.epilogueEditor.TabIndex = 0;
			// 
			// cboTargetToStage
			// 
			this.cboTargetToStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTargetToStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTargetToStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboTargetToStage.FormattingEnabled = true;
			this.cboTargetToStage.Location = new System.Drawing.Point(373, 46);
			this.cboTargetToStage.Name = "cboTargetToStage";
			this.cboTargetToStage.Size = new System.Drawing.Size(252, 21);
			this.cboTargetToStage.TabIndex = 2;
			// 
			// label41
			// 
			this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label41.AutoSize = true;
			this.label41.Location = new System.Drawing.Point(344, 49);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(23, 13);
			this.label41.TabIndex = 31;
			this.label41.Text = "To:";
			// 
			// CharacterEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1217, 770);
			this.Controls.Add(this.lblLinesOfDialogue);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.txtLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.picPortrait);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(7, 7);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "CharacterEditor";
			this.Text = "Character Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditor_FormClosing);
			this.Load += new System.EventHandler(this.CharacterEditor_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabMetadata.ResumeLayout(false);
			this.tabMetadata.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridAI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTags)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valRounds)).EndInit();
			this.tabWardrobe.ResumeLayout(false);
			this.tabWardrobe.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabImages.ResumeLayout(false);
			this.tabDialogue.ResumeLayout(false);
			this.splitDialogue.Panel1.ResumeLayout(false);
			this.splitDialogue.Panel1.PerformLayout();
			this.splitDialogue.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitDialogue)).EndInit();
			this.splitDialogue.ResumeLayout(false);
			this.grpCase.ResumeLayout(false);
			this.grpCase.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.grpConditions.ResumeLayout(false);
			this.tabControlConditions.ResumeLayout(false);
			this.tabTarget.ResumeLayout(false);
			this.tabTarget.PerformLayout();
			this.tabOther.ResumeLayout(false);
			this.tabOther.PerformLayout();
			this.tabConditions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridFilters)).EndInit();
			this.tabMisc.ResumeLayout(false);
			this.tabMisc.PerformLayout();
			this.tabEndings.ResumeLayout(false);
			this.splitMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridDialogue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdAddLine;
		private System.Windows.Forms.Button cmdAddStage;
		private System.Windows.Forms.TreeView treeDialogue;
		private System.Windows.Forms.PictureBox picPortrait;
		private System.Windows.Forms.Button cmdDelete;
		private System.Windows.Forms.ComboBox cboLineTarget;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cboLineFilter;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button cmdInsert;
		private System.Windows.Forms.Button cmdCopy;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabMetadata;
		private System.Windows.Forms.TabPage tabDialogue;
		private System.Windows.Forms.NumericUpDown valRounds;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cboSize;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cboGender;
		private System.Windows.Forms.TabPage tabWardrobe;
		private System.Windows.Forms.Button cmdRemoveClothes;
		private System.Windows.Forms.Button cmdAddClothes;
		private System.Windows.Forms.ListBox lstClothes;
		private System.Windows.Forms.Button cmdClothesDown;
		private System.Windows.Forms.Button cmdClothesUp;
		private System.Windows.Forms.ComboBox cboClothesPosition;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ComboBox cboClothesType;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtClothesLowerCase;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtClothesProperName;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.DataGridView gridTags;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTag;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox txtArtist;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtWriter;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cboDefaultPic;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.ComboBox cboTargetStage;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.ComboBox cboAlsoPlayingStage;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.ComboBox cboAlsoPlaying;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.ComboBox cboOwnHand;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox cboTargetHand;
		private System.Windows.Forms.GroupBox grpCase;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.ComboBox cboAlsoPlayingHand;
		private System.Windows.Forms.GroupBox grpConditions;
		private System.Windows.Forms.TabControl tabControlConditions;
		private System.Windows.Forms.TabPage tabTarget;
		private System.Windows.Forms.TabPage tabOther;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox cboTotalMales;
		private System.Windows.Forms.ComboBox cboTotalFemales;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.FlowLayoutPanel flowStageChecks;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button cmdRemoveDialogue;
		private System.Windows.Forms.Button cmdAddDialogue;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Button cmdAddToListing;
		private SPNATI_Character_Editor.KeyboardDataGridView gridDialogue;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lblAvailableVars;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.ComboBox cboCaseTags;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Button cmdPasteAll;
		private System.Windows.Forms.Button cmdCopyAll;
		private System.Windows.Forms.Label lblHelpText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblLinesOfDialogue;
		private System.Windows.Forms.ToolStripMenuItem exporttxtFileForPythonToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label lblTypeHelp;
		private System.Windows.Forms.Label lblPositionHelp;
		private System.Windows.Forms.ToolStripMenuItem importtxtToolStripMenuItem;
		private System.Windows.Forms.TabPage tabImages;
		private Controls.ImageManager imageImporter;
		private System.Windows.Forms.Button cmdSplit;
		private System.Windows.Forms.ContextMenuStrip splitMenu;
		private System.Windows.Forms.ToolStripMenuItem splitAll;
		private System.Windows.Forms.ToolStripMenuItem splitAtPoint;
		private System.Windows.Forms.ToolStripMenuItem duplicateThisCaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bulkReplaceToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem validatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem currentCharacterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem allCharactersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem separateThisStageIntoANewCaseToolStripMenuItem;
		private System.Windows.Forms.TabPage tabEndings;
		private Controls.EpilogueEditor epilogueEditor;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutCharacterEditorToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.ComboBox cboTreeFilter;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.ComboBox cboTreeTarget;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.ToolStripMenuItem howToGuideToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
		private System.Windows.Forms.DataGridView gridAI;
		private System.Windows.Forms.TabPage tabConditions;
		private System.Windows.Forms.DataGridView gridFilters;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColTagFilter;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTagCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAIStage;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColDifficulty;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem graphsToolStripItem;
		private System.Windows.Forms.ToolStripMenuItem dialogueTesterToolStripMenuItem;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColImage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColText;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColSilent;
		private System.Windows.Forms.SplitContainer splitDialogue;
		private System.Windows.Forms.ComboBox cboAlsoPlayingMaxStage;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.ComboBox cboTargetToStage;
		private System.Windows.Forms.Label label41;
	}
}


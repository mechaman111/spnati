using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.Activities
{
	partial class DialogueEditor
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.splitDialogue = new System.Windows.Forms.SplitContainer();
			this.grpCase = new System.Windows.Forms.GroupBox();
			this.cmdMakeResponse = new System.Windows.Forms.Button();
			this.cmdToggleMode = new System.Windows.Forms.Button();
			this.cmdCallOut = new System.Windows.Forms.Button();
			this.ckbShowBubbleColumns = new System.Windows.Forms.CheckBox();
			this.lblHelpText = new System.Windows.Forms.Label();
			this.cmdPasteAll = new System.Windows.Forms.Button();
			this.cmdCopyAll = new System.Windows.Forms.Button();
			this.cboCaseTags = new System.Windows.Forms.ComboBox();
			this.label34 = new System.Windows.Forms.Label();
			this.lblAvailableVars = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.flowStageChecks = new System.Windows.Forms.FlowLayoutPanel();
			this.grpConditions = new System.Windows.Forms.GroupBox();
			this.valPriority = new System.Windows.Forms.NumericUpDown();
			this.label73 = new System.Windows.Forms.Label();
			this.tableConditions = new Desktop.CommonControls.PropertyTable();
			this.tabControlConditions = new System.Windows.Forms.TabControl();
			this.tabTarget = new System.Windows.Forms.TabPage();
			this.label76 = new System.Windows.Forms.Label();
			this.valMaxStartingLayers = new System.Windows.Forms.NumericUpDown();
			this.valStartingLayers = new System.Windows.Forms.NumericUpDown();
			this.lblTargetStartingLayers = new System.Windows.Forms.Label();
			this.ckbTargetStatusNegated = new System.Windows.Forms.CheckBox();
			this.valMaxLayers = new System.Windows.Forms.NumericUpDown();
			this.label74 = new System.Windows.Forms.Label();
			this.valLayers = new System.Windows.Forms.NumericUpDown();
			this.lblTargetLayers = new System.Windows.Forms.Label();
			this.cboTargetNotMarker = new System.Windows.Forms.ComboBox();
			this.label72 = new System.Windows.Forms.Label();
			this.cboTargetStatus = new System.Windows.Forms.ComboBox();
			this.label75 = new System.Windows.Forms.Label();
			this.label66 = new System.Windows.Forms.Label();
			this.valMaxTimeInStage = new System.Windows.Forms.NumericUpDown();
			this.label53 = new System.Windows.Forms.Label();
			this.valMaxLosses = new System.Windows.Forms.NumericUpDown();
			this.label52 = new System.Windows.Forms.Label();
			this.valTimeInStage = new System.Windows.Forms.NumericUpDown();
			this.label45 = new System.Windows.Forms.Label();
			this.valLosses = new System.Windows.Forms.NumericUpDown();
			this.label43 = new System.Windows.Forms.Label();
			this.cboTargetToStage = new System.Windows.Forms.ComboBox();
			this.label41 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.cboTargetHand = new System.Windows.Forms.ComboBox();
			this.cboLineTarget = new System.Windows.Forms.ComboBox();
			this.label29 = new System.Windows.Forms.Label();
			this.cboLineFilter = new System.Windows.Forms.ComboBox();
			this.cboTargetStage = new System.Windows.Forms.ComboBox();
			this.tabOther = new System.Windows.Forms.TabPage();
			this.cboAlsoPlayingNotMarker = new System.Windows.Forms.ComboBox();
			this.label71 = new System.Windows.Forms.Label();
			this.label67 = new System.Windows.Forms.Label();
			this.valMaxAlsoTimeInStage = new System.Windows.Forms.NumericUpDown();
			this.label54 = new System.Windows.Forms.Label();
			this.valAlsoTimeInStage = new System.Windows.Forms.NumericUpDown();
			this.label44 = new System.Windows.Forms.Label();
			this.cboAlsoPlayingMaxStage = new System.Windows.Forms.ComboBox();
			this.label38 = new System.Windows.Forms.Label();
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
			this.ColGenderFilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColStatusFilterNegated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColStatusFilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColFilterCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabSelf = new System.Windows.Forms.TabPage();
			this.cboNotMarker = new System.Windows.Forms.ComboBox();
			this.label70 = new System.Windows.Forms.Label();
			this.cboOwnHand = new System.Windows.Forms.ComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.label69 = new System.Windows.Forms.Label();
			this.valMaxOwnTimeInStage = new System.Windows.Forms.NumericUpDown();
			this.label63 = new System.Windows.Forms.Label();
			this.valMaxOwnLosses = new System.Windows.Forms.NumericUpDown();
			this.label57 = new System.Windows.Forms.Label();
			this.valOwnLosses = new System.Windows.Forms.NumericUpDown();
			this.label51 = new System.Windows.Forms.Label();
			this.valOwnTimeInStage = new System.Windows.Forms.NumericUpDown();
			this.label42 = new System.Windows.Forms.Label();
			this.label68 = new System.Windows.Forms.Label();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.valMaxGameRounds = new System.Windows.Forms.NumericUpDown();
			this.label64 = new System.Windows.Forms.Label();
			this.valGameRounds = new System.Windows.Forms.NumericUpDown();
			this.label65 = new System.Windows.Forms.Label();
			this.cboMaxTotalFinished = new System.Windows.Forms.ComboBox();
			this.label62 = new System.Windows.Forms.Label();
			this.cboMaxTotalFinishing = new System.Windows.Forms.ComboBox();
			this.label61 = new System.Windows.Forms.Label();
			this.cboMaxTotalNaked = new System.Windows.Forms.ComboBox();
			this.label60 = new System.Windows.Forms.Label();
			this.cboMaxTotalExposed = new System.Windows.Forms.ComboBox();
			this.label59 = new System.Windows.Forms.Label();
			this.cboMaxTotalPlaying = new System.Windows.Forms.ComboBox();
			this.label58 = new System.Windows.Forms.Label();
			this.cboMaxTotalMales = new System.Windows.Forms.ComboBox();
			this.label56 = new System.Windows.Forms.Label();
			this.cboMaxTotalFemales = new System.Windows.Forms.ComboBox();
			this.label55 = new System.Windows.Forms.Label();
			this.cboTotalFinished = new System.Windows.Forms.ComboBox();
			this.label50 = new System.Windows.Forms.Label();
			this.cboTotalFinishing = new System.Windows.Forms.ComboBox();
			this.label49 = new System.Windows.Forms.Label();
			this.cboTotalNaked = new System.Windows.Forms.ComboBox();
			this.label48 = new System.Windows.Forms.Label();
			this.cboTotalExposed = new System.Windows.Forms.ComboBox();
			this.label47 = new System.Windows.Forms.Label();
			this.cboTotalPlaying = new System.Windows.Forms.ComboBox();
			this.label46 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.cboTotalMales = new System.Windows.Forms.ComboBox();
			this.cboTotalFemales = new System.Windows.Forms.ComboBox();
			this.triggerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabDialogue = new System.Windows.Forms.TabPage();
			this.tabNotes = new System.Windows.Forms.TabPage();
			this.treeDialogue = new SPNATI_Character_Editor.Controls.DialogueTree();
			this.gridDialogue = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.markerTarget = new SPNATI_Character_Editor.Controls.MarkerConditionField();
			this.markerAlsoPlaying = new SPNATI_Character_Editor.Controls.MarkerConditionField();
			this.markerSelf = new SPNATI_Character_Editor.Controls.MarkerConditionField();
			this.label1 = new System.Windows.Forms.Label();
			this.txtNotes = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitDialogue)).BeginInit();
			this.splitDialogue.Panel1.SuspendLayout();
			this.splitDialogue.Panel2.SuspendLayout();
			this.splitDialogue.SuspendLayout();
			this.grpCase.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.grpConditions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valPriority)).BeginInit();
			this.tabControlConditions.SuspendLayout();
			this.tabTarget.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxStartingLayers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valStartingLayers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxLayers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valLayers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxTimeInStage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxLosses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTimeInStage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valLosses)).BeginInit();
			this.tabOther.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxAlsoTimeInStage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valAlsoTimeInStage)).BeginInit();
			this.tabConditions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridFilters)).BeginInit();
			this.tabSelf.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxOwnTimeInStage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxOwnLosses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valOwnLosses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valOwnTimeInStage)).BeginInit();
			this.tabMisc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxGameRounds)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valGameRounds)).BeginInit();
			this.tabs.SuspendLayout();
			this.tabDialogue.SuspendLayout();
			this.tabNotes.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitDialogue
			// 
			this.splitDialogue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitDialogue.Location = new System.Drawing.Point(0, 0);
			this.splitDialogue.Name = "splitDialogue";
			// 
			// splitDialogue.Panel1
			// 
			this.splitDialogue.Panel1.Controls.Add(this.treeDialogue);
			// 
			// splitDialogue.Panel2
			// 
			this.splitDialogue.Panel2.Controls.Add(this.grpCase);
			this.splitDialogue.Size = new System.Drawing.Size(973, 671);
			this.splitDialogue.SplitterDistance = 266;
			this.splitDialogue.TabIndex = 16;
			// 
			// grpCase
			// 
			this.grpCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpCase.BackColor = System.Drawing.SystemColors.Control;
			this.grpCase.Controls.Add(this.tabs);
			this.grpCase.Controls.Add(this.cmdMakeResponse);
			this.grpCase.Controls.Add(this.cmdToggleMode);
			this.grpCase.Controls.Add(this.cmdCallOut);
			this.grpCase.Controls.Add(this.lblHelpText);
			this.grpCase.Controls.Add(this.cboCaseTags);
			this.grpCase.Controls.Add(this.label34);
			this.grpCase.Controls.Add(this.groupBox3);
			this.grpCase.Controls.Add(this.grpConditions);
			this.grpCase.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grpCase.Location = new System.Drawing.Point(3, 0);
			this.grpCase.Name = "grpCase";
			this.grpCase.Size = new System.Drawing.Size(697, 668);
			this.grpCase.TabIndex = 28;
			this.grpCase.TabStop = false;
			this.grpCase.Text = "Edit Case";
			// 
			// cmdMakeResponse
			// 
			this.cmdMakeResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdMakeResponse.Location = new System.Drawing.Point(506, 17);
			this.cmdMakeResponse.Name = "cmdMakeResponse";
			this.cmdMakeResponse.Size = new System.Drawing.Size(104, 23);
			this.cmdMakeResponse.TabIndex = 45;
			this.cmdMakeResponse.Text = "Respond to This...";
			this.toolTip1.SetToolTip(this.cmdMakeResponse, "Creates a response to this case on another character");
			this.cmdMakeResponse.UseVisualStyleBackColor = true;
			this.cmdMakeResponse.Click += new System.EventHandler(this.cmdMakeResponse_Click);
			// 
			// cmdToggleMode
			// 
			this.cmdToggleMode.Location = new System.Drawing.Point(71, 180);
			this.cmdToggleMode.Name = "cmdToggleMode";
			this.cmdToggleMode.Size = new System.Drawing.Size(20, 20);
			this.cmdToggleMode.TabIndex = 32;
			this.cmdToggleMode.Text = "↺";
			this.toolTip1.SetToolTip(this.cmdToggleMode, "Toggles between new and old condition editors");
			this.cmdToggleMode.UseVisualStyleBackColor = true;
			this.cmdToggleMode.Click += new System.EventHandler(this.cmdToggleMode_Click);
			// 
			// cmdCallOut
			// 
			this.cmdCallOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCallOut.Location = new System.Drawing.Point(616, 17);
			this.cmdCallOut.Name = "cmdCallOut";
			this.cmdCallOut.Size = new System.Drawing.Size(75, 23);
			this.cmdCallOut.TabIndex = 44;
			this.cmdCallOut.Text = "Call Out...";
			this.toolTip1.SetToolTip(this.cmdCallOut, "Marks this situation as being \"noteworthy\" so it will appear in other character\'s" +
        " Writing Aids.");
			this.cmdCallOut.UseVisualStyleBackColor = true;
			this.cmdCallOut.Click += new System.EventHandler(this.cmdCallOut_Click);
			// 
			// ckbShowBubbleColumns
			// 
			this.ckbShowBubbleColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ckbShowBubbleColumns.AutoSize = true;
			this.ckbShowBubbleColumns.Location = new System.Drawing.Point(353, 6);
			this.ckbShowBubbleColumns.Name = "ckbShowBubbleColumns";
			this.ckbShowBubbleColumns.Size = new System.Drawing.Size(168, 17);
			this.ckbShowBubbleColumns.TabIndex = 43;
			this.ckbShowBubbleColumns.Text = "Show speech bubble columns";
			this.ckbShowBubbleColumns.UseVisualStyleBackColor = true;
			this.ckbShowBubbleColumns.CheckedChanged += new System.EventHandler(this.ckbShowSpeechBubbleColumns_CheckedChanged);
			// 
			// lblHelpText
			// 
			this.lblHelpText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHelpText.Location = new System.Drawing.Point(222, 0);
			this.lblHelpText.Name = "lblHelpText";
			this.lblHelpText.Size = new System.Drawing.Size(278, 59);
			this.lblHelpText.TabIndex = 38;
			this.lblHelpText.Text = "Help Text";
			this.lblHelpText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmdPasteAll
			// 
			this.cmdPasteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPasteAll.Location = new System.Drawing.Point(608, 3);
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
			this.cmdCopyAll.Location = new System.Drawing.Point(527, 3);
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
			// lblAvailableVars
			// 
			this.lblAvailableVars.AutoSize = true;
			this.lblAvailableVars.Location = new System.Drawing.Point(2, 8);
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
			this.groupBox3.Size = new System.Drawing.Size(682, 132);
			this.groupBox3.TabIndex = 37;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Applies to Stages";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.BackColor = System.Drawing.Color.White;
			this.chkSelectAll.Location = new System.Drawing.Point(609, -1);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
			this.chkSelectAll.TabIndex = 36;
			this.chkSelectAll.Text = "Select All";
			this.chkSelectAll.UseVisualStyleBackColor = false;
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// flowStageChecks
			// 
			this.flowStageChecks.AutoScroll = true;
			this.flowStageChecks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowStageChecks.Location = new System.Drawing.Point(3, 16);
			this.flowStageChecks.Name = "flowStageChecks";
			this.flowStageChecks.Size = new System.Drawing.Size(676, 113);
			this.flowStageChecks.TabIndex = 0;
			// 
			// grpConditions
			// 
			this.grpConditions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpConditions.Controls.Add(this.valPriority);
			this.grpConditions.Controls.Add(this.label73);
			this.grpConditions.Controls.Add(this.tableConditions);
			this.grpConditions.Controls.Add(this.tabControlConditions);
			this.grpConditions.Location = new System.Drawing.Point(9, 184);
			this.grpConditions.Name = "grpConditions";
			this.grpConditions.Size = new System.Drawing.Size(682, 233);
			this.grpConditions.TabIndex = 38;
			this.grpConditions.TabStop = false;
			this.grpConditions.Text = "Conditions";
			// 
			// valPriority
			// 
			this.valPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valPriority.Location = new System.Drawing.Point(618, 20);
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
			this.label73.Location = new System.Drawing.Point(573, 23);
			this.label73.Name = "label73";
			this.label73.Size = new System.Drawing.Size(41, 13);
			this.label73.TabIndex = 60;
			this.label73.Text = "Priority:";
			// 
			// tableConditions
			// 
			this.tableConditions.AllowDelete = true;
			this.tableConditions.AllowFavorites = true;
			this.tableConditions.AllowHelp = false;
			this.tableConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableConditions.Data = null;
			this.tableConditions.HideAddField = false;
			this.tableConditions.HideSpeedButtons = false;
			this.tableConditions.Location = new System.Drawing.Point(6, 19);
			this.tableConditions.Name = "tableConditions";
			this.tableConditions.PlaceholderText = "Add a condition";
			this.tableConditions.RemoveCaption = "Remove condition";
			this.tableConditions.RowHeaderWidth = 0F;
			this.tableConditions.Size = new System.Drawing.Size(670, 208);
			this.tableConditions.Sorted = false;
			this.tableConditions.TabIndex = 31;
			this.tableConditions.UseAutoComplete = true;
			// 
			// tabControlConditions
			// 
			this.tabControlConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlConditions.Controls.Add(this.tabTarget);
			this.tabControlConditions.Controls.Add(this.tabOther);
			this.tabControlConditions.Controls.Add(this.tabConditions);
			this.tabControlConditions.Controls.Add(this.tabSelf);
			this.tabControlConditions.Controls.Add(this.tabMisc);
			this.tabControlConditions.Location = new System.Drawing.Point(6, 19);
			this.tabControlConditions.Name = "tabControlConditions";
			this.tabControlConditions.SelectedIndex = 0;
			this.tabControlConditions.Size = new System.Drawing.Size(670, 211);
			this.tabControlConditions.TabIndex = 30;
			// 
			// tabTarget
			// 
			this.tabTarget.Controls.Add(this.markerTarget);
			this.tabTarget.Controls.Add(this.label76);
			this.tabTarget.Controls.Add(this.valMaxStartingLayers);
			this.tabTarget.Controls.Add(this.valStartingLayers);
			this.tabTarget.Controls.Add(this.lblTargetStartingLayers);
			this.tabTarget.Controls.Add(this.ckbTargetStatusNegated);
			this.tabTarget.Controls.Add(this.valMaxLayers);
			this.tabTarget.Controls.Add(this.label74);
			this.tabTarget.Controls.Add(this.valLayers);
			this.tabTarget.Controls.Add(this.lblTargetLayers);
			this.tabTarget.Controls.Add(this.cboTargetNotMarker);
			this.tabTarget.Controls.Add(this.label72);
			this.tabTarget.Controls.Add(this.cboTargetStatus);
			this.tabTarget.Controls.Add(this.label75);
			this.tabTarget.Controls.Add(this.label66);
			this.tabTarget.Controls.Add(this.valMaxTimeInStage);
			this.tabTarget.Controls.Add(this.label53);
			this.tabTarget.Controls.Add(this.valMaxLosses);
			this.tabTarget.Controls.Add(this.label52);
			this.tabTarget.Controls.Add(this.valTimeInStage);
			this.tabTarget.Controls.Add(this.label45);
			this.tabTarget.Controls.Add(this.valLosses);
			this.tabTarget.Controls.Add(this.label43);
			this.tabTarget.Controls.Add(this.cboTargetToStage);
			this.tabTarget.Controls.Add(this.label41);
			this.tabTarget.Controls.Add(this.label21);
			this.tabTarget.Controls.Add(this.label6);
			this.tabTarget.Controls.Add(this.label5);
			this.tabTarget.Controls.Add(this.label25);
			this.tabTarget.Controls.Add(this.cboTargetHand);
			this.tabTarget.Controls.Add(this.cboLineTarget);
			this.tabTarget.Controls.Add(this.label29);
			this.tabTarget.Controls.Add(this.cboLineFilter);
			this.tabTarget.Controls.Add(this.cboTargetStage);
			this.tabTarget.Location = new System.Drawing.Point(4, 22);
			this.tabTarget.Name = "tabTarget";
			this.tabTarget.Padding = new System.Windows.Forms.Padding(3);
			this.tabTarget.Size = new System.Drawing.Size(662, 185);
			this.tabTarget.TabIndex = 0;
			this.tabTarget.Text = "Target";
			this.tabTarget.UseVisualStyleBackColor = true;
			// 
			// label76
			// 
			this.label76.AutoSize = true;
			this.label76.Location = new System.Drawing.Point(517, 22);
			this.label76.Name = "label76";
			this.label76.Size = new System.Drawing.Size(19, 13);
			this.label76.TabIndex = 12;
			this.label76.Text = "to:";
			// 
			// valMaxStartingLayers
			// 
			this.valMaxStartingLayers.Location = new System.Drawing.Point(542, 20);
			this.valMaxStartingLayers.Name = "valMaxStartingLayers";
			this.valMaxStartingLayers.Size = new System.Drawing.Size(43, 20);
			this.valMaxStartingLayers.TabIndex = 13;
			// 
			// valStartingLayers
			// 
			this.valStartingLayers.Location = new System.Drawing.Point(468, 20);
			this.valStartingLayers.Name = "valStartingLayers";
			this.valStartingLayers.Size = new System.Drawing.Size(43, 20);
			this.valStartingLayers.TabIndex = 11;
			// 
			// lblTargetStartingLayers
			// 
			this.lblTargetStartingLayers.AutoSize = true;
			this.lblTargetStartingLayers.Location = new System.Drawing.Point(344, 22);
			this.lblTargetStartingLayers.Name = "lblTargetStartingLayers";
			this.lblTargetStartingLayers.Size = new System.Drawing.Size(76, 13);
			this.lblTargetStartingLayers.TabIndex = 10;
			this.lblTargetStartingLayers.Text = "Starting layers:";
			// 
			// ckbTargetStatusNegated
			// 
			this.ckbTargetStatusNegated.AutoSize = true;
			this.ckbTargetStatusNegated.Location = new System.Drawing.Point(81, 75);
			this.ckbTargetStatusNegated.Name = "ckbTargetStatusNegated";
			this.ckbTargetStatusNegated.Size = new System.Drawing.Size(43, 17);
			this.ckbTargetStatusNegated.TabIndex = 31;
			this.ckbTargetStatusNegated.Text = "Not";
			this.ckbTargetStatusNegated.UseVisualStyleBackColor = true;
			// 
			// valMaxLayers
			// 
			this.valMaxLayers.Location = new System.Drawing.Point(542, 74);
			this.valMaxLayers.Name = "valMaxLayers";
			this.valMaxLayers.Size = new System.Drawing.Size(43, 20);
			this.valMaxLayers.TabIndex = 43;
			// 
			// label74
			// 
			this.label74.AutoSize = true;
			this.label74.Location = new System.Drawing.Point(517, 76);
			this.label74.Name = "label74";
			this.label74.Size = new System.Drawing.Size(19, 13);
			this.label74.TabIndex = 42;
			this.label74.Text = "to:";
			// 
			// valLayers
			// 
			this.valLayers.Location = new System.Drawing.Point(468, 74);
			this.valLayers.Name = "valLayers";
			this.valLayers.Size = new System.Drawing.Size(43, 20);
			this.valLayers.TabIndex = 41;
			// 
			// lblTargetLayers
			// 
			this.lblTargetLayers.AutoSize = true;
			this.lblTargetLayers.Location = new System.Drawing.Point(344, 76);
			this.lblTargetLayers.Name = "lblTargetLayers";
			this.lblTargetLayers.Size = new System.Drawing.Size(58, 13);
			this.lblTargetLayers.TabIndex = 40;
			this.lblTargetLayers.Text = "Layers left:";
			// 
			// cboTargetNotMarker
			// 
			this.cboTargetNotMarker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTargetNotMarker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboTargetNotMarker.FormattingEnabled = true;
			this.cboTargetNotMarker.Location = new System.Drawing.Point(416, 154);
			this.cboTargetNotMarker.Name = "cboTargetNotMarker";
			this.cboTargetNotMarker.Size = new System.Drawing.Size(209, 21);
			this.cboTargetNotMarker.TabIndex = 93;
			// 
			// label72
			// 
			this.label72.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label72.AutoSize = true;
			this.label72.Location = new System.Drawing.Point(344, 157);
			this.label72.Name = "label72";
			this.label72.Size = new System.Drawing.Size(49, 13);
			this.label72.TabIndex = 92;
			this.label72.Text = "Not said:";
			// 
			// cboTargetStatus
			// 
			this.cboTargetStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTargetStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboTargetStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTargetStatus.FormattingEnabled = true;
			this.cboTargetStatus.Location = new System.Drawing.Point(130, 72);
			this.cboTargetStatus.Name = "cboTargetStatus";
			this.cboTargetStatus.Size = new System.Drawing.Size(193, 21);
			this.cboTargetStatus.TabIndex = 32;
			// 
			// label75
			// 
			this.label75.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label75.AutoSize = true;
			this.label75.Location = new System.Drawing.Point(6, 76);
			this.label75.Name = "label75";
			this.label75.Size = new System.Drawing.Size(40, 13);
			this.label75.TabIndex = 30;
			this.label75.Text = "Status:";
			// 
			// label66
			// 
			this.label66.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label66.AutoSize = true;
			this.label66.Location = new System.Drawing.Point(6, 157);
			this.label66.Name = "label66";
			this.label66.Size = new System.Drawing.Size(66, 13);
			this.label66.TabIndex = 90;
			this.label66.Text = "Said marker:";
			// 
			// valMaxTimeInStage
			// 
			this.valMaxTimeInStage.Location = new System.Drawing.Point(542, 128);
			this.valMaxTimeInStage.Name = "valMaxTimeInStage";
			this.valMaxTimeInStage.Size = new System.Drawing.Size(43, 20);
			this.valMaxTimeInStage.TabIndex = 83;
			// 
			// label53
			// 
			this.label53.AutoSize = true;
			this.label53.Location = new System.Drawing.Point(517, 130);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(19, 13);
			this.label53.TabIndex = 82;
			this.label53.Text = "to:";
			// 
			// valMaxLosses
			// 
			this.valMaxLosses.Location = new System.Drawing.Point(542, 101);
			this.valMaxLosses.Name = "valMaxLosses";
			this.valMaxLosses.Size = new System.Drawing.Size(43, 20);
			this.valMaxLosses.TabIndex = 63;
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Location = new System.Drawing.Point(517, 103);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(19, 13);
			this.label52.TabIndex = 62;
			this.label52.Text = "to:";
			// 
			// valTimeInStage
			// 
			this.valTimeInStage.Location = new System.Drawing.Point(468, 128);
			this.valTimeInStage.Name = "valTimeInStage";
			this.valTimeInStage.Size = new System.Drawing.Size(43, 20);
			this.valTimeInStage.TabIndex = 81;
			// 
			// label45
			// 
			this.label45.AutoSize = true;
			this.label45.Location = new System.Drawing.Point(344, 130);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(87, 13);
			this.label45.TabIndex = 80;
			this.label45.Text = "Rounds in stage:";
			// 
			// valLosses
			// 
			this.valLosses.Location = new System.Drawing.Point(468, 101);
			this.valLosses.Name = "valLosses";
			this.valLosses.Size = new System.Drawing.Size(43, 20);
			this.valLosses.TabIndex = 61;
			// 
			// label43
			// 
			this.label43.AutoSize = true;
			this.label43.Location = new System.Drawing.Point(344, 103);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(101, 13);
			this.label43.TabIndex = 60;
			this.label43.Text = "Consecutive losses:";
			// 
			// cboTargetToStage
			// 
			this.cboTargetToStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTargetToStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboTargetToStage.FormattingEnabled = true;
			this.cboTargetToStage.Location = new System.Drawing.Point(373, 46);
			this.cboTargetToStage.Name = "cboTargetToStage";
			this.cboTargetToStage.Size = new System.Drawing.Size(252, 21);
			this.cboTargetToStage.TabIndex = 23;
			// 
			// label41
			// 
			this.label41.AutoSize = true;
			this.label41.Location = new System.Drawing.Point(344, 49);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(23, 13);
			this.label41.TabIndex = 22;
			this.label41.Text = "To:";
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
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 130);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 13);
			this.label6.TabIndex = 70;
			this.label6.Text = "Tag:";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Player:";
			// 
			// label25
			// 
			this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(6, 103);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(36, 13);
			this.label25.TabIndex = 50;
			this.label25.Text = "Hand:";
			// 
			// cboTargetHand
			// 
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
			this.cboTargetHand.Location = new System.Drawing.Point(81, 100);
			this.cboTargetHand.Name = "cboTargetHand";
			this.cboTargetHand.Size = new System.Drawing.Size(242, 21);
			this.cboTargetHand.TabIndex = 51;
			// 
			// cboLineTarget
			// 
			this.cboLineTarget.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboLineTarget.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboLineTarget.FormattingEnabled = true;
			this.cboLineTarget.Location = new System.Drawing.Point(81, 19);
			this.cboLineTarget.Name = "cboLineTarget";
			this.cboLineTarget.Size = new System.Drawing.Size(242, 21);
			this.cboLineTarget.TabIndex = 1;
			this.cboLineTarget.SelectedIndexChanged += new System.EventHandler(this.cboLineTarget_SelectedIndexChanged);
			// 
			// label29
			// 
			this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(6, 49);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(62, 13);
			this.label29.TabIndex = 20;
			this.label29.Text = "From stage:";
			// 
			// cboLineFilter
			// 
			this.cboLineFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboLineFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboLineFilter.FormattingEnabled = true;
			this.cboLineFilter.Location = new System.Drawing.Point(81, 127);
			this.cboLineFilter.Name = "cboLineFilter";
			this.cboLineFilter.Size = new System.Drawing.Size(242, 21);
			this.cboLineFilter.TabIndex = 71;
			// 
			// cboTargetStage
			// 
			this.cboTargetStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTargetStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboTargetStage.FormattingEnabled = true;
			this.cboTargetStage.Location = new System.Drawing.Point(81, 46);
			this.cboTargetStage.Name = "cboTargetStage";
			this.cboTargetStage.Size = new System.Drawing.Size(242, 21);
			this.cboTargetStage.TabIndex = 21;
			// 
			// tabOther
			// 
			this.tabOther.Controls.Add(this.markerAlsoPlaying);
			this.tabOther.Controls.Add(this.cboAlsoPlayingNotMarker);
			this.tabOther.Controls.Add(this.label71);
			this.tabOther.Controls.Add(this.label67);
			this.tabOther.Controls.Add(this.valMaxAlsoTimeInStage);
			this.tabOther.Controls.Add(this.label54);
			this.tabOther.Controls.Add(this.valAlsoTimeInStage);
			this.tabOther.Controls.Add(this.label44);
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
			this.tabOther.Size = new System.Drawing.Size(662, 185);
			this.tabOther.TabIndex = 1;
			this.tabOther.Text = "Other Player";
			this.tabOther.UseVisualStyleBackColor = true;
			// 
			// cboAlsoPlayingNotMarker
			// 
			this.cboAlsoPlayingNotMarker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboAlsoPlayingNotMarker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboAlsoPlayingNotMarker.FormattingEnabled = true;
			this.cboAlsoPlayingNotMarker.Location = new System.Drawing.Point(418, 126);
			this.cboAlsoPlayingNotMarker.Name = "cboAlsoPlayingNotMarker";
			this.cboAlsoPlayingNotMarker.Size = new System.Drawing.Size(207, 21);
			this.cboAlsoPlayingNotMarker.TabIndex = 45;
			// 
			// label71
			// 
			this.label71.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label71.AutoSize = true;
			this.label71.Location = new System.Drawing.Point(325, 129);
			this.label71.Name = "label71";
			this.label71.Size = new System.Drawing.Size(84, 13);
			this.label71.TabIndex = 44;
			this.label71.Text = "Not said marker:";
			// 
			// label67
			// 
			this.label67.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label67.AutoSize = true;
			this.label67.Location = new System.Drawing.Point(6, 129);
			this.label67.Name = "label67";
			this.label67.Size = new System.Drawing.Size(66, 13);
			this.label67.TabIndex = 42;
			this.label67.Text = "Said marker:";
			// 
			// valMaxAlsoTimeInStage
			// 
			this.valMaxAlsoTimeInStage.Location = new System.Drawing.Point(195, 100);
			this.valMaxAlsoTimeInStage.Name = "valMaxAlsoTimeInStage";
			this.valMaxAlsoTimeInStage.Size = new System.Drawing.Size(65, 20);
			this.valMaxAlsoTimeInStage.TabIndex = 33;
			// 
			// label54
			// 
			this.label54.AutoSize = true;
			this.label54.Location = new System.Drawing.Point(170, 102);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(19, 13);
			this.label54.TabIndex = 32;
			this.label54.Text = "to:";
			// 
			// valAlsoTimeInStage
			// 
			this.valAlsoTimeInStage.Location = new System.Drawing.Point(99, 100);
			this.valAlsoTimeInStage.Name = "valAlsoTimeInStage";
			this.valAlsoTimeInStage.Size = new System.Drawing.Size(65, 20);
			this.valAlsoTimeInStage.TabIndex = 31;
			// 
			// label44
			// 
			this.label44.AutoSize = true;
			this.label44.Location = new System.Drawing.Point(6, 102);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(87, 13);
			this.label44.TabIndex = 30;
			this.label44.Text = "Rounds in stage:";
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
			this.cboAlsoPlaying.Location = new System.Drawing.Point(99, 19);
			this.cboAlsoPlaying.Name = "cboAlsoPlaying";
			this.cboAlsoPlaying.Size = new System.Drawing.Size(526, 21);
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
			this.cboAlsoPlayingStage.Location = new System.Drawing.Point(99, 46);
			this.cboAlsoPlayingStage.Name = "cboAlsoPlayingStage";
			this.cboAlsoPlayingStage.Size = new System.Drawing.Size(224, 21);
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
			this.cboAlsoPlayingHand.Location = new System.Drawing.Point(99, 73);
			this.cboAlsoPlayingHand.Name = "cboAlsoPlayingHand";
			this.cboAlsoPlayingHand.Size = new System.Drawing.Size(526, 21);
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
			this.tabConditions.Size = new System.Drawing.Size(662, 185);
			this.tabConditions.TabIndex = 3;
			this.tabConditions.Text = "Tag/gender/status counts";
			this.tabConditions.UseVisualStyleBackColor = true;
			// 
			// gridFilters
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridFilters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTagFilter,
            this.ColGenderFilter,
            this.ColStatusFilterNegated,
            this.ColStatusFilter,
            this.ColFilterCount});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridFilters.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridFilters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridFilters.Location = new System.Drawing.Point(3, 3);
			this.gridFilters.Name = "gridFilters";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridFilters.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridFilters.Size = new System.Drawing.Size(656, 179);
			this.gridFilters.TabIndex = 0;
			// 
			// ColTagFilter
			// 
			this.ColTagFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTagFilter.HeaderText = "Tag";
			this.ColTagFilter.Name = "ColTagFilter";
			this.ColTagFilter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// ColGenderFilter
			// 
			this.ColGenderFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColGenderFilter.FillWeight = 80F;
			this.ColGenderFilter.HeaderText = "Gender";
			this.ColGenderFilter.Items.AddRange(new object[] {
            "",
            "female",
            "male"});
			this.ColGenderFilter.Name = "ColGenderFilter";
			// 
			// ColStatusFilterNegated
			// 
			this.ColStatusFilterNegated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.ColStatusFilterNegated.HeaderText = "Not";
			this.ColStatusFilterNegated.Name = "ColStatusFilterNegated";
			this.ColStatusFilterNegated.Width = 30;
			// 
			// ColStatusFilter
			// 
			this.ColStatusFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColStatusFilter.DropDownWidth = 2;
			this.ColStatusFilter.HeaderText = "Status";
			this.ColStatusFilter.Name = "ColStatusFilter";
			// 
			// ColFilterCount
			// 
			this.ColFilterCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColFilterCount.FillWeight = 80F;
			this.ColFilterCount.HeaderText = "Required number";
			this.ColFilterCount.Name = "ColFilterCount";
			// 
			// tabSelf
			// 
			this.tabSelf.Controls.Add(this.markerSelf);
			this.tabSelf.Controls.Add(this.cboNotMarker);
			this.tabSelf.Controls.Add(this.label70);
			this.tabSelf.Controls.Add(this.cboOwnHand);
			this.tabSelf.Controls.Add(this.label26);
			this.tabSelf.Controls.Add(this.label69);
			this.tabSelf.Controls.Add(this.valMaxOwnTimeInStage);
			this.tabSelf.Controls.Add(this.label63);
			this.tabSelf.Controls.Add(this.valMaxOwnLosses);
			this.tabSelf.Controls.Add(this.label57);
			this.tabSelf.Controls.Add(this.valOwnLosses);
			this.tabSelf.Controls.Add(this.label51);
			this.tabSelf.Controls.Add(this.valOwnTimeInStage);
			this.tabSelf.Controls.Add(this.label42);
			this.tabSelf.Controls.Add(this.label68);
			this.tabSelf.Location = new System.Drawing.Point(4, 22);
			this.tabSelf.Name = "tabSelf";
			this.tabSelf.Padding = new System.Windows.Forms.Padding(3);
			this.tabSelf.Size = new System.Drawing.Size(662, 185);
			this.tabSelf.TabIndex = 4;
			this.tabSelf.Text = "Self";
			this.tabSelf.UseVisualStyleBackColor = true;
			// 
			// cboNotMarker
			// 
			this.cboNotMarker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboNotMarker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboNotMarker.FormattingEnabled = true;
			this.cboNotMarker.Location = new System.Drawing.Point(428, 45);
			this.cboNotMarker.Name = "cboNotMarker";
			this.cboNotMarker.Size = new System.Drawing.Size(197, 21);
			this.cboNotMarker.TabIndex = 61;
			// 
			// label70
			// 
			this.label70.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label70.AutoSize = true;
			this.label70.Location = new System.Drawing.Point(321, 48);
			this.label70.Name = "label70";
			this.label70.Size = new System.Drawing.Size(84, 13);
			this.label70.TabIndex = 67;
			this.label70.Text = "Not said marker:";
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
			this.cboOwnHand.Location = new System.Drawing.Point(115, 72);
			this.cboOwnHand.Name = "cboOwnHand";
			this.cboOwnHand.Size = new System.Drawing.Size(200, 21);
			this.cboOwnHand.TabIndex = 62;
			// 
			// label26
			// 
			this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(6, 75);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(59, 13);
			this.label26.TabIndex = 66;
			this.label26.Text = "Own hand:";
			// 
			// label69
			// 
			this.label69.AutoSize = true;
			this.label69.Location = new System.Drawing.Point(6, 3);
			this.label69.Name = "label69";
			this.label69.Size = new System.Drawing.Size(147, 13);
			this.label69.TabIndex = 64;
			this.label69.Text = "Targets this own player\'s data";
			// 
			// valMaxOwnTimeInStage
			// 
			this.valMaxOwnTimeInStage.Location = new System.Drawing.Point(195, 19);
			this.valMaxOwnTimeInStage.Name = "valMaxOwnTimeInStage";
			this.valMaxOwnTimeInStage.Size = new System.Drawing.Size(54, 20);
			this.valMaxOwnTimeInStage.TabIndex = 57;
			// 
			// label63
			// 
			this.label63.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label63.AutoSize = true;
			this.label63.Location = new System.Drawing.Point(175, 21);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(19, 13);
			this.label63.TabIndex = 63;
			this.label63.Text = "to:";
			// 
			// valMaxOwnLosses
			// 
			this.valMaxOwnLosses.Location = new System.Drawing.Point(513, 19);
			this.valMaxOwnLosses.Name = "valMaxOwnLosses";
			this.valMaxOwnLosses.Size = new System.Drawing.Size(54, 20);
			this.valMaxOwnLosses.TabIndex = 59;
			// 
			// label57
			// 
			this.label57.AutoSize = true;
			this.label57.Location = new System.Drawing.Point(488, 21);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(19, 13);
			this.label57.TabIndex = 62;
			this.label57.Text = "to:";
			// 
			// valOwnLosses
			// 
			this.valOwnLosses.Location = new System.Drawing.Point(428, 19);
			this.valOwnLosses.Name = "valOwnLosses";
			this.valOwnLosses.Size = new System.Drawing.Size(54, 20);
			this.valOwnLosses.TabIndex = 58;
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.Location = new System.Drawing.Point(321, 21);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(101, 13);
			this.label51.TabIndex = 61;
			this.label51.Text = "Consecutive losses:";
			// 
			// valOwnTimeInStage
			// 
			this.valOwnTimeInStage.Location = new System.Drawing.Point(115, 19);
			this.valOwnTimeInStage.Name = "valOwnTimeInStage";
			this.valOwnTimeInStage.Size = new System.Drawing.Size(54, 20);
			this.valOwnTimeInStage.TabIndex = 56;
			// 
			// label42
			// 
			this.label42.AutoSize = true;
			this.label42.Location = new System.Drawing.Point(6, 21);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(87, 13);
			this.label42.TabIndex = 60;
			this.label42.Text = "Rounds in stage:";
			// 
			// label68
			// 
			this.label68.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label68.AutoSize = true;
			this.label68.Location = new System.Drawing.Point(6, 48);
			this.label68.Name = "label68";
			this.label68.Size = new System.Drawing.Size(66, 13);
			this.label68.TabIndex = 45;
			this.label68.Text = "Said marker:";
			// 
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.valMaxGameRounds);
			this.tabMisc.Controls.Add(this.label64);
			this.tabMisc.Controls.Add(this.valGameRounds);
			this.tabMisc.Controls.Add(this.label65);
			this.tabMisc.Controls.Add(this.cboMaxTotalFinished);
			this.tabMisc.Controls.Add(this.label62);
			this.tabMisc.Controls.Add(this.cboMaxTotalFinishing);
			this.tabMisc.Controls.Add(this.label61);
			this.tabMisc.Controls.Add(this.cboMaxTotalNaked);
			this.tabMisc.Controls.Add(this.label60);
			this.tabMisc.Controls.Add(this.cboMaxTotalExposed);
			this.tabMisc.Controls.Add(this.label59);
			this.tabMisc.Controls.Add(this.cboMaxTotalPlaying);
			this.tabMisc.Controls.Add(this.label58);
			this.tabMisc.Controls.Add(this.cboMaxTotalMales);
			this.tabMisc.Controls.Add(this.label56);
			this.tabMisc.Controls.Add(this.cboMaxTotalFemales);
			this.tabMisc.Controls.Add(this.label55);
			this.tabMisc.Controls.Add(this.cboTotalFinished);
			this.tabMisc.Controls.Add(this.label50);
			this.tabMisc.Controls.Add(this.cboTotalFinishing);
			this.tabMisc.Controls.Add(this.label49);
			this.tabMisc.Controls.Add(this.cboTotalNaked);
			this.tabMisc.Controls.Add(this.label48);
			this.tabMisc.Controls.Add(this.cboTotalExposed);
			this.tabMisc.Controls.Add(this.label47);
			this.tabMisc.Controls.Add(this.cboTotalPlaying);
			this.tabMisc.Controls.Add(this.label46);
			this.tabMisc.Controls.Add(this.label37);
			this.tabMisc.Controls.Add(this.label32);
			this.tabMisc.Controls.Add(this.label31);
			this.tabMisc.Controls.Add(this.cboTotalMales);
			this.tabMisc.Controls.Add(this.cboTotalFemales);
			this.tabMisc.Location = new System.Drawing.Point(4, 22);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(662, 185);
			this.tabMisc.TabIndex = 2;
			this.tabMisc.Text = "Misc";
			this.tabMisc.UseVisualStyleBackColor = true;
			// 
			// valMaxGameRounds
			// 
			this.valMaxGameRounds.Location = new System.Drawing.Point(200, 46);
			this.valMaxGameRounds.Name = "valMaxGameRounds";
			this.valMaxGameRounds.Size = new System.Drawing.Size(54, 20);
			this.valMaxGameRounds.TabIndex = 6;
			// 
			// label64
			// 
			this.label64.AutoSize = true;
			this.label64.Location = new System.Drawing.Point(175, 48);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(19, 13);
			this.label64.TabIndex = 59;
			this.label64.Text = "to:";
			// 
			// valGameRounds
			// 
			this.valGameRounds.Location = new System.Drawing.Point(115, 46);
			this.valGameRounds.Name = "valGameRounds";
			this.valGameRounds.Size = new System.Drawing.Size(54, 20);
			this.valGameRounds.TabIndex = 5;
			// 
			// label65
			// 
			this.label65.AutoSize = true;
			this.label65.Location = new System.Drawing.Point(8, 48);
			this.label65.Name = "label65";
			this.label65.Size = new System.Drawing.Size(73, 13);
			this.label65.TabIndex = 58;
			this.label65.Text = "Game rounds:";
			// 
			// cboMaxTotalFinished
			// 
			this.cboMaxTotalFinished.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaxTotalFinished.FormattingEnabled = true;
			this.cboMaxTotalFinished.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMaxTotalFinished.Location = new System.Drawing.Point(519, 97);
			this.cboMaxTotalFinished.Name = "cboMaxTotalFinished";
			this.cboMaxTotalFinished.Size = new System.Drawing.Size(38, 21);
			this.cboMaxTotalFinished.TabIndex = 18;
			// 
			// label62
			// 
			this.label62.AutoSize = true;
			this.label62.Location = new System.Drawing.Point(494, 100);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(19, 13);
			this.label62.TabIndex = 53;
			this.label62.Text = "to:";
			// 
			// cboMaxTotalFinishing
			// 
			this.cboMaxTotalFinishing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaxTotalFinishing.FormattingEnabled = true;
			this.cboMaxTotalFinishing.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMaxTotalFinishing.Location = new System.Drawing.Point(410, 97);
			this.cboMaxTotalFinishing.Name = "cboMaxTotalFinishing";
			this.cboMaxTotalFinishing.Size = new System.Drawing.Size(38, 21);
			this.cboMaxTotalFinishing.TabIndex = 16;
			// 
			// label61
			// 
			this.label61.AutoSize = true;
			this.label61.Location = new System.Drawing.Point(385, 100);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(19, 13);
			this.label61.TabIndex = 51;
			this.label61.Text = "to:";
			// 
			// cboMaxTotalNaked
			// 
			this.cboMaxTotalNaked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaxTotalNaked.FormattingEnabled = true;
			this.cboMaxTotalNaked.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMaxTotalNaked.Location = new System.Drawing.Point(279, 97);
			this.cboMaxTotalNaked.Name = "cboMaxTotalNaked";
			this.cboMaxTotalNaked.Size = new System.Drawing.Size(38, 21);
			this.cboMaxTotalNaked.TabIndex = 14;
			// 
			// label60
			// 
			this.label60.AutoSize = true;
			this.label60.Location = new System.Drawing.Point(254, 100);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(19, 13);
			this.label60.TabIndex = 49;
			this.label60.Text = "to:";
			// 
			// cboMaxTotalExposed
			// 
			this.cboMaxTotalExposed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaxTotalExposed.FormattingEnabled = true;
			this.cboMaxTotalExposed.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMaxTotalExposed.Location = new System.Drawing.Point(177, 97);
			this.cboMaxTotalExposed.Name = "cboMaxTotalExposed";
			this.cboMaxTotalExposed.Size = new System.Drawing.Size(38, 21);
			this.cboMaxTotalExposed.TabIndex = 12;
			// 
			// label59
			// 
			this.label59.AutoSize = true;
			this.label59.Location = new System.Drawing.Point(152, 100);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(19, 13);
			this.label59.TabIndex = 47;
			this.label59.Text = "to:";
			// 
			// cboMaxTotalPlaying
			// 
			this.cboMaxTotalPlaying.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaxTotalPlaying.FormattingEnabled = true;
			this.cboMaxTotalPlaying.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMaxTotalPlaying.Location = new System.Drawing.Point(68, 97);
			this.cboMaxTotalPlaying.Name = "cboMaxTotalPlaying";
			this.cboMaxTotalPlaying.Size = new System.Drawing.Size(38, 21);
			this.cboMaxTotalPlaying.TabIndex = 10;
			// 
			// label58
			// 
			this.label58.AutoSize = true;
			this.label58.Location = new System.Drawing.Point(43, 100);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(19, 13);
			this.label58.TabIndex = 45;
			this.label58.Text = "to:";
			// 
			// cboMaxTotalMales
			// 
			this.cboMaxTotalMales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaxTotalMales.FormattingEnabled = true;
			this.cboMaxTotalMales.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMaxTotalMales.Location = new System.Drawing.Point(435, 19);
			this.cboMaxTotalMales.Name = "cboMaxTotalMales";
			this.cboMaxTotalMales.Size = new System.Drawing.Size(54, 21);
			this.cboMaxTotalMales.TabIndex = 4;
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.Location = new System.Drawing.Point(410, 22);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(19, 13);
			this.label56.TabIndex = 41;
			this.label56.Text = "to:";
			// 
			// cboMaxTotalFemales
			// 
			this.cboMaxTotalFemales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaxTotalFemales.FormattingEnabled = true;
			this.cboMaxTotalFemales.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMaxTotalFemales.Location = new System.Drawing.Point(200, 19);
			this.cboMaxTotalFemales.Name = "cboMaxTotalFemales";
			this.cboMaxTotalFemales.Size = new System.Drawing.Size(54, 21);
			this.cboMaxTotalFemales.TabIndex = 2;
			// 
			// label55
			// 
			this.label55.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label55.AutoSize = true;
			this.label55.Location = new System.Drawing.Point(175, 22);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(19, 13);
			this.label55.TabIndex = 39;
			this.label55.Text = "to:";
			// 
			// cboTotalFinished
			// 
			this.cboTotalFinished.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTotalFinished.FormattingEnabled = true;
			this.cboTotalFinished.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboTotalFinished.Location = new System.Drawing.Point(519, 73);
			this.cboTotalFinished.Name = "cboTotalFinished";
			this.cboTotalFinished.Size = new System.Drawing.Size(38, 21);
			this.cboTotalFinished.TabIndex = 17;
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.Location = new System.Drawing.Point(454, 76);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(59, 13);
			this.label50.TabIndex = 35;
			this.label50.Text = "# Finished:";
			// 
			// cboTotalFinishing
			// 
			this.cboTotalFinishing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTotalFinishing.FormattingEnabled = true;
			this.cboTotalFinishing.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboTotalFinishing.Location = new System.Drawing.Point(410, 73);
			this.cboTotalFinishing.Name = "cboTotalFinishing";
			this.cboTotalFinishing.Size = new System.Drawing.Size(38, 21);
			this.cboTotalFinishing.TabIndex = 15;
			// 
			// label49
			// 
			this.label49.AutoSize = true;
			this.label49.Location = new System.Drawing.Point(323, 76);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(81, 13);
			this.label49.TabIndex = 33;
			this.label49.Text = "# Masturbating:";
			// 
			// cboTotalNaked
			// 
			this.cboTotalNaked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTotalNaked.FormattingEnabled = true;
			this.cboTotalNaked.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboTotalNaked.Location = new System.Drawing.Point(279, 73);
			this.cboTotalNaked.Name = "cboTotalNaked";
			this.cboTotalNaked.Size = new System.Drawing.Size(38, 21);
			this.cboTotalNaked.TabIndex = 13;
			// 
			// label48
			// 
			this.label48.AutoSize = true;
			this.label48.Location = new System.Drawing.Point(221, 76);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(52, 13);
			this.label48.TabIndex = 31;
			this.label48.Text = "# Naked:";
			// 
			// cboTotalExposed
			// 
			this.cboTotalExposed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTotalExposed.FormattingEnabled = true;
			this.cboTotalExposed.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboTotalExposed.Location = new System.Drawing.Point(177, 73);
			this.cboTotalExposed.Name = "cboTotalExposed";
			this.cboTotalExposed.Size = new System.Drawing.Size(38, 21);
			this.cboTotalExposed.TabIndex = 11;
			// 
			// label47
			// 
			this.label47.AutoSize = true;
			this.label47.Location = new System.Drawing.Point(110, 76);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(61, 13);
			this.label47.TabIndex = 29;
			this.label47.Text = "# Exposed:";
			// 
			// cboTotalPlaying
			// 
			this.cboTotalPlaying.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTotalPlaying.FormattingEnabled = true;
			this.cboTotalPlaying.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboTotalPlaying.Location = new System.Drawing.Point(68, 73);
			this.cboTotalPlaying.Name = "cboTotalPlaying";
			this.cboTotalPlaying.Size = new System.Drawing.Size(38, 21);
			this.cboTotalPlaying.TabIndex = 9;
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(8, 76);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(54, 13);
			this.label46.TabIndex = 27;
			this.label46.Text = "# Playing:";
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
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(276, 22);
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
			this.label31.Location = new System.Drawing.Point(6, 22);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(73, 13);
			this.label31.TabIndex = 22;
			this.label31.Text = "Total females:";
			// 
			// cboTotalMales
			// 
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
			this.cboTotalMales.Location = new System.Drawing.Point(350, 19);
			this.cboTotalMales.Name = "cboTotalMales";
			this.cboTotalMales.Size = new System.Drawing.Size(54, 21);
			this.cboTotalMales.TabIndex = 3;
			// 
			// cboTotalFemales
			// 
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
			this.cboTotalFemales.Location = new System.Drawing.Point(115, 19);
			this.cboTotalFemales.Name = "cboTotalFemales";
			this.cboTotalFemales.Size = new System.Drawing.Size(54, 21);
			this.cboTotalFemales.TabIndex = 1;
			// 
			// triggerMenu
			// 
			this.triggerMenu.Name = "triggerMenu";
			this.triggerMenu.ShowImageMargin = false;
			this.triggerMenu.Size = new System.Drawing.Size(36, 4);
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tabDialogue);
			this.tabs.Controls.Add(this.tabNotes);
			this.tabs.Location = new System.Drawing.Point(0, 420);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(697, 251);
			this.tabs.TabIndex = 95;
			// 
			// tabDialogue
			// 
			this.tabDialogue.Controls.Add(this.lblAvailableVars);
			this.tabDialogue.Controls.Add(this.cmdCopyAll);
			this.tabDialogue.Controls.Add(this.ckbShowBubbleColumns);
			this.tabDialogue.Controls.Add(this.cmdPasteAll);
			this.tabDialogue.Controls.Add(this.gridDialogue);
			this.tabDialogue.Location = new System.Drawing.Point(4, 22);
			this.tabDialogue.Name = "tabDialogue";
			this.tabDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tabDialogue.Size = new System.Drawing.Size(689, 225);
			this.tabDialogue.TabIndex = 0;
			this.tabDialogue.Text = "Dialogue";
			this.tabDialogue.UseVisualStyleBackColor = true;
			// 
			// tabNotes
			// 
			this.tabNotes.Controls.Add(this.txtNotes);
			this.tabNotes.Controls.Add(this.label1);
			this.tabNotes.Location = new System.Drawing.Point(4, 22);
			this.tabNotes.Name = "tabNotes";
			this.tabNotes.Padding = new System.Windows.Forms.Padding(3);
			this.tabNotes.Size = new System.Drawing.Size(689, 225);
			this.tabNotes.TabIndex = 1;
			this.tabNotes.Text = "Notes";
			this.tabNotes.UseVisualStyleBackColor = true;
			// 
			// treeDialogue
			// 
			this.treeDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeDialogue.Location = new System.Drawing.Point(3, 3);
			this.treeDialogue.Name = "treeDialogue";
			this.treeDialogue.Size = new System.Drawing.Size(259, 665);
			this.treeDialogue.TabIndex = 40;
			this.treeDialogue.SelectedNodeChanging += new System.EventHandler<SPNATI_Character_Editor.Controls.CaseSelectionEventArgs>(this.tree_SelectedNodeChanging);
			this.treeDialogue.SelectedNodeChanged += new System.EventHandler<SPNATI_Character_Editor.Controls.CaseSelectionEventArgs>(this.tree_SelectedCaseChanged);
			this.treeDialogue.CreatingCase += new System.EventHandler<SPNATI_Character_Editor.Controls.CaseCreationEventArgs>(this.tree_CreatingCase);
			this.treeDialogue.CreatedCase += new System.EventHandler<SPNATI_Character_Editor.Controls.CaseCreationEventArgs>(this.tree_CreatedCase);
			// 
			// gridDialogue
			// 
			this.gridDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridDialogue.Location = new System.Drawing.Point(3, 32);
			this.gridDialogue.Name = "gridDialogue";
			this.gridDialogue.ReadOnly = false;
			this.gridDialogue.Size = new System.Drawing.Size(680, 336);
			this.gridDialogue.TabIndex = 42;
			this.gridDialogue.KeyDown += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.gridDialogue_KeyDown);
			this.gridDialogue.HighlightRow += new System.EventHandler<int>(this.gridDialogue_HighlightRow);
			// 
			// markerTarget
			// 
			this.markerTarget.Location = new System.Drawing.Point(81, 154);
			this.markerTarget.Name = "markerTarget";
			this.markerTarget.Size = new System.Drawing.Size(242, 23);
			this.markerTarget.TabIndex = 94;
			this.markerTarget.Value = null;
			// 
			// markerAlsoPlaying
			// 
			this.markerAlsoPlaying.Location = new System.Drawing.Point(99, 126);
			this.markerAlsoPlaying.Name = "markerAlsoPlaying";
			this.markerAlsoPlaying.Size = new System.Drawing.Size(226, 23);
			this.markerAlsoPlaying.TabIndex = 46;
			this.markerAlsoPlaying.Value = null;
			// 
			// markerSelf
			// 
			this.markerSelf.Location = new System.Drawing.Point(115, 45);
			this.markerSelf.Name = "markerSelf";
			this.markerSelf.Size = new System.Drawing.Size(200, 23);
			this.markerSelf.TabIndex = 68;
			this.markerSelf.Value = null;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(413, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Jot down any personal notes about this case here. These notes won\'t appear in gam" +
    "e.";
			// 
			// txtNotes
			// 
			this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNotes.Location = new System.Drawing.Point(5, 19);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(678, 206);
			this.txtNotes.TabIndex = 1;
			this.txtNotes.Validated += new System.EventHandler(this.txtNotes_Validated);
			// 
			// DialogueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitDialogue);
			this.Name = "DialogueEditor";
			this.Size = new System.Drawing.Size(973, 671);
			this.splitDialogue.Panel1.ResumeLayout(false);
			this.splitDialogue.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitDialogue)).EndInit();
			this.splitDialogue.ResumeLayout(false);
			this.grpCase.ResumeLayout(false);
			this.grpCase.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.grpConditions.ResumeLayout(false);
			this.grpConditions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valPriority)).EndInit();
			this.tabControlConditions.ResumeLayout(false);
			this.tabTarget.ResumeLayout(false);
			this.tabTarget.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxStartingLayers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valStartingLayers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxLayers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valLayers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxTimeInStage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxLosses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTimeInStage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valLosses)).EndInit();
			this.tabOther.ResumeLayout(false);
			this.tabOther.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxAlsoTimeInStage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valAlsoTimeInStage)).EndInit();
			this.tabConditions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridFilters)).EndInit();
			this.tabSelf.ResumeLayout(false);
			this.tabSelf.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxOwnTimeInStage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valMaxOwnLosses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valOwnLosses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valOwnTimeInStage)).EndInit();
			this.tabMisc.ResumeLayout(false);
			this.tabMisc.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valMaxGameRounds)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valGameRounds)).EndInit();
			this.tabs.ResumeLayout(false);
			this.tabDialogue.ResumeLayout(false);
			this.tabDialogue.PerformLayout();
			this.tabNotes.ResumeLayout(false);
			this.tabNotes.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitDialogue;
		private System.Windows.Forms.GroupBox grpCase;
		private System.Windows.Forms.CheckBox ckbShowBubbleColumns;
		private Controls.DialogueGrid gridDialogue;
		private System.Windows.Forms.Label lblHelpText;
		private System.Windows.Forms.Button cmdPasteAll;
		private System.Windows.Forms.Button cmdCopyAll;
		private System.Windows.Forms.ComboBox cboCaseTags;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label lblAvailableVars;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.FlowLayoutPanel flowStageChecks;
		private System.Windows.Forms.GroupBox grpConditions;
		private System.Windows.Forms.TabControl tabControlConditions;
		private System.Windows.Forms.TabPage tabTarget;
		private System.Windows.Forms.ComboBox cboTargetNotMarker;
		private System.Windows.Forms.Label label72;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.NumericUpDown valMaxTimeInStage;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.NumericUpDown valMaxLosses;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.NumericUpDown valTimeInStage;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.NumericUpDown valLosses;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.ComboBox cboTargetToStage;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox cboTargetHand;
		private System.Windows.Forms.ComboBox cboLineTarget;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.ComboBox cboTargetStage;
		private System.Windows.Forms.ComboBox cboLineFilter;
		private System.Windows.Forms.TabPage tabOther;
		private System.Windows.Forms.ComboBox cboAlsoPlayingNotMarker;
		private System.Windows.Forms.Label label71;
		private System.Windows.Forms.Label label67;
		private System.Windows.Forms.NumericUpDown valMaxAlsoTimeInStage;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.NumericUpDown valAlsoTimeInStage;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.ComboBox cboAlsoPlayingMaxStage;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.ComboBox cboAlsoPlaying;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.ComboBox cboAlsoPlayingStage;
		private System.Windows.Forms.ComboBox cboAlsoPlayingHand;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TabPage tabConditions;
		private System.Windows.Forms.DataGridView gridFilters;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColTagFilter;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColGenderFilter;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColStatusFilterNegated;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColStatusFilter;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColFilterCount;
		private System.Windows.Forms.TabPage tabSelf;
		private System.Windows.Forms.ComboBox cboNotMarker;
		private System.Windows.Forms.Label label70;
		private System.Windows.Forms.ComboBox cboOwnHand;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label69;
		private System.Windows.Forms.NumericUpDown valMaxOwnTimeInStage;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.NumericUpDown valMaxOwnLosses;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.NumericUpDown valOwnLosses;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.NumericUpDown valOwnTimeInStage;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label68;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.NumericUpDown valPriority;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.NumericUpDown valMaxGameRounds;
		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.NumericUpDown valGameRounds;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.ComboBox cboMaxTotalFinished;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.ComboBox cboMaxTotalFinishing;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.ComboBox cboMaxTotalNaked;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.ComboBox cboMaxTotalExposed;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.ComboBox cboMaxTotalPlaying;
		private System.Windows.Forms.Label label58;
		private System.Windows.Forms.ComboBox cboMaxTotalMales;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.ComboBox cboMaxTotalFemales;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.ComboBox cboTotalFinished;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.ComboBox cboTotalFinishing;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.ComboBox cboTotalNaked;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.ComboBox cboTotalExposed;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.ComboBox cboTotalPlaying;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox cboTotalMales;
		private System.Windows.Forms.ComboBox cboTotalFemales;
		private System.Windows.Forms.ContextMenuStrip triggerMenu;
		private System.Windows.Forms.NumericUpDown valMaxLayers;
		private System.Windows.Forms.Label label74;
		private System.Windows.Forms.NumericUpDown valLayers;
		private System.Windows.Forms.Label lblTargetLayers;
		private System.Windows.Forms.ComboBox cboTargetStatus;
		private System.Windows.Forms.Label label75;
		private System.Windows.Forms.CheckBox ckbTargetStatusNegated;
		private System.Windows.Forms.NumericUpDown valMaxStartingLayers;
		private System.Windows.Forms.NumericUpDown valStartingLayers;
		private System.Windows.Forms.Label lblTargetStartingLayers;
		private System.Windows.Forms.Label label76;
		private Controls.MarkerConditionField markerSelf;
		private Controls.MarkerConditionField markerTarget;
		private Controls.MarkerConditionField markerAlsoPlaying;
		private System.Windows.Forms.Button cmdCallOut;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.CommonControls.PropertyTable tableConditions;
		private System.Windows.Forms.Button cmdToggleMode;
		private System.Windows.Forms.Button cmdMakeResponse;
		private Controls.DialogueTree treeDialogue;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabDialogue;
		private System.Windows.Forms.TabPage tabNotes;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.Label label1;
	}
}

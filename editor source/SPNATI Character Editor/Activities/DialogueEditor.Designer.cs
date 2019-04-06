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
			this.triggerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdMakeResponse = new System.Windows.Forms.Button();
			this.cmdCallOut = new System.Windows.Forms.Button();
			this.splitDialogue = new System.Windows.Forms.SplitContainer();
			this.panelCase = new System.Windows.Forms.Panel();
			this.lblHelpText = new System.Windows.Forms.Label();
			this.tabCase = new System.Windows.Forms.TabControl();
			this.tabConditions = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.flowStageChecks = new System.Windows.Forms.FlowLayoutPanel();
			this.grpConditions = new System.Windows.Forms.GroupBox();
			this.valPriority = new System.Windows.Forms.NumericUpDown();
			this.label73 = new System.Windows.Forms.Label();
			this.tableConditions = new Desktop.CommonControls.PropertyTable();
			this.tabTags = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabDialogue = new System.Windows.Forms.TabPage();
			this.lblAvailableVars = new System.Windows.Forms.Label();
			this.cmdCopyAll = new System.Windows.Forms.Button();
			this.ckbShowAdvanced = new System.Windows.Forms.CheckBox();
			this.cmdPasteAll = new System.Windows.Forms.Button();
			this.tabNotes = new System.Windows.Forms.TabPage();
			this.txtNotes = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboCaseTags = new System.Windows.Forms.ComboBox();
			this.label34 = new System.Windows.Forms.Label();
			this.splitCase = new System.Windows.Forms.SplitContainer();
			this.treeDialogue = new SPNATI_Character_Editor.Controls.DialogueTree();
			this.lstRemoveTags = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.lstAddTags = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.gridDialogue = new SPNATI_Character_Editor.Controls.DialogueGrid();
			((System.ComponentModel.ISupportInitialize)(this.splitDialogue)).BeginInit();
			this.splitDialogue.Panel1.SuspendLayout();
			this.splitDialogue.Panel2.SuspendLayout();
			this.splitDialogue.SuspendLayout();
			this.panelCase.SuspendLayout();
			this.tabCase.SuspendLayout();
			this.tabConditions.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.grpConditions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valPriority)).BeginInit();
			this.tabTags.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabDialogue.SuspendLayout();
			this.tabNotes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitCase)).BeginInit();
			this.splitCase.Panel1.SuspendLayout();
			this.splitCase.Panel2.SuspendLayout();
			this.splitCase.SuspendLayout();
			this.SuspendLayout();
			// 
			// triggerMenu
			// 
			this.triggerMenu.Name = "triggerMenu";
			this.triggerMenu.ShowImageMargin = false;
			this.triggerMenu.Size = new System.Drawing.Size(36, 4);
			// 
			// cmdMakeResponse
			// 
			this.cmdMakeResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdMakeResponse.Location = new System.Drawing.Point(508, 3);
			this.cmdMakeResponse.Name = "cmdMakeResponse";
			this.cmdMakeResponse.Size = new System.Drawing.Size(104, 23);
			this.cmdMakeResponse.TabIndex = 45;
			this.cmdMakeResponse.Text = "Respond to This...";
			this.toolTip1.SetToolTip(this.cmdMakeResponse, "Creates a response to this case on another character");
			this.cmdMakeResponse.UseVisualStyleBackColor = true;
			this.cmdMakeResponse.Click += new System.EventHandler(this.cmdMakeResponse_Click);
			// 
			// cmdCallOut
			// 
			this.cmdCallOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCallOut.Location = new System.Drawing.Point(618, 3);
			this.cmdCallOut.Name = "cmdCallOut";
			this.cmdCallOut.Size = new System.Drawing.Size(75, 23);
			this.cmdCallOut.TabIndex = 44;
			this.cmdCallOut.Text = "Call Out...";
			this.toolTip1.SetToolTip(this.cmdCallOut, "Marks this situation as being \"noteworthy\" so it will appear in other character\'s" +
        " Writing Aids.");
			this.cmdCallOut.UseVisualStyleBackColor = true;
			this.cmdCallOut.Click += new System.EventHandler(this.cmdCallOut_Click);
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
			this.splitDialogue.Panel2.Controls.Add(this.panelCase);
			this.splitDialogue.Size = new System.Drawing.Size(973, 671);
			this.splitDialogue.SplitterDistance = 266;
			this.splitDialogue.TabIndex = 16;
			// 
			// panelCase
			// 
			this.panelCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelCase.BackColor = System.Drawing.SystemColors.Control;
			this.panelCase.Controls.Add(this.splitCase);
			this.panelCase.Controls.Add(this.lblHelpText);
			this.panelCase.Controls.Add(this.cmdMakeResponse);
			this.panelCase.Controls.Add(this.cmdCallOut);
			this.panelCase.Controls.Add(this.cboCaseTags);
			this.panelCase.Controls.Add(this.label34);
			this.panelCase.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelCase.Location = new System.Drawing.Point(3, 0);
			this.panelCase.Name = "panelCase";
			this.panelCase.Size = new System.Drawing.Size(697, 668);
			this.panelCase.TabIndex = 28;
			// 
			// lblHelpText
			// 
			this.lblHelpText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHelpText.Location = new System.Drawing.Point(222, 5);
			this.lblHelpText.Name = "lblHelpText";
			this.lblHelpText.Size = new System.Drawing.Size(278, 38);
			this.lblHelpText.TabIndex = 38;
			this.lblHelpText.Text = "Help Text";
			// 
			// tabCase
			// 
			this.tabCase.Controls.Add(this.tabConditions);
			this.tabCase.Controls.Add(this.tabTags);
			this.tabCase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabCase.Location = new System.Drawing.Point(0, 0);
			this.tabCase.Name = "tabCase";
			this.tabCase.SelectedIndex = 0;
			this.tabCase.Size = new System.Drawing.Size(697, 366);
			this.tabCase.TabIndex = 96;
			// 
			// tabConditions
			// 
			this.tabConditions.Controls.Add(this.groupBox3);
			this.tabConditions.Controls.Add(this.grpConditions);
			this.tabConditions.Location = new System.Drawing.Point(4, 22);
			this.tabConditions.Name = "tabConditions";
			this.tabConditions.Padding = new System.Windows.Forms.Padding(3);
			this.tabConditions.Size = new System.Drawing.Size(689, 340);
			this.tabConditions.TabIndex = 0;
			this.tabConditions.Text = "Conditions";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.chkSelectAll);
			this.groupBox3.Controls.Add(this.flowStageChecks);
			this.groupBox3.Location = new System.Drawing.Point(6, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(677, 119);
			this.groupBox3.TabIndex = 37;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Applies to Stages";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.BackColor = System.Drawing.Color.White;
			this.chkSelectAll.Location = new System.Drawing.Point(604, -1);
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
			this.flowStageChecks.Size = new System.Drawing.Size(671, 100);
			this.flowStageChecks.TabIndex = 0;
			// 
			// grpConditions
			// 
			this.grpConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpConditions.Controls.Add(this.valPriority);
			this.grpConditions.Controls.Add(this.label73);
			this.grpConditions.Controls.Add(this.tableConditions);
			this.grpConditions.Location = new System.Drawing.Point(6, 128);
			this.grpConditions.Name = "grpConditions";
			this.grpConditions.Size = new System.Drawing.Size(674, 206);
			this.grpConditions.TabIndex = 38;
			this.grpConditions.TabStop = false;
			this.grpConditions.Text = "Conditions";
			// 
			// valPriority
			// 
			this.valPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valPriority.Location = new System.Drawing.Point(610, 20);
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
			this.label73.Location = new System.Drawing.Point(565, 23);
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
			this.tableConditions.AllowMacros = true;
			this.tableConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableConditions.Data = null;
			this.tableConditions.HideAddField = false;
			this.tableConditions.HideSpeedButtons = false;
			this.tableConditions.Location = new System.Drawing.Point(6, 19);
			this.tableConditions.Name = "tableConditions";
			this.tableConditions.PlaceholderText = "Add a condition";
			this.tableConditions.PreserveControls = false;
			this.tableConditions.PreviewData = null;
			this.tableConditions.RemoveCaption = "Remove condition";
			this.tableConditions.RowHeaderWidth = 0F;
			this.tableConditions.RunInitialAddEvents = true;
			this.tableConditions.Size = new System.Drawing.Size(662, 181);
			this.tableConditions.Sorted = false;
			this.tableConditions.TabIndex = 31;
			this.tableConditions.UndoManager = null;
			this.tableConditions.UseAutoComplete = true;
			this.tableConditions.EditingMacro += new System.EventHandler<Desktop.CommonControls.MacroArgs>(this.tableConditions_EditingMacro);
			this.tableConditions.MacroChanged += new System.EventHandler<Desktop.Macro>(this.tableConditions_MacroChanged);
			// 
			// tabTags
			// 
			this.tabTags.Controls.Add(this.lstRemoveTags);
			this.tabTags.Controls.Add(this.lstAddTags);
			this.tabTags.Controls.Add(this.label3);
			this.tabTags.Controls.Add(this.label2);
			this.tabTags.Location = new System.Drawing.Point(4, 22);
			this.tabTags.Name = "tabTags";
			this.tabTags.Padding = new System.Windows.Forms.Padding(3);
			this.tabTags.Size = new System.Drawing.Size(689, 353);
			this.tabTags.TabIndex = 1;
			this.tabTags.Text = "Tags";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(345, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(158, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Tags to Remove with This Case";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(137, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Tags to Add with This Case";
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabDialogue);
			this.tabs.Controls.Add(this.tabNotes);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Margin = new System.Windows.Forms.Padding(0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(697, 273);
			this.tabs.TabIndex = 95;
			// 
			// tabDialogue
			// 
			this.tabDialogue.Controls.Add(this.lblAvailableVars);
			this.tabDialogue.Controls.Add(this.cmdCopyAll);
			this.tabDialogue.Controls.Add(this.ckbShowAdvanced);
			this.tabDialogue.Controls.Add(this.cmdPasteAll);
			this.tabDialogue.Controls.Add(this.gridDialogue);
			this.tabDialogue.Location = new System.Drawing.Point(4, 22);
			this.tabDialogue.Name = "tabDialogue";
			this.tabDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tabDialogue.Size = new System.Drawing.Size(689, 247);
			this.tabDialogue.TabIndex = 0;
			this.tabDialogue.Text = "Dialogue";
			this.tabDialogue.UseVisualStyleBackColor = true;
			// 
			// lblAvailableVars
			// 
			this.lblAvailableVars.AutoSize = true;
			this.lblAvailableVars.Location = new System.Drawing.Point(2, 8);
			this.lblAvailableVars.Name = "lblAvailableVars";
			this.lblAvailableVars.Size = new System.Drawing.Size(158, 13);
			this.lblAvailableVars.TabIndex = 32;
			this.lblAvailableVars.Text = "Hover to see available variables";
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
			// ckbShowAdvanced
			// 
			this.ckbShowAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ckbShowAdvanced.AutoSize = true;
			this.ckbShowAdvanced.Location = new System.Drawing.Point(417, 6);
			this.ckbShowAdvanced.Name = "ckbShowAdvanced";
			this.ckbShowAdvanced.Size = new System.Drawing.Size(104, 17);
			this.ckbShowAdvanced.TabIndex = 43;
			this.ckbShowAdvanced.Text = "Show advanced";
			this.ckbShowAdvanced.UseVisualStyleBackColor = true;
			this.ckbShowAdvanced.CheckedChanged += new System.EventHandler(this.ckbShowAdvanced_CheckedChanged);
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
			// tabNotes
			// 
			this.tabNotes.Controls.Add(this.txtNotes);
			this.tabNotes.Controls.Add(this.label1);
			this.tabNotes.Location = new System.Drawing.Point(4, 22);
			this.tabNotes.Name = "tabNotes";
			this.tabNotes.Padding = new System.Windows.Forms.Padding(3);
			this.tabNotes.Size = new System.Drawing.Size(689, 234);
			this.tabNotes.TabIndex = 1;
			this.tabNotes.Text = "Notes";
			this.tabNotes.UseVisualStyleBackColor = true;
			// 
			// txtNotes
			// 
			this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNotes.Location = new System.Drawing.Point(5, 19);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(678, 215);
			this.txtNotes.TabIndex = 1;
			this.txtNotes.Validated += new System.EventHandler(this.txtNotes_Validated);
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
			// cboCaseTags
			// 
			this.cboCaseTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCaseTags.FormattingEnabled = true;
			this.cboCaseTags.Location = new System.Drawing.Point(41, 5);
			this.cboCaseTags.Name = "cboCaseTags";
			this.cboCaseTags.Size = new System.Drawing.Size(175, 21);
			this.cboCaseTags.TabIndex = 35;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(6, 8);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(34, 13);
			this.label34.TabIndex = 34;
			this.label34.Text = "Type:";
			// 
			// splitCase
			// 
			this.splitCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitCase.Location = new System.Drawing.Point(0, 28);
			this.splitCase.Name = "splitCase";
			this.splitCase.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitCase.Panel1
			// 
			this.splitCase.Panel1.Controls.Add(this.tabCase);
			// 
			// splitCase.Panel2
			// 
			this.splitCase.Panel2.Controls.Add(this.tabs);
			this.splitCase.Size = new System.Drawing.Size(697, 643);
			this.splitCase.SplitterDistance = 366;
			this.splitCase.TabIndex = 61;
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
			// lstRemoveTags
			// 
			this.lstRemoveTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstRemoveTags.Location = new System.Drawing.Point(348, 19);
			this.lstRemoveTags.Name = "lstRemoveTags";
			this.lstRemoveTags.RecordType = null;
			this.lstRemoveTags.SelectedItems = new string[0];
			this.lstRemoveTags.Size = new System.Drawing.Size(335, 328);
			this.lstRemoveTags.TabIndex = 3;
			// 
			// lstAddTags
			// 
			this.lstAddTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstAddTags.Location = new System.Drawing.Point(6, 19);
			this.lstAddTags.Name = "lstAddTags";
			this.lstAddTags.RecordType = null;
			this.lstAddTags.SelectedItems = new string[0];
			this.lstAddTags.Size = new System.Drawing.Size(336, 328);
			this.lstAddTags.TabIndex = 2;
			// 
			// gridDialogue
			// 
			this.gridDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridDialogue.Location = new System.Drawing.Point(3, 32);
			this.gridDialogue.Name = "gridDialogue";
			this.gridDialogue.ReadOnly = false;
			this.gridDialogue.Size = new System.Drawing.Size(680, 212);
			this.gridDialogue.TabIndex = 42;
			this.gridDialogue.KeyDown += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.gridDialogue_KeyDown);
			this.gridDialogue.HighlightRow += new System.EventHandler<int>(this.gridDialogue_HighlightRow);
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
			this.panelCase.ResumeLayout(false);
			this.panelCase.PerformLayout();
			this.tabCase.ResumeLayout(false);
			this.tabConditions.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.grpConditions.ResumeLayout(false);
			this.grpConditions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valPriority)).EndInit();
			this.tabTags.ResumeLayout(false);
			this.tabTags.PerformLayout();
			this.tabs.ResumeLayout(false);
			this.tabDialogue.ResumeLayout(false);
			this.tabDialogue.PerformLayout();
			this.tabNotes.ResumeLayout(false);
			this.tabNotes.PerformLayout();
			this.splitCase.Panel1.ResumeLayout(false);
			this.splitCase.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitCase)).EndInit();
			this.splitCase.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitDialogue;
		private System.Windows.Forms.Panel panelCase;
		private System.Windows.Forms.CheckBox ckbShowAdvanced;
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
		private System.Windows.Forms.NumericUpDown valPriority;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.ContextMenuStrip triggerMenu;
		private System.Windows.Forms.Button cmdCallOut;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.CommonControls.PropertyTable tableConditions;
		private System.Windows.Forms.Button cmdMakeResponse;
		private Controls.DialogueTree treeDialogue;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabDialogue;
		private System.Windows.Forms.TabPage tabNotes;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabCase;
		private System.Windows.Forms.TabPage tabConditions;
		private System.Windows.Forms.TabPage tabTags;
		private RecordSelectBox lstRemoveTags;
		private RecordSelectBox lstAddTags;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SplitContainer splitCase;
	}
}

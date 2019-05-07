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
			this.lstRemoveTags = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.lstAddTags = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabDialogue = new System.Windows.Forms.TabPage();
			this.lblAvailableVars = new System.Windows.Forms.Label();
			this.cmdCopyAll = new System.Windows.Forms.Button();
			this.cmdPasteAll = new System.Windows.Forms.Button();
			this.gridDialogue = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.tabNotes = new System.Windows.Forms.TabPage();
			this.txtNotes = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblHelpText = new System.Windows.Forms.Label();
			this.cboCaseTags = new System.Windows.Forms.ComboBox();
			this.label34 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitCase)).BeginInit();
			this.splitCase.Panel1.SuspendLayout();
			this.splitCase.Panel2.SuspendLayout();
			this.splitCase.SuspendLayout();
			this.tabCase.SuspendLayout();
			this.tabConditions.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.grpConditions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valPriority)).BeginInit();
			this.tabTags.SuspendLayout();
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
			this.splitCase.Location = new System.Drawing.Point(0, 24);
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
			this.splitCase.Size = new System.Drawing.Size(670, 656);
			this.splitCase.SplitterDistance = 373;
			this.splitCase.TabIndex = 62;
			// 
			// tabCase
			// 
			this.tabCase.Controls.Add(this.tabConditions);
			this.tabCase.Controls.Add(this.tabTags);
			this.tabCase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabCase.Location = new System.Drawing.Point(0, 0);
			this.tabCase.Name = "tabCase";
			this.tabCase.SelectedIndex = 0;
			this.tabCase.Size = new System.Drawing.Size(670, 373);
			this.tabCase.TabIndex = 96;
			// 
			// tabConditions
			// 
			this.tabConditions.Controls.Add(this.groupBox3);
			this.tabConditions.Controls.Add(this.grpConditions);
			this.tabConditions.Location = new System.Drawing.Point(4, 22);
			this.tabConditions.Name = "tabConditions";
			this.tabConditions.Padding = new System.Windows.Forms.Padding(3);
			this.tabConditions.Size = new System.Drawing.Size(662, 347);
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
			this.groupBox3.Size = new System.Drawing.Size(650, 119);
			this.groupBox3.TabIndex = 37;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Applies to Stages";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.BackColor = System.Drawing.Color.White;
			this.chkSelectAll.Location = new System.Drawing.Point(577, -1);
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
			this.flowStageChecks.Size = new System.Drawing.Size(644, 100);
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
			this.grpConditions.Size = new System.Drawing.Size(647, 213);
			this.grpConditions.TabIndex = 38;
			this.grpConditions.TabStop = false;
			this.grpConditions.Text = "Conditions";
			// 
			// valPriority
			// 
			this.valPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valPriority.Location = new System.Drawing.Point(583, 20);
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
			this.label73.Location = new System.Drawing.Point(538, 23);
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
			this.tableConditions.Size = new System.Drawing.Size(635, 188);
			this.tableConditions.Sorted = false;
			this.tableConditions.TabIndex = 31;
			this.tableConditions.UndoManager = null;
			this.tableConditions.UseAutoComplete = true;
			this.tableConditions.EditingMacro += new System.EventHandler<Desktop.CommonControls.MacroArgs>(this.tableConditions_EditingMacro);
			this.tableConditions.MacroChanged += new System.EventHandler<Desktop.CommonControls.MacroArgs>(this.tableConditions_MacroChanged);
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
			this.tabTags.Size = new System.Drawing.Size(662, 347);
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
			this.lstRemoveTags.RecordType = null;
			this.lstRemoveTags.SelectedItems = new string[0];
			this.lstRemoveTags.Size = new System.Drawing.Size(308, 322);
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
			this.lstAddTags.Size = new System.Drawing.Size(336, 322);
			this.lstAddTags.TabIndex = 2;
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
			this.tabs.Size = new System.Drawing.Size(670, 279);
			this.tabs.TabIndex = 95;
			// 
			// tabDialogue
			// 
			this.tabDialogue.Controls.Add(this.lblAvailableVars);
			this.tabDialogue.Controls.Add(this.cmdCopyAll);
			this.tabDialogue.Controls.Add(this.cmdPasteAll);
			this.tabDialogue.Controls.Add(this.gridDialogue);
			this.tabDialogue.Location = new System.Drawing.Point(4, 22);
			this.tabDialogue.Name = "tabDialogue";
			this.tabDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tabDialogue.Size = new System.Drawing.Size(662, 253);
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
			this.cmdCopyAll.Location = new System.Drawing.Point(500, 3);
			this.cmdCopyAll.Name = "cmdCopyAll";
			this.cmdCopyAll.Size = new System.Drawing.Size(75, 23);
			this.cmdCopyAll.TabIndex = 39;
			this.cmdCopyAll.Text = "Copy All";
			this.cmdCopyAll.UseVisualStyleBackColor = true;
			this.cmdCopyAll.Click += new System.EventHandler(this.cmdCopyAll_Click);
			// 
			// cmdPasteAll
			// 
			this.cmdPasteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPasteAll.Location = new System.Drawing.Point(581, 3);
			this.cmdPasteAll.Name = "cmdPasteAll";
			this.cmdPasteAll.Size = new System.Drawing.Size(75, 23);
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
			this.gridDialogue.Size = new System.Drawing.Size(653, 218);
			this.gridDialogue.TabIndex = 42;
			this.gridDialogue.KeyDown += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.gridDialogue_KeyDown);
			this.gridDialogue.HighlightRow += new System.EventHandler<int>(this.gridDialogue_HighlightRow);
			// 
			// tabNotes
			// 
			this.tabNotes.Controls.Add(this.txtNotes);
			this.tabNotes.Controls.Add(this.label1);
			this.tabNotes.Location = new System.Drawing.Point(4, 22);
			this.tabNotes.Name = "tabNotes";
			this.tabNotes.Padding = new System.Windows.Forms.Padding(3);
			this.tabNotes.Size = new System.Drawing.Size(662, 253);
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
			this.txtNotes.Size = new System.Drawing.Size(651, 221);
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
			// lblHelpText
			// 
			this.lblHelpText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHelpText.Location = new System.Drawing.Point(217, 0);
			this.lblHelpText.Name = "lblHelpText";
			this.lblHelpText.Size = new System.Drawing.Size(278, 38);
			this.lblHelpText.TabIndex = 65;
			this.lblHelpText.Text = "Help Text";
			// 
			// cboCaseTags
			// 
			this.cboCaseTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCaseTags.FormattingEnabled = true;
			this.cboCaseTags.Location = new System.Drawing.Point(36, 0);
			this.cboCaseTags.Name = "cboCaseTags";
			this.cboCaseTags.Size = new System.Drawing.Size(175, 21);
			this.cboCaseTags.TabIndex = 64;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(1, 3);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(34, 13);
			this.label34.TabIndex = 63;
			this.label34.Text = "Type:";
			// 
			// CaseControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblHelpText);
			this.Controls.Add(this.cboCaseTags);
			this.Controls.Add(this.label34);
			this.Controls.Add(this.splitCase);
			this.Name = "CaseControl";
			this.Size = new System.Drawing.Size(670, 680);
			this.Load += new System.EventHandler(this.CaseControl_Load);
			this.splitCase.Panel1.ResumeLayout(false);
			this.splitCase.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitCase)).EndInit();
			this.splitCase.ResumeLayout(false);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitCase;
		private System.Windows.Forms.TabControl tabCase;
		private System.Windows.Forms.TabPage tabConditions;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox chkSelectAll;
		private System.Windows.Forms.FlowLayoutPanel flowStageChecks;
		private System.Windows.Forms.GroupBox grpConditions;
		private System.Windows.Forms.NumericUpDown valPriority;
		private System.Windows.Forms.Label label73;
		private Desktop.CommonControls.PropertyTable tableConditions;
		private System.Windows.Forms.TabPage tabTags;
		private RecordSelectBox lstRemoveTags;
		private RecordSelectBox lstAddTags;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabDialogue;
		private System.Windows.Forms.Label lblAvailableVars;
		private System.Windows.Forms.Button cmdCopyAll;
		private System.Windows.Forms.Button cmdPasteAll;
		private DialogueGrid gridDialogue;
		private System.Windows.Forms.TabPage tabNotes;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblHelpText;
		private System.Windows.Forms.ComboBox cboCaseTags;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

namespace SPNATI_Character_Editor.Activities
{
	partial class BanterWizard
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdFilter = new Desktop.Skinning.SkinnedButton();
            this.lstCharacters = new Desktop.Skinning.SkinnedListBox();
            this.lblCharacters = new Desktop.Skinning.SkinnedLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.grpLines = new Desktop.Skinning.SkinnedGroupBox();
            this.gridLines = new Desktop.Skinning.SkinnedDataGridView();
            this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNoMatches = new Desktop.Skinning.SkinnedLabel();
            this.cmdCreateResponse = new Desktop.Skinning.SkinnedButton();
            this.lblCaseInfo = new Desktop.Skinning.SkinnedLabel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.grpBaseLine = new Desktop.Skinning.SkinnedGroupBox();
            this.lstBasicLines = new Desktop.Skinning.SkinnedListBox();
            this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
            this.lblBasicText = new Desktop.Skinning.SkinnedLabel();
            this.grpResponse = new Desktop.Skinning.SkinnedGroupBox();
            this.cmdDiscard = new Desktop.Skinning.SkinnedButton();
            this.cmdAccept = new Desktop.Skinning.SkinnedButton();
            this.cmdJump = new Desktop.Skinning.SkinnedButton();
            this.gridResponse = new SPNATI_Character_Editor.Controls.DialogueGrid();
            this.ctlResponse = new SPNATI_Character_Editor.Controls.CaseControl();
            this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
            this.panelLoad = new Desktop.Skinning.SkinnedPanel();
            this.lblProgress = new Desktop.Skinning.SkinnedLabel();
            this.progress = new Desktop.Skinning.SkinnedProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grpLines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grpBaseLine.SuspendLayout();
            this.grpResponse.SuspendLayout();
            this.panelLoad.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdFilter);
            this.splitContainer1.Panel1.Controls.Add(this.lstCharacters);
            this.splitContainer1.Panel1.Controls.Add(this.lblCharacters);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.panelLoad);
            this.splitContainer1.Size = new System.Drawing.Size(1161, 674);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 1;
            // 
            // cmdFilter
            // 
            this.cmdFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFilter.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdFilter.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
            this.cmdFilter.Flat = false;
            this.cmdFilter.Location = new System.Drawing.Point(12, 25);
            this.cmdFilter.Name = "cmdFilter";
            this.cmdFilter.Size = new System.Drawing.Size(184, 23);
            this.cmdFilter.TabIndex = 5;
            this.cmdFilter.Text = "Filter Targets";
            this.toolTip1.SetToolTip(this.cmdFilter, "Only display characters who actually target yours. Very slow!");
            this.cmdFilter.UseVisualStyleBackColor = true;
            this.cmdFilter.Click += new System.EventHandler(this.cmdFilter_Click);
            // 
            // lstCharacters
            // 
            this.lstCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCharacters.BackColor = System.Drawing.Color.White;
            this.lstCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lstCharacters.ForeColor = System.Drawing.Color.Black;
            this.lstCharacters.FormattingEnabled = true;
            this.lstCharacters.Location = new System.Drawing.Point(12, 51);
            this.lstCharacters.Name = "lstCharacters";
            this.lstCharacters.Size = new System.Drawing.Size(184, 303);
            this.lstCharacters.TabIndex = 1;
            this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);
            // 
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCharacters.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCharacters.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblCharacters.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblCharacters.Location = new System.Drawing.Point(12, 9);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(126, 13);
            this.lblCharacters.TabIndex = 0;
            this.lblCharacters.Text = "Characters that target {0}";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.grpLines);
            this.splitContainer2.Panel1.Controls.Add(this.lblCaseInfo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(951, 674);
            this.splitContainer2.SplitterDistance = 272;
            this.splitContainer2.TabIndex = 0;
            // 
            // grpLines
            // 
            this.grpLines.BackColor = System.Drawing.Color.White;
            this.grpLines.Controls.Add(this.gridLines);
            this.grpLines.Controls.Add(this.lblNoMatches);
            this.grpLines.Controls.Add(this.cmdCreateResponse);
            this.grpLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLines.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
            this.grpLines.Image = null;
            this.grpLines.Location = new System.Drawing.Point(0, 0);
            this.grpLines.Name = "grpLines";
            this.grpLines.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.grpLines.ShowIndicatorBar = false;
            this.grpLines.Size = new System.Drawing.Size(951, 272);
            this.grpLines.TabIndex = 5;
            this.grpLines.TabStop = false;
            this.grpLines.Text = "Lines";
            // 
            // gridLines
            // 
            this.gridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLines.BackgroundColor = System.Drawing.Color.White;
            this.gridLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridLines.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridLines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColText,
            this.ColStage,
            this.ColCase});
            this.gridLines.Data = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridLines.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridLines.EnableHeadersVisualStyles = false;
            this.gridLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridLines.GridColor = System.Drawing.Color.LightGray;
            this.gridLines.Location = new System.Drawing.Point(6, 25);
            this.gridLines.MultiSelect = false;
            this.gridLines.Name = "gridLines";
            this.gridLines.ReadOnly = true;
            this.gridLines.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridLines.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridLines.Size = new System.Drawing.Size(939, 212);
            this.gridLines.TabIndex = 0;
            this.gridLines.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLines_CellEnter);
            // 
            // ColText
            // 
            this.ColText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColText.HeaderText = "Text";
            this.ColText.Name = "ColText";
            this.ColText.ReadOnly = true;
            // 
            // ColStage
            // 
            this.ColStage.HeaderText = "Stages";
            this.ColStage.Name = "ColStage";
            this.ColStage.ReadOnly = true;
            this.ColStage.Width = 50;
            // 
            // ColCase
            // 
            this.ColCase.HeaderText = "Case";
            this.ColCase.Name = "ColCase";
            this.ColCase.ReadOnly = true;
            this.ColCase.Width = 150;
            // 
            // lblNoMatches
            // 
            this.lblNoMatches.AutoSize = true;
            this.lblNoMatches.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblNoMatches.ForeColor = System.Drawing.Color.Red;
            this.lblNoMatches.Highlight = Desktop.Skinning.SkinnedHighlight.Bad;
            this.lblNoMatches.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
            this.lblNoMatches.Location = new System.Drawing.Point(6, 28);
            this.lblNoMatches.Name = "lblNoMatches";
            this.lblNoMatches.Size = new System.Drawing.Size(93, 21);
            this.lblNoMatches.TabIndex = 4;
            this.lblNoMatches.Text = "None found";
            this.lblNoMatches.Visible = false;
            // 
            // cmdCreateResponse
            // 
            this.cmdCreateResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreateResponse.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdCreateResponse.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
            this.cmdCreateResponse.Flat = false;
            this.cmdCreateResponse.Location = new System.Drawing.Point(804, 243);
            this.cmdCreateResponse.Name = "cmdCreateResponse";
            this.cmdCreateResponse.Size = new System.Drawing.Size(141, 23);
            this.cmdCreateResponse.TabIndex = 0;
            this.cmdCreateResponse.Text = "Create Response";
            this.cmdCreateResponse.UseVisualStyleBackColor = true;
            this.cmdCreateResponse.Click += new System.EventHandler(this.cmdCreateResponse_Click);
            // 
            // lblCaseInfo
            // 
            this.lblCaseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCaseInfo.AutoSize = true;
            this.lblCaseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCaseInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCaseInfo.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblCaseInfo.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblCaseInfo.Location = new System.Drawing.Point(3, 251);
            this.lblCaseInfo.Name = "lblCaseInfo";
            this.lblCaseInfo.Size = new System.Drawing.Size(0, 13);
            this.lblCaseInfo.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.grpBaseLine);
            this.splitContainer3.Panel1.Controls.Add(this.lblBasicText);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.grpResponse);
            this.splitContainer3.Size = new System.Drawing.Size(951, 398);
            this.splitContainer3.SplitterDistance = 440;
            this.splitContainer3.TabIndex = 6;
            // 
            // grpBaseLine
            // 
            this.grpBaseLine.BackColor = System.Drawing.Color.White;
            this.grpBaseLine.Controls.Add(this.lstBasicLines);
            this.grpBaseLine.Controls.Add(this.skinnedLabel1);
            this.grpBaseLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBaseLine.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
            this.grpBaseLine.Image = null;
            this.grpBaseLine.Location = new System.Drawing.Point(0, 0);
            this.grpBaseLine.Name = "grpBaseLine";
            this.grpBaseLine.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.grpBaseLine.ShowIndicatorBar = false;
            this.grpBaseLine.Size = new System.Drawing.Size(440, 398);
            this.grpBaseLine.TabIndex = 8;
            this.grpBaseLine.TabStop = false;
            this.grpBaseLine.Text = "Response";
            // 
            // lstBasicLines
            // 
            this.lstBasicLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBasicLines.BackColor = System.Drawing.Color.White;
            this.lstBasicLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lstBasicLines.ForeColor = System.Drawing.Color.Black;
            this.lstBasicLines.FormattingEnabled = true;
            this.lstBasicLines.IntegralHeight = false;
            this.lstBasicLines.Location = new System.Drawing.Point(6, 52);
            this.lstBasicLines.Name = "lstBasicLines";
            this.lstBasicLines.Size = new System.Drawing.Size(427, 342);
            this.lstBasicLines.TabIndex = 7;
            // 
            // skinnedLabel1
            // 
            this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.skinnedLabel1.ForeColor = System.Drawing.Color.Red;
            this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Bad;
            this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.skinnedLabel1.Location = new System.Drawing.Point(7, 23);
            this.skinnedLabel1.Name = "skinnedLabel1";
            this.skinnedLabel1.Size = new System.Drawing.Size(426, 35);
            this.skinnedLabel1.TabIndex = 8;
            this.skinnedLabel1.Text = "Disclaimer: These are estimates. Actual results in game may vary depending on the" +
    " game state.";
            // 
            // lblBasicText
            // 
            this.lblBasicText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBasicText.AutoSize = true;
            this.lblBasicText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblBasicText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBasicText.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblBasicText.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblBasicText.Location = new System.Drawing.Point(174, 0);
            this.lblBasicText.Name = "lblBasicText";
            this.lblBasicText.Size = new System.Drawing.Size(0, 13);
            this.lblBasicText.TabIndex = 6;
            // 
            // grpResponse
            // 
            this.grpResponse.BackColor = System.Drawing.Color.White;
            this.grpResponse.Controls.Add(this.cmdDiscard);
            this.grpResponse.Controls.Add(this.cmdAccept);
            this.grpResponse.Controls.Add(this.cmdJump);
            this.grpResponse.Controls.Add(this.gridResponse);
            this.grpResponse.Controls.Add(this.ctlResponse);
            this.grpResponse.Controls.Add(this.skinnedLabel2);
            this.grpResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResponse.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
            this.grpResponse.Image = null;
            this.grpResponse.Location = new System.Drawing.Point(0, 0);
            this.grpResponse.Name = "grpResponse";
            this.grpResponse.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.grpResponse.ShowIndicatorBar = false;
            this.grpResponse.Size = new System.Drawing.Size(507, 398);
            this.grpResponse.TabIndex = 6;
            this.grpResponse.TabStop = false;
            this.grpResponse.Text = "Write Response";
            // 
            // cmdDiscard
            // 
            this.cmdDiscard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDiscard.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdDiscard.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
            this.cmdDiscard.Flat = true;
            this.cmdDiscard.ForeColor = System.Drawing.Color.Blue;
            this.cmdDiscard.Location = new System.Drawing.Point(359, 369);
            this.cmdDiscard.Name = "cmdDiscard";
            this.cmdDiscard.Size = new System.Drawing.Size(141, 23);
            this.cmdDiscard.TabIndex = 13;
            this.cmdDiscard.Text = "Discard";
            this.cmdDiscard.UseVisualStyleBackColor = true;
            this.cmdDiscard.Click += new System.EventHandler(this.cmdDiscard_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdAccept.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
            this.cmdAccept.Flat = false;
            this.cmdAccept.Location = new System.Drawing.Point(212, 369);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(141, 23);
            this.cmdAccept.TabIndex = 12;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdJump
            // 
            this.cmdJump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdJump.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdJump.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
            this.cmdJump.Flat = false;
            this.cmdJump.Location = new System.Drawing.Point(65, 369);
            this.cmdJump.Name = "cmdJump";
            this.cmdJump.Size = new System.Drawing.Size(141, 23);
            this.cmdJump.TabIndex = 11;
            this.cmdJump.Text = "Edit Full Screen";
            this.cmdJump.UseVisualStyleBackColor = true;
            this.cmdJump.Click += new System.EventHandler(this.cmdJump_Click);
            // 
            // gridResponse
            // 
            this.gridResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridResponse.Location = new System.Drawing.Point(9, 52);
            this.gridResponse.Name = "gridResponse";
            this.gridResponse.ReadOnly = false;
            this.gridResponse.Size = new System.Drawing.Size(492, 311);
            this.gridResponse.TabIndex = 0;
            this.gridResponse.HighlightRow += new System.EventHandler<int>(this.gridResponse_HighlightRow);
            // 
            // ctlResponse
            // 
            this.ctlResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlResponse.Location = new System.Drawing.Point(9, 52);
            this.ctlResponse.Name = "ctlResponse";
            this.ctlResponse.Size = new System.Drawing.Size(492, 311);
            this.ctlResponse.TabIndex = 10;
            this.ctlResponse.HighlightRow += new System.EventHandler<int>(this.gridResponse_HighlightRow);
            // 
            // skinnedLabel2
            // 
            this.skinnedLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.skinnedLabel2.ForeColor = System.Drawing.Color.Red;
            this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Bad;
            this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.skinnedLabel2.Location = new System.Drawing.Point(6, 23);
            this.skinnedLabel2.Name = "skinnedLabel2";
            this.skinnedLabel2.Size = new System.Drawing.Size(495, 26);
            this.skinnedLabel2.TabIndex = 9;
            this.skinnedLabel2.Text = "These play at the same time as the selected line above, so be careful not to over" +
    "write the line that is giving context to what the opponent is saying!";
            // 
            // panelLoad
            // 
            this.panelLoad.Controls.Add(this.lblProgress);
            this.panelLoad.Controls.Add(this.progress);
            this.panelLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLoad.Location = new System.Drawing.Point(0, 0);
            this.panelLoad.Name = "panelLoad";
            this.panelLoad.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
            this.panelLoad.Size = new System.Drawing.Size(951, 674);
            this.panelLoad.TabIndex = 5;
            this.panelLoad.TabSide = Desktop.Skinning.TabSide.None;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblProgress.ForeColor = System.Drawing.Color.Blue;
            this.lblProgress.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblProgress.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
            this.lblProgress.Location = new System.Drawing.Point(80, 292);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(784, 23);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "Scanning ...";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.Location = new System.Drawing.Point(80, 322);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(784, 23);
            this.progress.TabIndex = 0;
            // 
            // BanterWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "BanterWizard";
            this.Size = new System.Drawing.Size(1161, 674);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.grpLines.ResumeLayout(false);
            this.grpLines.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.grpBaseLine.ResumeLayout(false);
            this.grpResponse.ResumeLayout(false);
            this.panelLoad.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private Desktop.Skinning.SkinnedListBox lstCharacters;
		private Desktop.Skinning.SkinnedLabel lblCharacters;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.Skinning.SkinnedLabel lblCaseInfo;
		private Desktop.Skinning.SkinnedButton cmdCreateResponse;
		private Desktop.Skinning.SkinnedDataGridView gridLines;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColText;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColCase;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private Desktop.Skinning.SkinnedLabel lblBasicText;
		private Desktop.Skinning.SkinnedListBox lstBasicLines;
		private Desktop.Skinning.SkinnedLabel lblNoMatches;
		private Desktop.Skinning.SkinnedButton cmdFilter;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedGroupBox grpLines;
		private Desktop.Skinning.SkinnedGroupBox grpBaseLine;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedGroupBox grpResponse;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Controls.CaseControl ctlResponse;
		private Desktop.Skinning.SkinnedButton cmdDiscard;
		private Desktop.Skinning.SkinnedButton cmdAccept;
		private Desktop.Skinning.SkinnedButton cmdJump;
		private Controls.DialogueGrid gridResponse;
		private Desktop.Skinning.SkinnedPanel panelLoad;
		private Desktop.Skinning.SkinnedProgressBar progress;
		private Desktop.Skinning.SkinnedLabel lblProgress;
	}
}

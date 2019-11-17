namespace SPNATI_Character_Editor.Activities
{
	partial class WritingAid
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitSituations = new System.Windows.Forms.SplitContainer();
			this.containerSituation = new Desktop.Skinning.SkinnedGroupBox();
			this.recFilter = new Desktop.CommonControls.RecordField();
			this.lblPriority = new Desktop.Skinning.SkinnedLabel();
			this.cboPriority = new Desktop.Skinning.SkinnedComboBox();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.cmdRespond = new Desktop.Skinning.SkinnedButton();
			this.chkFilter = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdNew = new Desktop.Skinning.SkinnedButton();
			this.valSuggestions = new Desktop.Skinning.SkinnedNumericUpDown();
			this.gridSituations = new Desktop.Skinning.SkinnedDataGridView();
			this.ColCharacter = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColStages = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColTrigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColJump = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.containerLines = new Desktop.Skinning.SkinnedGroupBox();
			this.gridActiveSituation = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.containerResponse = new Desktop.Skinning.SkinnedGroupBox();
			this.gridLines = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.cmdJumpToDialogue = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdAccept = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.lblResponseCount = new Desktop.Skinning.SkinnedLabel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitSituations)).BeginInit();
			this.splitSituations.Panel1.SuspendLayout();
			this.splitSituations.Panel2.SuspendLayout();
			this.splitSituations.SuspendLayout();
			this.containerSituation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valSuggestions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridSituations)).BeginInit();
			this.containerLines.SuspendLayout();
			this.containerResponse.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitSituations);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.containerResponse);
			this.splitContainer1.Size = new System.Drawing.Size(947, 642);
			this.splitContainer1.SplitterDistance = 330;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitSituations
			// 
			this.splitSituations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitSituations.Location = new System.Drawing.Point(0, 0);
			this.splitSituations.Name = "splitSituations";
			this.splitSituations.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitSituations.Panel1
			// 
			this.splitSituations.Panel1.Controls.Add(this.containerSituation);
			// 
			// splitSituations.Panel2
			// 
			this.splitSituations.Panel2.Controls.Add(this.containerLines);
			this.splitSituations.Panel2Collapsed = true;
			this.splitSituations.Size = new System.Drawing.Size(943, 326);
			this.splitSituations.SplitterDistance = 178;
			this.splitSituations.TabIndex = 6;
			// 
			// containerSituation
			// 
			this.containerSituation.BackColor = System.Drawing.Color.White;
			this.containerSituation.Controls.Add(this.lblResponseCount);
			this.containerSituation.Controls.Add(this.skinnedLabel1);
			this.containerSituation.Controls.Add(this.recFilter);
			this.containerSituation.Controls.Add(this.lblPriority);
			this.containerSituation.Controls.Add(this.cboPriority);
			this.containerSituation.Controls.Add(this.label6);
			this.containerSituation.Controls.Add(this.label3);
			this.containerSituation.Controls.Add(this.cmdRespond);
			this.containerSituation.Controls.Add(this.chkFilter);
			this.containerSituation.Controls.Add(this.cmdNew);
			this.containerSituation.Controls.Add(this.valSuggestions);
			this.containerSituation.Controls.Add(this.gridSituations);
			this.containerSituation.Controls.Add(this.label5);
			this.containerSituation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.containerSituation.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.containerSituation.Image = null;
			this.containerSituation.Location = new System.Drawing.Point(0, 0);
			this.containerSituation.Name = "containerSituation";
			this.containerSituation.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.containerSituation.ShowIndicatorBar = false;
			this.containerSituation.Size = new System.Drawing.Size(943, 326);
			this.containerSituation.TabIndex = 11;
			this.containerSituation.TabStop = false;
			this.containerSituation.Text = "Choose a Situation to Respond To";
			// 
			// recFilter
			// 
			this.recFilter.AllowCreate = false;
			this.recFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.recFilter.Location = new System.Drawing.Point(64, 299);
			this.recFilter.Name = "recFilter";
			this.recFilter.PlaceholderText = null;
			this.recFilter.Record = null;
			this.recFilter.RecordContext = null;
			this.recFilter.RecordFilter = null;
			this.recFilter.RecordKey = null;
			this.recFilter.RecordType = null;
			this.recFilter.Size = new System.Drawing.Size(165, 20);
			this.recFilter.TabIndex = 13;
			this.recFilter.UseAutoComplete = true;
			this.recFilter.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recFilter_RecordChanged);
			// 
			// lblPriority
			// 
			this.lblPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPriority.AutoSize = true;
			this.lblPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblPriority.ForeColor = System.Drawing.Color.Black;
			this.lblPriority.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblPriority.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblPriority.Location = new System.Drawing.Point(235, 302);
			this.lblPriority.Name = "lblPriority";
			this.lblPriority.Size = new System.Drawing.Size(44, 13);
			this.lblPriority.TabIndex = 11;
			this.lblPriority.Text = "Filter to:";
			// 
			// cboPriority
			// 
			this.cboPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cboPriority.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboPriority.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboPriority.BackColor = System.Drawing.Color.White;
			this.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPriority.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboPriority.FormattingEnabled = true;
			this.cboPriority.KeyMember = null;
			this.cboPriority.Location = new System.Drawing.Point(293, 299);
			this.cboPriority.Name = "cboPriority";
			this.cboPriority.SelectedIndex = -1;
			this.cboPriority.SelectedItem = null;
			this.cboPriority.Size = new System.Drawing.Size(115, 21);
			this.cboPriority.Sorted = false;
			this.cboPriority.TabIndex = 12;
			this.cboPriority.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.Color.Black;
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label6.Location = new System.Drawing.Point(874, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "suggestions";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(6, 302);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Filter to:";
			// 
			// cmdRespond
			// 
			this.cmdRespond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRespond.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdRespond.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdRespond.Flat = false;
			this.cmdRespond.Location = new System.Drawing.Point(675, 297);
			this.cmdRespond.Name = "cmdRespond";
			this.cmdRespond.Size = new System.Drawing.Size(128, 23);
			this.cmdRespond.TabIndex = 6;
			this.cmdRespond.Text = "Respond";
			this.cmdRespond.UseVisualStyleBackColor = true;
			this.cmdRespond.Click += new System.EventHandler(this.cmdRespond_Click);
			// 
			// chkFilter
			// 
			this.chkFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkFilter.AutoSize = true;
			this.chkFilter.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkFilter.Location = new System.Drawing.Point(528, 6);
			this.chkFilter.Name = "chkFilter";
			this.chkFilter.Size = new System.Drawing.Size(193, 17);
			this.chkFilter.TabIndex = 10;
			this.chkFilter.Text = "Include situations I\'ve responded to";
			this.chkFilter.UseVisualStyleBackColor = true;
			this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
			// 
			// cmdNew
			// 
			this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNew.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdNew.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdNew.Flat = false;
			this.cmdNew.Location = new System.Drawing.Point(809, 297);
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Size = new System.Drawing.Size(128, 23);
			this.cmdNew.TabIndex = 2;
			this.cmdNew.Text = "New Options";
			this.cmdNew.UseVisualStyleBackColor = true;
			this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
			// 
			// valSuggestions
			// 
			this.valSuggestions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valSuggestions.BackColor = System.Drawing.Color.White;
			this.valSuggestions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valSuggestions.ForeColor = System.Drawing.Color.Black;
			this.valSuggestions.Location = new System.Drawing.Point(823, 4);
			this.valSuggestions.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.valSuggestions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valSuggestions.Name = "valSuggestions";
			this.valSuggestions.Size = new System.Drawing.Size(45, 20);
			this.valSuggestions.TabIndex = 7;
			this.valSuggestions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valSuggestions.ValueChanged += new System.EventHandler(this.valSuggestions_ValueChanged);
			// 
			// gridSituations
			// 
			this.gridSituations.AllowUserToAddRows = false;
			this.gridSituations.AllowUserToDeleteRows = false;
			this.gridSituations.AllowUserToResizeRows = false;
			this.gridSituations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridSituations.BackgroundColor = System.Drawing.Color.White;
			this.gridSituations.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridSituations.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridSituations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridSituations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridSituations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCharacter,
            this.ColName,
            this.ColDescription,
            this.ColStages,
            this.ColTrigger,
            this.ColJump});
			this.gridSituations.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridSituations.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridSituations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.gridSituations.EnableHeadersVisualStyles = false;
			this.gridSituations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridSituations.GridColor = System.Drawing.Color.LightGray;
			this.gridSituations.Location = new System.Drawing.Point(6, 25);
			this.gridSituations.MultiSelect = false;
			this.gridSituations.Name = "gridSituations";
			this.gridSituations.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridSituations.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridSituations.RowHeadersVisible = false;
			this.gridSituations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridSituations.Size = new System.Drawing.Size(931, 268);
			this.gridSituations.TabIndex = 3;
			this.gridSituations.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridSituations_CellContentClick);
			this.gridSituations.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridSituations_CellPainting);
			this.gridSituations.SelectionChanged += new System.EventHandler(this.gridSituations_SelectionChanged);
			// 
			// ColCharacter
			// 
			this.ColCharacter.HeaderText = "Character";
			this.ColCharacter.Name = "ColCharacter";
			this.ColCharacter.Width = 125;
			// 
			// ColName
			// 
			this.ColName.HeaderText = "Name";
			this.ColName.Name = "ColName";
			this.ColName.Width = 125;
			// 
			// ColDescription
			// 
			this.ColDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColDescription.HeaderText = "Description";
			this.ColDescription.Name = "ColDescription";
			// 
			// ColStages
			// 
			this.ColStages.HeaderText = "Stages";
			this.ColStages.Name = "ColStages";
			this.ColStages.ReadOnly = true;
			this.ColStages.Width = 80;
			// 
			// ColTrigger
			// 
			this.ColTrigger.HeaderText = "Trigger";
			this.ColTrigger.Name = "ColTrigger";
			this.ColTrigger.ReadOnly = true;
			this.ColTrigger.Width = 150;
			// 
			// ColJump
			// 
			this.ColJump.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColJump.Flat = false;
			this.ColJump.HeaderText = "";
			this.ColJump.Name = "ColJump";
			this.ColJump.Width = 21;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(766, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Show me";
			// 
			// containerLines
			// 
			this.containerLines.Controls.Add(this.gridActiveSituation);
			this.containerLines.Dock = System.Windows.Forms.DockStyle.Fill;
			this.containerLines.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.containerLines.Image = null;
			this.containerLines.Location = new System.Drawing.Point(0, 0);
			this.containerLines.Name = "containerLines";
			this.containerLines.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.containerLines.ShowIndicatorBar = false;
			this.containerLines.Size = new System.Drawing.Size(150, 46);
			this.containerLines.TabIndex = 4;
			this.containerLines.TabStop = false;
			this.containerLines.Text = "Lines They Might Say";
			// 
			// gridActiveSituation
			// 
			this.gridActiveSituation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridActiveSituation.Location = new System.Drawing.Point(6, 25);
			this.gridActiveSituation.Name = "gridActiveSituation";
			this.gridActiveSituation.ReadOnly = true;
			this.gridActiveSituation.Size = new System.Drawing.Size(138, 15);
			this.gridActiveSituation.TabIndex = 3;
			this.gridActiveSituation.HighlightRow += new System.EventHandler<int>(this.gridActiveSituation_HighlightRow);
			// 
			// containerResponse
			// 
			this.containerResponse.BackColor = System.Drawing.Color.White;
			this.containerResponse.Controls.Add(this.gridLines);
			this.containerResponse.Controls.Add(this.cmdJumpToDialogue);
			this.containerResponse.Controls.Add(this.cmdCancel);
			this.containerResponse.Controls.Add(this.cmdAccept);
			this.containerResponse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.containerResponse.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.containerResponse.Image = null;
			this.containerResponse.Location = new System.Drawing.Point(0, 0);
			this.containerResponse.Name = "containerResponse";
			this.containerResponse.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.containerResponse.ShowIndicatorBar = false;
			this.containerResponse.Size = new System.Drawing.Size(943, 304);
			this.containerResponse.TabIndex = 10;
			this.containerResponse.TabStop = false;
			this.containerResponse.Text = "Write Some Lines in Response";
			// 
			// gridLines
			// 
			this.gridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLines.Location = new System.Drawing.Point(6, 26);
			this.gridLines.Name = "gridLines";
			this.gridLines.ReadOnly = false;
			this.gridLines.Size = new System.Drawing.Size(931, 245);
			this.gridLines.TabIndex = 0;
			this.gridLines.HighlightRow += new System.EventHandler<int>(this.gridLines_HighlightRow);
			// 
			// cmdJumpToDialogue
			// 
			this.cmdJumpToDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdJumpToDialogue.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdJumpToDialogue.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdJumpToDialogue.Flat = false;
			this.cmdJumpToDialogue.Location = new System.Drawing.Point(521, 275);
			this.cmdJumpToDialogue.Name = "cmdJumpToDialogue";
			this.cmdJumpToDialogue.Size = new System.Drawing.Size(148, 23);
			this.cmdJumpToDialogue.TabIndex = 9;
			this.cmdJumpToDialogue.Text = "Edit Full Screen";
			this.cmdJumpToDialogue.UseVisualStyleBackColor = true;
			this.cmdJumpToDialogue.Click += new System.EventHandler(this.cmdJumpToDialogue_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.Blue;
			this.cmdCancel.Location = new System.Drawing.Point(809, 275);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(128, 23);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdAccept
			// 
			this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAccept.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAccept.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdAccept.Flat = false;
			this.cmdAccept.Location = new System.Drawing.Point(675, 275);
			this.cmdAccept.Name = "cmdAccept";
			this.cmdAccept.Size = new System.Drawing.Size(128, 23);
			this.cmdAccept.TabIndex = 8;
			this.cmdAccept.Text = "Accept";
			this.cmdAccept.UseVisualStyleBackColor = true;
			this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(515, 302);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(122, 13);
			this.skinnedLabel1.TabIndex = 14;
			this.skinnedLabel1.Text = "Total situations handled:";
			// 
			// lblResponseCount
			// 
			this.lblResponseCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblResponseCount.AutoSize = true;
			this.lblResponseCount.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblResponseCount.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblResponseCount.Location = new System.Drawing.Point(643, 302);
			this.lblResponseCount.Name = "lblResponseCount";
			this.lblResponseCount.Size = new System.Drawing.Size(13, 13);
			this.lblResponseCount.TabIndex = 15;
			this.lblResponseCount.Text = "0";
			// 
			// WritingAid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "WritingAid";
			this.Size = new System.Drawing.Size(947, 642);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitSituations.Panel1.ResumeLayout(false);
			this.splitSituations.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitSituations)).EndInit();
			this.splitSituations.ResumeLayout(false);
			this.containerSituation.ResumeLayout(false);
			this.containerSituation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valSuggestions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridSituations)).EndInit();
			this.containerLines.ResumeLayout(false);
			this.containerResponse.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private Controls.DialogueGrid gridLines;
		private Desktop.Skinning.SkinnedButton cmdNew;
		private Desktop.Skinning.SkinnedDataGridView gridSituations;
		private Desktop.Skinning.SkinnedLabel label3;
		private Controls.DialogueGrid gridActiveSituation;
		private System.Windows.Forms.SplitContainer splitSituations;
		private Desktop.Skinning.SkinnedButton cmdRespond;
		private Desktop.Skinning.SkinnedButton cmdAccept;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedNumericUpDown valSuggestions;
		private Desktop.Skinning.SkinnedButton cmdJumpToDialogue;
		private Desktop.Skinning.SkinnedCheckBox chkFilter;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColCharacter;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStages;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTrigger;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColJump;
		private Desktop.Skinning.SkinnedGroupBox containerSituation;
		private Desktop.Skinning.SkinnedGroupBox containerResponse;
		private Desktop.Skinning.SkinnedGroupBox containerLines;
		private Desktop.Skinning.SkinnedLabel lblPriority;
		private Desktop.Skinning.SkinnedComboBox cboPriority;
		private Desktop.CommonControls.RecordField recFilter;
		private Desktop.Skinning.SkinnedLabel lblResponseCount;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
	}
}

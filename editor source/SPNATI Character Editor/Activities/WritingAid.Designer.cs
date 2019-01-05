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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitSituations = new System.Windows.Forms.SplitContainer();
			this.chkFilter = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.valSuggestions = new System.Windows.Forms.NumericUpDown();
			this.cmdRespond = new System.Windows.Forms.Button();
			this.cmdNew = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.gridSituations = new System.Windows.Forms.DataGridView();
			this.ColCharacter = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColStages = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColTrigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColJump = new System.Windows.Forms.DataGridViewButtonColumn();
			this.label3 = new System.Windows.Forms.Label();
			this.cboFilter = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gridActiveSituation = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.cmdJumpToDialogue = new System.Windows.Forms.Button();
			this.cmdAccept = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.gridLines = new SPNATI_Character_Editor.Controls.DialogueGrid();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitSituations)).BeginInit();
			this.splitSituations.Panel1.SuspendLayout();
			this.splitSituations.Panel2.SuspendLayout();
			this.splitSituations.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valSuggestions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridSituations)).BeginInit();
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
			this.splitContainer1.Panel2.Controls.Add(this.cmdJumpToDialogue);
			this.splitContainer1.Panel2.Controls.Add(this.cmdAccept);
			this.splitContainer1.Panel2.Controls.Add(this.cmdCancel);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.gridLines);
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
			this.splitSituations.Panel1.Controls.Add(this.chkFilter);
			this.splitSituations.Panel1.Controls.Add(this.label6);
			this.splitSituations.Panel1.Controls.Add(this.label5);
			this.splitSituations.Panel1.Controls.Add(this.valSuggestions);
			this.splitSituations.Panel1.Controls.Add(this.cmdRespond);
			this.splitSituations.Panel1.Controls.Add(this.cmdNew);
			this.splitSituations.Panel1.Controls.Add(this.label1);
			this.splitSituations.Panel1.Controls.Add(this.gridSituations);
			this.splitSituations.Panel1.Controls.Add(this.label3);
			this.splitSituations.Panel1.Controls.Add(this.cboFilter);
			// 
			// splitSituations.Panel2
			// 
			this.splitSituations.Panel2.Controls.Add(this.label2);
			this.splitSituations.Panel2.Controls.Add(this.gridActiveSituation);
			this.splitSituations.Panel2Collapsed = true;
			this.splitSituations.Size = new System.Drawing.Size(943, 326);
			this.splitSituations.SplitterDistance = 178;
			this.splitSituations.TabIndex = 6;
			// 
			// chkFilter
			// 
			this.chkFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkFilter.AutoSize = true;
			this.chkFilter.Location = new System.Drawing.Point(531, 9);
			this.chkFilter.Name = "chkFilter";
			this.chkFilter.Size = new System.Drawing.Size(193, 17);
			this.chkFilter.TabIndex = 10;
			this.chkFilter.Text = "Include situations I\'ve responded to";
			this.chkFilter.UseVisualStyleBackColor = true;
			this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(877, 9);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "suggestions";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(769, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Show me";
			// 
			// valSuggestions
			// 
			this.valSuggestions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.valSuggestions.Location = new System.Drawing.Point(826, 7);
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
			// cmdRespond
			// 
			this.cmdRespond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRespond.Location = new System.Drawing.Point(678, 300);
			this.cmdRespond.Name = "cmdRespond";
			this.cmdRespond.Size = new System.Drawing.Size(128, 23);
			this.cmdRespond.TabIndex = 6;
			this.cmdRespond.Text = "Respond";
			this.cmdRespond.UseVisualStyleBackColor = true;
			this.cmdRespond.Click += new System.EventHandler(this.cmdRespond_Click);
			// 
			// cmdNew
			// 
			this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNew.Location = new System.Drawing.Point(812, 300);
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Size = new System.Drawing.Size(128, 23);
			this.cmdNew.TabIndex = 2;
			this.cmdNew.Text = "Give Me New Options";
			this.cmdNew.UseVisualStyleBackColor = true;
			this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(162, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Choose a situation to respond to:";
			// 
			// gridSituations
			// 
			this.gridSituations.AllowUserToAddRows = false;
			this.gridSituations.AllowUserToDeleteRows = false;
			this.gridSituations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridSituations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridSituations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCharacter,
            this.ColName,
            this.ColDescription,
            this.ColStages,
            this.ColTrigger,
            this.ColJump});
			this.gridSituations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.gridSituations.Location = new System.Drawing.Point(6, 30);
			this.gridSituations.MultiSelect = false;
			this.gridSituations.Name = "gridSituations";
			this.gridSituations.RowHeadersVisible = false;
			this.gridSituations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridSituations.Size = new System.Drawing.Size(934, 264);
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
			this.ColJump.HeaderText = "";
			this.ColJump.Name = "ColJump";
			this.ColJump.Width = 21;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 303);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Filter to:";
			// 
			// cboFilter
			// 
			this.cboFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilter.FormattingEnabled = true;
			this.cboFilter.Location = new System.Drawing.Point(61, 300);
			this.cboFilter.Name = "cboFilter";
			this.cboFilter.Size = new System.Drawing.Size(165, 21);
			this.cboFilter.TabIndex = 5;
			this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Lines they might say:";
			// 
			// gridActiveSituation
			// 
			this.gridActiveSituation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridActiveSituation.Location = new System.Drawing.Point(3, 22);
			this.gridActiveSituation.Name = "gridActiveSituation";
			this.gridActiveSituation.ReadOnly = true;
			this.gridActiveSituation.Size = new System.Drawing.Size(2523, 431);
			this.gridActiveSituation.TabIndex = 3;
			this.gridActiveSituation.HighlightRow += new System.EventHandler<int>(this.gridActiveSituation_HighlightRow);
			// 
			// cmdJumpToDialogue
			// 
			this.cmdJumpToDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdJumpToDialogue.Location = new System.Drawing.Point(544, 267);
			this.cmdJumpToDialogue.Name = "cmdJumpToDialogue";
			this.cmdJumpToDialogue.Size = new System.Drawing.Size(128, 23);
			this.cmdJumpToDialogue.TabIndex = 9;
			this.cmdJumpToDialogue.Text = "Edit in Dialogue Editor";
			this.cmdJumpToDialogue.UseVisualStyleBackColor = true;
			this.cmdJumpToDialogue.Click += new System.EventHandler(this.cmdJumpToDialogue_Click);
			// 
			// cmdAccept
			// 
			this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAccept.Location = new System.Drawing.Point(678, 267);
			this.cmdAccept.Name = "cmdAccept";
			this.cmdAccept.Size = new System.Drawing.Size(128, 23);
			this.cmdAccept.TabIndex = 8;
			this.cmdAccept.Text = "Accept";
			this.cmdAccept.UseVisualStyleBackColor = true;
			this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point(812, 267);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(128, 23);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Write some lines in response:";
			// 
			// gridLines
			// 
			this.gridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLines.Location = new System.Drawing.Point(3, 16);
			this.gridLines.Name = "gridLines";
			this.gridLines.ReadOnly = false;
			this.gridLines.Size = new System.Drawing.Size(937, 245);
			this.gridLines.TabIndex = 0;
			this.gridLines.HighlightRow += new System.EventHandler<int>(this.gridLines_HighlightRow);
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
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitSituations.Panel1.ResumeLayout(false);
			this.splitSituations.Panel1.PerformLayout();
			this.splitSituations.Panel2.ResumeLayout(false);
			this.splitSituations.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitSituations)).EndInit();
			this.splitSituations.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.valSuggestions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridSituations)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private Controls.DialogueGrid gridLines;
		private System.Windows.Forms.Button cmdNew;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView gridSituations;
		private System.Windows.Forms.ComboBox cboFilter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private Controls.DialogueGrid gridActiveSituation;
		private System.Windows.Forms.SplitContainer splitSituations;
		private System.Windows.Forms.Button cmdRespond;
		private System.Windows.Forms.Button cmdAccept;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown valSuggestions;
		private System.Windows.Forms.Button cmdJumpToDialogue;
		private System.Windows.Forms.CheckBox chkFilter;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColCharacter;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStages;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTrigger;
		private System.Windows.Forms.DataGridViewButtonColumn ColJump;
	}
}

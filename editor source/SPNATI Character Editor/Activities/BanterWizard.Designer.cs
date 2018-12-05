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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstTags = new System.Windows.Forms.ListBox();
			this.lblTags = new System.Windows.Forms.Label();
			this.lstCharacters = new System.Windows.Forms.ListBox();
			this.lblCharacters = new System.Windows.Forms.Label();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.lblCaseInfo = new System.Windows.Forms.Label();
			this.cmdCreateResponse = new System.Windows.Forms.Button();
			this.lblLines = new System.Windows.Forms.Label();
			this.gridLines = new System.Windows.Forms.DataGridView();
			this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColCase = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.lblBasicText = new System.Windows.Forms.Label();
			this.lstBasicLines = new System.Windows.Forms.ListBox();
			this.lblBaseLine = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblResponse = new System.Windows.Forms.Label();
			this.gridResponse = new SPNATI_Character_Editor.Controls.DialogueGrid();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.lstTags);
			this.splitContainer1.Panel1.Controls.Add(this.lblTags);
			this.splitContainer1.Panel1.Controls.Add(this.lstCharacters);
			this.splitContainer1.Panel1.Controls.Add(this.lblCharacters);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1161, 674);
			this.splitContainer1.SplitterDistance = 206;
			this.splitContainer1.TabIndex = 1;
			// 
			// lstTags
			// 
			this.lstTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstTags.FormattingEnabled = true;
			this.lstTags.Location = new System.Drawing.Point(12, 375);
			this.lstTags.Name = "lstTags";
			this.lstTags.Size = new System.Drawing.Size(184, 277);
			this.lstTags.TabIndex = 3;
			this.lstTags.SelectedIndexChanged += new System.EventHandler(this.lstTags_SelectedIndexChanged);
			// 
			// lblTags
			// 
			this.lblTags.AutoSize = true;
			this.lblTags.Location = new System.Drawing.Point(12, 359);
			this.lblTags.Name = "lblTags";
			this.lblTags.Size = new System.Drawing.Size(136, 13);
			this.lblTags.TabIndex = 2;
			this.lblTags.Text = "Characters that target a tag";
			// 
			// lstCharacters
			// 
			this.lstCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCharacters.FormattingEnabled = true;
			this.lstCharacters.Location = new System.Drawing.Point(12, 25);
			this.lstCharacters.Name = "lstCharacters";
			this.lstCharacters.Size = new System.Drawing.Size(184, 329);
			this.lstCharacters.TabIndex = 1;
			this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);
			// 
			// lblCharacters
			// 
			this.lblCharacters.AutoSize = true;
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
			this.splitContainer2.Panel1.Controls.Add(this.lblCaseInfo);
			this.splitContainer2.Panel1.Controls.Add(this.cmdCreateResponse);
			this.splitContainer2.Panel1.Controls.Add(this.lblLines);
			this.splitContainer2.Panel1.Controls.Add(this.gridLines);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer2.Size = new System.Drawing.Size(951, 674);
			this.splitContainer2.SplitterDistance = 272;
			this.splitContainer2.TabIndex = 0;
			// 
			// lblCaseInfo
			// 
			this.lblCaseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblCaseInfo.AutoSize = true;
			this.lblCaseInfo.Location = new System.Drawing.Point(3, 251);
			this.lblCaseInfo.Name = "lblCaseInfo";
			this.lblCaseInfo.Size = new System.Drawing.Size(0, 13);
			this.lblCaseInfo.TabIndex = 3;
			// 
			// cmdCreateResponse
			// 
			this.cmdCreateResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCreateResponse.Location = new System.Drawing.Point(832, 246);
			this.cmdCreateResponse.Name = "cmdCreateResponse";
			this.cmdCreateResponse.Size = new System.Drawing.Size(116, 23);
			this.cmdCreateResponse.TabIndex = 0;
			this.cmdCreateResponse.Text = "Create Response";
			this.cmdCreateResponse.UseVisualStyleBackColor = true;
			this.cmdCreateResponse.Click += new System.EventHandler(this.cmdCreateResponse_Click);
			// 
			// lblLines
			// 
			this.lblLines.AutoSize = true;
			this.lblLines.Location = new System.Drawing.Point(3, 9);
			this.lblLines.Name = "lblLines";
			this.lblLines.Size = new System.Drawing.Size(32, 13);
			this.lblLines.TabIndex = 2;
			this.lblLines.Text = "Lines";
			// 
			// gridLines
			// 
			this.gridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColText,
            this.ColStage,
            this.ColCase});
			this.gridLines.Location = new System.Drawing.Point(3, 25);
			this.gridLines.MultiSelect = false;
			this.gridLines.Name = "gridLines";
			this.gridLines.ReadOnly = true;
			this.gridLines.Size = new System.Drawing.Size(945, 216);
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
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.lblBasicText);
			this.splitContainer3.Panel1.Controls.Add(this.lstBasicLines);
			this.splitContainer3.Panel1.Controls.Add(this.lblBaseLine);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.label3);
			this.splitContainer3.Panel2.Controls.Add(this.lblResponse);
			this.splitContainer3.Panel2.Controls.Add(this.gridResponse);
			this.splitContainer3.Size = new System.Drawing.Size(951, 398);
			this.splitContainer3.SplitterDistance = 440;
			this.splitContainer3.TabIndex = 6;
			// 
			// lblBasicText
			// 
			this.lblBasicText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblBasicText.AutoSize = true;
			this.lblBasicText.Location = new System.Drawing.Point(174, 0);
			this.lblBasicText.Name = "lblBasicText";
			this.lblBasicText.Size = new System.Drawing.Size(0, 13);
			this.lblBasicText.TabIndex = 6;
			// 
			// lstBasicLines
			// 
			this.lstBasicLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstBasicLines.FormattingEnabled = true;
			this.lstBasicLines.Location = new System.Drawing.Point(6, 16);
			this.lstBasicLines.Name = "lstBasicLines";
			this.lstBasicLines.Size = new System.Drawing.Size(431, 368);
			this.lstBasicLines.TabIndex = 7;
			// 
			// lblBaseLine
			// 
			this.lblBaseLine.AutoSize = true;
			this.lblBaseLine.Location = new System.Drawing.Point(3, 0);
			this.lblBaseLine.Name = "lblBaseLine";
			this.lblBaseLine.Size = new System.Drawing.Size(154, 13);
			this.lblBaseLine.TabIndex = 6;
			this.lblBaseLine.Text = "{0} is responding to these lines:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Responses";
			// 
			// lblResponse
			// 
			this.lblResponse.AutoSize = true;
			this.lblResponse.Location = new System.Drawing.Point(105, 0);
			this.lblResponse.Name = "lblResponse";
			this.lblResponse.Size = new System.Drawing.Size(13, 13);
			this.lblResponse.TabIndex = 5;
			this.lblResponse.Text = "a";
			// 
			// gridResponse
			// 
			this.gridResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridResponse.Location = new System.Drawing.Point(6, 16);
			this.gridResponse.Name = "gridResponse";
			this.gridResponse.ReadOnly = false;
			this.gridResponse.Size = new System.Drawing.Size(498, 379);
			this.gridResponse.TabIndex = 0;
			this.gridResponse.HighlightRow += new System.EventHandler<int>(this.gridResponse_HighlightRow);
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
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox lstTags;
		private System.Windows.Forms.Label lblTags;
		private System.Windows.Forms.ListBox lstCharacters;
		private System.Windows.Forms.Label lblCharacters;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Label lblCaseInfo;
		private System.Windows.Forms.Button cmdCreateResponse;
		private System.Windows.Forms.Label lblLines;
		private System.Windows.Forms.DataGridView gridLines;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColText;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColCase;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.Label lblBasicText;
		private System.Windows.Forms.ListBox lstBasicLines;
		private System.Windows.Forms.Label lblBaseLine;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblResponse;
		private Controls.DialogueGrid gridResponse;
	}
}

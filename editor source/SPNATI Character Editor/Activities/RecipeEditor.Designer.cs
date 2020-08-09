namespace SPNATI_Character_Editor.Activities
{
	partial class RecipeEditor
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.txtDescription = new Desktop.Skinning.SkinnedTextBox();
			this.cmdOpen = new Desktop.Skinning.SkinnedButton();
			this.tableConditions = new Desktop.CommonControls.PropertyTable();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.cboTag = new Desktop.Skinning.SkinnedComboBox();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.txtFile = new Desktop.Skinning.SkinnedTextBox();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.txtGroup = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox3 = new Desktop.Skinning.SkinnedGroupBox();
			this.gridLines = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.txtLabel = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedGroupBox1.SuspendLayout();
			this.skinnedGroupBox2.SuspendLayout();
			this.skinnedGroupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(6, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(82, 25);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(402, 20);
			this.txtName.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(6, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Description:";
			// 
			// txtDescription
			// 
			this.txtDescription.BackColor = System.Drawing.Color.White;
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtDescription.ForeColor = System.Drawing.Color.Black;
			this.txtDescription.Location = new System.Drawing.Point(82, 77);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(799, 79);
			this.txtDescription.TabIndex = 5;
			// 
			// cmdOpen
			// 
			this.cmdOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOpen.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOpen.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOpen.Flat = false;
			this.cmdOpen.Location = new System.Drawing.Point(777, 23);
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Size = new System.Drawing.Size(104, 23);
			this.cmdOpen.TabIndex = 3;
			this.cmdOpen.Text = "Open Folder";
			this.cmdOpen.UseVisualStyleBackColor = true;
			this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
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
			this.tableConditions.HideAddField = true;
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
			this.tableConditions.Size = new System.Drawing.Size(875, 320);
			this.tableConditions.Sorted = false;
			this.tableConditions.TabIndex = 20;
			this.tableConditions.UndoManager = null;
			this.tableConditions.UseAutoComplete = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label4.Location = new System.Drawing.Point(6, 165);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Case tag:";
			// 
			// cboTag
			// 
			this.cboTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboTag.BackColor = System.Drawing.Color.White;
			this.cboTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTag.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboTag.FormattingEnabled = true;
			this.cboTag.Location = new System.Drawing.Point(82, 162);
			this.cboTag.Name = "cboTag";
			this.cboTag.SelectedIndex = -1;
			this.cboTag.SelectedItem = null;
			this.cboTag.Size = new System.Drawing.Size(270, 21);
			this.cboTag.Sorted = false;
			this.cboTag.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(490, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "File name:";
			// 
			// txtFile
			// 
			this.txtFile.BackColor = System.Drawing.Color.White;
			this.txtFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtFile.ForeColor = System.Drawing.Color.Black;
			this.txtFile.Location = new System.Drawing.Point(551, 25);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(220, 20);
			this.txtFile.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label6.Location = new System.Drawing.Point(6, 54);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(39, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "Group:";
			// 
			// txtGroup
			// 
			this.txtGroup.BackColor = System.Drawing.Color.White;
			this.txtGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtGroup.ForeColor = System.Drawing.Color.Black;
			this.txtGroup.Location = new System.Drawing.Point(82, 51);
			this.txtGroup.Name = "txtGroup";
			this.txtGroup.Size = new System.Drawing.Size(402, 20);
			this.txtGroup.TabIndex = 4;
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox1.Controls.Add(this.tableConditions);
			this.skinnedGroupBox1.Location = new System.Drawing.Point(3, 199);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.Size = new System.Drawing.Size(887, 349);
			this.skinnedGroupBox1.TabIndex = 22;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Conditions";
			// 
			// skinnedGroupBox2
			// 
			this.skinnedGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox2.Controls.Add(this.skinnedLabel1);
			this.skinnedGroupBox2.Controls.Add(this.txtLabel);
			this.skinnedGroupBox2.Controls.Add(this.txtName);
			this.skinnedGroupBox2.Controls.Add(this.label1);
			this.skinnedGroupBox2.Controls.Add(this.label4);
			this.skinnedGroupBox2.Controls.Add(this.cboTag);
			this.skinnedGroupBox2.Controls.Add(this.txtGroup);
			this.skinnedGroupBox2.Controls.Add(this.cmdOpen);
			this.skinnedGroupBox2.Controls.Add(this.label2);
			this.skinnedGroupBox2.Controls.Add(this.txtDescription);
			this.skinnedGroupBox2.Controls.Add(this.label6);
			this.skinnedGroupBox2.Controls.Add(this.label5);
			this.skinnedGroupBox2.Controls.Add(this.txtFile);
			this.skinnedGroupBox2.Location = new System.Drawing.Point(3, 3);
			this.skinnedGroupBox2.Name = "skinnedGroupBox2";
			this.skinnedGroupBox2.Size = new System.Drawing.Size(887, 190);
			this.skinnedGroupBox2.TabIndex = 23;
			this.skinnedGroupBox2.TabStop = false;
			this.skinnedGroupBox2.Text = "Information";
			// 
			// skinnedGroupBox3
			// 
			this.skinnedGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox3.Controls.Add(this.gridLines);
			this.skinnedGroupBox3.Location = new System.Drawing.Point(3, 554);
			this.skinnedGroupBox3.Name = "skinnedGroupBox3";
			this.skinnedGroupBox3.Size = new System.Drawing.Size(887, 124);
			this.skinnedGroupBox3.TabIndex = 24;
			this.skinnedGroupBox3.TabStop = false;
			this.skinnedGroupBox3.Text = "Lines";
			// 
			// gridLines
			// 
			this.gridLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLines.Location = new System.Drawing.Point(6, 23);
			this.gridLines.Name = "gridLines";
			this.gridLines.ReadOnly = false;
			this.gridLines.Size = new System.Drawing.Size(875, 95);
			this.gridLines.TabIndex = 0;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(490, 54);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(36, 13);
			this.skinnedLabel1.TabIndex = 23;
			this.skinnedLabel1.Text = "Label:";
			// 
			// txtLabel
			// 
			this.txtLabel.BackColor = System.Drawing.Color.White;
			this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtLabel.ForeColor = System.Drawing.Color.Black;
			this.txtLabel.Location = new System.Drawing.Point(551, 51);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(220, 20);
			this.txtLabel.TabIndex = 22;
			// 
			// RecipeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedGroupBox3);
			this.Controls.Add(this.skinnedGroupBox2);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Name = "RecipeEditor";
			this.Size = new System.Drawing.Size(893, 681);
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox2.ResumeLayout(false);
			this.skinnedGroupBox2.PerformLayout();
			this.skinnedGroupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedTextBox txtName;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedTextBox txtDescription;
		private Desktop.Skinning.SkinnedButton cmdOpen;
		private Desktop.CommonControls.PropertyTable tableConditions;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedComboBox cboTag;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedTextBox txtFile;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedTextBox txtGroup;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox2;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox3;
		private Controls.DialogueGrid gridLines;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedTextBox txtLabel;
	}
}

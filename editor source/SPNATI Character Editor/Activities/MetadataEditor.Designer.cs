namespace SPNATI_Character_Editor.Activities
{
	partial class MetadataEditor
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.txtLabel = new Desktop.Skinning.SkinnedTextBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.gridAI = new Desktop.Skinning.SkinnedDataGridView();
			this.ColAIStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDifficulty = new Desktop.Skinning.SkinnedDataGridViewComboBoxColumn();
			this.label7 = new Desktop.Skinning.SkinnedLabel();
			this.label22 = new Desktop.Skinning.SkinnedLabel();
			this.txtDescription = new Desktop.Skinning.SkinnedTextBox();
			this.cboDefaultPic = new Desktop.Skinning.SkinnedComboBox();
			this.label24 = new Desktop.Skinning.SkinnedLabel();
			this.txtHeight = new Desktop.Skinning.SkinnedTextBox();
			this.txtLastName = new Desktop.Skinning.SkinnedTextBox();
			this.label23 = new Desktop.Skinning.SkinnedLabel();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.txtFirstName = new Desktop.Skinning.SkinnedTextBox();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.label19 = new Desktop.Skinning.SkinnedLabel();
			this.txtArtist = new Desktop.Skinning.SkinnedTextBox();
			this.label18 = new Desktop.Skinning.SkinnedLabel();
			this.txtWriter = new Desktop.Skinning.SkinnedTextBox();
			this.label17 = new Desktop.Skinning.SkinnedLabel();
			this.txtSource = new Desktop.Skinning.SkinnedTextBox();
			this.valRounds = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label12 = new Desktop.Skinning.SkinnedLabel();
			this.cboSize = new Desktop.Skinning.SkinnedComboBox();
			this.label10 = new Desktop.Skinning.SkinnedLabel();
			this.lblSize = new Desktop.Skinning.SkinnedLabel();
			this.cboGender = new Desktop.Skinning.SkinnedComboBox();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.cboTitleGender = new Desktop.Skinning.SkinnedComboBox();
			this.lblTitleGender = new Desktop.Skinning.SkinnedLabel();
			this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox3 = new Desktop.Skinning.SkinnedGroupBox();
			this.cmdExpandGender = new Desktop.Skinning.SkinnedIcon();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridAI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valRounds)).BeginInit();
			this.skinnedGroupBox1.SuspendLayout();
			this.skinnedGroupBox2.SuspendLayout();
			this.skinnedGroupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLabel
			// 
			this.txtLabel.BackColor = System.Drawing.Color.White;
			this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtLabel.ForeColor = System.Drawing.Color.Black;
			this.txtLabel.Location = new System.Drawing.Point(75, 5);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(100, 20);
			this.txtLabel.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Label:";
			// 
			// gridAI
			// 
			this.gridAI.AllowUserToResizeColumns = false;
			this.gridAI.AllowUserToResizeRows = false;
			this.gridAI.BackgroundColor = System.Drawing.Color.White;
			this.gridAI.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridAI.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridAI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.gridAI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColAIStage,
            this.ColDifficulty});
			this.gridAI.Data = null;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridAI.DefaultCellStyle = dataGridViewCellStyle5;
			this.gridAI.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridAI.EnableHeadersVisualStyles = false;
			this.gridAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridAI.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(153)))), ((int)(((byte)(243)))));
			this.gridAI.Location = new System.Drawing.Point(107, 27);
			this.gridAI.Name = "gridAI";
			this.gridAI.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridAI.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.gridAI.RowHeadersVisible = false;
			this.gridAI.Size = new System.Drawing.Size(212, 101);
			this.gridAI.TabIndex = 17;
			// 
			// ColAIStage
			// 
			this.ColAIStage.DataPropertyName = "Stage";
			this.ColAIStage.HeaderText = "Stage";
			this.ColAIStage.MinimumWidth = 50;
			this.ColAIStage.Name = "ColAIStage";
			this.ColAIStage.Width = 50;
			// 
			// ColDifficulty
			// 
			this.ColDifficulty.AutoComplete = false;
			this.ColDifficulty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColDifficulty.DataPropertyName = "Value";
			this.ColDifficulty.DisplayMember = null;
			this.ColDifficulty.HeaderText = "Intelligence";
			this.ColDifficulty.Name = "ColDifficulty";
			this.ColDifficulty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColDifficulty.Sorted = false;
			this.ColDifficulty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label7.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label7.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label7.Location = new System.Drawing.Point(6, 31);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 106;
			this.label7.Text = "Intelligence:";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label22.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label22.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label22.Location = new System.Drawing.Point(6, 80);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(43, 13);
			this.label22.TabIndex = 94;
			this.label22.Text = "Portrait:";
			// 
			// txtDescription
			// 
			this.txtDescription.BackColor = System.Drawing.Color.White;
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtDescription.ForeColor = System.Drawing.Color.Black;
			this.txtDescription.Location = new System.Drawing.Point(107, 130);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(570, 90);
			this.txtDescription.TabIndex = 14;
			// 
			// cboDefaultPic
			// 
			this.cboDefaultPic.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboDefaultPic.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboDefaultPic.BackColor = System.Drawing.Color.White;
			this.cboDefaultPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDefaultPic.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboDefaultPic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboDefaultPic.FormattingEnabled = true;
			this.cboDefaultPic.KeyMember = null;
			this.cboDefaultPic.Location = new System.Drawing.Point(107, 76);
			this.cboDefaultPic.Name = "cboDefaultPic";
			this.cboDefaultPic.SelectedIndex = -1;
			this.cboDefaultPic.SelectedItem = null;
			this.cboDefaultPic.Size = new System.Drawing.Size(212, 21);
			this.cboDefaultPic.Sorted = false;
			this.cboDefaultPic.TabIndex = 11;
			this.cboDefaultPic.SelectedIndexChanged += new System.EventHandler(this.cboDefaultPic_SelectedIndexChanged);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label24.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label24.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label24.Location = new System.Drawing.Point(6, 134);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(63, 13);
			this.label24.TabIndex = 105;
			this.label24.Text = "Description:";
			// 
			// txtHeight
			// 
			this.txtHeight.BackColor = System.Drawing.Color.White;
			this.txtHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtHeight.ForeColor = System.Drawing.Color.Black;
			this.txtHeight.Location = new System.Drawing.Point(434, 76);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(114, 20);
			this.txtHeight.TabIndex = 12;
			// 
			// txtLastName
			// 
			this.txtLastName.BackColor = System.Drawing.Color.White;
			this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtLastName.ForeColor = System.Drawing.Color.Black;
			this.txtLastName.Location = new System.Drawing.Point(434, 22);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(252, 20);
			this.txtLastName.TabIndex = 7;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label23.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label23.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label23.Location = new System.Drawing.Point(365, 80);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(41, 13);
			this.label23.TabIndex = 104;
			this.label23.Text = "Height:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label3.Location = new System.Drawing.Point(365, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 85;
			this.label3.Text = "Last name:";
			// 
			// txtFirstName
			// 
			this.txtFirstName.BackColor = System.Drawing.Color.White;
			this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtFirstName.ForeColor = System.Drawing.Color.Black;
			this.txtFirstName.Location = new System.Drawing.Point(107, 22);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(252, 20);
			this.txtFirstName.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label2.Location = new System.Drawing.Point(6, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 82;
			this.label2.Text = "First name:";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label19.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label19.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label19.Location = new System.Drawing.Point(6, 54);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(33, 13);
			this.label19.TabIndex = 102;
			this.label19.Text = "Artist:";
			// 
			// txtArtist
			// 
			this.txtArtist.BackColor = System.Drawing.Color.White;
			this.txtArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtArtist.ForeColor = System.Drawing.Color.Black;
			this.txtArtist.Location = new System.Drawing.Point(107, 50);
			this.txtArtist.Name = "txtArtist";
			this.txtArtist.Size = new System.Drawing.Size(212, 20);
			this.txtArtist.TabIndex = 16;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label18.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label18.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label18.Location = new System.Drawing.Point(6, 27);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(38, 13);
			this.label18.TabIndex = 101;
			this.label18.Text = "Writer:";
			// 
			// txtWriter
			// 
			this.txtWriter.BackColor = System.Drawing.Color.White;
			this.txtWriter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtWriter.ForeColor = System.Drawing.Color.Black;
			this.txtWriter.Location = new System.Drawing.Point(107, 23);
			this.txtWriter.Name = "txtWriter";
			this.txtWriter.Size = new System.Drawing.Size(212, 20);
			this.txtWriter.TabIndex = 15;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label17.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label17.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label17.Location = new System.Drawing.Point(6, 107);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(37, 13);
			this.label17.TabIndex = 100;
			this.label17.Text = "Origin:";
			// 
			// txtSource
			// 
			this.txtSource.BackColor = System.Drawing.Color.White;
			this.txtSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtSource.ForeColor = System.Drawing.Color.Black;
			this.txtSource.Location = new System.Drawing.Point(107, 103);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(252, 20);
			this.txtSource.TabIndex = 13;
			// 
			// valRounds
			// 
			this.valRounds.BackColor = System.Drawing.Color.White;
			this.valRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valRounds.ForeColor = System.Drawing.Color.Black;
			this.valRounds.Location = new System.Drawing.Point(107, 134);
			this.valRounds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valRounds.Name = "valRounds";
			this.valRounds.Size = new System.Drawing.Size(62, 20);
			this.valRounds.TabIndex = 18;
			this.valRounds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label12.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label12.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label12.Location = new System.Drawing.Point(6, 137);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(86, 13);
			this.label12.TabIndex = 96;
			this.label12.Text = "Rounds to finish:";
			// 
			// cboSize
			// 
			this.cboSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboSize.BackColor = System.Drawing.Color.White;
			this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSize.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboSize.FormattingEnabled = true;
			this.cboSize.KeyMember = null;
			this.cboSize.Location = new System.Drawing.Point(434, 49);
			this.cboSize.Name = "cboSize";
			this.cboSize.SelectedIndex = -1;
			this.cboSize.SelectedItem = null;
			this.cboSize.Size = new System.Drawing.Size(114, 21);
			this.cboSize.Sorted = false;
			this.cboSize.TabIndex = 10;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label10.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label10.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label10.Location = new System.Drawing.Point(6, 53);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(45, 13);
			this.label10.TabIndex = 90;
			this.label10.Text = "Gender:";
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblSize.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblSize.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.lblSize.Location = new System.Drawing.Point(365, 53);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(30, 13);
			this.lblSize.TabIndex = 92;
			this.lblSize.Text = "Size:";
			// 
			// cboGender
			// 
			this.cboGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboGender.BackColor = System.Drawing.Color.White;
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboGender.FormattingEnabled = true;
			this.cboGender.KeyMember = null;
			this.cboGender.Location = new System.Drawing.Point(107, 49);
			this.cboGender.Name = "cboGender";
			this.cboGender.SelectedIndex = -1;
			this.cboGender.SelectedItem = null;
			this.cboGender.Size = new System.Drawing.Size(101, 21);
			this.cboGender.Sorted = false;
			this.cboGender.TabIndex = 8;
			this.cboGender.SelectedIndexChanged += new System.EventHandler(this.cboGender_SelectedIndexChanged);
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox1.Controls.Add(this.cmdExpandGender);
			this.skinnedGroupBox1.Controls.Add(this.cboTitleGender);
			this.skinnedGroupBox1.Controls.Add(this.lblTitleGender);
			this.skinnedGroupBox1.Controls.Add(this.label2);
			this.skinnedGroupBox1.Controls.Add(this.txtFirstName);
			this.skinnedGroupBox1.Controls.Add(this.label3);
			this.skinnedGroupBox1.Controls.Add(this.cboSize);
			this.skinnedGroupBox1.Controls.Add(this.txtDescription);
			this.skinnedGroupBox1.Controls.Add(this.lblSize);
			this.skinnedGroupBox1.Controls.Add(this.label22);
			this.skinnedGroupBox1.Controls.Add(this.cboDefaultPic);
			this.skinnedGroupBox1.Controls.Add(this.txtLastName);
			this.skinnedGroupBox1.Controls.Add(this.label24);
			this.skinnedGroupBox1.Controls.Add(this.label10);
			this.skinnedGroupBox1.Controls.Add(this.txtHeight);
			this.skinnedGroupBox1.Controls.Add(this.cboGender);
			this.skinnedGroupBox1.Controls.Add(this.label23);
			this.skinnedGroupBox1.Controls.Add(this.txtSource);
			this.skinnedGroupBox1.Controls.Add(this.label17);
			this.skinnedGroupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox1.Image = null;
			this.skinnedGroupBox1.Location = new System.Drawing.Point(6, 30);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox1.ShowIndicatorBar = false;
			this.skinnedGroupBox1.Size = new System.Drawing.Size(696, 231);
			this.skinnedGroupBox1.TabIndex = 108;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Demographics";
			// 
			// cboTitleGender
			// 
			this.cboTitleGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboTitleGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboTitleGender.BackColor = System.Drawing.Color.White;
			this.cboTitleGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTitleGender.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboTitleGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboTitleGender.FormattingEnabled = true;
			this.cboTitleGender.KeyMember = null;
			this.cboTitleGender.Location = new System.Drawing.Point(258, 49);
			this.cboTitleGender.Name = "cboTitleGender";
			this.cboTitleGender.SelectedIndex = -1;
			this.cboTitleGender.SelectedItem = null;
			this.cboTitleGender.Size = new System.Drawing.Size(101, 21);
			this.cboTitleGender.Sorted = false;
			this.cboTitleGender.TabIndex = 9;
			this.toolTip1.SetToolTip(this.cboTitleGender, "Gender as it displays for character selection");
			this.cboTitleGender.Visible = false;
			// 
			// lblTitleGender
			// 
			this.lblTitleGender.AutoSize = true;
			this.lblTitleGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblTitleGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblTitleGender.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblTitleGender.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.lblTitleGender.Location = new System.Drawing.Point(214, 53);
			this.lblTitleGender.Name = "lblTitleGender";
			this.lblTitleGender.Size = new System.Drawing.Size(43, 13);
			this.lblTitleGender.TabIndex = 106;
			this.lblTitleGender.Text = "On title:";
			this.lblTitleGender.Visible = false;
			// 
			// skinnedGroupBox2
			// 
			this.skinnedGroupBox2.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox2.Controls.Add(this.label18);
			this.skinnedGroupBox2.Controls.Add(this.txtWriter);
			this.skinnedGroupBox2.Controls.Add(this.label19);
			this.skinnedGroupBox2.Controls.Add(this.txtArtist);
			this.skinnedGroupBox2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox2.Image = null;
			this.skinnedGroupBox2.Location = new System.Drawing.Point(6, 267);
			this.skinnedGroupBox2.Name = "skinnedGroupBox2";
			this.skinnedGroupBox2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox2.ShowIndicatorBar = false;
			this.skinnedGroupBox2.Size = new System.Drawing.Size(696, 80);
			this.skinnedGroupBox2.TabIndex = 109;
			this.skinnedGroupBox2.TabStop = false;
			this.skinnedGroupBox2.Text = "Credits";
			// 
			// skinnedGroupBox3
			// 
			this.skinnedGroupBox3.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox3.Controls.Add(this.label12);
			this.skinnedGroupBox3.Controls.Add(this.valRounds);
			this.skinnedGroupBox3.Controls.Add(this.gridAI);
			this.skinnedGroupBox3.Controls.Add(this.label7);
			this.skinnedGroupBox3.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox3.Image = null;
			this.skinnedGroupBox3.Location = new System.Drawing.Point(6, 353);
			this.skinnedGroupBox3.Name = "skinnedGroupBox3";
			this.skinnedGroupBox3.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox3.ShowIndicatorBar = false;
			this.skinnedGroupBox3.Size = new System.Drawing.Size(696, 163);
			this.skinnedGroupBox3.TabIndex = 110;
			this.skinnedGroupBox3.TabStop = false;
			this.skinnedGroupBox3.Text = "Gameplay";
			// 
			// cmdExpandGender
			// 
			this.cmdExpandGender.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdExpandGender.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdExpandGender.Flat = false;
			this.cmdExpandGender.Image = global::SPNATI_Character_Editor.Properties.Resources.Expand;
			this.cmdExpandGender.Location = new System.Drawing.Point(214, 51);
			this.cmdExpandGender.Name = "cmdExpandGender";
			this.cmdExpandGender.Size = new System.Drawing.Size(16, 16);
			this.cmdExpandGender.TabIndex = 108;
			this.cmdExpandGender.Text = "skinnedIcon1";
			this.toolTip1.SetToolTip(this.cmdExpandGender, "Show advanced gender options");
			this.cmdExpandGender.UseVisualStyleBackColor = true;
			this.cmdExpandGender.Click += new System.EventHandler(this.cmdExpandGender_Click);
			// 
			// MetadataEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedGroupBox3);
			this.Controls.Add(this.skinnedGroupBox2);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Controls.Add(this.txtLabel);
			this.Controls.Add(this.label1);
			this.Name = "MetadataEditor";
			this.Size = new System.Drawing.Size(897, 589);
			((System.ComponentModel.ISupportInitialize)(this.gridAI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valRounds)).EndInit();
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox1.PerformLayout();
			this.skinnedGroupBox2.ResumeLayout(false);
			this.skinnedGroupBox2.PerformLayout();
			this.skinnedGroupBox3.ResumeLayout(false);
			this.skinnedGroupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedTextBox txtLabel;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedDataGridView gridAI;
		private Desktop.Skinning.SkinnedLabel label7;
		private Desktop.Skinning.SkinnedLabel label22;
		private Desktop.Skinning.SkinnedTextBox txtDescription;
		private Desktop.Skinning.SkinnedComboBox cboDefaultPic;
		private Desktop.Skinning.SkinnedLabel label24;
		private Desktop.Skinning.SkinnedTextBox txtHeight;
		private Desktop.Skinning.SkinnedTextBox txtLastName;
		private Desktop.Skinning.SkinnedLabel label23;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedTextBox txtFirstName;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedLabel label19;
		private Desktop.Skinning.SkinnedTextBox txtArtist;
		private Desktop.Skinning.SkinnedLabel label18;
		private Desktop.Skinning.SkinnedTextBox txtWriter;
		private Desktop.Skinning.SkinnedLabel label17;
		private Desktop.Skinning.SkinnedTextBox txtSource;
		private Desktop.Skinning.SkinnedNumericUpDown valRounds;
		private Desktop.Skinning.SkinnedLabel label12;
		private Desktop.Skinning.SkinnedComboBox cboSize;
		private Desktop.Skinning.SkinnedLabel label10;
		private Desktop.Skinning.SkinnedLabel lblSize;
		private Desktop.Skinning.SkinnedComboBox cboGender;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox2;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox3;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAIStage;
		private Desktop.Skinning.SkinnedDataGridViewComboBoxColumn ColDifficulty;
		private Desktop.Skinning.SkinnedComboBox cboTitleGender;
		private Desktop.Skinning.SkinnedLabel lblTitleGender;
		private Desktop.Skinning.SkinnedIcon cmdExpandGender;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

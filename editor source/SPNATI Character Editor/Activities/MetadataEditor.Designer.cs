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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.valPicScale = new Desktop.Skinning.SkinnedNumericUpDown();
            this.lblPicScale = new Desktop.Skinning.SkinnedLabel();
            this.valPicY = new Desktop.Skinning.SkinnedNumericUpDown();
            this.lblPicY = new Desktop.Skinning.SkinnedLabel();
            this.valPicX = new Desktop.Skinning.SkinnedNumericUpDown();
            this.cmdExpandPicOptions = new Desktop.Skinning.SkinnedIcon();
            this.lblPicX = new Desktop.Skinning.SkinnedLabel();
            this.cmdExpandGender = new Desktop.Skinning.SkinnedIcon();
            this.cboTitleGender = new Desktop.Skinning.SkinnedComboBox();
            this.lblTitleGender = new Desktop.Skinning.SkinnedLabel();
            this.agelabel = new Desktop.Skinning.SkinnedLabel();
            this.txtAge = new Desktop.Skinning.SkinnedTextBox();
            this.pronunciationguidelabel = new Desktop.Skinning.SkinnedLabel();
            this.txtpronunciationGuide = new Desktop.Skinning.SkinnedTextBox();
            this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
            this.skinnedGroupBox3 = new Desktop.Skinning.SkinnedGroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdExpandLabel = new Desktop.Skinning.SkinnedIcon();
            this.txtTitleLabel = new Desktop.Skinning.SkinnedTextBox();
            this.txtOtherNotes = new Desktop.Skinning.SkinnedTextBox();
            this.lblTitleLabel = new Desktop.Skinning.SkinnedLabel();
            this.skinnedGroupBox4 = new Desktop.Skinning.SkinnedGroupBox();
            this.skinnedGroupBox5 = new Desktop.Skinning.SkinnedGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridAI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valRounds)).BeginInit();
            this.skinnedGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valPicScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valPicY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valPicX)).BeginInit();
            this.skinnedGroupBox2.SuspendLayout();
            this.skinnedGroupBox3.SuspendLayout();
            this.skinnedGroupBox4.SuspendLayout();
            this.skinnedGroupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLabel
            // 
            this.txtLabel.BackColor = System.Drawing.Color.White;
            this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtLabel.ForeColor = System.Drawing.Color.Black;
            this.txtLabel.Location = new System.Drawing.Point(45, 5);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(100, 20);
            this.txtLabel.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtLabel, "The name other characters will call this character, instead of their first name.");
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.gridAI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColAIStage,
            this.ColDifficulty});
            this.gridAI.Data = null;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridAI.DefaultCellStyle = dataGridViewCellStyle14;
            this.gridAI.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridAI.EnableHeadersVisualStyles = false;
            this.gridAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridAI.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(153)))), ((int)(((byte)(243)))));
            this.gridAI.Location = new System.Drawing.Point(107, 27);
            this.gridAI.Name = "gridAI";
            this.gridAI.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAI.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.gridAI.RowHeadersVisible = false;
            this.gridAI.Size = new System.Drawing.Size(230, 125);
            this.gridAI.TabIndex = 18;
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
            this.txtDescription.TabIndex = 15;
            this.toolTip1.SetToolTip(this.txtDescription, "A description of the character which will be displayed on the character select sc" +
        "reen.");
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
            this.cboDefaultPic.Size = new System.Drawing.Size(252, 21);
            this.cboDefaultPic.Sorted = false;
            this.cboDefaultPic.TabIndex = 12;
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
            this.txtHeight.Location = new System.Drawing.Point(107, 44);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(101, 20);
            this.txtHeight.TabIndex = 13;
            this.toolTip1.SetToolTip(this.txtHeight, "The character\'s height as portrayed in SPNATI. Does not show up in-game.");
            // 
            // txtLastName
            // 
            this.txtLastName.BackColor = System.Drawing.Color.White;
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtLastName.ForeColor = System.Drawing.Color.Black;
            this.txtLastName.Location = new System.Drawing.Point(434, 22);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(252, 20);
            this.txtLastName.TabIndex = 8;
            this.toolTip1.SetToolTip(this.txtLastName, "The character\'s surname, if they have one.");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label23.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.label23.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
            this.label23.Location = new System.Drawing.Point(6, 47);
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
            this.txtFirstName.TabIndex = 7;
            this.toolTip1.SetToolTip(this.txtFirstName, "The character\'s first name.");
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
            this.label19.Location = new System.Drawing.Point(8, 60);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 13);
            this.label19.TabIndex = 102;
            this.label19.Text = "Artist(s):";
            // 
            // txtArtist
            // 
            this.txtArtist.BackColor = System.Drawing.Color.White;
            this.txtArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtArtist.ForeColor = System.Drawing.Color.Black;
            this.txtArtist.Location = new System.Drawing.Point(107, 60);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(252, 20);
            this.txtArtist.TabIndex = 17;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label18.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.label18.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
            this.label18.Location = new System.Drawing.Point(8, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 13);
            this.label18.TabIndex = 101;
            this.label18.Text = "Writer(s):";
            // 
            // txtWriter
            // 
            this.txtWriter.BackColor = System.Drawing.Color.White;
            this.txtWriter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtWriter.ForeColor = System.Drawing.Color.Black;
            this.txtWriter.Location = new System.Drawing.Point(107, 33);
            this.txtWriter.Name = "txtWriter";
            this.txtWriter.Size = new System.Drawing.Size(252, 20);
            this.txtWriter.TabIndex = 16;
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
            this.txtSource.TabIndex = 14;
            this.toolTip1.SetToolTip(this.txtSource, "The character\'s source material.");
            // 
            // valRounds
            // 
            this.valRounds.BackColor = System.Drawing.Color.White;
            this.valRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.valRounds.ForeColor = System.Drawing.Color.Black;
            this.valRounds.Location = new System.Drawing.Point(107, 158);
            this.valRounds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.valRounds.Name = "valRounds";
            this.valRounds.Size = new System.Drawing.Size(62, 20);
            this.valRounds.TabIndex = 19;
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
            this.label12.Location = new System.Drawing.Point(6, 161);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 96;
            this.label12.Text = "Phases to finish:";
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
            this.cboSize.TabIndex = 11;
            this.toolTip1.SetToolTip(this.cboSize, "The Size of a character\'s penis or breasts.");
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
            this.cboGender.TabIndex = 9;
            this.toolTip1.SetToolTip(this.cboGender, "The gender of the character.");
            this.cboGender.SelectedIndexChanged += new System.EventHandler(this.cboGender_SelectedIndexChanged);
            // 
            // skinnedGroupBox1
            // 
            this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
            this.skinnedGroupBox1.Controls.Add(this.valPicScale);
            this.skinnedGroupBox1.Controls.Add(this.lblPicScale);
            this.skinnedGroupBox1.Controls.Add(this.valPicY);
            this.skinnedGroupBox1.Controls.Add(this.lblPicY);
            this.skinnedGroupBox1.Controls.Add(this.valPicX);
            this.skinnedGroupBox1.Controls.Add(this.cmdExpandPicOptions);
            this.skinnedGroupBox1.Controls.Add(this.lblPicX);
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
            this.skinnedGroupBox1.Controls.Add(this.cboGender);
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
            // valPicScale
            // 
            this.valPicScale.BackColor = System.Drawing.Color.White;
            this.valPicScale.DecimalPlaces = 1;
            this.valPicScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.valPicScale.ForeColor = System.Drawing.Color.Black;
            this.valPicScale.Location = new System.Drawing.Point(627, 76);
            this.valPicScale.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.valPicScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.valPicScale.Name = "valPicScale";
            this.valPicScale.Size = new System.Drawing.Size(61, 20);
            this.valPicScale.TabIndex = 115;
            this.toolTip1.SetToolTip(this.valPicScale, "Scaling factor for the character\'s image on the selection screen.");
            this.valPicScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.valPicScale.Visible = false;
            // 
            // lblPicScale
            // 
            this.lblPicScale.AutoSize = true;
            this.lblPicScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblPicScale.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPicScale.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblPicScale.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblPicScale.Location = new System.Drawing.Point(567, 80);
            this.lblPicScale.Name = "lblPicScale";
            this.lblPicScale.Size = new System.Drawing.Size(54, 13);
            this.lblPicScale.TabIndex = 114;
            this.lblPicScale.Text = "Scale (%):";
            this.lblPicScale.Visible = false;
            // 
            // valPicY
            // 
            this.valPicY.BackColor = System.Drawing.Color.White;
            this.valPicY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.valPicY.ForeColor = System.Drawing.Color.Black;
            this.valPicY.Location = new System.Drawing.Point(500, 76);
            this.valPicY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.valPicY.Name = "valPicY";
            this.valPicY.Size = new System.Drawing.Size(61, 20);
            this.valPicY.TabIndex = 113;
            this.toolTip1.SetToolTip(this.valPicY, "Offset for the character\'s image on the selection screen. Positive values move th" +
        "e image up.");
            this.valPicY.Visible = false;
            // 
            // lblPicY
            // 
            this.lblPicY.AutoSize = true;
            this.lblPicY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblPicY.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPicY.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblPicY.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblPicY.Location = new System.Drawing.Point(477, 80);
            this.lblPicY.Name = "lblPicY";
            this.lblPicY.Size = new System.Drawing.Size(17, 13);
            this.lblPicY.TabIndex = 112;
            this.lblPicY.Text = "Y:";
            this.lblPicY.Visible = false;
            // 
            // valPicX
            // 
            this.valPicX.BackColor = System.Drawing.Color.White;
            this.valPicX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.valPicX.ForeColor = System.Drawing.Color.Black;
            this.valPicX.Location = new System.Drawing.Point(410, 76);
            this.valPicX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.valPicX.Name = "valPicX";
            this.valPicX.Size = new System.Drawing.Size(61, 20);
            this.valPicX.TabIndex = 111;
            this.toolTip1.SetToolTip(this.valPicX, "Offset for the character\'s image on the selection screen. Positive values move th" +
        "e image to the right.");
            this.valPicX.Visible = false;
            // 
            // cmdExpandPicOptions
            // 
            this.cmdExpandPicOptions.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdExpandPicOptions.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
            this.cmdExpandPicOptions.Flat = false;
            this.cmdExpandPicOptions.Image = global::SPNATI_Character_Editor.Properties.Resources.Expand;
            this.cmdExpandPicOptions.Location = new System.Drawing.Point(365, 78);
            this.cmdExpandPicOptions.Name = "cmdExpandPicOptions";
            this.cmdExpandPicOptions.Size = new System.Drawing.Size(16, 16);
            this.cmdExpandPicOptions.TabIndex = 110;
            this.cmdExpandPicOptions.Text = "skinnedIcon1";
            this.toolTip1.SetToolTip(this.cmdExpandPicOptions, "Show advanced select screen options");
            this.cmdExpandPicOptions.UseVisualStyleBackColor = true;
            this.cmdExpandPicOptions.Click += new System.EventHandler(this.cmdExpandPicOptions_Click);
            // 
            // lblPicX
            // 
            this.lblPicX.AutoSize = true;
            this.lblPicX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblPicX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPicX.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblPicX.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblPicX.Location = new System.Drawing.Point(387, 80);
            this.lblPicX.Name = "lblPicX";
            this.lblPicX.Size = new System.Drawing.Size(17, 13);
            this.lblPicX.TabIndex = 109;
            this.lblPicX.Text = "X:";
            this.lblPicX.Visible = false;
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
            this.cboTitleGender.TabIndex = 10;
            this.toolTip1.SetToolTip(this.cboTitleGender, "Gender as it displays for the character selection screen.");
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
            // agelabel
            // 
            this.agelabel.AutoSize = true;
            this.agelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.agelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.agelabel.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.agelabel.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
            this.agelabel.Location = new System.Drawing.Point(6, 71);
            this.agelabel.Name = "agelabel";
            this.agelabel.Size = new System.Drawing.Size(29, 13);
            this.agelabel.TabIndex = 110;
            this.agelabel.Text = "Age:";
            // 
            // txtAge
            // 
            this.txtAge.BackColor = System.Drawing.Color.White;
            this.txtAge.ForeColor = System.Drawing.Color.Black;
            this.txtAge.Location = new System.Drawing.Point(107, 68);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(101, 20);
            this.txtAge.TabIndex = 109;
            this.toolTip1.SetToolTip(this.txtAge, "The character\'s age as portrayed in SPNATI. Does not show up in-game.");
            // 
            // pronunciationguidelabel
            // 
            this.pronunciationguidelabel.AutoSize = true;
            this.pronunciationguidelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pronunciationguidelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pronunciationguidelabel.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.pronunciationguidelabel.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
            this.pronunciationguidelabel.Location = new System.Drawing.Point(6, 97);
            this.pronunciationguidelabel.Name = "pronunciationguidelabel";
            this.pronunciationguidelabel.Size = new System.Drawing.Size(75, 13);
            this.pronunciationguidelabel.TabIndex = 111;
            this.pronunciationguidelabel.Text = "Pronunciation:";
            // 
            // txtpronunciationGuide
            // 
            this.txtpronunciationGuide.BackColor = System.Drawing.Color.White;
            this.txtpronunciationGuide.ForeColor = System.Drawing.Color.Black;
            this.txtpronunciationGuide.Location = new System.Drawing.Point(107, 94);
            this.txtpronunciationGuide.Name = "txtpronunciationGuide";
            this.txtpronunciationGuide.Size = new System.Drawing.Size(101, 20);
            this.txtpronunciationGuide.TabIndex = 109;
            this.toolTip1.SetToolTip(this.txtpronunciationGuide, "A pronounciation guide for the character\'s name. Does not show up in-game.");
            // 
            // skinnedGroupBox2
            // 
            this.skinnedGroupBox2.BackColor = System.Drawing.Color.White;
            this.skinnedGroupBox2.Controls.Add(this.label19);
            this.skinnedGroupBox2.Controls.Add(this.txtArtist);
            this.skinnedGroupBox2.Controls.Add(this.label18);
            this.skinnedGroupBox2.Controls.Add(this.txtWriter);
            this.skinnedGroupBox2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
            this.skinnedGroupBox2.Image = null;
            this.skinnedGroupBox2.Location = new System.Drawing.Point(6, 463);
            this.skinnedGroupBox2.Name = "skinnedGroupBox2";
            this.skinnedGroupBox2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.skinnedGroupBox2.ShowIndicatorBar = false;
            this.skinnedGroupBox2.Size = new System.Drawing.Size(696, 101);
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
            this.skinnedGroupBox3.Location = new System.Drawing.Point(242, 268);
            this.skinnedGroupBox3.Name = "skinnedGroupBox3";
            this.skinnedGroupBox3.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.skinnedGroupBox3.ShowIndicatorBar = false;
            this.skinnedGroupBox3.Size = new System.Drawing.Size(460, 189);
            this.skinnedGroupBox3.TabIndex = 110;
            this.skinnedGroupBox3.TabStop = false;
            this.skinnedGroupBox3.Text = "Gameplay";
            // 
            // cmdExpandLabel
            // 
            this.cmdExpandLabel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdExpandLabel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
            this.cmdExpandLabel.Flat = false;
            this.cmdExpandLabel.Image = global::SPNATI_Character_Editor.Properties.Resources.Expand;
            this.cmdExpandLabel.Location = new System.Drawing.Point(151, 6);
            this.cmdExpandLabel.Name = "cmdExpandLabel";
            this.cmdExpandLabel.Size = new System.Drawing.Size(16, 16);
            this.cmdExpandLabel.TabIndex = 109;
            this.cmdExpandLabel.Text = "skinnedIcon1";
            this.toolTip1.SetToolTip(this.cmdExpandLabel, "Show advanced label options");
            this.cmdExpandLabel.UseVisualStyleBackColor = true;
            this.cmdExpandLabel.Click += new System.EventHandler(this.cmdExpandLabel_Click);
            // 
            // txtTitleLabel
            // 
            this.txtTitleLabel.BackColor = System.Drawing.Color.White;
            this.txtTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtTitleLabel.ForeColor = System.Drawing.Color.Black;
            this.txtTitleLabel.Location = new System.Drawing.Point(200, 5);
            this.txtTitleLabel.Name = "txtTitleLabel";
            this.txtTitleLabel.Size = new System.Drawing.Size(100, 20);
            this.txtTitleLabel.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtTitleLabel, "The character\'s name shown on the character select screen.");
            this.txtTitleLabel.Visible = false;
            // 
            // txtOtherNotes
            // 
            this.txtOtherNotes.AcceptsReturn = true;
            this.txtOtherNotes.BackColor = System.Drawing.Color.White;
            this.txtOtherNotes.ForeColor = System.Drawing.Color.Black;
            this.txtOtherNotes.Location = new System.Drawing.Point(98, 28);
            this.txtOtherNotes.Multiline = true;
            this.txtOtherNotes.Name = "txtOtherNotes";
            this.txtOtherNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOtherNotes.Size = new System.Drawing.Size(570, 223);
            this.txtOtherNotes.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtOtherNotes, "This information is only seen in this editor, and could be used for taking notes," +
        " giving tips to other writers wanting to target your character, etc.");
            // 
            // lblTitleLabel
            // 
            this.lblTitleLabel.AutoSize = true;
            this.lblTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitleLabel.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblTitleLabel.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
            this.lblTitleLabel.Location = new System.Drawing.Point(151, 8);
            this.lblTitleLabel.Name = "lblTitleLabel";
            this.lblTitleLabel.Size = new System.Drawing.Size(43, 13);
            this.lblTitleLabel.TabIndex = 109;
            this.lblTitleLabel.Text = "On title:";
            this.lblTitleLabel.Visible = false;
            // 
            // skinnedGroupBox4
            // 
            this.skinnedGroupBox4.BackColor = System.Drawing.Color.White;
            this.skinnedGroupBox4.Controls.Add(this.agelabel);
            this.skinnedGroupBox4.Controls.Add(this.pronunciationguidelabel);
            this.skinnedGroupBox4.Controls.Add(this.txtAge);
            this.skinnedGroupBox4.Controls.Add(this.txtpronunciationGuide);
            this.skinnedGroupBox4.Controls.Add(this.label23);
            this.skinnedGroupBox4.Controls.Add(this.txtHeight);
            this.skinnedGroupBox4.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
            this.skinnedGroupBox4.Image = null;
            this.skinnedGroupBox4.Location = new System.Drawing.Point(6, 268);
            this.skinnedGroupBox4.Name = "skinnedGroupBox4";
            this.skinnedGroupBox4.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.skinnedGroupBox4.ShowIndicatorBar = false;
            this.skinnedGroupBox4.Size = new System.Drawing.Size(230, 189);
            this.skinnedGroupBox4.TabIndex = 112;
            this.skinnedGroupBox4.TabStop = false;
            this.skinnedGroupBox4.Text = "Bio";
            // 
            // skinnedGroupBox5
            // 
            this.skinnedGroupBox5.BackColor = System.Drawing.Color.White;
            this.skinnedGroupBox5.Controls.Add(this.txtOtherNotes);
            this.skinnedGroupBox5.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
            this.skinnedGroupBox5.Image = null;
            this.skinnedGroupBox5.Location = new System.Drawing.Point(6, 570);
            this.skinnedGroupBox5.Name = "skinnedGroupBox5";
            this.skinnedGroupBox5.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.skinnedGroupBox5.ShowIndicatorBar = false;
            this.skinnedGroupBox5.Size = new System.Drawing.Size(696, 271);
            this.skinnedGroupBox5.TabIndex = 113;
            this.skinnedGroupBox5.TabStop = false;
            this.skinnedGroupBox5.Text = "Other Notes";
            // 
            // MetadataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.skinnedGroupBox5);
            this.Controls.Add(this.skinnedGroupBox3);
            this.Controls.Add(this.skinnedGroupBox4);
            this.Controls.Add(this.txtTitleLabel);
            this.Controls.Add(this.lblTitleLabel);
            this.Controls.Add(this.cmdExpandLabel);
            this.Controls.Add(this.skinnedGroupBox2);
            this.Controls.Add(this.skinnedGroupBox1);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.label1);
            this.Name = "MetadataEditor";
            this.Size = new System.Drawing.Size(900, 1100);
            ((System.ComponentModel.ISupportInitialize)(this.gridAI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valRounds)).EndInit();
            this.skinnedGroupBox1.ResumeLayout(false);
            this.skinnedGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valPicScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valPicY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valPicX)).EndInit();
            this.skinnedGroupBox2.ResumeLayout(false);
            this.skinnedGroupBox2.PerformLayout();
            this.skinnedGroupBox3.ResumeLayout(false);
            this.skinnedGroupBox3.PerformLayout();
            this.skinnedGroupBox4.ResumeLayout(false);
            this.skinnedGroupBox4.PerformLayout();
            this.skinnedGroupBox5.ResumeLayout(false);
            this.skinnedGroupBox5.PerformLayout();
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
        private Desktop.Skinning.SkinnedIcon cmdExpandLabel;
        private Desktop.Skinning.SkinnedLabel lblTitleLabel;
        private Desktop.Skinning.SkinnedTextBox txtTitleLabel;
        private Desktop.Skinning.SkinnedLabel agelabel;
        private Desktop.Skinning.SkinnedLabel pronunciationguidelabel;
        private Desktop.Skinning.SkinnedTextBox txtAge;
        private Desktop.Skinning.SkinnedTextBox txtpronunciationGuide;
        private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox4;
        private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox5;
        private Desktop.Skinning.SkinnedTextBox txtOtherNotes;
        private Desktop.Skinning.SkinnedNumericUpDown valPicX;
        private Desktop.Skinning.SkinnedIcon cmdExpandPicOptions;
        private Desktop.Skinning.SkinnedLabel lblPicX;
        private Desktop.Skinning.SkinnedLabel lblPicY;
        private Desktop.Skinning.SkinnedLabel lblPicScale;
        private Desktop.Skinning.SkinnedNumericUpDown valPicY;
        private Desktop.Skinning.SkinnedNumericUpDown valPicScale;
    }
}

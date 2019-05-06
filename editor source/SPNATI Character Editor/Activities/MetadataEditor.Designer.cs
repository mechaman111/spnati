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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.txtLabel = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblUnlisted = new System.Windows.Forms.Label();
			this.lblTesting = new System.Windows.Forms.Label();
			this.lblIncomplete = new System.Windows.Forms.Label();
			this.lblOffline = new System.Windows.Forms.Label();
			this.gridAI = new System.Windows.Forms.DataGridView();
			this.label7 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.cboDefaultPic = new System.Windows.Forms.ComboBox();
			this.label24 = new System.Windows.Forms.Label();
			this.txtHeight = new System.Windows.Forms.TextBox();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.txtArtist = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.txtWriter = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.valRounds = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.cboSize = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.cboGender = new System.Windows.Forms.ComboBox();
			this.ColAIStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDifficulty = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridAI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valRounds)).BeginInit();
			this.SuspendLayout();
			// 
			// txtLabel
			// 
			this.txtLabel.Location = new System.Drawing.Point(72, 3);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(100, 20);
			this.txtLabel.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Label:";
			// 
			// lblUnlisted
			// 
			this.lblUnlisted.AutoSize = true;
			this.lblUnlisted.ForeColor = System.Drawing.Color.DarkOrange;
			this.lblUnlisted.Location = new System.Drawing.Point(178, 6);
			this.lblUnlisted.Name = "lblUnlisted";
			this.lblUnlisted.Size = new System.Drawing.Size(130, 13);
			this.lblUnlisted.TabIndex = 22;
			this.lblUnlisted.Text = "UNLISTED CHARACTER";
			// 
			// lblTesting
			// 
			this.lblTesting.AutoSize = true;
			this.lblTesting.ForeColor = System.Drawing.Color.Olive;
			this.lblTesting.Location = new System.Drawing.Point(178, 6);
			this.lblTesting.Name = "lblTesting";
			this.lblTesting.Size = new System.Drawing.Size(104, 13);
			this.lblTesting.TabIndex = 21;
			this.lblTesting.Text = "TEST CHARACTER";
			// 
			// lblIncomplete
			// 
			this.lblIncomplete.AutoSize = true;
			this.lblIncomplete.ForeColor = System.Drawing.Color.Maroon;
			this.lblIncomplete.Location = new System.Drawing.Point(178, 6);
			this.lblIncomplete.Name = "lblIncomplete";
			this.lblIncomplete.Size = new System.Drawing.Size(145, 13);
			this.lblIncomplete.TabIndex = 20;
			this.lblIncomplete.Text = "INCOMPLETE CHARACTER";
			// 
			// lblOffline
			// 
			this.lblOffline.AutoSize = true;
			this.lblOffline.ForeColor = System.Drawing.SystemColors.Highlight;
			this.lblOffline.Location = new System.Drawing.Point(178, 6);
			this.lblOffline.Name = "lblOffline";
			this.lblOffline.Size = new System.Drawing.Size(120, 13);
			this.lblOffline.TabIndex = 19;
			this.lblOffline.Text = "OFFLINE CHARACTER";
			// 
			// gridAI
			// 
			this.gridAI.AllowUserToResizeColumns = false;
			this.gridAI.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridAI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridAI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColAIStage,
            this.ColDifficulty});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridAI.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridAI.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridAI.Location = new System.Drawing.Point(363, 161);
			this.gridAI.Name = "gridAI";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridAI.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridAI.RowHeadersVisible = false;
			this.gridAI.Size = new System.Drawing.Size(212, 101);
			this.gridAI.TabIndex = 107;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(293, 165);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 106;
			this.label7.Text = "Intelligence:";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(3, 85);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(43, 13);
			this.label22.TabIndex = 94;
			this.label22.Text = "Portrait:";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(72, 268);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(503, 90);
			this.txtDescription.TabIndex = 99;
			// 
			// cboDefaultPic
			// 
			this.cboDefaultPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDefaultPic.FormattingEnabled = true;
			this.cboDefaultPic.Location = new System.Drawing.Point(72, 82);
			this.cboDefaultPic.Name = "cboDefaultPic";
			this.cboDefaultPic.Size = new System.Drawing.Size(156, 21);
			this.cboDefaultPic.TabIndex = 89;
			this.cboDefaultPic.SelectedIndexChanged += new System.EventHandler(this.cboDefaultPic_SelectedIndexChanged);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(3, 271);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(63, 13);
			this.label24.TabIndex = 105;
			this.label24.Text = "Description:";
			// 
			// txtHeight
			// 
			this.txtHeight.Location = new System.Drawing.Point(363, 135);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(62, 20);
			this.txtHeight.TabIndex = 97;
			// 
			// txtLastName
			// 
			this.txtLastName.Location = new System.Drawing.Point(243, 29);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(100, 20);
			this.txtLastName.TabIndex = 84;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(293, 138);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(41, 13);
			this.label23.TabIndex = 104;
			this.label23.Text = "Height:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(178, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 85;
			this.label3.Text = "Last name:";
			// 
			// txtFirstName
			// 
			this.txtFirstName.Location = new System.Drawing.Point(72, 29);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(100, 20);
			this.txtFirstName.TabIndex = 83;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 82;
			this.label2.Text = "First name:";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(293, 112);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(33, 13);
			this.label19.TabIndex = 102;
			this.label19.Text = "Artist:";
			// 
			// txtArtist
			// 
			this.txtArtist.Location = new System.Drawing.Point(363, 109);
			this.txtArtist.Name = "txtArtist";
			this.txtArtist.Size = new System.Drawing.Size(212, 20);
			this.txtArtist.TabIndex = 93;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(3, 112);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(38, 13);
			this.label18.TabIndex = 101;
			this.label18.Text = "Writer:";
			// 
			// txtWriter
			// 
			this.txtWriter.Location = new System.Drawing.Point(72, 109);
			this.txtWriter.Name = "txtWriter";
			this.txtWriter.Size = new System.Drawing.Size(212, 20);
			this.txtWriter.TabIndex = 91;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(3, 138);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(37, 13);
			this.label17.TabIndex = 100;
			this.label17.Text = "Origin:";
			// 
			// txtSource
			// 
			this.txtSource.Location = new System.Drawing.Point(72, 135);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(212, 20);
			this.txtSource.TabIndex = 95;
			// 
			// valRounds
			// 
			this.valRounds.Location = new System.Drawing.Point(452, 56);
			this.valRounds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valRounds.Name = "valRounds";
			this.valRounds.Size = new System.Drawing.Size(59, 20);
			this.valRounds.TabIndex = 88;
			this.valRounds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(360, 58);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(86, 13);
			this.label12.TabIndex = 96;
			this.label12.Text = "Rounds to finish:";
			// 
			// cboSize
			// 
			this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSize.FormattingEnabled = true;
			this.cboSize.Items.AddRange(new object[] {
            "small",
            "medium",
            "large"});
			this.cboSize.Location = new System.Drawing.Point(243, 55);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(100, 21);
			this.cboSize.TabIndex = 87;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 58);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(45, 13);
			this.label10.TabIndex = 90;
			this.label10.Text = "Gender:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(178, 58);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(30, 13);
			this.label11.TabIndex = 92;
			this.label11.Text = "Size:";
			// 
			// cboGender
			// 
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FormattingEnabled = true;
			this.cboGender.Items.AddRange(new object[] {
            "female",
            "male"});
			this.cboGender.Location = new System.Drawing.Point(72, 55);
			this.cboGender.Name = "cboGender";
			this.cboGender.Size = new System.Drawing.Size(100, 21);
			this.cboGender.TabIndex = 86;
			// 
			// ColAIStage
			// 
			this.ColAIStage.HeaderText = "Stage";
			this.ColAIStage.MinimumWidth = 50;
			this.ColAIStage.Name = "ColAIStage";
			this.ColAIStage.Width = 50;
			// 
			// ColDifficulty
			// 
			this.ColDifficulty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColDifficulty.HeaderText = "Intelligence";
			this.ColDifficulty.Items.AddRange(new object[] {
            "",
            "bad",
            "average",
            "good",
            "best"});
			this.ColDifficulty.Name = "ColDifficulty";
			this.ColDifficulty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColDifficulty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// MetadataEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridAI);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.cboDefaultPic);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.txtHeight);
			this.Controls.Add(this.txtLastName);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtFirstName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.txtArtist);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.txtWriter);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.txtSource);
			this.Controls.Add(this.valRounds);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.cboSize);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.cboGender);
			this.Controls.Add(this.lblUnlisted);
			this.Controls.Add(this.lblTesting);
			this.Controls.Add(this.lblIncomplete);
			this.Controls.Add(this.lblOffline);
			this.Controls.Add(this.txtLabel);
			this.Controls.Add(this.label1);
			this.Name = "MetadataEditor";
			this.Size = new System.Drawing.Size(897, 589);
			((System.ComponentModel.ISupportInitialize)(this.gridAI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valRounds)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblUnlisted;
		private System.Windows.Forms.Label lblTesting;
		private System.Windows.Forms.Label lblIncomplete;
		private System.Windows.Forms.Label lblOffline;
		private System.Windows.Forms.DataGridView gridAI;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.ComboBox cboDefaultPic;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox txtArtist;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtWriter;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.NumericUpDown valRounds;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cboSize;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cboGender;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAIStage;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColDifficulty;
	}
}

namespace SPNATI_Character_Editor.Activities
{
	partial class AdvancedMetadataEditor
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			this.valScale = new Desktop.Skinning.SkinnedNumericUpDown();
			this.lblScale = new Desktop.Skinning.SkinnedLabel();
			this.gridLabels = new Desktop.Skinning.SkinnedDataGridView();
			this.colLabelsStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colLabelsLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblStageLabels = new Desktop.Skinning.SkinnedLabel();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.cboDialogueLayer = new Desktop.Skinning.SkinnedComboBox();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.valLayer = new Desktop.Skinning.SkinnedNumericUpDown();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.gridOtherNicknames = new Desktop.Skinning.SkinnedDataGridView();
			this.ColOtherNickname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gridNicknames = new Desktop.Skinning.SkinnedDataGridView();
			this.ColCharacter = new Desktop.CommonControls.RecordColumn();
			this.ColNickname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.skinnedGroupBox3 = new Desktop.Skinning.SkinnedGroupBox();
			this.styleControl = new SPNATI_Character_Editor.Controls.StyleControl();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			((System.ComponentModel.ISupportInitialize)(this.valScale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLabels)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valLayer)).BeginInit();
			this.skinnedGroupBox1.SuspendLayout();
			this.skinnedGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridOtherNicknames)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridNicknames)).BeginInit();
			this.skinnedGroupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// valScale
			// 
			this.valScale.BackColor = System.Drawing.Color.White;
			this.valScale.DecimalPlaces = 1;
			this.valScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valScale.ForeColor = System.Drawing.Color.Black;
			this.valScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.valScale.Location = new System.Drawing.Point(133, 141);
			this.valScale.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
			this.valScale.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.valScale.Name = "valScale";
			this.valScale.Size = new System.Drawing.Size(66, 20);
			this.valScale.TabIndex = 89;
			this.valScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// lblScale
			// 
			this.lblScale.AutoSize = true;
			this.lblScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblScale.ForeColor = System.Drawing.Color.Black;
			this.lblScale.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblScale.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblScale.Location = new System.Drawing.Point(6, 144);
			this.lblScale.Name = "lblScale";
			this.lblScale.Size = new System.Drawing.Size(84, 13);
			this.lblScale.TabIndex = 88;
			this.lblScale.Text = "Scale factor (%):";
			// 
			// gridLabels
			// 
			this.gridLabels.AllowUserToResizeColumns = false;
			this.gridLabels.AllowUserToResizeRows = false;
			this.gridLabels.BackgroundColor = System.Drawing.Color.White;
			this.gridLabels.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridLabels.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridLabels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridLabels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLabelsStage,
            this.colLabelsLabel});
			this.gridLabels.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridLabels.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridLabels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridLabels.EnableHeadersVisualStyles = false;
			this.gridLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridLabels.GridColor = System.Drawing.Color.LightGray;
			this.gridLabels.Location = new System.Drawing.Point(133, 24);
			this.gridLabels.Name = "gridLabels";
			this.gridLabels.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridLabels.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridLabels.RowHeadersVisible = false;
			this.gridLabels.Size = new System.Drawing.Size(226, 111);
			this.gridLabels.TabIndex = 87;
			// 
			// colLabelsStage
			// 
			this.colLabelsStage.DataPropertyName = "Stage";
			this.colLabelsStage.HeaderText = "Stage";
			this.colLabelsStage.MinimumWidth = 50;
			this.colLabelsStage.Name = "colLabelsStage";
			this.colLabelsStage.Width = 50;
			// 
			// colLabelsLabel
			// 
			this.colLabelsLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colLabelsLabel.DataPropertyName = "Value";
			this.colLabelsLabel.HeaderText = "Label";
			this.colLabelsLabel.Name = "colLabelsLabel";
			this.colLabelsLabel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// lblStageLabels
			// 
			this.lblStageLabels.AutoSize = true;
			this.lblStageLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblStageLabels.ForeColor = System.Drawing.Color.Black;
			this.lblStageLabels.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblStageLabels.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblStageLabels.Location = new System.Drawing.Point(6, 28);
			this.lblStageLabels.Name = "lblStageLabels";
			this.lblStageLabels.Size = new System.Drawing.Size(99, 13);
			this.lblStageLabels.TabIndex = 86;
			this.lblStageLabels.Text = "Vary label by stage:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(6, 170);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121, 13);
			this.label1.TabIndex = 90;
			this.label1.Text = "Speech bubble position:";
			// 
			// cboDialogueLayer
			// 
			this.cboDialogueLayer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboDialogueLayer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboDialogueLayer.BackColor = System.Drawing.Color.White;
			this.cboDialogueLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDialogueLayer.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboDialogueLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboDialogueLayer.FormattingEnabled = true;
			this.cboDialogueLayer.KeyMember = null;
			this.cboDialogueLayer.Location = new System.Drawing.Point(133, 167);
			this.cboDialogueLayer.Name = "cboDialogueLayer";
			this.cboDialogueLayer.SelectedIndex = -1;
			this.cboDialogueLayer.SelectedItem = null;
			this.cboDialogueLayer.Size = new System.Drawing.Size(121, 21);
			this.cboDialogueLayer.Sorted = false;
			this.cboDialogueLayer.TabIndex = 91;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(6, 196);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 92;
			this.label2.Text = "Z layer:";
			// 
			// valLayer
			// 
			this.valLayer.BackColor = System.Drawing.Color.White;
			this.valLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valLayer.ForeColor = System.Drawing.Color.Black;
			this.valLayer.Location = new System.Drawing.Point(133, 194);
			this.valLayer.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
			this.valLayer.Name = "valLayer";
			this.valLayer.Size = new System.Drawing.Size(66, 20);
			this.valLayer.TabIndex = 93;
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox1.Controls.Add(this.gridLabels);
			this.skinnedGroupBox1.Controls.Add(this.valLayer);
			this.skinnedGroupBox1.Controls.Add(this.lblStageLabels);
			this.skinnedGroupBox1.Controls.Add(this.label2);
			this.skinnedGroupBox1.Controls.Add(this.valScale);
			this.skinnedGroupBox1.Controls.Add(this.cboDialogueLayer);
			this.skinnedGroupBox1.Controls.Add(this.lblScale);
			this.skinnedGroupBox1.Controls.Add(this.label1);
			this.skinnedGroupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox1.Image = null;
			this.skinnedGroupBox1.Location = new System.Drawing.Point(6, 6);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox1.ShowIndicatorBar = false;
			this.skinnedGroupBox1.Size = new System.Drawing.Size(420, 220);
			this.skinnedGroupBox1.TabIndex = 94;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Advanced Metadata";
			// 
			// skinnedGroupBox2
			// 
			this.skinnedGroupBox2.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox2.Controls.Add(this.gridOtherNicknames);
			this.skinnedGroupBox2.Controls.Add(this.gridNicknames);
			this.skinnedGroupBox2.Controls.Add(this.label4);
			this.skinnedGroupBox2.Controls.Add(this.label3);
			this.skinnedGroupBox2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox2.Image = null;
			this.skinnedGroupBox2.Location = new System.Drawing.Point(6, 232);
			this.skinnedGroupBox2.Name = "skinnedGroupBox2";
			this.skinnedGroupBox2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox2.ShowIndicatorBar = false;
			this.skinnedGroupBox2.Size = new System.Drawing.Size(420, 284);
			this.skinnedGroupBox2.TabIndex = 95;
			this.skinnedGroupBox2.TabStop = false;
			this.skinnedGroupBox2.Text = "Nicknames";
			// 
			// gridOtherNicknames
			// 
			this.gridOtherNicknames.AllowUserToDeleteRows = false;
			this.gridOtherNicknames.AllowUserToResizeColumns = false;
			this.gridOtherNicknames.AllowUserToResizeRows = false;
			this.gridOtherNicknames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridOtherNicknames.BackgroundColor = System.Drawing.Color.White;
			this.gridOtherNicknames.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridOtherNicknames.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridOtherNicknames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.gridOtherNicknames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridOtherNicknames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOtherNickname});
			this.gridOtherNicknames.Data = null;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridOtherNicknames.DefaultCellStyle = dataGridViewCellStyle5;
			this.gridOtherNicknames.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridOtherNicknames.EnableHeadersVisualStyles = false;
			this.gridOtherNicknames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridOtherNicknames.GridColor = System.Drawing.Color.LightGray;
			this.gridOtherNicknames.Location = new System.Drawing.Point(246, 43);
			this.gridOtherNicknames.Name = "gridOtherNicknames";
			this.gridOtherNicknames.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridOtherNicknames.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.gridOtherNicknames.RowHeadersVisible = false;
			this.gridOtherNicknames.Size = new System.Drawing.Size(165, 235);
			this.gridOtherNicknames.TabIndex = 3;
			// 
			// ColOtherNickname
			// 
			this.ColOtherNickname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColOtherNickname.HeaderText = "Nickname";
			this.ColOtherNickname.Name = "ColOtherNickname";
			// 
			// gridNicknames
			// 
			this.gridNicknames.AllowUserToDeleteRows = false;
			this.gridNicknames.AllowUserToResizeColumns = false;
			this.gridNicknames.AllowUserToResizeRows = false;
			this.gridNicknames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridNicknames.BackgroundColor = System.Drawing.Color.White;
			this.gridNicknames.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridNicknames.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridNicknames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.gridNicknames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridNicknames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCharacter,
            this.ColNickname});
			this.gridNicknames.Data = null;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridNicknames.DefaultCellStyle = dataGridViewCellStyle8;
			this.gridNicknames.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridNicknames.EnableHeadersVisualStyles = false;
			this.gridNicknames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridNicknames.GridColor = System.Drawing.Color.LightGray;
			this.gridNicknames.Location = new System.Drawing.Point(9, 43);
			this.gridNicknames.Name = "gridNicknames";
			this.gridNicknames.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridNicknames.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.gridNicknames.RowHeadersVisible = false;
			this.gridNicknames.Size = new System.Drawing.Size(231, 235);
			this.gridNicknames.TabIndex = 2;
			// 
			// ColCharacter
			// 
			this.ColCharacter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColCharacter.HeaderText = "Character";
			this.ColCharacter.Name = "ColCharacter";
			this.ColCharacter.RecordType = null;
			// 
			// ColNickname
			// 
			this.ColNickname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColNickname.HeaderText = "Nickname";
			this.ColNickname.Name = "ColNickname";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(243, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(147, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Nicknames for everyone else:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(170, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Nicknames for specific characters:";
			// 
			// skinnedGroupBox3
			// 
			this.skinnedGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox3.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox3.Controls.Add(this.styleControl);
			this.skinnedGroupBox3.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox3.Image = null;
			this.skinnedGroupBox3.Location = new System.Drawing.Point(432, 6);
			this.skinnedGroupBox3.Name = "skinnedGroupBox3";
			this.skinnedGroupBox3.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox3.ShowIndicatorBar = false;
			this.skinnedGroupBox3.Size = new System.Drawing.Size(544, 616);
			this.skinnedGroupBox3.TabIndex = 96;
			this.skinnedGroupBox3.TabStop = false;
			this.skinnedGroupBox3.Text = "Custom Text Formatting";
			// 
			// styleControl
			// 
			this.styleControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.styleControl.Location = new System.Drawing.Point(6, 24);
			this.styleControl.Name = "styleControl";
			this.styleControl.Size = new System.Drawing.Size(532, 586);
			this.styleControl.TabIndex = 0;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(3, 519);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(423, 64);
			this.skinnedLabel1.TabIndex = 97;
			this.skinnedLabel1.Text = "To assign a nickname mid-game, set a per-target marker named \"nickname\" on a case" +
    " where the desired character is the target.";
			// 
			// AdvancedMetadataEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedGroupBox3);
			this.Controls.Add(this.skinnedGroupBox2);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Name = "AdvancedMetadataEditor";
			this.Size = new System.Drawing.Size(979, 625);
			((System.ComponentModel.ISupportInitialize)(this.valScale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLabels)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valLayer)).EndInit();
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox1.PerformLayout();
			this.skinnedGroupBox2.ResumeLayout(false);
			this.skinnedGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridOtherNicknames)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridNicknames)).EndInit();
			this.skinnedGroupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedNumericUpDown valScale;
		private Desktop.Skinning.SkinnedLabel lblScale;
		private Desktop.Skinning.SkinnedDataGridView gridLabels;
		private Desktop.Skinning.SkinnedLabel lblStageLabels;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedComboBox cboDialogueLayer;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedNumericUpDown valLayer;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox2;
		private Desktop.Skinning.SkinnedDataGridView gridNicknames;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private Desktop.Skinning.SkinnedDataGridView gridOtherNicknames;
		private Desktop.CommonControls.RecordColumn ColCharacter;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColNickname;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColOtherNickname;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox3;
		private Controls.StyleControl styleControl;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLabelsStage;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLabelsLabel;
	}
}

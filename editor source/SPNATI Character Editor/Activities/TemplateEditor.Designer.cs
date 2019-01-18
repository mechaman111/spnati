namespace SPNATI_Character_Editor.Activities
{
	partial class TemplateEditor
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
			this.cmdPreviewPose = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.gridEmotions = new System.Windows.Forms.DataGridView();
			this.label9 = new System.Windows.Forms.Label();
			this.gridLayers = new System.Windows.Forms.DataGridView();
			this.ColLayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColBlush = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColAnger = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColJuice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.txtBaseCode = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmdGenerate = new System.Windows.Forms.Button();
			this.cmdSaveTemplate = new System.Windows.Forms.Button();
			this.cmdLoadTemplate = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.ColPoseKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPoseL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColR = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColB = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColCrop = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridEmotions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLayers)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdPreviewPose
			// 
			this.cmdPreviewPose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPreviewPose.Location = new System.Drawing.Point(814, 6);
			this.cmdPreviewPose.Name = "cmdPreviewPose";
			this.cmdPreviewPose.Size = new System.Drawing.Size(114, 23);
			this.cmdPreviewPose.TabIndex = 11;
			this.cmdPreviewPose.Text = "Preview Selected";
			this.cmdPreviewPose.UseVisualStyleBackColor = true;
			this.cmdPreviewPose.Click += new System.EventHandler(this.cmdPreviewPose_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 359);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(39, 13);
			this.label10.TabIndex = 18;
			this.label10.Text = "Poses:";
			// 
			// gridEmotions
			// 
			this.gridEmotions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridEmotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridEmotions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColPoseKey,
            this.ColPoseL,
            this.ColT,
            this.ColR,
            this.ColB,
            this.dataGridViewTextBoxColumn1,
            this.ColCrop});
			this.gridEmotions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridEmotions.Location = new System.Drawing.Point(75, 359);
			this.gridEmotions.MultiSelect = false;
			this.gridEmotions.Name = "gridEmotions";
			this.gridEmotions.RowHeadersVisible = false;
			this.gridEmotions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gridEmotions.Size = new System.Drawing.Size(973, 207);
			this.gridEmotions.TabIndex = 17;
			this.gridEmotions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEmotions_CellContentClick);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 106);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 13);
			this.label9.TabIndex = 16;
			this.label9.Text = "Clothing:";
			// 
			// gridLayers
			// 
			this.gridLayers.AllowUserToAddRows = false;
			this.gridLayers.AllowUserToDeleteRows = false;
			this.gridLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLayerName,
            this.ColCode,
            this.ColBlush,
            this.ColAnger,
            this.ColJuice});
			this.gridLayers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridLayers.Location = new System.Drawing.Point(75, 106);
			this.gridLayers.MultiSelect = false;
			this.gridLayers.Name = "gridLayers";
			this.gridLayers.RowHeadersVisible = false;
			this.gridLayers.RowHeadersWidth = 130;
			this.gridLayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gridLayers.Size = new System.Drawing.Size(973, 247);
			this.gridLayers.TabIndex = 15;
			// 
			// ColLayerName
			// 
			this.ColLayerName.HeaderText = "Stage";
			this.ColLayerName.Name = "ColLayerName";
			this.ColLayerName.ReadOnly = true;
			this.ColLayerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColCode
			// 
			this.ColCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColCode.HeaderText = "Code";
			this.ColCode.Name = "ColCode";
			this.ColCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColBlush
			// 
			this.ColBlush.HeaderText = "Blush";
			this.ColBlush.Name = "ColBlush";
			this.ColBlush.Width = 50;
			// 
			// ColAnger
			// 
			this.ColAnger.HeaderText = "Anger";
			this.ColAnger.Name = "ColAnger";
			this.ColAnger.Width = 50;
			// 
			// ColJuice
			// 
			this.ColJuice.HeaderText = "Juice";
			this.ColJuice.Name = "ColJuice";
			this.ColJuice.Width = 50;
			// 
			// txtBaseCode
			// 
			this.txtBaseCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBaseCode.Location = new System.Drawing.Point(75, 36);
			this.txtBaseCode.Multiline = true;
			this.txtBaseCode.Name = "txtBaseCode";
			this.txtBaseCode.Size = new System.Drawing.Size(973, 64);
			this.txtBaseCode.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 36);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(61, 13);
			this.label8.TabIndex = 12;
			this.label8.Text = "Base code:";
			// 
			// cmdGenerate
			// 
			this.cmdGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGenerate.Location = new System.Drawing.Point(934, 7);
			this.cmdGenerate.Name = "cmdGenerate";
			this.cmdGenerate.Size = new System.Drawing.Size(114, 23);
			this.cmdGenerate.TabIndex = 13;
			this.cmdGenerate.Text = "Generate Pose List";
			this.cmdGenerate.UseVisualStyleBackColor = true;
			this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
			// 
			// cmdSaveTemplate
			// 
			this.cmdSaveTemplate.Location = new System.Drawing.Point(111, 6);
			this.cmdSaveTemplate.Name = "cmdSaveTemplate";
			this.cmdSaveTemplate.Size = new System.Drawing.Size(96, 23);
			this.cmdSaveTemplate.TabIndex = 10;
			this.cmdSaveTemplate.Text = "Save Template...";
			this.cmdSaveTemplate.UseVisualStyleBackColor = true;
			this.cmdSaveTemplate.Click += new System.EventHandler(this.cmdSaveTemplate_Click);
			// 
			// cmdLoadTemplate
			// 
			this.cmdLoadTemplate.Location = new System.Drawing.Point(7, 6);
			this.cmdLoadTemplate.Name = "cmdLoadTemplate";
			this.cmdLoadTemplate.Size = new System.Drawing.Size(98, 23);
			this.cmdLoadTemplate.TabIndex = 9;
			this.cmdLoadTemplate.Text = "Load Template...";
			this.cmdLoadTemplate.UseVisualStyleBackColor = true;
			this.cmdLoadTemplate.Click += new System.EventHandler(this.cmdLoadTemplate_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Text files|*.txt";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Text files|*.txt";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(412, 572);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(636, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "In order for templates to work properly, make sure you export your codes in Kisek" +
    "ae with appropriate filters set!";
			// 
			// ColPoseKey
			// 
			this.ColPoseKey.HeaderText = "Emotion";
			this.ColPoseKey.Name = "ColPoseKey";
			// 
			// ColPoseL
			// 
			this.ColPoseL.HeaderText = "L";
			this.ColPoseL.Name = "ColPoseL";
			this.ColPoseL.Width = 40;
			// 
			// ColT
			// 
			this.ColT.HeaderText = "T";
			this.ColT.Name = "ColT";
			this.ColT.Width = 40;
			// 
			// ColR
			// 
			this.ColR.HeaderText = "R";
			this.ColR.Name = "ColR";
			this.ColR.Width = 40;
			// 
			// ColB
			// 
			this.ColB.HeaderText = "B";
			this.ColB.Name = "ColB";
			this.ColB.Width = 40;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.HeaderText = "Code";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// ColCrop
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.NullValue = "Crop";
			this.ColCrop.DefaultCellStyle = dataGridViewCellStyle1;
			this.ColCrop.HeaderText = "Crop";
			this.ColCrop.Name = "ColCrop";
			this.ColCrop.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColCrop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ColCrop.Width = 60;
			// 
			// TemplateEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdPreviewPose);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.gridEmotions);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.gridLayers);
			this.Controls.Add(this.txtBaseCode);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.cmdGenerate);
			this.Controls.Add(this.cmdSaveTemplate);
			this.Controls.Add(this.cmdLoadTemplate);
			this.Name = "TemplateEditor";
			this.Size = new System.Drawing.Size(1051, 594);
			((System.ComponentModel.ISupportInitialize)(this.gridEmotions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLayers)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdPreviewPose;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.DataGridView gridEmotions;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DataGridView gridLayers;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLayerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColCode;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColBlush;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAnger;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColJuice;
		private System.Windows.Forms.TextBox txtBaseCode;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button cmdGenerate;
		private System.Windows.Forms.Button cmdSaveTemplate;
		private System.Windows.Forms.Button cmdLoadTemplate;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPoseKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPoseL;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColT;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColR;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColB;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewButtonColumn ColCrop;
	}
}

namespace SPNATI_Character_Editor.Activities
{
	partial class ScreenshotTaker
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.cmdAdvanced = new Desktop.Skinning.SkinnedButton();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.cmdCompressSelected = new Desktop.Skinning.SkinnedButton();
			this.cmdCompressAll = new Desktop.Skinning.SkinnedButton();
			this.gridFiles = new Desktop.Skinning.SkinnedDataGridView();
			this.ColFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColStatus = new System.Windows.Forms.DataGridViewImageColumn();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdMarkCompressed = new Desktop.Skinning.SkinnedButton();
			this.skinnedGroupBox1.SuspendLayout();
			this.skinnedGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridFiles)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(5, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "File name:";
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(66, 28);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(174, 20);
			this.txtName.TabIndex = 1;
			// 
			// cmdImport
			// 
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImport.Flat = false;
			this.cmdImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdImport.Location = new System.Drawing.Point(8, 54);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(232, 70);
			this.cmdImport.TabIndex = 4;
			this.cmdImport.Text = "Capture && Crop";
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(6, 127);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(894, 42);
			this.label2.TabIndex = 5;
			this.label2.Text = "This will take a screenshot of whatever scene is currently in Kisekae without per" +
    "forming any additional processing and save it to the provided file name.";
			// 
			// cmdAdvanced
			// 
			this.cmdAdvanced.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAdvanced.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdAdvanced.Flat = false;
			this.cmdAdvanced.Location = new System.Drawing.Point(246, 54);
			this.cmdAdvanced.Name = "cmdAdvanced";
			this.cmdAdvanced.Size = new System.Drawing.Size(194, 23);
			this.cmdAdvanced.TabIndex = 6;
			this.cmdAdvanced.Text = "Set Part Transparencies...";
			this.cmdAdvanced.UseVisualStyleBackColor = true;
			this.cmdAdvanced.Click += new System.EventHandler(this.cmdAdvanced_Click);
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox1.Controls.Add(this.txtName);
			this.skinnedGroupBox1.Controls.Add(this.label2);
			this.skinnedGroupBox1.Controls.Add(this.cmdAdvanced);
			this.skinnedGroupBox1.Controls.Add(this.label1);
			this.skinnedGroupBox1.Controls.Add(this.cmdImport);
			this.skinnedGroupBox1.Location = new System.Drawing.Point(3, 3);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.Size = new System.Drawing.Size(906, 172);
			this.skinnedGroupBox1.TabIndex = 7;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Screenshots";
			// 
			// skinnedGroupBox2
			// 
			this.skinnedGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox2.Controls.Add(this.cmdMarkCompressed);
			this.skinnedGroupBox2.Controls.Add(this.cmdCompressSelected);
			this.skinnedGroupBox2.Controls.Add(this.cmdCompressAll);
			this.skinnedGroupBox2.Controls.Add(this.gridFiles);
			this.skinnedGroupBox2.Location = new System.Drawing.Point(3, 181);
			this.skinnedGroupBox2.Name = "skinnedGroupBox2";
			this.skinnedGroupBox2.Size = new System.Drawing.Size(906, 388);
			this.skinnedGroupBox2.TabIndex = 8;
			this.skinnedGroupBox2.TabStop = false;
			this.skinnedGroupBox2.Text = "Image Compression";
			// 
			// cmdCompressSelected
			// 
			this.cmdCompressSelected.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCompressSelected.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCompressSelected.Flat = false;
			this.cmdCompressSelected.Location = new System.Drawing.Point(386, 56);
			this.cmdCompressSelected.Name = "cmdCompressSelected";
			this.cmdCompressSelected.Size = new System.Drawing.Size(170, 23);
			this.cmdCompressSelected.TabIndex = 5;
			this.cmdCompressSelected.Text = "Compress Selected";
			this.toolTip1.SetToolTip(this.cmdCompressSelected, "Compress selected images");
			this.cmdCompressSelected.UseVisualStyleBackColor = true;
			this.cmdCompressSelected.Click += new System.EventHandler(this.cmdCompressSelected_Click);
			// 
			// cmdCompressAll
			// 
			this.cmdCompressAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCompressAll.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCompressAll.Flat = false;
			this.cmdCompressAll.Location = new System.Drawing.Point(386, 27);
			this.cmdCompressAll.Name = "cmdCompressAll";
			this.cmdCompressAll.Size = new System.Drawing.Size(170, 23);
			this.cmdCompressAll.TabIndex = 4;
			this.cmdCompressAll.Text = "Compress All";
			this.toolTip1.SetToolTip(this.cmdCompressAll, "Compress all uncompressed images");
			this.cmdCompressAll.UseVisualStyleBackColor = true;
			this.cmdCompressAll.Click += new System.EventHandler(this.cmdCompressAll_Click);
			// 
			// gridFiles
			// 
			this.gridFiles.AllowUserToAddRows = false;
			this.gridFiles.AllowUserToDeleteRows = false;
			this.gridFiles.AllowUserToResizeColumns = false;
			this.gridFiles.AllowUserToResizeRows = false;
			this.gridFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridFiles.BackgroundColor = System.Drawing.Color.White;
			this.gridFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColFile,
            this.ColSize,
            this.ColStatus});
			this.gridFiles.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridFiles.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridFiles.EnableHeadersVisualStyles = false;
			this.gridFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridFiles.GridColor = System.Drawing.Color.LightGray;
			this.gridFiles.Location = new System.Drawing.Point(9, 27);
			this.gridFiles.Name = "gridFiles";
			this.gridFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridFiles.RowHeadersVisible = false;
			this.gridFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridFiles.Size = new System.Drawing.Size(371, 355);
			this.gridFiles.TabIndex = 3;
			// 
			// ColFile
			// 
			this.ColFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColFile.HeaderText = "File";
			this.ColFile.Name = "ColFile";
			this.ColFile.ReadOnly = true;
			// 
			// ColSize
			// 
			this.ColSize.FillWeight = 80F;
			this.ColSize.HeaderText = "File Size";
			this.ColSize.Name = "ColSize";
			this.ColSize.ReadOnly = true;
			this.ColSize.Width = 80;
			// 
			// ColStatus
			// 
			this.ColStatus.HeaderText = "Compressed?";
			this.ColStatus.Name = "ColStatus";
			this.ColStatus.ReadOnly = true;
			this.ColStatus.Width = 75;
			// 
			// cmdMarkCompressed
			// 
			this.cmdMarkCompressed.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdMarkCompressed.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdMarkCompressed.Flat = false;
			this.cmdMarkCompressed.Location = new System.Drawing.Point(386, 85);
			this.cmdMarkCompressed.Name = "cmdMarkCompressed";
			this.cmdMarkCompressed.Size = new System.Drawing.Size(170, 23);
			this.cmdMarkCompressed.TabIndex = 6;
			this.cmdMarkCompressed.Text = "Mark Compressed";
			this.toolTip1.SetToolTip(this.cmdMarkCompressed, "Mark selected files as already compressed");
			this.cmdMarkCompressed.UseVisualStyleBackColor = true;
			this.cmdMarkCompressed.Click += new System.EventHandler(this.cmdMarkCompressed_Click);
			// 
			// ScreenshotTaker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedGroupBox2);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Name = "ScreenshotTaker";
			this.Size = new System.Drawing.Size(912, 572);
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox1.PerformLayout();
			this.skinnedGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedTextBox txtName;
		private Desktop.Skinning.SkinnedButton cmdImport;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedButton cmdAdvanced;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox2;
		private Desktop.Skinning.SkinnedButton cmdCompressSelected;
		private Desktop.Skinning.SkinnedButton cmdCompressAll;
		private Desktop.Skinning.SkinnedDataGridView gridFiles;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColFile;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColSize;
		private System.Windows.Forms.DataGridViewImageColumn ColStatus;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedButton cmdMarkCompressed;
	}
}

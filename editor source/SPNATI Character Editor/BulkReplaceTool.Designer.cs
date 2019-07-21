namespace SPNATI_Character_Editor
{
	partial class BulkReplaceTool
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.gridDestinations = new Desktop.Skinning.SkinnedDataGridView();
			this.ColTag = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.cboSource = new Desktop.Skinning.SkinnedComboBox();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			((System.ComponentModel.ISupportInitialize)(this.gridDestinations)).BeginInit();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label1.Location = new System.Drawing.Point(13, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Source Case:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label2.Location = new System.Drawing.Point(272, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Cases to Replace:";
			// 
			// gridDestinations
			// 
			this.gridDestinations.AllowUserToResizeColumns = false;
			this.gridDestinations.AllowUserToResizeRows = false;
			this.gridDestinations.BackgroundColor = System.Drawing.Color.White;
			this.gridDestinations.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridDestinations.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridDestinations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.gridDestinations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDestinations.ColumnHeadersVisible = false;
			this.gridDestinations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTag});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridDestinations.DefaultCellStyle = dataGridViewCellStyle8;
			this.gridDestinations.EnableHeadersVisualStyles = false;
			this.gridDestinations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridDestinations.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(153)))), ((int)(((byte)(243)))));
			this.gridDestinations.Location = new System.Drawing.Point(275, 58);
			this.gridDestinations.Name = "gridDestinations";
			this.gridDestinations.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridDestinations.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.gridDestinations.RowHeadersVisible = false;
			this.gridDestinations.Size = new System.Drawing.Size(225, 141);
			this.gridDestinations.TabIndex = 1;
			// 
			// ColTag
			// 
			this.ColTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTag.HeaderText = "Tag";
			this.ColTag.Name = "ColTag";
			// 
			// cboSource
			// 
			this.cboSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSource.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cboSource.FormattingEnabled = true;
			this.cboSource.Location = new System.Drawing.Point(16, 58);
			this.cboSource.Name = "cboSource";
			this.cboSource.SelectedIndex = -1;
			this.cboSource.SelectedItem = null;
			this.cboSource.Size = new System.Drawing.Size(212, 21);
			this.cboSource.Sorted = false;
			this.cboSource.TabIndex = 0;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(353, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(434, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label3.Location = new System.Drawing.Point(239, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "→";
			// 
			// label4
			// 
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label4.Location = new System.Drawing.Point(13, 82);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(215, 117);
			this.label4.TabIndex = 5;
			this.label4.Text = "This will replace all non-targeted dialogue across all stages with the dialogue f" +
    "rom the source case type.";
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 212);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(512, 30);
			this.skinnedPanel1.TabIndex = 6;
			// 
			// BulkReplaceTool
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(512, 242);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cboSource);
			this.Controls.Add(this.gridDestinations);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "BulkReplaceTool";
			this.Text = "Bulk Replace Tool";
			((System.ComponentModel.ISupportInitialize)(this.gridDestinations)).EndInit();
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedDataGridView gridDestinations;
		private Desktop.Skinning.SkinnedComboBox cboSource;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColTag;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
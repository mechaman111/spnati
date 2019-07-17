namespace SPNATI_Character_Editor.Forms
{
	partial class StageImageSelection
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.grid = new Desktop.Skinning.SkinnedDataGridView();
			this.ColStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColImage = new Desktop.Skinning.SkinnedDataGridViewComboBoxColumn();
			this.ColClear = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.AllowUserToAddRows = false;
			this.grid.AllowUserToDeleteRows = false;
			this.grid.AllowUserToResizeColumns = false;
			this.grid.AllowUserToResizeRows = false;
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid.BackgroundColor = System.Drawing.Color.White;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColStage,
            this.ColImage,
            this.ColClear});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grid.DefaultCellStyle = dataGridViewCellStyle2;
			this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.grid.EnableHeadersVisualStyles = false;
			this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(153)))), ((int)(((byte)(243)))));
			this.grid.Location = new System.Drawing.Point(12, 37);
			this.grid.Name = "grid";
			this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.grid.RowHeadersVisible = false;
			this.grid.RowHeadersWidth = 21;
			this.grid.Size = new System.Drawing.Size(260, 290);
			this.grid.TabIndex = 1;
			this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
			this.grid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEnter);
			this.grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
			this.grid.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.grid_CellParsing);
			this.grid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grid_DataError);
			this.grid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);
			// 
			// ColStage
			// 
			this.ColStage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColStage.FillWeight = 98.47716F;
			this.ColStage.HeaderText = "Stage";
			this.ColStage.Name = "ColStage";
			this.ColStage.ReadOnly = true;
			// 
			// ColImage
			// 
			this.ColImage.FillWeight = 101.5228F;
			this.ColImage.HeaderText = "Image";
			this.ColImage.Name = "ColImage";
			this.ColImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColImage.Width = 120;
			// 
			// ColClear
			// 
			this.ColClear.HeaderText = "";
			this.ColClear.Name = "ColClear";
			this.ColClear.Width = 21;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(125, 4);
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
			this.cmdCancel.Location = new System.Drawing.Point(206, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 338);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(284, 30);
			this.skinnedPanel1.TabIndex = 4;
			// 
			// StageImageSelection
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(284, 368);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.grid);
			this.Name = "StageImageSelection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Images Per Stage";
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedDataGridView grid;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStage;
		private Desktop.Skinning.SkinnedDataGridViewComboBoxColumn ColImage;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColClear;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
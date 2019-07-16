namespace SPNATI_Character_Editor.Activities
{
	partial class SkinTagEditor
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridRemove = new Desktop.Skinning.SkinnedDataGridView();
			this.ColTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColRemove = new Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn();
			this.gridAdd = new Desktop.Skinning.SkinnedDataGridView();
			this.ColTagAdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			((System.ComponentModel.ISupportInitialize)(this.gridRemove)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAdd)).BeginInit();
			this.skinnedGroupBox1.SuspendLayout();
			this.skinnedGroupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridRemove
			// 
			this.gridRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridRemove.BackgroundColor = System.Drawing.Color.White;
			this.gridRemove.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridRemove.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridRemove.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.gridRemove.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridRemove.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTag,
            this.ColRemove});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridRemove.DefaultCellStyle = dataGridViewCellStyle8;
			this.gridRemove.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridRemove.EnableHeadersVisualStyles = false;
			this.gridRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridRemove.GridColor = System.Drawing.Color.LightGray;
			this.gridRemove.Location = new System.Drawing.Point(6, 25);
			this.gridRemove.MultiSelect = false;
			this.gridRemove.Name = "gridRemove";
			this.gridRemove.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridRemove.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.gridRemove.RowHeadersVisible = false;
			this.gridRemove.Size = new System.Drawing.Size(316, 534);
			this.gridRemove.TabIndex = 2;
			// 
			// ColTag
			// 
			this.ColTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTag.HeaderText = "Tag";
			this.ColTag.Name = "ColTag";
			this.ColTag.ReadOnly = true;
			// 
			// ColRemove
			// 
			this.ColRemove.HeaderText = "Remove?";
			this.ColRemove.Name = "ColRemove";
			this.ColRemove.Width = 75;
			// 
			// gridAdd
			// 
			this.gridAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridAdd.BackgroundColor = System.Drawing.Color.White;
			this.gridAdd.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridAdd.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridAdd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.gridAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAdd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTagAdd});
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridAdd.DefaultCellStyle = dataGridViewCellStyle11;
			this.gridAdd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridAdd.EnableHeadersVisualStyles = false;
			this.gridAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridAdd.GridColor = System.Drawing.Color.LightGray;
			this.gridAdd.Location = new System.Drawing.Point(6, 25);
			this.gridAdd.MultiSelect = false;
			this.gridAdd.Name = "gridAdd";
			this.gridAdd.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridAdd.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.gridAdd.RowHeadersVisible = false;
			this.gridAdd.Size = new System.Drawing.Size(316, 533);
			this.gridAdd.TabIndex = 3;
			this.gridAdd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridAdd_EditingControlShowing);
			// 
			// ColTagAdd
			// 
			this.ColTagAdd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTagAdd.HeaderText = "Tag";
			this.ColTagAdd.Name = "ColTagAdd";
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.skinnedGroupBox1.Controls.Add(this.gridRemove);
			this.skinnedGroupBox1.Location = new System.Drawing.Point(3, 3);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.Size = new System.Drawing.Size(328, 565);
			this.skinnedGroupBox1.TabIndex = 4;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Tags to Remove When Skin in Use";
			// 
			// skinnedGroupBox2
			// 
			this.skinnedGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.skinnedGroupBox2.Controls.Add(this.gridAdd);
			this.skinnedGroupBox2.Location = new System.Drawing.Point(337, 3);
			this.skinnedGroupBox2.Name = "skinnedGroupBox2";
			this.skinnedGroupBox2.Size = new System.Drawing.Size(328, 565);
			this.skinnedGroupBox2.TabIndex = 5;
			this.skinnedGroupBox2.TabStop = false;
			this.skinnedGroupBox2.Text = "Tags to Add When Skin in Use";
			// 
			// SkinTagEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedGroupBox2);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Name = "SkinTagEditor";
			this.Size = new System.Drawing.Size(756, 571);
			((System.ComponentModel.ISupportInitialize)(this.gridRemove)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAdd)).EndInit();
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.Skinning.SkinnedDataGridView gridRemove;
		private Desktop.Skinning.SkinnedDataGridView gridAdd;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTagAdd;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTag;
		private Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn ColRemove;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox2;
	}
}

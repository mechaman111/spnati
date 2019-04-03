namespace SPNATI_Character_Editor.Activities
{
	partial class TagEditor
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
			this.gridTags = new System.Windows.Forms.DataGridView();
			this.ColTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.toc = new System.Windows.Forms.TreeView();
			this.mainPane = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tagGrid = new SPNATI_Character_Editor.Controls.TagGrid();
			this.tagList = new SPNATI_Character_Editor.Controls.TagList();
			((System.ComponentModel.ISupportInitialize)(this.gridTags)).BeginInit();
			this.mainPane.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridTags
			// 
			this.gridTags.AllowUserToResizeColumns = false;
			this.gridTags.AllowUserToResizeRows = false;
			this.gridTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridTags.ColumnHeadersVisible = false;
			this.gridTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTag});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridTags.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridTags.Location = new System.Drawing.Point(109, 492);
			this.gridTags.Name = "gridTags";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridTags.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridTags.RowHeadersVisible = false;
			this.gridTags.Size = new System.Drawing.Size(150, 93);
			this.gridTags.TabIndex = 104;
			// 
			// ColTag
			// 
			this.ColTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTag.HeaderText = "Tag";
			this.ColTag.Name = "ColTag";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 13);
			this.label1.TabIndex = 106;
			this.label1.Text = "Describe your character\'s appearance and personality.";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 492);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 13);
			this.label2.TabIndex = 107;
			this.label2.Text = "Miscellaneous tags:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(265, 492);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(261, 13);
			this.label3.TabIndex = 108;
			this.label3.Text = "Seek mod approval when using a tag not listed above";
			// 
			// toc
			// 
			this.toc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.toc.BackColor = System.Drawing.SystemColors.Window;
			this.toc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.toc.HideSelection = false;
			this.toc.Location = new System.Drawing.Point(0, 0);
			this.toc.Margin = new System.Windows.Forms.Padding(0);
			this.toc.Name = "toc";
			this.toc.Size = new System.Drawing.Size(156, 473);
			this.toc.TabIndex = 110;
			this.toc.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.toc_AfterSelect);
			// 
			// mainPane
			// 
			this.mainPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainPane.AutoScroll = true;
			this.mainPane.Controls.Add(this.tagGrid);
			this.mainPane.Location = new System.Drawing.Point(159, 0);
			this.mainPane.Margin = new System.Windows.Forms.Padding(0);
			this.mainPane.Name = "mainPane";
			this.mainPane.Size = new System.Drawing.Size(514, 473);
			this.mainPane.TabIndex = 111;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(3, 16);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.toc);
			this.splitContainer1.Panel1.Controls.Add(this.mainPane);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tagList);
			this.splitContainer1.Size = new System.Drawing.Size(860, 473);
			this.splitContainer1.SplitterDistance = 673;
			this.splitContainer1.TabIndex = 112;
			// 
			// tagGrid
			// 
			this.tagGrid.AutoSize = true;
			this.tagGrid.ColumnHeaderHeight = 100;
			this.tagGrid.Location = new System.Drawing.Point(0, 0);
			this.tagGrid.Margin = new System.Windows.Forms.Padding(0);
			this.tagGrid.Name = "tagGrid";
			this.tagGrid.RowHeaderWidth = 130;
			this.tagGrid.Size = new System.Drawing.Size(395, 191);
			this.tagGrid.TabIndex = 109;
			// 
			// tagList
			// 
			this.tagList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tagList.Location = new System.Drawing.Point(0, 0);
			this.tagList.Name = "tagList";
			this.tagList.Size = new System.Drawing.Size(183, 473);
			this.tagList.TabIndex = 0;
			// 
			// TagEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridTags);
			this.Name = "TagEditor";
			this.Size = new System.Drawing.Size(866, 588);
			((System.ComponentModel.ISupportInitialize)(this.gridTags)).EndInit();
			this.mainPane.ResumeLayout(false);
			this.mainPane.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridTags;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTag;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private Controls.TagGrid tagGrid;
		private System.Windows.Forms.TreeView toc;
		private System.Windows.Forms.Panel mainPane;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Controls.TagList tagList;
	}
}

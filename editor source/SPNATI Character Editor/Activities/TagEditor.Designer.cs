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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.toc = new Desktop.Skinning.SkinnedListBox();
			this.mainPane = new Desktop.Skinning.SkinnedPanel();
			this.tagGrid = new SPNATI_Character_Editor.Controls.TagGrid();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tagList = new SPNATI_Character_Editor.Controls.TagList();
			this.mainPane.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 13);
			this.label1.TabIndex = 106;
			this.label1.Text = "Describe your character\'s appearance and personality.";
			// 
			// toc
			// 
			this.toc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.toc.BackColor = System.Drawing.Color.White;
			this.toc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.toc.Font = new System.Drawing.Font("Arial", 9F);
			this.toc.ForeColor = System.Drawing.Color.Black;
			this.toc.ItemHeight = 15;
			this.toc.Location = new System.Drawing.Point(0, 0);
			this.toc.Margin = new System.Windows.Forms.Padding(0);
			this.toc.Name = "toc";
			this.toc.Size = new System.Drawing.Size(156, 557);
			this.toc.TabIndex = 110;
			this.toc.SelectedIndexChanged += new System.EventHandler(this.toc_SelectedIndexChanged);
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
			this.mainPane.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.mainPane.Size = new System.Drawing.Size(514, 569);
			this.mainPane.TabIndex = 111;
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
			this.splitContainer1.Size = new System.Drawing.Size(860, 569);
			this.splitContainer1.SplitterDistance = 673;
			this.splitContainer1.TabIndex = 112;
			// 
			// tagList
			// 
			this.tagList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tagList.Location = new System.Drawing.Point(0, 0);
			this.tagList.Name = "tagList";
			this.tagList.Size = new System.Drawing.Size(183, 569);
			this.tagList.TabIndex = 0;
			// 
			// TagEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.label1);
			this.Name = "TagEditor";
			this.Size = new System.Drawing.Size(866, 588);
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
		private Desktop.Skinning.SkinnedLabel label1;
		private Controls.TagGrid tagGrid;
		private Desktop.Skinning.SkinnedListBox toc;
		private Desktop.Skinning.SkinnedPanel mainPane;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Controls.TagList tagList;
	}
}

namespace SPNATI_Character_Editor.Activities
{
	partial class PoseCreator
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tsPoseList = new System.Windows.Forms.ToolStrip();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.canvas = new Desktop.CommonControls.DBPanel();
			this.lstPoses = new Desktop.CommonControls.DBTreeView();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.tsCollapseAll = new System.Windows.Forms.ToolStripButton();
			this.tsExpandAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsCut = new System.Windows.Forms.ToolStripButton();
			this.tsCopy = new System.Windows.Forms.ToolStripButton();
			this.tsPaste = new System.Windows.Forms.ToolStripButton();
			this.tsDuplicate = new System.Windows.Forms.ToolStripButton();
			this.openFileDialog1 = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			this.preview = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tsPoseList.SuspendLayout();
			this.canvas.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.canvas);
			this.splitContainer1.Size = new System.Drawing.Size(998, 674);
			this.splitContainer1.SplitterDistance = 252;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.lstPoses);
			this.splitContainer2.Panel1.Controls.Add(this.tsPoseList);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.table);
			this.splitContainer2.Size = new System.Drawing.Size(252, 674);
			this.splitContainer2.SplitterDistance = 211;
			this.splitContainer2.TabIndex = 2;
			// 
			// tsPoseList
			// 
			this.tsPoseList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsPoseList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsRemove,
            this.toolStripSeparator1,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsDuplicate,
            this.toolStripSeparator2,
            this.tsCollapseAll,
            this.tsExpandAll});
			this.tsPoseList.Location = new System.Drawing.Point(0, 0);
			this.tsPoseList.Name = "tsPoseList";
			this.tsPoseList.Size = new System.Drawing.Size(248, 25);
			this.tsPoseList.TabIndex = 1;
			this.tsPoseList.Text = "toolStrip1";
			// 
			// table
			// 
			this.table.AllowDelete = false;
			this.table.AllowFavorites = false;
			this.table.AllowHelp = false;
			this.table.Data = null;
			this.table.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table.HideAddField = true;
			this.table.HideSpeedButtons = true;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.Name = "table";
			this.table.PlaceholderText = null;
			this.table.RemoveCaption = "Remove";
			this.table.RowHeaderWidth = 85F;
			this.table.Size = new System.Drawing.Size(248, 455);
			this.table.Sorted = true;
			this.table.TabIndex = 0;
			this.table.UseAutoComplete = true;
			this.table.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.table_PropertyChanged);
			// 
			// canvas
			// 
			this.canvas.Controls.Add(this.preview);
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(738, 670);
			this.canvas.TabIndex = 0;
			// 
			// lstPoses
			// 
			this.lstPoses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstPoses.Location = new System.Drawing.Point(0, 25);
			this.lstPoses.Name = "lstPoses";
			this.lstPoses.Size = new System.Drawing.Size(248, 182);
			this.lstPoses.TabIndex = 2;
			this.lstPoses.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.lstPoses_AfterSelect);
			this.lstPoses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPoses_KeyDown);
			// 
			// tsAdd
			// 
			this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(23, 22);
			this.tsAdd.Text = "Add Pose";
			this.tsAdd.ToolTipText = "Add sprite-based pose";
			this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
			// 
			// tsRemove
			// 
			this.tsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(23, 22);
			this.tsRemove.Text = "Remove pose";
			this.tsRemove.ToolTipText = "Remove pose";
			this.tsRemove.Click += new System.EventHandler(this.tsRemove_Click);
			// 
			// tsCollapseAll
			// 
			this.tsCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCollapseAll.Image = global::SPNATI_Character_Editor.Properties.Resources.CollapseAll;
			this.tsCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCollapseAll.Name = "tsCollapseAll";
			this.tsCollapseAll.Size = new System.Drawing.Size(23, 22);
			this.tsCollapseAll.Text = "Collapse all";
			this.tsCollapseAll.ToolTipText = "Collapse all";
			this.tsCollapseAll.Click += new System.EventHandler(this.tsCollapseAll_Click);
			// 
			// tsExpandAll
			// 
			this.tsExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsExpandAll.Image = global::SPNATI_Character_Editor.Properties.Resources.ExpandAll;
			this.tsExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsExpandAll.Name = "tsExpandAll";
			this.tsExpandAll.Size = new System.Drawing.Size(23, 22);
			this.tsExpandAll.Text = "Expand all";
			this.tsExpandAll.ToolTipText = "Expand all";
			this.tsExpandAll.Click += new System.EventHandler(this.tsExpandAll_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsCut
			// 
			this.tsCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCut.Image = global::SPNATI_Character_Editor.Properties.Resources.Cut;
			this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCut.Name = "tsCut";
			this.tsCut.Size = new System.Drawing.Size(23, 22);
			this.tsCut.Text = "Cut";
			this.tsCut.Click += new System.EventHandler(this.tsCut_Click);
			// 
			// tsCopy
			// 
			this.tsCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCopy.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopy.Name = "tsCopy";
			this.tsCopy.Size = new System.Drawing.Size(23, 22);
			this.tsCopy.Text = "Copy";
			this.tsCopy.Click += new System.EventHandler(this.tsCopy_Click);
			// 
			// tsPaste
			// 
			this.tsPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsPaste.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPaste.Name = "tsPaste";
			this.tsPaste.Size = new System.Drawing.Size(23, 22);
			this.tsPaste.Text = "Paste";
			this.tsPaste.Click += new System.EventHandler(this.tsPaste_Click);
			// 
			// tsDuplicate
			// 
			this.tsDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsDuplicate.Image = global::SPNATI_Character_Editor.Properties.Resources.Duplicate;
			this.tsDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDuplicate.Name = "tsDuplicate";
			this.tsDuplicate.Size = new System.Drawing.Size(23, 22);
			this.tsDuplicate.Text = "Duplicate";
			this.tsDuplicate.Click += new System.EventHandler(this.tsDuplicate_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "";
			this.openFileDialog1.UseAbsolutePaths = false;
			// 
			// preview
			// 
			this.preview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.preview.Location = new System.Drawing.Point(0, 0);
			this.preview.Name = "preview";
			this.preview.Size = new System.Drawing.Size(738, 670);
			this.preview.TabIndex = 0;
			// 
			// PoseCreator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "PoseCreator";
			this.Size = new System.Drawing.Size(998, 674);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tsPoseList.ResumeLayout(false);
			this.tsPoseList.PerformLayout();
			this.canvas.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStrip tsPoseList;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.CommonControls.PropertyTable table;
		private Desktop.CommonControls.DBPanel canvas;
		private Controls.CharacterImageDialog openFileDialog1;
		private Desktop.CommonControls.DBTreeView lstPoses;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsCollapseAll;
		private System.Windows.Forms.ToolStripButton tsExpandAll;
		private System.Windows.Forms.ToolStripButton tsCut;
		private System.Windows.Forms.ToolStripButton tsCopy;
		private System.Windows.Forms.ToolStripButton tsPaste;
		private System.Windows.Forms.ToolStripButton tsDuplicate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private Controls.CharacterImageBox preview;
	}
}

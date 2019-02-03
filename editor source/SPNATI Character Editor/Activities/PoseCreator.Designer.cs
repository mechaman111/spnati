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
			this.lstPoses = new System.Windows.Forms.ListBox();
			this.tsPoseList = new System.Windows.Forms.ToolStrip();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.canvas = new Desktop.CommonControls.DBPanel();
			this.tsAddLink = new System.Windows.Forms.ToolStripButton();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.openFileDialog1 = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tsPoseList.SuspendLayout();
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
			// lstPoses
			// 
			this.lstPoses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstPoses.FormattingEnabled = true;
			this.lstPoses.Location = new System.Drawing.Point(3, 23);
			this.lstPoses.Name = "lstPoses";
			this.lstPoses.Size = new System.Drawing.Size(242, 173);
			this.lstPoses.TabIndex = 0;
			this.lstPoses.SelectedIndexChanged += new System.EventHandler(this.lstPoses_SelectedIndexChanged);
			// 
			// tsPoseList
			// 
			this.tsPoseList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsPoseList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddLink,
            this.tsAdd,
            this.tsRemove});
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
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(738, 670);
			this.canvas.TabIndex = 0;
			// 
			// tsAddLink
			// 
			this.tsAddLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddLink.Image = global::SPNATI_Character_Editor.Properties.Resources.AddLink;
			this.tsAddLink.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddLink.Name = "tsAddLink";
			this.tsAddLink.Size = new System.Drawing.Size(23, 22);
			this.tsAddLink.Text = "Add pose link";
			this.tsAddLink.ToolTipText = "Link image across stages";
			this.tsAddLink.Click += new System.EventHandler(this.tsAddLink_Click);
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
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "";
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox lstPoses;
		private System.Windows.Forms.ToolStrip tsPoseList;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Desktop.CommonControls.PropertyTable table;
		private System.Windows.Forms.ToolStripButton tsAddLink;
		private Desktop.CommonControls.DBPanel canvas;
		private Controls.CharacterImageDialog openFileDialog1;
	}
}

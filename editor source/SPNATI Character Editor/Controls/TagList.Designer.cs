namespace SPNATI_Character_Editor.Controls
{
	partial class TagList
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
			this.grid = new Desktop.Skinning.SkinnedDataGridView();
			this.ColTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColStages = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.AllowUserToDeleteRows = false;
			this.grid.AllowUserToResizeColumns = false;
			this.grid.AllowUserToResizeRows = false;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTag,
            this.ColStages});
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.grid.Location = new System.Drawing.Point(0, 0);
			this.grid.Name = "grid";
			this.grid.RowHeadersVisible = false;
			this.grid.Size = new System.Drawing.Size(150, 150);
			this.grid.TabIndex = 0;
			this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
			this.grid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellValidated);
			this.grid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grid_CellValidating);
			this.grid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);
			// 
			// ColTag
			// 
			this.ColTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTag.HeaderText = "Tag";
			this.ColTag.Name = "ColTag";
			// 
			// ColStages
			// 
			this.ColStages.HeaderText = "Stages";
			this.ColStages.Name = "ColStages";
			this.ColStages.Width = 80;
			// 
			// toolTip1
			// 
			this.toolTip1.IsBalloon = true;
			// 
			// TagList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grid);
			this.Name = "TagList";
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedDataGridView grid;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTag;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColStages;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

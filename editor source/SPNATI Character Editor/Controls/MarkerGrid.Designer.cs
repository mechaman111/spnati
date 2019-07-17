namespace SPNATI_Character_Editor.Controls
{
	partial class MarkerGrid
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
			this.gridMarkers = new Desktop.Skinning.SkinnedDataGridView();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColScope = new Desktop.Skinning.SkinnedDataGridViewComboBoxColumn();
			this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDelete = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridMarkers)).BeginInit();
			this.SuspendLayout();
			// 
			// gridMarkers
			// 
			this.gridMarkers.AllowUserToResizeRows = false;
			this.gridMarkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridMarkers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColScope,
            this.ColDescription,
            this.ColDelete});
			this.gridMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridMarkers.Location = new System.Drawing.Point(0, 0);
			this.gridMarkers.MultiSelect = false;
			this.gridMarkers.Name = "gridMarkers";
			this.gridMarkers.Size = new System.Drawing.Size(653, 362);
			this.gridMarkers.TabIndex = 5;
			this.gridMarkers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMarkers_CellContentClick);
			this.gridMarkers.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridMarkers_CellPainting);
			this.gridMarkers.SelectionChanged += new System.EventHandler(this.gridMarkers_SelectionChanged);
			// 
			// ColName
			// 
			this.ColName.HeaderText = "Name";
			this.ColName.Name = "ColName";
			this.ColName.Width = 120;
			// 
			// ColScope
			// 
			this.ColScope.HeaderText = "Scope";
			this.ColScope.Name = "ColScope";
			this.ColScope.Width = 80;
			// 
			// ColDescription
			// 
			this.ColDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColDescription.HeaderText = "Description";
			this.ColDescription.Name = "ColDescription";
			this.ColDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColDelete
			// 
			this.ColDelete.HeaderText = "";
			this.ColDelete.Name = "ColDelete";
			this.ColDelete.Width = 21;
			// 
			// MarkerGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridMarkers);
			this.Name = "MarkerGrid";
			this.Size = new System.Drawing.Size(653, 362);
			((System.ComponentModel.ISupportInitialize)(this.gridMarkers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedDataGridView gridMarkers;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private Desktop.Skinning.SkinnedDataGridViewComboBoxColumn ColScope;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColDelete;
	}
}

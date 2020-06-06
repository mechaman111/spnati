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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridMarkers = new Desktop.Skinning.SkinnedDataGridView();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColScope = new Desktop.Skinning.SkinnedDataGridViewComboBoxColumn();
			this.ColPersistent = new Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn();
			this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDelete = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridMarkers)).BeginInit();
			this.SuspendLayout();
			// 
			// gridMarkers
			// 
			this.gridMarkers.AllowUserToResizeRows = false;
			this.gridMarkers.BackgroundColor = System.Drawing.Color.White;
			this.gridMarkers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridMarkers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridMarkers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridMarkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridMarkers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColScope,
            this.ColPersistent,
            this.ColDescription,
            this.ColDelete});
			this.gridMarkers.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridMarkers.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridMarkers.EnableHeadersVisualStyles = false;
			this.gridMarkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridMarkers.GridColor = System.Drawing.Color.LightGray;
			this.gridMarkers.Location = new System.Drawing.Point(0, 0);
			this.gridMarkers.MultiSelect = false;
			this.gridMarkers.Name = "gridMarkers";
			this.gridMarkers.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridMarkers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
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
			this.ColScope.AutoComplete = false;
			this.ColScope.DisplayMember = null;
			this.ColScope.HeaderText = "Scope";
			this.ColScope.Name = "ColScope";
			this.ColScope.Sorted = false;
			this.ColScope.Width = 80;
			// 
			// ColPersistent
			// 
			this.ColPersistent.HeaderText = "Persistent";
			this.ColPersistent.Name = "ColPersistent";
			this.ColPersistent.Width = 60;
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
			this.ColDelete.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColDelete.Flat = false;
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
		private Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn ColPersistent;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColDelete;
	}
}

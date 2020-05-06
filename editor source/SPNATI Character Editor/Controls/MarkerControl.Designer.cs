namespace SPNATI_Character_Editor.Controls
{
	partial class MarkerControl
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
			this.ColOperator = new Desktop.CommonControls.RecordColumn();
			this.ColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPerTarget = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColWhen = new Desktop.CommonControls.RecordColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridMarkers)).BeginInit();
			this.SuspendLayout();
			// 
			// gridMarkers
			// 
			this.gridMarkers.AllowUserToDeleteRows = false;
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
            this.ColOperator,
            this.ColValue,
            this.ColPerTarget,
            this.ColWhen});
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
			this.gridMarkers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
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
			this.gridMarkers.RowHeadersVisible = false;
			this.gridMarkers.Size = new System.Drawing.Size(577, 266);
			this.gridMarkers.TabIndex = 0;
			// 
			// ColName
			// 
			this.ColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColName.HeaderText = "Name";
			this.ColName.Name = "ColName";
			// 
			// ColOperator
			// 
			this.ColOperator.AllowsNew = false;
			this.ColOperator.HeaderText = "Op";
			this.ColOperator.Name = "ColOperator";
			this.ColOperator.RecordFilter = null;
			this.ColOperator.RecordType = null;
			this.ColOperator.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColOperator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ColOperator.Width = 50;
			// 
			// ColValue
			// 
			this.ColValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColValue.HeaderText = "Value";
			this.ColValue.Name = "ColValue";
			// 
			// ColPerTarget
			// 
			this.ColPerTarget.HeaderText = "Per Target";
			this.ColPerTarget.Name = "ColPerTarget";
			this.ColPerTarget.Width = 65;
			// 
			// ColWhen
			// 
			this.ColWhen.AllowsNew = false;
			this.ColWhen.HeaderText = "When to Set";
			this.ColWhen.Name = "ColWhen";
			this.ColWhen.RecordFilter = null;
			this.ColWhen.RecordType = null;
			// 
			// MarkerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridMarkers);
			this.Name = "MarkerControl";
			this.Size = new System.Drawing.Size(577, 266);
			((System.ComponentModel.ISupportInitialize)(this.gridMarkers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedDataGridView gridMarkers;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private Desktop.CommonControls.RecordColumn ColOperator;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColValue;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColPerTarget;
		private Desktop.CommonControls.RecordColumn ColWhen;
	}
}

namespace SPNATI_Character_Editor.Activities
{
	partial class CharacterConfiguration
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.gridPrefixes = new Desktop.Skinning.SkinnedDataGridView();
			this.ColPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridPrefixes)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(316, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Exclude images with these prefixes from use as poses in dialogue:";
			// 
			// gridPrefixes
			// 
			this.gridPrefixes.AllowUserToDeleteRows = false;
			this.gridPrefixes.AllowUserToResizeColumns = false;
			this.gridPrefixes.AllowUserToResizeRows = false;
			this.gridPrefixes.BackgroundColor = System.Drawing.Color.White;
			this.gridPrefixes.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridPrefixes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridPrefixes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridPrefixes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridPrefixes.ColumnHeadersVisible = false;
			this.gridPrefixes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColPrefix});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridPrefixes.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridPrefixes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridPrefixes.EnableHeadersVisualStyles = false;
			this.gridPrefixes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridPrefixes.GridColor = System.Drawing.Color.LightGray;
			this.gridPrefixes.Location = new System.Drawing.Point(6, 19);
			this.gridPrefixes.Name = "gridPrefixes";
			this.gridPrefixes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridPrefixes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridPrefixes.RowHeadersVisible = false;
			this.gridPrefixes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gridPrefixes.Size = new System.Drawing.Size(313, 320);
			this.gridPrefixes.TabIndex = 1;
			// 
			// ColPrefix
			// 
			this.ColPrefix.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColPrefix.HeaderText = "Prefix";
			this.ColPrefix.Name = "ColPrefix";
			// 
			// CharacterConfiguration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridPrefixes);
			this.Controls.Add(this.label1);
			this.Name = "CharacterConfiguration";
			this.Size = new System.Drawing.Size(935, 644);
			((System.ComponentModel.ISupportInitialize)(this.gridPrefixes)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedDataGridView gridPrefixes;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPrefix;
	}
}

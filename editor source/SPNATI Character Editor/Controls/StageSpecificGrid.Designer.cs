namespace SPNATI_Character_Editor.Controls
{
	partial class StageSpecificGrid
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
			this.grid = new System.Windows.Forms.DataGridView();
			this.ColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColValue,
            this.ColStage});
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.grid.Location = new System.Drawing.Point(0, 0);
			this.grid.Name = "grid";
			this.grid.RowHeadersVisible = false;
			this.grid.Size = new System.Drawing.Size(153, 151);
			this.grid.TabIndex = 0;
			// 
			// ColValue
			// 
			this.ColValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColValue.HeaderText = "Value";
			this.ColValue.Name = "ColValue";
			// 
			// ColStage
			// 
			this.ColStage.HeaderText = "Stage";
			this.ColStage.Name = "ColStage";
			this.ColStage.Width = 50;
			// 
			// StageSpecificGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grid);
			this.Name = "StageSpecificGrid";
			this.Size = new System.Drawing.Size(153, 151);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView grid;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStage;
	}
}

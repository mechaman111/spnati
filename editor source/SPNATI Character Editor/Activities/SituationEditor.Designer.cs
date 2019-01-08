namespace SPNATI_Character_Editor.Activities
{
	partial class SituationEditor
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
			this.gridCases = new System.Windows.Forms.DataGridView();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColStages = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColTrigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColJump = new System.Windows.Forms.DataGridViewButtonColumn();
			this.gridLines = new SPNATI_Character_Editor.Controls.DialogueGrid();
			((System.ComponentModel.ISupportInitialize)(this.gridCases)).BeginInit();
			this.SuspendLayout();
			// 
			// gridCases
			// 
			this.gridCases.AllowUserToAddRows = false;
			this.gridCases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridCases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridCases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColDescription,
            this.ColStages,
            this.ColTrigger,
            this.ColJump});
			this.gridCases.Location = new System.Drawing.Point(0, 0);
			this.gridCases.MultiSelect = false;
			this.gridCases.Name = "gridCases";
			this.gridCases.Size = new System.Drawing.Size(999, 458);
			this.gridCases.TabIndex = 0;
			this.gridCases.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCases_CellContentClick);
			this.gridCases.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridCases_CellPainting);
			this.gridCases.SelectionChanged += new System.EventHandler(this.gridCases_SelectionChanged);
			this.gridCases.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.gridCases_UserDeletingRow);
			// 
			// ColName
			// 
			this.ColName.HeaderText = "Name";
			this.ColName.Name = "ColName";
			this.ColName.Width = 125;
			// 
			// ColDescription
			// 
			this.ColDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColDescription.HeaderText = "Description";
			this.ColDescription.Name = "ColDescription";
			// 
			// ColStages
			// 
			this.ColStages.HeaderText = "Stages";
			this.ColStages.Name = "ColStages";
			this.ColStages.ReadOnly = true;
			this.ColStages.Width = 80;
			// 
			// ColTrigger
			// 
			this.ColTrigger.HeaderText = "Trigger";
			this.ColTrigger.Name = "ColTrigger";
			this.ColTrigger.ReadOnly = true;
			this.ColTrigger.Width = 150;
			// 
			// ColJump
			// 
			this.ColJump.HeaderText = "";
			this.ColJump.Name = "ColJump";
			this.ColJump.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColJump.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ColJump.Width = 24;
			// 
			// gridLines
			// 
			this.gridLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLines.Location = new System.Drawing.Point(0, 462);
			this.gridLines.Name = "gridLines";
			this.gridLines.ReadOnly = false;
			this.gridLines.Size = new System.Drawing.Size(999, 169);
			this.gridLines.TabIndex = 1;
			this.gridLines.HighlightRow += new System.EventHandler<int>(this.gridLines_HighlightRow);
			// 
			// SituationEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridLines);
			this.Controls.Add(this.gridCases);
			this.Name = "SituationEditor";
			this.Size = new System.Drawing.Size(999, 634);
			((System.ComponentModel.ISupportInitialize)(this.gridCases)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridCases;
		private Controls.DialogueGrid gridLines;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStages;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTrigger;
		private System.Windows.Forms.DataGridViewButtonColumn ColJump;
	}
}

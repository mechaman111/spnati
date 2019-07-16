namespace SPNATI_Character_Editor.Controls
{
	partial class DialogueGrid
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridDialogue = new Desktop.Skinning.SkinnedDataGridView();
			this.ColImage = new Desktop.Skinning.SkinnedDataGridViewComboBoxColumn();
			this.ColImageOptions = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarker = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarkerOptions = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColTrophy = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColMore = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColDelete = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridDialogue)).BeginInit();
			this.SuspendLayout();
			// 
			// gridDialogue
			// 
			this.gridDialogue.BackgroundColor = System.Drawing.Color.White;
			this.gridDialogue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridDialogue.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.gridDialogue.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridDialogue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.gridDialogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDialogue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColImage,
            this.ColImageOptions,
            this.ColText,
            this.ColMarker,
            this.ColMarkerOptions,
            this.ColTrophy,
            this.ColMore,
            this.ColDelete});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridDialogue.DefaultCellStyle = dataGridViewCellStyle8;
			this.gridDialogue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridDialogue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridDialogue.EnableHeadersVisualStyles = false;
			this.gridDialogue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridDialogue.GridColor = System.Drawing.Color.LightGray;
			this.gridDialogue.Location = new System.Drawing.Point(0, 0);
			this.gridDialogue.MultiSelect = false;
			this.gridDialogue.Name = "gridDialogue";
			this.gridDialogue.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridDialogue.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.gridDialogue.RowHeadersVisible = false;
			this.gridDialogue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gridDialogue.Size = new System.Drawing.Size(572, 380);
			this.gridDialogue.TabIndex = 42;
			this.gridDialogue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellContentClick);
			this.gridDialogue.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellEnter);
			this.gridDialogue.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridDialogue_CellPainting);
			this.gridDialogue.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.gridDialogue_CellParsing);
			this.gridDialogue.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridDialogue_CellValidating);
			this.gridDialogue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellValueChanged);
			this.gridDialogue.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridDialogue_CurrentCellDirtyStateChanged);
			this.gridDialogue.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridDialogue_EditingControlShowing);
			this.gridDialogue.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridDialogue_RowsAdded);
			this.gridDialogue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridDialogue_KeyDown);
			// 
			// ColImage
			// 
			this.ColImage.AutoComplete = false;
			this.ColImage.DisplayMember = null;
			this.ColImage.HeaderText = "Image";
			this.ColImage.Name = "ColImage";
			this.ColImage.Sorted = false;
			this.ColImage.Width = 120;
			// 
			// ColImageOptions
			// 
			this.ColImageOptions.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColImageOptions.Flat = false;
			this.ColImageOptions.HeaderText = "";
			this.ColImageOptions.Name = "ColImageOptions";
			this.ColImageOptions.Width = 21;
			// 
			// ColText
			// 
			this.ColText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColText.HeaderText = "Text";
			this.ColText.Name = "ColText";
			this.ColText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColMarker
			// 
			this.ColMarker.HeaderText = "Marker";
			this.ColMarker.Name = "ColMarker";
			this.ColMarker.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColMarkerOptions
			// 
			this.ColMarkerOptions.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColMarkerOptions.Flat = false;
			this.ColMarkerOptions.HeaderText = "";
			this.ColMarkerOptions.Name = "ColMarkerOptions";
			this.ColMarkerOptions.Width = 21;
			// 
			// ColTrophy
			// 
			this.ColTrophy.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColTrophy.Flat = false;
			this.ColTrophy.HeaderText = "";
			this.ColTrophy.Name = "ColTrophy";
			this.ColTrophy.Width = 21;
			// 
			// ColMore
			// 
			this.ColMore.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColMore.Flat = false;
			this.ColMore.HeaderText = "";
			this.ColMore.Name = "ColMore";
			this.ColMore.Width = 21;
			// 
			// ColDelete
			// 
			this.ColDelete.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColDelete.Flat = false;
			this.ColDelete.HeaderText = "";
			this.ColDelete.Name = "ColDelete";
			this.ColDelete.Width = 21;
			// 
			// DialogueGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridDialogue);
			this.Name = "DialogueGrid";
			this.Size = new System.Drawing.Size(572, 380);
			this.Leave += new System.EventHandler(this.DialogueGrid_Leave);
			((System.ComponentModel.ISupportInitialize)(this.gridDialogue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedDataGridView gridDialogue;
		private Desktop.Skinning.SkinnedDataGridViewComboBoxColumn ColImage;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColImageOptions;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColText;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColMarker;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColMarkerOptions;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColTrophy;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColMore;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColDelete;
	}
}

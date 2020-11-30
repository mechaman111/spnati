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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridDialogue = new Desktop.Skinning.SkinnedDataGridView();
			this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.applyToColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ColImage = new Desktop.Skinning.SkinnedDataGridViewComboBoxColumn();
			this.ColImageOptions = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarker = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarkerOptions = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColOnce = new Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn();
			this.ColTrophy = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColMore = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColDelete = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridDialogue)).BeginInit();
			this.mnuContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridDialogue
			// 
			this.gridDialogue.AllowUserToResizeRows = false;
			this.gridDialogue.BackgroundColor = System.Drawing.Color.White;
			this.gridDialogue.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridDialogue.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.gridDialogue.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridDialogue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridDialogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDialogue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColImage,
            this.ColImageOptions,
            this.ColText,
            this.ColMarker,
            this.ColMarkerOptions,
            this.ColOnce,
            this.ColTrophy,
            this.ColMore,
            this.ColDelete});
			this.gridDialogue.ContextMenuStrip = this.mnuContext;
			this.gridDialogue.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridDialogue.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridDialogue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridDialogue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridDialogue.EnableHeadersVisualStyles = false;
			this.gridDialogue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridDialogue.GridColor = System.Drawing.Color.LightGray;
			this.gridDialogue.Location = new System.Drawing.Point(0, 0);
			this.gridDialogue.MultiSelect = false;
			this.gridDialogue.Name = "gridDialogue";
			this.gridDialogue.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridDialogue.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridDialogue.RowHeadersVisible = false;
			this.gridDialogue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gridDialogue.Size = new System.Drawing.Size(572, 380);
			this.gridDialogue.TabIndex = 42;
			this.gridDialogue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellContentClick);
			this.gridDialogue.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellEnter);
			this.gridDialogue.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridDialogue_CellMouseDown);
			this.gridDialogue.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridDialogue_CellPainting);
			this.gridDialogue.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.gridDialogue_CellParsing);
			this.gridDialogue.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridDialogue_CellValidating);
			this.gridDialogue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellValueChanged);
			this.gridDialogue.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridDialogue_CurrentCellDirtyStateChanged);
			this.gridDialogue.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridDialogue_EditingControlShowing);
			this.gridDialogue.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridDialogue_RowsAdded);
			this.gridDialogue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridDialogue_KeyDown);
			// 
			// mnuContext
			// 
			this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyToColumnToolStripMenuItem});
			this.mnuContext.Name = "mnuContext";
			this.mnuContext.Size = new System.Drawing.Size(166, 26);
			this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
			// 
			// applyToColumnToolStripMenuItem
			// 
			this.applyToColumnToolStripMenuItem.Name = "applyToColumnToolStripMenuItem";
			this.applyToColumnToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.applyToColumnToolStripMenuItem.Text = "Apply to Column";
			this.applyToColumnToolStripMenuItem.Click += new System.EventHandler(this.applyToColumnToolStripMenuItem_Click);
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
			// ColOnce
			// 
			this.ColOnce.HeaderText = "";
			this.ColOnce.Name = "ColOnce";
			this.ColOnce.ToolTipText = "Only play once per game";
			this.ColOnce.Width = 21;
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
			this.mnuContext.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedDataGridView gridDialogue;
		private System.Windows.Forms.ContextMenuStrip mnuContext;
		private System.Windows.Forms.ToolStripMenuItem applyToColumnToolStripMenuItem;
		private Desktop.Skinning.SkinnedDataGridViewComboBoxColumn ColImage;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColImageOptions;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColText;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColMarker;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColMarkerOptions;
		private Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn ColOnce;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColTrophy;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColMore;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColDelete;
	}
}

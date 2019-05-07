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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridDialogue = new SPNATI_Character_Editor.KeyboardDataGridView();
			this.ColImage = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarker = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarkerOptions = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ColTrophy = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ColMore = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridDialogue)).BeginInit();
			this.SuspendLayout();
			// 
			// gridDialogue
			// 
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
            this.ColText,
            this.ColMarker,
            this.ColMarkerOptions,
            this.ColTrophy,
            this.ColMore,
            this.ColDelete});
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
			this.gridDialogue.Location = new System.Drawing.Point(0, 0);
			this.gridDialogue.MultiSelect = false;
			this.gridDialogue.Name = "gridDialogue";
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
			this.ColImage.HeaderText = "Image";
			this.ColImage.MaxDropDownItems = 20;
			this.ColImage.Name = "ColImage";
			this.ColImage.Width = 120;
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
			this.ColMarkerOptions.HeaderText = "";
			this.ColMarkerOptions.Name = "ColMarkerOptions";
			this.ColMarkerOptions.Width = 21;
			// 
			// ColTrophy
			// 
			this.ColTrophy.HeaderText = "";
			this.ColTrophy.Name = "ColTrophy";
			this.ColTrophy.Width = 21;
			// 
			// ColMore
			// 
			this.ColMore.HeaderText = "";
			this.ColMore.Name = "ColMore";
			this.ColMore.Width = 21;
			// 
			// ColDelete
			// 
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

		private KeyboardDataGridView gridDialogue;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColImage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColText;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColMarker;
		private System.Windows.Forms.DataGridViewButtonColumn ColMarkerOptions;
		private System.Windows.Forms.DataGridViewButtonColumn ColTrophy;
		private System.Windows.Forms.DataGridViewButtonColumn ColMore;
		private System.Windows.Forms.DataGridViewButtonColumn ColDelete;
	}
}

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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridDialogue = new SPNATI_Character_Editor.KeyboardDataGridView();
			this.ColImage = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColText = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarker = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarkerValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPerTarget = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColDirection = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.ColMarkerValue,
            this.ColPerTarget,
            this.ColDirection,
            this.ColLocation,
            this.ColDelete});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridDialogue.DefaultCellStyle = dataGridViewCellStyle3;
			this.gridDialogue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridDialogue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridDialogue.Location = new System.Drawing.Point(0, 0);
			this.gridDialogue.MultiSelect = false;
			this.gridDialogue.Name = "gridDialogue";
			this.gridDialogue.Size = new System.Drawing.Size(572, 380);
			this.gridDialogue.TabIndex = 42;
			this.gridDialogue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellContentClick);
			this.gridDialogue.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDialogue_CellEnter);
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
			// ColMarkerValue
			// 
			this.ColMarkerValue.HeaderText = "Value";
			this.ColMarkerValue.Name = "ColMarkerValue";
			this.ColMarkerValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColMarkerValue.Width = 80;
			// 
			// ColPerTarget
			// 
			this.ColPerTarget.HeaderText = "Per Target";
			this.ColPerTarget.Name = "ColPerTarget";
			this.ColPerTarget.Width = 70;
			// 
			// ColDirection
			// 
			this.ColDirection.HeaderText = "Arrow direction";
			this.ColDirection.Items.AddRange(new object[] {
            "down",
            "left",
            "right",
            "up"});
			this.ColDirection.Name = "ColDirection";
			this.ColDirection.Visible = false;
			this.ColDirection.Width = 60;
			// 
			// ColLocation
			// 
			this.ColLocation.FillWeight = 20F;
			this.ColLocation.HeaderText = "Arrow location";
			this.ColLocation.Name = "ColLocation";
			this.ColLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColLocation.Visible = false;
			this.ColLocation.Width = 60;
			// 
			// ColDelete
			// 
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.NullValue = "X";
			this.ColDelete.DefaultCellStyle = dataGridViewCellStyle2;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn ColMarkerValue;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColPerTarget;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColDirection;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLocation;
		private System.Windows.Forms.DataGridViewButtonColumn ColDelete;
	}
}

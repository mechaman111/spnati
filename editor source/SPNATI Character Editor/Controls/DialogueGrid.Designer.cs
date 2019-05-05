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
			this.ColMarkerValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPerTarget = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColGender = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColIntelligence = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColSize = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColDirection = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColMarkerOptions = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ColTrophy = new System.Windows.Forms.DataGridViewButtonColumn();
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
            this.ColWeight,
            this.ColGender,
            this.ColLabel,
            this.ColIntelligence,
            this.ColSize,
            this.ColDirection,
            this.ColLocation,
			this.ColMarkerOptions,
            this.ColTrophy,
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
			// ColWeight
			// 
			this.ColWeight.HeaderText = "Weight";
			this.ColWeight.Name = "ColWeight";
			this.ColWeight.Visible = false;
			this.ColWeight.Width = 60;
			// 
			// ColGender
			// 
			this.ColGender.HeaderText = "Gender";
			this.ColGender.Items.AddRange(new object[] {
            "",
            "female",
            "male"});
			this.ColGender.Name = "ColGender";
			this.ColGender.Visible = false;
			this.ColGender.Width = 80;
			// 
			// ColLabel
			// 
			this.ColLabel.HeaderText = "Label";
			this.ColLabel.Name = "ColLabel";
			this.ColLabel.Visible = false;
			this.ColLabel.Width = 80;
			// 
			// ColIntelligence
			// 
			this.ColIntelligence.HeaderText = "AI";
			this.ColIntelligence.Items.AddRange(new object[] {
            "",
            "bad",
            "average",
            "good",
            "best"});
			this.ColIntelligence.Name = "ColIntelligence";
			this.ColIntelligence.Visible = false;
			this.ColIntelligence.Width = 80;
			// 
			// ColSize
			// 
			this.ColSize.HeaderText = "Size";
			this.ColSize.Items.AddRange(new object[] {
            "",
            "small",
            "medium",
            "large"});
			this.ColSize.Name = "ColSize";
			this.ColSize.Visible = false;
			this.ColSize.Width = 80;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn ColMarkerValue;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColPerTarget;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColWeight;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColGender;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLabel;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColIntelligence;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColSize;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColDirection;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLocation;
		private System.Windows.Forms.DataGridViewButtonColumn ColMarkerOptions;
		private System.Windows.Forms.DataGridViewButtonColumn ColTrophy;
		private System.Windows.Forms.DataGridViewButtonColumn ColDelete;
	}
}

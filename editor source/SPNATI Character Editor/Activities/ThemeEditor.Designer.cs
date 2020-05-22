namespace SPNATI_Character_Editor.Activities
{
	partial class ThemeEditor
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
			this.skinTester1 = new Desktop.Skinning.SkinTester();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.gridCustom = new Desktop.Skinning.SkinnedDataGridView();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColColor = new System.Windows.Forms.DataGridViewButtonColumn();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.tmrRow = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridCustom)).BeginInit();
			this.SuspendLayout();
			// 
			// skinTester1
			// 
			this.skinTester1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinTester1.Location = new System.Drawing.Point(0, 0);
			this.skinTester1.Name = "skinTester1";
			this.skinTester1.Size = new System.Drawing.Size(805, 686);
			this.skinTester1.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.skinnedGroupBox1);
			this.splitContainer1.Panel1.Controls.Add(this.skinTester1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.gridCustom);
			this.splitContainer1.Panel2.Controls.Add(this.table);
			this.splitContainer1.Panel2MinSize = 450;
			this.splitContainer1.Size = new System.Drawing.Size(1269, 686);
			this.splitContainer1.SplitterDistance = 805;
			this.splitContainer1.TabIndex = 1;
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox1.Image = null;
			this.skinnedGroupBox1.Location = new System.Drawing.Point(605, 283);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Critical;
			this.skinnedGroupBox1.ShowIndicatorBar = false;
			this.skinnedGroupBox1.Size = new System.Drawing.Size(197, 28);
			this.skinnedGroupBox1.TabIndex = 1;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Alert";
			// 
			// gridCustom
			// 
			this.gridCustom.AllowUserToDeleteRows = false;
			this.gridCustom.AllowUserToResizeColumns = false;
			this.gridCustom.AllowUserToResizeRows = false;
			this.gridCustom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridCustom.BackgroundColor = System.Drawing.Color.White;
			this.gridCustom.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridCustom.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridCustom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridCustom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridCustom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColColor});
			this.gridCustom.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridCustom.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridCustom.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridCustom.EnableHeadersVisualStyles = false;
			this.gridCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridCustom.GridColor = System.Drawing.Color.LightGray;
			this.gridCustom.Location = new System.Drawing.Point(0, 503);
			this.gridCustom.Margin = new System.Windows.Forms.Padding(0);
			this.gridCustom.Name = "gridCustom";
			this.gridCustom.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridCustom.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridCustom.Size = new System.Drawing.Size(460, 183);
			this.gridCustom.TabIndex = 11;
			this.gridCustom.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustom_CellContentClick);
			// 
			// ColName
			// 
			this.ColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColName.HeaderText = "Key";
			this.ColName.Name = "ColName";
			// 
			// ColColor
			// 
			this.ColColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColColor.HeaderText = "Color";
			this.ColColor.Name = "ColColor";
			// 
			// table
			// 
			this.table.AllowDelete = false;
			this.table.AllowFavorites = false;
			this.table.AllowHelp = false;
			this.table.AllowMacros = false;
			this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.table.BackColor = System.Drawing.Color.White;
			this.table.Data = null;
			this.table.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.table.HideAddField = true;
			this.table.HideSpeedButtons = true;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.ModifyingProperty = null;
			this.table.Name = "table";
			this.table.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.table.PlaceholderText = null;
			this.table.PreserveControls = true;
			this.table.PreviewData = null;
			this.table.RemoveCaption = "Remove";
			this.table.RowHeaderWidth = 80F;
			this.table.RunInitialAddEvents = false;
			this.table.Size = new System.Drawing.Size(460, 500);
			this.table.Sorted = false;
			this.table.TabIndex = 10;
			this.table.UndoManager = null;
			this.table.UseAutoComplete = false;
			this.table.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.table_PropertyChanged);
			// 
			// tmrRow
			// 
			this.tmrRow.Interval = 1;
			this.tmrRow.Tick += new System.EventHandler(this.tmrRow_Tick);
			// 
			// ThemeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ThemeEditor";
			this.Size = new System.Drawing.Size(1269, 686);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridCustom)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinTester skinTester1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Desktop.CommonControls.PropertyTable table;
		private Desktop.Skinning.SkinnedDataGridView gridCustom;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private System.Windows.Forms.DataGridViewButtonColumn ColColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Timer tmrRow;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
	}
}

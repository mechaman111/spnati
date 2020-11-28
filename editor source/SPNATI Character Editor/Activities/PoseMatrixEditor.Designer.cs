namespace SPNATI_Character_Editor.Activities
{
	partial class PoseMatrixEditor
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
			this.grid = new Desktop.Skinning.SkinnedDataGridView();
			this.gridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cutCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteCellsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cutColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skinnedSplitContainer1 = new Desktop.Skinning.SkinnedSplitContainer();
			this.cmdEditPipeline = new Desktop.Skinning.SkinnedIcon();
			this.searchBar = new SPNATI_Character_Editor.Controls.CodeReplaceBar();
			this.cmdFolder = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.tsSheet = new System.Windows.Forms.ToolStrip();
			this.tsAddPose = new System.Windows.Forms.ToolStripButton();
			this.tsRemovePose = new System.Windows.Forms.ToolStripButton();
			this.tsSort = new System.Windows.Forms.ToolStripButton();
			this.tsApplyCrop = new System.Windows.Forms.ToolStripButton();
			this.tsApplyCode = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsCut = new System.Windows.Forms.ToolStripButton();
			this.tsCopy = new System.Windows.Forms.ToolStripButton();
			this.tsPaste = new System.Windows.Forms.ToolStripButton();
			this.tsReplace = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsPoseList = new System.Windows.Forms.ToolStripButton();
			this.sepSkin = new System.Windows.Forms.ToolStripSeparator();
			this.tsAddMain = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsAddRow = new System.Windows.Forms.ToolStripButton();
			this.tsRemoveRow = new System.Windows.Forms.ToolStripButton();
			this.sptMode = new System.Windows.Forms.SplitContainer();
			this.panelSingle = new System.Windows.Forms.Panel();
			this.cmdImportFull = new Desktop.Skinning.SkinnedIcon();
			this.cmdCrop = new Desktop.Skinning.SkinnedButton();
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.lblHeader = new Desktop.Skinning.SkinnedLabel();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.panelStage = new System.Windows.Forms.Panel();
			this.cmdLoadToKKL = new Desktop.Skinning.SkinnedButton();
			this.panelPose = new System.Windows.Forms.Panel();
			this.cmdImportLineup = new Desktop.Skinning.SkinnedButton();
			this.cmdLineup = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel4 = new Desktop.Skinning.SkinnedLabel();
			this.picHelp = new System.Windows.Forms.PictureBox();
			this.tabStrip = new Desktop.Skinning.SkinnedTabStrip();
			this.tabControl = new Desktop.Skinning.SkinnedTabControl();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdImportAll = new Desktop.Skinning.SkinnedButton();
			this.cmdImportNew = new Desktop.Skinning.SkinnedButton();
			this.cmdImportSelected = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.gridMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer1)).BeginInit();
			this.skinnedSplitContainer1.Panel1.SuspendLayout();
			this.skinnedSplitContainer1.Panel2.SuspendLayout();
			this.skinnedSplitContainer1.SuspendLayout();
			this.tsSheet.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sptMode)).BeginInit();
			this.sptMode.Panel1.SuspendLayout();
			this.sptMode.Panel2.SuspendLayout();
			this.sptMode.SuspendLayout();
			this.panelSingle.SuspendLayout();
			this.panelStage.SuspendLayout();
			this.panelPose.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.AllowUserToAddRows = false;
			this.grid.AllowUserToDeleteRows = false;
			this.grid.AllowUserToOrderColumns = true;
			this.grid.AllowUserToResizeColumns = false;
			this.grid.AllowUserToResizeRows = false;
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.grid.BackgroundColor = System.Drawing.Color.White;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.ContextMenuStrip = this.gridMenu;
			this.grid.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grid.DefaultCellStyle = dataGridViewCellStyle2;
			this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.grid.EnableHeadersVisualStyles = false;
			this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.grid.GridColor = System.Drawing.Color.LightGray;
			this.grid.Location = new System.Drawing.Point(0, 25);
			this.grid.Margin = new System.Windows.Forms.Padding(0);
			this.grid.Name = "grid";
			this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.grid.ShowEditingIcon = false;
			this.grid.Size = new System.Drawing.Size(783, 273);
			this.grid.TabIndex = 0;
			this.grid.TopLeftHeaderMouseDown += new System.EventHandler<System.EventArgs>(this.grid_TopLeftHeaderMouseDown);
			this.grid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_CellMouseDown);
			this.grid.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnDisplayIndexChanged);
			this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
			// 
			// gridMenu
			// 
			this.gridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutCellToolStripMenuItem,
            this.copyCellToolStripMenuItem,
            this.pasteCellToolStripMenuItem,
            this.deleteCellsToolStripMenuItem,
            this.insertColumnToolStripMenuItem,
            this.addColumnToolStripMenuItem,
            this.toolStripSeparator1,
            this.cutColumnsToolStripMenuItem,
            this.copyColumnsToolStripMenuItem,
            this.pasteColumnsToolStripMenuItem,
            this.deleteColumnsToolStripMenuItem,
            this.copyRowsToolStripMenuItem,
            this.pasteRowsToolStripMenuItem});
			this.gridMenu.Name = "gridMenu";
			this.gridMenu.Size = new System.Drawing.Size(204, 274);
			this.gridMenu.Opening += new System.ComponentModel.CancelEventHandler(this.gridMenu_Opening);
			// 
			// cutCellToolStripMenuItem
			// 
			this.cutCellToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Cut;
			this.cutCellToolStripMenuItem.Name = "cutCellToolStripMenuItem";
			this.cutCellToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutCellToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.cutCellToolStripMenuItem.Tag = "Cell";
			this.cutCellToolStripMenuItem.Text = "Cut Cell(s)";
			this.cutCellToolStripMenuItem.Click += new System.EventHandler(this.cutCellToolStripMenuItem_Click);
			// 
			// copyCellToolStripMenuItem
			// 
			this.copyCellToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.copyCellToolStripMenuItem.Name = "copyCellToolStripMenuItem";
			this.copyCellToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyCellToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.copyCellToolStripMenuItem.Tag = "Cell";
			this.copyCellToolStripMenuItem.Text = "Copy Cell(s)";
			this.copyCellToolStripMenuItem.Click += new System.EventHandler(this.copyCellToolStripMenuItem_Click);
			// 
			// pasteCellToolStripMenuItem
			// 
			this.pasteCellToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.pasteCellToolStripMenuItem.Name = "pasteCellToolStripMenuItem";
			this.pasteCellToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
			this.pasteCellToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.pasteCellToolStripMenuItem.Tag = "Cell";
			this.pasteCellToolStripMenuItem.Text = "Paste Cell(s)";
			this.pasteCellToolStripMenuItem.Click += new System.EventHandler(this.pasteCellToolStripMenuItem_Click);
			// 
			// deleteCellsToolStripMenuItem
			// 
			this.deleteCellsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Delete;
			this.deleteCellsToolStripMenuItem.Name = "deleteCellsToolStripMenuItem";
			this.deleteCellsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteCellsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.deleteCellsToolStripMenuItem.Tag = "Cell";
			this.deleteCellsToolStripMenuItem.Text = "Delete Cell(s)";
			this.deleteCellsToolStripMenuItem.Click += new System.EventHandler(this.deleteCellsToolStripMenuItem_Click);
			// 
			// insertColumnToolStripMenuItem
			// 
			this.insertColumnToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.InsertColumn;
			this.insertColumnToolStripMenuItem.Name = "insertColumnToolStripMenuItem";
			this.insertColumnToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
			this.insertColumnToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.insertColumnToolStripMenuItem.Tag = "Column";
			this.insertColumnToolStripMenuItem.Text = "Insert Column";
			this.insertColumnToolStripMenuItem.Click += new System.EventHandler(this.insertColumnToolStripMenuItem_Click);
			// 
			// addColumnToolStripMenuItem
			// 
			this.addColumnToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.addColumnToolStripMenuItem.Name = "addColumnToolStripMenuItem";
			this.addColumnToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.addColumnToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.addColumnToolStripMenuItem.Tag = "Column";
			this.addColumnToolStripMenuItem.Text = "Add Column";
			this.addColumnToolStripMenuItem.Click += new System.EventHandler(this.addColumnToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
			this.toolStripSeparator1.Tag = "Column";
			// 
			// cutColumnsToolStripMenuItem
			// 
			this.cutColumnsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Cut;
			this.cutColumnsToolStripMenuItem.Name = "cutColumnsToolStripMenuItem";
			this.cutColumnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutColumnsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.cutColumnsToolStripMenuItem.Tag = "Column";
			this.cutColumnsToolStripMenuItem.Text = "Cut Column(s)";
			this.cutColumnsToolStripMenuItem.Click += new System.EventHandler(this.cutCellToolStripMenuItem_Click);
			// 
			// copyColumnsToolStripMenuItem
			// 
			this.copyColumnsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.copyColumnsToolStripMenuItem.Name = "copyColumnsToolStripMenuItem";
			this.copyColumnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyColumnsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.copyColumnsToolStripMenuItem.Tag = "Column";
			this.copyColumnsToolStripMenuItem.Text = "Copy Column(s)";
			this.copyColumnsToolStripMenuItem.Click += new System.EventHandler(this.copyCellToolStripMenuItem_Click);
			// 
			// pasteColumnsToolStripMenuItem
			// 
			this.pasteColumnsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.pasteColumnsToolStripMenuItem.Name = "pasteColumnsToolStripMenuItem";
			this.pasteColumnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteColumnsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.pasteColumnsToolStripMenuItem.Tag = "Column";
			this.pasteColumnsToolStripMenuItem.Text = "Paste Column(s)";
			this.pasteColumnsToolStripMenuItem.Click += new System.EventHandler(this.pasteCellToolStripMenuItem_Click);
			// 
			// deleteColumnsToolStripMenuItem
			// 
			this.deleteColumnsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Delete;
			this.deleteColumnsToolStripMenuItem.Name = "deleteColumnsToolStripMenuItem";
			this.deleteColumnsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteColumnsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.deleteColumnsToolStripMenuItem.Tag = "Column";
			this.deleteColumnsToolStripMenuItem.Text = "Delete Column(s)";
			this.deleteColumnsToolStripMenuItem.Click += new System.EventHandler(this.deleteCellsToolStripMenuItem_Click);
			// 
			// copyRowsToolStripMenuItem
			// 
			this.copyRowsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.copyRowsToolStripMenuItem.Name = "copyRowsToolStripMenuItem";
			this.copyRowsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyRowsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.copyRowsToolStripMenuItem.Tag = "Row";
			this.copyRowsToolStripMenuItem.Text = "Copy Row(s)";
			this.copyRowsToolStripMenuItem.Click += new System.EventHandler(this.copyCellToolStripMenuItem_Click);
			// 
			// pasteRowsToolStripMenuItem
			// 
			this.pasteRowsToolStripMenuItem.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.pasteRowsToolStripMenuItem.Name = "pasteRowsToolStripMenuItem";
			this.pasteRowsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteRowsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.pasteRowsToolStripMenuItem.Tag = "Row";
			this.pasteRowsToolStripMenuItem.Text = "Paste Row(s)";
			this.pasteRowsToolStripMenuItem.Click += new System.EventHandler(this.pasteCellToolStripMenuItem_Click);
			// 
			// skinnedSplitContainer1
			// 
			this.skinnedSplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedSplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.skinnedSplitContainer1.Location = new System.Drawing.Point(0, 30);
			this.skinnedSplitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.skinnedSplitContainer1.Name = "skinnedSplitContainer1";
			this.skinnedSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// skinnedSplitContainer1.Panel1
			// 
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.cmdEditPipeline);
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.searchBar);
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.cmdFolder);
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.skinnedLabel1);
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.tsSheet);
			this.skinnedSplitContainer1.Panel1.Controls.Add(this.grid);
			// 
			// skinnedSplitContainer1.Panel2
			// 
			this.skinnedSplitContainer1.Panel2.Controls.Add(this.sptMode);
			this.skinnedSplitContainer1.Size = new System.Drawing.Size(783, 556);
			this.skinnedSplitContainer1.SplitterColor = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedSplitContainer1.SplitterDistance = 321;
			this.skinnedSplitContainer1.TabIndex = 1;
			// 
			// cmdEditPipeline
			// 
			this.cmdEditPipeline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdEditPipeline.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdEditPipeline.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdEditPipeline.Flat = false;
			this.cmdEditPipeline.Image = global::SPNATI_Character_Editor.Properties.Resources.Pipeline;
			this.cmdEditPipeline.Location = new System.Drawing.Point(466, 1);
			this.cmdEditPipeline.Name = "cmdEditPipeline";
			this.cmdEditPipeline.Size = new System.Drawing.Size(25, 23);
			this.cmdEditPipeline.TabIndex = 41;
			this.cmdEditPipeline.Text = "Edit Pipeline";
			this.toolTip1.SetToolTip(this.cmdEditPipeline, "Edit a pipeline using the selected cell");
			this.cmdEditPipeline.UseVisualStyleBackColor = true;
			this.cmdEditPipeline.Click += new System.EventHandler(this.cmdEditPipeline_Click);
			// 
			// searchBar
			// 
			this.searchBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.searchBar.Location = new System.Drawing.Point(0, 298);
			this.searchBar.Margin = new System.Windows.Forms.Padding(0);
			this.searchBar.Name = "searchBar";
			this.searchBar.Size = new System.Drawing.Size(783, 23);
			this.searchBar.TabIndex = 39;
			this.searchBar.Close += new System.EventHandler(this.searchBar_Close);
			this.searchBar.Enter += new System.EventHandler(this.searchBar_Enter);
			// 
			// cmdFolder
			// 
			this.cmdFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdFolder.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdFolder.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdFolder.Flat = true;
			this.cmdFolder.ForeColor = System.Drawing.Color.Blue;
			this.cmdFolder.Location = new System.Drawing.Point(485, 1);
			this.cmdFolder.Name = "cmdFolder";
			this.cmdFolder.Size = new System.Drawing.Size(113, 23);
			this.cmdFolder.TabIndex = 38;
			this.cmdFolder.Text = "Open Folder";
			this.cmdFolder.UseVisualStyleBackColor = true;
			this.cmdFolder.Click += new System.EventHandler(this.cmdFolder_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(604, 6);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(172, 13);
			this.skinnedLabel1.TabIndex = 2;
			this.skinnedLabel1.Text = "Alt+drag to reorder column headers";
			// 
			// tsSheet
			// 
			this.tsSheet.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsSheet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddPose,
            this.tsRemovePose,
            this.tsSort,
            this.tsApplyCrop,
            this.tsApplyCode,
            this.toolStripSeparator2,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsReplace,
            this.toolStripSeparator3,
            this.tsPoseList,
            this.sepSkin,
            this.tsAddMain,
            this.toolStripSeparator4,
            this.tsAddRow,
            this.tsRemoveRow});
			this.tsSheet.Location = new System.Drawing.Point(0, 0);
			this.tsSheet.Name = "tsSheet";
			this.tsSheet.Size = new System.Drawing.Size(783, 25);
			this.tsSheet.TabIndex = 1;
			this.tsSheet.Tag = "Background";
			this.tsSheet.Text = "toolStrip1";
			// 
			// tsAddPose
			// 
			this.tsAddPose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddPose.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAddPose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddPose.Name = "tsAddPose";
			this.tsAddPose.Size = new System.Drawing.Size(23, 22);
			this.tsAddPose.Text = "Add Pose Column";
			this.tsAddPose.Click += new System.EventHandler(this.tsAddPose_Click);
			// 
			// tsRemovePose
			// 
			this.tsRemovePose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemovePose.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemovePose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemovePose.Name = "tsRemovePose";
			this.tsRemovePose.Size = new System.Drawing.Size(23, 22);
			this.tsRemovePose.Text = "Remove Pose Column";
			this.tsRemovePose.Click += new System.EventHandler(this.tsRemovePose_Click);
			// 
			// tsSort
			// 
			this.tsSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsSort.Image = global::SPNATI_Character_Editor.Properties.Resources.SortHorizontal;
			this.tsSort.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsSort.Name = "tsSort";
			this.tsSort.Size = new System.Drawing.Size(23, 22);
			this.tsSort.Text = "Sort Columns";
			this.tsSort.Click += new System.EventHandler(this.tsSort_Click);
			// 
			// tsApplyCrop
			// 
			this.tsApplyCrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsApplyCrop.Image = global::SPNATI_Character_Editor.Properties.Resources.CopyRect;
			this.tsApplyCrop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsApplyCrop.Name = "tsApplyCrop";
			this.tsApplyCrop.Size = new System.Drawing.Size(23, 22);
			this.tsApplyCrop.Text = "Applying Cropping Across Column";
			this.tsApplyCrop.ToolTipText = "Apply current cell\'s cropping to all rows";
			this.tsApplyCrop.Click += new System.EventHandler(this.tsApplyCrop_Click);
			// 
			// tsApplyCode
			// 
			this.tsApplyCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsApplyCode.Image = global::SPNATI_Character_Editor.Properties.Resources.CopyCode;
			this.tsApplyCode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsApplyCode.Name = "tsApplyCode";
			this.tsApplyCode.Size = new System.Drawing.Size(23, 22);
			this.tsApplyCode.Text = "Apply current cell to rows below it";
			this.tsApplyCode.ToolTipText = "Copy the current cell\'s value into the whole column";
			this.tsApplyCode.Click += new System.EventHandler(this.tsApplyCode_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsCut
			// 
			this.tsCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCut.Image = global::SPNATI_Character_Editor.Properties.Resources.Cut;
			this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCut.Name = "tsCut";
			this.tsCut.Size = new System.Drawing.Size(23, 22);
			this.tsCut.Text = "Cut";
			this.tsCut.Click += new System.EventHandler(this.tsCut_Click);
			// 
			// tsCopy
			// 
			this.tsCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsCopy.Image = global::SPNATI_Character_Editor.Properties.Resources.Copy;
			this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopy.Name = "tsCopy";
			this.tsCopy.Size = new System.Drawing.Size(23, 22);
			this.tsCopy.Text = "Copy";
			this.tsCopy.Click += new System.EventHandler(this.tsCopy_Click);
			// 
			// tsPaste
			// 
			this.tsPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsPaste.Image = global::SPNATI_Character_Editor.Properties.Resources.Paste;
			this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPaste.Name = "tsPaste";
			this.tsPaste.Size = new System.Drawing.Size(23, 22);
			this.tsPaste.Text = "Paste";
			this.tsPaste.Click += new System.EventHandler(this.tsPaste_Click);
			// 
			// tsReplace
			// 
			this.tsReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsReplace.Image = global::SPNATI_Character_Editor.Properties.Resources.Replace;
			this.tsReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsReplace.Name = "tsReplace";
			this.tsReplace.Size = new System.Drawing.Size(23, 22);
			this.tsReplace.Text = "Replace text";
			this.tsReplace.Click += new System.EventHandler(this.tsReplace_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsPoseList
			// 
			this.tsPoseList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsPoseList.Image = global::SPNATI_Character_Editor.Properties.Resources.PoseList;
			this.tsPoseList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPoseList.Name = "tsPoseList";
			this.tsPoseList.Size = new System.Drawing.Size(23, 22);
			this.tsPoseList.Text = "Create Pose List";
			this.tsPoseList.ToolTipText = "Turn this sheet into a pose list";
			this.tsPoseList.Click += new System.EventHandler(this.tsPoseList_Click);
			// 
			// sepSkin
			// 
			this.sepSkin.Name = "sepSkin";
			this.sepSkin.Size = new System.Drawing.Size(6, 25);
			this.sepSkin.Visible = false;
			// 
			// tsAddMain
			// 
			this.tsAddMain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddMain.Image = global::SPNATI_Character_Editor.Properties.Resources.AddLink;
			this.tsAddMain.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddMain.Name = "tsAddMain";
			this.tsAddMain.Size = new System.Drawing.Size(23, 22);
			this.tsAddMain.Text = "Add Poses from Main Character";
			this.tsAddMain.Visible = false;
			this.tsAddMain.Click += new System.EventHandler(this.tsAddMain_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tsAddRow
			// 
			this.tsAddRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAddRow.Image = global::SPNATI_Character_Editor.Properties.Resources.AddRow;
			this.tsAddRow.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAddRow.Name = "tsAddRow";
			this.tsAddRow.Size = new System.Drawing.Size(23, 22);
			this.tsAddRow.Text = "Add Row";
			this.tsAddRow.ToolTipText = "Add Row";
			this.tsAddRow.Click += new System.EventHandler(this.tsAddRow_Click);
			// 
			// tsRemoveRow
			// 
			this.tsRemoveRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemoveRow.Image = global::SPNATI_Character_Editor.Properties.Resources.RemoveRow;
			this.tsRemoveRow.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemoveRow.Name = "tsRemoveRow";
			this.tsRemoveRow.Size = new System.Drawing.Size(23, 22);
			this.tsRemoveRow.Text = "Remove Row";
			this.tsRemoveRow.ToolTipText = "Remove Row";
			this.tsRemoveRow.Click += new System.EventHandler(this.tsRemoveRow_Click);
			// 
			// sptMode
			// 
			this.sptMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sptMode.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.sptMode.Location = new System.Drawing.Point(0, 0);
			this.sptMode.Name = "sptMode";
			this.sptMode.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// sptMode.Panel1
			// 
			this.sptMode.Panel1.Controls.Add(this.panelSingle);
			this.sptMode.Panel1.Controls.Add(this.lblHeader);
			this.sptMode.Panel1.Controls.Add(this.table);
			this.sptMode.Panel1.Controls.Add(this.panelStage);
			this.sptMode.Panel1.Controls.Add(this.panelPose);
			// 
			// sptMode.Panel2
			// 
			this.sptMode.Panel2.Controls.Add(this.skinnedLabel4);
			this.sptMode.Panel2.Controls.Add(this.picHelp);
			this.sptMode.Size = new System.Drawing.Size(783, 231);
			this.sptMode.SplitterDistance = 79;
			this.sptMode.TabIndex = 5;
			// 
			// panelSingle
			// 
			this.panelSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelSingle.Controls.Add(this.cmdImportFull);
			this.panelSingle.Controls.Add(this.cmdCrop);
			this.panelSingle.Controls.Add(this.cmdImport);
			this.panelSingle.Location = new System.Drawing.Point(497, 0);
			this.panelSingle.Margin = new System.Windows.Forms.Padding(0);
			this.panelSingle.Name = "panelSingle";
			this.panelSingle.Size = new System.Drawing.Size(286, 29);
			this.panelSingle.TabIndex = 3;
			// 
			// cmdImportFull
			// 
			this.cmdImportFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportFull.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImportFull.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdImportFull.Flat = false;
			this.cmdImportFull.Image = global::SPNATI_Character_Editor.Properties.Resources.Refresh;
			this.cmdImportFull.Location = new System.Drawing.Point(265, 6);
			this.cmdImportFull.Name = "cmdImportFull";
			this.cmdImportFull.Size = new System.Drawing.Size(16, 16);
			this.cmdImportFull.TabIndex = 42;
			this.cmdImportFull.Text = "Force Refresh";
			this.toolTip1.SetToolTip(this.cmdImportFull, "Import the image and rebuild all intermediate assets");
			this.cmdImportFull.UseVisualStyleBackColor = true;
			this.cmdImportFull.Click += new System.EventHandler(this.cmdImportFull_Click);
			// 
			// cmdCrop
			// 
			this.cmdCrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCrop.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCrop.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCrop.Flat = false;
			this.cmdCrop.Location = new System.Drawing.Point(106, 3);
			this.cmdCrop.Name = "cmdCrop";
			this.cmdCrop.Size = new System.Drawing.Size(75, 23);
			this.cmdCrop.TabIndex = 3;
			this.cmdCrop.Text = "Crop";
			this.toolTip1.SetToolTip(this.cmdCrop, "Re-import the image and adjust the cropping");
			this.cmdCrop.UseVisualStyleBackColor = true;
			this.cmdCrop.Click += new System.EventHandler(this.cmdCrop_Click);
			// 
			// cmdImport
			// 
			this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdImport.Flat = false;
			this.cmdImport.Location = new System.Drawing.Point(187, 3);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(75, 23);
			this.cmdImport.TabIndex = 2;
			this.cmdImport.Text = "Import";
			this.toolTip1.SetToolTip(this.cmdImport, "Re-import the image with the current cropping");
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// lblHeader
			// 
			this.lblHeader.AutoSize = true;
			this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblHeader.ForeColor = System.Drawing.Color.Blue;
			this.lblHeader.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblHeader.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblHeader.Location = new System.Drawing.Point(3, 4);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new System.Drawing.Size(78, 21);
			this.lblHeader.TabIndex = 1;
			this.lblHeader.Text = "Pose Data";
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
			this.table.Location = new System.Drawing.Point(3, 28);
			this.table.ModifyingProperty = null;
			this.table.Name = "table";
			this.table.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.table.PlaceholderText = null;
			this.table.PreserveControls = true;
			this.table.PreviewData = null;
			this.table.RemoveCaption = "Remove";
			this.table.RowHeaderWidth = 0F;
			this.table.RunInitialAddEvents = false;
			this.table.Size = new System.Drawing.Size(777, 48);
			this.table.Sorted = false;
			this.table.TabIndex = 0;
			this.table.UndoManager = null;
			this.table.UseAutoComplete = false;
			// 
			// panelStage
			// 
			this.panelStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelStage.Controls.Add(this.cmdLoadToKKL);
			this.panelStage.Location = new System.Drawing.Point(583, 0);
			this.panelStage.Margin = new System.Windows.Forms.Padding(0);
			this.panelStage.Name = "panelStage";
			this.panelStage.Size = new System.Drawing.Size(200, 29);
			this.panelStage.TabIndex = 4;
			// 
			// cmdLoadToKKL
			// 
			this.cmdLoadToKKL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdLoadToKKL.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdLoadToKKL.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdLoadToKKL.Flat = false;
			this.cmdLoadToKKL.Location = new System.Drawing.Point(81, 3);
			this.cmdLoadToKKL.Name = "cmdLoadToKKL";
			this.cmdLoadToKKL.Size = new System.Drawing.Size(116, 23);
			this.cmdLoadToKKL.TabIndex = 2;
			this.cmdLoadToKKL.Text = "Load into KKL";
			this.cmdLoadToKKL.UseVisualStyleBackColor = true;
			this.cmdLoadToKKL.Click += new System.EventHandler(this.cmdLoadToKKL_Click);
			// 
			// panelPose
			// 
			this.panelPose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPose.Controls.Add(this.cmdImportLineup);
			this.panelPose.Controls.Add(this.cmdLineup);
			this.panelPose.Location = new System.Drawing.Point(455, 0);
			this.panelPose.Margin = new System.Windows.Forms.Padding(0);
			this.panelPose.Name = "panelPose";
			this.panelPose.Size = new System.Drawing.Size(328, 29);
			this.panelPose.TabIndex = 4;
			// 
			// cmdImportLineup
			// 
			this.cmdImportLineup.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImportLineup.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdImportLineup.Flat = false;
			this.cmdImportLineup.Location = new System.Drawing.Point(89, 3);
			this.cmdImportLineup.Name = "cmdImportLineup";
			this.cmdImportLineup.Size = new System.Drawing.Size(113, 23);
			this.cmdImportLineup.TabIndex = 3;
			this.cmdImportLineup.Text = "Import Lineup";
			this.toolTip1.SetToolTip(this.cmdImportLineup, "Creates codes for every stage using a character lineup in Kisekae");
			this.cmdImportLineup.UseVisualStyleBackColor = true;
			this.cmdImportLineup.Click += new System.EventHandler(this.cmdImportLineup_Click);
			// 
			// cmdLineup
			// 
			this.cmdLineup.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdLineup.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdLineup.Flat = false;
			this.cmdLineup.Location = new System.Drawing.Point(208, 3);
			this.cmdLineup.Name = "cmdLineup";
			this.cmdLineup.Size = new System.Drawing.Size(117, 23);
			this.cmdLineup.TabIndex = 0;
			this.cmdLineup.Text = "Export Lineup";
			this.toolTip1.SetToolTip(this.cmdLineup, "Load this pose as a character lineup in Kisekae");
			this.cmdLineup.UseVisualStyleBackColor = true;
			this.cmdLineup.Click += new System.EventHandler(this.cmdLineup_Click);
			// 
			// skinnedLabel4
			// 
			this.skinnedLabel4.AutoSize = true;
			this.skinnedLabel4.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.skinnedLabel4.ForeColor = System.Drawing.Color.Blue;
			this.skinnedLabel4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel4.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.skinnedLabel4.Location = new System.Drawing.Point(5, 7);
			this.skinnedLabel4.Name = "skinnedLabel4";
			this.skinnedLabel4.Size = new System.Drawing.Size(148, 21);
			this.skinnedLabel4.TabIndex = 15;
			this.skinnedLabel4.Text = "Set Export Filters To:";
			// 
			// picHelp
			// 
			this.picHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picHelp.InitialImage = null;
			this.picHelp.Location = new System.Drawing.Point(5, 31);
			this.picHelp.Name = "picHelp";
			this.picHelp.Size = new System.Drawing.Size(773, 111);
			this.picHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picHelp.TabIndex = 14;
			this.picHelp.TabStop = false;
			// 
			// tabStrip
			// 
			this.tabStrip.AddCaption = "";
			this.tabStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabStrip.DecorationText = null;
			this.tabStrip.Location = new System.Drawing.Point(0, 0);
			this.tabStrip.Margin = new System.Windows.Forms.Padding(0);
			this.tabStrip.Name = "tabStrip";
			this.tabStrip.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tabStrip.ShowAddButton = true;
			this.tabStrip.ShowCloseButton = false;
			this.tabStrip.Size = new System.Drawing.Size(491, 30);
			this.tabStrip.StartMargin = 5;
			this.tabStrip.TabControl = this.tabControl;
			this.tabStrip.TabIndex = 2;
			this.tabStrip.TabMargin = 5;
			this.tabStrip.TabPadding = 20;
			this.tabStrip.TabSize = -1;
			this.tabStrip.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tabStrip.Text = "skinnedTabStrip1";
			this.tabStrip.Vertical = false;
			this.tabStrip.CloseButtonClicked += new System.EventHandler(this.tabStrip_CloseButtonClicked);
			this.tabStrip.AddButtonClicked += new System.EventHandler(this.tabStrip_AddButtonClicked);
			// 
			// tabControl
			// 
			this.tabControl.Location = new System.Drawing.Point(690, 82);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(72, 100);
			this.tabControl.TabIndex = 3;
			this.tabControl.Visible = false;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// cmdImportAll
			// 
			this.cmdImportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImportAll.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImportAll.Flat = false;
			this.cmdImportAll.Location = new System.Drawing.Point(701, 3);
			this.cmdImportAll.Name = "cmdImportAll";
			this.cmdImportAll.Size = new System.Drawing.Size(79, 23);
			this.cmdImportAll.TabIndex = 27;
			this.cmdImportAll.Text = "All";
			this.toolTip1.SetToolTip(this.cmdImportAll, "Create all images, replacing existing ones");
			this.cmdImportAll.UseVisualStyleBackColor = true;
			this.cmdImportAll.Click += new System.EventHandler(this.cmdImportAll_Click);
			// 
			// cmdImportNew
			// 
			this.cmdImportNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportNew.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImportNew.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImportNew.Flat = false;
			this.cmdImportNew.Location = new System.Drawing.Point(616, 3);
			this.cmdImportNew.Name = "cmdImportNew";
			this.cmdImportNew.Size = new System.Drawing.Size(79, 23);
			this.cmdImportNew.TabIndex = 26;
			this.cmdImportNew.Text = "New";
			this.toolTip1.SetToolTip(this.cmdImportNew, "Creates images that don\'t exist yet");
			this.cmdImportNew.UseVisualStyleBackColor = true;
			this.cmdImportNew.Click += new System.EventHandler(this.cmdImportNew_Click);
			// 
			// cmdImportSelected
			// 
			this.cmdImportSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportSelected.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImportSelected.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImportSelected.Flat = false;
			this.cmdImportSelected.Location = new System.Drawing.Point(531, 3);
			this.cmdImportSelected.Name = "cmdImportSelected";
			this.cmdImportSelected.Size = new System.Drawing.Size(79, 23);
			this.cmdImportSelected.TabIndex = 28;
			this.cmdImportSelected.Text = "Selected";
			this.toolTip1.SetToolTip(this.cmdImportSelected, "Creates images for the selected cells");
			this.cmdImportSelected.UseVisualStyleBackColor = true;
			this.cmdImportSelected.Click += new System.EventHandler(this.cmdImportSelected_Click);
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(494, 9);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(39, 13);
			this.skinnedLabel2.TabIndex = 29;
			this.skinnedLabel2.Text = "Import:";
			// 
			// PoseMatrixEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdImportSelected);
			this.Controls.Add(this.cmdImportAll);
			this.Controls.Add(this.cmdImportNew);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.tabStrip);
			this.Controls.Add(this.skinnedSplitContainer1);
			this.Controls.Add(this.skinnedLabel2);
			this.Name = "PoseMatrixEditor";
			this.Size = new System.Drawing.Size(783, 586);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.gridMenu.ResumeLayout(false);
			this.skinnedSplitContainer1.Panel1.ResumeLayout(false);
			this.skinnedSplitContainer1.Panel1.PerformLayout();
			this.skinnedSplitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer1)).EndInit();
			this.skinnedSplitContainer1.ResumeLayout(false);
			this.tsSheet.ResumeLayout(false);
			this.tsSheet.PerformLayout();
			this.sptMode.Panel1.ResumeLayout(false);
			this.sptMode.Panel1.PerformLayout();
			this.sptMode.Panel2.ResumeLayout(false);
			this.sptMode.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.sptMode)).EndInit();
			this.sptMode.ResumeLayout(false);
			this.panelSingle.ResumeLayout(false);
			this.panelStage.ResumeLayout(false);
			this.panelPose.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedDataGridView grid;
		private Desktop.Skinning.SkinnedSplitContainer skinnedSplitContainer1;
		private Desktop.Skinning.SkinnedTabStrip tabStrip;
		private Desktop.Skinning.SkinnedTabControl tabControl;
		private Desktop.CommonControls.PropertyTable table;
		private Desktop.Skinning.SkinnedLabel lblHeader;
		private Desktop.Skinning.SkinnedButton cmdImport;
		private Desktop.Skinning.SkinnedButton cmdLineup;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedButton cmdImportLineup;
		private System.Windows.Forms.Panel panelPose;
		private System.Windows.Forms.Panel panelSingle;
		private System.Windows.Forms.Panel panelStage;
		private System.Windows.Forms.SplitContainer sptMode;
		private Desktop.Skinning.SkinnedLabel skinnedLabel4;
		private System.Windows.Forms.PictureBox picHelp;
		private Desktop.Skinning.SkinnedButton cmdLoadToKKL;
		private Desktop.Skinning.SkinnedButton cmdImportAll;
		private Desktop.Skinning.SkinnedButton cmdImportNew;
		private System.Windows.Forms.ToolStrip tsSheet;
		private System.Windows.Forms.ToolStripButton tsAddPose;
		private System.Windows.Forms.ToolStripButton tsRemovePose;
		private System.Windows.Forms.ToolStripButton tsSort;
		private System.Windows.Forms.ToolStripButton tsApplyCrop;
		private System.Windows.Forms.ToolStripButton tsApplyCode;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private System.Windows.Forms.ContextMenuStrip gridMenu;
		private System.Windows.Forms.ToolStripMenuItem cutCellToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyCellToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteCellToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteCellsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addColumnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutColumnsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyColumnsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteColumnsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteColumnsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem insertColumnToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsCut;
		private System.Windows.Forms.ToolStripButton tsCopy;
		private System.Windows.Forms.ToolStripButton tsPaste;
		private System.Windows.Forms.ToolStripMenuItem copyRowsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteRowsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator sepSkin;
		private System.Windows.Forms.ToolStripButton tsAddMain;
		private Desktop.Skinning.SkinnedButton cmdFolder;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsPoseList;
		private Desktop.Skinning.SkinnedButton cmdImportSelected;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Controls.CodeReplaceBar searchBar;
		private System.Windows.Forms.ToolStripButton tsReplace;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton tsAddRow;
		private System.Windows.Forms.ToolStripButton tsRemoveRow;
		private Desktop.Skinning.SkinnedButton cmdCrop;
		private Desktop.Skinning.SkinnedIcon cmdEditPipeline;
		private Desktop.Skinning.SkinnedIcon cmdImportFull;
	}
}

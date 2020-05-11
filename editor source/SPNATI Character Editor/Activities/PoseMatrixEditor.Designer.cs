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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.grid = new Desktop.Skinning.SkinnedDataGridView();
			this.skinnedSplitContainer1 = new Desktop.Skinning.SkinnedSplitContainer();
			this.tsSheet = new System.Windows.Forms.ToolStrip();
			this.sptMode = new System.Windows.Forms.SplitContainer();
			this.lblHeader = new Desktop.Skinning.SkinnedLabel();
			this.table = new Desktop.CommonControls.PropertyTable();
			this.panelStage = new System.Windows.Forms.Panel();
			this.cmdLoadToKKL = new Desktop.Skinning.SkinnedButton();
			this.panelSingle = new System.Windows.Forms.Panel();
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.panelPose = new System.Windows.Forms.Panel();
			this.cmdImportLineup = new Desktop.Skinning.SkinnedButton();
			this.cmdLineup = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel4 = new Desktop.Skinning.SkinnedLabel();
			this.tabStrip = new Desktop.Skinning.SkinnedTabStrip();
			this.tabControl = new Desktop.Skinning.SkinnedTabControl();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdImportAll = new Desktop.Skinning.SkinnedButton();
			this.cmdImportNew = new Desktop.Skinning.SkinnedButton();
			this.tsAddPose = new System.Windows.Forms.ToolStripButton();
			this.tsRemovePose = new System.Windows.Forms.ToolStripButton();
			this.tsSort = new System.Windows.Forms.ToolStripButton();
			this.tsApplyCrop = new System.Windows.Forms.ToolStripButton();
			this.picHelp = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.skinnedSplitContainer1)).BeginInit();
			this.skinnedSplitContainer1.Panel1.SuspendLayout();
			this.skinnedSplitContainer1.Panel2.SuspendLayout();
			this.skinnedSplitContainer1.SuspendLayout();
			this.tsSheet.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sptMode)).BeginInit();
			this.sptMode.Panel1.SuspendLayout();
			this.sptMode.Panel2.SuspendLayout();
			this.sptMode.SuspendLayout();
			this.panelStage.SuspendLayout();
			this.panelSingle.SuspendLayout();
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
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Data = null;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grid.DefaultCellStyle = dataGridViewCellStyle5;
			this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.grid.EnableHeadersVisualStyles = false;
			this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.grid.GridColor = System.Drawing.Color.LightGray;
			this.grid.Location = new System.Drawing.Point(0, 25);
			this.grid.Margin = new System.Windows.Forms.Padding(0);
			this.grid.MultiSelect = false;
			this.grid.Name = "grid";
			this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.grid.ShowEditingIcon = false;
			this.grid.Size = new System.Drawing.Size(783, 296);
			this.grid.TabIndex = 0;
			this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
			this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
			this.grid.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnDisplayIndexChanged);
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
			// tsSheet
			// 
			this.tsSheet.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsSheet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddPose,
            this.tsRemovePose,
            this.tsSort,
            this.tsApplyCrop});
			this.tsSheet.Location = new System.Drawing.Point(0, 0);
			this.tsSheet.Name = "tsSheet";
			this.tsSheet.Size = new System.Drawing.Size(783, 25);
			this.tsSheet.TabIndex = 1;
			this.tsSheet.Tag = "Background";
			this.tsSheet.Text = "toolStrip1";
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
			this.sptMode.Panel1.Controls.Add(this.lblHeader);
			this.sptMode.Panel1.Controls.Add(this.table);
			this.sptMode.Panel1.Controls.Add(this.panelStage);
			this.sptMode.Panel1.Controls.Add(this.panelSingle);
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
			this.table.PreserveControls = false;
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
			// panelSingle
			// 
			this.panelSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelSingle.Controls.Add(this.cmdImport);
			this.panelSingle.Location = new System.Drawing.Point(583, 0);
			this.panelSingle.Margin = new System.Windows.Forms.Padding(0);
			this.panelSingle.Name = "panelSingle";
			this.panelSingle.Size = new System.Drawing.Size(200, 29);
			this.panelSingle.TabIndex = 3;
			// 
			// cmdImport
			// 
			this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdImport.Flat = false;
			this.cmdImport.Location = new System.Drawing.Point(122, 3);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(75, 23);
			this.cmdImport.TabIndex = 2;
			this.cmdImport.Text = "Import";
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
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
			this.tabStrip.Size = new System.Drawing.Size(580, 30);
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
			this.cmdImportAll.Location = new System.Drawing.Point(684, 3);
			this.cmdImportAll.Name = "cmdImportAll";
			this.cmdImportAll.Size = new System.Drawing.Size(95, 23);
			this.cmdImportAll.TabIndex = 27;
			this.cmdImportAll.Text = "Import All";
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
			this.cmdImportNew.Location = new System.Drawing.Point(583, 3);
			this.cmdImportNew.Name = "cmdImportNew";
			this.cmdImportNew.Size = new System.Drawing.Size(95, 23);
			this.cmdImportNew.TabIndex = 26;
			this.cmdImportNew.Text = "Import New";
			this.toolTip1.SetToolTip(this.cmdImportNew, "Creates images that don\'t exist yet");
			this.cmdImportNew.UseVisualStyleBackColor = true;
			this.cmdImportNew.Click += new System.EventHandler(this.cmdImportNew_Click);
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
			this.tsSort.Image = global::SPNATI_Character_Editor.Properties.Resources.Sort;
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
			this.tsApplyCrop.Click += new System.EventHandler(this.tsApplyCrop_Click);
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
			// PoseMatrixEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdImportAll);
			this.Controls.Add(this.cmdImportNew);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.tabStrip);
			this.Controls.Add(this.skinnedSplitContainer1);
			this.Name = "PoseMatrixEditor";
			this.Size = new System.Drawing.Size(783, 586);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
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
			this.panelStage.ResumeLayout(false);
			this.panelSingle.ResumeLayout(false);
			this.panelPose.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
			this.ResumeLayout(false);

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
	}
}

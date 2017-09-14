namespace SPNATI_Character_Editor.Controls
{
	partial class ImageManager
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
			this.splitPreviewer = new System.Windows.Forms.SplitContainer();
			this.lblCurrentPoseFile = new System.Windows.Forms.Label();
			this.cmdImportAll = new System.Windows.Forms.Button();
			this.cmdImportNew = new System.Windows.Forms.Button();
			this.cmdClear = new System.Windows.Forms.Button();
			this.cmdExport = new System.Windows.Forms.Button();
			this.cmdImport = new System.Windows.Forms.Button();
			this.gridPoses = new System.Windows.Forms.DataGridView();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdCrop = new System.Windows.Forms.Button();
			this.valHeight = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.valWidth = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.previewPanel = new SPNATI_Character_Editor.Controls.DBPanel();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdLoadTemplate = new System.Windows.Forms.Button();
			this.cmdSaveTemplate = new System.Windows.Forms.Button();
			this.cmdGenerate = new System.Windows.Forms.Button();
			this.cmdPreviewPose = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPoses = new System.Windows.Forms.TabPage();
			this.tabTemplate = new System.Windows.Forms.TabPage();
			this.label10 = new System.Windows.Forms.Label();
			this.gridEmotions = new System.Windows.Forms.DataGridView();
			this.ColPoseKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label9 = new System.Windows.Forms.Label();
			this.gridLayers = new System.Windows.Forms.DataGridView();
			this.ColLayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColBlush = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColAnger = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColJuice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.txtBaseCode = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.ColStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPose = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColR = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColB = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColImage = new System.Windows.Forms.DataGridViewImageColumn();
			this.ColImport = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.splitPreviewer)).BeginInit();
			this.splitPreviewer.Panel1.SuspendLayout();
			this.splitPreviewer.Panel2.SuspendLayout();
			this.splitPreviewer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridPoses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valWidth)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPoses.SuspendLayout();
			this.tabTemplate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridEmotions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLayers)).BeginInit();
			this.SuspendLayout();
			// 
			// splitPreviewer
			// 
			this.splitPreviewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitPreviewer.Location = new System.Drawing.Point(3, 3);
			this.splitPreviewer.Name = "splitPreviewer";
			this.splitPreviewer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitPreviewer.Panel1
			// 
			this.splitPreviewer.Panel1.Controls.Add(this.lblCurrentPoseFile);
			this.splitPreviewer.Panel1.Controls.Add(this.cmdImportAll);
			this.splitPreviewer.Panel1.Controls.Add(this.cmdImportNew);
			this.splitPreviewer.Panel1.Controls.Add(this.cmdClear);
			this.splitPreviewer.Panel1.Controls.Add(this.cmdExport);
			this.splitPreviewer.Panel1.Controls.Add(this.cmdImport);
			this.splitPreviewer.Panel1.Controls.Add(this.gridPoses);
			// 
			// splitPreviewer.Panel2
			// 
			this.splitPreviewer.Panel2.Controls.Add(this.label7);
			this.splitPreviewer.Panel2.Controls.Add(this.label6);
			this.splitPreviewer.Panel2.Controls.Add(this.label2);
			this.splitPreviewer.Panel2.Controls.Add(this.label1);
			this.splitPreviewer.Panel2.Controls.Add(this.label5);
			this.splitPreviewer.Panel2.Controls.Add(this.cmdCancel);
			this.splitPreviewer.Panel2.Controls.Add(this.cmdCrop);
			this.splitPreviewer.Panel2.Controls.Add(this.valHeight);
			this.splitPreviewer.Panel2.Controls.Add(this.label3);
			this.splitPreviewer.Panel2.Controls.Add(this.valWidth);
			this.splitPreviewer.Panel2.Controls.Add(this.label4);
			this.splitPreviewer.Panel2.Controls.Add(this.previewPanel);
			this.splitPreviewer.Panel2MinSize = 500;
			this.splitPreviewer.Size = new System.Drawing.Size(952, 597);
			this.splitPreviewer.SplitterDistance = 57;
			this.splitPreviewer.TabIndex = 4;
			// 
			// lblCurrentPoseFile
			// 
			this.lblCurrentPoseFile.AutoSize = true;
			this.lblCurrentPoseFile.Location = new System.Drawing.Point(265, 8);
			this.lblCurrentPoseFile.Name = "lblCurrentPoseFile";
			this.lblCurrentPoseFile.Size = new System.Drawing.Size(0, 13);
			this.lblCurrentPoseFile.TabIndex = 9;
			// 
			// cmdImportAll
			// 
			this.cmdImportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportAll.Location = new System.Drawing.Point(854, 3);
			this.cmdImportAll.Name = "cmdImportAll";
			this.cmdImportAll.Size = new System.Drawing.Size(95, 23);
			this.cmdImportAll.TabIndex = 8;
			this.cmdImportAll.Text = "Import All";
			this.toolTip1.SetToolTip(this.cmdImportAll, "Import all poses in the list, replacing existing images.");
			this.cmdImportAll.UseVisualStyleBackColor = true;
			this.cmdImportAll.Click += new System.EventHandler(this.cmdImportAll_Click);
			// 
			// cmdImportNew
			// 
			this.cmdImportNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportNew.Location = new System.Drawing.Point(753, 3);
			this.cmdImportNew.Name = "cmdImportNew";
			this.cmdImportNew.Size = new System.Drawing.Size(95, 23);
			this.cmdImportNew.TabIndex = 7;
			this.cmdImportNew.Text = "Import New";
			this.toolTip1.SetToolTip(this.cmdImportNew, "Import poses that have no image yet");
			this.cmdImportNew.UseVisualStyleBackColor = true;
			this.cmdImportNew.Click += new System.EventHandler(this.cmdImportNew_Click);
			// 
			// cmdClear
			// 
			this.cmdClear.Location = new System.Drawing.Point(205, 3);
			this.cmdClear.Name = "cmdClear";
			this.cmdClear.Size = new System.Drawing.Size(54, 23);
			this.cmdClear.TabIndex = 6;
			this.cmdClear.Text = "Clear";
			this.toolTip1.SetToolTip(this.cmdClear, "Clear all poses from the list. Does not delete images.");
			this.cmdClear.UseVisualStyleBackColor = true;
			this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
			// 
			// cmdExport
			// 
			this.cmdExport.Location = new System.Drawing.Point(104, 3);
			this.cmdExport.Name = "cmdExport";
			this.cmdExport.Size = new System.Drawing.Size(95, 23);
			this.cmdExport.TabIndex = 5;
			this.cmdExport.Text = "Save Pose List";
			this.toolTip1.SetToolTip(this.cmdExport, "Save the pose list to disk");
			this.cmdExport.UseVisualStyleBackColor = true;
			this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
			// 
			// cmdImport
			// 
			this.cmdImport.Location = new System.Drawing.Point(3, 3);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(95, 23);
			this.cmdImport.TabIndex = 4;
			this.cmdImport.Text = "Load Pose List";
			this.toolTip1.SetToolTip(this.cmdImport, "Load a pose list from disk.");
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// gridPoses
			// 
			this.gridPoses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPoses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridPoses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColStage,
            this.ColPose,
            this.ColL,
            this.ColT,
            this.ColR,
            this.ColB,
            this.ColData,
            this.ColImage,
            this.ColImport});
			this.gridPoses.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridPoses.Location = new System.Drawing.Point(3, 29);
			this.gridPoses.MultiSelect = false;
			this.gridPoses.Name = "gridPoses";
			this.gridPoses.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.gridPoses.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridPoses.RowTemplate.Height = 100;
			this.gridPoses.Size = new System.Drawing.Size(946, 26);
			this.gridPoses.TabIndex = 3;
			this.gridPoses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPoses_CellContentClick);
			this.gridPoses.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.gridPoses_RowPrePaint);
			this.gridPoses.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridPoses_RowsAdded);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(690, 72);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(207, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Left Mouse Button (inside box) - Move Box";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(690, 59);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(195, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Right Mouse Button (edge) - Mirror Drag";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(690, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Left Mouse Button (edge) - Drag Edge";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(676, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Cropping Box Instructions:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 11);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Import Preview";
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(594, 6);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 13;
			this.cmdCancel.Text = "Cancel";
			this.toolTip1.SetToolTip(this.cmdCancel, "Cancel import");
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdCrop
			// 
			this.cmdCrop.Location = new System.Drawing.Point(513, 6);
			this.cmdCrop.Name = "cmdCrop";
			this.cmdCrop.Size = new System.Drawing.Size(75, 23);
			this.cmdCrop.TabIndex = 12;
			this.cmdCrop.Text = "Accept";
			this.toolTip1.SetToolTip(this.cmdCrop, "Crops the image and saves it");
			this.cmdCrop.UseVisualStyleBackColor = true;
			this.cmdCrop.Click += new System.EventHandler(this.cmdCrop_Click);
			// 
			// valHeight
			// 
			this.valHeight.Location = new System.Drawing.Point(426, 9);
			this.valHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.valHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.valHeight.Name = "valHeight";
			this.valHeight.Size = new System.Drawing.Size(81, 20);
			this.valHeight.TabIndex = 11;
			this.valHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.valHeight.ValueChanged += new System.EventHandler(this.valHeight_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(379, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Height:";
			// 
			// valWidth
			// 
			this.valWidth.Location = new System.Drawing.Point(289, 9);
			this.valWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.valWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.valWidth.Name = "valWidth";
			this.valWidth.Size = new System.Drawing.Size(81, 20);
			this.valWidth.TabIndex = 9;
			this.valWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.valWidth.ValueChanged += new System.EventHandler(this.valWidth_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(245, 11);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Width:";
			// 
			// previewPanel
			// 
			this.previewPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.previewPanel.Location = new System.Drawing.Point(3, 33);
			this.previewPanel.Name = "previewPanel";
			this.previewPanel.Size = new System.Drawing.Size(666, 500);
			this.previewPanel.TabIndex = 3;
			this.previewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPanel_Paint);
			this.previewPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseDown);
			this.previewPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseMove);
			this.previewPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseUp);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Text files|*.txt";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Text files|*.txt";
			// 
			// cmdLoadTemplate
			// 
			this.cmdLoadTemplate.Location = new System.Drawing.Point(7, 7);
			this.cmdLoadTemplate.Name = "cmdLoadTemplate";
			this.cmdLoadTemplate.Size = new System.Drawing.Size(98, 23);
			this.cmdLoadTemplate.TabIndex = 0;
			this.cmdLoadTemplate.Text = "Load Template...";
			this.toolTip1.SetToolTip(this.cmdLoadTemplate, "Load template from a txt file");
			this.cmdLoadTemplate.UseVisualStyleBackColor = true;
			this.cmdLoadTemplate.Click += new System.EventHandler(this.cmdLoadTemplate_Click);
			// 
			// cmdSaveTemplate
			// 
			this.cmdSaveTemplate.Location = new System.Drawing.Point(111, 7);
			this.cmdSaveTemplate.Name = "cmdSaveTemplate";
			this.cmdSaveTemplate.Size = new System.Drawing.Size(96, 23);
			this.cmdSaveTemplate.TabIndex = 1;
			this.cmdSaveTemplate.Text = "Save Template...";
			this.toolTip1.SetToolTip(this.cmdSaveTemplate, "Save template to txt file");
			this.cmdSaveTemplate.UseVisualStyleBackColor = true;
			this.cmdSaveTemplate.Click += new System.EventHandler(this.cmdSaveTemplate_Click);
			// 
			// cmdGenerate
			// 
			this.cmdGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGenerate.Location = new System.Drawing.Point(838, 7);
			this.cmdGenerate.Name = "cmdGenerate";
			this.cmdGenerate.Size = new System.Drawing.Size(114, 23);
			this.cmdGenerate.TabIndex = 3;
			this.cmdGenerate.Text = "Generate Pose List";
			this.toolTip1.SetToolTip(this.cmdGenerate, "Generate a pose list from this template");
			this.cmdGenerate.UseVisualStyleBackColor = true;
			this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
			// 
			// cmdPreviewPose
			// 
			this.cmdPreviewPose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPreviewPose.Location = new System.Drawing.Point(718, 7);
			this.cmdPreviewPose.Name = "cmdPreviewPose";
			this.cmdPreviewPose.Size = new System.Drawing.Size(114, 23);
			this.cmdPreviewPose.TabIndex = 2;
			this.cmdPreviewPose.Text = "Preview Selected";
			this.toolTip1.SetToolTip(this.cmdPreviewPose, "Shows a preview of the selected clothing and pose");
			this.cmdPreviewPose.UseVisualStyleBackColor = true;
			this.cmdPreviewPose.Click += new System.EventHandler(this.cmdPreviewPose_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPoses);
			this.tabControl.Controls.Add(this.tabTemplate);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(966, 629);
			this.tabControl.TabIndex = 5;
			// 
			// tabPoses
			// 
			this.tabPoses.Controls.Add(this.splitPreviewer);
			this.tabPoses.Location = new System.Drawing.Point(4, 22);
			this.tabPoses.Name = "tabPoses";
			this.tabPoses.Padding = new System.Windows.Forms.Padding(3);
			this.tabPoses.Size = new System.Drawing.Size(958, 603);
			this.tabPoses.TabIndex = 0;
			this.tabPoses.Text = "Poses";
			this.tabPoses.UseVisualStyleBackColor = true;
			// 
			// tabTemplate
			// 
			this.tabTemplate.Controls.Add(this.cmdPreviewPose);
			this.tabTemplate.Controls.Add(this.label10);
			this.tabTemplate.Controls.Add(this.gridEmotions);
			this.tabTemplate.Controls.Add(this.label9);
			this.tabTemplate.Controls.Add(this.gridLayers);
			this.tabTemplate.Controls.Add(this.txtBaseCode);
			this.tabTemplate.Controls.Add(this.label8);
			this.tabTemplate.Controls.Add(this.cmdGenerate);
			this.tabTemplate.Controls.Add(this.cmdSaveTemplate);
			this.tabTemplate.Controls.Add(this.cmdLoadTemplate);
			this.tabTemplate.Location = new System.Drawing.Point(4, 22);
			this.tabTemplate.Name = "tabTemplate";
			this.tabTemplate.Padding = new System.Windows.Forms.Padding(3);
			this.tabTemplate.Size = new System.Drawing.Size(958, 603);
			this.tabTemplate.TabIndex = 1;
			this.tabTemplate.Text = "Template";
			this.tabTemplate.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 360);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(39, 13);
			this.label10.TabIndex = 8;
			this.label10.Text = "Poses:";
			// 
			// gridEmotions
			// 
			this.gridEmotions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridEmotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridEmotions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColPoseKey,
            this.dataGridViewTextBoxColumn1});
			this.gridEmotions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridEmotions.Location = new System.Drawing.Point(75, 360);
			this.gridEmotions.MultiSelect = false;
			this.gridEmotions.Name = "gridEmotions";
			this.gridEmotions.RowHeadersVisible = false;
			this.gridEmotions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridEmotions.Size = new System.Drawing.Size(877, 237);
			this.gridEmotions.TabIndex = 7;
			this.gridEmotions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEmotions_CellEndEdit);
			// 
			// ColPoseKey
			// 
			this.ColPoseKey.HeaderText = "Emotion";
			this.ColPoseKey.Name = "ColPoseKey";
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.HeaderText = "Code";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 107);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 13);
			this.label9.TabIndex = 6;
			this.label9.Text = "Clothing:";
			// 
			// gridLayers
			// 
			this.gridLayers.AllowUserToAddRows = false;
			this.gridLayers.AllowUserToDeleteRows = false;
			this.gridLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLayerName,
            this.ColCode,
            this.ColBlush,
            this.ColAnger,
            this.ColJuice});
			this.gridLayers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridLayers.Location = new System.Drawing.Point(75, 107);
			this.gridLayers.MultiSelect = false;
			this.gridLayers.Name = "gridLayers";
			this.gridLayers.RowHeadersVisible = false;
			this.gridLayers.RowHeadersWidth = 130;
			this.gridLayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridLayers.Size = new System.Drawing.Size(877, 247);
			this.gridLayers.TabIndex = 5;
			this.gridLayers.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLayers_CellEndEdit);
			// 
			// ColLayerName
			// 
			this.ColLayerName.HeaderText = "Stage";
			this.ColLayerName.Name = "ColLayerName";
			this.ColLayerName.ReadOnly = true;
			this.ColLayerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColCode
			// 
			this.ColCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColCode.HeaderText = "Code";
			this.ColCode.Name = "ColCode";
			this.ColCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColBlush
			// 
			this.ColBlush.HeaderText = "Blush";
			this.ColBlush.Name = "ColBlush";
			this.ColBlush.Width = 50;
			// 
			// ColAnger
			// 
			this.ColAnger.HeaderText = "Anger";
			this.ColAnger.Name = "ColAnger";
			this.ColAnger.Width = 50;
			// 
			// ColJuice
			// 
			this.ColJuice.HeaderText = "Juice";
			this.ColJuice.Name = "ColJuice";
			this.ColJuice.Width = 50;
			// 
			// txtBaseCode
			// 
			this.txtBaseCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBaseCode.Location = new System.Drawing.Point(75, 37);
			this.txtBaseCode.Multiline = true;
			this.txtBaseCode.Name = "txtBaseCode";
			this.txtBaseCode.Size = new System.Drawing.Size(877, 64);
			this.txtBaseCode.TabIndex = 4;
			this.txtBaseCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBaseCode_KeyDown);
			this.txtBaseCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtBaseCode_Validating);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 37);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(61, 13);
			this.label8.TabIndex = 3;
			this.label8.Text = "Base code:";
			// 
			// ColStage
			// 
			this.ColStage.HeaderText = "Stage";
			this.ColStage.Name = "ColStage";
			this.ColStage.Width = 50;
			// 
			// ColPose
			// 
			this.ColPose.HeaderText = "Pose";
			this.ColPose.Name = "ColPose";
			// 
			// ColL
			// 
			this.ColL.HeaderText = "L";
			this.ColL.Name = "ColL";
			this.ColL.Width = 40;
			// 
			// ColT
			// 
			this.ColT.HeaderText = "T";
			this.ColT.Name = "ColT";
			this.ColT.Width = 40;
			// 
			// ColR
			// 
			this.ColR.HeaderText = "R";
			this.ColR.Name = "ColR";
			this.ColR.Width = 40;
			// 
			// ColB
			// 
			this.ColB.HeaderText = "B";
			this.ColB.Name = "ColB";
			this.ColB.Width = 40;
			// 
			// ColData
			// 
			this.ColData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.ColData.DefaultCellStyle = dataGridViewCellStyle1;
			this.ColData.HeaderText = "Code";
			this.ColData.Name = "ColData";
			// 
			// ColImage
			// 
			this.ColImage.HeaderText = "Image";
			this.ColImage.Name = "ColImage";
			this.ColImage.Width = 75;
			// 
			// ColImport
			// 
			this.ColImport.HeaderText = "";
			this.ColImport.Name = "ColImport";
			this.ColImport.Width = 70;
			// 
			// ImageManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Name = "ImageManager";
			this.Size = new System.Drawing.Size(966, 629);
			this.splitPreviewer.Panel1.ResumeLayout(false);
			this.splitPreviewer.Panel1.PerformLayout();
			this.splitPreviewer.Panel2.ResumeLayout(false);
			this.splitPreviewer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitPreviewer)).EndInit();
			this.splitPreviewer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridPoses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valWidth)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPoses.ResumeLayout(false);
			this.tabTemplate.ResumeLayout(false);
			this.tabTemplate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridEmotions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLayers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private DBPanel previewPanel;
		private System.Windows.Forms.SplitContainer splitPreviewer;
		private System.Windows.Forms.Button cmdCrop;
		private System.Windows.Forms.NumericUpDown valHeight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown valWidth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView gridPoses;
		private System.Windows.Forms.Button cmdExport;
		private System.Windows.Forms.Button cmdImport;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdClear;
		private System.Windows.Forms.Button cmdImportAll;
		private System.Windows.Forms.Button cmdImportNew;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblCurrentPoseFile;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPoses;
		private System.Windows.Forms.TabPage tabTemplate;
		private System.Windows.Forms.Button cmdSaveTemplate;
		private System.Windows.Forms.Button cmdLoadTemplate;
		private System.Windows.Forms.Button cmdGenerate;
		private System.Windows.Forms.TextBox txtBaseCode;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.DataGridView gridLayers;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DataGridView gridEmotions;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPoseKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLayerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColCode;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColBlush;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAnger;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColJuice;
		private System.Windows.Forms.Button cmdPreviewPose;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPose;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColL;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColT;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColR;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColB;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColData;
		private System.Windows.Forms.DataGridViewImageColumn ColImage;
		private System.Windows.Forms.DataGridViewButtonColumn ColImport;
	}
}

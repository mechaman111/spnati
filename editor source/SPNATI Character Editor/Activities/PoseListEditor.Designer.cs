namespace SPNATI_Character_Editor.Activities
{
	partial class PoseListEditor
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.cmdImportAll = new Desktop.Skinning.SkinnedButton();
			this.cmdImportNew = new Desktop.Skinning.SkinnedButton();
			this.cmdClear = new Desktop.Skinning.SkinnedButton();
			this.cmdExport = new Desktop.Skinning.SkinnedButton();
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.gridPoses = new Desktop.Skinning.SkinnedDataGridView();
			this.ColStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPose = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColR = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColB = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColAdvanced = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.ColImage = new System.Windows.Forms.DataGridViewImageColumn();
			this.ColImport = new Desktop.Skinning.SkinnedDataGridViewButtonColumn();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.lblCurrentPoseFile = new Desktop.Skinning.SkinnedLabel();
			this.chkRequired = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdCopyCrop = new Desktop.Skinning.SkinnedButton();
			this.cmdFolder = new Desktop.Skinning.SkinnedButton();
			this.cmdToMatrix = new Desktop.Skinning.SkinnedButton();
			((System.ComponentModel.ISupportInitialize)(this.gridPoses)).BeginInit();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdImportAll
			// 
			this.cmdImportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImportAll.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImportAll.Flat = false;
			this.cmdImportAll.Location = new System.Drawing.Point(843, 3);
			this.cmdImportAll.Name = "cmdImportAll";
			this.cmdImportAll.Size = new System.Drawing.Size(95, 23);
			this.cmdImportAll.TabIndex = 25;
			this.cmdImportAll.Text = "Import All";
			this.cmdImportAll.UseVisualStyleBackColor = true;
			this.cmdImportAll.Click += new System.EventHandler(this.cmdImportAll_Click);
			// 
			// cmdImportNew
			// 
			this.cmdImportNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImportNew.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImportNew.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImportNew.Flat = false;
			this.cmdImportNew.Location = new System.Drawing.Point(742, 3);
			this.cmdImportNew.Name = "cmdImportNew";
			this.cmdImportNew.Size = new System.Drawing.Size(95, 23);
			this.cmdImportNew.TabIndex = 24;
			this.cmdImportNew.Text = "Import New";
			this.cmdImportNew.UseVisualStyleBackColor = true;
			this.cmdImportNew.Click += new System.EventHandler(this.cmdImportNew_Click);
			// 
			// cmdClear
			// 
			this.cmdClear.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdClear.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdClear.Flat = true;
			this.cmdClear.ForeColor = System.Drawing.Color.Blue;
			this.cmdClear.Location = new System.Drawing.Point(237, 3);
			this.cmdClear.Name = "cmdClear";
			this.cmdClear.Size = new System.Drawing.Size(54, 23);
			this.cmdClear.TabIndex = 23;
			this.cmdClear.Text = "Clear";
			this.cmdClear.UseVisualStyleBackColor = true;
			this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
			// 
			// cmdExport
			// 
			this.cmdExport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdExport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdExport.Flat = false;
			this.cmdExport.Location = new System.Drawing.Point(120, 3);
			this.cmdExport.Name = "cmdExport";
			this.cmdExport.Size = new System.Drawing.Size(113, 23);
			this.cmdExport.TabIndex = 22;
			this.cmdExport.Text = "Save Pose List";
			this.cmdExport.UseVisualStyleBackColor = true;
			this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
			// 
			// cmdImport
			// 
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdImport.Flat = false;
			this.cmdImport.Location = new System.Drawing.Point(3, 3);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(113, 23);
			this.cmdImport.TabIndex = 21;
			this.cmdImport.Text = "Load Pose List";
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// gridPoses
			// 
			this.gridPoses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridPoses.BackgroundColor = System.Drawing.Color.White;
			this.gridPoses.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridPoses.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.gridPoses.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridPoses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.gridPoses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridPoses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColStage,
            this.ColPose,
            this.ColL,
            this.ColT,
            this.ColR,
            this.ColB,
            this.ColData,
            this.ColAdvanced,
            this.ColImage,
            this.ColImport});
			this.gridPoses.ContextMenuStrip = this.contextMenu;
			this.gridPoses.Data = null;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridPoses.DefaultCellStyle = dataGridViewCellStyle9;
			this.gridPoses.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridPoses.EnableHeadersVisualStyles = false;
			this.gridPoses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridPoses.GridColor = System.Drawing.Color.LightGray;
			this.gridPoses.Location = new System.Drawing.Point(3, 61);
			this.gridPoses.MultiSelect = false;
			this.gridPoses.Name = "gridPoses";
			this.gridPoses.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridPoses.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.gridPoses.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.gridPoses.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridPoses.RowTemplate.Height = 100;
			this.gridPoses.Size = new System.Drawing.Size(935, 562);
			this.gridPoses.TabIndex = 20;
			this.gridPoses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPoses_CellContentClick);
			this.gridPoses.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPoses_CellEnter);
			this.gridPoses.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridPoses_ColumnHeaderMouseClick);
			this.gridPoses.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.gridPoses_RowPrePaint);
			this.gridPoses.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridPoses_RowsAdded);
			this.gridPoses.Scroll += new System.Windows.Forms.ScrollEventHandler(this.gridPoses_Scroll);
			this.gridPoses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridPoses_MouseDown);
			// 
			// ColStage
			// 
			this.ColStage.HeaderText = "Stage";
			this.ColStage.Name = "ColStage";
			this.ColStage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.ColStage.Width = 50;
			// 
			// ColPose
			// 
			this.ColPose.HeaderText = "Pose";
			this.ColPose.Name = "ColPose";
			this.ColPose.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// ColL
			// 
			this.ColL.HeaderText = "L";
			this.ColL.Name = "ColL";
			this.ColL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColL.Width = 40;
			// 
			// ColT
			// 
			this.ColT.HeaderText = "T";
			this.ColT.Name = "ColT";
			this.ColT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColT.Width = 40;
			// 
			// ColR
			// 
			this.ColR.HeaderText = "R";
			this.ColR.Name = "ColR";
			this.ColR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColR.Width = 40;
			// 
			// ColB
			// 
			this.ColB.HeaderText = "B";
			this.ColB.Name = "ColB";
			this.ColB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColB.Width = 40;
			// 
			// ColData
			// 
			this.ColData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.ColData.DefaultCellStyle = dataGridViewCellStyle7;
			this.ColData.HeaderText = "Code";
			this.ColData.Name = "ColData";
			this.ColData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColAdvanced
			// 
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.NullValue = "More...";
			this.ColAdvanced.DefaultCellStyle = dataGridViewCellStyle8;
			this.ColAdvanced.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColAdvanced.Flat = false;
			this.ColAdvanced.HeaderText = "";
			this.ColAdvanced.Name = "ColAdvanced";
			this.ColAdvanced.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColAdvanced.Width = 70;
			// 
			// ColImage
			// 
			this.ColImage.HeaderText = "Image";
			this.ColImage.Name = "ColImage";
			this.ColImage.Width = 75;
			// 
			// ColImport
			// 
			this.ColImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.ColImport.Flat = false;
			this.ColImport.HeaderText = "";
			this.ColImport.Name = "ColImport";
			this.ColImport.Width = 90;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.duplicateToolStripMenuItem});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(167, 98);
			this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Enabled = false;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
			// 
			// duplicateToolStripMenuItem
			// 
			this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			this.duplicateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.duplicateToolStripMenuItem.Text = "Duplicate";
			this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(3, 7);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 33;
			this.label5.Text = "Import Preview";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Text files|*.txt";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Text files|*.txt";
			// 
			// lblCurrentPoseFile
			// 
			this.lblCurrentPoseFile.AutoSize = true;
			this.lblCurrentPoseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblCurrentPoseFile.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCurrentPoseFile.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblCurrentPoseFile.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblCurrentPoseFile.Location = new System.Drawing.Point(297, 7);
			this.lblCurrentPoseFile.Name = "lblCurrentPoseFile";
			this.lblCurrentPoseFile.Size = new System.Drawing.Size(100, 13);
			this.lblCurrentPoseFile.TabIndex = 34;
			this.lblCurrentPoseFile.Text = "No pose list loaded.";
			// 
			// chkRequired
			// 
			this.chkRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkRequired.AutoSize = true;
			this.chkRequired.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkRequired.Location = new System.Drawing.Point(809, 36);
			this.chkRequired.Name = "chkRequired";
			this.chkRequired.Size = new System.Drawing.Size(129, 17);
			this.chkRequired.TabIndex = 35;
			this.chkRequired.Text = "Include missing poses";
			this.chkRequired.UseVisualStyleBackColor = true;
			// 
			// cmdCopyCrop
			// 
			this.cmdCopyCrop.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCopyCrop.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCopyCrop.Flat = true;
			this.cmdCopyCrop.ForeColor = System.Drawing.Color.Blue;
			this.cmdCopyCrop.Location = new System.Drawing.Point(3, 32);
			this.cmdCopyCrop.Name = "cmdCopyCrop";
			this.cmdCopyCrop.Size = new System.Drawing.Size(113, 23);
			this.cmdCopyCrop.TabIndex = 36;
			this.cmdCopyCrop.Text = "Copy Cropping";
			this.cmdCopyCrop.UseVisualStyleBackColor = true;
			this.cmdCopyCrop.Click += new System.EventHandler(this.cmdCopyCrop_Click);
			// 
			// cmdFolder
			// 
			this.cmdFolder.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdFolder.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdFolder.Flat = true;
			this.cmdFolder.ForeColor = System.Drawing.Color.Blue;
			this.cmdFolder.Location = new System.Drawing.Point(120, 32);
			this.cmdFolder.Name = "cmdFolder";
			this.cmdFolder.Size = new System.Drawing.Size(113, 23);
			this.cmdFolder.TabIndex = 37;
			this.cmdFolder.Text = "Open Folder";
			this.cmdFolder.UseVisualStyleBackColor = true;
			this.cmdFolder.Click += new System.EventHandler(this.cmdFolder_Click);
			// 
			// cmdToMatrix
			// 
			this.cmdToMatrix.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdToMatrix.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdToMatrix.Flat = true;
			this.cmdToMatrix.ForeColor = System.Drawing.Color.Blue;
			this.cmdToMatrix.Location = new System.Drawing.Point(237, 32);
			this.cmdToMatrix.Name = "cmdToMatrix";
			this.cmdToMatrix.Size = new System.Drawing.Size(113, 23);
			this.cmdToMatrix.TabIndex = 38;
			this.cmdToMatrix.Text = "Add to Matrix";
			this.cmdToMatrix.UseVisualStyleBackColor = true;
			this.cmdToMatrix.Click += new System.EventHandler(this.cmdToMatrix_Click);
			// 
			// PoseListEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdToMatrix);
			this.Controls.Add(this.cmdFolder);
			this.Controls.Add(this.cmdCopyCrop);
			this.Controls.Add(this.chkRequired);
			this.Controls.Add(this.lblCurrentPoseFile);
			this.Controls.Add(this.cmdImportAll);
			this.Controls.Add(this.cmdImportNew);
			this.Controls.Add(this.cmdClear);
			this.Controls.Add(this.cmdExport);
			this.Controls.Add(this.cmdImport);
			this.Controls.Add(this.gridPoses);
			this.Controls.Add(this.label5);
			this.Name = "PoseListEditor";
			this.Size = new System.Drawing.Size(941, 626);
			((System.ComponentModel.ISupportInitialize)(this.gridPoses)).EndInit();
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Desktop.Skinning.SkinnedButton cmdImportAll;
		private Desktop.Skinning.SkinnedButton cmdImportNew;
		private Desktop.Skinning.SkinnedButton cmdClear;
		private Desktop.Skinning.SkinnedButton cmdExport;
		private Desktop.Skinning.SkinnedButton cmdImport;
		private Desktop.Skinning.SkinnedDataGridView gridPoses;
		private Desktop.Skinning.SkinnedLabel label5;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private Desktop.Skinning.SkinnedLabel lblCurrentPoseFile;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
		private Desktop.Skinning.SkinnedCheckBox chkRequired;
		private Desktop.Skinning.SkinnedButton cmdCopyCrop;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColStage;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPose;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColL;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColT;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColR;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColB;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColData;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColAdvanced;
		private System.Windows.Forms.DataGridViewImageColumn ColImage;
		private Desktop.Skinning.SkinnedDataGridViewButtonColumn ColImport;
		private Desktop.Skinning.SkinnedButton cmdFolder;
		private Desktop.Skinning.SkinnedButton cmdToMatrix;
	}
}

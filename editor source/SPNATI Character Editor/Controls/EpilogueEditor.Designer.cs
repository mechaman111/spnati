namespace SPNATI_Character_Editor.Controls
{
	partial class EpilogueEditor
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
			this.cboEnding = new Desktop.Skinning.SkinnedComboBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cmdDeleteEnding = new Desktop.Skinning.SkinnedButton();
			this.cmdAddEnding = new Desktop.Skinning.SkinnedButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pnlHeader = new Desktop.Skinning.SkinnedPanel();
			this.tmrActivate = new System.Windows.Forms.Timer(this.components);
			this.tabs = new Desktop.Skinning.SkinnedTabControl();
			this.pageGeneral = new System.Windows.Forms.TabPage();
			this.grpMarkers = new Desktop.Skinning.SkinnedGroupBox();
			this.gridMarkers = new SPNATI_Character_Editor.Controls.MarkerControl();
			this.grpConditions = new Desktop.Skinning.SkinnedGroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.selAlsoPlayingAnyMarkers = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.selAlsoPlayingNotMarkers = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.selAlsoPlayingAllMarkers = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.groupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.selAnyMarkers = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.selNotMarkers = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.selAllMarkers = new SPNATI_Character_Editor.Controls.RecordSelectBox();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.label7 = new Desktop.Skinning.SkinnedLabel();
			this.grpInfo = new Desktop.Skinning.SkinnedGroupBox();
			this.tableGeneral = new Desktop.CommonControls.PropertyTable();
			this.pageScenes = new System.Windows.Forms.TabPage();
			this.canvas = new SPNATI_Character_Editor.Controls.EpilogueCanvas();
			this.pageEditor = new System.Windows.Forms.TabPage();
			this.liveEditor = new SPNATI_Character_Editor.Controls.LiveEpilogueEditor();
			this.strip = new Desktop.Skinning.SkinnedTabStrip();
			this.pnlHeader.SuspendLayout();
			this.tabs.SuspendLayout();
			this.pageGeneral.SuspendLayout();
			this.grpMarkers.SuspendLayout();
			this.grpConditions.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.grpInfo.SuspendLayout();
			this.pageScenes.SuspendLayout();
			this.pageEditor.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboEnding
			// 
			this.cboEnding.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboEnding.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboEnding.BackColor = System.Drawing.Color.White;
			this.cboEnding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEnding.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cboEnding.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboEnding.FormattingEnabled = true;
			this.cboEnding.KeyMember = null;
			this.cboEnding.Location = new System.Drawing.Point(49, 8);
			this.cboEnding.Name = "cboEnding";
			this.cboEnding.SelectedIndex = -1;
			this.cboEnding.SelectedItem = null;
			this.cboEnding.Size = new System.Drawing.Size(170, 21);
			this.cboEnding.Sorted = false;
			this.cboEnding.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cboEnding, "Select an ending to edit");
			this.cboEnding.SelectedIndexChanged += new System.EventHandler(this.cboEnding_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Primary;
			this.label1.Location = new System.Drawing.Point(3, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Ending:";
			// 
			// imageFileDialog
			// 
			this.imageFileDialog.FileName = "openFileDialog1";
			// 
			// cmdDeleteEnding
			// 
			this.cmdDeleteEnding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDeleteEnding.Background = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.cmdDeleteEnding.Enabled = false;
			this.cmdDeleteEnding.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdDeleteEnding.Flat = false;
			this.cmdDeleteEnding.ForeColor = System.Drawing.Color.Red;
			this.cmdDeleteEnding.Location = new System.Drawing.Point(892, 7);
			this.cmdDeleteEnding.Name = "cmdDeleteEnding";
			this.cmdDeleteEnding.Size = new System.Drawing.Size(75, 23);
			this.cmdDeleteEnding.TabIndex = 20;
			this.cmdDeleteEnding.Text = "Delete";
			this.toolTip1.SetToolTip(this.cmdDeleteEnding, "Delete this ending");
			this.cmdDeleteEnding.UseVisualStyleBackColor = true;
			this.cmdDeleteEnding.Click += new System.EventHandler(this.cmdDeleteEnding_Click);
			// 
			// cmdAddEnding
			// 
			this.cmdAddEnding.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAddEnding.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdAddEnding.Flat = false;
			this.cmdAddEnding.Location = new System.Drawing.Point(225, 7);
			this.cmdAddEnding.Name = "cmdAddEnding";
			this.cmdAddEnding.Size = new System.Drawing.Size(75, 23);
			this.cmdAddEnding.TabIndex = 21;
			this.cmdAddEnding.Text = "Add New";
			this.toolTip1.SetToolTip(this.cmdAddEnding, "Add a new ending");
			this.cmdAddEnding.UseVisualStyleBackColor = true;
			this.cmdAddEnding.Click += new System.EventHandler(this.cmdAddEnding_Click);
			// 
			// pnlHeader
			// 
			this.pnlHeader.Controls.Add(this.label1);
			this.pnlHeader.Controls.Add(this.cboEnding);
			this.pnlHeader.Controls.Add(this.cmdAddEnding);
			this.pnlHeader.Controls.Add(this.cmdDeleteEnding);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.PanelType = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.pnlHeader.Size = new System.Drawing.Size(973, 36);
			this.pnlHeader.TabIndex = 24;
			this.pnlHeader.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// tmrActivate
			// 
			this.tmrActivate.Interval = 1;
			this.tmrActivate.Tick += new System.EventHandler(this.tmrActivate_Tick);
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.pageGeneral);
			this.tabs.Controls.Add(this.pageScenes);
			this.tabs.Controls.Add(this.pageEditor);
			this.tabs.Enabled = false;
			this.tabs.Location = new System.Drawing.Point(0, 57);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(973, 589);
			this.tabs.TabIndex = 25;
			// 
			// pageGeneral
			// 
			this.pageGeneral.BackColor = System.Drawing.Color.White;
			this.pageGeneral.Controls.Add(this.grpMarkers);
			this.pageGeneral.Controls.Add(this.grpConditions);
			this.pageGeneral.Controls.Add(this.grpInfo);
			this.pageGeneral.ForeColor = System.Drawing.Color.Black;
			this.pageGeneral.Location = new System.Drawing.Point(4, 22);
			this.pageGeneral.Name = "pageGeneral";
			this.pageGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.pageGeneral.Size = new System.Drawing.Size(965, 563);
			this.pageGeneral.TabIndex = 0;
			this.pageGeneral.Text = "General";
			// 
			// grpMarkers
			// 
			this.grpMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.grpMarkers.BackColor = System.Drawing.Color.White;
			this.grpMarkers.Controls.Add(this.gridMarkers);
			this.grpMarkers.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpMarkers.Image = null;
			this.grpMarkers.Location = new System.Drawing.Point(374, 409);
			this.grpMarkers.Name = "grpMarkers";
			this.grpMarkers.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpMarkers.ShowIndicatorBar = false;
			this.grpMarkers.Size = new System.Drawing.Size(585, 151);
			this.grpMarkers.TabIndex = 3;
			this.grpMarkers.TabStop = false;
			this.grpMarkers.Text = "Markers to Set";
			// 
			// gridMarkers
			// 
			this.gridMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMarkers.Location = new System.Drawing.Point(5, 23);
			this.gridMarkers.Name = "gridMarkers";
			this.gridMarkers.ShowWhen = true;
			this.gridMarkers.Size = new System.Drawing.Size(577, 123);
			this.gridMarkers.TabIndex = 0;
			// 
			// grpConditions
			// 
			this.grpConditions.BackColor = System.Drawing.Color.White;
			this.grpConditions.Controls.Add(this.tableLayoutPanel1);
			this.grpConditions.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpConditions.Image = null;
			this.grpConditions.Location = new System.Drawing.Point(374, 6);
			this.grpConditions.Name = "grpConditions";
			this.grpConditions.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpConditions.ShowIndicatorBar = false;
			this.grpConditions.Size = new System.Drawing.Size(585, 397);
			this.grpConditions.TabIndex = 2;
			this.grpConditions.TabStop = false;
			this.grpConditions.Text = "Advanced Unlock Conditions";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 22);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(579, 369);
			this.tableLayoutPanel1.TabIndex = 46;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.White;
			this.groupBox2.Controls.Add(this.selAlsoPlayingAnyMarkers);
			this.groupBox2.Controls.Add(this.selAlsoPlayingNotMarkers);
			this.groupBox2.Controls.Add(this.selAlsoPlayingAllMarkers);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.groupBox2.Image = null;
			this.groupBox2.Location = new System.Drawing.Point(292, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.groupBox2.ShowIndicatorBar = false;
			this.groupBox2.Size = new System.Drawing.Size(279, 360);
			this.groupBox2.TabIndex = 45;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Other characters\' markers";
			// 
			// selAlsoPlayingAnyMarkers
			// 
			this.selAlsoPlayingAnyMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAlsoPlayingAnyMarkers.Location = new System.Drawing.Point(6, 263);
			this.selAlsoPlayingAnyMarkers.Name = "selAlsoPlayingAnyMarkers";
			this.selAlsoPlayingAnyMarkers.RecordContext = null;
			this.selAlsoPlayingAnyMarkers.RecordFilter = null;
			this.selAlsoPlayingAnyMarkers.RecordType = null;
			this.selAlsoPlayingAnyMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingAnyMarkers.Size = new System.Drawing.Size(267, 86);
			this.selAlsoPlayingAnyMarkers.TabIndex = 7;
			// 
			// selAlsoPlayingNotMarkers
			// 
			this.selAlsoPlayingNotMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAlsoPlayingNotMarkers.Location = new System.Drawing.Point(6, 150);
			this.selAlsoPlayingNotMarkers.Name = "selAlsoPlayingNotMarkers";
			this.selAlsoPlayingNotMarkers.RecordContext = null;
			this.selAlsoPlayingNotMarkers.RecordFilter = null;
			this.selAlsoPlayingNotMarkers.RecordType = null;
			this.selAlsoPlayingNotMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingNotMarkers.Size = new System.Drawing.Size(267, 86);
			this.selAlsoPlayingNotMarkers.TabIndex = 6;
			// 
			// selAlsoPlayingAllMarkers
			// 
			this.selAlsoPlayingAllMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAlsoPlayingAllMarkers.Location = new System.Drawing.Point(6, 38);
			this.selAlsoPlayingAllMarkers.Name = "selAlsoPlayingAllMarkers";
			this.selAlsoPlayingAllMarkers.RecordContext = null;
			this.selAlsoPlayingAllMarkers.RecordFilter = null;
			this.selAlsoPlayingAllMarkers.RecordType = null;
			this.selAlsoPlayingAllMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingAllMarkers.Size = new System.Drawing.Size(267, 86);
			this.selAlsoPlayingAllMarkers.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label4.Location = new System.Drawing.Point(10, 247);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(225, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Require any of the following markers to be set:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(10, 134);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(232, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Require none of the following markers to be set:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label6.Location = new System.Drawing.Point(7, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(206, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Require all the following markers to be set:";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.White;
			this.groupBox1.Controls.Add(this.selAnyMarkers);
			this.groupBox1.Controls.Add(this.selNotMarkers);
			this.groupBox1.Controls.Add(this.selAllMarkers);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.groupBox1.Image = null;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.groupBox1.ShowIndicatorBar = false;
			this.groupBox1.Size = new System.Drawing.Size(283, 360);
			this.groupBox1.TabIndex = 44;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Own markers";
			// 
			// selAnyMarkers
			// 
			this.selAnyMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAnyMarkers.Location = new System.Drawing.Point(6, 263);
			this.selAnyMarkers.Name = "selAnyMarkers";
			this.selAnyMarkers.RecordContext = null;
			this.selAnyMarkers.RecordFilter = null;
			this.selAnyMarkers.RecordType = null;
			this.selAnyMarkers.SelectedItems = new string[0];
			this.selAnyMarkers.Size = new System.Drawing.Size(271, 86);
			this.selAnyMarkers.TabIndex = 4;
			// 
			// selNotMarkers
			// 
			this.selNotMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selNotMarkers.Location = new System.Drawing.Point(6, 150);
			this.selNotMarkers.Name = "selNotMarkers";
			this.selNotMarkers.RecordContext = null;
			this.selNotMarkers.RecordFilter = null;
			this.selNotMarkers.RecordType = null;
			this.selNotMarkers.SelectedItems = new string[0];
			this.selNotMarkers.Size = new System.Drawing.Size(271, 86);
			this.selNotMarkers.TabIndex = 3;
			// 
			// selAllMarkers
			// 
			this.selAllMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAllMarkers.Location = new System.Drawing.Point(6, 38);
			this.selAllMarkers.Name = "selAllMarkers";
			this.selAllMarkers.RecordContext = null;
			this.selAllMarkers.RecordFilter = null;
			this.selAllMarkers.RecordType = null;
			this.selAllMarkers.SelectedItems = new string[0];
			this.selAllMarkers.Size = new System.Drawing.Size(271, 86);
			this.selAllMarkers.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(10, 247);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(225, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Require any of the following markers to be set:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(10, 134);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(232, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Require none of the following markers to be set:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label7.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label7.Location = new System.Drawing.Point(7, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(206, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Require all the following markers to be set:";
			// 
			// grpInfo
			// 
			this.grpInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.grpInfo.BackColor = System.Drawing.Color.White;
			this.grpInfo.Controls.Add(this.tableGeneral);
			this.grpInfo.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpInfo.Image = null;
			this.grpInfo.Location = new System.Drawing.Point(6, 6);
			this.grpInfo.Name = "grpInfo";
			this.grpInfo.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpInfo.ShowIndicatorBar = false;
			this.grpInfo.Size = new System.Drawing.Size(362, 551);
			this.grpInfo.TabIndex = 1;
			this.grpInfo.TabStop = false;
			this.grpInfo.Text = "Information & Basic Conditions";
			// 
			// tableGeneral
			// 
			this.tableGeneral.AllowDelete = false;
			this.tableGeneral.AllowFavorites = false;
			this.tableGeneral.AllowHelp = true;
			this.tableGeneral.AllowMacros = false;
			this.tableGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableGeneral.BackColor = System.Drawing.Color.White;
			this.tableGeneral.Data = null;
			this.tableGeneral.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableGeneral.HideAddField = true;
			this.tableGeneral.HideSpeedButtons = true;
			this.tableGeneral.Location = new System.Drawing.Point(3, 22);
			this.tableGeneral.ModifyingProperty = null;
			this.tableGeneral.Name = "tableGeneral";
			this.tableGeneral.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableGeneral.PlaceholderText = null;
			this.tableGeneral.PreserveControls = false;
			this.tableGeneral.PreviewData = null;
			this.tableGeneral.RemoveCaption = "Remove";
			this.tableGeneral.RowHeaderWidth = 130F;
			this.tableGeneral.RunInitialAddEvents = false;
			this.tableGeneral.Size = new System.Drawing.Size(356, 526);
			this.tableGeneral.Sorted = true;
			this.tableGeneral.TabIndex = 0;
			this.tableGeneral.UndoManager = null;
			this.tableGeneral.UseAutoComplete = false;
			// 
			// pageScenes
			// 
			this.pageScenes.BackColor = System.Drawing.Color.White;
			this.pageScenes.Controls.Add(this.canvas);
			this.pageScenes.ForeColor = System.Drawing.Color.Black;
			this.pageScenes.Location = new System.Drawing.Point(4, 22);
			this.pageScenes.Name = "pageScenes";
			this.pageScenes.Size = new System.Drawing.Size(965, 563);
			this.pageScenes.TabIndex = 1;
			this.pageScenes.Text = "Scenes";
			// 
			// canvas
			// 
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Enabled = false;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Margin = new System.Windows.Forms.Padding(0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(965, 563);
			this.canvas.TabIndex = 23;
			this.canvas.ZoomLevel = 1F;
			// 
			// pageEditor
			// 
			this.pageEditor.BackColor = System.Drawing.Color.White;
			this.pageEditor.Controls.Add(this.liveEditor);
			this.pageEditor.ForeColor = System.Drawing.Color.Black;
			this.pageEditor.Location = new System.Drawing.Point(4, 22);
			this.pageEditor.Name = "pageEditor";
			this.pageEditor.Padding = new System.Windows.Forms.Padding(3);
			this.pageEditor.Size = new System.Drawing.Size(965, 563);
			this.pageEditor.TabIndex = 2;
			this.pageEditor.Text = "Scenes";
			// 
			// liveEditor
			// 
			this.liveEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.liveEditor.Location = new System.Drawing.Point(3, 3);
			this.liveEditor.Name = "liveEditor";
			this.liveEditor.Size = new System.Drawing.Size(959, 557);
			this.liveEditor.TabIndex = 0;
			// 
			// strip
			// 
			this.strip.AddCaption = null;
			this.strip.DecorationText = null;
			this.strip.Dock = System.Windows.Forms.DockStyle.Top;
			this.strip.Location = new System.Drawing.Point(0, 36);
			this.strip.Margin = new System.Windows.Forms.Padding(0);
			this.strip.Name = "strip";
			this.strip.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.strip.ShowAddButton = false;
			this.strip.ShowCloseButton = false;
			this.strip.Size = new System.Drawing.Size(973, 23);
			this.strip.StartMargin = 5;
			this.strip.TabControl = this.tabs;
			this.strip.TabIndex = 22;
			this.strip.TabMargin = 5;
			this.strip.TabPadding = 20;
			this.strip.TabSize = -1;
			this.strip.TabType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.strip.Text = "skinnedTabStrip1";
			this.strip.Vertical = false;
			// 
			// EpilogueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.strip);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.pnlHeader);
			this.Name = "EpilogueEditor";
			this.Size = new System.Drawing.Size(973, 646);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.tabs.ResumeLayout(false);
			this.pageGeneral.ResumeLayout(false);
			this.grpMarkers.ResumeLayout(false);
			this.grpConditions.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpInfo.ResumeLayout(false);
			this.pageScenes.ResumeLayout(false);
			this.pageEditor.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.Skinning.SkinnedComboBox cboEnding;
		private Desktop.Skinning.SkinnedLabel label1;
		private System.Windows.Forms.OpenFileDialog imageFileDialog;
		private Desktop.Skinning.SkinnedButton cmdDeleteEnding;
		private Desktop.Skinning.SkinnedButton cmdAddEnding;
		private System.Windows.Forms.ToolTip toolTip1;
		private EpilogueCanvas canvas;
		private Desktop.Skinning.SkinnedPanel pnlHeader;
		private System.Windows.Forms.Timer tmrActivate;
		private Desktop.Skinning.SkinnedTabControl tabs;
		private System.Windows.Forms.TabPage pageGeneral;
		private System.Windows.Forms.TabPage pageScenes;
		private Desktop.CommonControls.PropertyTable tableGeneral;
		private Desktop.Skinning.SkinnedGroupBox grpInfo;
		private Desktop.Skinning.SkinnedGroupBox grpConditions;
		private Desktop.Skinning.SkinnedGroupBox groupBox2;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedGroupBox groupBox1;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedLabel label7;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Desktop.Skinning.SkinnedTabStrip strip;
		private Desktop.Skinning.SkinnedGroupBox grpMarkers;
		private MarkerControl gridMarkers;
		private RecordSelectBox selAllMarkers;
		private RecordSelectBox selAnyMarkers;
		private RecordSelectBox selNotMarkers;
		private RecordSelectBox selAlsoPlayingAllMarkers;
		private RecordSelectBox selAlsoPlayingAnyMarkers;
		private RecordSelectBox selAlsoPlayingNotMarkers;
		private System.Windows.Forms.TabPage pageEditor;
		private LiveEpilogueEditor liveEditor;
	}
}

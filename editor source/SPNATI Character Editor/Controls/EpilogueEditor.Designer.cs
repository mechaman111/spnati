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
			this.cboEnding = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cmdDeleteEnding = new System.Windows.Forms.Button();
			this.cmdAddEnding = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.tmrActivate = new System.Windows.Forms.Timer(this.components);
			this.tabs = new System.Windows.Forms.TabControl();
			this.pageGeneral = new System.Windows.Forms.TabPage();
			this.grpConditions = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.grpInfo = new System.Windows.Forms.GroupBox();
			this.tableGeneral = new Desktop.CommonControls.PropertyTable();
			this.pageScenes = new System.Windows.Forms.TabPage();
			this.selAnyMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selNotMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAllMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAlsoPlayingAnyMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAlsoPlayingNotMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAlsoPlayingAllMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.canvas = new SPNATI_Character_Editor.Controls.EpilogueCanvas();
			this.pnlHeader.SuspendLayout();
			this.tabs.SuspendLayout();
			this.pageGeneral.SuspendLayout();
			this.grpConditions.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.grpInfo.SuspendLayout();
			this.pageScenes.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboEnding
			// 
			this.cboEnding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEnding.FormattingEnabled = true;
			this.cboEnding.Location = new System.Drawing.Point(49, 8);
			this.cboEnding.Name = "cboEnding";
			this.cboEnding.Size = new System.Drawing.Size(170, 21);
			this.cboEnding.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cboEnding, "Select an ending to edit");
			this.cboEnding.SelectedIndexChanged += new System.EventHandler(this.cboEnding_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
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
			this.cmdDeleteEnding.Enabled = false;
			this.cmdDeleteEnding.Location = new System.Drawing.Point(866, 7);
			this.cmdDeleteEnding.Name = "cmdDeleteEnding";
			this.cmdDeleteEnding.Size = new System.Drawing.Size(101, 23);
			this.cmdDeleteEnding.TabIndex = 20;
			this.cmdDeleteEnding.Text = "Delete Ending";
			this.toolTip1.SetToolTip(this.cmdDeleteEnding, "Delete this ending");
			this.cmdDeleteEnding.UseVisualStyleBackColor = true;
			this.cmdDeleteEnding.Click += new System.EventHandler(this.cmdDeleteEnding_Click);
			// 
			// cmdAddEnding
			// 
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
			this.pnlHeader.Size = new System.Drawing.Size(973, 36);
			this.pnlHeader.TabIndex = 24;
			// 
			// tmrActivate
			// 
			this.tmrActivate.Interval = 1;
			this.tmrActivate.Tick += new System.EventHandler(this.tmrActivate_Tick);
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.pageGeneral);
			this.tabs.Controls.Add(this.pageScenes);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Enabled = false;
			this.tabs.Location = new System.Drawing.Point(0, 36);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(973, 610);
			this.tabs.TabIndex = 25;
			// 
			// pageGeneral
			// 
			this.pageGeneral.Controls.Add(this.grpConditions);
			this.pageGeneral.Controls.Add(this.grpInfo);
			this.pageGeneral.Location = new System.Drawing.Point(4, 22);
			this.pageGeneral.Name = "pageGeneral";
			this.pageGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.pageGeneral.Size = new System.Drawing.Size(965, 584);
			this.pageGeneral.TabIndex = 0;
			this.pageGeneral.Text = "General";
			this.pageGeneral.UseVisualStyleBackColor = true;
			// 
			// grpConditions
			// 
			this.grpConditions.Controls.Add(this.tableLayoutPanel1);
			this.grpConditions.Location = new System.Drawing.Point(374, 6);
			this.grpConditions.Name = "grpConditions";
			this.grpConditions.Size = new System.Drawing.Size(585, 572);
			this.grpConditions.TabIndex = 2;
			this.grpConditions.TabStop = false;
			this.grpConditions.Text = "Unlock Conditions";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(579, 553);
			this.tableLayoutPanel1.TabIndex = 46;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.selAnyMarkers);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.selNotMarkers);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.selAllMarkers);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(283, 360);
			this.groupBox1.TabIndex = 44;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Own markers";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 245);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(225, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Require any of the following markers to be set:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(232, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Require none of the following markers to be set:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(206, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Require all the following markers to be set:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.selAlsoPlayingAnyMarkers);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.selAlsoPlayingNotMarkers);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.selAlsoPlayingAllMarkers);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(292, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(279, 360);
			this.groupBox2.TabIndex = 45;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Other markers";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 245);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(225, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Require any of the following markers to be set:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(10, 132);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(232, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Require none of the following markers to be set:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(206, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Require all the following markers to be set:";
			// 
			// grpInfo
			// 
			this.grpInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.grpInfo.Controls.Add(this.tableGeneral);
			this.grpInfo.Location = new System.Drawing.Point(6, 6);
			this.grpInfo.Name = "grpInfo";
			this.grpInfo.Size = new System.Drawing.Size(362, 572);
			this.grpInfo.TabIndex = 1;
			this.grpInfo.TabStop = false;
			this.grpInfo.Text = "Information";
			// 
			// tableGeneral
			// 
			this.tableGeneral.AllowDelete = false;
			this.tableGeneral.AllowFavorites = false;
			this.tableGeneral.AllowHelp = true;
			this.tableGeneral.BackColor = System.Drawing.Color.Transparent;
			this.tableGeneral.Data = null;
			this.tableGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableGeneral.HideAddField = true;
			this.tableGeneral.HideSpeedButtons = true;
			this.tableGeneral.Location = new System.Drawing.Point(3, 16);
			this.tableGeneral.Name = "tableGeneral";
			this.tableGeneral.PlaceholderText = null;
			this.tableGeneral.RemoveCaption = "Remove";
			this.tableGeneral.RowHeaderWidth = 130F;
			this.tableGeneral.Size = new System.Drawing.Size(356, 553);
			this.tableGeneral.Sorted = true;
			this.tableGeneral.TabIndex = 0;
			this.tableGeneral.UseAutoComplete = false;
			// 
			// pageScenes
			// 
			this.pageScenes.Controls.Add(this.canvas);
			this.pageScenes.Location = new System.Drawing.Point(4, 22);
			this.pageScenes.Name = "pageScenes";
			this.pageScenes.Padding = new System.Windows.Forms.Padding(3);
			this.pageScenes.Size = new System.Drawing.Size(965, 584);
			this.pageScenes.TabIndex = 1;
			this.pageScenes.Text = "Scenes";
			this.pageScenes.UseVisualStyleBackColor = true;
			// 
			// selAnyMarkers
			// 
			this.selAnyMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAnyMarkers.Location = new System.Drawing.Point(10, 262);
			this.selAnyMarkers.Name = "selAnyMarkers";
			this.selAnyMarkers.SelectedItems = new string[0];
			this.selAnyMarkers.Size = new System.Drawing.Size(267, 85);
			this.selAnyMarkers.TabIndex = 1;
			// 
			// selNotMarkers
			// 
			this.selNotMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selNotMarkers.Location = new System.Drawing.Point(10, 149);
			this.selNotMarkers.Name = "selNotMarkers";
			this.selNotMarkers.SelectedItems = new string[0];
			this.selNotMarkers.Size = new System.Drawing.Size(267, 85);
			this.selNotMarkers.TabIndex = 1;
			// 
			// selAllMarkers
			// 
			this.selAllMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAllMarkers.Location = new System.Drawing.Point(7, 37);
			this.selAllMarkers.Name = "selAllMarkers";
			this.selAllMarkers.SelectedItems = new string[0];
			this.selAllMarkers.Size = new System.Drawing.Size(270, 85);
			this.selAllMarkers.TabIndex = 1;
			// 
			// selAlsoPlayingAnyMarkers
			// 
			this.selAlsoPlayingAnyMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAlsoPlayingAnyMarkers.Location = new System.Drawing.Point(10, 262);
			this.selAlsoPlayingAnyMarkers.Name = "selAlsoPlayingAnyMarkers";
			this.selAlsoPlayingAnyMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingAnyMarkers.Size = new System.Drawing.Size(263, 85);
			this.selAlsoPlayingAnyMarkers.TabIndex = 1;
			// 
			// selAlsoPlayingNotMarkers
			// 
			this.selAlsoPlayingNotMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAlsoPlayingNotMarkers.Location = new System.Drawing.Point(10, 149);
			this.selAlsoPlayingNotMarkers.Name = "selAlsoPlayingNotMarkers";
			this.selAlsoPlayingNotMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingNotMarkers.Size = new System.Drawing.Size(263, 85);
			this.selAlsoPlayingNotMarkers.TabIndex = 1;
			// 
			// selAlsoPlayingAllMarkers
			// 
			this.selAlsoPlayingAllMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.selAlsoPlayingAllMarkers.Location = new System.Drawing.Point(7, 37);
			this.selAlsoPlayingAllMarkers.Name = "selAlsoPlayingAllMarkers";
			this.selAlsoPlayingAllMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingAllMarkers.Size = new System.Drawing.Size(266, 85);
			this.selAlsoPlayingAllMarkers.TabIndex = 1;
			// 
			// canvas
			// 
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Enabled = false;
			this.canvas.Location = new System.Drawing.Point(3, 3);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(959, 578);
			this.canvas.TabIndex = 23;
			this.canvas.ZoomLevel = 1F;
			// 
			// EpilogueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.pnlHeader);
			this.Name = "EpilogueEditor";
			this.Size = new System.Drawing.Size(973, 646);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.tabs.ResumeLayout(false);
			this.pageGeneral.ResumeLayout(false);
			this.grpConditions.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.grpInfo.ResumeLayout(false);
			this.pageScenes.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ComboBox cboEnding;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog imageFileDialog;
		private System.Windows.Forms.Button cmdDeleteEnding;
		private System.Windows.Forms.Button cmdAddEnding;
		private System.Windows.Forms.ToolTip toolTip1;
		private EpilogueCanvas canvas;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Timer tmrActivate;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage pageGeneral;
		private System.Windows.Forms.TabPage pageScenes;
		private Desktop.CommonControls.PropertyTable tableGeneral;
		private System.Windows.Forms.GroupBox grpInfo;
		private System.Windows.Forms.GroupBox grpConditions;
		private System.Windows.Forms.GroupBox groupBox2;
		private SelectBox selAlsoPlayingAnyMarkers;
		private System.Windows.Forms.Label label4;
		private SelectBox selAlsoPlayingNotMarkers;
		private System.Windows.Forms.Label label5;
		private SelectBox selAlsoPlayingAllMarkers;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox1;
		private SelectBox selAnyMarkers;
		private System.Windows.Forms.Label label3;
		private SelectBox selNotMarkers;
		private System.Windows.Forms.Label label2;
		private SelectBox selAllMarkers;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}

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
			this.label2 = new System.Windows.Forms.Label();
			this.cboGender = new System.Windows.Forms.ComboBox();
			this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdPrevScreen = new System.Windows.Forms.Button();
			this.txtScreenImage = new System.Windows.Forms.TextBox();
			this.cmdNextScreen = new System.Windows.Forms.Button();
			this.cmdBrowseImage = new System.Windows.Forms.Button();
			this.gridText = new System.Windows.Forms.DataGridView();
			this.ColX = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColY = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColArrow = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmdDeleteEnding = new System.Windows.Forms.Button();
			this.groupScreen = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lblScreen = new System.Windows.Forms.Label();
			this.cmdRemoveScreen = new System.Windows.Forms.Button();
			this.cmdInsertScreen = new System.Windows.Forms.Button();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.cmdAddEnding = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.gridText)).BeginInit();
			this.groupScreen.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboEnding
			// 
			this.cboEnding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEnding.FormattingEnabled = true;
			this.cboEnding.Location = new System.Drawing.Point(65, 3);
			this.cboEnding.Name = "cboEnding";
			this.cboEnding.Size = new System.Drawing.Size(185, 21);
			this.cboEnding.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cboEnding, "Select an ending to edit");
			this.cboEnding.SelectedIndexChanged += new System.EventHandler(this.cboEnding_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Ending:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(333, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Available to gender:";
			// 
			// cboGender
			// 
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FormattingEnabled = true;
			this.cboGender.Items.AddRange(new object[] {
            "male",
            "female",
            "any"});
			this.cboGender.Location = new System.Drawing.Point(440, 2);
			this.cboGender.Name = "cboGender";
			this.cboGender.Size = new System.Drawing.Size(87, 21);
			this.cboGender.TabIndex = 12;
			this.toolTip1.SetToolTip(this.cboGender, "What gender the player must be for this ending to be available");
			// 
			// imageFileDialog
			// 
			this.imageFileDialog.FileName = "openFileDialog1";
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitle.Location = new System.Drawing.Point(569, 3);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(294, 20);
			this.txtTitle.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(533, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Title:";
			// 
			// cmdPrevScreen
			// 
			this.cmdPrevScreen.Location = new System.Drawing.Point(6, 20);
			this.cmdPrevScreen.Name = "cmdPrevScreen";
			this.cmdPrevScreen.Size = new System.Drawing.Size(64, 23);
			this.cmdPrevScreen.TabIndex = 15;
			this.cmdPrevScreen.Text = "Previous";
			this.toolTip1.SetToolTip(this.cmdPrevScreen, "Return to previous screen");
			this.cmdPrevScreen.UseVisualStyleBackColor = true;
			this.cmdPrevScreen.Click += new System.EventHandler(this.cmdPrevScreen_Click);
			// 
			// txtScreenImage
			// 
			this.txtScreenImage.Location = new System.Drawing.Point(76, 22);
			this.txtScreenImage.Name = "txtScreenImage";
			this.txtScreenImage.ReadOnly = true;
			this.txtScreenImage.Size = new System.Drawing.Size(142, 20);
			this.txtScreenImage.TabIndex = 16;
			this.txtScreenImage.Validated += new System.EventHandler(this.txtScreenImage_Validated);
			// 
			// cmdNextScreen
			// 
			this.cmdNextScreen.Location = new System.Drawing.Point(300, 20);
			this.cmdNextScreen.Name = "cmdNextScreen";
			this.cmdNextScreen.Size = new System.Drawing.Size(60, 23);
			this.cmdNextScreen.TabIndex = 17;
			this.cmdNextScreen.Text = "Next";
			this.toolTip1.SetToolTip(this.cmdNextScreen, "Advance to next screen");
			this.cmdNextScreen.UseVisualStyleBackColor = true;
			this.cmdNextScreen.Click += new System.EventHandler(this.cmdNextScreen_Click);
			// 
			// cmdBrowseImage
			// 
			this.cmdBrowseImage.Location = new System.Drawing.Point(220, 20);
			this.cmdBrowseImage.Name = "cmdBrowseImage";
			this.cmdBrowseImage.Size = new System.Drawing.Size(74, 23);
			this.cmdBrowseImage.TabIndex = 18;
			this.cmdBrowseImage.Text = "Set image...";
			this.toolTip1.SetToolTip(this.cmdBrowseImage, "Browse to select a background for this screen");
			this.cmdBrowseImage.UseVisualStyleBackColor = true;
			this.cmdBrowseImage.Click += new System.EventHandler(this.cmdBrowseImage_Click);
			// 
			// gridText
			// 
			this.gridText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridText.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridText.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColX,
            this.ColY,
            this.ColWidth,
            this.ColArrow,
            this.ColContent});
			this.gridText.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridText.Location = new System.Drawing.Point(6, 49);
			this.gridText.Name = "gridText";
			this.gridText.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridText.Size = new System.Drawing.Size(955, 140);
			this.gridText.TabIndex = 19;
			this.gridText.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridText_CellEndEdit);
			this.gridText.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridText_CellEnter);
			// 
			// ColX
			// 
			this.ColX.HeaderText = "X";
			this.ColX.Name = "ColX";
			this.ColX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColY
			// 
			this.ColY.HeaderText = "Y";
			this.ColY.Name = "ColY";
			this.ColY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColWidth
			// 
			this.ColWidth.HeaderText = "Width";
			this.ColWidth.Name = "ColWidth";
			this.ColWidth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColArrow
			// 
			this.ColArrow.HeaderText = "Arrow";
			this.ColArrow.Name = "ColArrow";
			// 
			// ColContent
			// 
			this.ColContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColContent.HeaderText = "Text";
			this.ColContent.Name = "ColContent";
			this.ColContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// cmdDeleteEnding
			// 
			this.cmdDeleteEnding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDeleteEnding.Location = new System.Drawing.Point(869, 1);
			this.cmdDeleteEnding.Name = "cmdDeleteEnding";
			this.cmdDeleteEnding.Size = new System.Drawing.Size(101, 23);
			this.cmdDeleteEnding.TabIndex = 20;
			this.cmdDeleteEnding.Text = "Delete Ending";
			this.toolTip1.SetToolTip(this.cmdDeleteEnding, "Delete this ending");
			this.cmdDeleteEnding.UseVisualStyleBackColor = true;
			this.cmdDeleteEnding.Click += new System.EventHandler(this.cmdDeleteEnding_Click);
			// 
			// groupScreen
			// 
			this.groupScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupScreen.Controls.Add(this.label4);
			this.groupScreen.Controls.Add(this.lblScreen);
			this.groupScreen.Controls.Add(this.cmdRemoveScreen);
			this.groupScreen.Controls.Add(this.cmdInsertScreen);
			this.groupScreen.Controls.Add(this.cmdPrevScreen);
			this.groupScreen.Controls.Add(this.gridText);
			this.groupScreen.Controls.Add(this.txtScreenImage);
			this.groupScreen.Controls.Add(this.cmdBrowseImage);
			this.groupScreen.Controls.Add(this.cmdNextScreen);
			this.groupScreen.Location = new System.Drawing.Point(3, 448);
			this.groupScreen.Name = "groupScreen";
			this.groupScreen.Size = new System.Drawing.Size(967, 195);
			this.groupScreen.TabIndex = 0;
			this.groupScreen.TabStop = false;
			this.groupScreen.Text = "Screens";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(570, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(193, 13);
			this.label4.TabIndex = 23;
			this.label4.Text = "Lines appear one at a time in sequence";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblScreen
			// 
			this.lblScreen.AutoSize = true;
			this.lblScreen.Location = new System.Drawing.Point(370, 25);
			this.lblScreen.Name = "lblScreen";
			this.lblScreen.Size = new System.Drawing.Size(0, 13);
			this.lblScreen.TabIndex = 22;
			// 
			// cmdRemoveScreen
			// 
			this.cmdRemoveScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRemoveScreen.Location = new System.Drawing.Point(866, 20);
			this.cmdRemoveScreen.Name = "cmdRemoveScreen";
			this.cmdRemoveScreen.Size = new System.Drawing.Size(95, 23);
			this.cmdRemoveScreen.TabIndex = 21;
			this.cmdRemoveScreen.Text = "Remove Screen";
			this.toolTip1.SetToolTip(this.cmdRemoveScreen, "Remove this screen");
			this.cmdRemoveScreen.UseVisualStyleBackColor = true;
			this.cmdRemoveScreen.Click += new System.EventHandler(this.cmdRemoveScreen_Click);
			// 
			// cmdInsertScreen
			// 
			this.cmdInsertScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdInsertScreen.Location = new System.Drawing.Point(769, 20);
			this.cmdInsertScreen.Name = "cmdInsertScreen";
			this.cmdInsertScreen.Size = new System.Drawing.Size(91, 23);
			this.cmdInsertScreen.TabIndex = 20;
			this.cmdInsertScreen.Text = "Insert Screen";
			this.toolTip1.SetToolTip(this.cmdInsertScreen, "Insert a new screen at this position");
			this.cmdInsertScreen.UseVisualStyleBackColor = true;
			this.cmdInsertScreen.Click += new System.EventHandler(this.cmdInsertScreen_Click);
			// 
			// wb
			// 
			this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wb.Location = new System.Drawing.Point(3, 30);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.ScrollBarsEnabled = false;
			this.wb.Size = new System.Drawing.Size(965, 411);
			this.wb.TabIndex = 0;
			this.wb.Url = new System.Uri("", System.UriKind.Relative);
			this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
			// 
			// cmdAddEnding
			// 
			this.cmdAddEnding.Location = new System.Drawing.Point(256, 1);
			this.cmdAddEnding.Name = "cmdAddEnding";
			this.cmdAddEnding.Size = new System.Drawing.Size(75, 23);
			this.cmdAddEnding.TabIndex = 21;
			this.cmdAddEnding.Text = "Add";
			this.toolTip1.SetToolTip(this.cmdAddEnding, "Add a new ending");
			this.cmdAddEnding.UseVisualStyleBackColor = true;
			this.cmdAddEnding.Click += new System.EventHandler(this.cmdAddEnding_Click);
			// 
			// EpilogueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdAddEnding);
			this.Controls.Add(this.wb);
			this.Controls.Add(this.groupScreen);
			this.Controls.Add(this.cmdDeleteEnding);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.cboGender);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboEnding);
			this.Name = "EpilogueEditor";
			this.Size = new System.Drawing.Size(973, 646);
			((System.ComponentModel.ISupportInitialize)(this.gridText)).EndInit();
			this.groupScreen.ResumeLayout(false);
			this.groupScreen.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox cboEnding;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboGender;
		private System.Windows.Forms.OpenFileDialog imageFileDialog;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdPrevScreen;
		private System.Windows.Forms.TextBox txtScreenImage;
		private System.Windows.Forms.Button cmdNextScreen;
		private System.Windows.Forms.Button cmdBrowseImage;
		private System.Windows.Forms.DataGridView gridText;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColX;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColY;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColWidth;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColArrow;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColContent;
		private System.Windows.Forms.Button cmdDeleteEnding;
		private System.Windows.Forms.GroupBox groupScreen;
		private System.Windows.Forms.WebBrowser wb;
		private System.Windows.Forms.Button cmdRemoveScreen;
		private System.Windows.Forms.Button cmdInsertScreen;
		private System.Windows.Forms.Label lblScreen;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button cmdAddEnding;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

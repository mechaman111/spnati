namespace SPNATI_Character_Editor.Activities
{
	partial class DeckEditor
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
			this.table = new Desktop.CommonControls.PropertyTable();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.grpFront = new Desktop.Skinning.SkinnedGroupBox();
			this.tabStrip = new Desktop.Skinning.SkinnedTabStrip();
			this.tabsFront = new Desktop.Skinning.SkinnedTabControl();
			this.pnlSuits = new Desktop.Skinning.SkinnedPanel();
			this.pnlClub = new Desktop.Skinning.SkinnedPanel();
			this.pnlDiamond = new Desktop.Skinning.SkinnedPanel();
			this.pnlHeart = new Desktop.Skinning.SkinnedPanel();
			this.pnlSpade = new Desktop.Skinning.SkinnedPanel();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.recFront = new Desktop.CommonControls.RecordField();
			this.skinnedGroupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.cmdRemoveBack = new Desktop.Skinning.SkinnedIcon();
			this.cmdAddBack = new Desktop.Skinning.SkinnedIcon();
			this.lstBacks = new Desktop.Skinning.SkinnedListBox();
			this.picBack = new System.Windows.Forms.PictureBox();
			this.cmdBrowseBack = new Desktop.Skinning.SkinnedButton();
			this.txtBack = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.skinnedGroupBox1.SuspendLayout();
			this.grpFront.SuspendLayout();
			this.pnlSuits.SuspendLayout();
			this.skinnedGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
			this.SuspendLayout();
			// 
			// table
			// 
			this.table.AllowDelete = false;
			this.table.AllowFavorites = false;
			this.table.AllowHelp = true;
			this.table.AllowMacros = false;
			this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.table.BackColor = System.Drawing.Color.White;
			this.table.Data = null;
			this.table.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.table.HideAddField = true;
			this.table.HideSpeedButtons = true;
			this.table.Location = new System.Drawing.Point(6, 26);
			this.table.ModifyingProperty = null;
			this.table.Name = "table";
			this.table.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.table.PlaceholderText = null;
			this.table.PreserveControls = false;
			this.table.PreviewData = null;
			this.table.RemoveCaption = "Remove";
			this.table.RowHeaderWidth = 120F;
			this.table.RunInitialAddEvents = false;
			this.table.Size = new System.Drawing.Size(437, 265);
			this.table.Sorted = false;
			this.table.TabIndex = 0;
			this.table.UndoManager = null;
			this.table.UseAutoComplete = false;
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox1.Controls.Add(this.table);
			this.skinnedGroupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox1.Image = null;
			this.skinnedGroupBox1.Location = new System.Drawing.Point(3, 3);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox1.ShowIndicatorBar = false;
			this.skinnedGroupBox1.Size = new System.Drawing.Size(449, 297);
			this.skinnedGroupBox1.TabIndex = 1;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Deck Information";
			// 
			// grpFront
			// 
			this.grpFront.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpFront.BackColor = System.Drawing.Color.White;
			this.grpFront.Controls.Add(this.tabStrip);
			this.grpFront.Controls.Add(this.pnlSuits);
			this.grpFront.Controls.Add(this.skinnedLabel1);
			this.grpFront.Controls.Add(this.recFront);
			this.grpFront.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpFront.Image = null;
			this.grpFront.Location = new System.Drawing.Point(458, 3);
			this.grpFront.Name = "grpFront";
			this.grpFront.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpFront.ShowIndicatorBar = false;
			this.grpFront.Size = new System.Drawing.Size(462, 516);
			this.grpFront.TabIndex = 2;
			this.grpFront.TabStop = false;
			this.grpFront.Text = "Card Front";
			// 
			// tabStrip
			// 
			this.tabStrip.AddCaption = null;
			this.tabStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabStrip.DecorationText = null;
			this.tabStrip.Location = new System.Drawing.Point(9, 21);
			this.tabStrip.Margin = new System.Windows.Forms.Padding(0);
			this.tabStrip.Name = "tabStrip";
			this.tabStrip.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tabStrip.ShowAddButton = true;
			this.tabStrip.ShowCloseButton = true;
			this.tabStrip.Size = new System.Drawing.Size(447, 28);
			this.tabStrip.StartMargin = 5;
			this.tabStrip.TabControl = this.tabsFront;
			this.tabStrip.TabIndex = 5;
			this.tabStrip.TabMargin = 5;
			this.tabStrip.TabPadding = 20;
			this.tabStrip.TabSize = 100;
			this.tabStrip.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tabStrip.Text = "skinnedTabStrip1";
			this.tabStrip.Vertical = false;
			this.tabStrip.CloseButtonClicked += new System.EventHandler(this.tabStrip_CloseButtonClicked);
			this.tabStrip.AddButtonClicked += new System.EventHandler(this.tabStrip_AddButtonClicked);
			// 
			// tabsFront
			// 
			this.tabsFront.Location = new System.Drawing.Point(490, 293);
			this.tabsFront.Name = "tabsFront";
			this.tabsFront.SelectedIndex = 0;
			this.tabsFront.Size = new System.Drawing.Size(200, 100);
			this.tabsFront.TabIndex = 7;
			this.tabsFront.SelectedIndexChanged += new System.EventHandler(this.tabsFront_SelectedIndexChanged);
			// 
			// pnlSuits
			// 
			this.pnlSuits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSuits.AutoScroll = true;
			this.pnlSuits.Controls.Add(this.pnlClub);
			this.pnlSuits.Controls.Add(this.pnlDiamond);
			this.pnlSuits.Controls.Add(this.pnlHeart);
			this.pnlSuits.Controls.Add(this.pnlSpade);
			this.pnlSuits.Location = new System.Drawing.Point(9, 80);
			this.pnlSuits.Name = "pnlSuits";
			this.pnlSuits.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlSuits.Size = new System.Drawing.Size(447, 430);
			this.pnlSuits.TabIndex = 4;
			this.pnlSuits.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlSuits.Tag = "";
			// 
			// pnlClub
			// 
			this.pnlClub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlClub.AutoScroll = true;
			this.pnlClub.Location = new System.Drawing.Point(0, 558);
			this.pnlClub.Name = "pnlClub";
			this.pnlClub.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlClub.Size = new System.Drawing.Size(430, 180);
			this.pnlClub.TabIndex = 4;
			this.pnlClub.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlClub.Tag = "clubs";
			// 
			// pnlDiamond
			// 
			this.pnlDiamond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDiamond.AutoScroll = true;
			this.pnlDiamond.Location = new System.Drawing.Point(0, 372);
			this.pnlDiamond.Name = "pnlDiamond";
			this.pnlDiamond.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlDiamond.Size = new System.Drawing.Size(430, 180);
			this.pnlDiamond.TabIndex = 4;
			this.pnlDiamond.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlDiamond.Tag = "diamo";
			// 
			// pnlHeart
			// 
			this.pnlHeart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlHeart.AutoScroll = true;
			this.pnlHeart.Location = new System.Drawing.Point(0, 0);
			this.pnlHeart.Name = "pnlHeart";
			this.pnlHeart.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlHeart.Size = new System.Drawing.Size(430, 180);
			this.pnlHeart.TabIndex = 2;
			this.pnlHeart.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlHeart.Tag = "heart";
			// 
			// pnlSpade
			// 
			this.pnlSpade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlSpade.AutoScroll = true;
			this.pnlSpade.Location = new System.Drawing.Point(0, 186);
			this.pnlSpade.Name = "pnlSpade";
			this.pnlSpade.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.pnlSpade.Size = new System.Drawing.Size(430, 180);
			this.pnlSpade.TabIndex = 3;
			this.pnlSpade.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlSpade.Tag = "spade";
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(6, 58);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(39, 13);
			this.skinnedLabel1.TabIndex = 1;
			this.skinnedLabel1.Text = "Folder:";
			// 
			// recFront
			// 
			this.recFront.AllowCreate = false;
			this.recFront.Location = new System.Drawing.Point(51, 54);
			this.recFront.Name = "recFront";
			this.recFront.PlaceholderText = null;
			this.recFront.Record = null;
			this.recFront.RecordContext = null;
			this.recFront.RecordFilter = null;
			this.recFront.RecordKey = null;
			this.recFront.RecordType = null;
			this.recFront.Size = new System.Drawing.Size(150, 20);
			this.recFront.TabIndex = 0;
			this.recFront.UseAutoComplete = false;
			this.recFront.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recFront_RecordChanged);
			// 
			// skinnedGroupBox2
			// 
			this.skinnedGroupBox2.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox2.Controls.Add(this.cmdRemoveBack);
			this.skinnedGroupBox2.Controls.Add(this.cmdAddBack);
			this.skinnedGroupBox2.Controls.Add(this.lstBacks);
			this.skinnedGroupBox2.Controls.Add(this.picBack);
			this.skinnedGroupBox2.Controls.Add(this.cmdBrowseBack);
			this.skinnedGroupBox2.Controls.Add(this.txtBack);
			this.skinnedGroupBox2.Controls.Add(this.skinnedLabel2);
			this.skinnedGroupBox2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox2.Image = null;
			this.skinnedGroupBox2.Location = new System.Drawing.Point(3, 306);
			this.skinnedGroupBox2.Name = "skinnedGroupBox2";
			this.skinnedGroupBox2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox2.ShowIndicatorBar = false;
			this.skinnedGroupBox2.Size = new System.Drawing.Size(449, 207);
			this.skinnedGroupBox2.TabIndex = 3;
			this.skinnedGroupBox2.TabStop = false;
			this.skinnedGroupBox2.Text = "Card Back";
			// 
			// cmdRemoveBack
			// 
			this.cmdRemoveBack.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdRemoveBack.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdRemoveBack.Flat = false;
			this.cmdRemoveBack.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.cmdRemoveBack.Location = new System.Drawing.Point(28, 26);
			this.cmdRemoveBack.Name = "cmdRemoveBack";
			this.cmdRemoveBack.Size = new System.Drawing.Size(16, 16);
			this.cmdRemoveBack.TabIndex = 6;
			this.cmdRemoveBack.Text = "Add";
			this.cmdRemoveBack.UseVisualStyleBackColor = true;
			this.cmdRemoveBack.Click += new System.EventHandler(this.cmdRemoveBack_Click);
			// 
			// cmdAddBack
			// 
			this.cmdAddBack.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAddBack.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdAddBack.Flat = false;
			this.cmdAddBack.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.cmdAddBack.Location = new System.Drawing.Point(6, 26);
			this.cmdAddBack.Name = "cmdAddBack";
			this.cmdAddBack.Size = new System.Drawing.Size(16, 16);
			this.cmdAddBack.TabIndex = 5;
			this.cmdAddBack.Text = "Add";
			this.cmdAddBack.UseVisualStyleBackColor = true;
			this.cmdAddBack.Click += new System.EventHandler(this.cmdAddBack_Click);
			// 
			// lstBacks
			// 
			this.lstBacks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstBacks.BackColor = System.Drawing.Color.White;
			this.lstBacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstBacks.ForeColor = System.Drawing.Color.Black;
			this.lstBacks.FormattingEnabled = true;
			this.lstBacks.IntegralHeight = false;
			this.lstBacks.Location = new System.Drawing.Point(6, 44);
			this.lstBacks.Name = "lstBacks";
			this.lstBacks.Size = new System.Drawing.Size(160, 154);
			this.lstBacks.TabIndex = 4;
			this.lstBacks.SelectedIndexChanged += new System.EventHandler(this.lstBacks_SelectedIndexChanged);
			// 
			// picBack
			// 
			this.picBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picBack.Location = new System.Drawing.Point(283, 48);
			this.picBack.Name = "picBack";
			this.picBack.Size = new System.Drawing.Size(113, 150);
			this.picBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picBack.TabIndex = 3;
			this.picBack.TabStop = false;
			// 
			// cmdBrowseBack
			// 
			this.cmdBrowseBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseBack.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowseBack.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBrowseBack.Flat = false;
			this.cmdBrowseBack.Location = new System.Drawing.Point(402, 20);
			this.cmdBrowseBack.Name = "cmdBrowseBack";
			this.cmdBrowseBack.Size = new System.Drawing.Size(38, 23);
			this.cmdBrowseBack.TabIndex = 2;
			this.cmdBrowseBack.Text = "...";
			this.cmdBrowseBack.UseVisualStyleBackColor = true;
			this.cmdBrowseBack.Click += new System.EventHandler(this.cmdBrowseBack_Click);
			// 
			// txtBack
			// 
			this.txtBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBack.BackColor = System.Drawing.Color.White;
			this.txtBack.Enabled = false;
			this.txtBack.ForeColor = System.Drawing.Color.Black;
			this.txtBack.Location = new System.Drawing.Point(246, 22);
			this.txtBack.Name = "txtBack";
			this.txtBack.Size = new System.Drawing.Size(150, 20);
			this.txtBack.TabIndex = 1;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(214, 24);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(26, 13);
			this.skinnedLabel2.TabIndex = 0;
			this.skinnedLabel2.Text = "File:";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.SVG)|*.BMP;*.JPG;*.GIF;*.PNG;*.SVG|All file" +
    "s (*.*)|*.*";
			// 
			// DeckEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedGroupBox2);
			this.Controls.Add(this.grpFront);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Controls.Add(this.tabsFront);
			this.Name = "DeckEditor";
			this.Size = new System.Drawing.Size(923, 522);
			this.skinnedGroupBox1.ResumeLayout(false);
			this.grpFront.ResumeLayout(false);
			this.grpFront.PerformLayout();
			this.pnlSuits.ResumeLayout(false);
			this.skinnedGroupBox2.ResumeLayout(false);
			this.skinnedGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.PropertyTable table;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedGroupBox grpFront;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.CommonControls.RecordField recFront;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox2;
		private Desktop.Skinning.SkinnedButton cmdBrowseBack;
		private Desktop.Skinning.SkinnedTextBox txtBack;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.PictureBox picBack;
		private Desktop.Skinning.SkinnedPanel pnlHeart;
		private Desktop.Skinning.SkinnedPanel pnlSpade;
		private Desktop.Skinning.SkinnedPanel pnlSuits;
		private Desktop.Skinning.SkinnedPanel pnlDiamond;
		private Desktop.Skinning.SkinnedPanel pnlClub;
		private Desktop.Skinning.SkinnedIcon cmdRemoveBack;
		private Desktop.Skinning.SkinnedIcon cmdAddBack;
		private Desktop.Skinning.SkinnedListBox lstBacks;
		private Desktop.Skinning.SkinnedTabStrip tabStrip;
		private Desktop.Skinning.SkinnedTabControl tabsFront;
	}
}

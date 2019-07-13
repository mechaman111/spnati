namespace SPNATI_Character_Editor.Controls
{
	partial class StyleControl
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.lstStyles = new Desktop.Skinning.SkinnedListBox();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.txtSample = new Desktop.Skinning.SkinnedTextBox();
			this.cmdAddAttribute = new Desktop.Skinning.SkinnedButton();
			this.tableAttributes = new Desktop.CommonControls.PropertyTable();
			this.lblUsage = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.wbPreview = new System.Windows.Forms.WebBrowser();
			this.txtAdvanced = new Desktop.Skinning.SkinnedTextBox();
			this.tmrCreate = new System.Windows.Forms.Timer(this.components);
			this.txtDescription = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
			this.splitContainer1.Panel1.Controls.Add(this.lstStyles);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(482, 477);
			this.splitContainer1.SplitterDistance = 116;
			this.splitContainer1.TabIndex = 1;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsRemove});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(116, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Tag = "Surface";
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsAdd
			// 
			this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(23, 22);
			this.tsAdd.Text = "Add Style";
			this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
			// 
			// tsRemove
			// 
			this.tsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(23, 22);
			this.tsRemove.Text = "Remove Style";
			this.tsRemove.Click += new System.EventHandler(this.tsRemove_Click);
			// 
			// lstStyles
			// 
			this.lstStyles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstStyles.BackColor = System.Drawing.Color.White;
			this.lstStyles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstStyles.ForeColor = System.Drawing.Color.Black;
			this.lstStyles.FormattingEnabled = true;
			this.lstStyles.Location = new System.Drawing.Point(3, 27);
			this.lstStyles.Name = "lstStyles";
			this.lstStyles.Size = new System.Drawing.Size(113, 433);
			this.lstStyles.TabIndex = 0;
			this.lstStyles.SelectedIndexChanged += new System.EventHandler(this.lstStyles_SelectedIndexChanged);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.txtDescription);
			this.splitContainer2.Panel1.Controls.Add(this.skinnedLabel2);
			this.splitContainer2.Panel1.Controls.Add(this.txtSample);
			this.splitContainer2.Panel1.Controls.Add(this.cmdAddAttribute);
			this.splitContainer2.Panel1.Controls.Add(this.tableAttributes);
			this.splitContainer2.Panel1.Controls.Add(this.lblUsage);
			this.splitContainer2.Panel1.Controls.Add(this.txtName);
			this.splitContainer2.Panel1.Controls.Add(this.skinnedLabel1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.skinnedPanel1);
			this.splitContainer2.Size = new System.Drawing.Size(362, 477);
			this.splitContainer2.SplitterDistance = 342;
			this.splitContainer2.TabIndex = 0;
			// 
			// txtSample
			// 
			this.txtSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSample.BackColor = System.Drawing.Color.White;
			this.txtSample.ForeColor = System.Drawing.Color.Black;
			this.txtSample.Location = new System.Drawing.Point(92, 318);
			this.txtSample.Name = "txtSample";
			this.txtSample.ReadOnly = true;
			this.txtSample.Size = new System.Drawing.Size(267, 20);
			this.txtSample.TabIndex = 5;
			// 
			// cmdAddAttribute
			// 
			this.cmdAddAttribute.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAddAttribute.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdAddAttribute.Flat = true;
			this.cmdAddAttribute.ForeColor = System.Drawing.Color.Blue;
			this.cmdAddAttribute.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.cmdAddAttribute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdAddAttribute.Location = new System.Drawing.Point(3, 56);
			this.cmdAddAttribute.Name = "cmdAddAttribute";
			this.cmdAddAttribute.Size = new System.Drawing.Size(56, 23);
			this.cmdAddAttribute.TabIndex = 3;
			this.cmdAddAttribute.Text = "Add";
			this.cmdAddAttribute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdAddAttribute.UseVisualStyleBackColor = true;
			this.cmdAddAttribute.Click += new System.EventHandler(this.cmdAddAttribute_Click);
			// 
			// tableAttributes
			// 
			this.tableAttributes.AllowDelete = true;
			this.tableAttributes.AllowFavorites = false;
			this.tableAttributes.AllowHelp = false;
			this.tableAttributes.AllowMacros = false;
			this.tableAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableAttributes.BackColor = System.Drawing.Color.White;
			this.tableAttributes.Data = null;
			this.tableAttributes.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableAttributes.HideAddField = true;
			this.tableAttributes.HideSpeedButtons = false;
			this.tableAttributes.Location = new System.Drawing.Point(3, 56);
			this.tableAttributes.ModifyingProperty = null;
			this.tableAttributes.Name = "tableAttributes";
			this.tableAttributes.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.tableAttributes.PlaceholderText = null;
			this.tableAttributes.PreserveControls = false;
			this.tableAttributes.PreviewData = null;
			this.tableAttributes.RemoveCaption = "Remove";
			this.tableAttributes.RowHeaderWidth = 70F;
			this.tableAttributes.RunInitialAddEvents = false;
			this.tableAttributes.Size = new System.Drawing.Size(359, 256);
			this.tableAttributes.Sorted = false;
			this.tableAttributes.TabIndex = 4;
			this.tableAttributes.UndoManager = null;
			this.tableAttributes.UseAutoComplete = false;
			// 
			// lblUsage
			// 
			this.lblUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblUsage.AutoSize = true;
			this.lblUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblUsage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblUsage.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblUsage.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.lblUsage.Location = new System.Drawing.Point(2, 321);
			this.lblUsage.Name = "lblUsage";
			this.lblUsage.Size = new System.Drawing.Size(84, 13);
			this.lblUsage.TabIndex = 2;
			this.lblUsage.Text = "Dialogue usage:";
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(71, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(138, 20);
			this.txtName.TabIndex = 1;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(3, 6);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(62, 13);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Style name:";
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.wbPreview);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 0);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedPanel1.Size = new System.Drawing.Size(362, 131);
			this.skinnedPanel1.TabIndex = 1;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// wbPreview
			// 
			this.wbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wbPreview.Location = new System.Drawing.Point(3, 3);
			this.wbPreview.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbPreview.Name = "wbPreview";
			this.wbPreview.ScrollBarsEnabled = false;
			this.wbPreview.Size = new System.Drawing.Size(356, 125);
			this.wbPreview.TabIndex = 0;
			this.wbPreview.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbPreview_DocumentCompleted);
			// 
			// txtAdvanced
			// 
			this.txtAdvanced.AcceptsReturn = true;
			this.txtAdvanced.AcceptsTab = true;
			this.txtAdvanced.BackColor = System.Drawing.Color.White;
			this.txtAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAdvanced.Font = new System.Drawing.Font("Courier New", 10F);
			this.txtAdvanced.ForeColor = System.Drawing.Color.Black;
			this.txtAdvanced.Location = new System.Drawing.Point(0, 0);
			this.txtAdvanced.Multiline = true;
			this.txtAdvanced.Name = "txtAdvanced";
			this.txtAdvanced.Size = new System.Drawing.Size(482, 477);
			this.txtAdvanced.TabIndex = 2;
			this.txtAdvanced.TabStop = false;
			// 
			// tmrCreate
			// 
			this.tmrCreate.Tick += new System.EventHandler(this.tmrCreate_Tick);
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.BackColor = System.Drawing.Color.White;
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtDescription.ForeColor = System.Drawing.Color.Black;
			this.txtDescription.Location = new System.Drawing.Point(71, 29);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(288, 20);
			this.txtDescription.TabIndex = 2;
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(3, 32);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(63, 13);
			this.skinnedLabel2.TabIndex = 6;
			this.skinnedLabel2.Text = "Description:";
			// 
			// StyleControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.txtAdvanced);
			this.Name = "StyleControl";
			this.Size = new System.Drawing.Size(482, 477);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedListBox lstStyles;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.WebBrowser wbPreview;
		private Desktop.Skinning.SkinnedTextBox txtName;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel lblUsage;
		private Desktop.Skinning.SkinnedTextBox txtAdvanced;
		private Desktop.CommonControls.PropertyTable tableAttributes;
		private Desktop.Skinning.SkinnedButton cmdAddAttribute;
		private Desktop.Skinning.SkinnedTextBox txtSample;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private System.Windows.Forms.Timer tmrCreate;
		private Desktop.Skinning.SkinnedTextBox txtDescription;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
	}
}

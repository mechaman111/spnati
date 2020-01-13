namespace SPNATI_Character_Editor.Activities
{
	partial class CharacterPreview
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
			this.lblLinesOfDialogue = new Desktop.Skinning.SkinnedLabel();
			this.lblSkin = new Desktop.Skinning.SkinnedLabel();
			this.cboSkin = new Desktop.Skinning.SkinnedComboBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.cmdReference = new Desktop.Skinning.SkinnedButton();
			this.picPortrait = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.tabsReference = new Desktop.Skinning.SkinnedTabControl();
			this.tabTags = new System.Windows.Forms.TabPage();
			this.tabTargets = new System.Windows.Forms.TabPage();
			this.stripReference = new Desktop.Skinning.SkinnedTabStrip();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabsReference.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblLinesOfDialogue
			// 
			this.lblLinesOfDialogue.AutoSize = true;
			this.lblLinesOfDialogue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblLinesOfDialogue.ForeColor = System.Drawing.Color.Black;
			this.lblLinesOfDialogue.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblLinesOfDialogue.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblLinesOfDialogue.Location = new System.Drawing.Point(0, 1);
			this.lblLinesOfDialogue.Name = "lblLinesOfDialogue";
			this.lblLinesOfDialogue.Size = new System.Drawing.Size(13, 13);
			this.lblLinesOfDialogue.TabIndex = 17;
			this.lblLinesOfDialogue.Text = "0";
			// 
			// lblSkin
			// 
			this.lblSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSkin.AutoSize = true;
			this.lblSkin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblSkin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblSkin.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblSkin.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.lblSkin.Location = new System.Drawing.Point(115, 623);
			this.lblSkin.Name = "lblSkin";
			this.lblSkin.Size = new System.Drawing.Size(31, 13);
			this.lblSkin.TabIndex = 18;
			this.lblSkin.Text = "Skin:";
			// 
			// cboSkin
			// 
			this.cboSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cboSkin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboSkin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboSkin.BackColor = System.Drawing.Color.White;
			this.cboSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSkin.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cboSkin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboSkin.FormattingEnabled = true;
			this.cboSkin.KeyMember = null;
			this.cboSkin.Location = new System.Drawing.Point(145, 619);
			this.cboSkin.Name = "cboSkin";
			this.cboSkin.SelectedIndex = -1;
			this.cboSkin.SelectedItem = null;
			this.cboSkin.Size = new System.Drawing.Size(105, 21);
			this.cboSkin.Sorted = false;
			this.cboSkin.TabIndex = 19;
			this.cboSkin.SelectedIndexChanged += new System.EventHandler(this.cboSkin_SelectedIndexChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.cboSkin);
			this.splitContainer1.Panel1.Controls.Add(this.cmdReference);
			this.splitContainer1.Panel1.Controls.Add(this.lblLinesOfDialogue);
			this.splitContainer1.Panel1.Controls.Add(this.lblSkin);
			this.splitContainer1.Panel1.Controls.Add(this.picPortrait);
			this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabsReference);
			this.splitContainer1.Panel2.Controls.Add(this.stripReference);
			this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Size = new System.Drawing.Size(251, 641);
			this.splitContainer1.SplitterDistance = 448;
			this.splitContainer1.TabIndex = 22;
			// 
			// cmdReference
			// 
			this.cmdReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdReference.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdReference.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdReference.Flat = false;
			this.cmdReference.Image = global::SPNATI_Character_Editor.Properties.Resources.ChevronUp;
			this.cmdReference.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdReference.Location = new System.Drawing.Point(2, 619);
			this.cmdReference.Name = "cmdReference";
			this.cmdReference.Size = new System.Drawing.Size(103, 21);
			this.cmdReference.TabIndex = 21;
			this.cmdReference.Text = "Reference";
			this.cmdReference.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdReference.UseVisualStyleBackColor = true;
			this.cmdReference.Click += new System.EventHandler(this.cmdReference_Click);
			// 
			// picPortrait
			// 
			this.picPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picPortrait.Location = new System.Drawing.Point(0, 0);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(251, 640);
			this.picPortrait.TabIndex = 15;
			this.picPortrait.TabStop = false;
			// 
			// tabsReference
			// 
			this.tabsReference.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabsReference.Controls.Add(this.tabTags);
			this.tabsReference.Controls.Add(this.tabTargets);
			this.tabsReference.Location = new System.Drawing.Point(0, 23);
			this.tabsReference.Margin = new System.Windows.Forms.Padding(0);
			this.tabsReference.Name = "tabsReference";
			this.tabsReference.SelectedIndex = 0;
			this.tabsReference.Size = new System.Drawing.Size(251, 166);
			this.tabsReference.TabIndex = 0;
			// 
			// tabTags
			// 
			this.tabTags.BackColor = System.Drawing.Color.White;
			this.tabTags.Location = new System.Drawing.Point(4, 22);
			this.tabTags.Name = "tabTags";
			this.tabTags.Padding = new System.Windows.Forms.Padding(3);
			this.tabTags.Size = new System.Drawing.Size(243, 140);
			this.tabTags.TabIndex = 0;
			this.tabTags.Text = "Tags";
			// 
			// tabTargets
			// 
			this.tabTargets.BackColor = System.Drawing.Color.White;
			this.tabTargets.ForeColor = System.Drawing.Color.Black;
			this.tabTargets.Location = new System.Drawing.Point(4, 22);
			this.tabTargets.Name = "tabTargets";
			this.tabTargets.Size = new System.Drawing.Size(243, 140);
			this.tabTargets.TabIndex = 1;
			this.tabTargets.Text = "Targets";
			// 
			// stripReference
			// 
			this.stripReference.AddCaption = null;
			this.stripReference.DecorationText = null;
			this.stripReference.Dock = System.Windows.Forms.DockStyle.Top;
			this.stripReference.Location = new System.Drawing.Point(0, 0);
			this.stripReference.Margin = new System.Windows.Forms.Padding(0);
			this.stripReference.Name = "stripReference";
			this.stripReference.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripReference.ShowAddButton = false;
			this.stripReference.ShowCloseButton = false;
			this.stripReference.Size = new System.Drawing.Size(150, 23);
			this.stripReference.StartMargin = 5;
			this.stripReference.TabControl = this.tabsReference;
			this.stripReference.TabIndex = 1;
			this.stripReference.TabMargin = 5;
			this.stripReference.TabPadding = 20;
			this.stripReference.TabSize = -1;
			this.stripReference.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripReference.Vertical = false;
			// 
			// CharacterPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "CharacterPreview";
			this.Size = new System.Drawing.Size(251, 641);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabsReference.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblLinesOfDialogue;
		private SPNATI_Character_Editor.Controls.CharacterImageBox picPortrait;
		private Desktop.Skinning.SkinnedLabel lblSkin;
		private Desktop.Skinning.SkinnedComboBox cboSkin;
		private Desktop.Skinning.SkinnedButton cmdReference;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Desktop.Skinning.SkinnedTabControl tabsReference;
		private System.Windows.Forms.TabPage tabTags;
		private Desktop.Skinning.SkinnedTabStrip stripReference;
		private System.Windows.Forms.TabPage tabTargets;
	}
}

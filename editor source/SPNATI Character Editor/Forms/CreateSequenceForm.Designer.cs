namespace SPNATI_Character_Editor.Forms
{
	partial class CreateSequenceForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.radConvert = new Desktop.Skinning.SkinnedRadioButton();
			this.radCreate = new Desktop.Skinning.SkinnedRadioButton();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.lstFrames = new Desktop.Skinning.SkinnedListBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsRemove = new System.Windows.Forms.ToolStripButton();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.valTime = new Desktop.Skinning.SkinnedNumericUpDown();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.openDialog = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valTime)).BeginInit();
			this.SuspendLayout();
			// 
			// picPreview
			// 
			this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picPreview.Location = new System.Drawing.Point(12, 32);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(96, 248);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPreview.TabIndex = 0;
			this.picPreview.TabStop = false;
			// 
			// radConvert
			// 
			this.radConvert.AutoSize = true;
			this.radConvert.Location = new System.Drawing.Point(114, 32);
			this.radConvert.Name = "radConvert";
			this.radConvert.Size = new System.Drawing.Size(165, 17);
			this.radConvert.TabIndex = 1;
			this.radConvert.TabStop = true;
			this.radConvert.Text = "Convert Sprite Into Sequence";
			this.radConvert.UseVisualStyleBackColor = true;
			this.radConvert.CheckedChanged += new System.EventHandler(this.radConvert_CheckedChanged);
			// 
			// radCreate
			// 
			this.radCreate.AutoSize = true;
			this.radCreate.Checked = true;
			this.radCreate.Location = new System.Drawing.Point(114, 55);
			this.radCreate.Name = "radCreate";
			this.radCreate.Size = new System.Drawing.Size(133, 17);
			this.radCreate.TabIndex = 2;
			this.radCreate.TabStop = true;
			this.radCreate.Text = "Create New Sequence";
			this.radCreate.UseVisualStyleBackColor = true;
			this.radCreate.CheckedChanged += new System.EventHandler(this.radCreate_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(114, 77);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Images:";
			// 
			// lstFrames
			// 
			this.lstFrames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstFrames.BackColor = System.Drawing.Color.White;
			this.lstFrames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstFrames.ForeColor = System.Drawing.Color.Black;
			this.lstFrames.FormattingEnabled = true;
			this.lstFrames.Location = new System.Drawing.Point(116, 120);
			this.lstFrames.Name = "lstFrames";
			this.lstFrames.Size = new System.Drawing.Size(139, 160);
			this.lstFrames.TabIndex = 4;
			this.lstFrames.SelectedIndexChanged += new System.EventHandler(this.lstFrames_SelectedIndexChanged);
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsRemove});
			this.toolStrip1.Location = new System.Drawing.Point(116, 95);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(139, 25);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Tag = "Background";
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsAdd
			// 
			this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(23, 22);
			this.tsAdd.Text = "Add frame";
			this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
			// 
			// tsRemove
			// 
			this.tsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(23, 22);
			this.tsRemove.Text = "Remove frame";
			this.tsRemove.Click += new System.EventHandler(this.tsRemove_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(261, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(125, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Time between frames (s):";
			// 
			// valTime
			// 
			this.valTime.BackColor = System.Drawing.Color.White;
			this.valTime.DecimalPlaces = 2;
			this.valTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valTime.ForeColor = System.Drawing.Color.Black;
			this.valTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.valTime.Location = new System.Drawing.Point(264, 132);
			this.valTime.Name = "valTime";
			this.valTime.Size = new System.Drawing.Size(57, 20);
			this.valTime.TabIndex = 7;
			this.valTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(309, 228);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 8;
			this.cmdOK.Text = "Create";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.Blue;
			this.cmdCancel.Location = new System.Drawing.Point(309, 257);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 9;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(261, 77);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(264, 93);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(117, 20);
			this.txtName.TabIndex = 11;
			// 
			// openDialog
			// 
			this.openDialog.Filter = "";
			this.openDialog.IncludeOpponents = false;
			this.openDialog.UseAbsolutePaths = true;
			// 
			// CreateSequenceForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(396, 292);
			this.ControlBox = false;
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.valTime);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.lstFrames);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.radCreate);
			this.Controls.Add(this.radConvert);
			this.Controls.Add(this.picPreview);
			this.Name = "CreateSequenceForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create Sequence";
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valTime)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picPreview;
		private Desktop.Skinning.SkinnedRadioButton radConvert;
		private Desktop.Skinning.SkinnedRadioButton radCreate;
		private Desktop.Skinning.SkinnedLabel label1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedNumericUpDown valTime;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedListBox lstFrames;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsRemove;
		private Controls.CharacterImageDialog openDialog;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedTextBox txtName;
	}
}
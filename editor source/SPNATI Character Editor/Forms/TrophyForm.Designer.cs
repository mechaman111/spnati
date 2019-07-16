namespace SPNATI_Character_Editor.Forms
{
	partial class TrophyForm
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.groupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.valSet = new Desktop.Skinning.SkinnedNumericUpDown();
			this.radSet = new Desktop.Skinning.SkinnedRadioButton();
			this.valIncrement = new Desktop.Skinning.SkinnedNumericUpDown();
			this.radIncrement = new Desktop.Skinning.SkinnedRadioButton();
			this.valDecrement = new Desktop.Skinning.SkinnedNumericUpDown();
			this.radDecrement = new Desktop.Skinning.SkinnedRadioButton();
			this.radUnlock = new Desktop.Skinning.SkinnedRadioButton();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.lblText = new Desktop.Skinning.SkinnedLabel();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.recId = new Desktop.CommonControls.RecordField();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel2 = new Desktop.Skinning.SkinnedPanel();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valIncrement)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valDecrement)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.skinnedPanel1.SuspendLayout();
			this.skinnedPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Primary;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Collectible:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.valSet);
			this.groupBox1.Controls.Add(this.radSet);
			this.groupBox1.Controls.Add(this.valIncrement);
			this.groupBox1.Controls.Add(this.radIncrement);
			this.groupBox1.Controls.Add(this.valDecrement);
			this.groupBox1.Controls.Add(this.radDecrement);
			this.groupBox1.Controls.Add(this.radUnlock);
			this.groupBox1.Location = new System.Drawing.Point(13, 65);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(211, 128);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Effect";
			// 
			// valSet
			// 
			this.valSet.BackColor = System.Drawing.Color.White;
			this.valSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valSet.ForeColor = System.Drawing.Color.Black;
			this.valSet.Location = new System.Drawing.Point(91, 93);
			this.valSet.Name = "valSet";
			this.valSet.Size = new System.Drawing.Size(53, 20);
			this.valSet.TabIndex = 6;
			// 
			// radSet
			// 
			this.radSet.AutoSize = true;
			this.radSet.Location = new System.Drawing.Point(6, 95);
			this.radSet.Name = "radSet";
			this.radSet.Size = new System.Drawing.Size(44, 17);
			this.radSet.TabIndex = 5;
			this.radSet.TabStop = true;
			this.radSet.Text = "Set:";
			this.radSet.UseVisualStyleBackColor = true;
			this.radSet.CheckedChanged += new System.EventHandler(this.radSet_CheckedChanged);
			// 
			// valIncrement
			// 
			this.valIncrement.BackColor = System.Drawing.Color.White;
			this.valIncrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valIncrement.ForeColor = System.Drawing.Color.Black;
			this.valIncrement.Location = new System.Drawing.Point(91, 70);
			this.valIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valIncrement.Name = "valIncrement";
			this.valIncrement.Size = new System.Drawing.Size(53, 20);
			this.valIncrement.TabIndex = 4;
			this.valIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// radIncrement
			// 
			this.radIncrement.AutoSize = true;
			this.radIncrement.Location = new System.Drawing.Point(6, 72);
			this.radIncrement.Name = "radIncrement";
			this.radIncrement.Size = new System.Drawing.Size(75, 17);
			this.radIncrement.TabIndex = 3;
			this.radIncrement.TabStop = true;
			this.radIncrement.Text = "Increment:";
			this.radIncrement.UseVisualStyleBackColor = true;
			this.radIncrement.CheckedChanged += new System.EventHandler(this.radIncrement_CheckedChanged);
			// 
			// valDecrement
			// 
			this.valDecrement.BackColor = System.Drawing.Color.White;
			this.valDecrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valDecrement.ForeColor = System.Drawing.Color.Black;
			this.valDecrement.Location = new System.Drawing.Point(91, 47);
			this.valDecrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.valDecrement.Name = "valDecrement";
			this.valDecrement.Size = new System.Drawing.Size(53, 20);
			this.valDecrement.TabIndex = 2;
			this.valDecrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// radDecrement
			// 
			this.radDecrement.AutoSize = true;
			this.radDecrement.Location = new System.Drawing.Point(6, 49);
			this.radDecrement.Name = "radDecrement";
			this.radDecrement.Size = new System.Drawing.Size(80, 17);
			this.radDecrement.TabIndex = 1;
			this.radDecrement.TabStop = true;
			this.radDecrement.Text = "Decrement:";
			this.radDecrement.UseVisualStyleBackColor = true;
			this.radDecrement.CheckedChanged += new System.EventHandler(this.radDecrement_CheckedChanged);
			// 
			// radUnlock
			// 
			this.radUnlock.AutoSize = true;
			this.radUnlock.Location = new System.Drawing.Point(6, 26);
			this.radUnlock.Name = "radUnlock";
			this.radUnlock.Size = new System.Drawing.Size(59, 17);
			this.radUnlock.TabIndex = 0;
			this.radUnlock.TabStop = true;
			this.radUnlock.Text = "Unlock";
			this.radUnlock.UseVisualStyleBackColor = true;
			this.radUnlock.CheckedChanged += new System.EventHandler(this.radUnlock_CheckedChanged);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(214, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
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
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(295, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// lblText
			// 
			this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblText.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblText.Location = new System.Drawing.Point(12, 200);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(349, 47);
			this.lblText.TabIndex = 6;
			this.lblText.Text = "Text";
			// 
			// picPreview
			// 
			this.picPreview.BackColor = System.Drawing.Color.Transparent;
			this.picPreview.Location = new System.Drawing.Point(232, 65);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(128, 128);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPreview.TabIndex = 7;
			this.picPreview.TabStop = false;
			// 
			// recId
			// 
			this.recId.AllowCreate = false;
			this.recId.Location = new System.Drawing.Point(67, 5);
			this.recId.Name = "recId";
			this.recId.PlaceholderText = null;
			this.recId.Record = null;
			this.recId.RecordContext = null;
			this.recId.RecordFilter = null;
			this.recId.RecordKey = null;
			this.recId.RecordType = null;
			this.recId.Size = new System.Drawing.Size(150, 20);
			this.recId.TabIndex = 0;
			this.recId.UseAutoComplete = true;
			this.recId.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recId_RecordChanged);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 253);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(373, 30);
			this.skinnedPanel1.TabIndex = 8;
			// 
			// skinnedPanel2
			// 
			this.skinnedPanel2.Controls.Add(this.label1);
			this.skinnedPanel2.Controls.Add(this.recId);
			this.skinnedPanel2.Location = new System.Drawing.Point(1, 27);
			this.skinnedPanel2.Name = "skinnedPanel2";
			this.skinnedPanel2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedPanel2.Size = new System.Drawing.Size(371, 30);
			this.skinnedPanel2.TabIndex = 9;
			// 
			// TrophyForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(373, 283);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel2);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.lblText);
			this.Controls.Add(this.groupBox1);
			this.Name = "TrophyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Unlock Collectible";
			this.Shown += new System.EventHandler(this.TrophyForm_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valIncrement)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valDecrement)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel2.ResumeLayout(false);
			this.skinnedPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.RecordField recId;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedGroupBox groupBox1;
		private Desktop.Skinning.SkinnedNumericUpDown valSet;
		private Desktop.Skinning.SkinnedRadioButton radSet;
		private Desktop.Skinning.SkinnedNumericUpDown valIncrement;
		private Desktop.Skinning.SkinnedRadioButton radIncrement;
		private Desktop.Skinning.SkinnedNumericUpDown valDecrement;
		private Desktop.Skinning.SkinnedRadioButton radDecrement;
		private Desktop.Skinning.SkinnedRadioButton radUnlock;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel lblText;
		private System.Windows.Forms.PictureBox picPreview;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel2;
	}
}
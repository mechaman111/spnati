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
			this.recId = new Desktop.CommonControls.RecordField();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.valSet = new System.Windows.Forms.NumericUpDown();
			this.radSet = new System.Windows.Forms.RadioButton();
			this.valIncrement = new System.Windows.Forms.NumericUpDown();
			this.radIncrement = new System.Windows.Forms.RadioButton();
			this.valDecrement = new System.Windows.Forms.NumericUpDown();
			this.radDecrement = new System.Windows.Forms.RadioButton();
			this.radUnlock = new System.Windows.Forms.RadioButton();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.lblText = new System.Windows.Forms.Label();
			this.picPreview = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valIncrement)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valDecrement)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// recId
			// 
			this.recId.AllowCreate = false;
			this.recId.Location = new System.Drawing.Point(76, 12);
			this.recId.Name = "recId";
			this.recId.PlaceholderText = null;
			this.recId.Record = null;
			this.recId.RecordContext = null;
			this.recId.RecordKey = null;
			this.recId.RecordType = null;
			this.recId.Size = new System.Drawing.Size(150, 20);
			this.recId.TabIndex = 0;
			this.recId.UseAutoComplete = true;
			this.recId.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recId_RecordChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 16);
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
			this.groupBox1.Location = new System.Drawing.Point(15, 38);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(211, 114);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Effect";
			// 
			// valSet
			// 
			this.valSet.Location = new System.Drawing.Point(91, 88);
			this.valSet.Name = "valSet";
			this.valSet.Size = new System.Drawing.Size(53, 20);
			this.valSet.TabIndex = 6;
			// 
			// radSet
			// 
			this.radSet.AutoSize = true;
			this.radSet.Location = new System.Drawing.Point(6, 88);
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
			this.valIncrement.Location = new System.Drawing.Point(91, 65);
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
			this.radIncrement.Location = new System.Drawing.Point(6, 65);
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
			this.valDecrement.Location = new System.Drawing.Point(91, 42);
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
			this.radDecrement.Location = new System.Drawing.Point(6, 42);
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
			this.radUnlock.Location = new System.Drawing.Point(6, 19);
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
			this.cmdOK.Location = new System.Drawing.Point(205, 204);
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
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(286, 204);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// lblText
			// 
			this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblText.Location = new System.Drawing.Point(12, 155);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(349, 47);
			this.lblText.TabIndex = 6;
			this.lblText.Text = "Text";
			// 
			// picPreview
			// 
			this.picPreview.Location = new System.Drawing.Point(232, 16);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(128, 128);
			this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPreview.TabIndex = 7;
			this.picPreview.TabStop = false;
			// 
			// TrophyForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(373, 239);
			this.ControlBox = false;
			this.Controls.Add(this.picPreview);
			this.Controls.Add(this.lblText);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recId);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown valSet;
		private System.Windows.Forms.RadioButton radSet;
		private System.Windows.Forms.NumericUpDown valIncrement;
		private System.Windows.Forms.RadioButton radIncrement;
		private System.Windows.Forms.NumericUpDown valDecrement;
		private System.Windows.Forms.RadioButton radDecrement;
		private System.Windows.Forms.RadioButton radUnlock;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Label lblText;
		private System.Windows.Forms.PictureBox picPreview;
	}
}
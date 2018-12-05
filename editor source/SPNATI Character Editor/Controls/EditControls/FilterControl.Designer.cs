namespace SPNATI_Character_Editor
{
	partial class FilterControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.recTag = new Desktop.CommonControls.RecordField();
			this.label2 = new System.Windows.Forms.Label();
			this.cboGender = new System.Windows.Forms.ComboBox();
			this.chkNot = new System.Windows.Forms.CheckBox();
			this.cboStatus = new System.Windows.Forms.ComboBox();
			this.valFrom = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.valTo = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(84, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "of Tag:";
			// 
			// recTag
			// 
			this.recTag.AllowCreate = true;
			this.recTag.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.recTag.Location = new System.Drawing.Point(126, 1);
			this.recTag.Name = "recTag";
			this.recTag.PlaceholderText = null;
			this.recTag.Record = null;
			this.recTag.RecordContext = null;
			this.recTag.RecordKey = null;
			this.recTag.RecordType = null;
			this.recTag.Size = new System.Drawing.Size(98, 20);
			this.recTag.TabIndex = 1;
			this.recTag.UseAutoComplete = true;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(230, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Gender:";
			// 
			// cboGender
			// 
			this.cboGender.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FormattingEnabled = true;
			this.cboGender.Items.AddRange(new object[] {
            "",
            "female",
            "male"});
			this.cboGender.Location = new System.Drawing.Point(281, 0);
			this.cboGender.Name = "cboGender";
			this.cboGender.Size = new System.Drawing.Size(62, 21);
			this.cboGender.TabIndex = 3;
			// 
			// chkNot
			// 
			this.chkNot.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkNot.AutoSize = true;
			this.chkNot.Location = new System.Drawing.Point(349, 2);
			this.chkNot.Name = "chkNot";
			this.chkNot.Size = new System.Drawing.Size(43, 17);
			this.chkNot.TabIndex = 4;
			this.chkNot.Text = "Not";
			this.chkNot.UseVisualStyleBackColor = true;
			// 
			// cboStatus
			// 
			this.cboStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStatus.FormattingEnabled = true;
			this.cboStatus.Location = new System.Drawing.Point(388, 0);
			this.cboStatus.Name = "cboStatus";
			this.cboStatus.Size = new System.Drawing.Size(332, 21);
			this.cboStatus.TabIndex = 5;
			// 
			// valFrom
			// 
			this.valFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.valFrom.Location = new System.Drawing.Point(3, 0);
			this.valFrom.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.valFrom.Name = "valFrom";
			this.valFrom.Size = new System.Drawing.Size(30, 20);
			this.valFrom.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(33, 2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "to:";
			// 
			// valTo
			// 
			this.valTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.valTo.Location = new System.Drawing.Point(48, 0);
			this.valTo.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.valTo.Name = "valTo";
			this.valTo.Size = new System.Drawing.Size(30, 20);
			this.valTo.TabIndex = 8;
			// 
			// FilterControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valTo);
			this.Controls.Add(this.valFrom);
			this.Controls.Add(this.cboStatus);
			this.Controls.Add(this.chkNot);
			this.Controls.Add(this.cboGender);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.recTag);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Name = "FilterControl";
			this.Size = new System.Drawing.Size(723, 21);
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private Desktop.CommonControls.RecordField recTag;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboGender;
		private System.Windows.Forms.CheckBox chkNot;
		private System.Windows.Forms.ComboBox cboStatus;
		private System.Windows.Forms.NumericUpDown valFrom;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown valTo;
	}
}

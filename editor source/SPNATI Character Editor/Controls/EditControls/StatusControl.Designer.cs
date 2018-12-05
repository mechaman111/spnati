namespace SPNATI_Character_Editor
{
	partial class StatusControl
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
			this.chkNegate = new System.Windows.Forms.CheckBox();
			this.cboStatus = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// chkNegate
			// 
			this.chkNegate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkNegate.AutoSize = true;
			this.chkNegate.Location = new System.Drawing.Point(3, 3);
			this.chkNegate.Name = "chkNegate";
			this.chkNegate.Size = new System.Drawing.Size(43, 17);
			this.chkNegate.TabIndex = 33;
			this.chkNegate.Text = "Not";
			this.chkNegate.UseVisualStyleBackColor = true;
			// 
			// cboStatus
			// 
			this.cboStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cboStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStatus.FormattingEnabled = true;
			this.cboStatus.Location = new System.Drawing.Point(52, 0);
			this.cboStatus.Name = "cboStatus";
			this.cboStatus.Size = new System.Drawing.Size(193, 21);
			this.cboStatus.TabIndex = 34;
			// 
			// StatusControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkNegate);
			this.Controls.Add(this.cboStatus);
			this.Name = "StatusControl";
			this.Size = new System.Drawing.Size(357, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkNegate;
		private System.Windows.Forms.ComboBox cboStatus;
	}
}

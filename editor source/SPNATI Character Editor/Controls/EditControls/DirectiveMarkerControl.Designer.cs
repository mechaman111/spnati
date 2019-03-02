namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class DirectiveMarkerControl
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
			this.txtValue = new System.Windows.Forms.TextBox();
			this.cboOperator = new System.Windows.Forms.ComboBox();
			this.recField = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(181, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(83, 20);
			this.txtValue.TabIndex = 8;
			// 
			// cboOperator
			// 
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Items.AddRange(new object[] {
            "",
            "==",
            "!=",
            "<",
            ">",
            "<=",
            ">="});
			this.cboOperator.Location = new System.Drawing.Point(122, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.Size = new System.Drawing.Size(56, 21);
			this.cboOperator.TabIndex = 7;
			// 
			// recField
			// 
			this.recField.AllowCreate = true;
			this.recField.Location = new System.Drawing.Point(0, 1);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(118, 20);
			this.recField.TabIndex = 6;
			this.recField.UseAutoComplete = false;
			// 
			// DirectiveMarkerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.recField);
			this.Name = "DirectiveMarkerControl";
			this.Size = new System.Drawing.Size(428, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.ComboBox cboOperator;
		private Desktop.CommonControls.RecordField recField;
	}
}

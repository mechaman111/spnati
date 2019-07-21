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
			this.txtValue = new Desktop.Skinning.SkinnedTextBox();
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.recField = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.BackColor = System.Drawing.Color.White;
			this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtValue.ForeColor = System.Drawing.Color.Black;
			this.txtValue.Location = new System.Drawing.Point(181, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(83, 20);
			this.txtValue.TabIndex = 8;
			// 
			// cboOperator
			// 
			this.cboOperator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboOperator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboOperator.BackColor = System.Drawing.Color.White;
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Location = new System.Drawing.Point(122, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(56, 21);
			this.cboOperator.Sorted = false;
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
			this.recField.RecordFilter = null;
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

		private Desktop.Skinning.SkinnedTextBox txtValue;
		private Desktop.Skinning.SkinnedComboBox cboOperator;
		private Desktop.CommonControls.RecordField recField;
	}
}

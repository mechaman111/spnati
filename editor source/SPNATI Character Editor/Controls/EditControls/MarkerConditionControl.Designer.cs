namespace SPNATI_Character_Editor
{
	partial class MarkerConditionControl
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
			this.recField = new Desktop.CommonControls.RecordField();
			this.chkPerTarget = new Desktop.Skinning.SkinnedCheckBox();
			this.txtValue = new Desktop.Skinning.SkinnedTextBox();
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
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
			this.recField.Size = new System.Drawing.Size(150, 20);
			this.recField.TabIndex = 0;
			this.recField.UseAutoComplete = false;
			// 
			// chkPerTarget
			// 
			this.chkPerTarget.AutoSize = true;
			this.chkPerTarget.Location = new System.Drawing.Point(324, 2);
			this.chkPerTarget.Name = "chkPerTarget";
			this.chkPerTarget.Size = new System.Drawing.Size(76, 17);
			this.chkPerTarget.TabIndex = 6;
			this.chkPerTarget.Text = "Per Target";
			this.chkPerTarget.UseVisualStyleBackColor = true;
			// 
			// txtValue
			// 
			this.txtValue.BackColor = System.Drawing.Color.White;
			this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtValue.ForeColor = System.Drawing.Color.Black;
			this.txtValue.Location = new System.Drawing.Point(218, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(100, 20);
			this.txtValue.TabIndex = 5;
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
			this.cboOperator.Location = new System.Drawing.Point(156, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(56, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 4;
			// 
			// MarkerConditionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkPerTarget);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.recField);
			this.Name = "MarkerConditionControl";
			this.Size = new System.Drawing.Size(605, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private Desktop.Skinning.SkinnedCheckBox chkPerTarget;
		private Desktop.Skinning.SkinnedTextBox txtValue;
		private Desktop.Skinning.SkinnedComboBox cboOperator;
	}
}

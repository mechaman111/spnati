namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	partial class GlobalCategoryControl
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
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.recCategory = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
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
			this.cboOperator.KeyMember = null;
			this.cboOperator.Location = new System.Drawing.Point(3, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(51, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 2;
			// 
			// recCategory
			// 
			this.recCategory.AllowCreate = false;
			this.recCategory.Location = new System.Drawing.Point(60, 0);
			this.recCategory.Name = "recCategory";
			this.recCategory.PlaceholderText = null;
			this.recCategory.Record = null;
			this.recCategory.RecordContext = null;
			this.recCategory.RecordFilter = null;
			this.recCategory.RecordKey = null;
			this.recCategory.RecordType = null;
			this.recCategory.Size = new System.Drawing.Size(135, 20);
			this.recCategory.TabIndex = 15;
			this.recCategory.UseAutoComplete = false;
			// 
			// NumericCategoryControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.recCategory);
			this.Controls.Add(this.cboOperator);
			this.Name = "NumericCategoryControl";
			this.Size = new System.Drawing.Size(403, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboOperator;
		private Desktop.CommonControls.RecordField recCategory;
	}
}

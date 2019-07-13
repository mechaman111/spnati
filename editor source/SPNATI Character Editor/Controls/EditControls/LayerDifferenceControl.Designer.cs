namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class LayerDifferenceControl
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
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.recType = new Desktop.CommonControls.RecordField();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.recCharacter = new Desktop.CommonControls.RecordField();
			this.txtValue = new Desktop.Skinning.SkinnedTextBox();
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(4, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Player:";
			// 
			// recType
			// 
			this.recType.AllowCreate = false;
			this.recType.Location = new System.Drawing.Point(49, 0);
			this.recType.Name = "recType";
			this.recType.PlaceholderText = null;
			this.recType.Record = null;
			this.recType.RecordContext = null;
			this.recType.RecordFilter = null;
			this.recType.RecordKey = null;
			this.recType.RecordType = null;
			this.recType.Size = new System.Drawing.Size(112, 20);
			this.recType.TabIndex = 11;
			this.recType.UseAutoComplete = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(167, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "compare with:";
			// 
			// recCharacter
			// 
			this.recCharacter.AllowCreate = false;
			this.recCharacter.Location = new System.Drawing.Point(246, 0);
			this.recCharacter.Name = "recCharacter";
			this.recCharacter.PlaceholderText = null;
			this.recCharacter.Record = null;
			this.recCharacter.RecordContext = null;
			this.recCharacter.RecordFilter = null;
			this.recCharacter.RecordKey = null;
			this.recCharacter.RecordType = null;
			this.recCharacter.Size = new System.Drawing.Size(112, 20);
			this.recCharacter.TabIndex = 14;
			this.recCharacter.UseAutoComplete = false;
			// 
			// txtValue
			// 
			this.txtValue.BackColor = System.Drawing.Color.White;
			this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtValue.ForeColor = System.Drawing.Color.Black;
			this.txtValue.Location = new System.Drawing.Point(421, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(100, 20);
			this.txtValue.TabIndex = 16;
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
			this.cboOperator.Location = new System.Drawing.Point(361, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(56, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 15;
			// 
			// LayerDifferenceControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.recCharacter);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.recType);
			this.Name = "LayerDifferenceControl";
			this.Size = new System.Drawing.Size(618, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.CommonControls.RecordField recType;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.CommonControls.RecordField recCharacter;
		private Desktop.Skinning.SkinnedTextBox txtValue;
		private Desktop.Skinning.SkinnedComboBox cboOperator;
	}
}

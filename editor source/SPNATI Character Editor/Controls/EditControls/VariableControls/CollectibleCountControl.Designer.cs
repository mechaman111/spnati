namespace SPNATI_Character_Editor
{
	partial class CollectibleCountControl
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.valCounter = new Desktop.Skinning.SkinnedNumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valCounter)).BeginInit();
			this.SuspendLayout();
			// 
			// recField
			// 
			this.recField.AllowCreate = false;
			this.recField.Location = new System.Drawing.Point(206, 1);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordFilter = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(118, 20);
			this.recField.TabIndex = 1;
			this.recField.UseAutoComplete = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(148, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Collectible:";
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
			this.cboOperator.Location = new System.Drawing.Point(327, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(44, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 2;
			// 
			// valCounter
			// 
			this.valCounter.BackColor = System.Drawing.Color.White;
			this.valCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valCounter.ForeColor = System.Drawing.Color.Black;
			this.valCounter.Location = new System.Drawing.Point(377, 0);
			this.valCounter.Name = "valCounter";
			this.valCounter.Size = new System.Drawing.Size(49, 20);
			this.valCounter.TabIndex = 3;
			// 
			// CollectibleCountControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valCounter);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recField);
			this.Name = "CollectibleCountControl";
			this.Size = new System.Drawing.Size(745, 21);
			this.Controls.SetChildIndex(this.recField, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.cboOperator, 0);
			this.Controls.SetChildIndex(this.valCounter, 0);
			((System.ComponentModel.ISupportInitialize)(this.valCounter)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedComboBox cboOperator;
		private Desktop.Skinning.SkinnedNumericUpDown valCounter;
	}
}

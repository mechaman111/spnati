namespace SPNATI_Character_Editor
{
	partial class ExpressionControl
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.cboValue = new Desktop.Skinning.SkinnedComboBox();
			this.cboExpression = new Desktop.Skinning.SkinnedComboBox();
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Variable:";
			// 
			// cboValue
			// 
			this.cboValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.cboValue.BackColor = System.Drawing.Color.White;
			this.cboValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.cboValue.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboValue.FormattingEnabled = true;
			this.cboValue.Location = new System.Drawing.Point(230, 0);
			this.cboValue.Name = "cboValue";
			this.cboValue.SelectedIndex = -1;
			this.cboValue.SelectedItem = null;
			this.cboValue.Size = new System.Drawing.Size(112, 21);
			this.cboValue.Sorted = false;
			this.cboValue.TabIndex = 2;
			// 
			// cboExpression
			// 
			this.cboExpression.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboExpression.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboExpression.BackColor = System.Drawing.Color.White;
			this.cboExpression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.cboExpression.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboExpression.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboExpression.FormattingEnabled = true;
			this.cboExpression.Location = new System.Drawing.Point(57, 0);
			this.cboExpression.Name = "cboExpression";
			this.cboExpression.SelectedIndex = -1;
			this.cboExpression.SelectedItem = null;
			this.cboExpression.Size = new System.Drawing.Size(118, 21);
			this.cboExpression.Sorted = false;
			this.cboExpression.TabIndex = 0;
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
			this.cboOperator.Location = new System.Drawing.Point(181, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(43, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 1;
			// 
			// ExpressionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.cboExpression);
			this.Controls.Add(this.cboValue);
			this.Controls.Add(this.label1);
			this.Name = "ExpressionControl";
			this.Size = new System.Drawing.Size(433, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedComboBox cboValue;
		private Desktop.Skinning.SkinnedComboBox cboExpression;
		private Desktop.Skinning.SkinnedComboBox cboOperator;
	}
}

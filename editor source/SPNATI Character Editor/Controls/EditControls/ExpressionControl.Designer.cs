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
			this.label1 = new System.Windows.Forms.Label();
			this.cboValue = new System.Windows.Forms.ComboBox();
			this.cboExpression = new System.Windows.Forms.ComboBox();
			this.cboOperator = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Variable:";
			// 
			// cboValue
			// 
			this.cboValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.cboValue.FormattingEnabled = true;
			this.cboValue.Location = new System.Drawing.Point(226, 0);
			this.cboValue.Name = "cboValue";
			this.cboValue.Size = new System.Drawing.Size(112, 21);
			this.cboValue.TabIndex = 2;
			// 
			// cboExpression
			// 
			this.cboExpression.FormattingEnabled = true;
			this.cboExpression.Location = new System.Drawing.Point(57, 0);
			this.cboExpression.Name = "cboExpression";
			this.cboExpression.Size = new System.Drawing.Size(118, 21);
			this.cboExpression.TabIndex = 0;
			// 
			// cboOperator
			// 
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.Enabled = false;
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Items.AddRange(new object[] {
            "==",
            "<=",
            "<",
            ">",
            ">=",
            "!="});
			this.cboOperator.Location = new System.Drawing.Point(181, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.Size = new System.Drawing.Size(39, 21);
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboValue;
		private System.Windows.Forms.ComboBox cboExpression;
		private System.Windows.Forms.ComboBox cboOperator;
	}
}

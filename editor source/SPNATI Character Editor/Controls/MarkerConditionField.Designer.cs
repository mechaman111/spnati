namespace SPNATI_Character_Editor.Controls
{
	partial class MarkerConditionField
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
			this.cboMarker = new System.Windows.Forms.ComboBox();
			this.cboOperator = new System.Windows.Forms.ComboBox();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cboMarker
			// 
			this.cboMarker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboMarker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboMarker.FormattingEnabled = true;
			this.cboMarker.Location = new System.Drawing.Point(0, 0);
			this.cboMarker.Margin = new System.Windows.Forms.Padding(0);
			this.cboMarker.Name = "cboMarker";
			this.cboMarker.Size = new System.Drawing.Size(123, 21);
			this.cboMarker.TabIndex = 92;
			// 
			// cboOperator
			// 
			this.cboOperator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboOperator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Location = new System.Drawing.Point(123, 0);
			this.cboOperator.Margin = new System.Windows.Forms.Padding(0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.Size = new System.Drawing.Size(31, 21);
			this.cboOperator.TabIndex = 94;
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.Location = new System.Drawing.Point(157, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(68, 20);
			this.txtValue.TabIndex = 95;
			// 
			// MarkerConditionField
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.cboMarker);
			this.Name = "MarkerConditionField";
			this.Size = new System.Drawing.Size(226, 23);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboMarker;
		private System.Windows.Forms.ComboBox cboOperator;
		private System.Windows.Forms.TextBox txtValue;
	}
}

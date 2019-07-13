namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleAttributeEditControl
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
			this.txtAttribute = new Desktop.CommonControls.TextField();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.txtValue = new Desktop.CommonControls.TextField();
			this.SuspendLayout();
			// 
			// txtAttribute
			// 
			this.txtAttribute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.txtAttribute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.txtAttribute.Location = new System.Drawing.Point(3, 1);
			this.txtAttribute.Multiline = false;
			this.txtAttribute.Name = "txtAttribute";
			this.txtAttribute.PlaceholderText = "";
			this.txtAttribute.ReadOnly = false;
			this.txtAttribute.Size = new System.Drawing.Size(91, 20);
			this.txtAttribute.TabIndex = 1;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(95, 5);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(37, 13);
			this.skinnedLabel2.TabIndex = 2;
			this.skinnedLabel2.Text = "Value:";
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.txtValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.txtValue.Location = new System.Drawing.Point(133, 1);
			this.txtValue.Multiline = false;
			this.txtValue.Name = "txtValue";
			this.txtValue.PlaceholderText = "";
			this.txtValue.ReadOnly = false;
			this.txtValue.Size = new System.Drawing.Size(308, 20);
			this.txtValue.TabIndex = 3;
			// 
			// StyleAttributeEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.txtAttribute);
			this.Controls.Add(this.skinnedLabel2);
			this.Name = "StyleAttributeEditControl";
			this.Size = new System.Drawing.Size(446, 23);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Desktop.CommonControls.TextField txtAttribute;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.CommonControls.TextField txtValue;
	}
}

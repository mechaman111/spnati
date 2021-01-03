namespace SPNATI_Character_Editor.Controls.VariableControls
{
	partial class ParameterField
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
			this.lblLabel = new Desktop.Skinning.SkinnedLabel();
			this.txtValue = new Desktop.Skinning.SkinnedTextBox();
			this.SuspendLayout();
			// 
			// lblLabel
			// 
			this.lblLabel.AutoSize = true;
			this.lblLabel.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblLabel.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblLabel.Location = new System.Drawing.Point(3, 3);
			this.lblLabel.Name = "lblLabel";
			this.lblLabel.Size = new System.Drawing.Size(36, 13);
			this.lblLabel.TabIndex = 0;
			this.lblLabel.Text = "Label:";
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.BackColor = System.Drawing.Color.White;
			this.txtValue.ForeColor = System.Drawing.Color.Black;
			this.txtValue.Location = new System.Drawing.Point(106, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(132, 20);
			this.txtValue.TabIndex = 1;
			this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
			// 
			// ParameterField
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.lblLabel);
			this.Name = "ParameterField";
			this.Size = new System.Drawing.Size(238, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblLabel;
		private Desktop.Skinning.SkinnedTextBox txtValue;
	}
}

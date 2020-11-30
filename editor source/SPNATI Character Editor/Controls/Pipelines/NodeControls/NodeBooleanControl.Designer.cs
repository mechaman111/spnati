namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodeBooleanControl
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
			this.chkValue = new Desktop.Skinning.SkinnedCheckBox();
			this.SuspendLayout();
			// 
			// chkValue
			// 
			this.chkValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkValue.AutoSize = true;
			this.chkValue.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkValue.Location = new System.Drawing.Point(9, 4);
			this.chkValue.Name = "chkValue";
			this.chkValue.Size = new System.Drawing.Size(15, 14);
			this.chkValue.TabIndex = 0;
			this.chkValue.UseVisualStyleBackColor = true;
			// 
			// NodeBooleanControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkValue);
			this.Name = "NodeBooleanControl";
			this.Size = new System.Drawing.Size(27, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedCheckBox chkValue;
	}
}

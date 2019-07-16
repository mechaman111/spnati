namespace Desktop.Reporting.Controls
{
	partial class BooleanSlicerControl
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
			this.chkYes = new Desktop.Skinning.SkinnedCheckBox();
			this.chkNo = new Desktop.Skinning.SkinnedCheckBox();
			this.SuspendLayout();
			// 
			// chkYes
			// 
			this.chkYes.AutoSize = true;
			this.chkYes.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkYes.Location = new System.Drawing.Point(3, 3);
			this.chkYes.Name = "chkYes";
			this.chkYes.Size = new System.Drawing.Size(44, 17);
			this.chkYes.TabIndex = 0;
			this.chkYes.Text = "Yes";
			this.chkYes.UseVisualStyleBackColor = true;
			this.chkYes.CheckedChanged += new System.EventHandler(this.chkYes_CheckedChanged);
			// 
			// chkNo
			// 
			this.chkNo.AutoSize = true;
			this.chkNo.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkNo.Location = new System.Drawing.Point(3, 26);
			this.chkNo.Name = "chkNo";
			this.chkNo.Size = new System.Drawing.Size(40, 17);
			this.chkNo.TabIndex = 1;
			this.chkNo.Text = "No";
			this.chkNo.UseVisualStyleBackColor = true;
			this.chkNo.CheckedChanged += new System.EventHandler(this.chkNo_CheckedChanged);
			// 
			// BooleanSlicerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkNo);
			this.Controls.Add(this.chkYes);
			this.Name = "BooleanSlicerControl";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Skinning.SkinnedCheckBox chkYes;
		private Skinning.SkinnedCheckBox chkNo;
	}
}

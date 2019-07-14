namespace SPNATI_Character_Editor
{
	partial class TagControl
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
			this.chkNot = new Desktop.Skinning.SkinnedCheckBox();
			this.SuspendLayout();
			// 
			// recField
			// 
			this.recField.AllowCreate = false;
			this.recField.Location = new System.Drawing.Point(198, 1);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordFilter = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(117, 20);
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
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Has tag:";
			// 
			// chkNot
			// 
			this.chkNot.AutoSize = true;
			this.chkNot.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkNot.Location = new System.Drawing.Point(321, 3);
			this.chkNot.Name = "chkNot";
			this.chkNot.Size = new System.Drawing.Size(43, 17);
			this.chkNot.TabIndex = 2;
			this.chkNot.Text = "Not";
			this.chkNot.UseVisualStyleBackColor = true;
			// 
			// TagControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkNot);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recField);
			this.Name = "TagControl";
			this.Size = new System.Drawing.Size(433, 21);
			this.Controls.SetChildIndex(this.recField, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.chkNot, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedCheckBox chkNot;
	}
}

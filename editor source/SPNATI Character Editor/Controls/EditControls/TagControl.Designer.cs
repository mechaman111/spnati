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
			this.label1 = new System.Windows.Forms.Label();
			this.chkNot = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// recField
			// 
			this.recField.AllowCreate = false;
			this.recField.Location = new System.Drawing.Point(63, 1);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(150, 20);
			this.recField.TabIndex = 0;
			this.recField.UseAutoComplete = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Has tag:";
			// 
			// chkNot
			// 
			this.chkNot.AutoSize = true;
			this.chkNot.Location = new System.Drawing.Point(219, 3);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkNot;
	}
}

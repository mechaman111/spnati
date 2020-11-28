namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class PipelineEditControl
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
			this.cmdEdit = new Desktop.Skinning.SkinnedButton();
			this.SuspendLayout();
			// 
			// recField
			// 
			this.recField.AllowCreate = false;
			this.recField.Location = new System.Drawing.Point(0, 0);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordFilter = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(112, 20);
			this.recField.TabIndex = 1;
			this.recField.UseAutoComplete = false;
			// 
			// cmdEdit
			// 
			this.cmdEdit.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdEdit.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdEdit.Flat = true;
			this.cmdEdit.ForeColor = System.Drawing.Color.Blue;
			this.cmdEdit.Location = new System.Drawing.Point(109, -2);
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Size = new System.Drawing.Size(41, 23);
			this.cmdEdit.TabIndex = 2;
			this.cmdEdit.Text = "Edit";
			this.cmdEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmdEdit.UseVisualStyleBackColor = true;
			this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
			// 
			// PipelineEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.recField);
			this.Controls.Add(this.cmdEdit);
			this.Name = "PipelineEditControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private Desktop.Skinning.SkinnedButton cmdEdit;
	}
}

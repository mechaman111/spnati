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
			this.components = new System.ComponentModel.Container();
			this.recField = new Desktop.CommonControls.RecordField();
			this.cmdEdit = new Desktop.Skinning.SkinnedButton();
			this.cmdParams = new Desktop.Skinning.SkinnedIcon();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
			this.toolTip1.SetToolTip(this.cmdEdit, "Edit pipeline");
			this.cmdEdit.UseVisualStyleBackColor = true;
			this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
			// 
			// cmdParams
			// 
			this.cmdParams.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdParams.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdParams.Flat = false;
			this.cmdParams.Image = global::SPNATI_Character_Editor.Properties.Resources.PoseList;
			this.cmdParams.Location = new System.Drawing.Point(156, 3);
			this.cmdParams.Name = "cmdParams";
			this.cmdParams.Size = new System.Drawing.Size(16, 16);
			this.cmdParams.TabIndex = 3;
			this.toolTip1.SetToolTip(this.cmdParams, "Edit parameters");
			this.cmdParams.UseVisualStyleBackColor = true;
			this.cmdParams.Click += new System.EventHandler(this.cmdParams_Click);
			// 
			// PipelineEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdParams);
			this.Controls.Add(this.recField);
			this.Controls.Add(this.cmdEdit);
			this.Name = "PipelineEditControl";
			this.Size = new System.Drawing.Size(191, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private Desktop.Skinning.SkinnedButton cmdEdit;
		private Desktop.Skinning.SkinnedIcon cmdParams;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

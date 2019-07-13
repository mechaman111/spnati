namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	partial class CostumeControl
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
			this.chkNot = new Desktop.Skinning.SkinnedCheckBox();
			this.recCostume = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
			// 
			// chkNot
			// 
			this.chkNot.AutoSize = true;
			this.chkNot.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkNot.Location = new System.Drawing.Point(285, 2);
			this.chkNot.Name = "chkNot";
			this.chkNot.Size = new System.Drawing.Size(43, 17);
			this.chkNot.TabIndex = 15;
			this.chkNot.Text = "Not";
			this.chkNot.UseVisualStyleBackColor = true;
			// 
			// recCostume
			// 
			this.recCostume.AllowCreate = false;
			this.recCostume.Location = new System.Drawing.Point(153, 0);
			this.recCostume.Name = "recCostume";
			this.recCostume.PlaceholderText = null;
			this.recCostume.Record = null;
			this.recCostume.RecordContext = null;
			this.recCostume.RecordFilter = null;
			this.recCostume.RecordKey = null;
			this.recCostume.RecordType = null;
			this.recCostume.Size = new System.Drawing.Size(126, 20);
			this.recCostume.TabIndex = 14;
			this.recCostume.UseAutoComplete = false;
			// 
			// CostumeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.recCostume);
			this.Controls.Add(this.chkNot);
			this.Name = "CostumeControl";
			this.Controls.SetChildIndex(this.chkNot, 0);
			this.Controls.SetChildIndex(this.recCostume, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedCheckBox chkNot;
		private Desktop.CommonControls.RecordField recCostume;
	}
}

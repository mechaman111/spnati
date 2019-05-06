namespace SPNATI_Character_Editor
{
	partial class CharacterPersistentMarkerControl
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
			this.recItem = new Desktop.CommonControls.RecordField();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.cboOperator = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.recCharacter = new Desktop.CommonControls.RecordField();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// recField
			// 
			this.recItem.AllowCreate = true;
			this.recItem.Location = new System.Drawing.Point(226, 1);
			this.recItem.Name = "recField";
			this.recItem.PlaceholderText = null;
			this.recItem.Record = null;
			this.recItem.RecordContext = null;
			this.recItem.RecordKey = null;
			this.recItem.RecordType = null;
			this.recItem.Size = new System.Drawing.Size(109, 20);
			this.recItem.TabIndex = 0;
			this.recItem.UseAutoComplete = false;
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(399, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(100, 20);
			this.txtValue.TabIndex = 5;
			// 
			// cboOperator
			// 
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Items.AddRange(new object[] {
            "",
            "==",
            "!=",
            "<",
            ">",
            "<=",
            ">="});
			this.cboOperator.Location = new System.Drawing.Point(339, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.Size = new System.Drawing.Size(56, 21);
			this.cboOperator.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(2, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Character:";
			// 
			// recCharacter
			// 
			this.recCharacter.AllowCreate = false;
			this.recCharacter.Location = new System.Drawing.Point(64, 0);
			this.recCharacter.Name = "recCharacter";
			this.recCharacter.PlaceholderText = null;
			this.recCharacter.Record = null;
			this.recCharacter.RecordContext = null;
			this.recCharacter.RecordKey = null;
			this.recCharacter.RecordType = null;
			this.recCharacter.Size = new System.Drawing.Size(112, 20);
			this.recCharacter.TabIndex = 6;
			this.recCharacter.UseAutoComplete = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(179, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Marker:";
			// 
			// CharacterPersistentMarkerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.recCharacter);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.recItem);
			this.Name = "CharacterPersistentMarkerControl";
			this.Size = new System.Drawing.Size(605, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recItem;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.ComboBox cboOperator;
		private System.Windows.Forms.Label label2;
		private Desktop.CommonControls.RecordField recCharacter;
		private System.Windows.Forms.Label label1;
	}
}

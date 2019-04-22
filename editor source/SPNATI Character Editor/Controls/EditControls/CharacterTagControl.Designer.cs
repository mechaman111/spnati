namespace SPNATI_Character_Editor
{
	partial class CharacterTagControl
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
			this.label2 = new System.Windows.Forms.Label();
			this.recCharacter = new Desktop.CommonControls.RecordField();
			this.label1 = new System.Windows.Forms.Label();
			this.chkNot = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// recItem
			// 
			this.recItem.AllowCreate = false;
			this.recItem.Location = new System.Drawing.Point(236, 1);
			this.recItem.Name = "recItem";
			this.recItem.PlaceholderText = null;
			this.recItem.Record = null;
			this.recItem.RecordContext = null;
			this.recItem.RecordKey = null;
			this.recItem.RecordType = null;
			this.recItem.Size = new System.Drawing.Size(112, 20);
			this.recItem.TabIndex = 1;
			this.recItem.UseAutoComplete = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Character:";
			// 
			// recCharacter
			// 
			this.recCharacter.AllowCreate = false;
			this.recCharacter.Location = new System.Drawing.Point(65, 1);
			this.recCharacter.Name = "recCharacter";
			this.recCharacter.PlaceholderText = null;
			this.recCharacter.Record = null;
			this.recCharacter.RecordContext = null;
			this.recCharacter.RecordKey = null;
			this.recCharacter.RecordType = null;
			this.recCharacter.Size = new System.Drawing.Size(112, 20);
			this.recCharacter.TabIndex = 0;
			this.recCharacter.UseAutoComplete = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(183, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Has tag:";
			// 
			// chkNot
			// 
			this.chkNot.AutoSize = true;
			this.chkNot.Location = new System.Drawing.Point(354, 3);
			this.chkNot.Name = "chkNot";
			this.chkNot.Size = new System.Drawing.Size(43, 17);
			this.chkNot.TabIndex = 6;
			this.chkNot.Text = "Not";
			this.chkNot.UseVisualStyleBackColor = true;
			// 
			// CharacterTagControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkNot);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.recCharacter);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recItem);
			this.Name = "CharacterTagControl";
			this.Size = new System.Drawing.Size(521, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recItem;
		private System.Windows.Forms.Label label2;
		private Desktop.CommonControls.RecordField recCharacter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkNot;
	}
}

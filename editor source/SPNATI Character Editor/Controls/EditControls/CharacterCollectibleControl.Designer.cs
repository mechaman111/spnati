namespace SPNATI_Character_Editor
{
	partial class CharacterCollectibleControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.radUnlocked = new System.Windows.Forms.RadioButton();
			this.radLocked = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.recCharacter = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
			// 
			// recItem
			// 
			this.recItem.AllowCreate = false;
			this.recItem.Location = new System.Drawing.Point(241, 1);
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(180, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Collectible:";
			// 
			// radUnlocked
			// 
			this.radUnlocked.AutoSize = true;
			this.radUnlocked.Location = new System.Drawing.Point(357, 2);
			this.radUnlocked.Name = "radUnlocked";
			this.radUnlocked.Size = new System.Drawing.Size(71, 17);
			this.radUnlocked.TabIndex = 2;
			this.radUnlocked.TabStop = true;
			this.radUnlocked.Text = "Unlocked";
			this.radUnlocked.UseVisualStyleBackColor = true;
			// 
			// radLocked
			// 
			this.radLocked.AutoSize = true;
			this.radLocked.Location = new System.Drawing.Point(427, 2);
			this.radLocked.Name = "radLocked";
			this.radLocked.Size = new System.Drawing.Size(61, 17);
			this.radLocked.TabIndex = 3;
			this.radLocked.TabStop = true;
			this.radLocked.Text = "Locked";
			this.radLocked.UseVisualStyleBackColor = true;
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
			// CharacterCollectibleControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.recCharacter);
			this.Controls.Add(this.radLocked);
			this.Controls.Add(this.radUnlocked);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recItem);
			this.Name = "CharacterCollectibleControl";
			this.Size = new System.Drawing.Size(521, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radUnlocked;
		private System.Windows.Forms.RadioButton radLocked;
		private System.Windows.Forms.Label label2;
		private Desktop.CommonControls.RecordField recCharacter;
	}
}

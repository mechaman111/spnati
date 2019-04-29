namespace SPNATI_Character_Editor
{
	partial class CollectibleControl
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
			this.radUnlocked = new System.Windows.Forms.RadioButton();
			this.radLocked = new System.Windows.Forms.RadioButton();
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
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Collectible:";
			// 
			// radUnlocked
			// 
			this.radUnlocked.AutoSize = true;
			this.radUnlocked.Location = new System.Drawing.Point(220, 2);
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
			this.radLocked.Location = new System.Drawing.Point(297, 2);
			this.radLocked.Name = "radLocked";
			this.radLocked.Size = new System.Drawing.Size(61, 17);
			this.radLocked.TabIndex = 3;
			this.radLocked.TabStop = true;
			this.radLocked.Text = "Locked";
			this.radLocked.UseVisualStyleBackColor = true;
			// 
			// CollectibleControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.radLocked);
			this.Controls.Add(this.radUnlocked);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recField);
			this.Name = "CollectibleControl";
			this.Size = new System.Drawing.Size(433, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radUnlocked;
		private System.Windows.Forms.RadioButton radLocked;
	}
}

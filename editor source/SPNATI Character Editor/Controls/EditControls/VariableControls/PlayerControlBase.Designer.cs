namespace SPNATI_Character_Editor
{
	partial class PlayerControlBase
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
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.recType = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Player:";
			// 
			// recType
			// 
			this.recType.AllowCreate = false;
			this.recType.Location = new System.Drawing.Point(48, 0);
			this.recType.Name = "recType";
			this.recType.PlaceholderText = null;
			this.recType.Record = null;
			this.recType.RecordContext = null;
			this.recType.RecordFilter = null;
			this.recType.RecordKey = null;
			this.recType.RecordType = null;
			this.recType.Size = new System.Drawing.Size(100, 20);
			this.recType.TabIndex = 0;
			this.recType.UseAutoComplete = false;
			// 
			// PlayerControlBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.recType);
			this.Name = "PlayerControlBase";
			this.Size = new System.Drawing.Size(527, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.CommonControls.RecordField recType;
	}
}

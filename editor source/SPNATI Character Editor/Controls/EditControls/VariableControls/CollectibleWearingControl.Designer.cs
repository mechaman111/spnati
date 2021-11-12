namespace SPNATI_Character_Editor
{
	partial class CollectibleWearingControl
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
            this.radWearing = new Desktop.Skinning.SkinnedRadioButton();
            this.radNotWearing = new Desktop.Skinning.SkinnedRadioButton();
            this.SuspendLayout();
            // 
            // recField
            // 
            this.recField.AllowCreate = false;
            this.recField.Location = new System.Drawing.Point(206, 1);
            this.recField.Name = "recField";
            this.recField.PlaceholderText = null;
            this.recField.Record = null;
            this.recField.RecordContext = null;
            this.recField.RecordFilter = null;
            this.recField.RecordKey = null;
            this.recField.RecordType = null;
            this.recField.Size = new System.Drawing.Size(118, 20);
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
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Collectible:";
            // 
            // radWearing
            // 
            this.radWearing.AutoSize = true;
            this.radWearing.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
            this.radWearing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.radWearing.Location = new System.Drawing.Point(327, 2);
            this.radWearing.Name = "radWearing";
            this.radWearing.Size = new System.Drawing.Size(65, 17);
            this.radWearing.TabIndex = 2;
            this.radWearing.TabStop = true;
            this.radWearing.Text = "Wearing";
            this.radWearing.UseVisualStyleBackColor = true;
            // 
            // radNotWearing
            // 
            this.radNotWearing.AutoSize = true;
            this.radNotWearing.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
            this.radNotWearing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.radNotWearing.Location = new System.Drawing.Point(392, 2);
            this.radNotWearing.Name = "radNotWearing";
            this.radNotWearing.Size = new System.Drawing.Size(85, 17);
            this.radNotWearing.TabIndex = 3;
            this.radNotWearing.TabStop = true;
            this.radNotWearing.Text = "Not Wearing";
            this.radNotWearing.UseVisualStyleBackColor = true;
            // 
            // CollectibleWearingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radNotWearing);
            this.Controls.Add(this.radWearing);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recField);
            this.Name = "CollectibleWearingControl";
            this.Size = new System.Drawing.Size(633, 21);
            this.Controls.SetChildIndex(this.recField, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.radWearing, 0);
            this.Controls.SetChildIndex(this.radNotWearing, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedRadioButton radWearing;
		private Desktop.Skinning.SkinnedRadioButton radNotWearing;
	}
}

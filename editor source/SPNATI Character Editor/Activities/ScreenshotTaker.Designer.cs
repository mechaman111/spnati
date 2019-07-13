namespace SPNATI_Character_Editor.Activities
{
	partial class ScreenshotTaker
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.cmdAdvanced = new Desktop.Skinning.SkinnedButton();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "File name:";
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(64, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(174, 20);
			this.txtName.TabIndex = 1;
			// 
			// cmdImport
			// 
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdImport.Flat = false;
			this.cmdImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdImport.Location = new System.Drawing.Point(6, 29);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(232, 70);
			this.cmdImport.TabIndex = 4;
			this.cmdImport.Text = "Capture && Crop";
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(6, 102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(903, 169);
			this.label2.TabIndex = 5;
			this.label2.Text = "This will take a screenshot of whatever scene is currently in Kisekae without per" +
    "forming any additional processing and save it to the provided file name.";
			// 
			// cmdAdvanced
			// 
			this.cmdAdvanced.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAdvanced.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdAdvanced.Flat = false;
			this.cmdAdvanced.Location = new System.Drawing.Point(244, 29);
			this.cmdAdvanced.Name = "cmdAdvanced";
			this.cmdAdvanced.Size = new System.Drawing.Size(194, 23);
			this.cmdAdvanced.TabIndex = 6;
			this.cmdAdvanced.Text = "Set Part Transparencies...";
			this.cmdAdvanced.UseVisualStyleBackColor = true;
			this.cmdAdvanced.Click += new System.EventHandler(this.cmdAdvanced_Click);
			// 
			// ScreenshotTaker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdAdvanced);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdImport);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Name = "ScreenshotTaker";
			this.Size = new System.Drawing.Size(912, 572);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedTextBox txtName;
		private Desktop.Skinning.SkinnedButton cmdImport;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedButton cmdAdvanced;
	}
}

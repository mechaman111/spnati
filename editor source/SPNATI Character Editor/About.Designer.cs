namespace SPNATI_Character_Editor
{
	partial class About
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.cmdOK = new Desktop.Skinning.SkinnedButton();
            this.lblVersion = new Desktop.Skinning.SkinnedLabel();
            this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
            this.lblAuthors = new Desktop.Skinning.SkinnedLabel();
            this.skinnedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
            this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
            this.cmdOK.Flat = false;
            this.cmdOK.Location = new System.Drawing.Point(206, 4);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVersion.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblVersion.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblVersion.Location = new System.Drawing.Point(12, 39);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(149, 13);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "SPNATI Character Editor v1.0";
            // 
            // skinnedPanel1
            // 
            this.skinnedPanel1.Controls.Add(this.cmdOK);
            this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.skinnedPanel1.Location = new System.Drawing.Point(0, 85);
            this.skinnedPanel1.Name = "skinnedPanel1";
            this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
            this.skinnedPanel1.Size = new System.Drawing.Size(284, 30);
            this.skinnedPanel1.TabIndex = 2;
            this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
            // 
            // lblAuthors
            // 
            this.lblAuthors.AutoSize = true;
            this.lblAuthors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblAuthors.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAuthors.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
            this.lblAuthors.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
            this.lblAuthors.Location = new System.Drawing.Point(14, 61);
            this.lblAuthors.Name = "lblAuthors";
            this.lblAuthors.Size = new System.Drawing.Size(217, 13);
            this.lblAuthors.TabIndex = 3;
            this.lblAuthors.Text = "by spnati_edit, ReformCopyright, and Kobrad";
            // 
            // About
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 115);
            this.ControlBox = false;
            this.Controls.Add(this.lblAuthors);
            this.Controls.Add(this.skinnedPanel1);
            this.Controls.Add(this.lblVersion);
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.skinnedPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedLabel lblVersion;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
        private Desktop.Skinning.SkinnedLabel lblAuthors;
    }
}
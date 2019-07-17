namespace SPNATI_Character_Editor.Forms
{
	partial class ChangeLogReview
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
			this.lstVersions = new Desktop.Skinning.SkinnedListBox();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstVersions
			// 
			this.lstVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstVersions.BackColor = System.Drawing.Color.White;
			this.lstVersions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstVersions.ForeColor = System.Drawing.Color.Black;
			this.lstVersions.FormattingEnabled = true;
			this.lstVersions.Location = new System.Drawing.Point(12, 38);
			this.lstVersions.Name = "lstVersions";
			this.lstVersions.Size = new System.Drawing.Size(158, 602);
			this.lstVersions.TabIndex = 0;
			this.lstVersions.SelectedIndexChanged += new System.EventHandler(this.lstVersions_SelectedIndexChanged);
			// 
			// wb
			// 
			this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wb.Location = new System.Drawing.Point(176, 38);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(905, 603);
			this.wb.TabIndex = 1;
			this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(1014, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 646);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(1093, 30);
			this.skinnedPanel1.TabIndex = 3;
			// 
			// ChangeLogReview
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1093, 676);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.wb);
			this.Controls.Add(this.lstVersions);
			this.Name = "ChangeLogReview";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Change Log Review";
			this.Load += new System.EventHandler(this.ChangeLogReview_Load);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedListBox lstVersions;
		private System.Windows.Forms.WebBrowser wb;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
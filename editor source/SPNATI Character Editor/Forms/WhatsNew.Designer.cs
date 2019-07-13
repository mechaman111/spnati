namespace SPNATI_Character_Editor.Forms
{
	partial class WhatsNew
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.lblVersion = new Desktop.Skinning.SkinnedLabel();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel2 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedGroupBox1.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.skinnedPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Title;
			this.label1.Location = new System.Drawing.Point(3, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(229, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "New Version Installed:";
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Level = Desktop.Skinning.SkinnedLabelLevel.Title;
			this.lblVersion.Location = new System.Drawing.Point(238, 4);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(42, 26);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "3.0";
			// 
			// wb
			// 
			this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wb.Location = new System.Drawing.Point(6, 33);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(1014, 521);
			this.wb.TabIndex = 3;
			this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(972, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedGroupBox1.Controls.Add(this.wb);
			this.skinnedGroupBox1.Location = new System.Drawing.Point(12, 63);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.Size = new System.Drawing.Size(1026, 560);
			this.skinnedGroupBox1.TabIndex = 5;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "What\'s New?";
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 627);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(1050, 30);
			this.skinnedPanel1.TabIndex = 6;
			// 
			// skinnedPanel2
			// 
			this.skinnedPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel2.AutoScroll = true;
			this.skinnedPanel2.Controls.Add(this.label1);
			this.skinnedPanel2.Controls.Add(this.lblVersion);
			this.skinnedPanel2.Location = new System.Drawing.Point(1, 26);
			this.skinnedPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.skinnedPanel2.Name = "skinnedPanel2";
			this.skinnedPanel2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedPanel2.Size = new System.Drawing.Size(1048, 34);
			this.skinnedPanel2.TabIndex = 7;
			// 
			// WhatsNew
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1050, 657);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel2);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.skinnedGroupBox1);
			this.Name = "WhatsNew";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Version Update";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WhatsNew_FormClosing);
			this.Load += new System.EventHandler(this.WhatsNew_Load);
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel2.ResumeLayout(false);
			this.skinnedPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel lblVersion;
		private System.Windows.Forms.WebBrowser wb;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel2;
	}
}
namespace SPNATI_Character_Editor.Forms
{
	partial class ErrorTrace
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
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLinkLabel1 = new Desktop.Skinning.SkinnedLinkLabel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdOpen = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.Red;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Bad;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 34);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(252, 21);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Sorry! Something just went wrong.";
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(9, 117);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(320, 13);
			this.skinnedLabel2.TabIndex = 1;
			this.skinnedLabel2.Text = "You may want to review the files for any identifying information first.";
			// 
			// skinnedLinkLabel1
			// 
			this.skinnedLinkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLinkLabel1.ForeColor = System.Drawing.Color.Black;
			this.skinnedLinkLabel1.LinkArea = new System.Windows.Forms.LinkArea(27, 4);
			this.skinnedLinkLabel1.LinkColor = System.Drawing.Color.Blue;
			this.skinnedLinkLabel1.Location = new System.Drawing.Point(12, 64);
			this.skinnedLinkLabel1.Name = "skinnedLinkLabel1";
			this.skinnedLinkLabel1.Size = new System.Drawing.Size(347, 43);
			this.skinnedLinkLabel1.TabIndex = 2;
			this.skinnedLinkLabel1.TabStop = true;
			this.skinnedLinkLabel1.Text = "Details have been recorded here. To help improve this software, please submit cra" +
    "shdetails.zip to the character_editor_help discord channel.";
			this.skinnedLinkLabel1.UseCompatibleTextRendering = true;
			this.skinnedLinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.skinnedLinkLabel1_LinkClicked);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOpen);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 145);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(371, 31);
			this.skinnedPanel1.TabIndex = 3;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(303, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(65, 23);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdOpen
			// 
			this.cmdOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdOpen.Background = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.cmdOpen.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdOpen.Flat = true;
			this.cmdOpen.ForeColor = System.Drawing.Color.White;
			this.cmdOpen.Location = new System.Drawing.Point(3, 3);
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Size = new System.Drawing.Size(109, 23);
			this.cmdOpen.TabIndex = 1;
			this.cmdOpen.Text = "Open Folder";
			this.cmdOpen.UseVisualStyleBackColor = true;
			this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
			// 
			// ErrorTrace
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(371, 176);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.skinnedLinkLabel1);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.skinnedLabel1);
			this.Name = "ErrorTrace";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SPNATI Character Editor";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedLinkLabel skinnedLinkLabel1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdOpen;
	}
}
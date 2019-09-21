namespace SPNATI_Character_Editor.Forms
{
	partial class ErrorReport
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorReport));
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.txtDetails = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 203);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(484, 31);
			this.skinnedPanel1.TabIndex = 4;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(416, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(65, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(226, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(184, 23);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "Create and Open Folder";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 35);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(460, 60);
			this.skinnedLabel1.TabIndex = 5;
			this.skinnedLabel1.Text = resources.GetString("skinnedLabel1.Text");
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(12, 95);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(39, 13);
			this.skinnedLabel2.TabIndex = 6;
			this.skinnedLabel2.Text = "Details";
			// 
			// txtDetails
			// 
			this.txtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDetails.BackColor = System.Drawing.Color.White;
			this.txtDetails.ForeColor = System.Drawing.Color.Black;
			this.txtDetails.Location = new System.Drawing.Point(15, 111);
			this.txtDetails.Multiline = true;
			this.txtDetails.Name = "txtDetails";
			this.txtDetails.Size = new System.Drawing.Size(457, 86);
			this.txtDetails.TabIndex = 7;
			// 
			// ErrorReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 234);
			this.ControlBox = false;
			this.Controls.Add(this.txtDetails);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "ErrorReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Error Report";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedTextBox txtDetails;
		private Desktop.Skinning.SkinnedButton cmdCancel;
	}
}
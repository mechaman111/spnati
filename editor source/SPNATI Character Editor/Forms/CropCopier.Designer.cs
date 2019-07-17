namespace SPNATI_Character_Editor.Forms
{
	partial class CropCopier
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
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.lstFrom = new Desktop.Skinning.SkinnedListBox();
			this.lstTo = new Desktop.Skinning.SkinnedCheckedListBox();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label1.Location = new System.Drawing.Point(12, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Copy From:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label2.Location = new System.Drawing.Point(165, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Copy To:";
			// 
			// lstFrom
			// 
			this.lstFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstFrom.BackColor = System.Drawing.Color.White;
			this.lstFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstFrom.ForeColor = System.Drawing.Color.Black;
			this.lstFrom.FormattingEnabled = true;
			this.lstFrom.IntegralHeight = false;
			this.lstFrom.Location = new System.Drawing.Point(12, 55);
			this.lstFrom.Name = "lstFrom";
			this.lstFrom.Size = new System.Drawing.Size(150, 244);
			this.lstFrom.TabIndex = 2;
			// 
			// lstTo
			// 
			this.lstTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstTo.BackColor = System.Drawing.Color.White;
			this.lstTo.CheckOnClick = true;
			this.lstTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstTo.ForeColor = System.Drawing.Color.Black;
			this.lstTo.FormattingEnabled = true;
			this.lstTo.Location = new System.Drawing.Point(168, 55);
			this.lstTo.MultiColumn = true;
			this.lstTo.Name = "lstTo";
			this.lstTo.Size = new System.Drawing.Size(300, 244);
			this.lstTo.TabIndex = 3;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(290, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(91, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "Copy";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(387, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(89, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 303);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(479, 30);
			this.skinnedPanel1.TabIndex = 6;
			// 
			// CropCopier
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(479, 333);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.lstTo);
			this.Controls.Add(this.lstFrom);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CropCopier";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Copy Cropping Data";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedListBox lstFrom;
		private Desktop.Skinning.SkinnedCheckedListBox lstTo;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
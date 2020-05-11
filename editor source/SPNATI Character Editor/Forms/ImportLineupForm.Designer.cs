namespace SPNATI_Character_Editor.Forms
{
	partial class ImportLineupForm
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
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.txtCode = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.picHelp = new System.Windows.Forms.PictureBox();
			this.skinnedLabel4 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 471);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(800, 30);
			this.skinnedPanel1.TabIndex = 7;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(611, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(91, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "Import";
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
			this.cmdCancel.Location = new System.Drawing.Point(708, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(89, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 35);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(776, 21);
			this.skinnedLabel1.TabIndex = 8;
			this.skinnedLabel1.Text = "This will import a lineup of characters and split them into separate codes per st" +
    "age.";
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.Red;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Bad;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.skinnedLabel2.Location = new System.Drawing.Point(11, 56);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(407, 21);
			this.skinnedLabel2.TabIndex = 9;
			this.skinnedLabel2.Text = "Be sure you export your code from KKL with ALL selected.";
			// 
			// txtCode
			// 
			this.txtCode.BackColor = System.Drawing.Color.White;
			this.txtCode.ForeColor = System.Drawing.Color.Black;
			this.txtCode.Location = new System.Drawing.Point(12, 105);
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(773, 225);
			this.txtCode.TabIndex = 10;
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(12, 89);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(70, 13);
			this.skinnedLabel3.TabIndex = 11;
			this.skinnedLabel3.Text = "Lineup Code:";
			// 
			// picHelp
			// 
			this.picHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picHelp.InitialImage = null;
			this.picHelp.Location = new System.Drawing.Point(12, 357);
			this.picHelp.Name = "picHelp";
			this.picHelp.Size = new System.Drawing.Size(773, 111);
			this.picHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picHelp.TabIndex = 12;
			this.picHelp.TabStop = false;
			// 
			// skinnedLabel4
			// 
			this.skinnedLabel4.AutoSize = true;
			this.skinnedLabel4.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.skinnedLabel4.ForeColor = System.Drawing.Color.Blue;
			this.skinnedLabel4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel4.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.skinnedLabel4.Location = new System.Drawing.Point(12, 333);
			this.skinnedLabel4.Name = "skinnedLabel4";
			this.skinnedLabel4.Size = new System.Drawing.Size(148, 21);
			this.skinnedLabel4.TabIndex = 13;
			this.skinnedLabel4.Text = "Set Export Filters To:";
			// 
			// ImportLineupForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(800, 501);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedLabel4);
			this.Controls.Add(this.picHelp);
			this.Controls.Add(this.skinnedLabel3);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportLineupForm";
			this.Sizable = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import Character Lineup";
			this.skinnedPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedTextBox txtCode;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
		private System.Windows.Forms.PictureBox picHelp;
		private Desktop.Skinning.SkinnedLabel skinnedLabel4;
	}
}
namespace SPNATI_Character_Editor.Forms
{
	partial class FirstLaunchSetup
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
			this.cmdBrowseKisekae = new Desktop.Skinning.SkinnedButton();
			this.txtKisekae = new Desktop.Skinning.SkinnedTextBox();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.cmdBrowse = new Desktop.Skinning.SkinnedButton();
			this.txtApplicationDirectory = new Desktop.Skinning.SkinnedTextBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.txtUserName = new Desktop.Skinning.SkinnedTextBox();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.Blue;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 35);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(580, 49);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Welcome to the SPNATI Character Editor! As this is your first time running this p" +
    "rogram, we need a few bits of information.";
			// 
			// cmdBrowseKisekae
			// 
			this.cmdBrowseKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseKisekae.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowseKisekae.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBrowseKisekae.Flat = false;
			this.cmdBrowseKisekae.Location = new System.Drawing.Point(560, 111);
			this.cmdBrowseKisekae.Name = "cmdBrowseKisekae";
			this.cmdBrowseKisekae.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowseKisekae.TabIndex = 14;
			this.cmdBrowseKisekae.Text = "...";
			this.cmdBrowseKisekae.UseVisualStyleBackColor = true;
			this.cmdBrowseKisekae.Click += new System.EventHandler(this.cmdBrowseKisekae_Click);
			// 
			// txtKisekae
			// 
			this.txtKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKisekae.BackColor = System.Drawing.Color.White;
			this.txtKisekae.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtKisekae.ForeColor = System.Drawing.Color.Black;
			this.txtKisekae.Location = new System.Drawing.Point(139, 113);
			this.txtKisekae.Name = "txtKisekae";
			this.txtKisekae.Size = new System.Drawing.Size(415, 20);
			this.txtKisekae.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(13, 116);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "KKL.exe location:";
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowse.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBrowse.Flat = false;
			this.cmdBrowse.Location = new System.Drawing.Point(560, 85);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowse.TabIndex = 11;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// txtApplicationDirectory
			// 
			this.txtApplicationDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtApplicationDirectory.BackColor = System.Drawing.Color.White;
			this.txtApplicationDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtApplicationDirectory.ForeColor = System.Drawing.Color.Black;
			this.txtApplicationDirectory.Location = new System.Drawing.Point(139, 87);
			this.txtApplicationDirectory.Name = "txtApplicationDirectory";
			this.txtApplicationDirectory.Size = new System.Drawing.Size(415, 20);
			this.txtApplicationDirectory.TabIndex = 10;
			this.txtApplicationDirectory.Validating += new System.ComponentModel.CancelEventHandler(this.txtApplicationDirectory_Validating);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(12, 90);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "SPNATI repository:";
			// 
			// txtUserName
			// 
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserName.BackColor = System.Drawing.Color.White;
			this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtUserName.ForeColor = System.Drawing.Color.Black;
			this.txtUserName.Location = new System.Drawing.Point(139, 139);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(238, 20);
			this.txtUserName.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(13, 142);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Username:";
			// 
			// cmdOK
			// 
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(445, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 18;
			this.cmdOK.Text = "Accept";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.skinnedLabel2);
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 171);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(604, 30);
			this.skinnedPanel1.TabIndex = 19;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(526, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 19;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Exe files|*.exe";
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(4, 8);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(431, 13);
			this.skinnedLabel2.TabIndex = 20;
			this.skinnedLabel2.Text = "To change these and other settings later, use the Gear in the upper right of the " +
    "main editor";
			// 
			// FirstLaunchSetup
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(604, 201);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.cmdBrowseKisekae);
			this.Controls.Add(this.txtKisekae);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtApplicationDirectory);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.skinnedLabel1);
			this.Name = "FirstLaunchSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Welcome";
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedButton cmdBrowseKisekae;
		private Desktop.Skinning.SkinnedTextBox txtKisekae;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedButton cmdBrowse;
		private Desktop.Skinning.SkinnedTextBox txtApplicationDirectory;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedTextBox txtUserName;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
	}
}
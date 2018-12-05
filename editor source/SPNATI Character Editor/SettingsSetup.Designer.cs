namespace SPNATI_Character_Editor
{
	partial class SettingsSetup
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
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.txtApplicationDirectory = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtKisekae = new System.Windows.Forms.TextBox();
			this.cmdBrowseKkl = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// txtApplicationDirectory
			// 
			this.txtApplicationDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtApplicationDirectory.Location = new System.Drawing.Point(126, 12);
			this.txtApplicationDirectory.Name = "txtApplicationDirectory";
			this.txtApplicationDirectory.Size = new System.Drawing.Size(449, 20);
			this.txtApplicationDirectory.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "SPNATI repository:";
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOk.Location = new System.Drawing.Point(457, 70);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 2;
			this.cmdOk.Text = "OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.Location = new System.Drawing.Point(581, 10);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowse.TabIndex = 3;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(538, 70);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Kisekae location:";
			this.label2.Visible = false;
			// 
			// txtKisekae
			// 
			this.txtKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKisekae.Location = new System.Drawing.Point(126, 40);
			this.txtKisekae.Name = "txtKisekae";
			this.txtKisekae.Size = new System.Drawing.Size(449, 20);
			this.txtKisekae.TabIndex = 6;
			this.txtKisekae.Visible = false;
			// 
			// cmdBrowseKkl
			// 
			this.cmdBrowseKkl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseKkl.Location = new System.Drawing.Point(581, 38);
			this.cmdBrowseKkl.Name = "cmdBrowseKkl";
			this.cmdBrowseKkl.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowseKkl.TabIndex = 7;
			this.cmdBrowseKkl.Text = "...";
			this.cmdBrowseKkl.UseVisualStyleBackColor = true;
			this.cmdBrowseKkl.Visible = false;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Exe files|*.exe";
			// 
			// SettingsSetup
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(625, 105);
			this.Controls.Add(this.cmdBrowseKkl);
			this.Controls.Add(this.txtKisekae);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtApplicationDirectory);
			this.Name = "SettingsSetup";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox txtApplicationDirectory;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtKisekae;
		private System.Windows.Forms.Button cmdBrowseKkl;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}
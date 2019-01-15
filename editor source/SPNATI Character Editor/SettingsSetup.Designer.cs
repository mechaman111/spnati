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
			this.components = new System.ComponentModel.Container();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.txtApplicationDirectory = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.valAutoSave = new System.Windows.Forms.NumericUpDown();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkHideImages = new System.Windows.Forms.CheckBox();
			this.chkIntellisense = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtFilter = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.valAutoSave)).BeginInit();
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
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "SPNATI repository:";
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOk.Location = new System.Drawing.Point(457, 111);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 4;
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
			this.cmdBrowse.TabIndex = 1;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(538, 111);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Username:";
			// 
			// txtUserName
			// 
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserName.Location = new System.Drawing.Point(126, 38);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(487, 20);
			this.txtUserName.TabIndex = 2;
			this.toolTip1.SetToolTip(this.txtUserName, "This is used for auto-saving. Only characters written by this user will be auto-s" +
        "aved.");
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Exe files|*.exe";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(103, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Auto-save (minutes):";
			// 
			// valAutoSave
			// 
			this.valAutoSave.Location = new System.Drawing.Point(126, 64);
			this.valAutoSave.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.valAutoSave.Name = "valAutoSave";
			this.valAutoSave.Size = new System.Drawing.Size(45, 20);
			this.valAutoSave.TabIndex = 3;
			this.toolTip1.SetToolTip(this.valAutoSave, "Number of minutes to auto-save characters you\'ve written. Use 0 to disable auto-s" +
        "ave.");
			// 
			// chkHideImages
			// 
			this.chkHideImages.AutoSize = true;
			this.chkHideImages.Location = new System.Drawing.Point(140, 91);
			this.chkHideImages.Name = "chkHideImages";
			this.chkHideImages.Size = new System.Drawing.Size(145, 17);
			this.chkHideImages.TabIndex = 9;
			this.chkHideImages.Text = "Include Prefixless Images";
			this.toolTip1.SetToolTip(this.chkHideImages, "If unchecked, images with no prefix (ex. 0-*.png) will not appear for use in dial" +
        "ogue lines.");
			this.chkHideImages.UseVisualStyleBackColor = true;
			this.chkHideImages.CheckedChanged += new System.EventHandler(this.chkHideImages_CheckedChanged);
			// 
			// chkIntellisense
			// 
			this.chkIntellisense.AutoSize = true;
			this.chkIntellisense.Location = new System.Drawing.Point(15, 91);
			this.chkIntellisense.Name = "chkIntellisense";
			this.chkIntellisense.Size = new System.Drawing.Size(119, 17);
			this.chkIntellisense.TabIndex = 8;
			this.chkIntellisense.Text = "Variable Intellisense";
			this.chkIntellisense.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(291, 92);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(143, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Exclude images starting with:";
			// 
			// txtFilter
			// 
			this.txtFilter.Location = new System.Drawing.Point(440, 89);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(92, 20);
			this.txtFilter.TabIndex = 11;
			// 
			// SettingsSetup
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(625, 146);
			this.Controls.Add(this.txtFilter);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkHideImages);
			this.Controls.Add(this.chkIntellisense);
			this.Controls.Add(this.valAutoSave);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtUserName);
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
			((System.ComponentModel.ISupportInitialize)(this.valAutoSave)).EndInit();
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
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown valAutoSave;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox chkIntellisense;
		private System.Windows.Forms.CheckBox chkHideImages;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtFilter;
	}
}
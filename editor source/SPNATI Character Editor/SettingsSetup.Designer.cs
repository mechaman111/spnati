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
			this.helpAutoSave = new System.Windows.Forms.Button();
			this.helpIntellisense = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.chkIntellisense = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.tabsSections = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.chkAutoBackup = new System.Windows.Forms.CheckBox();
			this.tabDialogue = new System.Windows.Forms.TabPage();
			this.chkInitialAdd = new System.Windows.Forms.CheckBox();
			this.tabBanter = new System.Windows.Forms.TabPage();
			this.chkAutoBanter = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cmdBrowseKisekae = new System.Windows.Forms.Button();
			this.txtKisekae = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.valAutoSave)).BeginInit();
			this.tabsSections.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabDialogue.SuspendLayout();
			this.tabBanter.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtApplicationDirectory
			// 
			this.txtApplicationDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtApplicationDirectory.Location = new System.Drawing.Point(116, 8);
			this.txtApplicationDirectory.Name = "txtApplicationDirectory";
			this.txtApplicationDirectory.Size = new System.Drawing.Size(353, 20);
			this.txtApplicationDirectory.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "SPNATI repository:";
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOk.Location = new System.Drawing.Point(457, 178);
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
			this.cmdBrowse.Location = new System.Drawing.Point(475, 6);
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
			this.cmdCancel.Location = new System.Drawing.Point(538, 178);
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
			this.label2.Location = new System.Drawing.Point(7, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Username:";
			// 
			// txtUserName
			// 
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserName.Location = new System.Drawing.Point(116, 60);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(391, 20);
			this.txtUserName.TabIndex = 4;
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
			this.label3.Location = new System.Drawing.Point(7, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(103, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Auto-save (minutes):";
			// 
			// valAutoSave
			// 
			this.valAutoSave.Location = new System.Drawing.Point(116, 86);
			this.valAutoSave.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.valAutoSave.Name = "valAutoSave";
			this.valAutoSave.Size = new System.Drawing.Size(45, 20);
			this.valAutoSave.TabIndex = 5;
			this.toolTip1.SetToolTip(this.valAutoSave, "Number of minutes to auto-save characters you\'ve written. Use 0 to disable auto-s" +
        "ave.");
			// 
			// chkHideImages
			// 
			this.chkHideImages.AutoSize = true;
			this.chkHideImages.Location = new System.Drawing.Point(6, 29);
			this.chkHideImages.Name = "chkHideImages";
			this.chkHideImages.Size = new System.Drawing.Size(143, 17);
			this.chkHideImages.TabIndex = 9;
			this.chkHideImages.Text = "Include prefixless images";
			this.toolTip1.SetToolTip(this.chkHideImages, "If unchecked, images with no prefix (ex. 0-*.png) will not appear for use in dial" +
        "ogue lines.");
			this.chkHideImages.UseVisualStyleBackColor = true;
			this.chkHideImages.CheckedChanged += new System.EventHandler(this.chkHideImages_CheckedChanged);
			// 
			// helpAutoSave
			// 
			this.helpAutoSave.FlatAppearance.BorderSize = 0;
			this.helpAutoSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.helpAutoSave.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.helpAutoSave.Location = new System.Drawing.Point(167, 83);
			this.helpAutoSave.Name = "helpAutoSave";
			this.helpAutoSave.Size = new System.Drawing.Size(26, 23);
			this.helpAutoSave.TabIndex = 6;
			this.toolTip1.SetToolTip(this.helpAutoSave, "Only characters with your username as the writer can be auto-saved");
			this.helpAutoSave.UseVisualStyleBackColor = true;
			// 
			// helpIntellisense
			// 
			this.helpIntellisense.FlatAppearance.BorderSize = 0;
			this.helpIntellisense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.helpIntellisense.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.helpIntellisense.Location = new System.Drawing.Point(149, 2);
			this.helpIntellisense.Name = "helpIntellisense";
			this.helpIntellisense.Size = new System.Drawing.Size(26, 23);
			this.helpIntellisense.TabIndex = 12;
			this.toolTip1.SetToolTip(this.helpIntellisense, "Typing ~ in a dialogue line will bring up a popup containing available variables," +
        " their meanings, parameters, and so on.");
			this.helpIntellisense.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.button1.Location = new System.Drawing.Point(149, 25);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(26, 23);
			this.button1.TabIndex = 13;
			this.toolTip1.SetToolTip(this.button1, "When checked, every image in the character\'s folder with no stage prefix (ex. 2-h" +
        "appy.png) will be available for poses in every stage.");
			this.button1.UseVisualStyleBackColor = true;
			// 
			// chkIntellisense
			// 
			this.chkIntellisense.AutoSize = true;
			this.chkIntellisense.Location = new System.Drawing.Point(6, 6);
			this.chkIntellisense.Name = "chkIntellisense";
			this.chkIntellisense.Size = new System.Drawing.Size(139, 17);
			this.chkIntellisense.TabIndex = 8;
			this.chkIntellisense.Text = "Use variable intellisense";
			this.chkIntellisense.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(143, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Exclude images starting with:";
			// 
			// txtFilter
			// 
			this.txtFilter.Location = new System.Drawing.Point(155, 50);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(92, 20);
			this.txtFilter.TabIndex = 11;
			// 
			// tabsSections
			// 
			this.tabsSections.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabsSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabsSections.Controls.Add(this.tabGeneral);
			this.tabsSections.Controls.Add(this.tabDialogue);
			this.tabsSections.Controls.Add(this.tabBanter);
			this.tabsSections.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabsSections.ItemSize = new System.Drawing.Size(25, 100);
			this.tabsSections.Location = new System.Drawing.Point(2, 2);
			this.tabsSections.Multiline = true;
			this.tabsSections.Name = "tabsSections";
			this.tabsSections.SelectedIndex = 0;
			this.tabsSections.Size = new System.Drawing.Size(621, 170);
			this.tabsSections.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabsSections.TabIndex = 12;
			this.tabsSections.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabsSections_DrawItem);
			// 
			// tabGeneral
			// 
			this.tabGeneral.BackColor = System.Drawing.SystemColors.Control;
			this.tabGeneral.Controls.Add(this.cmdBrowseKisekae);
			this.tabGeneral.Controls.Add(this.txtKisekae);
			this.tabGeneral.Controls.Add(this.label5);
			this.tabGeneral.Controls.Add(this.chkAutoBackup);
			this.tabGeneral.Controls.Add(this.helpAutoSave);
			this.tabGeneral.Controls.Add(this.cmdBrowse);
			this.tabGeneral.Controls.Add(this.txtApplicationDirectory);
			this.tabGeneral.Controls.Add(this.label1);
			this.tabGeneral.Controls.Add(this.txtUserName);
			this.tabGeneral.Controls.Add(this.valAutoSave);
			this.tabGeneral.Controls.Add(this.label2);
			this.tabGeneral.Controls.Add(this.label3);
			this.tabGeneral.Location = new System.Drawing.Point(104, 4);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(513, 162);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			// 
			// chkAutoBackup
			// 
			this.chkAutoBackup.AutoSize = true;
			this.chkAutoBackup.Location = new System.Drawing.Point(250, 87);
			this.chkAutoBackup.Name = "chkAutoBackup";
			this.chkAutoBackup.Size = new System.Drawing.Size(176, 17);
			this.chkAutoBackup.TabIndex = 7;
			this.chkAutoBackup.Text = "Create data recovery snapshots";
			this.chkAutoBackup.UseVisualStyleBackColor = true;
			// 
			// tabDialogue
			// 
			this.tabDialogue.BackColor = System.Drawing.SystemColors.Control;
			this.tabDialogue.Controls.Add(this.chkInitialAdd);
			this.tabDialogue.Controls.Add(this.button1);
			this.tabDialogue.Controls.Add(this.helpIntellisense);
			this.tabDialogue.Controls.Add(this.chkIntellisense);
			this.tabDialogue.Controls.Add(this.txtFilter);
			this.tabDialogue.Controls.Add(this.chkHideImages);
			this.tabDialogue.Controls.Add(this.label4);
			this.tabDialogue.Location = new System.Drawing.Point(104, 4);
			this.tabDialogue.Name = "tabDialogue";
			this.tabDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tabDialogue.Size = new System.Drawing.Size(513, 102);
			this.tabDialogue.TabIndex = 1;
			this.tabDialogue.Text = "Dialogue";
			// 
			// chkInitialAdd
			// 
			this.chkInitialAdd.AutoSize = true;
			this.chkInitialAdd.Location = new System.Drawing.Point(6, 74);
			this.chkInitialAdd.Name = "chkInitialAdd";
			this.chkInitialAdd.Size = new System.Drawing.Size(258, 17);
			this.chkInitialAdd.TabIndex = 14;
			this.chkInitialAdd.Text = "Auto-open selection form when adding conditions";
			this.chkInitialAdd.UseVisualStyleBackColor = true;
			// 
			// tabBanter
			// 
			this.tabBanter.BackColor = System.Drawing.SystemColors.Control;
			this.tabBanter.Controls.Add(this.chkAutoBanter);
			this.tabBanter.Location = new System.Drawing.Point(104, 4);
			this.tabBanter.Name = "tabBanter";
			this.tabBanter.Padding = new System.Windows.Forms.Padding(3);
			this.tabBanter.Size = new System.Drawing.Size(513, 102);
			this.tabBanter.TabIndex = 2;
			this.tabBanter.Text = "Banter Wizard";
			// 
			// chkAutoBanter
			// 
			this.chkAutoBanter.AutoSize = true;
			this.chkAutoBanter.Location = new System.Drawing.Point(6, 6);
			this.chkAutoBanter.Name = "chkAutoBanter";
			this.chkAutoBanter.Size = new System.Drawing.Size(383, 17);
			this.chkAutoBanter.TabIndex = 0;
			this.chkAutoBanter.Text = "Always filter list to characters who actually target yours (very slow initial loa" +
    "d)";
			this.chkAutoBanter.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "KKL.exe location:";
			// 
			// cmdBrowseKisekae
			// 
			this.cmdBrowseKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseKisekae.Location = new System.Drawing.Point(475, 32);
			this.cmdBrowseKisekae.Name = "cmdBrowseKisekae";
			this.cmdBrowseKisekae.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowseKisekae.TabIndex = 3;
			this.cmdBrowseKisekae.Text = "...";
			this.cmdBrowseKisekae.UseVisualStyleBackColor = true;
			this.cmdBrowseKisekae.Click += new System.EventHandler(this.cmdBrowseKisekae_Click);
			// 
			// txtKisekae
			// 
			this.txtKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKisekae.Location = new System.Drawing.Point(116, 34);
			this.txtKisekae.Name = "txtKisekae";
			this.txtKisekae.Size = new System.Drawing.Size(353, 20);
			this.txtKisekae.TabIndex = 2;
			// 
			// SettingsSetup
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(625, 213);
			this.Controls.Add(this.tabsSections);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOk);
			this.Name = "SettingsSetup";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.valAutoSave)).EndInit();
			this.tabsSections.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.tabDialogue.ResumeLayout(false);
			this.tabDialogue.PerformLayout();
			this.tabBanter.ResumeLayout(false);
			this.tabBanter.PerformLayout();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.TabControl tabsSections;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabDialogue;
		private System.Windows.Forms.Button helpAutoSave;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button helpIntellisense;
		private System.Windows.Forms.TabPage tabBanter;
		private System.Windows.Forms.CheckBox chkAutoBanter;
		private System.Windows.Forms.CheckBox chkAutoBackup;
		private System.Windows.Forms.CheckBox chkInitialAdd;
		private System.Windows.Forms.Button cmdBrowseKisekae;
		private System.Windows.Forms.TextBox txtKisekae;
		private System.Windows.Forms.Label label5;
	}
}
namespace SPNATI_Character_Editor.Forms
{
	partial class FailedImport
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FailedImport));
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel4 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel5 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel6 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.Red;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Bad;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 37);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(145, 13);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Failed to import from Kisekae.";
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(12, 59);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(305, 13);
			this.skinnedLabel2.TabIndex = 1;
			this.skinnedLabel2.Text = "This usually happens because of one of the following problems:";
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 199);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.skinnedPanel1.Size = new System.Drawing.Size(448, 30);
			this.skinnedPanel1.TabIndex = 2;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(370, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(41, 81);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(149, 13);
			this.skinnedLabel3.TabIndex = 3;
			this.skinnedLabel3.Text = "1. You are not running kkl.exe";
			// 
			// skinnedLabel4
			// 
			this.skinnedLabel4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel4.Location = new System.Drawing.Point(41, 99);
			this.skinnedLabel4.Name = "skinnedLabel4";
			this.skinnedLabel4.Size = new System.Drawing.Size(398, 30);
			this.skinnedLabel4.TabIndex = 4;
			this.skinnedLabel4.Text = "2. You are not running the same version of kkl.exe that the editor is configured " +
    "to communicate with. Check the editor settings.";
			// 
			// skinnedLabel5
			// 
			this.skinnedLabel5.AutoSize = true;
			this.skinnedLabel5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel5.Location = new System.Drawing.Point(41, 131);
			this.skinnedLabel5.Name = "skinnedLabel5";
			this.skinnedLabel5.Size = new System.Drawing.Size(236, 13);
			this.skinnedLabel5.TabIndex = 5;
			this.skinnedLabel5.Text = "3. You are running from a Mac or Linux machine.";
			// 
			// skinnedLabel6
			// 
			this.skinnedLabel6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel6.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel6.Location = new System.Drawing.Point(41, 148);
			this.skinnedLabel6.Name = "skinnedLabel6";
			this.skinnedLabel6.Size = new System.Drawing.Size(398, 43);
			this.skinnedLabel6.TabIndex = 6;
			this.skinnedLabel6.Text = resources.GetString("skinnedLabel6.Text");
			// 
			// FailedImport
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdOK;
			this.ClientSize = new System.Drawing.Size(448, 229);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedLabel6);
			this.Controls.Add(this.skinnedLabel5);
			this.Controls.Add(this.skinnedLabel4);
			this.Controls.Add(this.skinnedLabel3);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.skinnedLabel1);
			this.Name = "FailedImport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import Image";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
		private Desktop.Skinning.SkinnedLabel skinnedLabel4;
		private Desktop.Skinning.SkinnedLabel skinnedLabel5;
		private Desktop.Skinning.SkinnedLabel skinnedLabel6;
	}
}
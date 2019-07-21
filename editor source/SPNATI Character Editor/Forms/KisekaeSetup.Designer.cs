namespace SPNATI_Character_Editor.Forms
{
	partial class KisekaeSetup
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
			this.cmdBrowseKisekae = new Desktop.Skinning.SkinnedButton();
			this.txtKisekae = new Desktop.Skinning.SkinnedTextBox();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.cmdOk = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdBrowseKisekae
			// 
			this.cmdBrowseKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseKisekae.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowseKisekae.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBrowseKisekae.Flat = false;
			this.cmdBrowseKisekae.Location = new System.Drawing.Point(449, 78);
			this.cmdBrowseKisekae.Name = "cmdBrowseKisekae";
			this.cmdBrowseKisekae.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowseKisekae.TabIndex = 11;
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
			this.txtKisekae.Location = new System.Drawing.Point(109, 79);
			this.txtKisekae.Name = "txtKisekae";
			this.txtKisekae.Size = new System.Drawing.Size(334, 20);
			this.txtKisekae.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(12, 82);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "KKL.exe location:";
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOk.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOk.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOk.Flat = false;
			this.cmdOk.ForeColor = System.Drawing.Color.White;
			this.cmdOk.Location = new System.Drawing.Point(334, 3);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 12;
			this.cmdOk.Text = "OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(415, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 14;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(12, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(469, 41);
			this.label1.TabIndex = 15;
			this.label1.Text = "Hi! Some new features require knowing where kkl.exe is located. You won\'t need to" +
    " fill this in again unless you change where kkl.exe is located.";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.PrimaryDark;
			this.label2.Location = new System.Drawing.Point(4, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(201, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "You can change this later in File > Setup.";
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.label2);
			this.skinnedPanel1.Controls.Add(this.cmdOk);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 111);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(493, 30);
			this.skinnedPanel1.TabIndex = 17;
			// 
			// KisekaeSetup
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(493, 141);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdBrowseKisekae);
			this.Controls.Add(this.txtKisekae);
			this.Controls.Add(this.label5);
			this.Name = "KisekaeSetup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup";
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedButton cmdBrowseKisekae;
		private Desktop.Skinning.SkinnedTextBox txtKisekae;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedButton cmdOk;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel label1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
namespace SPNATI_Character_Editor.Forms
{
	partial class GameConfig
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
			this.chkDebug = new Desktop.Skinning.SkinnedCheckBox();
			this.chkEpilogues = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.chkCollectibles = new Desktop.Skinning.SkinnedCheckBox();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkDebug
			// 
			this.chkDebug.AutoSize = true;
			this.chkDebug.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkDebug.Location = new System.Drawing.Point(12, 42);
			this.chkDebug.Name = "chkDebug";
			this.chkDebug.Size = new System.Drawing.Size(405, 17);
			this.chkDebug.TabIndex = 0;
			this.chkDebug.Text = "Debug Mode (press Q in-game to display buttons for forcing an opponent to lose)";
			this.chkDebug.UseVisualStyleBackColor = true;
			// 
			// chkEpilogues
			// 
			this.chkEpilogues.AutoSize = true;
			this.chkEpilogues.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkEpilogues.Location = new System.Drawing.Point(12, 65);
			this.chkEpilogues.Name = "chkEpilogues";
			this.chkEpilogues.Size = new System.Drawing.Size(305, 17);
			this.chkEpilogues.TabIndex = 1;
			this.chkEpilogues.Text = "Unlock Epilogues (allows testing epilogues from the Gallery)";
			this.chkEpilogues.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(269, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
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
			this.cmdCancel.Location = new System.Drawing.Point(350, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// chkCollectibles
			// 
			this.chkCollectibles.AutoSize = true;
			this.chkCollectibles.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkCollectibles.Location = new System.Drawing.Point(12, 88);
			this.chkCollectibles.Name = "chkCollectibles";
			this.chkCollectibles.Size = new System.Drawing.Size(312, 17);
			this.chkCollectibles.TabIndex = 4;
			this.chkCollectibles.Text = "Unlock Collectibles (allows viewing collectibles in the Gallery)";
			this.chkCollectibles.UseVisualStyleBackColor = true;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 163);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(428, 30);
			this.skinnedPanel1.TabIndex = 5;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// GameConfig
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(428, 193);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.chkCollectibles);
			this.Controls.Add(this.chkEpilogues);
			this.Controls.Add(this.chkDebug);
			this.Name = "GameConfig";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Game Configuration";
			this.Load += new System.EventHandler(this.GameConfig_Load);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedCheckBox chkDebug;
		private Desktop.Skinning.SkinnedCheckBox chkEpilogues;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedCheckBox chkCollectibles;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
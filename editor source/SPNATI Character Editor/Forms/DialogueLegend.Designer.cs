namespace SPNATI_Character_Editor.Forms
{
	partial class DialogueLegend
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
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.lblBlue = new System.Windows.Forms.Label();
			this.lblOrange = new System.Windows.Forms.Label();
			this.lblGray = new System.Windows.Forms.Label();
			this.lblLightGray = new System.Windows.Forms.Label();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel2 = new Desktop.Skinning.SkinnedPanel();
			this.lblGreen = new System.Windows.Forms.Label();
			this.skinnedPanel1.SuspendLayout();
			this.skinnedPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(206, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			// 
			// lblBlue
			// 
			this.lblBlue.AutoSize = true;
			this.lblBlue.ForeColor = System.Drawing.Color.Blue;
			this.lblBlue.Location = new System.Drawing.Point(3, 3);
			this.lblBlue.Name = "lblBlue";
			this.lblBlue.Size = new System.Drawing.Size(113, 13);
			this.lblBlue.TabIndex = 43;
			this.lblBlue.Text = "Contains Default Lines";
			// 
			// lblOrange
			// 
			this.lblOrange.AutoSize = true;
			this.lblOrange.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblOrange.Location = new System.Drawing.Point(3, 36);
			this.lblOrange.Name = "lblOrange";
			this.lblOrange.Size = new System.Drawing.Size(106, 13);
			this.lblOrange.TabIndex = 44;
			this.lblOrange.Text = "Unlocks a Collectible";
			// 
			// lblGray
			// 
			this.lblGray.AutoSize = true;
			this.lblGray.ForeColor = System.Drawing.Color.Gray;
			this.lblGray.Location = new System.Drawing.Point(3, 53);
			this.lblGray.Name = "lblGray";
			this.lblGray.Size = new System.Drawing.Size(88, 13);
			this.lblGray.TabIndex = 45;
			this.lblGray.Text = "Hidden Condition";
			// 
			// lblLightGray
			// 
			this.lblLightGray.AutoSize = true;
			this.lblLightGray.ForeColor = System.Drawing.Color.Silver;
			this.lblLightGray.Location = new System.Drawing.Point(3, 69);
			this.lblLightGray.Name = "lblLightGray";
			this.lblLightGray.Size = new System.Drawing.Size(97, 13);
			this.lblLightGray.TabIndex = 46;
			this.lblLightGray.Text = "Hidden From Editor";
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.skinnedPanel1.Controls.Add(this.lblGreen);
			this.skinnedPanel1.Controls.Add(this.lblLightGray);
			this.skinnedPanel1.Controls.Add(this.lblBlue);
			this.skinnedPanel1.Controls.Add(this.lblGray);
			this.skinnedPanel1.Controls.Add(this.lblOrange);
			this.skinnedPanel1.Location = new System.Drawing.Point(12, 41);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedPanel1.Size = new System.Drawing.Size(260, 97);
			this.skinnedPanel1.TabIndex = 47;
			// 
			// skinnedPanel2
			// 
			this.skinnedPanel2.Controls.Add(this.cmdOK);
			this.skinnedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel2.Location = new System.Drawing.Point(0, 144);
			this.skinnedPanel2.Name = "skinnedPanel2";
			this.skinnedPanel2.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel2.Size = new System.Drawing.Size(284, 30);
			this.skinnedPanel2.TabIndex = 48;
			// 
			// lblGreen
			// 
			this.lblGreen.AutoSize = true;
			this.lblGreen.ForeColor = System.Drawing.Color.Green;
			this.lblGreen.Location = new System.Drawing.Point(3, 19);
			this.lblGreen.Name = "lblGreen";
			this.lblGreen.Size = new System.Drawing.Size(95, 13);
			this.lblGreen.TabIndex = 47;
			this.lblGreen.Text = "Targeted Dialogue";
			// 
			// DialogueLegend
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.cmdOK;
			this.ClientSize = new System.Drawing.Size(284, 174);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel2);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "DialogueLegend";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Legend";
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.skinnedPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedButton cmdOK;
		private System.Windows.Forms.Label lblBlue;
		private System.Windows.Forms.Label lblOrange;
		private System.Windows.Forms.Label lblGray;
		private System.Windows.Forms.Label lblLightGray;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel2;
		private System.Windows.Forms.Label lblGreen;
	}
}
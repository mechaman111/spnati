namespace SPNATI_Character_Editor.Forms
{
	partial class CallOutForm
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
			this.cmdCreate = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.radPri1 = new Desktop.Skinning.SkinnedRadioButton();
			this.radPri2 = new Desktop.Skinning.SkinnedRadioButton();
			this.radPri3 = new Desktop.Skinning.SkinnedRadioButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1.SuspendLayout();
			this.skinnedGroupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdCreate);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 187);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(578, 30);
			this.skinnedPanel1.TabIndex = 0;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdCreate
			// 
			this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCreate.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCreate.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCreate.Flat = false;
			this.cmdCreate.Location = new System.Drawing.Point(410, 3);
			this.cmdCreate.Name = "cmdCreate";
			this.cmdCreate.Size = new System.Drawing.Size(75, 23);
			this.cmdCreate.TabIndex = 0;
			this.cmdCreate.Text = "Create";
			this.cmdCreate.UseVisualStyleBackColor = true;
			this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(491, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox1.Controls.Add(this.skinnedLabel3);
			this.skinnedGroupBox1.Controls.Add(this.skinnedLabel2);
			this.skinnedGroupBox1.Controls.Add(this.skinnedLabel1);
			this.skinnedGroupBox1.Controls.Add(this.radPri3);
			this.skinnedGroupBox1.Controls.Add(this.radPri2);
			this.skinnedGroupBox1.Controls.Add(this.radPri1);
			this.skinnedGroupBox1.Location = new System.Drawing.Point(12, 36);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.Size = new System.Drawing.Size(558, 143);
			this.skinnedGroupBox1.TabIndex = 1;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Priority";
			// 
			// radPri1
			// 
			this.radPri1.AutoSize = true;
			this.radPri1.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radPri1.Location = new System.Drawing.Point(6, 29);
			this.radPri1.Name = "radPri1";
			this.radPri1.Size = new System.Drawing.Size(358, 17);
			this.radPri1.TabIndex = 0;
			this.radPri1.Text = "Must Target - Characters will look silly if they don\'t react to this situation";
			this.radPri1.UseVisualStyleBackColor = true;
			// 
			// radPri2
			// 
			this.radPri2.AutoSize = true;
			this.radPri2.Checked = true;
			this.radPri2.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radPri2.Location = new System.Drawing.Point(6, 65);
			this.radPri2.Name = "radPri2";
			this.radPri2.Size = new System.Drawing.Size(359, 17);
			this.radPri2.TabIndex = 1;
			this.radPri2.TabStop = true;
			this.radPri2.Text = "Noteworthy - Something worth reacting to but can still safely be ignored";
			this.radPri2.UseVisualStyleBackColor = true;
			// 
			// radPri3
			// 
			this.radPri3.AutoSize = true;
			this.radPri3.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radPri3.Location = new System.Drawing.Point(6, 101);
			this.radPri3.Name = "radPri3";
			this.radPri3.Size = new System.Drawing.Size(544, 17);
			this.radPri3.TabIndex = 2;
			this.radPri3.Text = "FYI - Situation is worth mentioning but other characters should focus on reacting" +
    " to higher priority situations first\r\n";
			this.radPri3.UseVisualStyleBackColor = true;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel1.Location = new System.Drawing.Point(44, 49);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(235, 13);
			this.skinnedLabel1.TabIndex = 3;
			this.skinnedLabel1.Text = "Example: Florina runs out of the room after losing";
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel2.Location = new System.Drawing.Point(44, 85);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(307, 13);
			this.skinnedLabel2.TabIndex = 4;
			this.skinnedLabel2.Text = "Example: Sei offers to buy a round of drinks for the other players";
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel3.Location = new System.Drawing.Point(44, 121);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(350, 13);
			this.skinnedLabel3.TabIndex = 5;
			this.skinnedLabel3.Text = "Example: Natsuki\'s shirt doesn\'t cover her panties after removing her skirt";
			// 
			// CallOutForm
			// 
			this.AcceptButton = this.cmdCreate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(578, 217);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedGroupBox1);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "CallOutForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Call Out New Situation";
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdCreate;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedRadioButton radPri3;
		private Desktop.Skinning.SkinnedRadioButton radPri2;
		private Desktop.Skinning.SkinnedRadioButton radPri1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
	}
}
namespace SPNATI_Character_Editor.Forms
{
	partial class AddPoseForm
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
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdCreate = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdCreate);
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 67);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(271, 30);
			this.skinnedPanel1.TabIndex = 0;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(193, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdCreate
			// 
			this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCreate.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCreate.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCreate.Flat = false;
			this.cmdCreate.Location = new System.Drawing.Point(112, 3);
			this.cmdCreate.Name = "cmdCreate";
			this.cmdCreate.Size = new System.Drawing.Size(75, 23);
			this.cmdCreate.TabIndex = 0;
			this.cmdCreate.Text = "Add";
			this.cmdCreate.UseVisualStyleBackColor = true;
			this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 39);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(38, 13);
			this.skinnedLabel1.TabIndex = 1;
			this.skinnedLabel1.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(57, 36);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(100, 20);
			this.txtName.TabIndex = 2;
			// 
			// AddPoseForm
			// 
			this.AcceptButton = this.cmdCreate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(271, 97);
			this.ControlBox = false;
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddPoseForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Sizable = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Pose";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdCreate;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedTextBox txtName;
	}
}
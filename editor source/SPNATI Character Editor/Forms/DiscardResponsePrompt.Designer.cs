namespace SPNATI_Character_Editor.Forms
{
	partial class DiscardResponsePrompt
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
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.cmdAccept = new Desktop.Skinning.SkinnedButton();
			this.cmdDiscard = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdDiscard);
			this.skinnedPanel1.Controls.Add(this.cmdAccept);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 63);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(466, 30);
			this.skinnedPanel1.TabIndex = 0;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 39);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(246, 13);
			this.skinnedLabel1.TabIndex = 1;
			this.skinnedLabel1.Text = "Do you want to save this response before leaving?";
			// 
			// cmdAccept
			// 
			this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAccept.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAccept.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdAccept.Flat = false;
			this.cmdAccept.Location = new System.Drawing.Point(124, 3);
			this.cmdAccept.Name = "cmdAccept";
			this.cmdAccept.Size = new System.Drawing.Size(106, 23);
			this.cmdAccept.TabIndex = 0;
			this.cmdAccept.Text = "Save && Close";
			this.cmdAccept.UseVisualStyleBackColor = true;
			this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
			// 
			// cmdDiscard
			// 
			this.cmdDiscard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDiscard.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdDiscard.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdDiscard.Flat = false;
			this.cmdDiscard.Location = new System.Drawing.Point(236, 3);
			this.cmdDiscard.Name = "cmdDiscard";
			this.cmdDiscard.Size = new System.Drawing.Size(106, 23);
			this.cmdDiscard.TabIndex = 1;
			this.cmdDiscard.Text = "Discard";
			this.cmdDiscard.UseVisualStyleBackColor = true;
			this.cmdDiscard.Click += new System.EventHandler(this.cmdDiscard_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(348, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(106, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Go Back";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// DiscardResponsePrompt
			// 
			this.AcceptButton = this.cmdAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(466, 93);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "DiscardResponsePrompt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Save Response";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedButton cmdDiscard;
		private Desktop.Skinning.SkinnedButton cmdAccept;
		private Desktop.Skinning.SkinnedButton cmdCancel;
	}
}
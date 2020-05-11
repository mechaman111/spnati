namespace SPNATI_Character_Editor.Forms
{
	partial class StageImageSelection
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
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.pnlImages = new Desktop.CommonControls.DBPanel();
			this.gridImages = new SPNATI_Character_Editor.Controls.StageImageGrid();
			this.skinnedPanel1.SuspendLayout();
			this.pnlImages.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(418, 4);
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
			this.cmdCancel.Location = new System.Drawing.Point(499, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 529);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(577, 30);
			this.skinnedPanel1.TabIndex = 4;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// pnlImages
			// 
			this.pnlImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlImages.AutoScroll = true;
			this.pnlImages.Controls.Add(this.gridImages);
			this.pnlImages.Location = new System.Drawing.Point(1, 27);
			this.pnlImages.Margin = new System.Windows.Forms.Padding(0);
			this.pnlImages.Name = "pnlImages";
			this.pnlImages.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.pnlImages.Size = new System.Drawing.Size(575, 500);
			this.pnlImages.TabIndex = 5;
			this.pnlImages.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// gridImages
			// 
			this.gridImages.AutoSize = true;
			this.gridImages.ColumnHeaderHeight = 72;
			this.gridImages.Location = new System.Drawing.Point(0, 0);
			this.gridImages.Name = "gridImages";
			this.gridImages.RowHeaderWidth = 110;
			this.gridImages.Size = new System.Drawing.Size(575, 500);
			this.gridImages.TabIndex = 4;
			// 
			// StageImageSelection
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(577, 559);
			this.ControlBox = false;
			this.Controls.Add(this.pnlImages);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "StageImageSelection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Images Per Stage";
			this.skinnedPanel1.ResumeLayout(false);
			this.pnlImages.ResumeLayout(false);
			this.pnlImages.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.CommonControls.DBPanel pnlImages;
		private Controls.StageImageGrid gridImages;
	}
}
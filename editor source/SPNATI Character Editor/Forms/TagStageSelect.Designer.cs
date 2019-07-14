namespace SPNATI_Character_Editor.Forms
{
	partial class TagStageSelect
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.lblTag = new Desktop.Skinning.SkinnedLabel();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.lstItems = new Desktop.Skinning.SkinnedListView();
			this.cmdSelectAll = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel2 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel3 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.skinnedPanel2.SuspendLayout();
			this.skinnedPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.PrimaryLight;
			this.label1.Location = new System.Drawing.Point(8, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tag applies to:";
			// 
			// lblTag
			// 
			this.lblTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTag.Level = Desktop.Skinning.SkinnedLabelLevel.Title;
			this.lblTag.Location = new System.Drawing.Point(12, 2);
			this.lblTag.Name = "lblTag";
			this.lblTag.Size = new System.Drawing.Size(258, 30);
			this.lblTag.TabIndex = 1;
			this.lblTag.Text = "Tag [Tag]";
			this.lblTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.Blue;
			this.cmdOK.Location = new System.Drawing.Point(125, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 3;
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
			this.cmdCancel.ForeColor = System.Drawing.Color.Blue;
			this.cmdCancel.Location = new System.Drawing.Point(206, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// lstItems
			// 
			this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstItems.CheckBoxes = true;
			this.lstItems.Location = new System.Drawing.Point(12, 90);
			this.lstItems.Name = "lstItems";
			this.lstItems.OwnerDraw = true;
			this.lstItems.Size = new System.Drawing.Size(260, 126);
			this.lstItems.TabIndex = 5;
			this.lstItems.UseCompatibleStateImageBehavior = false;
			this.lstItems.View = System.Windows.Forms.View.List;
			// 
			// cmdSelectAll
			// 
			this.cmdSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSelectAll.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdSelectAll.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdSelectAll.Flat = false;
			this.cmdSelectAll.Location = new System.Drawing.Point(192, 2);
			this.cmdSelectAll.Name = "cmdSelectAll";
			this.cmdSelectAll.Size = new System.Drawing.Size(88, 23);
			this.cmdSelectAll.TabIndex = 6;
			this.cmdSelectAll.Text = "Select All";
			this.cmdSelectAll.UseVisualStyleBackColor = true;
			this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 227);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(284, 30);
			this.skinnedPanel1.TabIndex = 7;
			// 
			// skinnedPanel2
			// 
			this.skinnedPanel2.Controls.Add(this.lblTag);
			this.skinnedPanel2.Location = new System.Drawing.Point(1, 27);
			this.skinnedPanel2.Name = "skinnedPanel2";
			this.skinnedPanel2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedPanel2.Size = new System.Drawing.Size(282, 32);
			this.skinnedPanel2.TabIndex = 8;
			// 
			// skinnedPanel3
			// 
			this.skinnedPanel3.Controls.Add(this.cmdSelectAll);
			this.skinnedPanel3.Controls.Add(this.label1);
			this.skinnedPanel3.Location = new System.Drawing.Point(1, 59);
			this.skinnedPanel3.Name = "skinnedPanel3";
			this.skinnedPanel3.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.skinnedPanel3.Size = new System.Drawing.Size(283, 27);
			this.skinnedPanel3.TabIndex = 9;
			// 
			// TagStageSelect
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(284, 257);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel3);
			this.Controls.Add(this.skinnedPanel2);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.lstItems);
			this.Name = "TagStageSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Stages";
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel2.ResumeLayout(false);
			this.skinnedPanel3.ResumeLayout(false);
			this.skinnedPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel lblTag;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedListView lstItems;
		private Desktop.Skinning.SkinnedButton cmdSelectAll;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel2;
		private Desktop.Skinning.SkinnedPanel skinnedPanel3;
	}
}
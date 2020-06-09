namespace Desktop
{
	partial class RecordLookup
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
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.cmdNew = new Desktop.Skinning.SkinnedButton();
			this.cmdAccept = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.lstItems = new Desktop.Skinning.SkinnedListView();
			this.cmdDelete = new Desktop.Skinning.SkinnedButton();
			this.lblRecent = new Desktop.Skinning.SkinnedLabel();
			this.lstRecent = new Desktop.Skinning.SkinnedListView();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label1.Location = new System.Drawing.Point(9, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(57, 31);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(592, 20);
			this.txtName.TabIndex = 1;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
			// 
			// cmdNew
			// 
			this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNew.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdNew.Enabled = false;
			this.cmdNew.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdNew.Flat = true;
			this.cmdNew.ForeColor = System.Drawing.Color.White;
			this.cmdNew.Location = new System.Drawing.Point(355, 3);
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Size = new System.Drawing.Size(97, 23);
			this.cmdNew.TabIndex = 23;
			this.cmdNew.Text = "Create New";
			this.cmdNew.UseVisualStyleBackColor = true;
			this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
			// 
			// cmdAccept
			// 
			this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAccept.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAccept.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdAccept.Flat = false;
			this.cmdAccept.ForeColor = System.Drawing.Color.Blue;
			this.cmdAccept.Location = new System.Drawing.Point(458, 3);
			this.cmdAccept.Name = "cmdAccept";
			this.cmdAccept.Size = new System.Drawing.Size(97, 23);
			this.cmdAccept.TabIndex = 24;
			this.cmdAccept.Text = "Accept";
			this.cmdAccept.UseVisualStyleBackColor = true;
			this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(561, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(97, 23);
			this.cmdCancel.TabIndex = 25;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// lstItems
			// 
			this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstItems.FullRowSelect = true;
			this.lstItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstItems.HideSelection = false;
			this.lstItems.Location = new System.Drawing.Point(12, 57);
			this.lstItems.MultiSelect = false;
			this.lstItems.Name = "lstItems";
			this.lstItems.OwnerDraw = true;
			this.lstItems.ShowItemToolTips = true;
			this.lstItems.Size = new System.Drawing.Size(637, 199);
			this.lstItems.TabIndex = 20;
			this.lstItems.UseCompatibleStateImageBehavior = false;
			this.lstItems.View = System.Windows.Forms.View.Tile;
			this.lstItems.DoubleClick += new System.EventHandler(this.lstItems_DoubleClick);
			// 
			// cmdDelete
			// 
			this.cmdDelete.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdDelete.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdDelete.Flat = true;
			this.cmdDelete.ForeColor = System.Drawing.Color.White;
			this.cmdDelete.Location = new System.Drawing.Point(3, 3);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(97, 23);
			this.cmdDelete.TabIndex = 22;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.cmdDelete.Visible = false;
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// lblRecent
			// 
			this.lblRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRecent.AutoSize = true;
			this.lblRecent.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblRecent.ForeColor = System.Drawing.Color.Blue;
			this.lblRecent.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblRecent.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblRecent.Location = new System.Drawing.Point(12, 261);
			this.lblRecent.Name = "lblRecent";
			this.lblRecent.Size = new System.Drawing.Size(57, 21);
			this.lblRecent.TabIndex = 8;
			this.lblRecent.Text = "Recent";
			// 
			// lstRecent
			// 
			this.lstRecent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstRecent.FullRowSelect = true;
			this.lstRecent.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstRecent.HideSelection = false;
			this.lstRecent.Location = new System.Drawing.Point(12, 285);
			this.lstRecent.MultiSelect = false;
			this.lstRecent.Name = "lstRecent";
			this.lstRecent.OwnerDraw = true;
			this.lstRecent.Size = new System.Drawing.Size(637, 71);
			this.lstRecent.TabIndex = 21;
			this.lstRecent.UseCompatibleStateImageBehavior = false;
			this.lstRecent.View = System.Windows.Forms.View.Tile;
			this.lstRecent.SelectedIndexChanged += new System.EventHandler(this.lstRecent_SelectedIndexChanged);
			this.lstRecent.DoubleClick += new System.EventHandler(this.lstRecent_DoubleClick);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdAccept);
			this.skinnedPanel1.Controls.Add(this.cmdNew);
			this.skinnedPanel1.Controls.Add(this.cmdDelete);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 362);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(661, 30);
			this.skinnedPanel1.TabIndex = 10;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// RecordLookup
			// 
			this.AcceptButton = this.cmdAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(661, 392);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.lstRecent);
			this.Controls.Add(this.lblRecent);
			this.Controls.Add(this.lstItems);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Name = "RecordLookup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Record Lookup";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.RecordLookup_Shown);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedTextBox txtName;
		private Desktop.Skinning.SkinnedButton cmdNew;
		private Desktop.Skinning.SkinnedButton cmdAccept;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedListView lstItems;
		private Desktop.Skinning.SkinnedButton cmdDelete;
		private Desktop.Skinning.SkinnedLabel lblRecent;
		private Desktop.Skinning.SkinnedListView lstRecent;
		private Skinning.SkinnedPanel skinnedPanel1;
	}
}
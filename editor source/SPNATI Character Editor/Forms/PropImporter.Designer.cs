namespace SPNATI_Character_Editor
{
	partial class PropImporter
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
			this.cmdOK = new System.Windows.Forms.Button();
			this.gridMissingImages = new System.Windows.Forms.DataGridView();
			this.ColImageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColImage = new System.Windows.Forms.DataGridViewImageColumn();
			this.ColAssign = new System.Windows.Forms.DataGridViewButtonColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.lblReady = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridMissingImages)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Enabled = false;
			this.cmdOK.Location = new System.Drawing.Point(187, 533);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 1;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// gridMissingImages
			// 
			this.gridMissingImages.AllowUserToAddRows = false;
			this.gridMissingImages.AllowUserToDeleteRows = false;
			this.gridMissingImages.AllowUserToResizeColumns = false;
			this.gridMissingImages.AllowUserToResizeRows = false;
			this.gridMissingImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMissingImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridMissingImages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColImageName,
            this.ColImage,
            this.ColAssign});
			this.gridMissingImages.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridMissingImages.Location = new System.Drawing.Point(12, 76);
			this.gridMissingImages.Name = "gridMissingImages";
			this.gridMissingImages.RowHeadersVisible = false;
			this.gridMissingImages.RowTemplate.Height = 100;
			this.gridMissingImages.Size = new System.Drawing.Size(331, 406);
			this.gridMissingImages.TabIndex = 0;
			this.gridMissingImages.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMissingImages_CellContentClick);
			// 
			// ColImageName
			// 
			this.ColImageName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColImageName.HeaderText = "Name";
			this.ColImageName.Name = "ColImageName";
			this.ColImageName.ReadOnly = true;
			// 
			// ColImage
			// 
			this.ColImage.HeaderText = "Image";
			this.ColImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
			this.ColImage.Name = "ColImage";
			this.ColImage.ReadOnly = true;
			// 
			// ColAssign
			// 
			this.ColAssign.HeaderText = "Source";
			this.ColAssign.Name = "ColAssign";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "We Need Your Help!";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(9, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(334, 44);
			this.label2.TabIndex = 3;
			this.label2.Text = "A pose uses one or more external images, which the editor can\'t import without kn" +
    "owing where they came from. Please pull your source images into the table below." +
    "";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(12, 485);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(331, 45);
			this.label3.TabIndex = 4;
			this.label3.Text = "Note: The editor will copy these images into your character\'s \"attachments\" subfo" +
    "lder, so if you edit your images, you will need to edit the copy to reflect the " +
    "changes in later imports.";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// lblReady
			// 
			this.lblReady.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblReady.ForeColor = System.Drawing.Color.Green;
			this.lblReady.Location = new System.Drawing.Point(12, 9);
			this.lblReady.Name = "lblReady";
			this.lblReady.Size = new System.Drawing.Size(331, 64);
			this.lblReady.TabIndex = 5;
			this.lblReady.Text = "Good to Go!";
			this.lblReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblReady.Visible = false;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(268, 533);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// PropImporter
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(355, 568);
			this.ControlBox = false;
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.lblReady);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMissingImages);
			this.Controls.Add(this.cmdOK);
			this.Name = "PropImporter";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import Images";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropImporter_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PropImporter_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.gridMissingImages)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.DataGridView gridMissingImages;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColImageName;
		private System.Windows.Forms.DataGridViewImageColumn ColImage;
		private System.Windows.Forms.DataGridViewButtonColumn ColAssign;
		private System.Windows.Forms.Label lblReady;
		private System.Windows.Forms.Button cmdCancel;
	}
}
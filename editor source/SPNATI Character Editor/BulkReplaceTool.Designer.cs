namespace SPNATI_Character_Editor
{
	partial class BulkReplaceTool
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.gridDestinations = new System.Windows.Forms.DataGridView();
			this.cboSource = new System.Windows.Forms.ComboBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.ColTag = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridDestinations)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Source Case:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(272, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Cases to Replace:";
			// 
			// gridDestinations
			// 
			this.gridDestinations.AllowUserToResizeColumns = false;
			this.gridDestinations.AllowUserToResizeRows = false;
			this.gridDestinations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDestinations.ColumnHeadersVisible = false;
			this.gridDestinations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTag});
			this.gridDestinations.Location = new System.Drawing.Point(275, 34);
			this.gridDestinations.Name = "gridDestinations";
			this.gridDestinations.RowHeadersVisible = false;
			this.gridDestinations.Size = new System.Drawing.Size(225, 141);
			this.gridDestinations.TabIndex = 1;
			// 
			// cboSource
			// 
			this.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSource.FormattingEnabled = true;
			this.cboSource.Location = new System.Drawing.Point(16, 34);
			this.cboSource.Name = "cboSource";
			this.cboSource.Size = new System.Drawing.Size(212, 21);
			this.cboSource.TabIndex = 0;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(344, 181);
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
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(425, 181);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// ColTag
			// 
			this.ColTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTag.HeaderText = "Tag";
			this.ColTag.Name = "ColTag";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(239, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "→";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(13, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(215, 117);
			this.label4.TabIndex = 5;
			this.label4.Text = "This will replace all non-targeted dialogue across all stages with the dialogue f" +
    "rom the source case type.";
			// 
			// BulkReplaceTool
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(512, 216);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cboSource);
			this.Controls.Add(this.gridDestinations);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "BulkReplaceTool";
			this.Text = "Bulk Replace Tool";
			((System.ComponentModel.ISupportInitialize)(this.gridDestinations)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView gridDestinations;
		private System.Windows.Forms.ComboBox cboSource;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColTag;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}
namespace Desktop.Reporting.Controls
{
	partial class SlicerGroupEntry
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblEntry = new System.Windows.Forms.Label();
			this.cmdGroup = new Desktop.Skinning.SkinnedIcon();
			this.txtGroupName = new Desktop.Skinning.SkinnedTextBox();
			this.cmdDelete = new Desktop.Skinning.SkinnedIcon();
			this.chkActive = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdEdit = new Desktop.Skinning.SkinnedIcon();
			this.SuspendLayout();
			// 
			// lblEntry
			// 
			this.lblEntry.AutoSize = true;
			this.lblEntry.Location = new System.Drawing.Point(44, 4);
			this.lblEntry.Name = "lblEntry";
			this.lblEntry.Size = new System.Drawing.Size(31, 13);
			this.lblEntry.TabIndex = 1;
			this.lblEntry.Text = "Entry";
			this.lblEntry.TextChanged += new System.EventHandler(this.lblEntry_TextChanged);
			// 
			// cmdGroup
			// 
			this.cmdGroup.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmdGroup.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdGroup.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdGroup.Flat = false;
			this.cmdGroup.Image = global::Desktop.Properties.Resources.Ungrouped;
			this.cmdGroup.Location = new System.Drawing.Point(19, 3);
			this.cmdGroup.Name = "cmdGroup";
			this.cmdGroup.Size = new System.Drawing.Size(16, 16);
			this.cmdGroup.TabIndex = 1;
			this.cmdGroup.UseVisualStyleBackColor = true;
			this.cmdGroup.Click += new System.EventHandler(this.cmdGroup_Click);
			// 
			// txtGroupName
			// 
			this.txtGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGroupName.BackColor = System.Drawing.Color.White;
			this.txtGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtGroupName.ForeColor = System.Drawing.Color.Black;
			this.txtGroupName.Location = new System.Drawing.Point(41, 1);
			this.txtGroupName.Name = "txtGroupName";
			this.txtGroupName.Size = new System.Drawing.Size(204, 20);
			this.txtGroupName.TabIndex = 2;
			this.txtGroupName.Visible = false;
			this.txtGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroupName_KeyDown);
			this.txtGroupName.Leave += new System.EventHandler(this.txtGroupName_Leave);
			this.txtGroupName.Validated += new System.EventHandler(this.txtGroupName_Validated);
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdDelete.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdDelete.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdDelete.Flat = false;
			this.cmdDelete.Image = global::Desktop.Properties.Resources.Delete;
			this.cmdDelete.Location = new System.Drawing.Point(251, 3);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(16, 16);
			this.cmdDelete.TabIndex = 4;
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// chkActive
			// 
			this.chkActive.AutoSize = true;
			this.chkActive.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkActive.Location = new System.Drawing.Point(0, 4);
			this.chkActive.Name = "chkActive";
			this.chkActive.Size = new System.Drawing.Size(15, 14);
			this.chkActive.TabIndex = 0;
			this.chkActive.UseVisualStyleBackColor = true;
			this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
			// 
			// cmdEdit
			// 
			this.cmdEdit.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdEdit.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdEdit.Flat = false;
			this.cmdEdit.Image = global::Desktop.Properties.Resources.Pencil;
			this.cmdEdit.Location = new System.Drawing.Point(57, 3);
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Size = new System.Drawing.Size(16, 16);
			this.cmdEdit.TabIndex = 3;
			this.cmdEdit.Text = "Edit";
			this.cmdEdit.UseVisualStyleBackColor = true;
			this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
			// 
			// SlicerGroupEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdGroup);
			this.Controls.Add(this.txtGroupName);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.lblEntry);
			this.Controls.Add(this.chkActive);
			this.Controls.Add(this.cmdEdit);
			this.Name = "SlicerGroupEntry";
			this.Size = new System.Drawing.Size(270, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Skinning.SkinnedCheckBox chkActive;
		private System.Windows.Forms.Label lblEntry;
		private Skinning.SkinnedIcon cmdDelete;
		private Skinning.SkinnedTextBox txtGroupName;
		private Skinning.SkinnedIcon cmdEdit;
		private Skinning.SkinnedIcon cmdGroup;
	}
}

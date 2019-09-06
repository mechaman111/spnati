namespace SPNATI_Character_Editor.Controls
{
	partial class StageImageControl
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
			this.cboImage = new Desktop.Skinning.SkinnedComboBox();
			this.grpImage = new Desktop.Skinning.SkinnedGroupBox();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.cmdDelete = new Desktop.Skinning.SkinnedIcon();
			this.gridStages = new SPNATI_Character_Editor.Controls.StageGrid();
			this.grpImage.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboImage
			// 
			this.cboImage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboImage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboImage.BackColor = System.Drawing.Color.White;
			this.cboImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboImage.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboImage.Location = new System.Drawing.Point(51, 5);
			this.cboImage.Name = "cboImage";
			this.cboImage.SelectedIndex = -1;
			this.cboImage.SelectedItem = null;
			this.cboImage.Size = new System.Drawing.Size(163, 23);
			this.cboImage.Sorted = false;
			this.cboImage.TabIndex = 0;
			this.cboImage.Text = "skinnedComboBox1";
			this.cboImage.SelectedIndexChanged += new System.EventHandler(this.cboImage_SelectedIndexChanged);
			// 
			// grpImage
			// 
			this.grpImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpImage.BackColor = System.Drawing.Color.White;
			this.grpImage.Controls.Add(this.skinnedLabel1);
			this.grpImage.Controls.Add(this.cmdDelete);
			this.grpImage.Controls.Add(this.cboImage);
			this.grpImage.Controls.Add(this.gridStages);
			this.grpImage.Location = new System.Drawing.Point(0, 0);
			this.grpImage.Margin = new System.Windows.Forms.Padding(0);
			this.grpImage.Name = "grpImage";
			this.grpImage.Size = new System.Drawing.Size(452, 143);
			this.grpImage.TabIndex = 2;
			this.grpImage.TabStop = false;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(6, 10);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(34, 13);
			this.skinnedLabel1.TabIndex = 3;
			this.skinnedLabel1.Text = "Pose:";
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDelete.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdDelete.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdDelete.Flat = false;
			this.cmdDelete.Image = global::SPNATI_Character_Editor.Properties.Resources.Delete;
			this.cmdDelete.Location = new System.Drawing.Point(423, 5);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(23, 23);
			this.cmdDelete.TabIndex = 2;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// gridStages
			// 
			this.gridStages.AutoSize = true;
			this.gridStages.ColumnHeaderHeight = 72;
			this.gridStages.Location = new System.Drawing.Point(6, 34);
			this.gridStages.Name = "gridStages";
			this.gridStages.ShowSelectAll = false;
			this.gridStages.Size = new System.Drawing.Size(120, 102);
			this.gridStages.TabIndex = 1;
			// 
			// StageImageControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpImage);
			this.Name = "StageImageControl";
			this.Size = new System.Drawing.Size(452, 147);
			this.grpImage.ResumeLayout(false);
			this.grpImage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboImage;
		private StageGrid gridStages;
		private Desktop.Skinning.SkinnedGroupBox grpImage;
		private Desktop.Skinning.SkinnedIcon cmdDelete;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
	}
}

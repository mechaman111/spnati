namespace SPNATI_Character_Editor.Forms
{
	partial class AddSheetForm
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
			this.components = new System.ComponentModel.Container();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdCreate = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.lblSheet = new Desktop.Skinning.SkinnedLabel();
			this.cboSheet = new Desktop.Skinning.SkinnedComboBox();
			this.chkGlobal = new Desktop.Skinning.SkinnedCheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdCreate);
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 113);
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
			this.txtName.Location = new System.Drawing.Point(73, 36);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(132, 20);
			this.txtName.TabIndex = 2;
			// 
			// lblSheet
			// 
			this.lblSheet.AutoSize = true;
			this.lblSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblSheet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblSheet.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblSheet.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.lblSheet.Location = new System.Drawing.Point(12, 66);
			this.lblSheet.Name = "lblSheet";
			this.lblSheet.Size = new System.Drawing.Size(57, 13);
			this.lblSheet.TabIndex = 3;
			this.lblSheet.Text = "Copy from:";
			this.lblSheet.Visible = false;
			// 
			// cboSheet
			// 
			this.cboSheet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboSheet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboSheet.BackColor = System.Drawing.Color.White;
			this.cboSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSheet.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboSheet.KeyMember = null;
			this.cboSheet.Location = new System.Drawing.Point(73, 62);
			this.cboSheet.Name = "cboSheet";
			this.cboSheet.SelectedIndex = -1;
			this.cboSheet.SelectedItem = null;
			this.cboSheet.Size = new System.Drawing.Size(132, 23);
			this.cboSheet.Sorted = false;
			this.cboSheet.TabIndex = 4;
			this.cboSheet.Visible = false;
			this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.cboSheet_SelectedIndexChanged);
			// 
			// chkGlobal
			// 
			this.chkGlobal.AutoSize = true;
			this.chkGlobal.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkGlobal.Location = new System.Drawing.Point(12, 91);
			this.chkGlobal.Name = "chkGlobal";
			this.chkGlobal.Size = new System.Drawing.Size(116, 17);
			this.chkGlobal.TabIndex = 5;
			this.chkGlobal.Text = "Stage-independent";
			this.toolTip1.SetToolTip(this.chkGlobal, "If checked, poses for each stage cannot be defined. Images will be saved without " +
        "a stage prefix and can be used in any stage.");
			this.chkGlobal.UseVisualStyleBackColor = true;
			this.chkGlobal.Visible = false;
			// 
			// AddSheetForm
			// 
			this.AcceptButton = this.cmdCreate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(271, 142);
			this.ControlBox = false;
			this.Controls.Add(this.chkGlobal);
			this.Controls.Add(this.cboSheet);
			this.Controls.Add(this.lblSheet);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddSheetForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Sizable = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Sheet";
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
		private Desktop.Skinning.SkinnedLabel lblSheet;
		private Desktop.Skinning.SkinnedComboBox cboSheet;
		private Desktop.Skinning.SkinnedCheckBox chkGlobal;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
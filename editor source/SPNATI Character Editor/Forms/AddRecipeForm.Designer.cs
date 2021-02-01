namespace SPNATI_Character_Editor.Forms
{
	partial class AddRecipeForm
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
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.txtDescription = new Desktop.Skinning.SkinnedTextBox();
			this.cmdEdit = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdEdit);
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdCreate);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 145);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(405, 30);
			this.skinnedPanel1.TabIndex = 1;
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
			this.cmdCancel.Location = new System.Drawing.Point(327, 3);
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
			this.cmdCreate.Location = new System.Drawing.Point(246, 3);
			this.cmdCreate.Name = "cmdCreate";
			this.cmdCreate.Size = new System.Drawing.Size(75, 23);
			this.cmdCreate.TabIndex = 0;
			this.cmdCreate.Text = "Create";
			this.cmdCreate.UseVisualStyleBackColor = true;
			this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(80, 33);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(318, 20);
			this.txtName.TabIndex = 25;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(4, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 24;
			this.label1.Text = "Name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(4, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 26;
			this.label2.Text = "Description:";
			// 
			// txtDescription
			// 
			this.txtDescription.BackColor = System.Drawing.Color.White;
			this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtDescription.ForeColor = System.Drawing.Color.Black;
			this.txtDescription.Location = new System.Drawing.Point(80, 59);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(318, 79);
			this.txtDescription.TabIndex = 29;
			// 
			// cmdEdit
			// 
			this.cmdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdEdit.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdEdit.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdEdit.Flat = false;
			this.cmdEdit.ForeColor = System.Drawing.Color.Red;
			this.cmdEdit.Location = new System.Drawing.Point(147, 3);
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Size = new System.Drawing.Size(93, 23);
			this.cmdEdit.TabIndex = 2;
			this.cmdEdit.Text = "Edit Recipe";
			this.cmdEdit.UseVisualStyleBackColor = true;
			this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
			// 
			// AddRecipeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(405, 175);
			this.ControlBox = false;
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "AddRecipeForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "That\'s it! I\'ve come up with a new recipe.";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdCreate;
		private Desktop.Skinning.SkinnedButton cmdEdit;
		private Desktop.Skinning.SkinnedTextBox txtName;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedTextBox txtDescription;
	}
}
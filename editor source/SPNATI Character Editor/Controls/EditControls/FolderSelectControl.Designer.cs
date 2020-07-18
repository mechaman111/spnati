namespace SPNATI_Character_Editor.Controls
{
	partial class FolderSelectControl
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
			this.txtValue = new Desktop.CommonControls.TextField();
			this.cmdBrowse = new Desktop.Skinning.SkinnedButton();
			this.characterFolderDialog1 = new SPNATI_Character_Editor.Controls.CharacterFolderDialog();
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.txtValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.txtValue.Location = new System.Drawing.Point(0, 0);
			this.txtValue.Multiline = false;
			this.txtValue.Name = "txtValue";
			this.txtValue.PlaceholderText = "";
			this.txtValue.ReadOnly = true;
			this.txtValue.Size = new System.Drawing.Size(318, 20);
			this.txtValue.TabIndex = 0;
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdBrowse.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowse.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cmdBrowse.Flat = false;
			this.cmdBrowse.ForeColor = System.Drawing.Color.Blue;
			this.cmdBrowse.Location = new System.Drawing.Point(324, -1);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(27, 22);
			this.cmdBrowse.TabIndex = 1;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.CmdBrowse_Click);
			// 
			// FolderSelectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtValue);
			this.Name = "FolderSelectControl";
			this.Size = new System.Drawing.Size(351, 20);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.TextField txtValue;
		private Desktop.Skinning.SkinnedButton cmdBrowse;
		private CharacterFolderDialog characterFolderDialog1;
	}
}

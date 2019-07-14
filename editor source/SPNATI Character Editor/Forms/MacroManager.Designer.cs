namespace SPNATI_Character_Editor.Forms
{
	partial class MacroManager
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
			this.lstMacros = new Desktop.Skinning.SkinnedListBox();
			this.cmdDelete = new Desktop.Skinning.SkinnedButton();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdEdit = new Desktop.Skinning.SkinnedButton();
			this.cmdNew = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstMacros
			// 
			this.lstMacros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstMacros.BackColor = System.Drawing.Color.White;
			this.lstMacros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstMacros.ForeColor = System.Drawing.Color.Black;
			this.lstMacros.FormattingEnabled = true;
			this.lstMacros.Location = new System.Drawing.Point(12, 33);
			this.lstMacros.Name = "lstMacros";
			this.lstMacros.Size = new System.Drawing.Size(169, 251);
			this.lstMacros.TabIndex = 0;
			this.lstMacros.DoubleClick += new System.EventHandler(this.lstMacros_DoubleClick);
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDelete.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdDelete.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdDelete.Flat = false;
			this.cmdDelete.Location = new System.Drawing.Point(187, 91);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(66, 23);
			this.cmdDelete.TabIndex = 2;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(187, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdEdit
			// 
			this.cmdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdEdit.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdEdit.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdEdit.Flat = false;
			this.cmdEdit.Location = new System.Drawing.Point(187, 33);
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.Size = new System.Drawing.Size(66, 23);
			this.cmdEdit.TabIndex = 1;
			this.cmdEdit.Text = "Edit";
			this.cmdEdit.UseVisualStyleBackColor = true;
			this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
			// 
			// cmdNew
			// 
			this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNew.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdNew.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdNew.Flat = false;
			this.cmdNew.Location = new System.Drawing.Point(187, 62);
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.Size = new System.Drawing.Size(66, 23);
			this.cmdNew.TabIndex = 6;
			this.cmdNew.Text = "New";
			this.cmdNew.UseVisualStyleBackColor = true;
			this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 289);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(265, 30);
			this.skinnedPanel1.TabIndex = 7;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// MacroManager
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(265, 319);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.cmdNew);
			this.Controls.Add(this.cmdEdit);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.lstMacros);
			this.Name = "MacroManager";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Macro Manager";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedListBox lstMacros;
		private Desktop.Skinning.SkinnedButton cmdDelete;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdEdit;
		private Desktop.Skinning.SkinnedButton cmdNew;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
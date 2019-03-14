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
			this.lstMacros = new System.Windows.Forms.ListBox();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstMacros
			// 
			this.lstMacros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstMacros.FormattingEnabled = true;
			this.lstMacros.Location = new System.Drawing.Point(12, 12);
			this.lstMacros.Name = "lstMacros";
			this.lstMacros.Size = new System.Drawing.Size(178, 238);
			this.lstMacros.TabIndex = 0;
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDelete.Location = new System.Drawing.Point(196, 12);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(57, 23);
			this.cmdDelete.TabIndex = 1;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(178, 255);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// MacroManager
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(265, 290);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.lstMacros);
			this.Name = "MacroManager";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Macro Manager";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lstMacros;
		private System.Windows.Forms.Button cmdDelete;
		private System.Windows.Forms.Button cmdOK;
	}
}
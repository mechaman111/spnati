namespace Desktop.Forms
{
	partial class MacroHelp
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
			this.lblHelp = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblHelp
			// 
			this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHelp.Location = new System.Drawing.Point(12, 9);
			this.lblHelp.Name = "lblHelp";
			this.lblHelp.Size = new System.Drawing.Size(260, 144);
			this.lblHelp.TabIndex = 0;
			this.lblHelp.Text = "Macros are shortcuts for pulling in properties.";
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(197, 156);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 1;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// MacroHelp
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 191);
			this.ControlBox = false;
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.lblHelp);
			this.Name = "MacroHelp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Macro Help";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblHelp;
		private System.Windows.Forms.Button cmdOK;
	}
}
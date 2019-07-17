namespace SPNATI_Character_Editor
{
	partial class HelpForm
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
			this.wb = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// wb
			// 
			this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wb.Location = new System.Drawing.Point(1, 28);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(1223, 673);
			this.wb.TabIndex = 0;
			this.wb.Url = new System.Uri("", System.UriKind.Relative);
			// 
			// HelpForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1226, 702);
			this.Controls.Add(this.wb);
			this.Name = "HelpForm";
			this.ShowIcon = false;
			this.Text = "Help";
			this.Load += new System.EventHandler(this.HelpForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser wb;
	}
}
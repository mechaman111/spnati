namespace SPNATI_Character_Editor.Controls
{
	partial class IntellisenseControl
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
			this.lstItems = new Desktop.Skinning.SkinnedListBox();
			this.lblTooltip = new Desktop.Skinning.SkinnedLinkLabel();
			this.SuspendLayout();
			// 
			// lstItems
			// 
			this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstItems.FormattingEnabled = true;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Margin = new System.Windows.Forms.Padding(0);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(230, 43);
			this.lstItems.TabIndex = 1;
			this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
			this.lstItems.DoubleClick += new System.EventHandler(this.lstItems_DoubleClick);
			// 
			// lblTooltip
			// 
			this.lblTooltip.ActiveLinkColor = System.Drawing.Color.Black;
			this.lblTooltip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTooltip.LinkColor = System.Drawing.Color.Black;
			this.lblTooltip.Location = new System.Drawing.Point(0, 43);
			this.lblTooltip.Name = "lblTooltip";
			this.lblTooltip.Size = new System.Drawing.Size(227, 39);
			this.lblTooltip.TabIndex = 2;
			this.lblTooltip.TabStop = true;
			this.lblTooltip.Text = "Tooltip";
			// 
			// IntellisenseControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lblTooltip);
			this.Controls.Add(this.lstItems);
			this.Name = "IntellisenseControl";
			this.Size = new System.Drawing.Size(230, 82);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedListBox lstItems;
		private Desktop.Skinning.SkinnedLinkLabel lblTooltip;
	}
}

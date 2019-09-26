namespace Desktop.CommonControls
{
	partial class ActivityLink
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
			this.lblLink = new Desktop.Skinning.SkinnedLinkLabel();
			this.cmdGo = new Desktop.Skinning.SkinnedIcon();
			this.SuspendLayout();
			// 
			// lblLink
			// 
			this.lblLink.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblLink.ForeColor = System.Drawing.Color.Black;
			this.lblLink.LinkColor = System.Drawing.Color.Blue;
			this.lblLink.Location = new System.Drawing.Point(0, 5);
			this.lblLink.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.lblLink.Name = "lblLink";
			this.lblLink.Size = new System.Drawing.Size(21, 13);
			this.lblLink.TabIndex = 5;
			this.lblLink.TabStop = true;
			this.lblLink.Text = "Go";
			this.lblLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLink_LinkClicked);
			// 
			// cmdGo
			// 
			this.cmdGo.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdGo.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdGo.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdGo.Flat = false;
			this.cmdGo.Image = global::Desktop.Properties.Resources.Expand;
			this.cmdGo.Location = new System.Drawing.Point(17, 3);
			this.cmdGo.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.cmdGo.Name = "cmdGo";
			this.cmdGo.Size = new System.Drawing.Size(16, 17);
			this.cmdGo.TabIndex = 3;
			this.cmdGo.Text = "skinnedIcon1";
			this.cmdGo.UseVisualStyleBackColor = true;
			this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
			// 
			// ActivityLink
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblLink);
			this.Controls.Add(this.cmdGo);
			this.Name = "ActivityLink";
			this.Size = new System.Drawing.Size(33, 24);
			this.ResumeLayout(false);

		}

		#endregion

		private Skinning.SkinnedIcon cmdGo;
		private Skinning.SkinnedLinkLabel lblLink;
	}
}

namespace Desktop
{
	partial class ToastControl
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
			this.grpBubble = new Desktop.Skinning.SkinnedGroupBox();
			this.lblText = new Desktop.Skinning.SkinnedLabel();
			this.cmdDismiss = new Desktop.Skinning.SkinnedButton();
			this.grpBubble.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBubble
			// 
			this.grpBubble.BackColor = System.Drawing.Color.White;
			this.grpBubble.Controls.Add(this.lblText);
			this.grpBubble.Controls.Add(this.cmdDismiss);
			this.grpBubble.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpBubble.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpBubble.Image = null;
			this.grpBubble.Location = new System.Drawing.Point(0, 0);
			this.grpBubble.Margin = new System.Windows.Forms.Padding(0);
			this.grpBubble.Name = "grpBubble";
			this.grpBubble.Size = new System.Drawing.Size(452, 90);
			this.grpBubble.TabIndex = 0;
			this.grpBubble.TabStop = false;
			this.grpBubble.Text = "Caption";
			// 
			// lblText
			// 
			this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblText.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblText.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblText.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblText.Location = new System.Drawing.Point(6, 29);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(440, 58);
			this.lblText.TabIndex = 1;
			this.lblText.Text = "This is a piece of toast.";
			// 
			// cmdDismiss
			// 
			this.cmdDismiss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDismiss.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdDismiss.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdDismiss.Flat = true;
			this.cmdDismiss.ForeColor = System.Drawing.Color.Blue;
			this.cmdDismiss.Location = new System.Drawing.Point(380, 3);
			this.cmdDismiss.Name = "cmdDismiss";
			this.cmdDismiss.Size = new System.Drawing.Size(69, 20);
			this.cmdDismiss.TabIndex = 0;
			this.cmdDismiss.Text = "Dismiss";
			this.cmdDismiss.UseVisualStyleBackColor = true;
			this.cmdDismiss.Click += new System.EventHandler(this.cmdDismiss_Click);
			// 
			// ToastControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpBubble);
			this.Name = "ToastControl";
			this.Size = new System.Drawing.Size(452, 90);
			this.grpBubble.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Skinning.SkinnedGroupBox grpBubble;
		private Skinning.SkinnedButton cmdDismiss;
		private Skinning.SkinnedLabel lblText;
	}
}

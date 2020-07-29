namespace SPNATI_Character_Editor.Forms
{
	partial class PoseSettingsForm
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
			this.panelHead = new System.Windows.Forms.FlowLayoutPanel();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.panelBody = new System.Windows.Forms.FlowLayoutPanel();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.panelClothing = new System.Windows.Forms.FlowLayoutPanel();
			this.chkManual = new Desktop.Skinning.SkinnedCheckBox();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelHead
			// 
			this.panelHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelHead.AutoScroll = true;
			this.panelHead.Location = new System.Drawing.Point(12, 60);
			this.panelHead.Name = "panelHead";
			this.panelHead.Size = new System.Drawing.Size(634, 154);
			this.panelHead.TabIndex = 0;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(499, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 1;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(580, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label1.Location = new System.Drawing.Point(9, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 21);
			this.label1.TabIndex = 3;
			this.label1.Text = "Head";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label2.Location = new System.Drawing.Point(9, 210);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 21);
			this.label2.TabIndex = 5;
			this.label2.Text = "Body";
			// 
			// panelBody
			// 
			this.panelBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelBody.AutoScroll = true;
			this.panelBody.Location = new System.Drawing.Point(12, 226);
			this.panelBody.Name = "panelBody";
			this.panelBody.Size = new System.Drawing.Size(634, 255);
			this.panelBody.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label3.Location = new System.Drawing.Point(9, 479);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 21);
			this.label3.TabIndex = 7;
			this.label3.Text = "Clothing";
			// 
			// panelClothing
			// 
			this.panelClothing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelClothing.AutoScroll = true;
			this.panelClothing.Location = new System.Drawing.Point(12, 495);
			this.panelClothing.Name = "panelClothing";
			this.panelClothing.Size = new System.Drawing.Size(634, 267);
			this.panelClothing.TabIndex = 6;
			// 
			// chkManual
			// 
			this.chkManual.AutoSize = true;
			this.chkManual.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkManual.Location = new System.Drawing.Point(12, 32);
			this.chkManual.Name = "chkManual";
			this.chkManual.Size = new System.Drawing.Size(485, 17);
			this.chkManual.TabIndex = 8;
			this.chkManual.Text = "Import code exactly as is with no editor pre-processing (setting up zoom, centeri" +
    "ng character, etc.)";
			this.chkManual.UseVisualStyleBackColor = true;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 768);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(658, 30);
			this.skinnedPanel1.TabIndex = 9;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// PoseSettingsForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(658, 798);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.chkManual);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panelHead);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.panelBody);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.panelClothing);
			this.Name = "PoseSettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Pose Settings";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel panelHead;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel label2;
		private System.Windows.Forms.FlowLayoutPanel panelBody;
		private Desktop.Skinning.SkinnedLabel label3;
		private System.Windows.Forms.FlowLayoutPanel panelClothing;
		private Desktop.Skinning.SkinnedCheckBox chkManual;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
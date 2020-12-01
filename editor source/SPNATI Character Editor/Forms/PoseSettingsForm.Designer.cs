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
			this.cmdHeadOff = new Desktop.Skinning.SkinnedButton();
			this.cmdHeadOn = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.cmdBodyOn = new Desktop.Skinning.SkinnedButton();
			this.cmdBodyOff = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.cmdClothesOn = new Desktop.Skinning.SkinnedButton();
			this.cmdClothesOff = new Desktop.Skinning.SkinnedButton();
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
			// cmdHeadOff
			// 
			this.cmdHeadOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdHeadOff.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdHeadOff.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdHeadOff.Flat = true;
			this.cmdHeadOff.ForeColor = System.Drawing.Color.Blue;
			this.cmdHeadOff.Location = new System.Drawing.Point(540, 45);
			this.cmdHeadOff.Name = "cmdHeadOff";
			this.cmdHeadOff.Size = new System.Drawing.Size(50, 23);
			this.cmdHeadOff.TabIndex = 10;
			this.cmdHeadOff.Text = "Off";
			this.cmdHeadOff.UseVisualStyleBackColor = true;
			this.cmdHeadOff.Click += new System.EventHandler(this.cmdHeadOff_Click);
			// 
			// cmdHeadOn
			// 
			this.cmdHeadOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdHeadOn.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdHeadOn.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdHeadOn.Flat = true;
			this.cmdHeadOn.ForeColor = System.Drawing.Color.Blue;
			this.cmdHeadOn.Location = new System.Drawing.Point(596, 45);
			this.cmdHeadOn.Name = "cmdHeadOn";
			this.cmdHeadOn.Size = new System.Drawing.Size(50, 23);
			this.cmdHeadOn.TabIndex = 11;
			this.cmdHeadOn.Text = "On";
			this.cmdHeadOn.UseVisualStyleBackColor = true;
			this.cmdHeadOn.Click += new System.EventHandler(this.cmdHeadOn_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(513, 50);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(21, 13);
			this.skinnedLabel1.TabIndex = 12;
			this.skinnedLabel1.Text = "All:";
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(513, 216);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(21, 13);
			this.skinnedLabel2.TabIndex = 15;
			this.skinnedLabel2.Text = "All:";
			// 
			// cmdBodyOn
			// 
			this.cmdBodyOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBodyOn.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBodyOn.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBodyOn.Flat = true;
			this.cmdBodyOn.ForeColor = System.Drawing.Color.Blue;
			this.cmdBodyOn.Location = new System.Drawing.Point(596, 211);
			this.cmdBodyOn.Name = "cmdBodyOn";
			this.cmdBodyOn.Size = new System.Drawing.Size(50, 23);
			this.cmdBodyOn.TabIndex = 14;
			this.cmdBodyOn.Text = "On";
			this.cmdBodyOn.UseVisualStyleBackColor = true;
			this.cmdBodyOn.Click += new System.EventHandler(this.cmdBodyOn_Click);
			// 
			// cmdBodyOff
			// 
			this.cmdBodyOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBodyOff.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBodyOff.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBodyOff.Flat = true;
			this.cmdBodyOff.ForeColor = System.Drawing.Color.Blue;
			this.cmdBodyOff.Location = new System.Drawing.Point(540, 211);
			this.cmdBodyOff.Name = "cmdBodyOff";
			this.cmdBodyOff.Size = new System.Drawing.Size(50, 23);
			this.cmdBodyOff.TabIndex = 13;
			this.cmdBodyOff.Text = "Off";
			this.cmdBodyOff.UseVisualStyleBackColor = true;
			this.cmdBodyOff.Click += new System.EventHandler(this.cmdBodyOff_Click);
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(513, 484);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(21, 13);
			this.skinnedLabel3.TabIndex = 18;
			this.skinnedLabel3.Text = "All:";
			// 
			// cmdClothesOn
			// 
			this.cmdClothesOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClothesOn.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdClothesOn.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdClothesOn.Flat = true;
			this.cmdClothesOn.ForeColor = System.Drawing.Color.Blue;
			this.cmdClothesOn.Location = new System.Drawing.Point(596, 479);
			this.cmdClothesOn.Name = "cmdClothesOn";
			this.cmdClothesOn.Size = new System.Drawing.Size(50, 23);
			this.cmdClothesOn.TabIndex = 17;
			this.cmdClothesOn.Text = "On";
			this.cmdClothesOn.UseVisualStyleBackColor = true;
			this.cmdClothesOn.Click += new System.EventHandler(this.cmdClothesOn_Click);
			// 
			// cmdClothesOff
			// 
			this.cmdClothesOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClothesOff.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdClothesOff.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdClothesOff.Flat = true;
			this.cmdClothesOff.ForeColor = System.Drawing.Color.Blue;
			this.cmdClothesOff.Location = new System.Drawing.Point(540, 479);
			this.cmdClothesOff.Name = "cmdClothesOff";
			this.cmdClothesOff.Size = new System.Drawing.Size(50, 23);
			this.cmdClothesOff.TabIndex = 16;
			this.cmdClothesOff.Text = "Off";
			this.cmdClothesOff.UseVisualStyleBackColor = true;
			this.cmdClothesOff.Click += new System.EventHandler(this.cmdClothesOff_Click);
			// 
			// PoseSettingsForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(658, 798);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedLabel3);
			this.Controls.Add(this.cmdClothesOn);
			this.Controls.Add(this.cmdClothesOff);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.cmdBodyOn);
			this.Controls.Add(this.cmdBodyOff);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.cmdHeadOn);
			this.Controls.Add(this.cmdHeadOff);
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
		private Desktop.Skinning.SkinnedButton cmdHeadOff;
		private Desktop.Skinning.SkinnedButton cmdHeadOn;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedButton cmdBodyOn;
		private Desktop.Skinning.SkinnedButton cmdBodyOff;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
		private Desktop.Skinning.SkinnedButton cmdClothesOn;
		private Desktop.Skinning.SkinnedButton cmdClothesOff;
	}
}
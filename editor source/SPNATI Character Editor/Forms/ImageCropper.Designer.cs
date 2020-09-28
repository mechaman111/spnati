namespace SPNATI_Character_Editor.Forms
{
	partial class ImageCropper
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageCropper));
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.label7 = new Desktop.Skinning.SkinnedLabel();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.tmrWait = new System.Windows.Forms.Timer(this.components);
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.panelManual = new Desktop.Skinning.SkinnedPanel();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.cmdReimport = new Desktop.Skinning.SkinnedButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.label8 = new Desktop.Skinning.SkinnedLabel();
			this.label9 = new Desktop.Skinning.SkinnedLabel();
			this.label10 = new Desktop.Skinning.SkinnedLabel();
			this.valLeft = new Desktop.Skinning.SkinnedNumericUpDown();
			this.valTop = new Desktop.Skinning.SkinnedNumericUpDown();
			this.valBottom = new Desktop.Skinning.SkinnedNumericUpDown();
			this.valRight = new Desktop.Skinning.SkinnedNumericUpDown();
			this.chkNoCrop = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdAdvanced = new Desktop.Skinning.SkinnedButton();
			this.previewPanel = new Desktop.CommonControls.DBPanel();
			this.lblWait = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdCopy = new Desktop.Skinning.SkinnedButton();
			this.panelManual.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valBottom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valRight)).BeginInit();
			this.previewPanel.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(829, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(748, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "Crop";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label7.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label7.Location = new System.Drawing.Point(698, 73);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(207, 13);
			this.label7.TabIndex = 22;
			this.label7.Text = "Left Mouse Button (inside box) - Move Box";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label6.Location = new System.Drawing.Point(698, 60);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(195, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "Right Mouse Button (edge) - Mirror Drag";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(698, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Left Mouse Button (edge) - Drag Edge";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(684, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "Cropping Box Instructions:";
			// 
			// tmrWait
			// 
			this.tmrWait.Interval = 250;
			this.tmrWait.Tick += new System.EventHandler(this.tmrWait_Tick);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.MediumBlue;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(3, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "Uh...";
			// 
			// panelManual
			// 
			this.panelManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.panelManual.Controls.Add(this.label4);
			this.panelManual.Controls.Add(this.label3);
			this.panelManual.Location = new System.Drawing.Point(687, 304);
			this.panelManual.Name = "panelManual";
			this.panelManual.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.panelManual.Size = new System.Drawing.Size(210, 171);
			this.panelManual.TabIndex = 24;
			this.panelManual.TabSide = Desktop.Skinning.TabSide.None;
			this.panelManual.Visible = false;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label4.Location = new System.Drawing.Point(11, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(187, 125);
			this.label4.TabIndex = 24;
			this.label4.Text = resources.GetString("label4.Text");
			// 
			// cmdReimport
			// 
			this.cmdReimport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdReimport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdReimport.Flat = false;
			this.cmdReimport.Location = new System.Drawing.Point(687, 511);
			this.cmdReimport.Name = "cmdReimport";
			this.cmdReimport.Size = new System.Drawing.Size(210, 23);
			this.cmdReimport.TabIndex = 26;
			this.cmdReimport.Text = "Reimport";
			this.toolTip1.SetToolTip(this.cmdReimport, "Recapture whatever is currently visible in Kisekae");
			this.cmdReimport.UseVisualStyleBackColor = true;
			this.cmdReimport.Click += new System.EventHandler(this.cmdReimport_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(685, 110);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(28, 13);
			this.label5.TabIndex = 27;
			this.label5.Text = "Left:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label8.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label8.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label8.Location = new System.Drawing.Point(788, 110);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(29, 13);
			this.label8.TabIndex = 28;
			this.label8.Text = "Top:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label9.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label9.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label9.Location = new System.Drawing.Point(685, 136);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(43, 13);
			this.label9.TabIndex = 29;
			this.label9.Text = "Bottom:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label10.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label10.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label10.Location = new System.Drawing.Point(788, 136);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(35, 13);
			this.label10.TabIndex = 30;
			this.label10.Text = "Right:";
			// 
			// valLeft
			// 
			this.valLeft.BackColor = System.Drawing.Color.White;
			this.valLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valLeft.ForeColor = System.Drawing.Color.Black;
			this.valLeft.Location = new System.Drawing.Point(737, 108);
			this.valLeft.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.valLeft.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.valLeft.Name = "valLeft";
			this.valLeft.Size = new System.Drawing.Size(45, 20);
			this.valLeft.TabIndex = 31;
			this.valLeft.ValueChanged += new System.EventHandler(this.CropValueChanged);
			// 
			// valTop
			// 
			this.valTop.BackColor = System.Drawing.Color.White;
			this.valTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valTop.ForeColor = System.Drawing.Color.Black;
			this.valTop.Location = new System.Drawing.Point(829, 108);
			this.valTop.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.valTop.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.valTop.Name = "valTop";
			this.valTop.Size = new System.Drawing.Size(45, 20);
			this.valTop.TabIndex = 32;
			this.valTop.ValueChanged += new System.EventHandler(this.CropValueChanged);
			// 
			// valBottom
			// 
			this.valBottom.BackColor = System.Drawing.Color.White;
			this.valBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valBottom.ForeColor = System.Drawing.Color.Black;
			this.valBottom.Location = new System.Drawing.Point(737, 134);
			this.valBottom.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.valBottom.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.valBottom.Name = "valBottom";
			this.valBottom.Size = new System.Drawing.Size(45, 20);
			this.valBottom.TabIndex = 33;
			this.valBottom.ValueChanged += new System.EventHandler(this.CropValueChanged);
			// 
			// valRight
			// 
			this.valRight.BackColor = System.Drawing.Color.White;
			this.valRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valRight.ForeColor = System.Drawing.Color.Black;
			this.valRight.Location = new System.Drawing.Point(829, 134);
			this.valRight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.valRight.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.valRight.Name = "valRight";
			this.valRight.Size = new System.Drawing.Size(45, 20);
			this.valRight.TabIndex = 34;
			this.valRight.ValueChanged += new System.EventHandler(this.CropValueChanged);
			// 
			// chkNoCrop
			// 
			this.chkNoCrop.AutoSize = true;
			this.chkNoCrop.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkNoCrop.Location = new System.Drawing.Point(688, 160);
			this.chkNoCrop.Name = "chkNoCrop";
			this.chkNoCrop.Size = new System.Drawing.Size(84, 17);
			this.chkNoCrop.TabIndex = 35;
			this.chkNoCrop.Text = "No cropping";
			this.chkNoCrop.UseVisualStyleBackColor = true;
			this.chkNoCrop.CheckedChanged += new System.EventHandler(this.chkNoCrop_CheckedChanged);
			// 
			// cmdAdvanced
			// 
			this.cmdAdvanced.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAdvanced.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdAdvanced.Flat = false;
			this.cmdAdvanced.Location = new System.Drawing.Point(687, 483);
			this.cmdAdvanced.Name = "cmdAdvanced";
			this.cmdAdvanced.Size = new System.Drawing.Size(210, 23);
			this.cmdAdvanced.TabIndex = 36;
			this.cmdAdvanced.Text = "Adjust Part Transparencies";
			this.cmdAdvanced.UseVisualStyleBackColor = true;
			this.cmdAdvanced.Click += new System.EventHandler(this.cmdAdvanced_Click);
			// 
			// previewPanel
			// 
			this.previewPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.previewPanel.Controls.Add(this.lblWait);
			this.previewPanel.Location = new System.Drawing.Point(12, 33);
			this.previewPanel.Name = "previewPanel";
			this.previewPanel.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.previewPanel.Size = new System.Drawing.Size(666, 500);
			this.previewPanel.TabIndex = 3;
			this.previewPanel.TabSide = Desktop.Skinning.TabSide.None;
			this.previewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPanel_Paint);
			this.previewPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseDown);
			this.previewPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseMove);
			this.previewPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseUp);
			// 
			// lblWait
			// 
			this.lblWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblWait.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblWait.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblWait.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblWait.Location = new System.Drawing.Point(3, 237);
			this.lblWait.Name = "lblWait";
			this.lblWait.Size = new System.Drawing.Size(660, 13);
			this.lblWait.TabIndex = 0;
			this.lblWait.Text = "Please wait";
			this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCopy);
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 542);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(909, 30);
			this.skinnedPanel1.TabIndex = 37;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdCopy
			// 
			this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCopy.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCopy.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCopy.Flat = true;
			this.cmdCopy.ForeColor = System.Drawing.Color.White;
			this.cmdCopy.Location = new System.Drawing.Point(3, 3);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new System.Drawing.Size(187, 23);
			this.cmdCopy.TabIndex = 6;
			this.cmdCopy.Text = "Copy Generated Code";
			this.cmdCopy.UseVisualStyleBackColor = true;
			this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
			// 
			// ImageCropper
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(909, 572);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.cmdAdvanced);
			this.Controls.Add(this.chkNoCrop);
			this.Controls.Add(this.valRight);
			this.Controls.Add(this.valBottom);
			this.Controls.Add(this.valTop);
			this.Controls.Add(this.valLeft);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmdReimport);
			this.Controls.Add(this.panelManual);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.previewPanel);
			this.Name = "ImageCropper";
			this.Text = "Cropping Utility";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageCropper_FormClosed);
			this.panelManual.ResumeLayout(false);
			this.panelManual.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valBottom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valRight)).EndInit();
			this.previewPanel.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedLabel label7;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel lblWait;
		private System.Windows.Forms.Timer tmrWait;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedPanel panelManual;
		private Desktop.Skinning.SkinnedButton cmdReimport;
		private Desktop.Skinning.SkinnedLabel label4;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedLabel label8;
		private Desktop.Skinning.SkinnedLabel label9;
		private Desktop.Skinning.SkinnedLabel label10;
		private Desktop.Skinning.SkinnedNumericUpDown valLeft;
		private Desktop.Skinning.SkinnedNumericUpDown valTop;
		private Desktop.Skinning.SkinnedNumericUpDown valBottom;
		private Desktop.Skinning.SkinnedNumericUpDown valRight;
		private Desktop.Skinning.SkinnedCheckBox chkNoCrop;
		private Desktop.Skinning.SkinnedButton cmdAdvanced;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.CommonControls.DBPanel previewPanel;
		private Desktop.Skinning.SkinnedButton cmdCopy;
	}
}
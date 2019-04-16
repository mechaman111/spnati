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
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.previewPanel = new Desktop.CommonControls.DBPanel();
			this.lblWait = new System.Windows.Forms.Label();
			this.tmrWait = new System.Windows.Forms.Timer(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.panelManual = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.cmdReimport = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.valLeft = new System.Windows.Forms.NumericUpDown();
			this.valTop = new System.Windows.Forms.NumericUpDown();
			this.valBottom = new System.Windows.Forms.NumericUpDown();
			this.valRight = new System.Windows.Forms.NumericUpDown();
			this.chkNoCrop = new System.Windows.Forms.CheckBox();
			this.cmdAdvanced = new System.Windows.Forms.Button();
			this.previewPanel.SuspendLayout();
			this.panelManual.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valBottom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valRight)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(603, 517);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(522, 517);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(698, 51);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(207, 13);
			this.label7.TabIndex = 22;
			this.label7.Text = "Left Mouse Button (inside box) - Move Box";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(698, 38);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(195, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "Right Mouse Button (edge) - Mirror Drag";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(698, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Left Mouse Button (edge) - Drag Edge";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(684, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "Cropping Box Instructions:";
			// 
			// previewPanel
			// 
			this.previewPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.previewPanel.Controls.Add(this.lblWait);
			this.previewPanel.Location = new System.Drawing.Point(12, 12);
			this.previewPanel.Name = "previewPanel";
			this.previewPanel.Size = new System.Drawing.Size(666, 500);
			this.previewPanel.TabIndex = 3;
			this.previewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPanel_Paint);
			this.previewPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseDown);
			this.previewPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseMove);
			this.previewPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.previewPanel_MouseUp);
			// 
			// lblWait
			// 
			this.lblWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblWait.Location = new System.Drawing.Point(3, 237);
			this.lblWait.Name = "lblWait";
			this.lblWait.Size = new System.Drawing.Size(660, 13);
			this.lblWait.TabIndex = 0;
			this.lblWait.Text = "Please wait";
			this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tmrWait
			// 
			this.tmrWait.Interval = 250;
			this.tmrWait.Tick += new System.EventHandler(this.tmrWait_Tick);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.MediumBlue;
			this.label3.Location = new System.Drawing.Point(3, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 31);
			this.label3.TabIndex = 23;
			this.label3.Text = "Uh...";
			// 
			// panelManual
			// 
			this.panelManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.panelManual.Controls.Add(this.label4);
			this.panelManual.Controls.Add(this.label3);
			this.panelManual.Location = new System.Drawing.Point(687, 284);
			this.panelManual.Name = "panelManual";
			this.panelManual.Size = new System.Drawing.Size(210, 171);
			this.panelManual.TabIndex = 24;
			this.panelManual.Visible = false;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(11, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(187, 125);
			this.label4.TabIndex = 24;
			this.label4.Text = resources.GetString("label4.Text");
			// 
			// cmdReimport
			// 
			this.cmdReimport.Location = new System.Drawing.Point(696, 489);
			this.cmdReimport.Name = "cmdReimport";
			this.cmdReimport.Size = new System.Drawing.Size(189, 23);
			this.cmdReimport.TabIndex = 26;
			this.cmdReimport.Text = "Reimport";
			this.toolTip1.SetToolTip(this.cmdReimport, "Recapture whatever is currently visible in Kisekae");
			this.cmdReimport.UseVisualStyleBackColor = true;
			this.cmdReimport.Click += new System.EventHandler(this.cmdReimport_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(685, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(28, 13);
			this.label5.TabIndex = 27;
			this.label5.Text = "Left:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(788, 88);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(29, 13);
			this.label8.TabIndex = 28;
			this.label8.Text = "Top:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(685, 114);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(43, 13);
			this.label9.TabIndex = 29;
			this.label9.Text = "Bottom:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(788, 114);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(35, 13);
			this.label10.TabIndex = 30;
			this.label10.Text = "Right:";
			// 
			// valLeft
			// 
			this.valLeft.Location = new System.Drawing.Point(737, 86);
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
			this.valTop.Location = new System.Drawing.Point(829, 86);
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
			this.valBottom.Location = new System.Drawing.Point(737, 112);
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
			this.valRight.Location = new System.Drawing.Point(829, 112);
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
			this.chkNoCrop.Location = new System.Drawing.Point(688, 138);
			this.chkNoCrop.Name = "chkNoCrop";
			this.chkNoCrop.Size = new System.Drawing.Size(84, 17);
			this.chkNoCrop.TabIndex = 35;
			this.chkNoCrop.Text = "No cropping";
			this.chkNoCrop.UseVisualStyleBackColor = true;
			this.chkNoCrop.CheckedChanged += new System.EventHandler(this.chkNoCrop_CheckedChanged);
			// 
			// cmdAdvanced
			// 
			this.cmdAdvanced.Location = new System.Drawing.Point(696, 461);
			this.cmdAdvanced.Name = "cmdAdvanced";
			this.cmdAdvanced.Size = new System.Drawing.Size(189, 23);
			this.cmdAdvanced.TabIndex = 36;
			this.cmdAdvanced.Text = "Adjust Part Transparencies...";
			this.cmdAdvanced.UseVisualStyleBackColor = true;
			this.cmdAdvanced.Click += new System.EventHandler(this.cmdAdvanced_Click);
			// 
			// ImageCropper
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(909, 552);
			this.ControlBox = false;
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
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.previewPanel);
			this.Name = "ImageCropper";
			this.Text = "Cropping Utility";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageCropper_FormClosed);
			this.previewPanel.ResumeLayout(false);
			this.panelManual.ResumeLayout(false);
			this.panelManual.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valBottom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valRight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private Desktop.CommonControls.DBPanel previewPanel;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblWait;
		private System.Windows.Forms.Timer tmrWait;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panelManual;
		private System.Windows.Forms.Button cmdReimport;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown valLeft;
		private System.Windows.Forms.NumericUpDown valTop;
		private System.Windows.Forms.NumericUpDown valBottom;
		private System.Windows.Forms.NumericUpDown valRight;
		private System.Windows.Forms.CheckBox chkNoCrop;
		private System.Windows.Forms.Button cmdAdvanced;
	}
}
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
			this.cmdReimport = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.previewPanel.SuspendLayout();
			this.panelManual.SuspendLayout();
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
			this.label3.Location = new System.Drawing.Point(3, 0);
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
			this.panelManual.Location = new System.Drawing.Point(687, 312);
			this.panelManual.Name = "panelManual";
			this.panelManual.Size = new System.Drawing.Size(210, 171);
			this.panelManual.TabIndex = 24;
			this.panelManual.Visible = false;
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
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(11, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(187, 125);
			this.label4.TabIndex = 24;
			this.label4.Text = resources.GetString("label4.Text");
			// 
			// ImageCropper
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(909, 552);
			this.ControlBox = false;
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
	}
}
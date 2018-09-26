namespace SPNATI_Character_Editor
{
	partial class AdvancedEpilogueConditions
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
			this.valMaxPlayerStartingLayers = new System.Windows.Forms.NumericUpDown();
			this.label52 = new System.Windows.Forms.Label();
			this.valPlayerStartingLayers = new System.Windows.Forms.NumericUpDown();
			this.lblPlayerStartingLayers = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblAlsoPlaying = new System.Windows.Forms.Label();
			this.cboAlsoPlaying = new System.Windows.Forms.ComboBox();
			this.GalleryImageSelectBtn = new System.Windows.Forms.Button();
			this.valGalleryImage = new System.Windows.Forms.TextBox();
			this.galleryImageFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label7 = new System.Windows.Forms.Label();
			this.selAlsoPlayingAnyMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAlsoPlayingNotMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAlsoPlayingAllMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAnyMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selNotMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			this.selAllMarkers = new SPNATI_Character_Editor.Controls.SelectBox();
			((System.ComponentModel.ISupportInitialize)(this.valMaxPlayerStartingLayers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valPlayerStartingLayers)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// valMaxPlayerStartingLayers
			// 
			this.valMaxPlayerStartingLayers.Location = new System.Drawing.Point(203, 12);
			this.valMaxPlayerStartingLayers.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.valMaxPlayerStartingLayers.Name = "valMaxPlayerStartingLayers";
			this.valMaxPlayerStartingLayers.Size = new System.Drawing.Size(43, 20);
			this.valMaxPlayerStartingLayers.TabIndex = 38;
			this.valMaxPlayerStartingLayers.ValueChanged += new System.EventHandler(this.valMaxPlayerStartingLayers_ValueChanged);
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Location = new System.Drawing.Point(178, 14);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(19, 13);
			this.label52.TabIndex = 40;
			this.label52.Text = "to:";
			// 
			// valPlayerStartingLayers
			// 
			this.valPlayerStartingLayers.Location = new System.Drawing.Point(129, 12);
			this.valPlayerStartingLayers.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.valPlayerStartingLayers.Name = "valPlayerStartingLayers";
			this.valPlayerStartingLayers.Size = new System.Drawing.Size(43, 20);
			this.valPlayerStartingLayers.TabIndex = 37;
			this.valPlayerStartingLayers.ValueChanged += new System.EventHandler(this.valPlayerStartingLayers_ValueChanged);
			// 
			// lblPlayerStartingLayers
			// 
			this.lblPlayerStartingLayers.AutoSize = true;
			this.lblPlayerStartingLayers.Location = new System.Drawing.Point(12, 14);
			this.lblPlayerStartingLayers.Name = "lblPlayerStartingLayers";
			this.lblPlayerStartingLayers.Size = new System.Drawing.Size(106, 13);
			this.lblPlayerStartingLayers.TabIndex = 39;
			this.lblPlayerStartingLayers.Text = "Player starting layers:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.selAnyMarkers);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.selNotMarkers);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.selAllMarkers);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(15, 38);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(314, 360);
			this.groupBox1.TabIndex = 41;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Own markers";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 245);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(225, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Require any of the following markers to be set:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(232, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Require none of the following markers to be set:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(206, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Require all the following markers to be set:";
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.Location = new System.Drawing.Point(501, 12);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 42;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(582, 12);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 42;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.selAlsoPlayingAnyMarkers);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.selAlsoPlayingNotMarkers);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.selAlsoPlayingAllMarkers);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(344, 38);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(314, 360);
			this.groupBox2.TabIndex = 43;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Other markers";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 245);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(225, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Require any of the following markers to be set:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(10, 132);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(232, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Require none of the following markers to be set:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(206, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Require all the following markers to be set:";
			// 
			// lblAlsoPlaying
			// 
			this.lblAlsoPlaying.AutoSize = true;
			this.lblAlsoPlaying.Location = new System.Drawing.Point(262, 14);
			this.lblAlsoPlaying.Name = "lblAlsoPlaying";
			this.lblAlsoPlaying.Size = new System.Drawing.Size(66, 13);
			this.lblAlsoPlaying.TabIndex = 44;
			this.lblAlsoPlaying.Text = "Also playing:";
			// 
			// cboAlsoPlaying
			// 
			this.cboAlsoPlaying.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboAlsoPlaying.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboAlsoPlaying.FormattingEnabled = true;
			this.cboAlsoPlaying.Location = new System.Drawing.Point(334, 12);
			this.cboAlsoPlaying.Name = "cboAlsoPlaying";
			this.cboAlsoPlaying.Size = new System.Drawing.Size(149, 21);
			this.cboAlsoPlaying.TabIndex = 45;
			// 
			// GalleryImageSelectBtn
			// 
			this.GalleryImageSelectBtn.Location = new System.Drawing.Point(334, 416);
			this.GalleryImageSelectBtn.Name = "GalleryImageSelectBtn";
			this.GalleryImageSelectBtn.Size = new System.Drawing.Size(43, 23);
			this.GalleryImageSelectBtn.TabIndex = 46;
			this.GalleryImageSelectBtn.Text = "...";
			this.GalleryImageSelectBtn.UseVisualStyleBackColor = true;
			this.GalleryImageSelectBtn.Click += new System.EventHandler(this.GalleryImageSelectBtn_Click);
			// 
			// valGalleryImage
			// 
			this.valGalleryImage.Location = new System.Drawing.Point(95, 418);
			this.valGalleryImage.Name = "valGalleryImage";
			this.valGalleryImage.Size = new System.Drawing.Size(234, 20);
			this.valGalleryImage.TabIndex = 47;
			// 
			// galleryImageFileDialog
			// 
			this.galleryImageFileDialog.FileName = "openFileDialog1";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(16, 421);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 13);
			this.label7.TabIndex = 48;
			this.label7.Text = "Gallery image:";
			// 
			// selAlsoPlayingAnyMarkers
			// 
			this.selAlsoPlayingAnyMarkers.Location = new System.Drawing.Point(10, 262);
			this.selAlsoPlayingAnyMarkers.Name = "selAlsoPlayingAnyMarkers";
			this.selAlsoPlayingAnyMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingAnyMarkers.Size = new System.Drawing.Size(293, 85);
			this.selAlsoPlayingAnyMarkers.TabIndex = 1;
			// 
			// selAlsoPlayingNotMarkers
			// 
			this.selAlsoPlayingNotMarkers.Location = new System.Drawing.Point(10, 149);
			this.selAlsoPlayingNotMarkers.Name = "selAlsoPlayingNotMarkers";
			this.selAlsoPlayingNotMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingNotMarkers.Size = new System.Drawing.Size(293, 85);
			this.selAlsoPlayingNotMarkers.TabIndex = 1;
			// 
			// selAlsoPlayingAllMarkers
			// 
			this.selAlsoPlayingAllMarkers.Location = new System.Drawing.Point(7, 37);
			this.selAlsoPlayingAllMarkers.Name = "selAlsoPlayingAllMarkers";
			this.selAlsoPlayingAllMarkers.SelectedItems = new string[0];
			this.selAlsoPlayingAllMarkers.Size = new System.Drawing.Size(293, 85);
			this.selAlsoPlayingAllMarkers.TabIndex = 1;
			// 
			// selAnyMarkers
			// 
			this.selAnyMarkers.Location = new System.Drawing.Point(10, 262);
			this.selAnyMarkers.Name = "selAnyMarkers";
			this.selAnyMarkers.SelectedItems = new string[0];
			this.selAnyMarkers.Size = new System.Drawing.Size(293, 85);
			this.selAnyMarkers.TabIndex = 1;
			this.selAnyMarkers.Enter += new System.EventHandler(this.markerControl_Enter);
			this.selAnyMarkers.Leave += new System.EventHandler(this.markerControl_Leave);
			// 
			// selNotMarkers
			// 
			this.selNotMarkers.Location = new System.Drawing.Point(10, 149);
			this.selNotMarkers.Name = "selNotMarkers";
			this.selNotMarkers.SelectedItems = new string[0];
			this.selNotMarkers.Size = new System.Drawing.Size(293, 85);
			this.selNotMarkers.TabIndex = 1;
			this.selNotMarkers.Enter += new System.EventHandler(this.markerControl_Enter);
			this.selNotMarkers.Leave += new System.EventHandler(this.markerControl_Leave);
			// 
			// selAllMarkers
			// 
			this.selAllMarkers.Location = new System.Drawing.Point(7, 37);
			this.selAllMarkers.Name = "selAllMarkers";
			this.selAllMarkers.SelectedItems = new string[0];
			this.selAllMarkers.Size = new System.Drawing.Size(293, 85);
			this.selAllMarkers.TabIndex = 1;
			this.selAllMarkers.Enter += new System.EventHandler(this.markerControl_Enter);
			this.selAllMarkers.Leave += new System.EventHandler(this.markerControl_Leave);
			// 
			// AdvancedEpilogueConditions
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(672, 447);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.valGalleryImage);
			this.Controls.Add(this.GalleryImageSelectBtn);
			this.Controls.Add(this.cboAlsoPlaying);
			this.Controls.Add(this.lblAlsoPlaying);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.valMaxPlayerStartingLayers);
			this.Controls.Add(this.label52);
			this.Controls.Add(this.valPlayerStartingLayers);
			this.Controls.Add(this.lblPlayerStartingLayers);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AdvancedEpilogueConditions";
			this.Text = "Advanced Epilogue Conditions and Properties";
			this.Load += new System.EventHandler(this.AdvancedEpilogueConditions_Load);
			((System.ComponentModel.ISupportInitialize)(this.valMaxPlayerStartingLayers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valPlayerStartingLayers)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown valMaxPlayerStartingLayers;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label lblPlayerStartingLayers;
		private System.Windows.Forms.GroupBox groupBox1;
		private Controls.SelectBox selAllMarkers;
		private System.Windows.Forms.Label label1;
		private Controls.SelectBox selAnyMarkers;
		private System.Windows.Forms.Label label3;
		private Controls.SelectBox selNotMarkers;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.GroupBox groupBox2;
		private Controls.SelectBox selAlsoPlayingAnyMarkers;
		private System.Windows.Forms.Label label4;
		private Controls.SelectBox selAlsoPlayingNotMarkers;
		private System.Windows.Forms.Label label5;
		private Controls.SelectBox selAlsoPlayingAllMarkers;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown valPlayerStartingLayers;
		private System.Windows.Forms.Label lblAlsoPlaying;
		private System.Windows.Forms.ComboBox cboAlsoPlaying;
        private System.Windows.Forms.Button GalleryImageSelectBtn;
        private System.Windows.Forms.TextBox valGalleryImage;
        private System.Windows.Forms.OpenFileDialog galleryImageFileDialog;
        private System.Windows.Forms.Label label7;
    }
}
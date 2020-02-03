namespace SPNATI_Character_Editor.Forms
{
	partial class PoseExporter
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.txtFile = new Desktop.Skinning.SkinnedTextBox();
			this.cmdBrowse = new Desktop.Skinning.SkinnedButton();
			this.cmdExport = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.lblName = new Desktop.Skinning.SkinnedLabel();
			this.valWidth = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.valHeight = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.valFrameRate = new Desktop.Skinning.SkinnedNumericUpDown();
			this.preview = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel2 = new Desktop.Skinning.SkinnedPanel();
			((System.ComponentModel.ISupportInitialize)(this.valWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valFrameRate)).BeginInit();
			this.skinnedPanel1.SuspendLayout();
			this.skinnedPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Primary;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(507, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "This feature is for sharing poses on Discord and the like. Do NOT put these GIFs " +
    "in your character\'s folder!";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Primary;
			this.label2.Location = new System.Drawing.Point(3, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(379, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "The game does NOT need poses to be converted to GIFs in order to use them.";
			// 
			// txtFile
			// 
			this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFile.BackColor = System.Drawing.Color.White;
			this.txtFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtFile.ForeColor = System.Drawing.Color.Black;
			this.txtFile.Location = new System.Drawing.Point(12, 71);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(425, 20);
			this.txtFile.TabIndex = 2;
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowse.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBrowse.Flat = false;
			this.cmdBrowse.Location = new System.Drawing.Point(443, 69);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(80, 23);
			this.cmdBrowse.TabIndex = 3;
			this.cmdBrowse.Text = "Browse...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// cmdExport
			// 
			this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdExport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdExport.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdExport.Flat = false;
			this.cmdExport.ForeColor = System.Drawing.Color.Red;
			this.cmdExport.Location = new System.Drawing.Point(370, 3);
			this.cmdExport.Name = "cmdExport";
			this.cmdExport.Size = new System.Drawing.Size(75, 23);
			this.cmdExport.TabIndex = 60;
			this.cmdExport.Text = "Export";
			this.cmdExport.UseVisualStyleBackColor = true;
			this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(452, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 61;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label3.Location = new System.Drawing.Point(15, 98);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Exporting Pose:";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblName.ForeColor = System.Drawing.Color.Black;
			this.lblName.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblName.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblName.Location = new System.Drawing.Point(102, 98);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 7;
			this.lblName.Text = "Name";
			// 
			// valWidth
			// 
			this.valWidth.BackColor = System.Drawing.Color.White;
			this.valWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valWidth.ForeColor = System.Drawing.Color.Black;
			this.valWidth.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.valWidth.Location = new System.Drawing.Point(59, 122);
			this.valWidth.Maximum = new decimal(new int[] {
            1400,
            0,
            0,
            0});
			this.valWidth.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.valWidth.Name = "valWidth";
			this.valWidth.Size = new System.Drawing.Size(57, 20);
			this.valWidth.TabIndex = 4;
			this.valWidth.Value = new decimal(new int[] {
            260,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label4.Location = new System.Drawing.Point(15, 124);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Width:";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "GIF files|*.gif";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label5.Location = new System.Drawing.Point(120, 124);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Height:";
			// 
			// valHeight
			// 
			this.valHeight.BackColor = System.Drawing.Color.White;
			this.valHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valHeight.ForeColor = System.Drawing.Color.Black;
			this.valHeight.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.valHeight.Location = new System.Drawing.Point(164, 122);
			this.valHeight.Maximum = new decimal(new int[] {
            1400,
            0,
            0,
            0});
			this.valHeight.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.valHeight.Name = "valHeight";
			this.valHeight.Size = new System.Drawing.Size(57, 20);
			this.valHeight.TabIndex = 5;
			this.valHeight.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label6.Location = new System.Drawing.Point(227, 124);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "FPS:";
			// 
			// valFrameRate
			// 
			this.valFrameRate.BackColor = System.Drawing.Color.White;
			this.valFrameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valFrameRate.ForeColor = System.Drawing.Color.Black;
			this.valFrameRate.Location = new System.Drawing.Point(263, 122);
			this.valFrameRate.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.valFrameRate.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.valFrameRate.Name = "valFrameRate";
			this.valFrameRate.Size = new System.Drawing.Size(57, 20);
			this.valFrameRate.TabIndex = 6;
			this.valFrameRate.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// preview
			// 
			this.preview.Location = new System.Drawing.Point(326, 71);
			this.preview.Name = "preview";
			this.preview.Size = new System.Drawing.Size(197, 493);
			this.preview.TabIndex = 10;
			this.preview.Visible = false;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdExport);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 148);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(530, 30);
			this.skinnedPanel1.TabIndex = 62;
			// 
			// skinnedPanel2
			// 
			this.skinnedPanel2.Controls.Add(this.label1);
			this.skinnedPanel2.Controls.Add(this.label2);
			this.skinnedPanel2.Location = new System.Drawing.Point(1, 25);
			this.skinnedPanel2.Name = "skinnedPanel2";
			this.skinnedPanel2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Primary;
			this.skinnedPanel2.Size = new System.Drawing.Size(528, 40);
			this.skinnedPanel2.TabIndex = 63;
			// 
			// PoseExporter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(530, 178);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel2);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.valFrameRate);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.valHeight);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.valWidth);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtFile);
			this.Controls.Add(this.preview);
			this.Name = "PoseExporter";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export Pose as GIF";
			((System.ComponentModel.ISupportInitialize)(this.valWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valFrameRate)).EndInit();
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel2.ResumeLayout(false);
			this.skinnedPanel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedTextBox txtFile;
		private Desktop.Skinning.SkinnedButton cmdBrowse;
		private Desktop.Skinning.SkinnedButton cmdExport;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedLabel lblName;
		private Desktop.Skinning.SkinnedNumericUpDown valWidth;
		private Desktop.Skinning.SkinnedLabel label4;
		private Controls.CharacterImageBox preview;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedNumericUpDown valHeight;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedNumericUpDown valFrameRate;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedPanel skinnedPanel2;
	}
}
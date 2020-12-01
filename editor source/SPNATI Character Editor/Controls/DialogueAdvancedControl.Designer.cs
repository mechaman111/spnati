namespace SPNATI_Character_Editor.Controls
{
	partial class DialogueAdvancedControl
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
			this.groupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.valLocation = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.cboDirection = new Desktop.Skinning.SkinnedComboBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.groupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.chkResetLabel = new Desktop.Skinning.SkinnedCheckBox();
			this.txtLabel = new Desktop.Skinning.SkinnedTextBox();
			this.cboAI = new Desktop.Skinning.SkinnedComboBox();
			this.label7 = new Desktop.Skinning.SkinnedLabel();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.cboSize = new Desktop.Skinning.SkinnedComboBox();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.cboGender = new Desktop.Skinning.SkinnedComboBox();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.chkResetAI = new Desktop.Skinning.SkinnedCheckBox();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.valWeight = new Desktop.Skinning.SkinnedNumericUpDown();
			this.skinnedGroupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.chkLayer = new Desktop.Skinning.SkinnedCheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLocation)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valWeight)).BeginInit();
			this.skinnedGroupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.White;
			this.groupBox1.Controls.Add(this.valLocation);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cboDirection);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.groupBox1.Image = null;
			this.groupBox1.Location = new System.Drawing.Point(3, 90);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.groupBox1.ShowIndicatorBar = false;
			this.groupBox1.Size = new System.Drawing.Size(200, 79);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Arrow";
			// 
			// valLocation
			// 
			this.valLocation.BackColor = System.Drawing.Color.White;
			this.valLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valLocation.ForeColor = System.Drawing.Color.Black;
			this.valLocation.Location = new System.Drawing.Point(80, 51);
			this.valLocation.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.valLocation.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
			this.valLocation.Name = "valLocation";
			this.valLocation.Size = new System.Drawing.Size(114, 20);
			this.valLocation.TabIndex = 11;
			this.valLocation.ValueChanged += new System.EventHandler(this.valLocation_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label2.Location = new System.Drawing.Point(6, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Location (%):";
			// 
			// cboDirection
			// 
			this.cboDirection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboDirection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboDirection.BackColor = System.Drawing.Color.White;
			this.cboDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDirection.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboDirection.FormattingEnabled = true;
			this.cboDirection.KeyMember = null;
			this.cboDirection.Location = new System.Drawing.Point(80, 24);
			this.cboDirection.Name = "cboDirection";
			this.cboDirection.SelectedIndex = -1;
			this.cboDirection.SelectedItem = null;
			this.cboDirection.Size = new System.Drawing.Size(114, 21);
			this.cboDirection.Sorted = false;
			this.cboDirection.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label1.Location = new System.Drawing.Point(6, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Direction:";
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.White;
			this.groupBox2.Controls.Add(this.chkResetLabel);
			this.groupBox2.Controls.Add(this.txtLabel);
			this.groupBox2.Controls.Add(this.cboAI);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.cboSize);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.cboGender);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.chkResetAI);
			this.groupBox2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.groupBox2.Image = null;
			this.groupBox2.Location = new System.Drawing.Point(3, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.groupBox2.ShowIndicatorBar = false;
			this.groupBox2.Size = new System.Drawing.Size(389, 78);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Change state";
			// 
			// chkResetLabel
			// 
			this.chkResetLabel.AutoSize = true;
			this.chkResetLabel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkResetLabel.Location = new System.Drawing.Point(329, 53);
			this.chkResetLabel.Name = "chkResetLabel";
			this.chkResetLabel.Size = new System.Drawing.Size(54, 17);
			this.chkResetLabel.TabIndex = 9;
			this.chkResetLabel.Text = "Reset";
			this.chkResetLabel.UseVisualStyleBackColor = true;
			this.chkResetLabel.CheckedChanged += new System.EventHandler(this.chkResetLabel_CheckedChanged);
			// 
			// txtLabel
			// 
			this.txtLabel.BackColor = System.Drawing.Color.White;
			this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtLabel.ForeColor = System.Drawing.Color.Black;
			this.txtLabel.Location = new System.Drawing.Point(240, 51);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(84, 20);
			this.txtLabel.TabIndex = 8;
			// 
			// cboAI
			// 
			this.cboAI.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboAI.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboAI.BackColor = System.Drawing.Color.White;
			this.cboAI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAI.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboAI.FormattingEnabled = true;
			this.cboAI.KeyMember = null;
			this.cboAI.Location = new System.Drawing.Point(241, 24);
			this.cboAI.Name = "cboAI";
			this.cboAI.SelectedIndex = -1;
			this.cboAI.SelectedItem = null;
			this.cboAI.Size = new System.Drawing.Size(83, 21);
			this.cboAI.Sorted = false;
			this.cboAI.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label7.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label7.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label7.Location = new System.Drawing.Point(172, 54);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(36, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Label:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label6.Location = new System.Drawing.Point(172, 27);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Intelligence:";
			// 
			// cboSize
			// 
			this.cboSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboSize.BackColor = System.Drawing.Color.White;
			this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSize.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboSize.FormattingEnabled = true;
			this.cboSize.KeyMember = null;
			this.cboSize.Location = new System.Drawing.Point(80, 51);
			this.cboSize.Name = "cboSize";
			this.cboSize.SelectedIndex = -1;
			this.cboSize.SelectedItem = null;
			this.cboSize.Size = new System.Drawing.Size(80, 21);
			this.cboSize.Sorted = false;
			this.cboSize.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label3.Location = new System.Drawing.Point(6, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Size:";
			// 
			// cboGender
			// 
			this.cboGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboGender.BackColor = System.Drawing.Color.White;
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboGender.FormattingEnabled = true;
			this.cboGender.KeyMember = null;
			this.cboGender.Location = new System.Drawing.Point(80, 24);
			this.cboGender.Name = "cboGender";
			this.cboGender.SelectedIndex = -1;
			this.cboGender.SelectedItem = null;
			this.cboGender.Size = new System.Drawing.Size(80, 21);
			this.cboGender.Sorted = false;
			this.cboGender.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label4.Location = new System.Drawing.Point(6, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Gender:";
			// 
			// chkResetAI
			// 
			this.chkResetAI.AutoSize = true;
			this.chkResetAI.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkResetAI.Location = new System.Drawing.Point(329, 26);
			this.chkResetAI.Name = "chkResetAI";
			this.chkResetAI.Size = new System.Drawing.Size(54, 17);
			this.chkResetAI.TabIndex = 4;
			this.chkResetAI.Text = "Reset";
			this.chkResetAI.UseVisualStyleBackColor = true;
			this.chkResetAI.CheckedChanged += new System.EventHandler(this.chkResetAI_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label5.Location = new System.Drawing.Point(5, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Weight:";
			// 
			// valWeight
			// 
			this.valWeight.BackColor = System.Drawing.Color.White;
			this.valWeight.DecimalPlaces = 2;
			this.valWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valWeight.ForeColor = System.Drawing.Color.Black;
			this.valWeight.Location = new System.Drawing.Point(55, 174);
			this.valWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.valWeight.Name = "valWeight";
			this.valWeight.Size = new System.Drawing.Size(60, 20);
			this.valWeight.TabIndex = 4;
			this.valWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			// 
			// skinnedGroupBox1
			// 
			this.skinnedGroupBox1.BackColor = System.Drawing.Color.White;
			this.skinnedGroupBox1.Controls.Add(this.chkLayer);
			this.skinnedGroupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.skinnedGroupBox1.Image = null;
			this.skinnedGroupBox1.Location = new System.Drawing.Point(209, 90);
			this.skinnedGroupBox1.Name = "skinnedGroupBox1";
			this.skinnedGroupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.skinnedGroupBox1.ShowIndicatorBar = false;
			this.skinnedGroupBox1.Size = new System.Drawing.Size(183, 79);
			this.skinnedGroupBox1.TabIndex = 12;
			this.skinnedGroupBox1.TabStop = false;
			this.skinnedGroupBox1.Text = "Speech Bubble";
			// 
			// chkLayer
			// 
			this.chkLayer.AutoSize = true;
			this.chkLayer.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkLayer.Location = new System.Drawing.Point(6, 26);
			this.chkLayer.Name = "chkLayer";
			this.chkLayer.Size = new System.Drawing.Size(115, 17);
			this.chkLayer.TabIndex = 0;
			this.chkLayer.Text = "Display over image";
			this.chkLayer.UseVisualStyleBackColor = true;
			// 
			// DialogueAdvancedControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.skinnedGroupBox1);
			this.Controls.Add(this.valWeight);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "DialogueAdvancedControl";
			this.Size = new System.Drawing.Size(395, 195);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLocation)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valWeight)).EndInit();
			this.skinnedGroupBox1.ResumeLayout(false);
			this.skinnedGroupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedGroupBox groupBox1;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedComboBox cboDirection;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedNumericUpDown valLocation;
		private Desktop.Skinning.SkinnedGroupBox groupBox2;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedComboBox cboGender;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedNumericUpDown valWeight;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedComboBox cboSize;
		private Desktop.Skinning.SkinnedTextBox txtLabel;
		private Desktop.Skinning.SkinnedComboBox cboAI;
		private Desktop.Skinning.SkinnedLabel label7;
		private Desktop.Skinning.SkinnedCheckBox chkResetLabel;
		private Desktop.Skinning.SkinnedCheckBox chkResetAI;
		private Desktop.Skinning.SkinnedGroupBox skinnedGroupBox1;
		private Desktop.Skinning.SkinnedCheckBox chkLayer;
	}
}

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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.valLocation = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.cboDirection = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtLabel = new System.Windows.Forms.TextBox();
			this.cboAI = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cboSize = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboGender = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.valWeight = new System.Windows.Forms.NumericUpDown();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLocation)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valWeight)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.valLocation);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cboDirection);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, 84);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 73);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Arrow";
			// 
			// valLocation
			// 
			this.valLocation.Location = new System.Drawing.Point(80, 46);
			this.valLocation.Name = "valLocation";
			this.valLocation.Size = new System.Drawing.Size(114, 20);
			this.valLocation.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Location (%):";
			// 
			// cboDirection
			// 
			this.cboDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDirection.FormattingEnabled = true;
			this.cboDirection.Location = new System.Drawing.Point(80, 19);
			this.cboDirection.Name = "cboDirection";
			this.cboDirection.Size = new System.Drawing.Size(114, 21);
			this.cboDirection.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Direction:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtLabel);
			this.groupBox2.Controls.Add(this.cboAI);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.cboSize);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.cboGender);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(3, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(389, 73);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Change state";
			// 
			// txtLabel
			// 
			this.txtLabel.Location = new System.Drawing.Point(270, 46);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(113, 20);
			this.txtLabel.TabIndex = 7;
			// 
			// cboAI
			// 
			this.cboAI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAI.FormattingEnabled = true;
			this.cboAI.Items.AddRange(new object[] {
            "",
            "bad",
            "average",
            "good",
            "best"});
			this.cboAI.Location = new System.Drawing.Point(270, 19);
			this.cboAI.Name = "cboAI";
			this.cboAI.Size = new System.Drawing.Size(113, 21);
			this.cboAI.TabIndex = 6;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(200, 49);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(36, 13);
			this.label7.TabIndex = 5;
			this.label7.Text = "Label:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(200, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Intelligence:";
			// 
			// cboSize
			// 
			this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSize.FormattingEnabled = true;
			this.cboSize.Items.AddRange(new object[] {
            "",
            "small",
            "medium",
            "large"});
			this.cboSize.Location = new System.Drawing.Point(80, 46);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(114, 21);
			this.cboSize.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Size:";
			// 
			// cboGender
			// 
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FormattingEnabled = true;
			this.cboGender.Items.AddRange(new object[] {
            "",
            "female",
            "male"});
			this.cboGender.Location = new System.Drawing.Point(80, 19);
			this.cboGender.Name = "cboGender";
			this.cboGender.Size = new System.Drawing.Size(114, 21);
			this.cboGender.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Gender:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 160);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Weight:";
			// 
			// valWeight
			// 
			this.valWeight.DecimalPlaces = 2;
			this.valWeight.Location = new System.Drawing.Point(53, 158);
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
			// DialogueAdvancedControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valWeight);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "DialogueAdvancedControl";
			this.Size = new System.Drawing.Size(395, 183);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valLocation)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valWeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboDirection;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown valLocation;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboGender;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown valWeight;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cboSize;
		private System.Windows.Forms.TextBox txtLabel;
		private System.Windows.Forms.ComboBox cboAI;
		private System.Windows.Forms.Label label7;
	}
}

namespace SPNATI_Character_Editor
{
	partial class PlayerRangeControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboMin = new System.Windows.Forms.ComboBox();
			this.cboMax = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "From:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(102, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(19, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "to:";
			// 
			// cboMin
			// 
			this.cboMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMin.FormattingEnabled = true;
			this.cboMin.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMin.Location = new System.Drawing.Point(42, 0);
			this.cboMin.Name = "cboMin";
			this.cboMin.Size = new System.Drawing.Size(54, 21);
			this.cboMin.TabIndex = 3;
			this.cboMin.SelectedIndexChanged += new System.EventHandler(this.cboMin_SelectedIndexChanged);
			// 
			// cboMax
			// 
			this.cboMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMax.FormattingEnabled = true;
			this.cboMax.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.cboMax.Location = new System.Drawing.Point(127, 0);
			this.cboMax.Name = "cboMax";
			this.cboMax.Size = new System.Drawing.Size(54, 21);
			this.cboMax.TabIndex = 4;
			this.cboMax.SelectedIndexChanged += new System.EventHandler(this.cboMax_SelectedIndexChanged);
			// 
			// PlayerRangeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboMax);
			this.Controls.Add(this.cboMin);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "PlayerRangeControl";
			this.Size = new System.Drawing.Size(276, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboMin;
		private System.Windows.Forms.ComboBox cboMax;
	}
}

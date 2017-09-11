namespace SPNATI_Character_Editor
{
	partial class ValidationForm
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
			this.txtWarnings = new System.Windows.Forms.TextBox();
			this.lstCharacters = new System.Windows.Forms.ListBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lstFilters = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtWarnings
			// 
			this.txtWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtWarnings.Location = new System.Drawing.Point(233, 25);
			this.txtWarnings.Multiline = true;
			this.txtWarnings.Name = "txtWarnings";
			this.txtWarnings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtWarnings.Size = new System.Drawing.Size(657, 522);
			this.txtWarnings.TabIndex = 2;
			// 
			// lstCharacters
			// 
			this.lstCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstCharacters.FormattingEnabled = true;
			this.lstCharacters.Location = new System.Drawing.Point(12, 25);
			this.lstCharacters.Name = "lstCharacters";
			this.lstCharacters.Size = new System.Drawing.Size(215, 394);
			this.lstCharacters.TabIndex = 1;
			this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(815, 553);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Characters";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(230, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Warnings";
			// 
			// lstFilters
			// 
			this.lstFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lstFilters.FormattingEnabled = true;
			this.lstFilters.Location = new System.Drawing.Point(12, 440);
			this.lstFilters.Name = "lstFilters";
			this.lstFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lstFilters.Size = new System.Drawing.Size(215, 134);
			this.lstFilters.TabIndex = 6;
			this.lstFilters.SelectedIndexChanged += new System.EventHandler(this.lstFilters_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 424);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Filters";
			// 
			// ValidationForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(902, 588);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lstFilters);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.lstCharacters);
			this.Controls.Add(this.txtWarnings);
			this.Name = "ValidationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Validation Results";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtWarnings;
		private System.Windows.Forms.ListBox lstCharacters;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lstFilters;
		private System.Windows.Forms.Label label3;
	}
}
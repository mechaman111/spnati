namespace SPNATI_Character_Editor.Controls
{
	partial class ValidationControl
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
			this.components = new System.ComponentModel.Container();
			this.label3 = new System.Windows.Forms.Label();
			this.lstFilters = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lstCharacters = new System.Windows.Forms.ListBox();
			this.lstWarnings = new System.Windows.Forms.ListBox();
			this.pnlValid = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.pnlWarnings = new System.Windows.Forms.Panel();
			this.cmdGoTo = new System.Windows.Forms.Button();
			this.pnlProgress = new System.Windows.Forms.Panel();
			this.lblProgress = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.cmdCopy = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pnlValid.SuspendLayout();
			this.pnlWarnings.SuspendLayout();
			this.pnlProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 520);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Filters";
			// 
			// lstFilters
			// 
			this.lstFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lstFilters.FormattingEnabled = true;
			this.lstFilters.Location = new System.Drawing.Point(3, 536);
			this.lstFilters.Name = "lstFilters";
			this.lstFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lstFilters.Size = new System.Drawing.Size(215, 134);
			this.lstFilters.TabIndex = 12;
			this.lstFilters.SelectedIndexChanged += new System.EventHandler(this.lstFilters_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(221, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Warnings";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Characters";
			// 
			// lstCharacters
			// 
			this.lstCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstCharacters.FormattingEnabled = true;
			this.lstCharacters.Location = new System.Drawing.Point(3, 21);
			this.lstCharacters.Name = "lstCharacters";
			this.lstCharacters.Size = new System.Drawing.Size(215, 498);
			this.lstCharacters.TabIndex = 8;
			this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);
			// 
			// lstWarnings
			// 
			this.lstWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstWarnings.HorizontalScrollbar = true;
			this.lstWarnings.Location = new System.Drawing.Point(3, 0);
			this.lstWarnings.Name = "lstWarnings";
			this.lstWarnings.Size = new System.Drawing.Size(843, 615);
			this.lstWarnings.TabIndex = 9;
			this.lstWarnings.SelectedIndexChanged += new System.EventHandler(this.lstWarnings_SelectedIndexChanged);
			this.lstWarnings.DoubleClick += new System.EventHandler(this.lstWarnings_DoubleClick);
			// 
			// pnlValid
			// 
			this.pnlValid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlValid.Controls.Add(this.label4);
			this.pnlValid.Location = new System.Drawing.Point(224, 21);
			this.pnlValid.Name = "pnlValid";
			this.pnlValid.Size = new System.Drawing.Size(849, 649);
			this.pnlValid.TabIndex = 14;
			this.pnlValid.Visible = false;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Green;
			this.label4.Location = new System.Drawing.Point(3, 235);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(843, 173);
			this.label4.TabIndex = 0;
			this.label4.Text = "Everything Checks Out!";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlWarnings
			// 
			this.pnlWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlWarnings.Controls.Add(this.cmdCopy);
			this.pnlWarnings.Controls.Add(this.cmdGoTo);
			this.pnlWarnings.Controls.Add(this.lstWarnings);
			this.pnlWarnings.Location = new System.Drawing.Point(224, 21);
			this.pnlWarnings.Name = "pnlWarnings";
			this.pnlWarnings.Size = new System.Drawing.Size(849, 649);
			this.pnlWarnings.TabIndex = 14;
			// 
			// cmdGoTo
			// 
			this.cmdGoTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGoTo.Location = new System.Drawing.Point(746, 623);
			this.cmdGoTo.Name = "cmdGoTo";
			this.cmdGoTo.Size = new System.Drawing.Size(100, 23);
			this.cmdGoTo.TabIndex = 10;
			this.cmdGoTo.Text = "Go to Warning";
			this.toolTip1.SetToolTip(this.cmdGoTo, "Jumps to the line causing the selected warning");
			this.cmdGoTo.UseVisualStyleBackColor = true;
			this.cmdGoTo.Click += new System.EventHandler(this.cmdGoTo_Click);
			// 
			// pnlProgress
			// 
			this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlProgress.Controls.Add(this.lblProgress);
			this.pnlProgress.Controls.Add(this.progressBar);
			this.pnlProgress.Location = new System.Drawing.Point(224, 21);
			this.pnlProgress.Name = "pnlProgress";
			this.pnlProgress.Size = new System.Drawing.Size(849, 649);
			this.pnlProgress.TabIndex = 1;
			this.pnlProgress.Visible = false;
			// 
			// lblProgress
			// 
			this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProgress.Location = new System.Drawing.Point(3, 286);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(843, 50);
			this.lblProgress.TabIndex = 3;
			this.lblProgress.Text = "Loading...";
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(282, 339);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(276, 23);
			this.progressBar.TabIndex = 2;
			// 
			// cmdCopy
			// 
			this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCopy.Location = new System.Drawing.Point(3, 623);
			this.cmdCopy.Name = "cmdCopy";
			this.cmdCopy.Size = new System.Drawing.Size(115, 23);
			this.cmdCopy.TabIndex = 11;
			this.cmdCopy.Text = "Copy to Clipboard";
			this.toolTip1.SetToolTip(this.cmdCopy, "Copies all validation warnings for this character to the clipboard");
			this.cmdCopy.UseVisualStyleBackColor = true;
			this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
			// 
			// ValidationControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlWarnings);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lstFilters);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstCharacters);
			this.Controls.Add(this.pnlProgress);
			this.Controls.Add(this.pnlValid);
			this.Name = "ValidationControl";
			this.Size = new System.Drawing.Size(1076, 673);
			this.pnlValid.ResumeLayout(false);
			this.pnlWarnings.ResumeLayout(false);
			this.pnlProgress.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox lstFilters;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lstCharacters;
		private System.Windows.Forms.ListBox lstWarnings;
		private System.Windows.Forms.Panel pnlWarnings;
		private System.Windows.Forms.Panel pnlValid;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel pnlProgress;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button cmdGoTo;
		private System.Windows.Forms.Button cmdCopy;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

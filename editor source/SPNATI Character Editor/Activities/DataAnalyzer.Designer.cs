namespace SPNATI_Character_Editor.Activities
{
	partial class DataAnalyzer
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
			this.cmdLoad = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlStart = new System.Windows.Forms.Panel();
			this.pnlLoad = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.pnlEdit = new System.Windows.Forms.Panel();
			this.pnlStart.SuspendLayout();
			this.pnlLoad.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdLoad
			// 
			this.cmdLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdLoad.Location = new System.Drawing.Point(152, 67);
			this.cmdLoad.Name = "cmdLoad";
			this.cmdLoad.Size = new System.Drawing.Size(221, 110);
			this.cmdLoad.TabIndex = 0;
			this.cmdLoad.Text = "Load";
			this.cmdLoad.UseVisualStyleBackColor = true;
			this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(65, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(390, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "This tool is used for reporting purposes to list all characters fulfilling certai" +
    "n criteria.";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(423, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Click Load to begin. This will take a long time as it must scan through all chara" +
    "cters\' files.";
			// 
			// pnlStart
			// 
			this.pnlStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlStart.Controls.Add(this.label1);
			this.pnlStart.Controls.Add(this.label2);
			this.pnlStart.Controls.Add(this.cmdLoad);
			this.pnlStart.Location = new System.Drawing.Point(178, 200);
			this.pnlStart.Name = "pnlStart";
			this.pnlStart.Size = new System.Drawing.Size(530, 198);
			this.pnlStart.TabIndex = 3;
			// 
			// pnlLoad
			// 
			this.pnlLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pnlLoad.Controls.Add(this.progressBar);
			this.pnlLoad.Controls.Add(this.label3);
			this.pnlLoad.Location = new System.Drawing.Point(178, 250);
			this.pnlLoad.Name = "pnlLoad";
			this.pnlLoad.Size = new System.Drawing.Size(530, 100);
			this.pnlLoad.TabIndex = 4;
			this.pnlLoad.Visible = false;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(205, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107, 25);
			this.label3.TabIndex = 0;
			this.label3.Text = "Loading...";
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(3, 43);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(524, 23);
			this.progressBar.TabIndex = 1;
			// 
			// pnlEdit
			// 
			this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlEdit.Location = new System.Drawing.Point(0, 0);
			this.pnlEdit.Name = "pnlEdit";
			this.pnlEdit.Size = new System.Drawing.Size(886, 615);
			this.pnlEdit.TabIndex = 5;
			this.pnlEdit.Visible = false;
			// 
			// DataAnalyzer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlEdit);
			this.Controls.Add(this.pnlLoad);
			this.Controls.Add(this.pnlStart);
			this.Name = "DataAnalyzer";
			this.Size = new System.Drawing.Size(886, 615);
			this.pnlStart.ResumeLayout(false);
			this.pnlStart.PerformLayout();
			this.pnlLoad.ResumeLayout(false);
			this.pnlLoad.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdLoad;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlStart;
		private System.Windows.Forms.Panel pnlLoad;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel pnlEdit;
	}
}

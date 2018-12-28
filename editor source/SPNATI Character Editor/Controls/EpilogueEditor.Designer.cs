namespace SPNATI_Character_Editor.Controls
{
	partial class EpilogueEditor
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
			this.cboEnding = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboGender = new System.Windows.Forms.ComboBox();
			this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdDeleteEnding = new System.Windows.Forms.Button();
			this.cmdAddEnding = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdAdvancedConditions = new System.Windows.Forms.Button();
			this.canvas = new SPNATI_Character_Editor.Controls.EpilogueCanvas();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboEnding
			// 
			this.cboEnding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEnding.FormattingEnabled = true;
			this.cboEnding.Location = new System.Drawing.Point(49, 8);
			this.cboEnding.Name = "cboEnding";
			this.cboEnding.Size = new System.Drawing.Size(170, 21);
			this.cboEnding.TabIndex = 0;
			this.toolTip1.SetToolTip(this.cboEnding, "Select an ending to edit");
			this.cboEnding.SelectedIndexChanged += new System.EventHandler(this.cboEnding_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Ending:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(306, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Available to gender:";
			// 
			// cboGender
			// 
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FormattingEnabled = true;
			this.cboGender.Items.AddRange(new object[] {
            "male",
            "female",
            "any"});
			this.cboGender.Location = new System.Drawing.Point(413, 8);
			this.cboGender.Name = "cboGender";
			this.cboGender.Size = new System.Drawing.Size(87, 21);
			this.cboGender.TabIndex = 12;
			this.toolTip1.SetToolTip(this.cboGender, "What gender the player must be for this ending to be available");
			// 
			// imageFileDialog
			// 
			this.imageFileDialog.FileName = "openFileDialog1";
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitle.Location = new System.Drawing.Point(639, 8);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(221, 20);
			this.txtTitle.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(603, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Title:";
			// 
			// cmdDeleteEnding
			// 
			this.cmdDeleteEnding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDeleteEnding.Location = new System.Drawing.Point(866, 7);
			this.cmdDeleteEnding.Name = "cmdDeleteEnding";
			this.cmdDeleteEnding.Size = new System.Drawing.Size(101, 23);
			this.cmdDeleteEnding.TabIndex = 20;
			this.cmdDeleteEnding.Text = "Delete Ending";
			this.toolTip1.SetToolTip(this.cmdDeleteEnding, "Delete this ending");
			this.cmdDeleteEnding.UseVisualStyleBackColor = true;
			this.cmdDeleteEnding.Click += new System.EventHandler(this.cmdDeleteEnding_Click);
			// 
			// cmdAddEnding
			// 
			this.cmdAddEnding.Location = new System.Drawing.Point(225, 7);
			this.cmdAddEnding.Name = "cmdAddEnding";
			this.cmdAddEnding.Size = new System.Drawing.Size(75, 23);
			this.cmdAddEnding.TabIndex = 21;
			this.cmdAddEnding.Text = "Add";
			this.toolTip1.SetToolTip(this.cmdAddEnding, "Add a new ending");
			this.cmdAddEnding.UseVisualStyleBackColor = true;
			this.cmdAddEnding.Click += new System.EventHandler(this.cmdAddEnding_Click);
			// 
			// cmdAdvancedConditions
			// 
			this.cmdAdvancedConditions.Location = new System.Drawing.Point(506, 7);
			this.cmdAdvancedConditions.Name = "cmdAdvancedConditions";
			this.cmdAdvancedConditions.Size = new System.Drawing.Size(90, 23);
			this.cmdAdvancedConditions.TabIndex = 22;
			this.cmdAdvancedConditions.Text = "Advanced...";
			this.cmdAdvancedConditions.UseVisualStyleBackColor = true;
			this.cmdAdvancedConditions.Click += new System.EventHandler(this.cmdAdvancedConditions_Click);
			// 
			// canvas
			// 
			this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.canvas.Enabled = false;
			this.canvas.Location = new System.Drawing.Point(0, 36);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(973, 607);
			this.canvas.TabIndex = 23;
			this.canvas.ZoomLevel = 1F;
			// 
			// pnlHeader
			// 
			this.pnlHeader.Controls.Add(this.label1);
			this.pnlHeader.Controls.Add(this.cboEnding);
			this.pnlHeader.Controls.Add(this.cmdAdvancedConditions);
			this.pnlHeader.Controls.Add(this.label2);
			this.pnlHeader.Controls.Add(this.cmdAddEnding);
			this.pnlHeader.Controls.Add(this.cboGender);
			this.pnlHeader.Controls.Add(this.cmdDeleteEnding);
			this.pnlHeader.Controls.Add(this.txtTitle);
			this.pnlHeader.Controls.Add(this.label3);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(973, 36);
			this.pnlHeader.TabIndex = 24;
			// 
			// EpilogueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlHeader);
			this.Controls.Add(this.canvas);
			this.Name = "EpilogueEditor";
			this.Size = new System.Drawing.Size(973, 646);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ComboBox cboEnding;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboGender;
		private System.Windows.Forms.OpenFileDialog imageFileDialog;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdDeleteEnding;
		private System.Windows.Forms.Button cmdAddEnding;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button cmdAdvancedConditions;
		private EpilogueCanvas canvas;
		private System.Windows.Forms.Panel pnlHeader;
	}
}

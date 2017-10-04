namespace SPNATI_Character_Editor.Controls
{
	partial class TestCharacter
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
			this.picPortrait = new System.Windows.Forms.PictureBox();
			this.lblName = new System.Windows.Forms.Label();
			this.lblText = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboStage = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboCharacter = new System.Windows.Forms.ComboBox();
			this.cboHand = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
			this.SuspendLayout();
			// 
			// picPortrait
			// 
			this.picPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picPortrait.Location = new System.Drawing.Point(3, 30);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(201, 469);
			this.picPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picPortrait.TabIndex = 0;
			this.picPortrait.TabStop = false;
			// 
			// lblName
			// 
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(3, 4);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(201, 23);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "Name";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblText
			// 
			this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblText.BackColor = System.Drawing.Color.Transparent;
			this.lblText.Location = new System.Drawing.Point(3, 502);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(201, 58);
			this.lblText.TabIndex = 2;
			this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 594);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Stage:";
			// 
			// cboStage
			// 
			this.cboStage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStage.FormattingEnabled = true;
			this.cboStage.Location = new System.Drawing.Point(69, 591);
			this.cboStage.Name = "cboStage";
			this.cboStage.Size = new System.Drawing.Size(135, 21);
			this.cboStage.TabIndex = 2;
			this.cboStage.SelectedIndexChanged += new System.EventHandler(this.cboStage_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 567);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Character:";
			// 
			// cboCharacter
			// 
			this.cboCharacter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCharacter.FormattingEnabled = true;
			this.cboCharacter.Location = new System.Drawing.Point(69, 564);
			this.cboCharacter.Name = "cboCharacter";
			this.cboCharacter.Size = new System.Drawing.Size(135, 21);
			this.cboCharacter.TabIndex = 1;
			this.cboCharacter.SelectedIndexChanged += new System.EventHandler(this.cboCharacter_SelectedIndexChanged);
			// 
			// cboHand
			// 
			this.cboHand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboHand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboHand.FormattingEnabled = true;
			this.cboHand.Items.AddRange(new object[] {
            "",
            "Nothing",
            "High Card",
            "One Pair",
            "Two Pair",
            "Three of a Kind",
            "Straight",
            "Flush",
            "Full House",
            "Four of a Kind",
            "Straight Flush",
            "Royal Flush"});
			this.cboHand.Location = new System.Drawing.Point(69, 618);
			this.cboHand.Name = "cboHand";
			this.cboHand.Size = new System.Drawing.Size(135, 21);
			this.cboHand.TabIndex = 6;
			this.cboHand.SelectedIndexChanged += new System.EventHandler(this.cboHand_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 621);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Hand:";
			// 
			// TestCharacter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboHand);
			this.Controls.Add(this.cboCharacter);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cboStage);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblText);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.picPortrait);
			this.Name = "TestCharacter";
			this.Size = new System.Drawing.Size(207, 642);
			((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picPortrait;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboStage;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboCharacter;
		private System.Windows.Forms.ComboBox cboHand;
		private System.Windows.Forms.Label label1;
	}
}

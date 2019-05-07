namespace SPNATI_Character_Editor.Activities
{
	partial class CharacterPreview
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
			this.lblLinesOfDialogue = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblSkin = new System.Windows.Forms.Label();
			this.cboSkin = new System.Windows.Forms.ComboBox();
			this.chkText = new System.Windows.Forms.CheckBox();
			this.picPortrait = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.SuspendLayout();
			// 
			// lblLinesOfDialogue
			// 
			this.lblLinesOfDialogue.AutoSize = true;
			this.lblLinesOfDialogue.Location = new System.Drawing.Point(129, -2);
			this.lblLinesOfDialogue.Name = "lblLinesOfDialogue";
			this.lblLinesOfDialogue.Size = new System.Drawing.Size(13, 13);
			this.lblLinesOfDialogue.TabIndex = 17;
			this.lblLinesOfDialogue.Text = "0";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(0, -2);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(123, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Unique lines of dialogue:";
			// 
			// lblSkin
			// 
			this.lblSkin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lblSkin.AutoSize = true;
			this.lblSkin.Location = new System.Drawing.Point(40, 620);
			this.lblSkin.Name = "lblSkin";
			this.lblSkin.Size = new System.Drawing.Size(31, 13);
			this.lblSkin.TabIndex = 18;
			this.lblSkin.Text = "Skin:";
			// 
			// cboSkin
			// 
			this.cboSkin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.cboSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSkin.FormattingEnabled = true;
			this.cboSkin.Location = new System.Drawing.Point(76, 617);
			this.cboSkin.Name = "cboSkin";
			this.cboSkin.Size = new System.Drawing.Size(121, 21);
			this.cboSkin.TabIndex = 19;
			this.cboSkin.SelectedIndexChanged += new System.EventHandler(this.cboSkin_SelectedIndexChanged);
			// 
			// chkText
			// 
			this.chkText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkText.AutoSize = true;
			this.chkText.Location = new System.Drawing.Point(176, -1);
			this.chkText.Name = "chkText";
			this.chkText.Size = new System.Drawing.Size(77, 17);
			this.chkText.TabIndex = 20;
			this.chkText.Text = "Show Text";
			this.chkText.UseVisualStyleBackColor = true;
			this.chkText.CheckedChanged += new System.EventHandler(this.chkText_CheckedChanged);
			// 
			// picPortrait
			// 
			this.picPortrait.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picPortrait.Location = new System.Drawing.Point(0, 0);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.ShowTextBox = false;
			this.picPortrait.Size = new System.Drawing.Size(251, 641);
			this.picPortrait.TabIndex = 15;
			this.picPortrait.TabStop = false;
			// 
			// CharacterPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkText);
			this.Controls.Add(this.cboSkin);
			this.Controls.Add(this.lblSkin);
			this.Controls.Add(this.lblLinesOfDialogue);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.picPortrait);
			this.Name = "CharacterPreview";
			this.Size = new System.Drawing.Size(251, 641);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblLinesOfDialogue;
		private System.Windows.Forms.Label label4;
		private SPNATI_Character_Editor.Controls.CharacterImageBox picPortrait;
		private System.Windows.Forms.Label lblSkin;
		private System.Windows.Forms.ComboBox cboSkin;
		private System.Windows.Forms.CheckBox chkText;
	}
}

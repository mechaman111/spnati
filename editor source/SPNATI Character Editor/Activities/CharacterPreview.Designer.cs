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
			// picPortrait
			// 
			this.picPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picPortrait.Location = new System.Drawing.Point(3, 74);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(242, 564);
			this.picPortrait.TabIndex = 15;
			this.picPortrait.TabStop = false;
			// 
			// CharacterPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
	}
}

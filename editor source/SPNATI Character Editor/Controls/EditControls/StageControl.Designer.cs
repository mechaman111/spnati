namespace SPNATI_Character_Editor
{
	partial class StageControl
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
			this.cboFrom = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboTo = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cboFrom
			// 
			this.cboFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboFrom.FormattingEnabled = true;
			this.cboFrom.Location = new System.Drawing.Point(42, 0);
			this.cboFrom.Name = "cboFrom";
			this.cboFrom.Size = new System.Drawing.Size(150, 21);
			this.cboFrom.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "From:";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(198, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(19, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "to:";
			// 
			// cboTo
			// 
			this.cboTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboTo.FormattingEnabled = true;
			this.cboTo.Location = new System.Drawing.Point(223, 0);
			this.cboTo.Name = "cboTo";
			this.cboTo.Size = new System.Drawing.Size(150, 21);
			this.cboTo.TabIndex = 3;
			// 
			// StageControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboTo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboFrom);
			this.Name = "StageControl";
			this.Size = new System.Drawing.Size(438, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboFrom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboTo;
	}
}

namespace SPNATI_Character_Editor.Activities
{
	partial class DictionaryEditor
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
			this.lstWords = new SPNATI_Character_Editor.Controls.SelectBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Words in Custom Dictionary:";
			// 
			// lstWords
			// 
			this.lstWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstWords.Location = new System.Drawing.Point(3, 19);
			this.lstWords.Name = "lstWords";
			this.lstWords.SelectedItems = new string[0];
			this.lstWords.Size = new System.Drawing.Size(292, 459);
			this.lstWords.TabIndex = 0;
			this.lstWords.ItemAdded += new System.EventHandler<object>(this.lstWords_ItemAdded);
			this.lstWords.ItemRemoved += new System.EventHandler<object>(this.lstWords_ItemRemoved);
			// 
			// DictionaryEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstWords);
			this.Name = "DictionaryEditor";
			this.Size = new System.Drawing.Size(420, 481);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.SelectBox lstWords;
		private System.Windows.Forms.Label label1;
	}
}

namespace Desktop.CommonControls
{
	partial class RecordField
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
			this.cmdSearch = new Desktop.Skinning.SkinnedIcon();
			this.txtInput = new Desktop.Skinning.SkinnedTextBox();
			this.SuspendLayout();
			// 
			// cmdSearch
			// 
			this.cmdSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdSearch.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdSearch.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cmdSearch.Flat = true;
			this.cmdSearch.ForeColor = System.Drawing.Color.Black;
			this.cmdSearch.Image = global::Desktop.Properties.Resources.Search;
			this.cmdSearch.Location = new System.Drawing.Point(131, 1);
			this.cmdSearch.Margin = new System.Windows.Forms.Padding(0);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(18, 18);
			this.cmdSearch.TabIndex = 3;
			this.cmdSearch.UseVisualStyleBackColor = true;
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// txtInput
			// 
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.txtInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtInput.BackColor = System.Drawing.Color.White;
			this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtInput.ForeColor = System.Drawing.Color.Black;
			this.txtInput.Location = new System.Drawing.Point(3, 3);
			this.txtInput.Margin = new System.Windows.Forms.Padding(0);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(126, 13);
			this.txtInput.TabIndex = 2;
			this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
			this.txtInput.Enter += new System.EventHandler(this.txtInput_Enter);
			this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
			this.txtInput.Leave += new System.EventHandler(this.txtInput_Leave);
			this.txtInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtInput_MouseUp);
			// 
			// RecordField
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdSearch);
			this.Controls.Add(this.txtInput);
			this.Name = "RecordField";
			this.Size = new System.Drawing.Size(150, 20);
			this.Load += new System.EventHandler(this.RecordField_Load);
			this.Validating += new System.ComponentModel.CancelEventHandler(this.RecordField_Validating);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedIcon cmdSearch;
		private Desktop.Skinning.SkinnedTextBox txtInput;
	}
}

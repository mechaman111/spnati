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
			this.cmdSearch = new System.Windows.Forms.Button();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmdSearch
			// 
			this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSearch.Location = new System.Drawing.Point(129, -1);
			this.cmdSearch.Margin = new System.Windows.Forms.Padding(0);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(22, 22);
			this.cmdSearch.TabIndex = 3;
			this.cmdSearch.Text = "🔍";
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
			this.txtInput.Location = new System.Drawing.Point(0, 0);
			this.txtInput.Margin = new System.Windows.Forms.Padding(0);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(129, 20);
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

		private System.Windows.Forms.Button cmdSearch;
		private System.Windows.Forms.TextBox txtInput;
	}
}

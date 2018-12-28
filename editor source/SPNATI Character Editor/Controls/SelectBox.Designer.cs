namespace SPNATI_Character_Editor.Controls
{
	partial class SelectBox
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
			this.lstSelectedItems = new System.Windows.Forms.ListBox();
			this.cboSelectableItems = new System.Windows.Forms.ComboBox();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstSelectedItems
			// 
			this.lstSelectedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstSelectedItems.FormattingEnabled = true;
			this.lstSelectedItems.IntegralHeight = false;
			this.lstSelectedItems.Location = new System.Drawing.Point(0, 27);
			this.lstSelectedItems.Name = "lstSelectedItems";
			this.lstSelectedItems.Size = new System.Drawing.Size(223, 92);
			this.lstSelectedItems.Sorted = true;
			this.lstSelectedItems.TabIndex = 0;
			this.lstSelectedItems.SelectedIndexChanged += new System.EventHandler(this.lstSelectedItems_SelectedIndexChanged);
			// 
			// cboSelectableItems
			// 
			this.cboSelectableItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboSelectableItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboSelectableItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboSelectableItems.FormattingEnabled = true;
			this.cboSelectableItems.Location = new System.Drawing.Point(0, 0);
			this.cboSelectableItems.Name = "cboSelectableItems";
			this.cboSelectableItems.Size = new System.Drawing.Size(223, 21);
			this.cboSelectableItems.Sorted = true;
			this.cboSelectableItems.TabIndex = 1;
			this.cboSelectableItems.SelectedIndexChanged += new System.EventHandler(this.cboSelectableItems_SelectedIndexChanged);
			this.cboSelectableItems.TextUpdate += new System.EventHandler(this.cboSelectableItems_TextUpdate);
			this.cboSelectableItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSelectableItems_KeyDown);
			// 
			// cmdAdd
			// 
			this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAdd.Enabled = false;
			this.cmdAdd.Location = new System.Drawing.Point(229, 0);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(63, 23);
			this.cmdAdd.TabIndex = 2;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// cmdRemove
			// 
			this.cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRemove.Enabled = false;
			this.cmdRemove.Location = new System.Drawing.Point(229, 27);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(63, 23);
			this.cmdRemove.TabIndex = 3;
			this.cmdRemove.Text = "Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			// 
			// SelectBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdRemove);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.cboSelectableItems);
			this.Controls.Add(this.lstSelectedItems);
			this.Name = "SelectBox";
			this.Size = new System.Drawing.Size(292, 119);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lstSelectedItems;
		private System.Windows.Forms.ComboBox cboSelectableItems;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Button cmdRemove;
	}
}

namespace SPNATI_Character_Editor.Controls
{
	partial class RecordSelectBox
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
			this.lstSelectedItems = new Desktop.Skinning.SkinnedListBox();
			this.cmdAdd = new Desktop.Skinning.SkinnedButton();
			this.cmdRemove = new Desktop.Skinning.SkinnedButton();
			this.recField = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
			// 
			// lstSelectedItems
			// 
			this.lstSelectedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstSelectedItems.BackColor = System.Drawing.Color.White;
			this.lstSelectedItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstSelectedItems.ForeColor = System.Drawing.Color.Black;
			this.lstSelectedItems.FormattingEnabled = true;
			this.lstSelectedItems.IntegralHeight = false;
			this.lstSelectedItems.Location = new System.Drawing.Point(0, 27);
			this.lstSelectedItems.Name = "lstSelectedItems";
			this.lstSelectedItems.Size = new System.Drawing.Size(205, 92);
			this.lstSelectedItems.Sorted = true;
			this.lstSelectedItems.TabIndex = 0;
			this.lstSelectedItems.SelectedIndexChanged += new System.EventHandler(this.lstSelectedItems_SelectedIndexChanged);
			// 
			// cmdAdd
			// 
			this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAdd.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAdd.Enabled = false;
			this.cmdAdd.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdAdd.Flat = false;
			this.cmdAdd.Location = new System.Drawing.Point(211, 0);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(79, 23);
			this.cmdAdd.TabIndex = 2;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// cmdRemove
			// 
			this.cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRemove.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdRemove.Enabled = false;
			this.cmdRemove.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdRemove.Flat = false;
			this.cmdRemove.Location = new System.Drawing.Point(211, 27);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(79, 23);
			this.cmdRemove.TabIndex = 3;
			this.cmdRemove.Text = "Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			// 
			// recField
			// 
			this.recField.AllowCreate = false;
			this.recField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.recField.Location = new System.Drawing.Point(0, 2);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordFilter = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(205, 20);
			this.recField.TabIndex = 4;
			this.recField.UseAutoComplete = false;
			this.recField.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recField_RecordChanged);
			// 
			// RecordSelectBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.recField);
			this.Controls.Add(this.cmdRemove);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.lstSelectedItems);
			this.Name = "RecordSelectBox";
			this.Size = new System.Drawing.Size(292, 119);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedListBox lstSelectedItems;
		private Desktop.Skinning.SkinnedButton cmdAdd;
		private Desktop.Skinning.SkinnedButton cmdRemove;
		private Desktop.CommonControls.RecordField recField;
	}
}

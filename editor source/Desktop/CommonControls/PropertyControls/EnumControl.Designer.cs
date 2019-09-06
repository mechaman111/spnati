namespace Desktop.CommonControls.PropertyControls
{
	partial class EnumControl
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
			this.cboItems = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// cboItems
			// 
			this.cboItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboItems.BackColor = System.Drawing.Color.White;
			this.cboItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboItems.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboItems.FormattingEnabled = true;
			this.cboItems.Location = new System.Drawing.Point(0, 0);
			this.cboItems.Name = "cboItems";
			this.cboItems.SelectedIndex = -1;
			this.cboItems.SelectedItem = null;
			this.cboItems.Size = new System.Drawing.Size(175, 25);
			this.cboItems.Sorted = false;
			this.cboItems.TabIndex = 0;
			// 
			// ComboBoxControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboItems);
			this.Name = "ComboBoxControl";
			this.Size = new System.Drawing.Size(175, 25);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboItems;
	}
}

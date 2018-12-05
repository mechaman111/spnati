namespace Desktop.CommonControls.PropertyControls
{
	partial class ComboBoxControl
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
			this.cboItems = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cboItems
			// 
			this.cboItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboItems.FormattingEnabled = true;
			this.cboItems.Location = new System.Drawing.Point(0, 0);
			this.cboItems.Name = "cboItems";
			this.cboItems.Size = new System.Drawing.Size(175, 21);
			this.cboItems.TabIndex = 0;
			this.cboItems.SelectedIndexChanged += new System.EventHandler(this.cboItems_SelectedIndexChanged);
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

		private System.Windows.Forms.ComboBox cboItems;
	}
}

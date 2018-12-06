namespace Desktop.CommonControls
{
	partial class EnumSelect
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
			this.lstItems = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// lstItems
			// 
			this.lstItems.CheckBoxes = true;
			this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(150, 150);
			this.lstItems.TabIndex = 0;
			this.lstItems.UseCompatibleStateImageBehavior = false;
			this.lstItems.View = System.Windows.Forms.View.List;
			// 
			// EnumSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lstItems);
			this.Name = "EnumSelect";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstItems;
	}
}

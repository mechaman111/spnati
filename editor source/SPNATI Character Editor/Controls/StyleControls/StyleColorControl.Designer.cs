namespace SPNATI_Character_Editor.Controls.StyleControls
{
	partial class StyleColorControl
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
			this.fldColor = new Desktop.CommonControls.ColorField();
			this.SuspendLayout();
			// 
			// fldColor
			// 
			this.fldColor.BackColor = System.Drawing.SystemColors.Control;
			this.fldColor.Color = System.Drawing.SystemColors.Control;
			this.fldColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.fldColor.Location = new System.Drawing.Point(3, 0);
			this.fldColor.Name = "fldColor";
			this.fldColor.Size = new System.Drawing.Size(75, 21);
			this.fldColor.TabIndex = 1;
			this.fldColor.UseVisualStyleBackColor = true;
			// 
			// StyleColorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fldColor);
			this.Name = "StyleColorControl";
			this.Size = new System.Drawing.Size(265, 21);
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.CommonControls.ColorField fldColor;
	}
}

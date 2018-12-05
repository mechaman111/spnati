namespace SPNATI_Character_Editor.Activities
{
	partial class ValidateAllActivity
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
			this.ctlValidation = new SPNATI_Character_Editor.Controls.ValidationControl();
			this.SuspendLayout();
			// 
			// ctlValidation
			// 
			this.ctlValidation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlValidation.Location = new System.Drawing.Point(0, 0);
			this.ctlValidation.Name = "ctlValidation";
			this.ctlValidation.Size = new System.Drawing.Size(1111, 631);
			this.ctlValidation.TabIndex = 0;
			// 
			// ValidateAllActivity
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ctlValidation);
			this.Name = "ValidateAllActivity";
			this.Size = new System.Drawing.Size(1111, 631);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.ValidationControl ctlValidation;
	}
}

namespace Desktop.CommonControls.PropertyControls
{
	partial class ColorControl
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
			this.colorPicker = new System.Windows.Forms.ColorDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdColor = new System.Windows.Forms.Button();
			this.txtValue = new Desktop.CommonControls.TextField();
			this.SuspendLayout();
			// 
			// colorPicker
			// 
			this.colorPicker.AnyColor = true;
			this.colorPicker.SolidColorOnly = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(87, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "#";
			// 
			// cmdColor
			// 
			this.cmdColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmdColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdColor.Location = new System.Drawing.Point(0, 0);
			this.cmdColor.Name = "cmdColor";
			this.cmdColor.Size = new System.Drawing.Size(86, 20);
			this.cmdColor.TabIndex = 0;
			this.cmdColor.UseVisualStyleBackColor = true;
			this.cmdColor.Click += new System.EventHandler(this.CmdColor_Click);
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(102, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(49, 20);
			this.txtValue.TabIndex = 3;
			// 
			// ColorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdColor);
			this.Name = "ColorControl";
			this.Size = new System.Drawing.Size(205, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdColor;
		private System.Windows.Forms.ColorDialog colorPicker;
		private System.Windows.Forms.Label label1;
		private TextField txtValue;
	}
}

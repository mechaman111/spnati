namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class ParticleColorControl
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
			this.txtValue = new Desktop.CommonControls.TextField();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdColor = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtValue2 = new Desktop.CommonControls.TextField();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdColor2 = new System.Windows.Forms.Button();
			this.colorPicker = new System.Windows.Forms.ColorDialog();
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(58, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(49, 20);
			this.txtValue.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "#";
			// 
			// cmdColor
			// 
			this.cmdColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmdColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdColor.Location = new System.Drawing.Point(0, 0);
			this.cmdColor.Name = "cmdColor";
			this.cmdColor.Size = new System.Drawing.Size(44, 20);
			this.cmdColor.TabIndex = 4;
			this.cmdColor.UseVisualStyleBackColor = true;
			this.cmdColor.Click += new System.EventHandler(this.cmdColor_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(113, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "to";
			// 
			// txtValue2
			// 
			this.txtValue2.Location = new System.Drawing.Point(190, 0);
			this.txtValue2.Name = "txtValue2";
			this.txtValue2.Size = new System.Drawing.Size(49, 20);
			this.txtValue2.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(176, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(14, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "#";
			// 
			// cmdColor2
			// 
			this.cmdColor2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmdColor2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdColor2.Location = new System.Drawing.Point(132, 0);
			this.cmdColor2.Name = "cmdColor2";
			this.cmdColor2.Size = new System.Drawing.Size(44, 20);
			this.cmdColor2.TabIndex = 8;
			this.cmdColor2.UseVisualStyleBackColor = true;
			this.cmdColor2.Click += new System.EventHandler(this.cmdColor_Click);
			// 
			// colorPicker
			// 
			this.colorPicker.AnyColor = true;
			this.colorPicker.SolidColorOnly = true;
			// 
			// ParticleColorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdColor2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdColor);
			this.Name = "ParticleColorControl";
			this.Size = new System.Drawing.Size(328, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.TextField txtValue;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdColor;
		private System.Windows.Forms.Label label2;
		private Desktop.CommonControls.TextField txtValue2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdColor2;
		private System.Windows.Forms.ColorDialog colorPicker;
	}
}

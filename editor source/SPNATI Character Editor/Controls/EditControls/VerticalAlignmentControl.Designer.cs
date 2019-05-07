namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class VerticalAlignmentControl
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
			this.components = new System.ComponentModel.Container();
			this.chkTop = new System.Windows.Forms.RadioButton();
			this.chkMiddle = new System.Windows.Forms.RadioButton();
			this.chkBottom = new System.Windows.Forms.RadioButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// chkTop
			// 
			this.chkTop.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkTop.AutoSize = true;
			this.chkTop.Image = global::SPNATI_Character_Editor.Properties.Resources.AlignTop;
			this.chkTop.Location = new System.Drawing.Point(-1, -1);
			this.chkTop.Name = "chkTop";
			this.chkTop.Size = new System.Drawing.Size(22, 22);
			this.chkTop.TabIndex = 1;
			this.toolTip1.SetToolTip(this.chkTop, "Top");
			this.chkTop.UseVisualStyleBackColor = true;
			// 
			// chkMiddle
			// 
			this.chkMiddle.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkMiddle.AutoSize = true;
			this.chkMiddle.Image = global::SPNATI_Character_Editor.Properties.Resources.AlignStretchVertical;
			this.chkMiddle.Location = new System.Drawing.Point(27, -1);
			this.chkMiddle.Name = "chkMiddle";
			this.chkMiddle.Size = new System.Drawing.Size(22, 22);
			this.chkMiddle.TabIndex = 2;
			this.toolTip1.SetToolTip(this.chkMiddle, "Center");
			this.chkMiddle.UseVisualStyleBackColor = true;
			// 
			// chkBottom
			// 
			this.chkBottom.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkBottom.AutoSize = true;
			this.chkBottom.Image = global::SPNATI_Character_Editor.Properties.Resources.AlignBottom;
			this.chkBottom.Location = new System.Drawing.Point(55, -1);
			this.chkBottom.Name = "chkBottom";
			this.chkBottom.Size = new System.Drawing.Size(22, 22);
			this.chkBottom.TabIndex = 3;
			this.toolTip1.SetToolTip(this.chkBottom, "Bottom");
			this.chkBottom.UseVisualStyleBackColor = true;
			// 
			// VerticalAlignmentControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkBottom);
			this.Controls.Add(this.chkMiddle);
			this.Controls.Add(this.chkTop);
			this.Name = "VerticalAlignmentControl";
			this.Size = new System.Drawing.Size(278, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.RadioButton chkTop;
		private System.Windows.Forms.RadioButton chkMiddle;
		private System.Windows.Forms.RadioButton chkBottom;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

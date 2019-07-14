namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class HorizontalAlignmentControl
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
			this.chkRight = new Desktop.Skinning.SkinnedRadioButton();
			this.chkMiddle = new Desktop.Skinning.SkinnedRadioButton();
			this.chkLeft = new Desktop.Skinning.SkinnedRadioButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// chkRight
			// 
			this.chkRight.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkRight.AutoSize = true;
			this.chkRight.Image = global::SPNATI_Character_Editor.Properties.Resources.AlignRight;
			this.chkRight.Location = new System.Drawing.Point(55, -1);
			this.chkRight.Name = "chkRight";
			this.chkRight.Size = new System.Drawing.Size(22, 22);
			this.chkRight.TabIndex = 3;
			this.toolTip1.SetToolTip(this.chkRight, "Right");
			this.chkRight.UseVisualStyleBackColor = true;
			// 
			// chkMiddle
			// 
			this.chkMiddle.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkMiddle.AutoSize = true;
			this.chkMiddle.Image = global::SPNATI_Character_Editor.Properties.Resources.AlignStretchHorizontal;
			this.chkMiddle.Location = new System.Drawing.Point(27, -1);
			this.chkMiddle.Name = "chkMiddle";
			this.chkMiddle.Size = new System.Drawing.Size(22, 22);
			this.chkMiddle.TabIndex = 2;
			this.toolTip1.SetToolTip(this.chkMiddle, "Center");
			this.chkMiddle.UseVisualStyleBackColor = true;
			// 
			// chkLeft
			// 
			this.chkLeft.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkLeft.AutoSize = true;
			this.chkLeft.Image = global::SPNATI_Character_Editor.Properties.Resources.AlignLeft;
			this.chkLeft.Location = new System.Drawing.Point(-1, -1);
			this.chkLeft.Name = "chkLeft";
			this.chkLeft.Size = new System.Drawing.Size(22, 22);
			this.chkLeft.TabIndex = 1;
			this.toolTip1.SetToolTip(this.chkLeft, "Left");
			this.chkLeft.UseVisualStyleBackColor = true;
			// 
			// HorizontalAlignmentControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkRight);
			this.Controls.Add(this.chkMiddle);
			this.Controls.Add(this.chkLeft);
			this.Name = "HorizontalAlignmentControl";
			this.Size = new System.Drawing.Size(278, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Desktop.Skinning.SkinnedRadioButton chkLeft;
		private Desktop.Skinning.SkinnedRadioButton chkMiddle;
		private Desktop.Skinning.SkinnedRadioButton chkRight;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

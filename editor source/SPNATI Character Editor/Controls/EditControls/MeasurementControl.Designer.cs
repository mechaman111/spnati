namespace SPNATI_Character_Editor.EditControls
{
	partial class MeasurementControl
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
			this.valValue = new Desktop.Skinning.SkinnedNumericUpDown();
			this.radPx = new Desktop.Skinning.SkinnedRadioButton();
			this.radPct = new Desktop.Skinning.SkinnedRadioButton();
			this.chkCentered = new Desktop.Skinning.SkinnedCheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.lblPct = new Desktop.Skinning.SkinnedLabel();
			((System.ComponentModel.ISupportInitialize)(this.valValue)).BeginInit();
			this.SuspendLayout();
			// 
			// valValue
			// 
			this.valValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.valValue.Location = new System.Drawing.Point(0, 0);
			this.valValue.Maximum = new decimal(new int[] {
			100000,
			0,
			0,
			0});
			this.valValue.Minimum = new decimal(new int[] {
			100000,
			0,
			0,
			-2147483648});
			this.valValue.Name = "valValue";
			this.valValue.Size = new System.Drawing.Size(54, 20);
			this.valValue.TabIndex = 0;
			// 
			// radPx
			// 
			this.radPx.AutoSize = true;
			this.radPx.Checked = true;
			this.radPx.Location = new System.Drawing.Point(59, 1);
			this.radPx.Name = "radPx";
			this.radPx.Size = new System.Drawing.Size(36, 17);
			this.radPx.TabIndex = 1;
			this.radPx.TabStop = true;
			this.radPx.Tag = "px";
			this.radPx.Text = "px";
			this.radPx.UseVisualStyleBackColor = true;
			// 
			// radPct
			// 
			this.radPct.AutoSize = true;
			this.radPct.Location = new System.Drawing.Point(95, 1);
			this.radPct.Name = "radPct";
			this.radPct.Size = new System.Drawing.Size(33, 17);
			this.radPct.TabIndex = 2;
			this.radPct.Tag = "%";
			this.radPct.Text = "%";
			this.radPct.UseVisualStyleBackColor = true;
			// 
			// chkCentered
			// 
			this.chkCentered.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkCentered.Image = Properties.Resources.Center;
			this.chkCentered.Location = new System.Drawing.Point(127, 0);
			this.chkCentered.Name = "chkCentered";
			this.chkCentered.Size = new System.Drawing.Size(22, 20);
			this.chkCentered.TabIndex = 3;
			this.toolTip1.SetToolTip(this.chkCentered, "Center on screen");
			this.chkCentered.UseVisualStyleBackColor = true;
			this.chkCentered.Visible = false;
			// 
			// lblPct
			// 
			this.lblPct.AutoSize = true;
			this.lblPct.Location = new System.Drawing.Point(155, 2);
			this.lblPct.Name = "lblPct";
			this.lblPct.Size = new System.Drawing.Size(15, 13);
			this.lblPct.TabIndex = 4;
			this.lblPct.Text = "%";
			this.lblPct.Visible = false;
			// 
			// MeasurementControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblPct);
			this.Controls.Add(this.chkCentered);
			this.Controls.Add(this.radPct);
			this.Controls.Add(this.radPx);
			this.Controls.Add(this.valValue);
			this.Name = "MeasurementControl";
			this.Size = new System.Drawing.Size(362, 20);
			((System.ComponentModel.ISupportInitialize)(this.valValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedNumericUpDown valValue;
		private Desktop.Skinning.SkinnedRadioButton radPx;
		private Desktop.Skinning.SkinnedRadioButton radPct;
		private Desktop.Skinning.SkinnedCheckBox chkCentered;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedLabel lblPct;
	}
}

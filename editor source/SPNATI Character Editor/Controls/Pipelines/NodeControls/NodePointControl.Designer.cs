namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodePointControl
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
			this.lblX = new Desktop.Skinning.SkinnedLabel();
			this.valX = new Desktop.Skinning.SkinnedTextBox();
			this.valY = new Desktop.Skinning.SkinnedTextBox();
			this.lblY = new Desktop.Skinning.SkinnedLabel();
			this.tmrDebounce = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// lblX
			// 
			this.lblX.AutoSize = true;
			this.lblX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblX.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.lblX.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblX.Location = new System.Drawing.Point(-3, 2);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(17, 13);
			this.lblX.TabIndex = 0;
			this.lblX.Text = "X:";
			// 
			// valX
			// 
			this.valX.BackColor = System.Drawing.Color.White;
			this.valX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valX.ForeColor = System.Drawing.Color.Black;
			this.valX.Location = new System.Drawing.Point(13, 0);
			this.valX.Name = "valX";
			this.valX.Size = new System.Drawing.Size(28, 20);
			this.valX.TabIndex = 1;
			// 
			// valY
			// 
			this.valY.BackColor = System.Drawing.Color.White;
			this.valY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valY.ForeColor = System.Drawing.Color.Black;
			this.valY.Location = new System.Drawing.Point(57, 0);
			this.valY.Name = "valY";
			this.valY.Size = new System.Drawing.Size(28, 20);
			this.valY.TabIndex = 3;
			// 
			// lblY
			// 
			this.lblY.AutoSize = true;
			this.lblY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblY.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.lblY.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblY.Location = new System.Drawing.Point(41, 2);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(17, 13);
			this.lblY.TabIndex = 2;
			this.lblY.Text = "Y:";
			// 
			// tmrDebounce
			// 
			this.tmrDebounce.Interval = 300;
			this.tmrDebounce.Tick += new System.EventHandler(this.tmrDebounce_Tick);
			// 
			// NodePointControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valY);
			this.Controls.Add(this.lblY);
			this.Controls.Add(this.valX);
			this.Controls.Add(this.lblX);
			this.Name = "NodePointControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblX;
		private Desktop.Skinning.SkinnedTextBox valX;
		private Desktop.Skinning.SkinnedTextBox valY;
		private Desktop.Skinning.SkinnedLabel lblY;
		private System.Windows.Forms.Timer tmrDebounce;
	}
}

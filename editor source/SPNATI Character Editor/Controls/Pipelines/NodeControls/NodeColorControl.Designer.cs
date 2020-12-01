namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodeColorControl
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
			this.colorField1 = new Desktop.CommonControls.ColorField();
			this.SuspendLayout();
			// 
			// colorField1
			// 
			this.colorField1.Color = System.Drawing.SystemColors.Control;
			this.colorField1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.colorField1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.colorField1.Location = new System.Drawing.Point(0, 0);
			this.colorField1.Name = "colorField1";
			this.colorField1.Size = new System.Drawing.Size(150, 21);
			this.colorField1.TabIndex = 0;
			this.colorField1.UseVisualStyleBackColor = true;
			// 
			// NodeColorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.colorField1);
			this.Name = "NodeColorControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.ColorField colorField1;
	}
}

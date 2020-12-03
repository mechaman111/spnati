namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodeTextControl
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
			this.txtValue = new Desktop.Skinning.SkinnedTextBox();
			this.tmrDebounce = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.BackColor = System.Drawing.Color.White;
			this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtValue.ForeColor = System.Drawing.Color.Black;
			this.txtValue.Location = new System.Drawing.Point(0, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(150, 20);
			this.txtValue.TabIndex = 0;
			// 
			// tmrDebounce
			// 
			this.tmrDebounce.Interval = 300;
			this.tmrDebounce.Tick += new System.EventHandler(this.tmrDebounce_Tick);
			// 
			// NodeTextControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtValue);
			this.Name = "NodeTextControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedTextBox txtValue;
		private System.Windows.Forms.Timer tmrDebounce;
	}
}

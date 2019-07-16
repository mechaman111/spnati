namespace Desktop.Reporting.Controls
{
	partial class ComboSlicerControl
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
			this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.mnuMerge = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SuspendLayout();
			// 
			// flowPanel
			// 
			this.flowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowPanel.AutoScroll = true;
			this.flowPanel.Location = new System.Drawing.Point(3, 0);
			this.flowPanel.Name = "flowPanel";
			this.flowPanel.Size = new System.Drawing.Size(325, 150);
			this.flowPanel.TabIndex = 3;
			// 
			// mnuMerge
			// 
			this.mnuMerge.Name = "mnuMerge";
			this.mnuMerge.Size = new System.Drawing.Size(61, 4);
			this.mnuMerge.Tag = "Surface";
			// 
			// ComboSlicerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.flowPanel);
			this.Name = "ComboSlicerControl";
			this.Size = new System.Drawing.Size(331, 150);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowPanel;
		private System.Windows.Forms.ContextMenuStrip mnuMerge;
	}
}

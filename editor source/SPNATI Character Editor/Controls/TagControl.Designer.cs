namespace SPNATI_Character_Editor.Controls
{
	partial class TagControl
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
			this.grpBox = new System.Windows.Forms.GroupBox();
			this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.grpBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBox
			// 
			this.grpBox.Controls.Add(this.flowPanel);
			this.grpBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpBox.Location = new System.Drawing.Point(0, 0);
			this.grpBox.Name = "grpBox";
			this.grpBox.Size = new System.Drawing.Size(185, 108);
			this.grpBox.TabIndex = 0;
			this.grpBox.TabStop = false;
			this.grpBox.Text = "Group Label";
			// 
			// flowPanel
			// 
			this.flowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowPanel.AutoSize = true;
			this.flowPanel.BackColor = System.Drawing.SystemColors.Control;
			this.flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.flowPanel.Location = new System.Drawing.Point(3, 19);
			this.flowPanel.Name = "flowPanel";
			this.flowPanel.Padding = new System.Windows.Forms.Padding(2);
			this.flowPanel.Size = new System.Drawing.Size(179, 83);
			this.flowPanel.TabIndex = 0;
			this.flowPanel.WrapContents = false;
			// 
			// TagControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpBox);
			this.Name = "TagControl";
			this.Size = new System.Drawing.Size(185, 108);
			this.grpBox.ResumeLayout(false);
			this.grpBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpBox;
		private System.Windows.Forms.FlowLayoutPanel flowPanel;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

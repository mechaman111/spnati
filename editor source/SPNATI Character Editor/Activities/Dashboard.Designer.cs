namespace SPNATI_Character_Editor.Activities
{
	partial class Dashboard
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.colLeft = new System.Windows.Forms.SplitContainer();
			this.colRight = new System.Windows.Forms.SplitContainer();
			this.tooltipPartners = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colLeft)).BeginInit();
			this.colLeft.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colRight)).BeginInit();
			this.colRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(6);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.colLeft);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.colRight);
			this.splitContainer1.Size = new System.Drawing.Size(817, 548);
			this.splitContainer1.SplitterDistance = 397;
			this.splitContainer1.TabIndex = 1;
			// 
			// colLeft
			// 
			this.colLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.colLeft.Location = new System.Drawing.Point(0, 0);
			this.colLeft.Name = "colLeft";
			this.colLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// colLeft.Panel1
			// 
			this.colLeft.Panel1.Padding = new System.Windows.Forms.Padding(6);
			// 
			// colLeft.Panel2
			// 
			this.colLeft.Panel2.Padding = new System.Windows.Forms.Padding(6);
			this.colLeft.Size = new System.Drawing.Size(397, 548);
			this.colLeft.SplitterDistance = 304;
			this.colLeft.TabIndex = 0;
			// 
			// colRight
			// 
			this.colRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.colRight.Location = new System.Drawing.Point(0, 0);
			this.colRight.Name = "colRight";
			this.colRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// colRight.Panel1
			// 
			this.colRight.Panel1.Padding = new System.Windows.Forms.Padding(6);
			// 
			// colRight.Panel2
			// 
			this.colRight.Panel2.Padding = new System.Windows.Forms.Padding(6);
			this.colRight.Size = new System.Drawing.Size(416, 548);
			this.colRight.SplitterDistance = 289;
			this.colRight.TabIndex = 0;
			// 
			// Dashboard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "Dashboard";
			this.Size = new System.Drawing.Size(817, 548);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.colLeft)).EndInit();
			this.colLeft.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.colRight)).EndInit();
			this.colRight.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer colLeft;
		private System.Windows.Forms.SplitContainer colRight;
		private System.Windows.Forms.ToolTip tooltipPartners;
	}
}

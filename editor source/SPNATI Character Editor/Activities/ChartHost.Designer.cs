namespace SPNATI_Character_Editor.Activities
{
	partial class ChartHost
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstGraph = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.viewPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.lblViews = new System.Windows.Forms.Label();
			this.chartContainer = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstGraph);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.viewPanel);
			this.splitContainer1.Panel2.Controls.Add(this.lblViews);
			this.splitContainer1.Panel2.Controls.Add(this.chartContainer);
			this.splitContainer1.Size = new System.Drawing.Size(1206, 743);
			this.splitContainer1.SplitterDistance = 215;
			this.splitContainer1.TabIndex = 1;
			// 
			// lstGraph
			// 
			this.lstGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstGraph.FormattingEnabled = true;
			this.lstGraph.Location = new System.Drawing.Point(3, 27);
			this.lstGraph.Name = "lstGraph";
			this.lstGraph.Size = new System.Drawing.Size(205, 706);
			this.lstGraph.TabIndex = 1;
			this.lstGraph.SelectedIndexChanged += new System.EventHandler(this.lstGraph_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Choose a Graph:";
			// 
			// viewPanel
			// 
			this.viewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.viewPanel.Location = new System.Drawing.Point(48, 3);
			this.viewPanel.Name = "viewPanel";
			this.viewPanel.Size = new System.Drawing.Size(932, 30);
			this.viewPanel.TabIndex = 2;
			// 
			// lblViews
			// 
			this.lblViews.AutoSize = true;
			this.lblViews.Location = new System.Drawing.Point(4, 8);
			this.lblViews.Name = "lblViews";
			this.lblViews.Size = new System.Drawing.Size(38, 13);
			this.lblViews.TabIndex = 1;
			this.lblViews.Text = "Views:";
			// 
			// chartContainer
			// 
			this.chartContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chartContainer.Location = new System.Drawing.Point(3, 39);
			this.chartContainer.Name = "chartContainer";
			this.chartContainer.Size = new System.Drawing.Size(977, 699);
			this.chartContainer.TabIndex = 0;
			// 
			// ChartHost
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ChartHost";
			this.Size = new System.Drawing.Size(1206, 743);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox lstGraph;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel viewPanel;
		private System.Windows.Forms.Label lblViews;
		private System.Windows.Forms.Panel chartContainer;
	}
}

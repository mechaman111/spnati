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
			this.lstGraph = new Desktop.Skinning.SkinnedListBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.viewPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.lblViews = new Desktop.Skinning.SkinnedLabel();
			this.chartContainer = new Desktop.Skinning.SkinnedPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.skinnedPanel1);
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
			this.lstGraph.BackColor = System.Drawing.Color.White;
			this.lstGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstGraph.ForeColor = System.Drawing.Color.Black;
			this.lstGraph.FormattingEnabled = true;
			this.lstGraph.Location = new System.Drawing.Point(3, 33);
			this.lstGraph.Name = "lstGraph";
			this.lstGraph.Size = new System.Drawing.Size(205, 693);
			this.lstGraph.TabIndex = 1;
			this.lstGraph.SelectedIndexChanged += new System.EventHandler(this.lstGraph_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label1.Location = new System.Drawing.Point(0, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 21);
			this.label1.TabIndex = 0;
			this.label1.Text = "Choose a Graph:";
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.viewPanel);
			this.skinnedPanel1.Controls.Add(this.lblViews);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 0);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.skinnedPanel1.Size = new System.Drawing.Size(983, 33);
			this.skinnedPanel1.TabIndex = 3;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// viewPanel
			// 
			this.viewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.viewPanel.Location = new System.Drawing.Point(50, 0);
			this.viewPanel.Name = "viewPanel";
			this.viewPanel.Size = new System.Drawing.Size(933, 30);
			this.viewPanel.TabIndex = 2;
			// 
			// lblViews
			// 
			this.lblViews.AutoSize = true;
			this.lblViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblViews.ForeColor = System.Drawing.Color.Black;
			this.lblViews.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblViews.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblViews.Location = new System.Drawing.Point(3, 9);
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
			this.chartContainer.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.chartContainer.Size = new System.Drawing.Size(977, 699);
			this.chartContainer.TabIndex = 0;
			this.chartContainer.TabSide = Desktop.Skinning.TabSide.None;
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
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private Desktop.Skinning.SkinnedListBox lstGraph;
		private Desktop.Skinning.SkinnedLabel label1;
		private System.Windows.Forms.FlowLayoutPanel viewPanel;
		private Desktop.Skinning.SkinnedLabel lblViews;
		private Desktop.Skinning.SkinnedPanel chartContainer;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}

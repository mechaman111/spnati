namespace SPNATI_Character_Editor.Controls.SlicerControls
{
	partial class IntervalSlicerControl
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
			this.sliderSplits = new SPNATI_Character_Editor.Controls.IntervalSlider();
			((System.ComponentModel.ISupportInitialize)(this.sliderSplits)).BeginInit();
			this.SuspendLayout();
			// 
			// flowPanel
			// 
			this.flowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowPanel.Location = new System.Drawing.Point(3, 30);
			this.flowPanel.Name = "flowPanel";
			this.flowPanel.Size = new System.Drawing.Size(257, 355);
			this.flowPanel.TabIndex = 4;
			// 
			// mnuMerge
			// 
			this.mnuMerge.Name = "mnuMerge";
			this.mnuMerge.Size = new System.Drawing.Size(61, 4);
			this.mnuMerge.Tag = "Surface";
			// 
			// sliderSplits
			// 
			this.sliderSplits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sliderSplits.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.sliderSplits.Location = new System.Drawing.Point(3, 3);
			this.sliderSplits.Maximum = 10;
			this.sliderSplits.Minimum = 0;
			this.sliderSplits.Name = "sliderSplits";
			this.sliderSplits.Size = new System.Drawing.Size(257, 21);
			this.sliderSplits.TabIndex = 6;
			// 
			// IntervalSlicerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.sliderSplits);
			this.Controls.Add(this.flowPanel);
			this.Name = "IntervalSlicerControl";
			this.Size = new System.Drawing.Size(263, 385);
			((System.ComponentModel.ISupportInitialize)(this.sliderSplits)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowPanel;
		private System.Windows.Forms.ContextMenuStrip mnuMerge;
		private IntervalSlider sliderSplits;
	}
}

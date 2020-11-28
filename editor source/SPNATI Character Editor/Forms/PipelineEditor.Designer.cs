namespace SPNATI_Character_Editor.Forms
{
	partial class PipelineEditor
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.graphEditor1 = new SPNATI_Character_Editor.Controls.Pipelines.GraphEditor();
			this.SuspendLayout();
			// 
			// graphEditor1
			// 
			this.graphEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.graphEditor1.Location = new System.Drawing.Point(1, 27);
			this.graphEditor1.Margin = new System.Windows.Forms.Padding(0);
			this.graphEditor1.Name = "graphEditor1";
			this.graphEditor1.Size = new System.Drawing.Size(1228, 669);
			this.graphEditor1.TabIndex = 0;
			// 
			// PipelineEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1232, 698);
			this.Controls.Add(this.graphEditor1);
			this.Name = "PipelineEditor";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Pipeline Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PipelineEditor_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.Pipelines.GraphEditor graphEditor1;
	}
}
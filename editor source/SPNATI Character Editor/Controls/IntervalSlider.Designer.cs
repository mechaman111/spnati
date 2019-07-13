namespace SPNATI_Character_Editor.Controls
{
	partial class IntervalSlider
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
			this.mnuOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuOptions
			// 
			this.mnuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsRemove});
			this.mnuOptions.Name = "mnuOptions";
			this.mnuOptions.Size = new System.Drawing.Size(144, 48);
			this.mnuOptions.Opening += new System.ComponentModel.CancelEventHandler(this.mnuOptions_Opening);
			// 
			// tsAdd
			// 
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(143, 22);
			this.tsAdd.Text = "Add Split";
			// 
			// tsRemove
			// 
			this.tsRemove.Image = global::SPNATI_Character_Editor.Properties.Resources.Remove;
			this.tsRemove.Name = "tsRemove";
			this.tsRemove.Size = new System.Drawing.Size(143, 22);
			this.tsRemove.Text = "Remove Split";
			// 
			// IntervalSlider
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.mnuOptions;
			this.Name = "IntervalSlider";
			this.Size = new System.Drawing.Size(150, 21);
			this.mnuOptions.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip mnuOptions;
		private System.Windows.Forms.ToolStripMenuItem tsAdd;
		private System.Windows.Forms.ToolStripMenuItem tsRemove;
	}
}

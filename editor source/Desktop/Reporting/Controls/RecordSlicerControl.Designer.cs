namespace Desktop.Reporting.Controls
{
	partial class RecordSlicerControl
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
			this.recField = new Desktop.CommonControls.RecordField();
			this.SuspendLayout();
			// 
			// flowPanel
			// 
			this.flowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowPanel.AutoScroll = true;
			this.flowPanel.Location = new System.Drawing.Point(0, 31);
			this.flowPanel.Name = "flowPanel";
			this.flowPanel.Size = new System.Drawing.Size(313, 177);
			this.flowPanel.TabIndex = 2;
			// 
			// mnuMerge
			// 
			this.mnuMerge.Name = "mnuMerge";
			this.mnuMerge.Size = new System.Drawing.Size(61, 4);
			this.mnuMerge.Tag = "Surface";
			// 
			// recField
			// 
			this.recField.AllowCreate = false;
			this.recField.Location = new System.Drawing.Point(3, 3);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordFilter = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(150, 20);
			this.recField.TabIndex = 0;
			this.recField.UseAutoComplete = false;
			this.recField.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recField_RecordChanged);
			// 
			// RecordSlicerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.recField);
			this.Controls.Add(this.flowPanel);
			this.Name = "RecordSlicerControl";
			this.Size = new System.Drawing.Size(313, 208);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowPanel;
		private CommonControls.RecordField recField;
		private System.Windows.Forms.ContextMenuStrip mnuMerge;
	}
}

namespace SPNATI_Character_Editor.Controls.Reference
{
	partial class TagGuide
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
			this.label1 = new System.Windows.Forms.Label();
			this.recTag = new Desktop.CommonControls.RecordField();
			this.lstItems = new Desktop.CommonControls.AccordionListView();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Search:";
			// 
			// recTag
			// 
			this.recTag.AllowCreate = false;
			this.recTag.Location = new System.Drawing.Point(53, 3);
			this.recTag.Name = "recTag";
			this.recTag.PlaceholderText = null;
			this.recTag.Record = null;
			this.recTag.RecordContext = null;
			this.recTag.RecordFilter = null;
			this.recTag.RecordKey = null;
			this.recTag.RecordType = null;
			this.recTag.Size = new System.Drawing.Size(150, 20);
			this.recTag.TabIndex = 2;
			this.recTag.UseAutoComplete = true;
			this.recTag.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recTag_RecordChanged);
			// 
			// lstItems
			// 
			this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstItems.DataSource = null;
			this.lstItems.Location = new System.Drawing.Point(0, 26);
			this.lstItems.Name = "lstItems";
			this.lstItems.SelectedItem = null;
			this.lstItems.Size = new System.Drawing.Size(268, 124);
			this.lstItems.TabIndex = 0;
			this.lstItems.Load += new System.EventHandler(this.lstItems_Load);
			// 
			// TagGuide
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.recTag);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstItems);
			this.Name = "TagGuide";
			this.Size = new System.Drawing.Size(268, 150);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.AccordionListView lstItems;
		private System.Windows.Forms.Label label1;
		private Desktop.CommonControls.RecordField recTag;
	}
}

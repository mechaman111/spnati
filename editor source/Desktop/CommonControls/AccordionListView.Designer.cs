namespace Desktop.CommonControls
{
	partial class AccordionListView
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
			this.view = new System.Windows.Forms.ListView();
			this.tmrTick = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// view
			// 
			this.view.AllowDrop = true;
			this.view.Dock = System.Windows.Forms.DockStyle.Fill;
			this.view.FullRowSelect = true;
			this.view.HideSelection = false;
			this.view.Location = new System.Drawing.Point(0, 0);
			this.view.MultiSelect = false;
			this.view.Name = "view";
			this.view.OwnerDraw = true;
			this.view.ShowItemToolTips = true;
			this.view.Size = new System.Drawing.Size(150, 150);
			this.view.TabIndex = 0;
			this.view.UseCompatibleStateImageBehavior = false;
			this.view.View = System.Windows.Forms.View.Details;
			this.view.VirtualMode = true;
			this.view.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.View_DrawColumnHeader);
			this.view.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.View_DrawItem);
			this.view.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.View_DrawSubItem);
			this.view.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.View_RetrieveVirtualItem);
			this.view.SelectedIndexChanged += new System.EventHandler(this.View_SelectedIndexChanged);
			this.view.SizeChanged += new System.EventHandler(this.View_SizeChanged);
			this.view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.view_MouseDown);
			this.view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.view_MouseUp);
			// 
			// tmrTick
			// 
			this.tmrTick.Interval = 1;
			this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
			// 
			// AccordionListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.view);
			this.Name = "AccordionListView";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView view;
		private System.Windows.Forms.Timer tmrTick;
	}
}

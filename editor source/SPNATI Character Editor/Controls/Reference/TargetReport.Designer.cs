using Desktop.CommonControls;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.Reference
{
	partial class TargetReport
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
			this.tsTargets = new ToolStrip();
			this.tsRefresh = new ToolStripButton();
			this.lstItems = new AccordionListView();
			this.tsTargets.SuspendLayout();
			base.SuspendLayout();
			this.tsTargets.GripStyle = ToolStripGripStyle.Hidden;
			this.tsTargets.Items.AddRange(new ToolStripItem[]
			{
				this.tsRefresh
			});
			this.tsTargets.Location = new Point(0, 0);
			this.tsTargets.Name = "tsTargets";
			this.tsTargets.Size = new Size(219, 25);
			this.tsTargets.TabIndex = 0;
			this.tsTargets.Tag = "Background";
			this.tsTargets.Text = "toolStrip1";
			this.tsRefresh.Alignment = ToolStripItemAlignment.Right;
			this.tsRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsRefresh.Image = Properties.Resources.Refresh;
			this.tsRefresh.ImageTransparentColor = Color.Magenta;
			this.tsRefresh.Name = "tsRefresh";
			this.tsRefresh.Size = new Size(23, 22);
			this.tsRefresh.Text = "Refresh";
			this.tsRefresh.Click += this.tsRefresh_Click;
			this.lstItems.DataSource = null;
			this.lstItems.Dock = DockStyle.Fill;
			this.lstItems.Location = new Point(0, 25);
			this.lstItems.Name = "lstItems";
			this.lstItems.SelectedItem = null;
			this.lstItems.ShowIndicators = false;
			this.lstItems.Size = new Size(219, 125);
			this.lstItems.TabIndex = 1;
			this.lstItems.DoubleClick += this.lstItems_DoubleClick;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.lstItems);
			base.Controls.Add(this.tsTargets);
			base.Name = "TargetReport";
			base.Size = new Size(219, 150);
			this.tsTargets.ResumeLayout(false);
			this.tsTargets.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		#endregion

		private Character _character;

		// Token: 0x04000A08 RID: 2568
		private System.Windows.Forms.ToolStrip tsTargets;

		// Token: 0x04000A09 RID: 2569
		private System.Windows.Forms.ToolStripButton tsRefresh;

		// Token: 0x04000A0A RID: 2570
		private Desktop.CommonControls.AccordionListView lstItems;
	}
}

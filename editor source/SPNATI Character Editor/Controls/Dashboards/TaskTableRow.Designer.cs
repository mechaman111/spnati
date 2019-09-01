namespace SPNATI_Character_Editor.Controls
{
	partial class TaskTableRow
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
			this.table = new System.Windows.Forms.TableLayoutPanel();
			this.link = new Desktop.CommonControls.ActivityLink();
			this.lblTask = new Desktop.Skinning.SkinnedLabel();
			this.fill = new Desktop.CommonControls.Graphs.FillGauge();
			this.cmdHelp = new Desktop.Skinning.SkinnedIcon();
			this.bubbleTip = new System.Windows.Forms.ToolTip(this.components);
			this.table.SuspendLayout();
			this.SuspendLayout();
			// 
			// table
			// 
			this.table.ColumnCount = 4;
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
			this.table.Controls.Add(this.link, 3, 0);
			this.table.Controls.Add(this.lblTask, 1, 0);
			this.table.Controls.Add(this.fill, 2, 0);
			this.table.Controls.Add(this.cmdHelp, 0, 0);
			this.table.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.Name = "table";
			this.table.RowCount = 1;
			this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table.Size = new System.Drawing.Size(361, 27);
			this.table.TabIndex = 0;
			// 
			// link
			// 
			this.link.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.link.LaunchParameters = null;
			this.link.Location = new System.Drawing.Point(323, 3);
			this.link.Name = "link";
			this.link.Size = new System.Drawing.Size(35, 20);
			this.link.TabIndex = 0;
			// 
			// lblTask
			// 
			this.lblTask.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblTask.AutoSize = true;
			this.lblTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblTask.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTask.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblTask.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblTask.Location = new System.Drawing.Point(27, 7);
			this.lblTask.Name = "lblTask";
			this.lblTask.Size = new System.Drawing.Size(108, 13);
			this.lblTask.TabIndex = 1;
			this.lblTask.Text = "This is a sample task.";
			// 
			// fill
			// 
			this.fill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.fill.Location = new System.Drawing.Point(203, 5);
			this.fill.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.fill.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.fill.Name = "fill";
			this.fill.ShowLimits = true;
			this.fill.ShowPercentage = false;
			this.fill.Size = new System.Drawing.Size(114, 16);
			this.fill.TabIndex = 2;
			this.fill.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			// 
			// cmdHelp
			// 
			this.cmdHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cmdHelp.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdHelp.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdHelp.Flat = false;
			this.cmdHelp.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.cmdHelp.Location = new System.Drawing.Point(3, 3);
			this.cmdHelp.Name = "cmdHelp";
			this.cmdHelp.Size = new System.Drawing.Size(18, 21);
			this.cmdHelp.TabIndex = 3;
			this.cmdHelp.Text = "skinnedIcon1";
			this.cmdHelp.UseVisualStyleBackColor = true;
			// 
			// bubbleTip
			// 
			this.bubbleTip.AutoPopDelay = 30000;
			this.bubbleTip.InitialDelay = 500;
			this.bubbleTip.IsBalloon = true;
			this.bubbleTip.ReshowDelay = 100;
			// 
			// TaskTableRow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.table);
			this.Name = "TaskTableRow";
			this.Size = new System.Drawing.Size(361, 27);
			this.table.ResumeLayout(false);
			this.table.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel table;
		private Desktop.CommonControls.ActivityLink link;
		private Desktop.Skinning.SkinnedLabel lblTask;
		private Desktop.CommonControls.Graphs.FillGauge fill;
		private Desktop.Skinning.SkinnedIcon cmdHelp;
		private System.Windows.Forms.ToolTip bubbleTip;
	}
}

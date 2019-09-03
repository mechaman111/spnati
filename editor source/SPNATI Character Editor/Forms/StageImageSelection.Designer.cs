namespace SPNATI_Character_Editor.Forms
{
	partial class StageImageSelection
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
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.pnlImages = new Desktop.CommonControls.DBPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.pnlZeroState = new Desktop.CommonControls.DBPanel();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.cmdAdd2 = new Desktop.Skinning.SkinnedIcon();
			this.skinnedPanel1.SuspendLayout();
			this.pnlImages.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.pnlZeroState.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(389, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(470, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 529);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(548, 30);
			this.skinnedPanel1.TabIndex = 4;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// pnlImages
			// 
			this.pnlImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlImages.AutoScroll = true;
			this.pnlImages.Controls.Add(this.pnlZeroState);
			this.pnlImages.Location = new System.Drawing.Point(4, 53);
			this.pnlImages.Margin = new System.Windows.Forms.Padding(0);
			this.pnlImages.Name = "pnlImages";
			this.pnlImages.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.pnlImages.Size = new System.Drawing.Size(541, 473);
			this.pnlImages.TabIndex = 5;
			this.pnlImages.TabSide = Desktop.Skinning.TabSide.None;
			this.pnlImages.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.pnlImages_ControlAdded);
			this.pnlImages.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.pnlImages_ControlRemoved);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd});
			this.toolStrip1.Location = new System.Drawing.Point(1, 27);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(546, 25);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Tag = "PrimaryLight";
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsAdd
			// 
			this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(23, 22);
			this.tsAdd.Text = "Add Image";
			this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
			// 
			// pnlZeroState
			// 
			this.pnlZeroState.Controls.Add(this.cmdAdd2);
			this.pnlZeroState.Controls.Add(this.skinnedLabel2);
			this.pnlZeroState.Controls.Add(this.skinnedLabel1);
			this.pnlZeroState.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlZeroState.Location = new System.Drawing.Point(0, 0);
			this.pnlZeroState.Name = "pnlZeroState";
			this.pnlZeroState.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.pnlZeroState.Size = new System.Drawing.Size(541, 473);
			this.pnlZeroState.TabIndex = 0;
			this.pnlZeroState.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(163, 224);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(30, 13);
			this.skinnedLabel1.TabIndex = 0;
			this.skinnedLabel1.Text = "Click";
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(217, 224);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(187, 13);
			this.skinnedLabel2.TabIndex = 2;
			this.skinnedLabel2.Text = "to add an image to a subset of stages.";
			// 
			// cmdAdd2
			// 
			this.cmdAdd2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cmdAdd2.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAdd2.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdAdd2.Flat = false;
			this.cmdAdd2.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.cmdAdd2.Location = new System.Drawing.Point(195, 222);
			this.cmdAdd2.Name = "cmdAdd2";
			this.cmdAdd2.Size = new System.Drawing.Size(16, 16);
			this.cmdAdd2.TabIndex = 3;
			this.cmdAdd2.Text = "skinnedIcon1";
			this.cmdAdd2.UseVisualStyleBackColor = true;
			this.cmdAdd2.Click += new System.EventHandler(this.tsAdd_Click);
			// 
			// StageImageSelection
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(548, 559);
			this.ControlBox = false;
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.pnlImages);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "StageImageSelection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Images Per Stage";
			this.skinnedPanel1.ResumeLayout(false);
			this.pnlImages.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.pnlZeroState.ResumeLayout(false);
			this.pnlZeroState.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.CommonControls.DBPanel pnlImages;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private Desktop.CommonControls.DBPanel pnlZeroState;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedIcon cmdAdd2;
	}
}
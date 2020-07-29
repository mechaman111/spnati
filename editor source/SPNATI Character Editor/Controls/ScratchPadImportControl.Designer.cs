namespace SPNATI_Character_Editor.Controls
{
	partial class ScratchPadImportControl
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
			this.lstLines = new Desktop.Skinning.SkinnedCheckedListBox();
			this.split = new System.Windows.Forms.SplitContainer();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdCancelImport = new Desktop.Skinning.SkinnedButton();
			this.cmdFinishImport = new Desktop.Skinning.SkinnedButton();
			this.caseCtl = new SPNATI_Character_Editor.Controls.CaseControl();
			((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
			this.split.Panel1.SuspendLayout();
			this.split.Panel2.SuspendLayout();
			this.split.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstLines
			// 
			this.lstLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstLines.BackColor = System.Drawing.Color.White;
			this.lstLines.CheckOnClick = true;
			this.lstLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstLines.ForeColor = System.Drawing.Color.Black;
			this.lstLines.FormattingEnabled = true;
			this.lstLines.Location = new System.Drawing.Point(3, 27);
			this.lstLines.Name = "lstLines";
			this.lstLines.Size = new System.Drawing.Size(849, 229);
			this.lstLines.TabIndex = 0;
			// 
			// split
			// 
			this.split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.split.Location = new System.Drawing.Point(0, 0);
			this.split.Name = "split";
			this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// split.Panel1
			// 
			this.split.Panel1.Controls.Add(this.cmdCancel);
			this.split.Panel1.Controls.Add(this.cmdImport);
			this.split.Panel1.Controls.Add(this.skinnedLabel1);
			this.split.Panel1.Controls.Add(this.lstLines);
			// 
			// split.Panel2
			// 
			this.split.Panel2.Controls.Add(this.cmdCancelImport);
			this.split.Panel2.Controls.Add(this.cmdFinishImport);
			this.split.Panel2.Controls.Add(this.caseCtl);
			this.split.Size = new System.Drawing.Size(855, 602);
			this.split.SplitterDistance = 301;
			this.split.TabIndex = 1;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(3, 8);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(221, 13);
			this.skinnedLabel1.TabIndex = 1;
			this.skinnedLabel1.Text = "Select one or more lines to import into a case:";
			// 
			// cmdImport
			// 
			this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdImport.Flat = false;
			this.cmdImport.Location = new System.Drawing.Point(696, 275);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(75, 23);
			this.cmdImport.TabIndex = 3;
			this.cmdImport.Text = "Import";
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.Blue;
			this.cmdCancel.Location = new System.Drawing.Point(777, 275);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdCancelImport
			// 
			this.cmdCancelImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancelImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancelImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancelImport.Flat = true;
			this.cmdCancelImport.ForeColor = System.Drawing.Color.Blue;
			this.cmdCancelImport.Location = new System.Drawing.Point(777, 271);
			this.cmdCancelImport.Name = "cmdCancelImport";
			this.cmdCancelImport.Size = new System.Drawing.Size(75, 23);
			this.cmdCancelImport.TabIndex = 6;
			this.cmdCancelImport.Text = "Cancel";
			this.cmdCancelImport.UseVisualStyleBackColor = true;
			this.cmdCancelImport.Click += new System.EventHandler(this.cmdCancelImport_Click);
			// 
			// cmdFinishImport
			// 
			this.cmdFinishImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdFinishImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdFinishImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdFinishImport.Flat = false;
			this.cmdFinishImport.Location = new System.Drawing.Point(696, 271);
			this.cmdFinishImport.Name = "cmdFinishImport";
			this.cmdFinishImport.Size = new System.Drawing.Size(75, 23);
			this.cmdFinishImport.TabIndex = 5;
			this.cmdFinishImport.Text = "Finish";
			this.cmdFinishImport.UseVisualStyleBackColor = true;
			this.cmdFinishImport.Click += new System.EventHandler(this.cmdFinishImport_Click);
			// 
			// caseCtl
			// 
			this.caseCtl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.caseCtl.Location = new System.Drawing.Point(0, -1);
			this.caseCtl.Name = "caseCtl";
			this.caseCtl.Size = new System.Drawing.Size(855, 270);
			this.caseCtl.TabIndex = 0;
			this.caseCtl.HighlightRow += new System.EventHandler<int>(this.caseCtl_HighlightRow);
			// 
			// ScratchPadImportControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.split);
			this.Name = "ScratchPadImportControl";
			this.Size = new System.Drawing.Size(855, 602);
			this.split.Panel1.ResumeLayout(false);
			this.split.Panel1.PerformLayout();
			this.split.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
			this.split.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedCheckedListBox lstLines;
		private System.Windows.Forms.SplitContainer split;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdImport;
		private Desktop.Skinning.SkinnedButton cmdCancelImport;
		private CaseControl caseCtl;
		private Desktop.Skinning.SkinnedButton cmdFinishImport;
	}
}

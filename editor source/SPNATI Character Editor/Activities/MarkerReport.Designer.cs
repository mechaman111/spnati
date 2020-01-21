namespace SPNATI_Character_Editor.Activities
{
	partial class MarkerReport
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.lstCharacters = new Desktop.Skinning.SkinnedListBox();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gridMarker = new SPNATI_Character_Editor.Controls.MarkerGrid();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.lstLines = new Desktop.Skinning.SkinnedListBox();
			this.picPortrait = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 21);
			this.label1.TabIndex = 2;
			this.label1.Text = "Characters";
			// 
			// lstCharacters
			// 
			this.lstCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCharacters.BackColor = System.Drawing.Color.White;
			this.lstCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstCharacters.ForeColor = System.Drawing.Color.Black;
			this.lstCharacters.FormattingEnabled = true;
			this.lstCharacters.Location = new System.Drawing.Point(6, 29);
			this.lstCharacters.Name = "lstCharacters";
			this.lstCharacters.Size = new System.Drawing.Size(237, 628);
			this.lstCharacters.TabIndex = 0;
			this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label2.Location = new System.Drawing.Point(3, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 21);
			this.label2.TabIndex = 3;
			this.label2.Text = "Markers";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.lstCharacters);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1095, 674);
			this.splitContainer1.SplitterDistance = 246;
			this.splitContainer1.TabIndex = 5;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.gridMarker);
			this.splitContainer2.Panel1.Controls.Add(this.label2);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.label3);
			this.splitContainer2.Panel2.Controls.Add(this.lstLines);
			this.splitContainer2.Panel2.Controls.Add(this.picPortrait);
			this.splitContainer2.Size = new System.Drawing.Size(845, 674);
			this.splitContainer2.SplitterDistance = 336;
			this.splitContainer2.TabIndex = 4;
			// 
			// gridMarker
			// 
			this.gridMarker.AllowPrivate = false;
			this.gridMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMarker.Location = new System.Drawing.Point(3, 30);
			this.gridMarker.Name = "gridMarker";
			this.gridMarker.ReadOnly = true;
			this.gridMarker.Size = new System.Drawing.Size(839, 303);
			this.gridMarker.TabIndex = 4;
			this.gridMarker.SelectionChanged += new System.EventHandler<SPNATI_Character_Editor.Marker>(this.gridMarker_SelectionChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.label3.Location = new System.Drawing.Point(4, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(181, 21);
			this.label3.TabIndex = 2;
			this.label3.Text = "Lines that set the marker";
			// 
			// lstLines
			// 
			this.lstLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstLines.BackColor = System.Drawing.Color.White;
			this.lstLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstLines.ForeColor = System.Drawing.Color.Black;
			this.lstLines.FormattingEnabled = true;
			this.lstLines.HorizontalScrollbar = true;
			this.lstLines.Location = new System.Drawing.Point(3, 26);
			this.lstLines.Name = "lstLines";
			this.lstLines.Size = new System.Drawing.Size(667, 290);
			this.lstLines.TabIndex = 1;
			this.lstLines.SelectedIndexChanged += new System.EventHandler(this.lstLines_SelectedIndexChanged);
			// 
			// picPortrait
			// 
			this.picPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picPortrait.Location = new System.Drawing.Point(676, 3);
			this.picPortrait.Name = "picPortrait";
			this.picPortrait.Size = new System.Drawing.Size(166, 319);
			this.picPortrait.TabIndex = 0;
			this.picPortrait.TabStop = false;
			// 
			// MarkerReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "MarkerReport";
			this.Size = new System.Drawing.Size(1095, 674);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedListBox lstCharacters;
		private Desktop.Skinning.SkinnedLabel label2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Controls.MarkerGrid gridMarker;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedListBox lstLines;
		private SPNATI_Character_Editor.Controls.CharacterImageBox picPortrait;
	}
}

namespace SPNATI_Character_Editor.Forms
{
	partial class EpilogueSelectForm
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
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.radDirective = new Desktop.Skinning.SkinnedRadioButton();
			this.radTimeline = new Desktop.Skinning.SkinnedRadioButton();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel4 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel5 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 192);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(504, 30);
			this.skinnedPanel1.TabIndex = 7;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(410, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(91, 23);
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.Blue;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 37);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(355, 21);
			this.skinnedLabel1.TabIndex = 8;
			this.skinnedLabel1.Text = "What style epilogue editor should be used for {0}?";
			// 
			// radDirective
			// 
			this.radDirective.AutoSize = true;
			this.radDirective.Checked = true;
			this.radDirective.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radDirective.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.radDirective.Location = new System.Drawing.Point(38, 85);
			this.radDirective.Name = "radDirective";
			this.radDirective.Size = new System.Drawing.Size(72, 17);
			this.radDirective.TabIndex = 9;
			this.radDirective.TabStop = true;
			this.radDirective.Text = "Directives";
			this.radDirective.UseVisualStyleBackColor = true;
			// 
			// radTimeline
			// 
			this.radTimeline.AutoSize = true;
			this.radTimeline.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.radTimeline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.radTimeline.Location = new System.Drawing.Point(38, 146);
			this.radTimeline.Name = "radTimeline";
			this.radTimeline.Size = new System.Drawing.Size(64, 17);
			this.radTimeline.TabIndex = 10;
			this.radTimeline.TabStop = true;
			this.radTimeline.Text = "Timeline";
			this.radTimeline.UseVisualStyleBackColor = true;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(67, 109);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(325, 13);
			this.skinnedLabel2.TabIndex = 11;
			this.skinnedLabel2.Text = "Animation and text bubbles are made using step-by-step instructions";
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(67, 166);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(396, 13);
			this.skinnedLabel3.TabIndex = 12;
			this.skinnedLabel3.Text = "Animations and text are made using an animation timeline similar to the Pose Make" +
    "r";
			// 
			// skinnedLabel4
			// 
			this.skinnedLabel4.AutoSize = true;
			this.skinnedLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel4.Location = new System.Drawing.Point(12, 65);
			this.skinnedLabel4.Name = "skinnedLabel4";
			this.skinnedLabel4.Size = new System.Drawing.Size(184, 13);
			this.skinnedLabel4.TabIndex = 13;
			this.skinnedLabel4.Text = "This choice cannot be changed later.";
			// 
			// skinnedLabel5
			// 
			this.skinnedLabel5.AutoSize = true;
			this.skinnedLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel5.Location = new System.Drawing.Point(67, 129);
			this.skinnedLabel5.Name = "skinnedLabel5";
			this.skinnedLabel5.Size = new System.Drawing.Size(237, 13);
			this.skinnedLabel5.TabIndex = 14;
			this.skinnedLabel5.Text = "Note: This is a legacy format and is unsupported.";
			// 
			// EpilogueSelectForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(504, 222);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedLabel5);
			this.Controls.Add(this.skinnedLabel4);
			this.Controls.Add(this.skinnedLabel3);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.radTimeline);
			this.Controls.Add(this.radDirective);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "EpilogueSelectForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Epilogue Editor";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedRadioButton radDirective;
		private Desktop.Skinning.SkinnedRadioButton radTimeline;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
		private Desktop.Skinning.SkinnedLabel skinnedLabel4;
		private Desktop.Skinning.SkinnedLabel skinnedLabel5;
	}
}
namespace SPNATI_Character_Editor.Activities
{
	partial class RespondToThis
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
			this.lblFrom = new Desktop.Skinning.SkinnedLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gridSource = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.imgSource = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.responseControl = new SPNATI_Character_Editor.Controls.CaseControl();
			this.lblResponse = new Desktop.Skinning.SkinnedLabel();
			this.imgResponse = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdAccept = new Desktop.Skinning.SkinnedButton();
			this.cmdJumpToDialogue = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblFrom
			// 
			this.lblFrom.AutoSize = true;
			this.lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblFrom.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFrom.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblFrom.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblFrom.Location = new System.Drawing.Point(3, -1);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(79, 13);
			this.lblFrom.TabIndex = 1;
			this.lblFrom.Text = "Responding to:";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(1, 9);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Size = new System.Drawing.Size(1252, 676);
			this.splitContainer1.SplitterDistance = 144;
			this.splitContainer1.TabIndex = 14;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.gridSource);
			this.splitContainer2.Panel1.Controls.Add(this.lblFrom);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.imgSource);
			this.splitContainer2.Size = new System.Drawing.Size(1252, 144);
			this.splitContainer2.SplitterDistance = 934;
			this.splitContainer2.TabIndex = 3;
			// 
			// gridSource
			// 
			this.gridSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridSource.Location = new System.Drawing.Point(2, 15);
			this.gridSource.Name = "gridSource";
			this.gridSource.ReadOnly = true;
			this.gridSource.Size = new System.Drawing.Size(928, 122);
			this.gridSource.TabIndex = 0;
			this.gridSource.HighlightRow += new System.EventHandler<int>(this.gridSource_HighlightRow);
			// 
			// imgSource
			// 
			this.imgSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imgSource.Location = new System.Drawing.Point(0, 0);
			this.imgSource.Name = "imgSource";
			this.imgSource.Size = new System.Drawing.Size(314, 144);
			this.imgSource.TabIndex = 0;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.responseControl);
			this.splitContainer3.Panel1.Controls.Add(this.lblResponse);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.imgResponse);
			this.splitContainer3.Size = new System.Drawing.Size(1252, 528);
			this.splitContainer3.SplitterDistance = 933;
			this.splitContainer3.TabIndex = 0;
			// 
			// responseControl
			// 
			this.responseControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.responseControl.Location = new System.Drawing.Point(3, 24);
			this.responseControl.Name = "responseControl";
			this.responseControl.Size = new System.Drawing.Size(928, 497);
			this.responseControl.TabIndex = 13;
			this.responseControl.HighlightRow += new System.EventHandler<int>(this.ResponseControl_HighlightRow);
			// 
			// lblResponse
			// 
			this.lblResponse.AutoSize = true;
			this.lblResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblResponse.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblResponse.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblResponse.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblResponse.Location = new System.Drawing.Point(3, 5);
			this.lblResponse.Name = "lblResponse";
			this.lblResponse.Size = new System.Drawing.Size(91, 13);
			this.lblResponse.TabIndex = 2;
			this.lblResponse.Text = "Response from X:";
			// 
			// imgResponse
			// 
			this.imgResponse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imgResponse.Location = new System.Drawing.Point(0, 0);
			this.imgResponse.Name = "imgResponse";
			this.imgResponse.Size = new System.Drawing.Size(315, 528);
			this.imgResponse.TabIndex = 0;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(1123, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(128, 23);
			this.cmdCancel.TabIndex = 10;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdAccept
			// 
			this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAccept.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdAccept.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdAccept.Flat = false;
			this.cmdAccept.Location = new System.Drawing.Point(989, 4);
			this.cmdAccept.Name = "cmdAccept";
			this.cmdAccept.Size = new System.Drawing.Size(128, 23);
			this.cmdAccept.TabIndex = 11;
			this.cmdAccept.Text = "Finish";
			this.cmdAccept.UseVisualStyleBackColor = true;
			this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
			// 
			// cmdJumpToDialogue
			// 
			this.cmdJumpToDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdJumpToDialogue.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdJumpToDialogue.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdJumpToDialogue.Flat = false;
			this.cmdJumpToDialogue.Location = new System.Drawing.Point(855, 4);
			this.cmdJumpToDialogue.Name = "cmdJumpToDialogue";
			this.cmdJumpToDialogue.Size = new System.Drawing.Size(128, 23);
			this.cmdJumpToDialogue.TabIndex = 12;
			this.cmdJumpToDialogue.Text = "Edit Full Screen";
			this.cmdJumpToDialogue.UseVisualStyleBackColor = true;
			this.cmdJumpToDialogue.Click += new System.EventHandler(this.cmdJumpToDialogue_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdAccept);
			this.skinnedPanel1.Controls.Add(this.cmdJumpToDialogue);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 685);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(1254, 30);
			this.skinnedPanel1.TabIndex = 15;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// RespondToThis
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "RespondToThis";
			this.Size = new System.Drawing.Size(1254, 715);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblFrom;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Controls.DialogueGrid gridSource;
		private Controls.CharacterImageBox imgSource;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private Controls.CaseControl responseControl;
		private Desktop.Skinning.SkinnedLabel lblResponse;
		private Controls.CharacterImageBox imgResponse;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdAccept;
		private Desktop.Skinning.SkinnedButton cmdJumpToDialogue;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}

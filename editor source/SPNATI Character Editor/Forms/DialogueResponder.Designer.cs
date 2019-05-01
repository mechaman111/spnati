namespace SPNATI_Character_Editor.Forms
{
	partial class DialogueResponder
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
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gridSource = new SPNATI_Character_Editor.Controls.DialogueGrid();
			this.imgSource = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.responseControl = new SPNATI_Character_Editor.Controls.CaseControl();
			this.cmdJumpToDialogue = new System.Windows.Forms.Button();
			this.cmdAccept = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.lblResponse = new System.Windows.Forms.Label();
			this.imgResponse = new SPNATI_Character_Editor.Controls.CharacterImageBox();
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
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Responding to:";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
			this.splitContainer1.Size = new System.Drawing.Size(1254, 720);
			this.splitContainer1.SplitterDistance = 154;
			this.splitContainer1.TabIndex = 2;
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
			this.splitContainer2.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.imgSource);
			this.splitContainer2.Size = new System.Drawing.Size(1254, 154);
			this.splitContainer2.SplitterDistance = 936;
			this.splitContainer2.TabIndex = 3;
			// 
			// gridSource
			// 
			this.gridSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridSource.Location = new System.Drawing.Point(2, 19);
			this.gridSource.Name = "gridSource";
			this.gridSource.ReadOnly = true;
			this.gridSource.Size = new System.Drawing.Size(930, 132);
			this.gridSource.TabIndex = 0;
			this.gridSource.HighlightRow += new System.EventHandler<int>(this.gridSource_HighlightRow);
			// 
			// imgSource
			// 
			this.imgSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imgSource.Location = new System.Drawing.Point(0, 0);
			this.imgSource.Name = "imgSource";
			this.imgSource.ShowTextBox = false;
			this.imgSource.Size = new System.Drawing.Size(314, 154);
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
			this.splitContainer3.Panel1.Controls.Add(this.cmdJumpToDialogue);
			this.splitContainer3.Panel1.Controls.Add(this.cmdAccept);
			this.splitContainer3.Panel1.Controls.Add(this.cmdCancel);
			this.splitContainer3.Panel1.Controls.Add(this.lblResponse);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.imgResponse);
			this.splitContainer3.Size = new System.Drawing.Size(1254, 562);
			this.splitContainer3.SplitterDistance = 935;
			this.splitContainer3.TabIndex = 0;
			// 
			// responseControl
			// 
			this.responseControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.responseControl.Location = new System.Drawing.Point(3, 16);
			this.responseControl.Name = "responseControl";
			this.responseControl.Size = new System.Drawing.Size(930, 505);
			this.responseControl.TabIndex = 13;
			// 
			// cmdJumpToDialogue
			// 
			this.cmdJumpToDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdJumpToDialogue.Location = new System.Drawing.Point(536, 527);
			this.cmdJumpToDialogue.Name = "cmdJumpToDialogue";
			this.cmdJumpToDialogue.Size = new System.Drawing.Size(128, 23);
			this.cmdJumpToDialogue.TabIndex = 12;
			this.cmdJumpToDialogue.Text = "Edit in Dialogue Editor";
			this.cmdJumpToDialogue.UseVisualStyleBackColor = true;
			this.cmdJumpToDialogue.Click += new System.EventHandler(this.cmdJumpToDialogue_Click);
			// 
			// cmdAccept
			// 
			this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAccept.Location = new System.Drawing.Point(670, 527);
			this.cmdAccept.Name = "cmdAccept";
			this.cmdAccept.Size = new System.Drawing.Size(128, 23);
			this.cmdAccept.TabIndex = 11;
			this.cmdAccept.Text = "Accept";
			this.cmdAccept.UseVisualStyleBackColor = true;
			this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point(804, 527);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(128, 23);
			this.cmdCancel.TabIndex = 10;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// lblResponse
			// 
			this.lblResponse.AutoSize = true;
			this.lblResponse.Location = new System.Drawing.Point(3, 0);
			this.lblResponse.Name = "lblResponse";
			this.lblResponse.Size = new System.Drawing.Size(91, 13);
			this.lblResponse.TabIndex = 2;
			this.lblResponse.Text = "Responds from X:";
			// 
			// imgResponse
			// 
			this.imgResponse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imgResponse.Location = new System.Drawing.Point(0, 0);
			this.imgResponse.Name = "imgResponse";
			this.imgResponse.ShowTextBox = false;
			this.imgResponse.Size = new System.Drawing.Size(315, 562);
			this.imgResponse.TabIndex = 0;
			// 
			// DialogueResponder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1254, 720);
			this.ControlBox = false;
			this.Controls.Add(this.splitContainer1);
			this.Name = "DialogueResponder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Respond to This";
			this.Shown += new System.EventHandler(this.DialogueResponder_Shown);
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
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.DialogueGrid gridSource;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.Label lblResponse;
		private Controls.CharacterImageBox imgSource;
		private Controls.CharacterImageBox imgResponse;
		private System.Windows.Forms.Button cmdJumpToDialogue;
		private System.Windows.Forms.Button cmdAccept;
		private System.Windows.Forms.Button cmdCancel;
		private Controls.CaseControl responseControl;
	}
}
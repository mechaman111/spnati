namespace SPNATI_Character_Editor.Forms
{
	partial class ResponseSetupForm
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
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.lblMarker = new Desktop.Skinning.SkinnedLabel();
			this.txtMarker = new Desktop.Skinning.SkinnedTextBox();
			this.lstStages = new Desktop.Skinning.SkinnedListView();
			this.chkOneTime = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label1.Location = new System.Drawing.Point(12, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(163, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Stages in which to use response:";
			// 
			// lblMarker
			// 
			this.lblMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMarker.AutoSize = true;
			this.lblMarker.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblMarker.Location = new System.Drawing.Point(150, 189);
			this.lblMarker.Name = "lblMarker";
			this.lblMarker.Size = new System.Drawing.Size(43, 13);
			this.lblMarker.TabIndex = 2;
			this.lblMarker.Text = "Marker:";
			// 
			// txtMarker
			// 
			this.txtMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMarker.BackColor = System.Drawing.Color.White;
			this.txtMarker.Enabled = false;
			this.txtMarker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtMarker.ForeColor = System.Drawing.Color.Black;
			this.txtMarker.Location = new System.Drawing.Point(199, 186);
			this.txtMarker.Name = "txtMarker";
			this.txtMarker.Size = new System.Drawing.Size(73, 20);
			this.txtMarker.TabIndex = 3;
			// 
			// lstStages
			// 
			this.lstStages.CheckBoxes = true;
			this.lstStages.Location = new System.Drawing.Point(12, 49);
			this.lstStages.MultiSelect = false;
			this.lstStages.Name = "lstStages";
			this.lstStages.OwnerDraw = true;
			this.lstStages.Size = new System.Drawing.Size(260, 132);
			this.lstStages.TabIndex = 4;
			this.lstStages.UseCompatibleStateImageBehavior = false;
			this.lstStages.View = System.Windows.Forms.View.List;
			this.lstStages.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstStages_ItemCheck);
			// 
			// chkOneTime
			// 
			this.chkOneTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkOneTime.AutoSize = true;
			this.chkOneTime.Location = new System.Drawing.Point(12, 188);
			this.chkOneTime.Name = "chkOneTime";
			this.chkOneTime.Size = new System.Drawing.Size(123, 17);
			this.chkOneTime.TabIndex = 5;
			this.chkOneTime.Text = "One Time Response";
			this.chkOneTime.UseVisualStyleBackColor = true;
			this.chkOneTime.CheckedChanged += new System.EventHandler(this.chkOneTime_CheckedChanged);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(125, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 6;
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
			this.cmdCancel.Location = new System.Drawing.Point(206, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 217);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(284, 30);
			this.skinnedPanel1.TabIndex = 8;
			// 
			// ResponseSetupForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(284, 247);
			this.ControlBox = false;
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.chkOneTime);
			this.Controls.Add(this.lstStages);
			this.Controls.Add(this.txtMarker);
			this.Controls.Add(this.lblMarker);
			this.Controls.Add(this.label1);
			this.Name = "ResponseSetupForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Setup Response";
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel lblMarker;
		private Desktop.Skinning.SkinnedTextBox txtMarker;
		private Desktop.Skinning.SkinnedListView lstStages;
		private Desktop.Skinning.SkinnedCheckBox chkOneTime;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
	}
}
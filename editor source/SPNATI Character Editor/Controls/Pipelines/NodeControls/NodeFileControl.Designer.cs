namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodeFileControl
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
			this.lblPath = new Desktop.Skinning.SkinnedLabel();
			this.cmdBrowse = new Desktop.Skinning.SkinnedButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.characterImageDialog1 = new SPNATI_Character_Editor.Controls.CharacterImageDialog();
			this.SuspendLayout();
			// 
			// lblPath
			// 
			this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPath.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblPath.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblPath.Location = new System.Drawing.Point(3, 0);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(114, 21);
			this.lblPath.TabIndex = 0;
			this.lblPath.Text = "Choose a file";
			this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowse.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cmdBrowse.Flat = false;
			this.cmdBrowse.Location = new System.Drawing.Point(123, 0);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(27, 21);
			this.cmdBrowse.TabIndex = 1;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// characterImageDialog1
			// 
			this.characterImageDialog1.Filter = "";
			// 
			// NodeFileControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.lblPath);
			this.Name = "NodeFileControl";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel lblPath;
		private Desktop.Skinning.SkinnedButton cmdBrowse;
		private CharacterImageDialog characterImageDialog1;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

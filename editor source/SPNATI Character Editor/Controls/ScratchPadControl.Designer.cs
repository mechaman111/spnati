namespace SPNATI_Character_Editor.Controls
{
	partial class ScratchPadControl
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
			this.txtLines = new Desktop.Skinning.SkinnedTextBox();
			this.lblInstructions = new Desktop.Skinning.SkinnedLabel();
			this.cmdImport = new Desktop.Skinning.SkinnedButton();
			this.importCtl = new SPNATI_Character_Editor.Controls.ScratchPadImportControl();
			this.SuspendLayout();
			// 
			// txtLines
			// 
			this.txtLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLines.BackColor = System.Drawing.Color.White;
			this.txtLines.ForeColor = System.Drawing.Color.Black;
			this.txtLines.Location = new System.Drawing.Point(3, 18);
			this.txtLines.Multiline = true;
			this.txtLines.Name = "txtLines";
			this.txtLines.Size = new System.Drawing.Size(781, 546);
			this.txtLines.TabIndex = 0;
			// 
			// lblInstructions
			// 
			this.lblInstructions.AutoSize = true;
			this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblInstructions.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblInstructions.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblInstructions.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblInstructions.Location = new System.Drawing.Point(0, 2);
			this.lblInstructions.Name = "lblInstructions";
			this.lblInstructions.Size = new System.Drawing.Size(430, 13);
			this.lblInstructions.TabIndex = 1;
			this.lblInstructions.Text = "Type free text lines here, one per row. Use the Import button to add them to the " +
    "character.";
			// 
			// cmdImport
			// 
			this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdImport.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdImport.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdImport.Flat = false;
			this.cmdImport.Location = new System.Drawing.Point(709, 570);
			this.cmdImport.Name = "cmdImport";
			this.cmdImport.Size = new System.Drawing.Size(75, 23);
			this.cmdImport.TabIndex = 2;
			this.cmdImport.Text = "Import";
			this.cmdImport.UseVisualStyleBackColor = true;
			this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
			// 
			// importCtl
			// 
			this.importCtl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.importCtl.Location = new System.Drawing.Point(0, 0);
			this.importCtl.Name = "importCtl";
			this.importCtl.Size = new System.Drawing.Size(787, 596);
			this.importCtl.TabIndex = 3;
			this.importCtl.Visible = false;
			this.importCtl.Cancel += new System.EventHandler(this.importCtl_Cancel);
			// 
			// ScratchPadControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.importCtl);
			this.Controls.Add(this.cmdImport);
			this.Controls.Add(this.lblInstructions);
			this.Controls.Add(this.txtLines);
			this.Name = "ScratchPadControl";
			this.Size = new System.Drawing.Size(787, 596);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedTextBox txtLines;
		private Desktop.Skinning.SkinnedLabel lblInstructions;
		private Desktop.Skinning.SkinnedButton cmdImport;
		private ScratchPadImportControl importCtl;
	}
}

namespace SPNATI_Character_Editor.Forms
{
	partial class DialogueGoals
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
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.valDaily = new Desktop.Skinning.SkinnedNumericUpDown();
			this.skinnedPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valDaily)).BeginInit();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 69);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(297, 30);
			this.skinnedPanel1.TabIndex = 0;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(219, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(138, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "Accept";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(13, 37);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(87, 13);
			this.skinnedLabel1.TabIndex = 1;
			this.skinnedLabel1.Text = "Daily lines target:";
			// 
			// valDaily
			// 
			this.valDaily.BackColor = System.Drawing.Color.White;
			this.valDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valDaily.ForeColor = System.Drawing.Color.Black;
			this.valDaily.Location = new System.Drawing.Point(138, 35);
			this.valDaily.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.valDaily.Name = "valDaily";
			this.valDaily.Size = new System.Drawing.Size(56, 20);
			this.valDaily.TabIndex = 2;
			// 
			// DialogueGoals
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(297, 99);
			this.ControlBox = false;
			this.Controls.Add(this.valDaily);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "DialogueGoals";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Dialogue Goals";
			this.skinnedPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.valDaily)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedNumericUpDown valDaily;
	}
}
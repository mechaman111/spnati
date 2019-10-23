namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	partial class HandQualityControl
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
			this.cboOperator = new Desktop.Skinning.SkinnedComboBox();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.cboRank = new Desktop.Skinning.SkinnedComboBox();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.cboCard = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// cboOperator
			// 
			this.cboOperator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboOperator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboOperator.BackColor = System.Drawing.Color.White;
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.KeyMember = null;
			this.cboOperator.Location = new System.Drawing.Point(149, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(44, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 14;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(195, 3);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(60, 13);
			this.skinnedLabel1.TabIndex = 15;
			this.skinnedLabel1.Text = "Hand rank:";
			// 
			// cboRank
			// 
			this.cboRank.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboRank.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboRank.BackColor = System.Drawing.Color.White;
			this.cboRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRank.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboRank.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboRank.FormattingEnabled = true;
			this.cboRank.KeyMember = null;
			this.cboRank.Location = new System.Drawing.Point(254, 0);
			this.cboRank.Name = "cboRank";
			this.cboRank.SelectedIndex = -1;
			this.cboRank.SelectedItem = null;
			this.cboRank.Size = new System.Drawing.Size(78, 21);
			this.cboRank.Sorted = false;
			this.cboRank.TabIndex = 16;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(331, 3);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(56, 13);
			this.skinnedLabel2.TabIndex = 17;
			this.skinnedLabel2.Text = "High card:";
			// 
			// cboCard
			// 
			this.cboCard.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboCard.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboCard.BackColor = System.Drawing.Color.White;
			this.cboCard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCard.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboCard.FormattingEnabled = true;
			this.cboCard.KeyMember = null;
			this.cboCard.Location = new System.Drawing.Point(386, 0);
			this.cboCard.Name = "cboCard";
			this.cboCard.SelectedIndex = -1;
			this.cboCard.SelectedItem = null;
			this.cboCard.Size = new System.Drawing.Size(67, 21);
			this.cboCard.Sorted = false;
			this.cboCard.TabIndex = 18;
			// 
			// HandQualityControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboCard);
			this.Controls.Add(this.cboRank);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.skinnedLabel2);
			this.Name = "HandQualityControl";
			this.Size = new System.Drawing.Size(605, 21);
			this.Controls.SetChildIndex(this.skinnedLabel2, 0);
			this.Controls.SetChildIndex(this.cboOperator, 0);
			this.Controls.SetChildIndex(this.skinnedLabel1, 0);
			this.Controls.SetChildIndex(this.cboRank, 0);
			this.Controls.SetChildIndex(this.cboCard, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboOperator;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedComboBox cboRank;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedComboBox cboCard;
	}
}

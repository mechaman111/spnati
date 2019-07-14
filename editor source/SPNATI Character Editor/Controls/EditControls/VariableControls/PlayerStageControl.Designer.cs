namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	partial class PlayerStageControl
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
			this.cboStage = new Desktop.Skinning.SkinnedComboBox();
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
			this.cboOperator.Location = new System.Drawing.Point(151, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.SelectedIndex = -1;
			this.cboOperator.SelectedItem = null;
			this.cboOperator.Size = new System.Drawing.Size(96, 21);
			this.cboOperator.Sorted = false;
			this.cboOperator.TabIndex = 13;
			// 
			// cboStage
			// 
			this.cboStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.cboStage.BackColor = System.Drawing.Color.White;
			this.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.cboStage.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboStage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboStage.Location = new System.Drawing.Point(250, 0);
			this.cboStage.Name = "cboStage";
			this.cboStage.SelectedIndex = -1;
			this.cboStage.SelectedItem = null;
			this.cboStage.Size = new System.Drawing.Size(142, 21);
			this.cboStage.Sorted = false;
			this.cboStage.TabIndex = 15;
			// 
			// PlayerStageControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboStage);
			this.Controls.Add(this.cboOperator);
			this.Name = "PlayerStageControl";
			this.Controls.SetChildIndex(this.cboOperator, 0);
			this.Controls.SetChildIndex(this.cboStage, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboOperator;
		private Desktop.Skinning.SkinnedComboBox cboStage;
	}
}

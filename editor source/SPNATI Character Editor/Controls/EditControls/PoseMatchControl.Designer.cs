namespace SPNATI_Character_Editor.Controls.EditControls
{
	partial class PoseMatchControl
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
			this.cboPose = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// cboPose
			// 
			this.cboPose.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboPose.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboPose.BackColor = System.Drawing.Color.White;
			this.cboPose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			this.cboPose.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboPose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboPose.KeyMember = null;
			this.cboPose.Location = new System.Drawing.Point(0, 0);
			this.cboPose.Name = "cboPose";
			this.cboPose.SelectedIndex = -1;
			this.cboPose.SelectedItem = null;
			this.cboPose.Size = new System.Drawing.Size(199, 21);
			this.cboPose.Sorted = true;
			this.cboPose.TabIndex = 0;
			// 
			// PoseMatchControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboPose);
			this.Name = "PoseMatchControl";
			this.Size = new System.Drawing.Size(489, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboPose;
	}
}

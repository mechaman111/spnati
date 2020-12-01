namespace SPNATI_Character_Editor.Controls.Pipelines.NodeControls
{
	partial class NodeEnumControl
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
			this.cboData = new Desktop.Skinning.SkinnedComboBox();
			this.SuspendLayout();
			// 
			// cboData
			// 
			this.cboData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboData.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboData.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboData.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cboData.KeyMember = null;
			this.cboData.Location = new System.Drawing.Point(0, 0);
			this.cboData.Name = "cboData";
			this.cboData.SelectedIndex = -1;
			this.cboData.SelectedItem = null;
			this.cboData.Size = new System.Drawing.Size(150, 21);
			this.cboData.Sorted = false;
			this.cboData.TabIndex = 0;
			this.cboData.Text = "skinnedComboBox1";
			// 
			// NodeEnum
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cboData);
			this.Name = "NodeEnum";
			this.Size = new System.Drawing.Size(150, 21);
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.Skinning.SkinnedComboBox cboData;
	}
}

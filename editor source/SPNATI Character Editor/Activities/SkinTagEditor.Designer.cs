namespace SPNATI_Character_Editor.Activities
{
	partial class SkinTagEditor
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.gridRemove = new System.Windows.Forms.DataGridView();
			this.gridAdd = new System.Windows.Forms.DataGridView();
			this.ColTagAdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColRemove = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridRemove)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAdd)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tags to Remove When Skin In Use";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(368, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(155, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Tags to Add When Skin In Use";
			// 
			// gridRemove
			// 
			this.gridRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridRemove.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridRemove.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTag,
            this.ColRemove});
			this.gridRemove.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridRemove.Location = new System.Drawing.Point(7, 16);
			this.gridRemove.MultiSelect = false;
			this.gridRemove.Name = "gridRemove";
			this.gridRemove.RowHeadersVisible = false;
			this.gridRemove.Size = new System.Drawing.Size(275, 552);
			this.gridRemove.TabIndex = 2;
			// 
			// gridAdd
			// 
			this.gridAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAdd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTagAdd});
			this.gridAdd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridAdd.Location = new System.Drawing.Point(371, 16);
			this.gridAdd.MultiSelect = false;
			this.gridAdd.Name = "gridAdd";
			this.gridAdd.RowHeadersVisible = false;
			this.gridAdd.Size = new System.Drawing.Size(275, 552);
			this.gridAdd.TabIndex = 3;
			this.gridAdd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridAdd_EditingControlShowing);
			// 
			// ColTagAdd
			// 
			this.ColTagAdd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTagAdd.HeaderText = "Tag";
			this.ColTagAdd.Name = "ColTagAdd";
			// 
			// ColTag
			// 
			this.ColTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColTag.HeaderText = "Tag";
			this.ColTag.Name = "ColTag";
			this.ColTag.ReadOnly = true;
			// 
			// ColRemove
			// 
			this.ColRemove.HeaderText = "Remove?";
			this.ColRemove.Name = "ColRemove";
			this.ColRemove.Width = 75;
			// 
			// SkinTagEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridAdd);
			this.Controls.Add(this.gridRemove);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "SkinTagEditor";
			this.Size = new System.Drawing.Size(756, 571);
			((System.ComponentModel.ISupportInitialize)(this.gridRemove)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAdd)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView gridRemove;
		private System.Windows.Forms.DataGridView gridAdd;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTagAdd;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTag;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColRemove;
	}
}

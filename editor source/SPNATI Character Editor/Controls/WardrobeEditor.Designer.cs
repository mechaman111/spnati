namespace SPNATI_Character_Editor.Controls
{
	partial class WardrobeEditor
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
			this.gridWardrobe = new System.Windows.Forms.DataGridView();
			this.cmdClothesDown = new System.Windows.Forms.Button();
			this.cmdClothesUp = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColLower = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColPosition = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridWardrobe)).BeginInit();
			this.SuspendLayout();
			// 
			// gridWardrobe
			// 
			this.gridWardrobe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridWardrobe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridWardrobe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColLower,
            this.ColType,
            this.ColPosition});
			this.gridWardrobe.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridWardrobe.Location = new System.Drawing.Point(3, 0);
			this.gridWardrobe.Name = "gridWardrobe";
			this.gridWardrobe.Size = new System.Drawing.Size(495, 338);
			this.gridWardrobe.TabIndex = 0;
			// 
			// cmdClothesDown
			// 
			this.cmdClothesDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClothesDown.Location = new System.Drawing.Point(504, 39);
			this.cmdClothesDown.Name = "cmdClothesDown";
			this.cmdClothesDown.Size = new System.Drawing.Size(35, 33);
			this.cmdClothesDown.TabIndex = 6;
			this.cmdClothesDown.Text = "▼";
			this.cmdClothesDown.UseVisualStyleBackColor = true;
			this.cmdClothesDown.Click += new System.EventHandler(this.cmdClothesDown_Click);
			// 
			// cmdClothesUp
			// 
			this.cmdClothesUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClothesUp.Location = new System.Drawing.Point(504, 0);
			this.cmdClothesUp.Name = "cmdClothesUp";
			this.cmdClothesUp.Size = new System.Drawing.Size(35, 33);
			this.cmdClothesUp.TabIndex = 5;
			this.cmdClothesUp.Text = "▲";
			this.cmdClothesUp.UseVisualStyleBackColor = true;
			this.cmdClothesUp.Click += new System.EventHandler(this.cmdClothesUp_Click);
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(3, 341);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(296, 13);
			this.label9.TabIndex = 15;
			this.label9.Text = "Note: Clothing is ordered from first layer to remove to last layer";
			// 
			// ColName
			// 
			this.ColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColName.HeaderText = "Name";
			this.ColName.Name = "ColName";
			// 
			// ColLower
			// 
			this.ColLower.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColLower.HeaderText = "Lowercase Name";
			this.ColLower.Name = "ColLower";
			// 
			// ColType
			// 
			this.ColType.HeaderText = "Type";
			this.ColType.Items.AddRange(new object[] {
            "extra",
            "minor",
            "major",
            "important"});
			this.ColType.Name = "ColType";
			// 
			// ColPosition
			// 
			this.ColPosition.HeaderText = "Position";
			this.ColPosition.Items.AddRange(new object[] {
            "upper",
            "lower",
            "other"});
			this.ColPosition.Name = "ColPosition";
			// 
			// WardrobeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label9);
			this.Controls.Add(this.cmdClothesDown);
			this.Controls.Add(this.cmdClothesUp);
			this.Controls.Add(this.gridWardrobe);
			this.Name = "WardrobeEditor";
			this.Size = new System.Drawing.Size(542, 354);
			((System.ComponentModel.ISupportInitialize)(this.gridWardrobe)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridWardrobe;
		private System.Windows.Forms.Button cmdClothesDown;
		private System.Windows.Forms.Button cmdClothesUp;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLower;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColType;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColPosition;
	}
}

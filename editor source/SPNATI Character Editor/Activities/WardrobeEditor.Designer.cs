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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColLower = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPlural = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColPosition = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridWardrobe)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridWardrobe
			// 
			this.gridWardrobe.AllowUserToResizeRows = false;
			this.gridWardrobe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridWardrobe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridWardrobe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColName,
            this.ColLower,
            this.ColPlural,
            this.ColType,
            this.ColPosition});
			this.gridWardrobe.Location = new System.Drawing.Point(3, 0);
			this.gridWardrobe.MultiSelect = false;
			this.gridWardrobe.Name = "gridWardrobe";
			this.gridWardrobe.Size = new System.Drawing.Size(939, 307);
			this.gridWardrobe.TabIndex = 0;
			this.gridWardrobe.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridWardrobe_CellValidated);
			this.gridWardrobe.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridWardrobe_CellValidating);
			this.gridWardrobe.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridWardrobe_RowsAdded);
			this.gridWardrobe.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.gridWardrobe_RowsRemoved);
			// 
			// cmdClothesDown
			// 
			this.cmdClothesDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClothesDown.Location = new System.Drawing.Point(948, 39);
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
			this.cmdClothesUp.Location = new System.Drawing.Point(948, 0);
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
			this.label9.Location = new System.Drawing.Point(3, 310);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(296, 13);
			this.label9.TabIndex = 15;
			this.label9.Text = "Note: Clothing is ordered from first layer to remove to last layer";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(6, 339);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(307, 76);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Type Glossary";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(197, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Important: covers nudity (eg. underwear)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(180, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Major: covers underwear (e.g. pants)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(182, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Minor: covers a little skin (e.g. jacket)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Extra: covers nothing of importance (e.g. necklace)";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Location = new System.Drawing.Point(319, 339);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(307, 76);
			this.groupBox2.TabIndex = 17;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Position Glossary";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 42);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(235, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Both: Covers both the chest area and the crotch";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 55);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(142, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Other: covers any other area";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 29);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(149, 13);
			this.label7.TabIndex = 1;
			this.label7.Text = "Lower: covers the crotch area";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(145, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Upper: covers the chest area";
			// 
			// ColName
			// 
			this.ColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColName.HeaderText = "Proper Name";
			this.ColName.Name = "ColName";
			this.ColName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ColName.Visible = false;
			// 
			// ColLower
			// 
			this.ColLower.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColLower.HeaderText = "Name (lowercase)";
			this.ColLower.Name = "ColLower";
			this.ColLower.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColPlural
			// 
			this.ColPlural.HeaderText = "Is Plural?";
			this.ColPlural.Name = "ColPlural";
			this.ColPlural.Width = 50;
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
            "both",
            "other"});
			this.ColPosition.Name = "ColPosition";
			// 
			// WardrobeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.cmdClothesDown);
			this.Controls.Add(this.cmdClothesUp);
			this.Controls.Add(this.gridWardrobe);
			this.Name = "WardrobeEditor";
			this.Size = new System.Drawing.Size(986, 611);
			((System.ComponentModel.ISupportInitialize)(this.gridWardrobe)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridWardrobe;
		private System.Windows.Forms.Button cmdClothesDown;
		private System.Windows.Forms.Button cmdClothesUp;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLower;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColPlural;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColType;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColPosition;
	}
}

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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gridWardrobe = new Desktop.Skinning.SkinnedDataGridView();
			this.ColLower = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColGeneric = new Desktop.CommonControls.RecordColumn();
			this.ColPlural = new Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn();
			this.ColType = new Desktop.CommonControls.RecordColumn();
			this.ColPosition = new Desktop.CommonControls.RecordColumn();
			this.cmdClothesDown = new Desktop.Skinning.SkinnedButton();
			this.cmdClothesUp = new Desktop.Skinning.SkinnedButton();
			this.label9 = new Desktop.Skinning.SkinnedLabel();
			this.groupBox1 = new Desktop.Skinning.SkinnedGroupBox();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.groupBox2 = new Desktop.Skinning.SkinnedGroupBox();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.label7 = new Desktop.Skinning.SkinnedLabel();
			this.label8 = new Desktop.Skinning.SkinnedLabel();
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
			this.gridWardrobe.BackgroundColor = System.Drawing.Color.White;
			this.gridWardrobe.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gridWardrobe.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridWardrobe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridWardrobe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridWardrobe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLower,
            this.ColGeneric,
            this.ColPlural,
            this.ColType,
            this.ColPosition});
			this.gridWardrobe.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridWardrobe.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridWardrobe.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridWardrobe.EnableHeadersVisualStyles = false;
			this.gridWardrobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.gridWardrobe.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(153)))), ((int)(((byte)(243)))));
			this.gridWardrobe.Location = new System.Drawing.Point(3, 3);
			this.gridWardrobe.MultiSelect = false;
			this.gridWardrobe.Name = "gridWardrobe";
			this.gridWardrobe.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridWardrobe.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridWardrobe.Size = new System.Drawing.Size(939, 307);
			this.gridWardrobe.TabIndex = 0;
			this.gridWardrobe.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridWardrobe_CellValidated);
			this.gridWardrobe.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridWardrobe_CellValidating);
			this.gridWardrobe.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridWardrobe_RowsAdded);
			this.gridWardrobe.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.gridWardrobe_RowsRemoved);
			// 
			// ColLower
			// 
			this.ColLower.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColLower.HeaderText = "Name (lowercase)";
			this.ColLower.Name = "ColLower";
			this.ColLower.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColGeneric
			// 
			this.ColGeneric.HeaderText = "Classification";
			this.ColGeneric.Name = "ColGeneric";
			this.ColGeneric.RecordType = null;
			this.ColGeneric.Width = 150;
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
			this.ColType.Name = "ColType";
			// 
			// ColPosition
			// 
			this.ColPosition.HeaderText = "Position";
			this.ColPosition.Name = "ColPosition";
			// 
			// cmdClothesDown
			// 
			this.cmdClothesDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClothesDown.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdClothesDown.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdClothesDown.Flat = true;
			this.cmdClothesDown.ForeColor = System.Drawing.Color.Blue;
			this.cmdClothesDown.Image = global::SPNATI_Character_Editor.Properties.Resources.DownArrow;
			this.cmdClothesDown.Location = new System.Drawing.Point(948, 42);
			this.cmdClothesDown.Name = "cmdClothesDown";
			this.cmdClothesDown.Size = new System.Drawing.Size(35, 33);
			this.cmdClothesDown.TabIndex = 6;
			this.cmdClothesDown.UseVisualStyleBackColor = true;
			this.cmdClothesDown.Click += new System.EventHandler(this.cmdClothesDown_Click);
			// 
			// cmdClothesUp
			// 
			this.cmdClothesUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdClothesUp.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdClothesUp.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdClothesUp.Flat = true;
			this.cmdClothesUp.ForeColor = System.Drawing.Color.Blue;
			this.cmdClothesUp.Image = global::SPNATI_Character_Editor.Properties.Resources.UpArrow;
			this.cmdClothesUp.Location = new System.Drawing.Point(948, 3);
			this.cmdClothesUp.Name = "cmdClothesUp";
			this.cmdClothesUp.Size = new System.Drawing.Size(35, 33);
			this.cmdClothesUp.TabIndex = 5;
			this.cmdClothesUp.UseVisualStyleBackColor = true;
			this.cmdClothesUp.Click += new System.EventHandler(this.cmdClothesUp_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label9.ForeColor = System.Drawing.Color.Black;
			this.label9.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label9.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label9.Location = new System.Drawing.Point(3, 313);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(296, 13);
			this.label9.TabIndex = 15;
			this.label9.Text = "Note: Clothing is ordered from first layer to remove to last layer";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.White;
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.groupBox1.Image = null;
			this.groupBox1.Location = new System.Drawing.Point(6, 342);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.groupBox1.ShowIndicatorBar = false;
			this.groupBox1.Size = new System.Drawing.Size(307, 99);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Type Glossary";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label4.Location = new System.Drawing.Point(6, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(197, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Important: covers nudity (eg. underwear)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(6, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(180, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Major: covers underwear (e.g. pants)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(6, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(182, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Minor: covers a little skin (e.g. jacket)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(6, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Extra: covers nothing of importance (e.g. necklace)";
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.White;
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.groupBox2.Image = null;
			this.groupBox2.Location = new System.Drawing.Point(319, 342);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.groupBox2.ShowIndicatorBar = false;
			this.groupBox2.Size = new System.Drawing.Size(307, 99);
			this.groupBox2.TabIndex = 17;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Position Glossary";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(6, 50);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(235, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Both: Covers both the chest area and the crotch";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.Color.Black;
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label6.Location = new System.Drawing.Point(6, 63);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(142, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Other: covers any other area";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label7.ForeColor = System.Drawing.Color.Black;
			this.label7.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label7.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label7.Location = new System.Drawing.Point(6, 37);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(149, 13);
			this.label7.TabIndex = 1;
			this.label7.Text = "Lower: covers the crotch area";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label8.ForeColor = System.Drawing.Color.Black;
			this.label8.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label8.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label8.Location = new System.Drawing.Point(6, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(145, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Upper: covers the chest area";
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

		private Desktop.Skinning.SkinnedDataGridView gridWardrobe;
		private Desktop.Skinning.SkinnedButton cmdClothesDown;
		private Desktop.Skinning.SkinnedButton cmdClothesUp;
		private Desktop.Skinning.SkinnedLabel label9;
		private Desktop.Skinning.SkinnedGroupBox groupBox1;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedGroupBox groupBox2;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedLabel label7;
		private Desktop.Skinning.SkinnedLabel label8;
		private Desktop.Skinning.SkinnedLabel label5;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColLower;
		private Desktop.CommonControls.RecordColumn ColGeneric;
		private Desktop.Skinning.SkinnedDataGridViewCheckBoxColumn ColPlural;
		private Desktop.CommonControls.RecordColumn ColType;
		private Desktop.CommonControls.RecordColumn ColPosition;
	}
}

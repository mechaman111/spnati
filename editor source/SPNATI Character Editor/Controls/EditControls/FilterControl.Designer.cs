namespace SPNATI_Character_Editor
{
	partial class FilterControl
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.recTag = new Desktop.CommonControls.RecordField();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.cboGender = new Desktop.Skinning.SkinnedComboBox();
			this.valFrom = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.valTo = new Desktop.Skinning.SkinnedNumericUpDown();
			this.cmdExpand = new Desktop.Skinning.SkinnedIcon();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableAdvanced = new Desktop.CommonControls.PropertyTable();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(84, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "of Tag:";
			// 
			// recTag
			// 
			this.recTag.AllowCreate = true;
			this.recTag.Location = new System.Drawing.Point(126, 1);
			this.recTag.Name = "recTag";
			this.recTag.PlaceholderText = null;
			this.recTag.Record = null;
			this.recTag.RecordContext = null;
			this.recTag.RecordFilter = null;
			this.recTag.RecordKey = null;
			this.recTag.RecordType = null;
			this.recTag.Size = new System.Drawing.Size(98, 20);
			this.recTag.TabIndex = 1;
			this.recTag.UseAutoComplete = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(230, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Gender:";
			// 
			// cboGender
			// 
			this.cboGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboGender.BackColor = System.Drawing.Color.White;
			this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGender.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboGender.FormattingEnabled = true;
			this.cboGender.Location = new System.Drawing.Point(281, 0);
			this.cboGender.Name = "cboGender";
			this.cboGender.SelectedIndex = -1;
			this.cboGender.SelectedItem = null;
			this.cboGender.Size = new System.Drawing.Size(62, 21);
			this.cboGender.Sorted = false;
			this.cboGender.TabIndex = 3;
			// 
			// valFrom
			// 
			this.valFrom.BackColor = System.Drawing.Color.White;
			this.valFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valFrom.ForeColor = System.Drawing.Color.Black;
			this.valFrom.Location = new System.Drawing.Point(3, 0);
			this.valFrom.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.valFrom.Name = "valFrom";
			this.valFrom.Size = new System.Drawing.Size(30, 20);
			this.valFrom.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(33, 2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "to:";
			// 
			// valTo
			// 
			this.valTo.BackColor = System.Drawing.Color.White;
			this.valTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valTo.ForeColor = System.Drawing.Color.Black;
			this.valTo.Location = new System.Drawing.Point(48, 0);
			this.valTo.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.valTo.Name = "valTo";
			this.valTo.Size = new System.Drawing.Size(30, 20);
			this.valTo.TabIndex = 8;
			// 
			// cmdExpand
			// 
			this.cmdExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdExpand.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdExpand.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdExpand.Flat = false;
			this.cmdExpand.FlatAppearance.BorderSize = 0;
			this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdExpand.Image = global::SPNATI_Character_Editor.Properties.Resources.ChevronDown;
			this.cmdExpand.Location = new System.Drawing.Point(703, 0);
			this.cmdExpand.Margin = new System.Windows.Forms.Padding(0);
			this.cmdExpand.Name = "cmdExpand";
			this.cmdExpand.Size = new System.Drawing.Size(16, 20);
			this.cmdExpand.TabIndex = 9;
			this.toolTip1.SetToolTip(this.cmdExpand, "Toggle advanced options");
			this.cmdExpand.UseVisualStyleBackColor = true;
			this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
			// 
			// tableAdvanced
			// 
			this.tableAdvanced.AllowDelete = true;
			this.tableAdvanced.AllowFavorites = false;
			this.tableAdvanced.AllowHelp = false;
			this.tableAdvanced.AllowMacros = false;
			this.tableAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableAdvanced.BackColor = System.Drawing.Color.Transparent;
			this.tableAdvanced.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableAdvanced.Data = null;
			this.tableAdvanced.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Secondary;
			this.tableAdvanced.HideAddField = true;
			this.tableAdvanced.HideSpeedButtons = false;
			this.tableAdvanced.Location = new System.Drawing.Point(3, 27);
			this.tableAdvanced.ModifyingProperty = null;
			this.tableAdvanced.Name = "tableAdvanced";
			this.tableAdvanced.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.tableAdvanced.PlaceholderText = null;
			this.tableAdvanced.PreserveControls = true;
			this.tableAdvanced.PreviewData = null;
			this.tableAdvanced.RemoveCaption = "Remove";
			this.tableAdvanced.RowHeaderWidth = 100F;
			this.tableAdvanced.RunInitialAddEvents = false;
			this.tableAdvanced.Size = new System.Drawing.Size(716, 89);
			this.tableAdvanced.Sorted = true;
			this.tableAdvanced.TabIndex = 10;
			this.tableAdvanced.UndoManager = null;
			this.tableAdvanced.UseAutoComplete = true;
			// 
			// FilterControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableAdvanced);
			this.Controls.Add(this.cmdExpand);
			this.Controls.Add(this.valTo);
			this.Controls.Add(this.valFrom);
			this.Controls.Add(this.cboGender);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.recTag);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Name = "FilterControl";
			this.Size = new System.Drawing.Size(723, 119);
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.CommonControls.RecordField recTag;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedComboBox cboGender;
		private Desktop.Skinning.SkinnedNumericUpDown valFrom;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedNumericUpDown valTo;
		private Desktop.Skinning.SkinnedIcon cmdExpand;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.CommonControls.PropertyTable tableAdvanced;
	}
}

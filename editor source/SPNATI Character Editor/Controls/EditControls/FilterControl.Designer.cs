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
			this.recWho = new Desktop.CommonControls.RecordField();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableAdvanced = new Desktop.CommonControls.PropertyTable();
			this.pnlRange = new System.Windows.Forms.Panel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.valTo = new Desktop.Skinning.SkinnedNumericUpDown();
			this.valFrom = new Desktop.Skinning.SkinnedNumericUpDown();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.pnlCharacter = new System.Windows.Forms.Panel();
			this.recCharacter = new Desktop.CommonControls.RecordField();
			this.skinnedLabel3 = new Desktop.Skinning.SkinnedLabel();
			this.pnlVariable = new System.Windows.Forms.Panel();
			this.txtVariable = new Desktop.Skinning.SkinnedTextBox();
			this.skinnedLabel4 = new Desktop.Skinning.SkinnedLabel();
			this.cmdExpand = new Desktop.Skinning.SkinnedIcon();
			this.grpContainer = new Desktop.Skinning.SkinnedGroupBox();
			this.pnlNot = new System.Windows.Forms.Panel();
			this.chkNot = new Desktop.Skinning.SkinnedCheckBox();
			this.pnlRange.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).BeginInit();
			this.pnlCharacter.SuspendLayout();
			this.pnlVariable.SuspendLayout();
			this.grpContainer.SuspendLayout();
			this.pnlNot.SuspendLayout();
			this.SuspendLayout();
			// 
			// recWho
			// 
			this.recWho.AllowCreate = false;
			this.recWho.Location = new System.Drawing.Point(36, 2);
			this.recWho.Name = "recWho";
			this.recWho.PlaceholderText = null;
			this.recWho.Record = null;
			this.recWho.RecordContext = null;
			this.recWho.RecordFilter = null;
			this.recWho.RecordKey = null;
			this.recWho.RecordType = null;
			this.recWho.Size = new System.Drawing.Size(94, 20);
			this.recWho.TabIndex = 24;
			this.recWho.UseAutoComplete = true;
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
			this.tableAdvanced.Data = null;
			this.tableAdvanced.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.tableAdvanced.HideAddField = true;
			this.tableAdvanced.HideSpeedButtons = false;
			this.tableAdvanced.Location = new System.Drawing.Point(6, 22);
			this.tableAdvanced.ModifyingProperty = null;
			this.tableAdvanced.Name = "tableAdvanced";
			this.tableAdvanced.PanelType = Desktop.Skinning.SkinnedBackgroundType.Transparent;
			this.tableAdvanced.PlaceholderText = "Add a condition...";
			this.tableAdvanced.PreserveControls = true;
			this.tableAdvanced.PreviewData = null;
			this.tableAdvanced.RemoveCaption = "Remove";
			this.tableAdvanced.RowHeaderWidth = 120F;
			this.tableAdvanced.RunInitialAddEvents = true;
			this.tableAdvanced.Size = new System.Drawing.Size(835, 93);
			this.tableAdvanced.Sorted = true;
			this.tableAdvanced.TabIndex = 25;
			this.tableAdvanced.UndoManager = null;
			this.tableAdvanced.UseAutoComplete = true;
			// 
			// pnlRange
			// 
			this.pnlRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRange.Controls.Add(this.skinnedLabel2);
			this.pnlRange.Controls.Add(this.valTo);
			this.pnlRange.Controls.Add(this.valFrom);
			this.pnlRange.Controls.Add(this.label3);
			this.pnlRange.Location = new System.Drawing.Point(569, 1);
			this.pnlRange.Margin = new System.Windows.Forms.Padding(0);
			this.pnlRange.Name = "pnlRange";
			this.pnlRange.Size = new System.Drawing.Size(141, 22);
			this.pnlRange.TabIndex = 27;
			this.pnlRange.Visible = false;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel2.Location = new System.Drawing.Point(-3, 4);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(38, 13);
			this.skinnedLabel2.TabIndex = 12;
			this.skinnedLabel2.Text = "Count:";
			// 
			// valTo
			// 
			this.valTo.BackColor = System.Drawing.Color.White;
			this.valTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valTo.ForeColor = System.Drawing.Color.Black;
			this.valTo.Location = new System.Drawing.Point(99, 1);
			this.valTo.Margin = new System.Windows.Forms.Padding(0);
			this.valTo.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.valTo.Name = "valTo";
			this.valTo.Size = new System.Drawing.Size(36, 20);
			this.valTo.TabIndex = 11;
			// 
			// valFrom
			// 
			this.valFrom.BackColor = System.Drawing.Color.White;
			this.valFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valFrom.ForeColor = System.Drawing.Color.Black;
			this.valFrom.Location = new System.Drawing.Point(40, 1);
			this.valFrom.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.valFrom.Name = "valFrom";
			this.valFrom.Size = new System.Drawing.Size(36, 20);
			this.valFrom.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(78, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "to:";
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(4, 5);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(32, 13);
			this.skinnedLabel1.TabIndex = 26;
			this.skinnedLabel1.Text = "Filter:";
			// 
			// pnlCharacter
			// 
			this.pnlCharacter.Controls.Add(this.recCharacter);
			this.pnlCharacter.Controls.Add(this.skinnedLabel3);
			this.pnlCharacter.Location = new System.Drawing.Point(135, 1);
			this.pnlCharacter.Margin = new System.Windows.Forms.Padding(0);
			this.pnlCharacter.Name = "pnlCharacter";
			this.pnlCharacter.Size = new System.Drawing.Size(136, 22);
			this.pnlCharacter.TabIndex = 28;
			this.pnlCharacter.Visible = false;
			// 
			// recCharacter
			// 
			this.recCharacter.AllowCreate = false;
			this.recCharacter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.recCharacter.Location = new System.Drawing.Point(15, 1);
			this.recCharacter.Name = "recCharacter";
			this.recCharacter.PlaceholderText = null;
			this.recCharacter.Record = null;
			this.recCharacter.RecordContext = null;
			this.recCharacter.RecordFilter = null;
			this.recCharacter.RecordKey = null;
			this.recCharacter.RecordType = null;
			this.recCharacter.Size = new System.Drawing.Size(118, 20);
			this.recCharacter.TabIndex = 13;
			this.recCharacter.UseAutoComplete = true;
			// 
			// skinnedLabel3
			// 
			this.skinnedLabel3.AutoSize = true;
			this.skinnedLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel3.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel3.Location = new System.Drawing.Point(-3, 4);
			this.skinnedLabel3.Name = "skinnedLabel3";
			this.skinnedLabel3.Size = new System.Drawing.Size(17, 13);
			this.skinnedLabel3.TabIndex = 12;
			this.skinnedLabel3.Text = "is:";
			// 
			// pnlVariable
			// 
			this.pnlVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlVariable.Controls.Add(this.txtVariable);
			this.pnlVariable.Controls.Add(this.skinnedLabel4);
			this.pnlVariable.Location = new System.Drawing.Point(710, 2);
			this.pnlVariable.Margin = new System.Windows.Forms.Padding(0);
			this.pnlVariable.Name = "pnlVariable";
			this.pnlVariable.Size = new System.Drawing.Size(110, 22);
			this.pnlVariable.TabIndex = 29;
			this.pnlVariable.Visible = false;
			// 
			// txtVariable
			// 
			this.txtVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtVariable.BackColor = System.Drawing.Color.White;
			this.txtVariable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtVariable.ForeColor = System.Drawing.Color.Black;
			this.txtVariable.Location = new System.Drawing.Point(50, 0);
			this.txtVariable.Name = "txtVariable";
			this.txtVariable.Size = new System.Drawing.Size(60, 20);
			this.txtVariable.TabIndex = 13;
			// 
			// skinnedLabel4
			// 
			this.skinnedLabel4.AutoSize = true;
			this.skinnedLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel4.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel4.Location = new System.Drawing.Point(3, 3);
			this.skinnedLabel4.Name = "skinnedLabel4";
			this.skinnedLabel4.Size = new System.Drawing.Size(48, 13);
			this.skinnedLabel4.TabIndex = 12;
			this.skinnedLabel4.Text = "Variable:";
			// 
			// cmdExpand
			// 
			this.cmdExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdExpand.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdExpand.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdExpand.Flat = false;
			this.cmdExpand.Image = global::SPNATI_Character_Editor.Properties.Resources.ChevronDown;
			this.cmdExpand.Location = new System.Drawing.Point(820, 1);
			this.cmdExpand.Name = "cmdExpand";
			this.cmdExpand.Size = new System.Drawing.Size(21, 21);
			this.cmdExpand.TabIndex = 30;
			this.cmdExpand.Text = "skinnedIcon1";
			this.cmdExpand.UseVisualStyleBackColor = true;
			this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
			// 
			// grpContainer
			// 
			this.grpContainer.BackColor = System.Drawing.Color.White;
			this.grpContainer.Controls.Add(this.pnlNot);
			this.grpContainer.Controls.Add(this.cmdExpand);
			this.grpContainer.Controls.Add(this.tableAdvanced);
			this.grpContainer.Controls.Add(this.recWho);
			this.grpContainer.Controls.Add(this.pnlVariable);
			this.grpContainer.Controls.Add(this.pnlRange);
			this.grpContainer.Controls.Add(this.pnlCharacter);
			this.grpContainer.Controls.Add(this.skinnedLabel1);
			this.grpContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpContainer.Highlight = Desktop.Skinning.SkinnedHighlight.Heading;
			this.grpContainer.Image = null;
			this.grpContainer.Location = new System.Drawing.Point(0, 0);
			this.grpContainer.Name = "grpContainer";
			this.grpContainer.PanelType = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.grpContainer.ShowIndicatorBar = true;
			this.grpContainer.Size = new System.Drawing.Size(844, 119);
			this.grpContainer.TabIndex = 31;
			this.grpContainer.TabStop = false;
			// 
			// pnlNot
			// 
			this.pnlNot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlNot.Controls.Add(this.chkNot);
			this.pnlNot.Location = new System.Drawing.Point(773, 1);
			this.pnlNot.Margin = new System.Windows.Forms.Padding(0);
			this.pnlNot.Name = "pnlNot";
			this.pnlNot.Size = new System.Drawing.Size(48, 22);
			this.pnlNot.TabIndex = 31;
			this.pnlNot.Visible = false;
			// 
			// chkNot
			// 
			this.chkNot.AutoSize = true;
			this.chkNot.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkNot.Location = new System.Drawing.Point(4, 3);
			this.chkNot.Name = "chkNot";
			this.chkNot.Size = new System.Drawing.Size(43, 17);
			this.chkNot.TabIndex = 0;
			this.chkNot.Text = "Not";
			this.chkNot.UseVisualStyleBackColor = true;
			// 
			// FilterControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpContainer);
			this.Name = "FilterControl";
			this.Size = new System.Drawing.Size(844, 119);
			this.pnlRange.ResumeLayout(false);
			this.pnlRange.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.valTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.valFrom)).EndInit();
			this.pnlCharacter.ResumeLayout(false);
			this.pnlCharacter.PerformLayout();
			this.pnlVariable.ResumeLayout(false);
			this.pnlVariable.PerformLayout();
			this.grpContainer.ResumeLayout(false);
			this.grpContainer.PerformLayout();
			this.pnlNot.ResumeLayout(false);
			this.pnlNot.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Desktop.CommonControls.RecordField recWho;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.CommonControls.PropertyTable tableAdvanced;
		private System.Windows.Forms.Panel pnlRange;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedNumericUpDown valTo;
		private Desktop.Skinning.SkinnedNumericUpDown valFrom;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private System.Windows.Forms.Panel pnlCharacter;
		private Desktop.CommonControls.RecordField recCharacter;
		private Desktop.Skinning.SkinnedLabel skinnedLabel3;
		private System.Windows.Forms.Panel pnlVariable;
		private Desktop.Skinning.SkinnedTextBox txtVariable;
		private Desktop.Skinning.SkinnedLabel skinnedLabel4;
		private Desktop.Skinning.SkinnedIcon cmdExpand;
		private Desktop.Skinning.SkinnedGroupBox grpContainer;
		private System.Windows.Forms.Panel pnlNot;
		private Desktop.Skinning.SkinnedCheckBox chkNot;
	}
}

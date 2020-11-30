namespace SPNATI_Character_Editor.Forms
{
	partial class PoseSelectionForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdCreate = new Desktop.Skinning.SkinnedButton();
			this.tabControl = new Desktop.Skinning.SkinnedTabControl();
			this.tabStrip = new Desktop.Skinning.SkinnedTabStrip();
			this.grid = new Desktop.Skinning.SkinnedDataGridView();
			this.preview = new SPNATI_Character_Editor.Controls.CharacterImageBox();
			this.cmdGenerate = new Desktop.Skinning.SkinnedButton();
			this.recCharacter = new Desktop.CommonControls.RecordField();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.lblMissingMatrix = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdCreate);
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 605);
			this.skinnedPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(1113, 30);
			this.skinnedPanel1.TabIndex = 1;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(1035, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdCreate
			// 
			this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCreate.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCreate.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCreate.Flat = false;
			this.cmdCreate.Location = new System.Drawing.Point(954, 3);
			this.cmdCreate.Name = "cmdCreate";
			this.cmdCreate.Size = new System.Drawing.Size(75, 23);
			this.cmdCreate.TabIndex = 0;
			this.cmdCreate.Text = "OK";
			this.cmdCreate.UseVisualStyleBackColor = true;
			this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
			// 
			// tabControl
			// 
			this.tabControl.Location = new System.Drawing.Point(716, 34);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(72, 100);
			this.tabControl.TabIndex = 5;
			this.tabControl.Visible = false;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabStrip
			// 
			this.tabStrip.AddCaption = "";
			this.tabStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabStrip.DecorationText = null;
			this.tabStrip.Location = new System.Drawing.Point(9, 30);
			this.tabStrip.Margin = new System.Windows.Forms.Padding(0);
			this.tabStrip.Name = "tabStrip";
			this.tabStrip.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tabStrip.ShowAddButton = false;
			this.tabStrip.ShowCloseButton = false;
			this.tabStrip.Size = new System.Drawing.Size(848, 30);
			this.tabStrip.StartMargin = 5;
			this.tabStrip.TabControl = this.tabControl;
			this.tabStrip.TabIndex = 4;
			this.tabStrip.TabMargin = 5;
			this.tabStrip.TabPadding = 20;
			this.tabStrip.TabSize = -1;
			this.tabStrip.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tabStrip.Text = "skinnedTabStrip1";
			this.tabStrip.Vertical = false;
			// 
			// grid
			// 
			this.grid.AllowUserToAddRows = false;
			this.grid.AllowUserToDeleteRows = false;
			this.grid.AllowUserToOrderColumns = true;
			this.grid.AllowUserToResizeColumns = false;
			this.grid.AllowUserToResizeRows = false;
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.grid.BackgroundColor = System.Drawing.Color.White;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Data = null;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.grid.DefaultCellStyle = dataGridViewCellStyle2;
			this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.grid.EnableHeadersVisualStyles = false;
			this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.grid.GridColor = System.Drawing.Color.LightGray;
			this.grid.Location = new System.Drawing.Point(9, 61);
			this.grid.Margin = new System.Windows.Forms.Padding(0);
			this.grid.MultiSelect = false;
			this.grid.Name = "grid";
			this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.grid.ShowEditingIcon = false;
			this.grid.Size = new System.Drawing.Size(848, 541);
			this.grid.TabIndex = 6;
			this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
			// 
			// preview
			// 
			this.preview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.preview.Location = new System.Drawing.Point(860, 28);
			this.preview.Name = "preview";
			this.preview.Size = new System.Drawing.Size(250, 574);
			this.preview.TabIndex = 7;
			// 
			// cmdGenerate
			// 
			this.cmdGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGenerate.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdGenerate.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdGenerate.Flat = false;
			this.cmdGenerate.Location = new System.Drawing.Point(952, 28);
			this.cmdGenerate.Name = "cmdGenerate";
			this.cmdGenerate.Size = new System.Drawing.Size(156, 23);
			this.cmdGenerate.TabIndex = 8;
			this.cmdGenerate.Text = "Generate Preview";
			this.cmdGenerate.UseVisualStyleBackColor = true;
			this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
			// 
			// recCharacter
			// 
			this.recCharacter.AllowCreate = false;
			this.recCharacter.Location = new System.Drawing.Point(56, 31);
			this.recCharacter.Name = "recCharacter";
			this.recCharacter.PlaceholderText = null;
			this.recCharacter.Record = null;
			this.recCharacter.RecordContext = null;
			this.recCharacter.RecordFilter = null;
			this.recCharacter.RecordKey = null;
			this.recCharacter.RecordType = null;
			this.recCharacter.Size = new System.Drawing.Size(150, 20);
			this.recCharacter.TabIndex = 9;
			this.recCharacter.UseAutoComplete = false;
			this.recCharacter.Visible = false;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 33);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(44, 13);
			this.skinnedLabel1.TabIndex = 10;
			this.skinnedLabel1.Text = "Source:";
			this.skinnedLabel1.Visible = false;
			// 
			// lblMissingMatrix
			// 
			this.lblMissingMatrix.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblMissingMatrix.AutoSize = true;
			this.lblMissingMatrix.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblMissingMatrix.ForeColor = System.Drawing.Color.Red;
			this.lblMissingMatrix.Highlight = Desktop.Skinning.SkinnedHighlight.Bad;
			this.lblMissingMatrix.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblMissingMatrix.Location = new System.Drawing.Point(344, 316);
			this.lblMissingMatrix.Name = "lblMissingMatrix";
			this.lblMissingMatrix.Size = new System.Drawing.Size(182, 21);
			this.lblMissingMatrix.TabIndex = 11;
			this.lblMissingMatrix.Text = "No Pose Matrix Available";
			this.lblMissingMatrix.Visible = false;
			// 
			// PoseSelectionForm
			// 
			this.AcceptButton = this.cmdCreate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(1112, 635);
			this.Controls.Add(this.lblMissingMatrix);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.recCharacter);
			this.Controls.Add(this.cmdGenerate);
			this.Controls.Add(this.preview);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.tabStrip);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "PoseSelectionForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select a Cell";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PoseSelectionForm_FormClosing);
			this.skinnedPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdCreate;
		private Desktop.Skinning.SkinnedTabControl tabControl;
		private Desktop.Skinning.SkinnedTabStrip tabStrip;
		private Desktop.Skinning.SkinnedDataGridView grid;
		private Controls.CharacterImageBox preview;
		private Desktop.Skinning.SkinnedButton cmdGenerate;
		private Desktop.CommonControls.RecordField recCharacter;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.Skinning.SkinnedLabel lblMissingMatrix;
	}
}
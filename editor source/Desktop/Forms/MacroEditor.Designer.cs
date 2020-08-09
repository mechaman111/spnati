namespace Desktop
{
	partial class MacroEditor
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
			this.components = new System.ComponentModel.Container();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.txtName = new Desktop.Skinning.SkinnedTextBox();
			this.lblHelp = new Desktop.Skinning.SkinnedLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableConditions = new Desktop.CommonControls.PropertyTable();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.cboMenu = new Desktop.Skinning.SkinnedComboBox();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.Location = new System.Drawing.Point(746, 3);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "Accept";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(827, 3);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(13, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtName.ForeColor = System.Drawing.Color.Black;
			this.txtName.Location = new System.Drawing.Point(57, 34);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(371, 20);
			this.txtName.TabIndex = 1;
			// 
			// lblHelp
			// 
			this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblHelp.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblHelp.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblHelp.Image = global::Desktop.Properties.Resources.Help;
			this.lblHelp.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblHelp.Location = new System.Drawing.Point(5, 4);
			this.lblHelp.Margin = new System.Windows.Forms.Padding(3);
			this.lblHelp.Name = "lblHelp";
			this.lblHelp.Size = new System.Drawing.Size(20, 22);
			this.lblHelp.TabIndex = 50;
			this.toolTip1.SetToolTip(this.lblHelp, "Show help");
			this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
			// 
			// tableConditions
			// 
			this.tableConditions.AllowDelete = true;
			this.tableConditions.AllowFavorites = false;
			this.tableConditions.AllowHelp = false;
			this.tableConditions.AllowMacros = false;
			this.tableConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableConditions.BackColor = System.Drawing.Color.White;
			this.tableConditions.Data = null;
			this.tableConditions.HeaderType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tableConditions.HideAddField = true;
			this.tableConditions.HideSpeedButtons = false;
			this.tableConditions.Location = new System.Drawing.Point(12, 59);
			this.tableConditions.ModifyingProperty = null;
			this.tableConditions.Name = "tableConditions";
			this.tableConditions.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.tableConditions.PlaceholderText = "Add a condition...";
			this.tableConditions.PreserveControls = false;
			this.tableConditions.PreviewData = null;
			this.tableConditions.RemoveCaption = "Remove";
			this.tableConditions.RowHeaderWidth = 0F;
			this.tableConditions.RunInitialAddEvents = true;
			this.tableConditions.Size = new System.Drawing.Size(881, 233);
			this.tableConditions.Sorted = false;
			this.tableConditions.TabIndex = 2;
			this.tableConditions.UndoManager = null;
			this.tableConditions.UseAutoComplete = true;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.lblHelp);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 298);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(905, 30);
			this.skinnedPanel1.TabIndex = 51;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(434, 37);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(37, 13);
			this.skinnedLabel1.TabIndex = 52;
			this.skinnedLabel1.Text = "Menu:";
			// 
			// cboMenu
			// 
			this.cboMenu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
			this.cboMenu.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
			this.cboMenu.BackColor = System.Drawing.Color.White;
			this.cboMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMenu.FieldType = Desktop.Skinning.SkinnedFieldType.Surface;
			this.cboMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cboMenu.Location = new System.Drawing.Point(477, 33);
			this.cboMenu.Name = "cboMenu";
			this.cboMenu.SelectedIndex = -1;
			this.cboMenu.SelectedItem = null;
			this.cboMenu.Size = new System.Drawing.Size(154, 23);
			this.cboMenu.Sorted = false;
			this.cboMenu.TabIndex = 53;
			// 
			// MacroEditor
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(905, 328);
			this.Controls.Add(this.cboMenu);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tableConditions);
			this.Name = "MacroEditor";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Macro";
			this.Load += new System.EventHandler(this.MacroEditor_Load);
			this.Shown += new System.EventHandler(this.MacroEditor_Shown);
			this.skinnedPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CommonControls.PropertyTable tableConditions;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedTextBox txtName;
		private Desktop.Skinning.SkinnedLabel lblHelp;
		private System.Windows.Forms.ToolTip toolTip1;
		private Skinning.SkinnedPanel skinnedPanel1;
		private Skinning.SkinnedLabel skinnedLabel1;
		private Skinning.SkinnedComboBox cboMenu;
	}
}
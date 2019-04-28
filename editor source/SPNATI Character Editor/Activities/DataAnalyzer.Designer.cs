namespace SPNATI_Character_Editor.Activities
{
	partial class DataAnalyzer
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
			this.cmdLoad = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlStart = new System.Windows.Forms.Panel();
			this.pnlLoad = new System.Windows.Forms.Panel();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.label3 = new System.Windows.Forms.Label();
			this.pnlEdit = new System.Windows.Forms.Panel();
			this.grpResults = new System.Windows.Forms.GroupBox();
			this.lstResults = new System.Windows.Forms.ListBox();
			this.lblError = new System.Windows.Forms.Label();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.gridCriteria = new System.Windows.Forms.DataGridView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtCustomExpression = new System.Windows.Forms.TextBox();
			this.radCustom = new System.Windows.Forms.RadioButton();
			this.radOr = new System.Windows.Forms.RadioButton();
			this.radAnd = new System.Windows.Forms.RadioButton();
			this.tree = new Desktop.CommonControls.DBTreeView();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.ColIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColOperator = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.picHelp = new System.Windows.Forms.PictureBox();
			this.pnlStart.SuspendLayout();
			this.pnlLoad.SuspendLayout();
			this.pnlEdit.SuspendLayout();
			this.grpResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridCriteria)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdLoad
			// 
			this.cmdLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdLoad.Location = new System.Drawing.Point(152, 67);
			this.cmdLoad.Name = "cmdLoad";
			this.cmdLoad.Size = new System.Drawing.Size(221, 110);
			this.cmdLoad.TabIndex = 0;
			this.cmdLoad.Text = "Load";
			this.cmdLoad.UseVisualStyleBackColor = true;
			this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(65, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(390, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "This tool is used for reporting purposes to list all characters fulfilling certai" +
    "n criteria.";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(423, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Click Load to begin. This will take a long time as it must scan through all chara" +
    "cters\' files.";
			// 
			// pnlStart
			// 
			this.pnlStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlStart.Controls.Add(this.label1);
			this.pnlStart.Controls.Add(this.label2);
			this.pnlStart.Controls.Add(this.cmdLoad);
			this.pnlStart.Location = new System.Drawing.Point(178, 200);
			this.pnlStart.Name = "pnlStart";
			this.pnlStart.Size = new System.Drawing.Size(530, 198);
			this.pnlStart.TabIndex = 3;
			// 
			// pnlLoad
			// 
			this.pnlLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pnlLoad.Controls.Add(this.progressBar);
			this.pnlLoad.Controls.Add(this.label3);
			this.pnlLoad.Location = new System.Drawing.Point(178, 250);
			this.pnlLoad.Name = "pnlLoad";
			this.pnlLoad.Size = new System.Drawing.Size(530, 100);
			this.pnlLoad.TabIndex = 4;
			this.pnlLoad.Visible = false;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(3, 43);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(524, 23);
			this.progressBar.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(205, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107, 25);
			this.label3.TabIndex = 0;
			this.label3.Text = "Loading...";
			// 
			// pnlEdit
			// 
			this.pnlEdit.Controls.Add(this.label4);
			this.pnlEdit.Controls.Add(this.grpResults);
			this.pnlEdit.Controls.Add(this.cmdAdd);
			this.pnlEdit.Controls.Add(this.gridCriteria);
			this.pnlEdit.Controls.Add(this.groupBox1);
			this.pnlEdit.Controls.Add(this.tree);
			this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlEdit.Location = new System.Drawing.Point(0, 0);
			this.pnlEdit.Name = "pnlEdit";
			this.pnlEdit.Size = new System.Drawing.Size(886, 615);
			this.pnlEdit.TabIndex = 5;
			this.pnlEdit.Visible = false;
			// 
			// grpResults
			// 
			this.grpResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.grpResults.Controls.Add(this.lstResults);
			this.grpResults.Controls.Add(this.lblError);
			this.grpResults.Location = new System.Drawing.Point(209, 224);
			this.grpResults.Name = "grpResults";
			this.grpResults.Size = new System.Drawing.Size(555, 388);
			this.grpResults.TabIndex = 7;
			this.grpResults.TabStop = false;
			this.grpResults.Text = "Results";
			// 
			// lstResults
			// 
			this.lstResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstResults.FormattingEnabled = true;
			this.lstResults.Location = new System.Drawing.Point(6, 19);
			this.lstResults.Name = "lstResults";
			this.lstResults.Size = new System.Drawing.Size(182, 355);
			this.lstResults.TabIndex = 4;
			this.lstResults.DoubleClick += new System.EventHandler(this.lstResults_DoubleClick);
			// 
			// lblError
			// 
			this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblError.ForeColor = System.Drawing.Color.DarkRed;
			this.lblError.Location = new System.Drawing.Point(194, 19);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(355, 181);
			this.lblError.TabIndex = 6;
			this.lblError.Text = "Error message";
			this.lblError.Visible = false;
			// 
			// cmdAdd
			// 
			this.cmdAdd.Enabled = false;
			this.cmdAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.cmdAdd.Location = new System.Drawing.Point(178, 18);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(25, 23);
			this.cmdAdd.TabIndex = 3;
			this.toolTip1.SetToolTip(this.cmdAdd, "Add selected criteria");
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// gridCriteria
			// 
			this.gridCriteria.AllowUserToAddRows = false;
			this.gridCriteria.AllowUserToDeleteRows = false;
			this.gridCriteria.AllowUserToResizeRows = false;
			this.gridCriteria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridCriteria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIndex,
            this.ColData,
            this.ColOperator,
            this.ColValue,
            this.ColDelete});
			this.gridCriteria.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridCriteria.Location = new System.Drawing.Point(209, 60);
			this.gridCriteria.MultiSelect = false;
			this.gridCriteria.Name = "gridCriteria";
			this.gridCriteria.RowHeadersVisible = false;
			this.gridCriteria.Size = new System.Drawing.Size(555, 150);
			this.gridCriteria.TabIndex = 2;
			this.gridCriteria.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCriteria_CellContentClick);
			this.gridCriteria.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridCriteria_CellPainting);
			this.gridCriteria.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCriteria_CellValidated);
			this.gridCriteria.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridCriteria_EditingControlShowing);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.picHelp);
			this.groupBox1.Controls.Add(this.txtCustomExpression);
			this.groupBox1.Controls.Add(this.radCustom);
			this.groupBox1.Controls.Add(this.radOr);
			this.groupBox1.Controls.Add(this.radAnd);
			this.groupBox1.Location = new System.Drawing.Point(209, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(555, 51);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Expression";
			// 
			// txtCustomExpression
			// 
			this.txtCustomExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCustomExpression.Location = new System.Drawing.Point(200, 18);
			this.txtCustomExpression.Name = "txtCustomExpression";
			this.txtCustomExpression.Size = new System.Drawing.Size(326, 20);
			this.txtCustomExpression.TabIndex = 3;
			this.txtCustomExpression.Validated += new System.EventHandler(this.txtCustomExpression_Validated);
			// 
			// radCustom
			// 
			this.radCustom.AutoSize = true;
			this.radCustom.Location = new System.Drawing.Point(131, 19);
			this.radCustom.Name = "radCustom";
			this.radCustom.Size = new System.Drawing.Size(63, 17);
			this.radCustom.TabIndex = 2;
			this.radCustom.TabStop = true;
			this.radCustom.Text = "Custom:";
			this.radCustom.UseVisualStyleBackColor = true;
			this.radCustom.CheckedChanged += new System.EventHandler(this.radCustom_CheckedChanged);
			// 
			// radOr
			// 
			this.radOr.AutoSize = true;
			this.radOr.Location = new System.Drawing.Point(73, 19);
			this.radOr.Name = "radOr";
			this.radOr.Size = new System.Drawing.Size(41, 17);
			this.radOr.TabIndex = 1;
			this.radOr.TabStop = true;
			this.radOr.Text = "OR";
			this.radOr.UseVisualStyleBackColor = true;
			this.radOr.CheckedChanged += new System.EventHandler(this.radOr_CheckedChanged);
			// 
			// radAnd
			// 
			this.radAnd.AutoSize = true;
			this.radAnd.Location = new System.Drawing.Point(6, 19);
			this.radAnd.Name = "radAnd";
			this.radAnd.Size = new System.Drawing.Size(48, 17);
			this.radAnd.TabIndex = 0;
			this.radAnd.TabStop = true;
			this.radAnd.Text = "AND";
			this.radAnd.UseVisualStyleBackColor = true;
			this.radAnd.CheckedChanged += new System.EventHandler(this.radAnd_CheckedChanged);
			// 
			// tree
			// 
			this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tree.Location = new System.Drawing.Point(3, 19);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(169, 593);
			this.tree.TabIndex = 0;
			this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
			this.tree.DoubleClick += new System.EventHandler(this.tree_DoubleClick);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Data Points";
			// 
			// ColIndex
			// 
			this.ColIndex.HeaderText = "#";
			this.ColIndex.Name = "ColIndex";
			this.ColIndex.ReadOnly = true;
			this.ColIndex.Width = 21;
			// 
			// ColData
			// 
			this.ColData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColData.HeaderText = "Property";
			this.ColData.Name = "ColData";
			this.ColData.ReadOnly = true;
			this.ColData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColOperator
			// 
			this.ColOperator.HeaderText = "Operator";
			this.ColOperator.Name = "ColOperator";
			this.ColOperator.Width = 80;
			// 
			// ColValue
			// 
			this.ColValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColValue.HeaderText = "Value";
			this.ColValue.Name = "ColValue";
			this.ColValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// ColDelete
			// 
			this.ColDelete.HeaderText = "";
			this.ColDelete.Name = "ColDelete";
			this.ColDelete.Width = 21;
			// 
			// picHelp
			// 
			this.picHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picHelp.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.picHelp.Location = new System.Drawing.Point(532, 20);
			this.picHelp.Name = "picHelp";
			this.picHelp.Size = new System.Drawing.Size(16, 16);
			this.picHelp.TabIndex = 4;
			this.picHelp.TabStop = false;
			this.toolTip1.SetToolTip(this.picHelp, "ex. 1 OR (2 AND NOT 3)");
			// 
			// DataAnalyzer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlEdit);
			this.Controls.Add(this.pnlLoad);
			this.Controls.Add(this.pnlStart);
			this.Name = "DataAnalyzer";
			this.Size = new System.Drawing.Size(886, 615);
			this.pnlStart.ResumeLayout(false);
			this.pnlStart.PerformLayout();
			this.pnlLoad.ResumeLayout(false);
			this.pnlLoad.PerformLayout();
			this.pnlEdit.ResumeLayout(false);
			this.pnlEdit.PerformLayout();
			this.grpResults.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridCriteria)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cmdLoad;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlStart;
		private System.Windows.Forms.Panel pnlLoad;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel pnlEdit;
		private Desktop.CommonControls.DBTreeView tree;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtCustomExpression;
		private System.Windows.Forms.RadioButton radCustom;
		private System.Windows.Forms.RadioButton radOr;
		private System.Windows.Forms.RadioButton radAnd;
		private System.Windows.Forms.DataGridView gridCriteria;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ListBox lstResults;
		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.GroupBox grpResults;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColIndex;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColData;
		private System.Windows.Forms.DataGridViewComboBoxColumn ColOperator;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColValue;
		private System.Windows.Forms.DataGridViewButtonColumn ColDelete;
		private System.Windows.Forms.PictureBox picHelp;
	}
}

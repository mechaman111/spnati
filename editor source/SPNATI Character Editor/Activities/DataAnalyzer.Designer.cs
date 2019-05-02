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
			this.grpExamples = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.gridExample1 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.grpResults = new System.Windows.Forms.GroupBox();
			this.lstResults = new System.Windows.Forms.ListBox();
			this.lblError = new System.Windows.Forms.Label();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.gridCriteria = new System.Windows.Forms.DataGridView();
			this.ColIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColOperator = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.picHelp = new System.Windows.Forms.PictureBox();
			this.txtCustomExpression = new System.Windows.Forms.TextBox();
			this.radCustom = new System.Windows.Forms.RadioButton();
			this.radOr = new System.Windows.Forms.RadioButton();
			this.radAnd = new System.Windows.Forms.RadioButton();
			this.tree = new Desktop.CommonControls.DBTreeView();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.gridExample2 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.gridExample3 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pnlStart.SuspendLayout();
			this.pnlLoad.SuspendLayout();
			this.pnlEdit.SuspendLayout();
			this.grpExamples.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridExample1)).BeginInit();
			this.grpResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridCriteria)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridExample2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridExample3)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdLoad
			// 
			this.cmdLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cmdLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdLoad.Location = new System.Drawing.Point(293, 67);
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
			this.label1.Location = new System.Drawing.Point(206, 24);
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
			this.label2.Location = new System.Drawing.Point(189, 44);
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
			this.pnlStart.Size = new System.Drawing.Size(813, 198);
			this.pnlStart.TabIndex = 3;
			// 
			// pnlLoad
			// 
			this.pnlLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pnlLoad.Controls.Add(this.progressBar);
			this.pnlLoad.Controls.Add(this.label3);
			this.pnlLoad.Location = new System.Drawing.Point(319, 250);
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
			this.pnlEdit.Controls.Add(this.splitContainer1);
			this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlEdit.Location = new System.Drawing.Point(0, 0);
			this.pnlEdit.Name = "pnlEdit";
			this.pnlEdit.Size = new System.Drawing.Size(1169, 615);
			this.pnlEdit.TabIndex = 5;
			this.pnlEdit.Visible = false;
			// 
			// grpExamples
			// 
			this.grpExamples.Controls.Add(this.label9);
			this.grpExamples.Controls.Add(this.label10);
			this.grpExamples.Controls.Add(this.gridExample3);
			this.grpExamples.Controls.Add(this.label8);
			this.grpExamples.Controls.Add(this.label7);
			this.grpExamples.Controls.Add(this.gridExample2);
			this.grpExamples.Controls.Add(this.label6);
			this.grpExamples.Controls.Add(this.label5);
			this.grpExamples.Controls.Add(this.gridExample1);
			this.grpExamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpExamples.Location = new System.Drawing.Point(0, 0);
			this.grpExamples.Name = "grpExamples";
			this.grpExamples.Size = new System.Drawing.Size(345, 615);
			this.grpExamples.TabIndex = 9;
			this.grpExamples.TabStop = false;
			this.grpExamples.Text = "Examples";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(6, 120);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(333, 31);
			this.label6.TabIndex = 9;
			this.label6.Text = "Match characters that have the tag \"shy\" and also have the tag \"shaved.\"";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(309, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "AND";
			// 
			// gridExample1
			// 
			this.gridExample1.AllowUserToAddRows = false;
			this.gridExample1.AllowUserToDeleteRows = false;
			this.gridExample1.AllowUserToResizeRows = false;
			this.gridExample1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridExample1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridExample1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewTextBoxColumn3});
			this.gridExample1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridExample1.Enabled = false;
			this.gridExample1.Location = new System.Drawing.Point(6, 31);
			this.gridExample1.MultiSelect = false;
			this.gridExample1.Name = "gridExample1";
			this.gridExample1.RowHeadersVisible = false;
			this.gridExample1.Size = new System.Drawing.Size(333, 86);
			this.gridExample1.TabIndex = 7;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "#";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Width = 21;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "Property";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewComboBoxColumn1
			// 
			this.dataGridViewComboBoxColumn1.HeaderText = "Operator";
			this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
			this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewComboBoxColumn1.Width = 80;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn3.HeaderText = "Value";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Data Points";
			// 
			// grpResults
			// 
			this.grpResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpResults.Controls.Add(this.lstResults);
			this.grpResults.Controls.Add(this.lblError);
			this.grpResults.Location = new System.Drawing.Point(3, 201);
			this.grpResults.Name = "grpResults";
			this.grpResults.Size = new System.Drawing.Size(546, 414);
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
			this.lstResults.Size = new System.Drawing.Size(182, 381);
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
			this.lblError.Size = new System.Drawing.Size(346, 119);
			this.lblError.TabIndex = 6;
			this.lblError.Text = "Error message";
			this.lblError.Visible = false;
			// 
			// cmdAdd
			// 
			this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAdd.Enabled = false;
			this.cmdAdd.Image = global::SPNATI_Character_Editor.Properties.Resources.Add;
			this.cmdAdd.Location = new System.Drawing.Point(232, 27);
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
			this.gridCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridCriteria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridCriteria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIndex,
            this.ColData,
            this.ColOperator,
            this.ColValue,
            this.ColDelete});
			this.gridCriteria.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridCriteria.Location = new System.Drawing.Point(4, 47);
			this.gridCriteria.MultiSelect = false;
			this.gridCriteria.Name = "gridCriteria";
			this.gridCriteria.RowHeadersVisible = false;
			this.gridCriteria.Size = new System.Drawing.Size(545, 148);
			this.gridCriteria.TabIndex = 2;
			this.gridCriteria.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCriteria_CellContentClick);
			this.gridCriteria.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridCriteria_CellPainting);
			this.gridCriteria.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCriteria_CellValidated);
			this.gridCriteria.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridCriteria_EditingControlShowing);
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
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.picHelp);
			this.groupBox1.Controls.Add(this.txtCustomExpression);
			this.groupBox1.Controls.Add(this.radCustom);
			this.groupBox1.Controls.Add(this.radOr);
			this.groupBox1.Controls.Add(this.radAnd);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(546, 51);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Expression";
			// 
			// picHelp
			// 
			this.picHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picHelp.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.picHelp.Location = new System.Drawing.Point(523, 20);
			this.picHelp.Name = "picHelp";
			this.picHelp.Size = new System.Drawing.Size(16, 16);
			this.picHelp.TabIndex = 4;
			this.picHelp.TabStop = false;
			this.toolTip1.SetToolTip(this.picHelp, "ex. 1 OR (2 AND NOT 3)");
			// 
			// txtCustomExpression
			// 
			this.txtCustomExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCustomExpression.Location = new System.Drawing.Point(200, 18);
			this.txtCustomExpression.Name = "txtCustomExpression";
			this.txtCustomExpression.Size = new System.Drawing.Size(317, 20);
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
			this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tree.Location = new System.Drawing.Point(6, 27);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(224, 585);
			this.tree.TabIndex = 0;
			this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
			this.tree.DoubleClick += new System.EventHandler(this.tree_DoubleClick);
			// 
			// gridExample2
			// 
			this.gridExample2.AllowUserToAddRows = false;
			this.gridExample2.AllowUserToDeleteRows = false;
			this.gridExample2.AllowUserToResizeRows = false;
			this.gridExample2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridExample2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridExample2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
			this.gridExample2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridExample2.Enabled = false;
			this.gridExample2.Location = new System.Drawing.Point(6, 172);
			this.gridExample2.MultiSelect = false;
			this.gridExample2.Name = "gridExample2";
			this.gridExample2.RowHeadersVisible = false;
			this.gridExample2.Size = new System.Drawing.Size(333, 86);
			this.gridExample2.TabIndex = 10;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.HeaderText = "#";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 21;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn5.HeaderText = "Property";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.HeaderText = "Operator";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn6.Width = 80;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn7.HeaderText = "Value";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(309, 156);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(30, 13);
			this.label7.TabIndex = 11;
			this.label7.Text = "AND";
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.Location = new System.Drawing.Point(6, 264);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(333, 31);
			this.label8.TabIndex = 12;
			this.label8.Text = "Match characters that are female and have more than 1 epilogue.";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.cmdAdd);
			this.splitContainer1.Panel1.Controls.Add(this.label4);
			this.splitContainer1.Panel1.Controls.Add(this.tree);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1169, 615);
			this.splitContainer1.SplitterDistance = 264;
			this.splitContainer1.TabIndex = 7;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.gridCriteria);
			this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer2.Panel1.Controls.Add(this.grpResults);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.grpExamples);
			this.splitContainer2.Size = new System.Drawing.Size(901, 615);
			this.splitContainer2.SplitterDistance = 552;
			this.splitContainer2.TabIndex = 0;
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.Location = new System.Drawing.Point(6, 404);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(333, 31);
			this.label9.TabIndex = 15;
			this.label9.Text = "Match characters that are either good intelligence or have both 6+ layers and 50+" +
    " targeted lines.";
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(257, 295);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(82, 13);
			this.label10.TabIndex = 14;
			this.label10.Text = "1 OR (2 AND 3)";
			// 
			// gridExample3
			// 
			this.gridExample3.AllowUserToAddRows = false;
			this.gridExample3.AllowUserToDeleteRows = false;
			this.gridExample3.AllowUserToResizeRows = false;
			this.gridExample3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridExample3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridExample3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
			this.gridExample3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridExample3.Enabled = false;
			this.gridExample3.Location = new System.Drawing.Point(6, 309);
			this.gridExample3.MultiSelect = false;
			this.gridExample3.Name = "gridExample3";
			this.gridExample3.RowHeadersVisible = false;
			this.gridExample3.Size = new System.Drawing.Size(333, 92);
			this.gridExample3.TabIndex = 13;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.HeaderText = "#";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.Width = 21;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn9.HeaderText = "Property";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.HeaderText = "Operator";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn10.Width = 80;
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn11.HeaderText = "Value";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// DataAnalyzer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlEdit);
			this.Controls.Add(this.pnlLoad);
			this.Controls.Add(this.pnlStart);
			this.Name = "DataAnalyzer";
			this.Size = new System.Drawing.Size(1169, 615);
			this.pnlStart.ResumeLayout(false);
			this.pnlStart.PerformLayout();
			this.pnlLoad.ResumeLayout(false);
			this.pnlLoad.PerformLayout();
			this.pnlEdit.ResumeLayout(false);
			this.grpExamples.ResumeLayout(false);
			this.grpExamples.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridExample1)).EndInit();
			this.grpResults.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridCriteria)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridExample2)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridExample3)).EndInit();
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
		private System.Windows.Forms.DataGridView gridExample1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.GroupBox grpExamples;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DataGridView gridExample2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.DataGridView gridExample3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
	}
}

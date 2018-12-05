namespace SPNATI_Character_Editor.Activities
{
	partial class AdvancedMetadataEditor
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
			this.valScale = new System.Windows.Forms.NumericUpDown();
			this.lblScale = new System.Windows.Forms.Label();
			this.gridLabels = new System.Windows.Forms.DataGridView();
			this.colLabelsStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colLabelsLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblStageLabels = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.valScale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLabels)).BeginInit();
			this.SuspendLayout();
			// 
			// valScale
			// 
			this.valScale.DecimalPlaces = 1;
			this.valScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.valScale.Location = new System.Drawing.Point(115, 121);
			this.valScale.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
			this.valScale.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.valScale.Name = "valScale";
			this.valScale.Size = new System.Drawing.Size(66, 20);
			this.valScale.TabIndex = 89;
			this.valScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// lblScale
			// 
			this.lblScale.AutoSize = true;
			this.lblScale.Location = new System.Drawing.Point(4, 124);
			this.lblScale.Name = "lblScale";
			this.lblScale.Size = new System.Drawing.Size(84, 13);
			this.lblScale.TabIndex = 88;
			this.lblScale.Text = "Scale factor (%):";
			// 
			// gridLabels
			// 
			this.gridLabels.AllowUserToResizeColumns = false;
			this.gridLabels.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridLabels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.gridLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridLabels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLabelsStage,
            this.colLabelsLabel});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridLabels.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridLabels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.gridLabels.Location = new System.Drawing.Point(115, 0);
			this.gridLabels.Name = "gridLabels";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gridLabels.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.gridLabels.RowHeadersVisible = false;
			this.gridLabels.Size = new System.Drawing.Size(226, 111);
			this.gridLabels.TabIndex = 87;
			// 
			// colLabelsStage
			// 
			this.colLabelsStage.HeaderText = "Stage";
			this.colLabelsStage.MinimumWidth = 50;
			this.colLabelsStage.Name = "colLabelsStage";
			this.colLabelsStage.Width = 50;
			// 
			// colLabelsLabel
			// 
			this.colLabelsLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colLabelsLabel.HeaderText = "Label";
			this.colLabelsLabel.Name = "colLabelsLabel";
			this.colLabelsLabel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// lblStageLabels
			// 
			this.lblStageLabels.AutoSize = true;
			this.lblStageLabels.Location = new System.Drawing.Point(4, 4);
			this.lblStageLabels.Name = "lblStageLabels";
			this.lblStageLabels.Size = new System.Drawing.Size(99, 13);
			this.lblStageLabels.TabIndex = 86;
			this.lblStageLabels.Text = "Vary label by stage:";
			// 
			// AdvancedMetadataEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valScale);
			this.Controls.Add(this.lblScale);
			this.Controls.Add(this.gridLabels);
			this.Controls.Add(this.lblStageLabels);
			this.Name = "AdvancedMetadataEditor";
			this.Size = new System.Drawing.Size(924, 625);
			((System.ComponentModel.ISupportInitialize)(this.valScale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridLabels)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown valScale;
		private System.Windows.Forms.Label lblScale;
		private System.Windows.Forms.DataGridView gridLabels;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLabelsStage;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLabelsLabel;
		private System.Windows.Forms.Label lblStageLabels;
	}
}

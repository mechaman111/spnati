namespace Desktop.CommonControls
{
	partial class PropertyTableRow
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
			this.table = new System.Windows.Forms.TableLayoutPanel();
			this.lblName = new System.Windows.Forms.Label();
			this.cmdPin = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.lblHelp = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.table.SuspendLayout();
			this.SuspendLayout();
			// 
			// table
			// 
			this.table.ColumnCount = 5;
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.table.Controls.Add(this.lblName, 1, 0);
			this.table.Controls.Add(this.cmdPin, 3, 0);
			this.table.Controls.Add(this.cmdRemove, 4, 0);
			this.table.Controls.Add(this.lblHelp, 0, 0);
			this.table.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table.Location = new System.Drawing.Point(0, 0);
			this.table.Name = "table";
			this.table.RowCount = 1;
			this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table.Size = new System.Drawing.Size(626, 28);
			this.table.TabIndex = 1;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(23, 7);
			this.lblName.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmdPin
			// 
			this.cmdPin.FlatAppearance.BorderSize = 0;
			this.cmdPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdPin.Location = new System.Drawing.Point(577, 3);
			this.cmdPin.Name = "cmdPin";
			this.cmdPin.Size = new System.Drawing.Size(20, 22);
			this.cmdPin.TabIndex = 2;
			this.cmdPin.Text = "☆";
			this.toolTip1.SetToolTip(this.cmdPin, "Marks or unmarks the condition as a favorite so it appears by default");
			this.cmdPin.UseVisualStyleBackColor = true;
			this.cmdPin.Click += new System.EventHandler(this.cmdPin_Click);
			// 
			// cmdRemove
			// 
			this.cmdRemove.FlatAppearance.BorderSize = 0;
			this.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdRemove.Font = new System.Drawing.Font("Segoe UI", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdRemove.ForeColor = System.Drawing.Color.DarkRed;
			this.cmdRemove.Location = new System.Drawing.Point(603, 3);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(20, 22);
			this.cmdRemove.TabIndex = 1;
			this.cmdRemove.Text = "❌";
			this.toolTip1.SetToolTip(this.cmdRemove, "Remove condition");
			this.cmdRemove.UseVisualStyleBackColor = true;
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			// 
			// lblHelp
			// 
			this.lblHelp.Image = global::Desktop.Properties.Resources.Help;
			this.lblHelp.Location = new System.Drawing.Point(3, 3);
			this.lblHelp.Margin = new System.Windows.Forms.Padding(3);
			this.lblHelp.Name = "lblHelp";
			this.lblHelp.Size = new System.Drawing.Size(14, 22);
			this.lblHelp.TabIndex = 4;
			// 
			// PropertyTableRow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.table);
			this.Name = "PropertyTableRow";
			this.Size = new System.Drawing.Size(626, 28);
			this.table.ResumeLayout(false);
			this.table.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel table;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button cmdPin;
		private System.Windows.Forms.Label lblHelp;
	}
}

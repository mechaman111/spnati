namespace SPNATI_Character_Editor.Activities
{
	partial class DataRecovery
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
			this.cmdCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblCharacter = new System.Windows.Forms.Label();
			this.recCharacter = new Desktop.CommonControls.RecordField();
			this.pnlRecovery = new System.Windows.Forms.Panel();
			this.cmdRecover = new System.Windows.Forms.Button();
			this.lstSnapshots = new System.Windows.Forms.ListView();
			this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colLines = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colEndings = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPoses = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblCorrupt = new System.Windows.Forms.Label();
			this.pnlRecovery.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point(484, 473);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 0;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Choose a snapshot to revert to:";
			// 
			// lblCharacter
			// 
			this.lblCharacter.AutoSize = true;
			this.lblCharacter.Location = new System.Drawing.Point(12, 15);
			this.lblCharacter.Name = "lblCharacter";
			this.lblCharacter.Size = new System.Drawing.Size(56, 13);
			this.lblCharacter.TabIndex = 2;
			this.lblCharacter.Text = "Character:";
			// 
			// recCharacter
			// 
			this.recCharacter.AllowCreate = false;
			this.recCharacter.Location = new System.Drawing.Point(75, 12);
			this.recCharacter.Name = "recCharacter";
			this.recCharacter.PlaceholderText = null;
			this.recCharacter.Record = null;
			this.recCharacter.RecordContext = null;
			this.recCharacter.RecordKey = null;
			this.recCharacter.RecordType = null;
			this.recCharacter.Size = new System.Drawing.Size(150, 20);
			this.recCharacter.TabIndex = 3;
			this.recCharacter.UseAutoComplete = false;
			this.recCharacter.RecordChanged += new System.EventHandler<Desktop.IRecord>(this.recCharacter_RecordChanged);
			// 
			// pnlRecovery
			// 
			this.pnlRecovery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRecovery.Controls.Add(this.cmdRecover);
			this.pnlRecovery.Controls.Add(this.lstSnapshots);
			this.pnlRecovery.Controls.Add(this.label1);
			this.pnlRecovery.Enabled = false;
			this.pnlRecovery.Location = new System.Drawing.Point(1, 38);
			this.pnlRecovery.Name = "pnlRecovery";
			this.pnlRecovery.Size = new System.Drawing.Size(569, 429);
			this.pnlRecovery.TabIndex = 4;
			// 
			// cmdRecover
			// 
			this.cmdRecover.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRecover.Enabled = false;
			this.cmdRecover.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdRecover.Location = new System.Drawing.Point(14, 342);
			this.cmdRecover.Name = "cmdRecover";
			this.cmdRecover.Size = new System.Drawing.Size(544, 80);
			this.cmdRecover.TabIndex = 2;
			this.cmdRecover.Text = "Revert to Snapshot";
			this.cmdRecover.UseVisualStyleBackColor = true;
			this.cmdRecover.Click += new System.EventHandler(this.cmdRecover_Click);
			// 
			// lstSnapshots
			// 
			this.lstSnapshots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstSnapshots.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Time,
            this.colLines,
            this.colEndings,
            this.colPoses});
			this.lstSnapshots.FullRowSelect = true;
			this.lstSnapshots.Location = new System.Drawing.Point(14, 16);
			this.lstSnapshots.Name = "lstSnapshots";
			this.lstSnapshots.Size = new System.Drawing.Size(544, 320);
			this.lstSnapshots.TabIndex = 0;
			this.lstSnapshots.UseCompatibleStateImageBehavior = false;
			this.lstSnapshots.View = System.Windows.Forms.View.Details;
			this.lstSnapshots.SelectedIndexChanged += new System.EventHandler(this.lstSnapshots_SelectedIndexChanged);
			// 
			// Time
			// 
			this.Time.Text = "Time";
			this.Time.Width = 200;
			// 
			// colLines
			// 
			this.colLines.Text = "Lines";
			this.colLines.Width = 80;
			// 
			// colEndings
			// 
			this.colEndings.Text = "Epilogues";
			this.colEndings.Width = 80;
			// 
			// colPoses
			// 
			this.colPoses.Text = "Poses";
			this.colPoses.Width = 80;
			// 
			// lblCorrupt
			// 
			this.lblCorrupt.AutoSize = true;
			this.lblCorrupt.ForeColor = System.Drawing.Color.Maroon;
			this.lblCorrupt.Location = new System.Drawing.Point(12, 15);
			this.lblCorrupt.Name = "lblCorrupt";
			this.lblCorrupt.Size = new System.Drawing.Size(484, 13);
			this.lblCorrupt.TabIndex = 5;
			this.lblCorrupt.Text = "Something went wrong loading {0}. You can try reverting to an older snapshot of t" +
    "he character\'s data.";
			this.lblCorrupt.Visible = false;
			// 
			// DataRecovery
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 508);
			this.ControlBox = false;
			this.Controls.Add(this.pnlRecovery);
			this.Controls.Add(this.recCharacter);
			this.Controls.Add(this.lblCharacter);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.lblCorrupt);
			this.Name = "DataRecovery";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Data Recovery";
			this.pnlRecovery.ResumeLayout(false);
			this.pnlRecovery.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblCharacter;
		private Desktop.CommonControls.RecordField recCharacter;
		private System.Windows.Forms.Panel pnlRecovery;
		private System.Windows.Forms.ListView lstSnapshots;
		private System.Windows.Forms.Button cmdRecover;
		private System.Windows.Forms.ColumnHeader Time;
		private System.Windows.Forms.ColumnHeader colLines;
		private System.Windows.Forms.ColumnHeader colEndings;
		private System.Windows.Forms.ColumnHeader colPoses;
		private System.Windows.Forms.Label lblCorrupt;
	}
}
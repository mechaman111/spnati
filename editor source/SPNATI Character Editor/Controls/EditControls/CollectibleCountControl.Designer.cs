namespace SPNATI_Character_Editor
{
	partial class CollectibleCountControl
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
			this.recField = new Desktop.CommonControls.RecordField();
			this.label1 = new System.Windows.Forms.Label();
			this.cboOperator = new System.Windows.Forms.ComboBox();
			this.valCounter = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valCounter)).BeginInit();
			this.SuspendLayout();
			// 
			// recField
			// 
			this.recField.AllowCreate = false;
			this.recField.Location = new System.Drawing.Point(63, 1);
			this.recField.Name = "recField";
			this.recField.PlaceholderText = null;
			this.recField.Record = null;
			this.recField.RecordContext = null;
			this.recField.RecordKey = null;
			this.recField.RecordType = null;
			this.recField.Size = new System.Drawing.Size(150, 20);
			this.recField.TabIndex = 0;
			this.recField.UseAutoComplete = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Collectible:";
			// 
			// cboOperator
			// 
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Items.AddRange(new object[] {
            "==",
            "<=",
            "<",
            ">",
            ">=",
            "!="});
			this.cboOperator.Location = new System.Drawing.Point(219, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.Size = new System.Drawing.Size(39, 21);
			this.cboOperator.TabIndex = 2;
			// 
			// valCounter
			// 
			this.valCounter.Location = new System.Drawing.Point(264, 0);
			this.valCounter.Name = "valCounter";
			this.valCounter.Size = new System.Drawing.Size(49, 20);
			this.valCounter.TabIndex = 3;
			// 
			// CollectibleCountControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.valCounter);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recField);
			this.Name = "CollectibleCountControl";
			this.Size = new System.Drawing.Size(433, 21);
			((System.ComponentModel.ISupportInitialize)(this.valCounter)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.CommonControls.RecordField recField;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboOperator;
		private System.Windows.Forms.NumericUpDown valCounter;
	}
}

namespace SPNATI_Character_Editor.Controls
{
	partial class MarkerOptions
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radClear = new System.Windows.Forms.RadioButton();
			this.radDecrement = new System.Windows.Forms.RadioButton();
			this.radIncrement = new System.Windows.Forms.RadioButton();
			this.radSet = new System.Windows.Forms.RadioButton();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.chkPerTarget = new System.Windows.Forms.CheckBox();
			this.chkPersistent = new System.Windows.Forms.CheckBox();
			this.lblMarker = new System.Windows.Forms.Label();
			this.txtMarker = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.radClear);
			this.groupBox1.Controls.Add(this.radDecrement);
			this.groupBox1.Controls.Add(this.radIncrement);
			this.groupBox1.Controls.Add(this.radSet);
			this.groupBox1.Controls.Add(this.txtValue);
			this.groupBox1.Location = new System.Drawing.Point(3, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(190, 67);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Marker Operation";
			// 
			// radClear
			// 
			this.radClear.AutoSize = true;
			this.radClear.Location = new System.Drawing.Point(135, 20);
			this.radClear.Name = "radClear";
			this.radClear.Size = new System.Drawing.Size(49, 17);
			this.radClear.TabIndex = 5;
			this.radClear.TabStop = true;
			this.radClear.Text = "Clear";
			this.radClear.UseVisualStyleBackColor = true;
			this.radClear.CheckedChanged += new System.EventHandler(this.radSet_CheckedChanged);
			// 
			// radDecrement
			// 
			this.radDecrement.AutoSize = true;
			this.radDecrement.Location = new System.Drawing.Point(88, 45);
			this.radDecrement.Name = "radDecrement";
			this.radDecrement.Size = new System.Drawing.Size(77, 17);
			this.radDecrement.TabIndex = 4;
			this.radDecrement.TabStop = true;
			this.radDecrement.Text = "Decrement";
			this.radDecrement.UseVisualStyleBackColor = true;
			this.radDecrement.CheckedChanged += new System.EventHandler(this.radSet_CheckedChanged);
			// 
			// radIncrement
			// 
			this.radIncrement.AutoSize = true;
			this.radIncrement.Location = new System.Drawing.Point(6, 45);
			this.radIncrement.Name = "radIncrement";
			this.radIncrement.Size = new System.Drawing.Size(72, 17);
			this.radIncrement.TabIndex = 3;
			this.radIncrement.TabStop = true;
			this.radIncrement.Text = "Increment";
			this.radIncrement.UseVisualStyleBackColor = true;
			this.radIncrement.CheckedChanged += new System.EventHandler(this.radSet_CheckedChanged);
			// 
			// radSet
			// 
			this.radSet.AutoSize = true;
			this.radSet.Location = new System.Drawing.Point(6, 20);
			this.radSet.Name = "radSet";
			this.radSet.Size = new System.Drawing.Size(44, 17);
			this.radSet.TabIndex = 2;
			this.radSet.TabStop = true;
			this.radSet.Text = "Set:";
			this.radSet.UseVisualStyleBackColor = true;
			this.radSet.CheckedChanged += new System.EventHandler(this.radSet_CheckedChanged);
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtValue.Location = new System.Drawing.Point(56, 19);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(73, 20);
			this.txtValue.TabIndex = 1;
			// 
			// chkPerTarget
			// 
			this.chkPerTarget.AutoSize = true;
			this.chkPerTarget.Location = new System.Drawing.Point(3, 102);
			this.chkPerTarget.Name = "chkPerTarget";
			this.chkPerTarget.Size = new System.Drawing.Size(76, 17);
			this.chkPerTarget.TabIndex = 2;
			this.chkPerTarget.Text = "Per Target";
			this.chkPerTarget.UseVisualStyleBackColor = true;
			// 
			// chkPersistent
			// 
			this.chkPersistent.AutoSize = true;
			this.chkPersistent.Location = new System.Drawing.Point(91, 102);
			this.chkPersistent.Name = "chkPersistent";
			this.chkPersistent.Size = new System.Drawing.Size(72, 17);
			this.chkPersistent.TabIndex = 3;
			this.chkPersistent.Text = "Persistent";
			this.chkPersistent.UseVisualStyleBackColor = true;
			// 
			// lblMarker
			// 
			this.lblMarker.AutoSize = true;
			this.lblMarker.Location = new System.Drawing.Point(3, 6);
			this.lblMarker.Name = "lblMarker";
			this.lblMarker.Size = new System.Drawing.Size(43, 13);
			this.lblMarker.TabIndex = 4;
			this.lblMarker.Text = "Marker:";
			// 
			// txtMarker
			// 
			this.txtMarker.Location = new System.Drawing.Point(52, 3);
			this.txtMarker.Name = "txtMarker";
			this.txtMarker.Size = new System.Drawing.Size(141, 20);
			this.txtMarker.TabIndex = 5;
			// 
			// MarkerOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtMarker);
			this.Controls.Add(this.lblMarker);
			this.Controls.Add(this.chkPersistent);
			this.Controls.Add(this.chkPerTarget);
			this.Controls.Add(this.groupBox1);
			this.Name = "MarkerOptions";
			this.Size = new System.Drawing.Size(196, 122);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radClear;
		private System.Windows.Forms.RadioButton radDecrement;
		private System.Windows.Forms.RadioButton radIncrement;
		private System.Windows.Forms.RadioButton radSet;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.CheckBox chkPerTarget;
		private System.Windows.Forms.CheckBox chkPersistent;
		private System.Windows.Forms.Label lblMarker;
		private System.Windows.Forms.TextBox txtMarker;
	}
}

namespace SPNATI_Character_Editor
{
	partial class GameSimulator
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
			this.cmdGo = new System.Windows.Forms.Button();
			this.cboTrigger = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboTarget = new System.Windows.Forms.ComboBox();
			this.lblReaction = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.char4 = new SPNATI_Character_Editor.Controls.TestCharacter();
			this.char3 = new SPNATI_Character_Editor.Controls.TestCharacter();
			this.char2 = new SPNATI_Character_Editor.Controls.TestCharacter();
			this.char1 = new SPNATI_Character_Editor.Controls.TestCharacter();
			this.label4 = new System.Windows.Forms.Label();
			this.valTotalRounds = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.valTotalRounds)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdGo
			// 
			this.cmdGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdGo.Location = new System.Drawing.Point(779, 689);
			this.cmdGo.Name = "cmdGo";
			this.cmdGo.Size = new System.Drawing.Size(79, 21);
			this.cmdGo.TabIndex = 4;
			this.cmdGo.Text = "Repeat";
			this.cmdGo.UseVisualStyleBackColor = true;
			this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
			// 
			// cboTrigger
			// 
			this.cboTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cboTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTrigger.FormattingEnabled = true;
			this.cboTrigger.Location = new System.Drawing.Point(87, 689);
			this.cboTrigger.Name = "cboTrigger";
			this.cboTrigger.Size = new System.Drawing.Size(201, 21);
			this.cboTrigger.TabIndex = 5;
			this.cboTrigger.SelectedIndexChanged += new System.EventHandler(this.cboTrigger_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 692);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Game Stage:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(306, 692);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Target:";
			// 
			// cboTarget
			// 
			this.cboTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cboTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTarget.FormattingEnabled = true;
			this.cboTarget.Location = new System.Drawing.Point(353, 689);
			this.cboTarget.Name = "cboTarget";
			this.cboTarget.Size = new System.Drawing.Size(132, 21);
			this.cboTarget.TabIndex = 8;
			this.cboTarget.SelectedIndexChanged += new System.EventHandler(this.cboTarget_SelectedIndexChanged);
			// 
			// lblReaction
			// 
			this.lblReaction.AutoSize = true;
			this.lblReaction.Location = new System.Drawing.Point(491, 692);
			this.lblReaction.Name = "lblReaction";
			this.lblReaction.Size = new System.Drawing.Size(0, 13);
			this.lblReaction.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(491, 693);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Filter:";
			// 
			// txtFilter
			// 
			this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtFilter.Location = new System.Drawing.Point(527, 689);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(100, 20);
			this.txtFilter.TabIndex = 11;
			// 
			// char4
			// 
			this.char4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.char4.Location = new System.Drawing.Point(651, 12);
			this.char4.Name = "char4";
			this.char4.Size = new System.Drawing.Size(207, 643);
			this.char4.TabIndex = 3;
			// 
			// char3
			// 
			this.char3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.char3.Location = new System.Drawing.Point(438, 12);
			this.char3.Name = "char3";
			this.char3.Size = new System.Drawing.Size(207, 643);
			this.char3.TabIndex = 2;
			// 
			// char2
			// 
			this.char2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.char2.Location = new System.Drawing.Point(225, 12);
			this.char2.Name = "char2";
			this.char2.Size = new System.Drawing.Size(207, 643);
			this.char2.TabIndex = 1;
			// 
			// char1
			// 
			this.char1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.char1.Location = new System.Drawing.Point(12, 12);
			this.char1.Name = "char1";
			this.char1.Size = new System.Drawing.Size(207, 643);
			this.char1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(640, 693);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Round:";
			// 
			// valTotalRounds
			// 
			this.valTotalRounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.valTotalRounds.Location = new System.Drawing.Point(688, 689);
			this.valTotalRounds.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
			this.valTotalRounds.Name = "valTotalRounds";
			this.valTotalRounds.Size = new System.Drawing.Size(67, 20);
			this.valTotalRounds.TabIndex = 13;
			this.valTotalRounds.ValueChanged += new System.EventHandler(this.valTotalRounds_ValueChanged);
			// 
			// GameSimulator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(870, 722);
			this.Controls.Add(this.valTotalRounds);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtFilter);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblReaction);
			this.Controls.Add(this.cboTarget);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboTrigger);
			this.Controls.Add(this.cmdGo);
			this.Controls.Add(this.char4);
			this.Controls.Add(this.char3);
			this.Controls.Add(this.char2);
			this.Controls.Add(this.char1);
			this.Name = "GameSimulator";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Simulator - SEE HELP FOR LIMITATIONS";
			((System.ComponentModel.ISupportInitialize)(this.valTotalRounds)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.TestCharacter char1;
		private Controls.TestCharacter char2;
		private Controls.TestCharacter char3;
		private Controls.TestCharacter char4;
		private System.Windows.Forms.Button cmdGo;
		private System.Windows.Forms.ComboBox cboTrigger;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboTarget;
		private System.Windows.Forms.Label lblReaction;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtFilter;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown valTotalRounds;
	}
}
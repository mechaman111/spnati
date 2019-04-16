namespace SPNATI_Character_Editor.Activities
{
	partial class SpellCheck
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
			this.txtLine = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdIgnore = new System.Windows.Forms.Button();
			this.cmdIgnoreAll = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtWord = new System.Windows.Forms.TextBox();
			this.cmdChange = new System.Windows.Forms.Button();
			this.cmdChangeAll = new System.Windows.Forms.Button();
			this.lstSuggestions = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lblGood = new System.Windows.Forms.Label();
			this.panelFix = new System.Windows.Forms.Panel();
			this.lblProgress = new System.Windows.Forms.Label();
			this.panelFix.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLine
			// 
			this.txtLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLine.Location = new System.Drawing.Point(6, 162);
			this.txtLine.Name = "txtLine";
			this.txtLine.ReadOnly = true;
			this.txtLine.Size = new System.Drawing.Size(369, 62);
			this.txtLine.TabIndex = 0;
			this.txtLine.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 146);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Context:";
			// 
			// cmdIgnore
			// 
			this.cmdIgnore.Location = new System.Drawing.Point(378, 3);
			this.cmdIgnore.Name = "cmdIgnore";
			this.cmdIgnore.Size = new System.Drawing.Size(75, 23);
			this.cmdIgnore.TabIndex = 2;
			this.cmdIgnore.Text = "&Ignore Once";
			this.cmdIgnore.UseVisualStyleBackColor = true;
			this.cmdIgnore.Click += new System.EventHandler(this.cmdIgnore_Click);
			// 
			// cmdIgnoreAll
			// 
			this.cmdIgnoreAll.Location = new System.Drawing.Point(378, 32);
			this.cmdIgnoreAll.Name = "cmdIgnoreAll";
			this.cmdIgnoreAll.Size = new System.Drawing.Size(75, 23);
			this.cmdIgnoreAll.TabIndex = 3;
			this.cmdIgnoreAll.Text = "I&gnore All";
			this.cmdIgnoreAll.UseVisualStyleBackColor = true;
			this.cmdIgnoreAll.Click += new System.EventHandler(this.cmdIgnoreAll_Click);
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(378, 61);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdAdd.TabIndex = 4;
			this.cmdAdd.Text = "&Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Not in Dictionary:";
			// 
			// txtWord
			// 
			this.txtWord.Location = new System.Drawing.Point(6, 16);
			this.txtWord.Name = "txtWord";
			this.txtWord.Size = new System.Drawing.Size(366, 20);
			this.txtWord.TabIndex = 0;
			this.txtWord.Enter += new System.EventHandler(this.txtWord_Enter);
			// 
			// cmdChange
			// 
			this.cmdChange.Location = new System.Drawing.Point(378, 90);
			this.cmdChange.Name = "cmdChange";
			this.cmdChange.Size = new System.Drawing.Size(75, 23);
			this.cmdChange.TabIndex = 5;
			this.cmdChange.Text = "&Change";
			this.cmdChange.UseVisualStyleBackColor = true;
			this.cmdChange.Click += new System.EventHandler(this.cmdChange_Click);
			// 
			// cmdChangeAll
			// 
			this.cmdChangeAll.Location = new System.Drawing.Point(378, 119);
			this.cmdChangeAll.Name = "cmdChangeAll";
			this.cmdChangeAll.Size = new System.Drawing.Size(75, 23);
			this.cmdChangeAll.TabIndex = 6;
			this.cmdChangeAll.Text = "Change A&ll";
			this.cmdChangeAll.UseVisualStyleBackColor = true;
			this.cmdChangeAll.Click += new System.EventHandler(this.cmdChangeAll_Click);
			// 
			// lstSuggestions
			// 
			this.lstSuggestions.FormattingEnabled = true;
			this.lstSuggestions.Location = new System.Drawing.Point(6, 61);
			this.lstSuggestions.Name = "lstSuggestions";
			this.lstSuggestions.Size = new System.Drawing.Size(366, 82);
			this.lstSuggestions.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Suggestions:";
			// 
			// lblGood
			// 
			this.lblGood.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblGood.AutoSize = true;
			this.lblGood.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGood.ForeColor = System.Drawing.Color.Green;
			this.lblGood.Location = new System.Drawing.Point(47, 296);
			this.lblGood.Name = "lblGood";
			this.lblGood.Size = new System.Drawing.Size(625, 65);
			this.lblGood.TabIndex = 11;
			this.lblGood.Text = "No Misspelled Words Found";
			this.lblGood.Visible = false;
			// 
			// panelFix
			// 
			this.panelFix.Controls.Add(this.lblProgress);
			this.panelFix.Controls.Add(this.label2);
			this.panelFix.Controls.Add(this.txtLine);
			this.panelFix.Controls.Add(this.label3);
			this.panelFix.Controls.Add(this.label1);
			this.panelFix.Controls.Add(this.lstSuggestions);
			this.panelFix.Controls.Add(this.cmdIgnore);
			this.panelFix.Controls.Add(this.cmdChangeAll);
			this.panelFix.Controls.Add(this.cmdIgnoreAll);
			this.panelFix.Controls.Add(this.cmdChange);
			this.panelFix.Controls.Add(this.cmdAdd);
			this.panelFix.Controls.Add(this.txtWord);
			this.panelFix.Enabled = false;
			this.panelFix.Location = new System.Drawing.Point(3, 3);
			this.panelFix.Name = "panelFix";
			this.panelFix.Size = new System.Drawing.Size(603, 290);
			this.panelFix.TabIndex = 12;
			// 
			// lblProgress
			// 
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(6, 231);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(34, 13);
			this.lblProgress.TabIndex = 11;
			this.lblProgress.Text = "0 of 0";
			// 
			// SpellCheck
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelFix);
			this.Controls.Add(this.lblGood);
			this.Name = "SpellCheck";
			this.Size = new System.Drawing.Size(723, 560);
			this.panelFix.ResumeLayout(false);
			this.panelFix.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtLine;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdIgnore;
		private System.Windows.Forms.Button cmdIgnoreAll;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtWord;
		private System.Windows.Forms.Button cmdChange;
		private System.Windows.Forms.Button cmdChangeAll;
		private System.Windows.Forms.ListBox lstSuggestions;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblGood;
		private System.Windows.Forms.Panel panelFix;
		private System.Windows.Forms.Label lblProgress;
	}
}

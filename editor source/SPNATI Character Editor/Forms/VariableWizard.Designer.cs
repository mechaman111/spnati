namespace SPNATI_Character_Editor.Forms
{
	partial class VariableWizard
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
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.txtLine = new System.Windows.Forms.TextBox();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.cmdOK = new Desktop.Skinning.SkinnedButton();
			this.skinnedLabel1 = new Desktop.Skinning.SkinnedLabel();
			this.recVariable = new Desktop.CommonControls.RecordField();
			this.lblFunction = new Desktop.Skinning.SkinnedLabel();
			this.recFunction = new Desktop.CommonControls.RecordField();
			this.panelParameters = new Desktop.Skinning.SkinnedPanel();
			this.panelExamples = new Desktop.Skinning.SkinnedPanel();
			this.skinnedLabel2 = new Desktop.Skinning.SkinnedLabel();
			this.lblParams = new Desktop.Skinning.SkinnedLabel();
			this.skinnedPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.txtLine);
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOK);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 420);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(631, 30);
			this.skinnedPanel1.TabIndex = 10;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// txtLine
			// 
			this.txtLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLine.Location = new System.Drawing.Point(3, 6);
			this.txtLine.Name = "txtLine";
			this.txtLine.Size = new System.Drawing.Size(463, 20);
			this.txtLine.TabIndex = 30;
			this.txtLine.TextChanged += new System.EventHandler(this.txtLine_TextChanged);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(553, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 32;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOK.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOK.Flat = false;
			this.cmdOK.ForeColor = System.Drawing.Color.White;
			this.cmdOK.Location = new System.Drawing.Point(472, 4);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 31;
			this.cmdOK.Text = "Insert";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// skinnedLabel1
			// 
			this.skinnedLabel1.AutoSize = true;
			this.skinnedLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.skinnedLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.skinnedLabel1.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.skinnedLabel1.Location = new System.Drawing.Point(12, 38);
			this.skinnedLabel1.Name = "skinnedLabel1";
			this.skinnedLabel1.Size = new System.Drawing.Size(48, 13);
			this.skinnedLabel1.TabIndex = 1;
			this.skinnedLabel1.Text = "Variable:";
			// 
			// recVariable
			// 
			this.recVariable.AllowCreate = false;
			this.recVariable.Location = new System.Drawing.Point(66, 35);
			this.recVariable.Name = "recVariable";
			this.recVariable.PlaceholderText = null;
			this.recVariable.Record = null;
			this.recVariable.RecordContext = null;
			this.recVariable.RecordFilter = null;
			this.recVariable.RecordKey = null;
			this.recVariable.RecordType = null;
			this.recVariable.Size = new System.Drawing.Size(150, 20);
			this.recVariable.TabIndex = 2;
			this.recVariable.UseAutoComplete = false;
			this.recVariable.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recVariable_RecordChanged);
			// 
			// lblFunction
			// 
			this.lblFunction.AutoSize = true;
			this.lblFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblFunction.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFunction.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblFunction.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.lblFunction.Location = new System.Drawing.Point(12, 64);
			this.lblFunction.Name = "lblFunction";
			this.lblFunction.Size = new System.Drawing.Size(51, 13);
			this.lblFunction.TabIndex = 3;
			this.lblFunction.Text = "Function:";
			this.lblFunction.Visible = false;
			// 
			// recFunction
			// 
			this.recFunction.AllowCreate = false;
			this.recFunction.Location = new System.Drawing.Point(66, 61);
			this.recFunction.Name = "recFunction";
			this.recFunction.PlaceholderText = null;
			this.recFunction.Record = null;
			this.recFunction.RecordContext = null;
			this.recFunction.RecordFilter = null;
			this.recFunction.RecordKey = null;
			this.recFunction.RecordType = null;
			this.recFunction.Size = new System.Drawing.Size(150, 20);
			this.recFunction.TabIndex = 4;
			this.recFunction.UseAutoComplete = false;
			this.recFunction.Visible = false;
			this.recFunction.RecordChanged += new System.EventHandler<Desktop.CommonControls.RecordEventArgs>(this.recFunction_RecordChanged);
			// 
			// panelParameters
			// 
			this.panelParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelParameters.Location = new System.Drawing.Point(12, 109);
			this.panelParameters.Name = "panelParameters";
			this.panelParameters.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.panelParameters.Size = new System.Drawing.Size(607, 205);
			this.panelParameters.TabIndex = 6;
			this.panelParameters.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// panelExamples
			// 
			this.panelExamples.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelExamples.Location = new System.Drawing.Point(12, 342);
			this.panelExamples.Name = "panelExamples";
			this.panelExamples.PanelType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.panelExamples.Size = new System.Drawing.Size(607, 69);
			this.panelExamples.TabIndex = 8;
			this.panelExamples.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// skinnedLabel2
			// 
			this.skinnedLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.skinnedLabel2.AutoSize = true;
			this.skinnedLabel2.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.skinnedLabel2.ForeColor = System.Drawing.Color.Blue;
			this.skinnedLabel2.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.skinnedLabel2.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.skinnedLabel2.Location = new System.Drawing.Point(11, 317);
			this.skinnedLabel2.Name = "skinnedLabel2";
			this.skinnedLabel2.Size = new System.Drawing.Size(78, 21);
			this.skinnedLabel2.TabIndex = 7;
			this.skinnedLabel2.Text = "Examples:";
			// 
			// lblParams
			// 
			this.lblParams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblParams.AutoSize = true;
			this.lblParams.Font = new System.Drawing.Font("Segoe UI", 12F);
			this.lblParams.ForeColor = System.Drawing.Color.Blue;
			this.lblParams.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.lblParams.Level = Desktop.Skinning.SkinnedLabelLevel.Heading;
			this.lblParams.Location = new System.Drawing.Point(12, 85);
			this.lblParams.Name = "lblParams";
			this.lblParams.Size = new System.Drawing.Size(91, 21);
			this.lblParams.TabIndex = 5;
			this.lblParams.Text = "Parameters:";
			// 
			// VariableWizard
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(631, 450);
			this.Controls.Add(this.lblParams);
			this.Controls.Add(this.skinnedLabel2);
			this.Controls.Add(this.panelExamples);
			this.Controls.Add(this.panelParameters);
			this.Controls.Add(this.recFunction);
			this.Controls.Add(this.lblFunction);
			this.Controls.Add(this.recVariable);
			this.Controls.Add(this.skinnedLabel1);
			this.Controls.Add(this.skinnedPanel1);
			this.Name = "VariableWizard";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Variable Wizard";
			this.skinnedPanel1.ResumeLayout(false);
			this.skinnedPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedButton cmdOK;
		private Desktop.Skinning.SkinnedLabel skinnedLabel1;
		private Desktop.CommonControls.RecordField recVariable;
		private Desktop.Skinning.SkinnedLabel lblFunction;
		private Desktop.CommonControls.RecordField recFunction;
		private Desktop.Skinning.SkinnedPanel panelParameters;
		private Desktop.Skinning.SkinnedPanel panelExamples;
		private System.Windows.Forms.TextBox txtLine;
		private Desktop.Skinning.SkinnedLabel skinnedLabel2;
		private Desktop.Skinning.SkinnedLabel lblParams;
	}
}
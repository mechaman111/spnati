using SPNATI_Character_Editor.Controls;

namespace SPNATI_Character_Editor.Activities
{
	partial class DialogueEditor
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
			this.triggerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmdMakeResponse = new System.Windows.Forms.Button();
			this.cmdCallOut = new System.Windows.Forms.Button();
			this.splitDialogue = new System.Windows.Forms.SplitContainer();
			this.treeDialogue = new SPNATI_Character_Editor.Controls.DialogueTree();
			this.panelCase = new System.Windows.Forms.Panel();
			this.caseControl = new SPNATI_Character_Editor.Controls.CaseControl();
			((System.ComponentModel.ISupportInitialize)(this.splitDialogue)).BeginInit();
			this.splitDialogue.Panel1.SuspendLayout();
			this.splitDialogue.Panel2.SuspendLayout();
			this.splitDialogue.SuspendLayout();
			this.panelCase.SuspendLayout();
			this.SuspendLayout();
			// 
			// triggerMenu
			// 
			this.triggerMenu.Name = "triggerMenu";
			this.triggerMenu.ShowImageMargin = false;
			this.triggerMenu.Size = new System.Drawing.Size(36, 4);
			// 
			// cmdMakeResponse
			// 
			this.cmdMakeResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdMakeResponse.Location = new System.Drawing.Point(508, 3);
			this.cmdMakeResponse.Name = "cmdMakeResponse";
			this.cmdMakeResponse.Size = new System.Drawing.Size(104, 23);
			this.cmdMakeResponse.TabIndex = 45;
			this.cmdMakeResponse.Text = "Respond to This...";
			this.toolTip1.SetToolTip(this.cmdMakeResponse, "Creates a response to this case on another character");
			this.cmdMakeResponse.UseVisualStyleBackColor = true;
			this.cmdMakeResponse.Click += new System.EventHandler(this.cmdMakeResponse_Click);
			// 
			// cmdCallOut
			// 
			this.cmdCallOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCallOut.Location = new System.Drawing.Point(618, 3);
			this.cmdCallOut.Name = "cmdCallOut";
			this.cmdCallOut.Size = new System.Drawing.Size(75, 23);
			this.cmdCallOut.TabIndex = 44;
			this.cmdCallOut.Text = "Call Out...";
			this.toolTip1.SetToolTip(this.cmdCallOut, "Marks this situation as being \"noteworthy\" so it will appear in other character\'s" +
        " Writing Aids.");
			this.cmdCallOut.UseVisualStyleBackColor = true;
			this.cmdCallOut.Click += new System.EventHandler(this.cmdCallOut_Click);
			// 
			// splitDialogue
			// 
			this.splitDialogue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitDialogue.Location = new System.Drawing.Point(0, 0);
			this.splitDialogue.Name = "splitDialogue";
			// 
			// splitDialogue.Panel1
			// 
			this.splitDialogue.Panel1.Controls.Add(this.treeDialogue);
			// 
			// splitDialogue.Panel2
			// 
			this.splitDialogue.Panel2.Controls.Add(this.panelCase);
			this.splitDialogue.Size = new System.Drawing.Size(973, 671);
			this.splitDialogue.SplitterDistance = 266;
			this.splitDialogue.TabIndex = 16;
			// 
			// treeDialogue
			// 
			this.treeDialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeDialogue.Location = new System.Drawing.Point(3, 3);
			this.treeDialogue.Name = "treeDialogue";
			this.treeDialogue.Size = new System.Drawing.Size(259, 665);
			this.treeDialogue.TabIndex = 40;
			this.treeDialogue.SelectedNodeChanging += new System.EventHandler<SPNATI_Character_Editor.Controls.CaseSelectionEventArgs>(this.tree_SelectedNodeChanging);
			this.treeDialogue.SelectedNodeChanged += new System.EventHandler<SPNATI_Character_Editor.Controls.CaseSelectionEventArgs>(this.tree_SelectedCaseChanged);
			this.treeDialogue.CreatingCase += new System.EventHandler<SPNATI_Character_Editor.Controls.CaseCreationEventArgs>(this.tree_CreatingCase);
			// 
			// panelCase
			// 
			this.panelCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelCase.BackColor = System.Drawing.SystemColors.Control;
			this.panelCase.Controls.Add(this.cmdMakeResponse);
			this.panelCase.Controls.Add(this.cmdCallOut);
			this.panelCase.Controls.Add(this.caseControl);
			this.panelCase.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelCase.Location = new System.Drawing.Point(3, 0);
			this.panelCase.Name = "panelCase";
			this.panelCase.Size = new System.Drawing.Size(697, 668);
			this.panelCase.TabIndex = 28;
			// 
			// caseControl
			// 
			this.caseControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.caseControl.Location = new System.Drawing.Point(0, 0);
			this.caseControl.Name = "caseControl";
			this.caseControl.Size = new System.Drawing.Size(697, 668);
			this.caseControl.TabIndex = 46;
			// 
			// DialogueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitDialogue);
			this.Name = "DialogueEditor";
			this.Size = new System.Drawing.Size(973, 671);
			this.splitDialogue.Panel1.ResumeLayout(false);
			this.splitDialogue.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitDialogue)).EndInit();
			this.splitDialogue.ResumeLayout(false);
			this.panelCase.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitDialogue;
		private System.Windows.Forms.Panel panelCase;
		private System.Windows.Forms.ContextMenuStrip triggerMenu;
		private System.Windows.Forms.Button cmdCallOut;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button cmdMakeResponse;
		private Controls.DialogueTree treeDialogue;
		private CaseControl caseControl;
	}
}

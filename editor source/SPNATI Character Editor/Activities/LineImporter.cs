using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 315)]
	public partial class LineImporter : Activity
	{
		private int _collapsedHeight;
		private Character _character;

		public LineImporter()
		{
			InitializeComponent();
			_collapsedHeight = txtCode.Height;
		}

		public override string Caption
		{
			get { return "Line Importer"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			ExpandCode(true);
		}

		public override void Save()
		{
			caseEditor.Save();
		}

		private void ExpandCode(bool expanded)
		{
			if (expanded)
			{
				txtCode.Height = Height - (Padding.Bottom + txtCode.Top);
				grpImports.Visible = false;
			}
			else
			{
				txtCode.Height = _collapsedHeight;
				grpImports.Visible = true;
				grpImports.Top = txtCode.Bottom + txtCode.Margin.Bottom;
				grpImports.Height = Height - (Padding.Bottom + grpImports.Top);
			}
		}

		private void Import()
		{
			string code = txtCode.Text;
			ExpandCode(false);
			if (_character == null || string.IsNullOrEmpty(code)) { return; }
			lstCases.Items.Clear();

			List<ImportEdit> edits = new List<ImportEdit>();
			string json = code;
			try
			{
				edits = Json.Deserialize<List<ImportEdit>>(json);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Failed to Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			List<Case> imports = new List<Case>();
			List<string> errors = new List<string>();
			for (int i = 0; i < edits.Count; i++)
			{
				ImportEdit edit = edits[i];
				try
				{
					Case importedCase = edit.CreateCase(_character);
					if (importedCase != null)
					{
						lstCases.Items.Add(importedCase);
						imports.Add(importedCase);
					}
				}
				catch (ImportLinesException ex)
				{
					errors.Add($"Error importing edit {i + 1}: {ex.Message}.");
				}
			}
			Shell.Instance.SetStatus($"Imported {imports.Count} case(s) with {errors.Count} error(s).");
			if (errors.Count > 0)
			{
				MessageBox.Show(string.Join("\r\n", errors), "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			if (lstCases.Items.Count > 0)
			{
				lstCases.SelectedIndex = 0;
			}
		}

		private void lstCases_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Case selectedCase = lstCases.SelectedItem as Case;
			caseEditor.SetCharacter(_character);
			if (selectedCase == null)
			{
				caseEditor.SetCase(null, null);
				caseEditor.Enabled = false;
				caseEditor.Visible = false;
				return;
			}
			else
			{
				caseEditor.Enabled = true;
				caseEditor.Visible = true;
				caseEditor.SetCase(new Stage(selectedCase.Stages[0]), selectedCase);
			}
		}

		private void caseEditor_HighlightRow(object sender, int index)
		{
			if (_character == null) { return; }
			string image = caseEditor.GetImage(index);
			ImageLibrary imageLibrary = ImageLibrary.Get(_character);
			CharacterImage img = null;
			img = imageLibrary.Find(image);
			if (img == null)
			{
				Case workingCase = caseEditor.GetCase();
				int stage = workingCase.Stages[0];
				image = DialogueLine.GetStageImage(stage, DialogueLine.GetDefaultImage(image));
				img = imageLibrary.Find(image);
			}
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, img);
		}

		private void cmdImport_Click(object sender, System.EventArgs e)
		{
			Import();
		}
	}
}

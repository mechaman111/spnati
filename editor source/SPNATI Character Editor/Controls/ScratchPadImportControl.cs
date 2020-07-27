using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class ScratchPadImportControl : UserControl
	{
		private Character _character;
		private List<string> _lines;
		private Case _workingCase;

		public event EventHandler Cancel;
		public event EventHandler<List<string>> ImportedLines;

		public ScratchPadImportControl()
		{
			InitializeComponent();
			split.Panel2Collapsed = true;
		}

		public void SetCharacter(Character character)
		{
			_character = character;
			caseCtl.SetCharacter(character);
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Cancel?.Invoke(this, EventArgs.Empty);
		}

		public void SetLines(List<string> lines)
		{
			lstLines.Items.Clear();
			foreach (string line in lines)
			{
				lstLines.Items.Add(line);
			}
			_lines = lines;
		}

		private void cmdImport_Click(object sender, EventArgs e)
		{
			if (lstLines.Items.Count == 0)
			{
				return;
			}

			_workingCase = new Case("hand");
			foreach (string line in lstLines.CheckedItems)
			{
				_workingCase.Lines.Add(new DialogueLine(null, line));
			}

			TriggerDefinition trigger = TriggerDatabase.GetTrigger(_workingCase.Tag);
			for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
			{
				int standardStage = TriggerDatabase.ToStandardStage(_character, i);
				if (standardStage >= trigger.StartStage && standardStage <= trigger.EndStage)
				{
					_workingCase.Stages.Add(i);
				}
			}
			caseCtl.SetCase(new Stage(_workingCase.Stages[0]), _workingCase);

			split.Panel1Collapsed = true;
		}

		private void cmdFinishImport_Click(object sender, EventArgs e)
		{
			caseCtl.Save();
			_character.Behavior.AddWorkingCase(_workingCase);

			//remove the checked lines
			for (int i = lstLines.Items.Count - 1; i >= 0; i--)
			{
				if (lstLines.GetItemChecked(i))
				{
					lstLines.Items.RemoveAt(i);
					_lines.RemoveAt(i);
				}
			}
			ImportedLines?.Invoke(this, _lines);

			split.Panel2Collapsed = true;

			if (_lines.Count == 0)
			{
				//nothing more to do
				Cancel?.Invoke(this, EventArgs.Empty);
			}
		}

		private void cmdCancelImport_Click(object sender, EventArgs e)
		{
			split.Panel2Collapsed = true;
		}

		private void caseCtl_HighlightRow(object sender, int index)
		{
			if (_character == null) { return; }
			PoseMapping image = caseCtl.GetImage(index);
			if (image != null)
			{
				Case workingCase = caseCtl.GetCase();
				int stage = workingCase.Stages[0];
				Shell.Instance.ActiveWorkspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, stage));
			}
		}
	}
}

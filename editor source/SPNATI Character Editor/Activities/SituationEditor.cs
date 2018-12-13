using Desktop;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 305)]
	public partial class SituationEditor : Activity
	{
		private Character _character;
		private CharacterEditorData _editorData;
		private ImageLibrary _imageLibrary;
		private Situation _selectedCase;
		
		public SituationEditor()
		{
			InitializeComponent();

			gridLines.ReadOnly = true;
		}

		public override string Caption
		{
			get { return "Situations"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			_editorData = CharacterDatabase.GetEditorData(_character);
		}

		protected override void OnFirstActivate()
		{
			_imageLibrary = ImageLibrary.Get(_character);
			PopulateLines();

			if (gridCases.RowCount == 0)
			{
				MessageBox.Show("Looks like you haven't declared any noteworthy situations yet! To get started, use the Call Out button when editing a line on the Dialogue screen.", "Situation Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Shell.Instance.Launch<Character, DialogueEditor>(_character);
			}
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length > 0)
			{
				Situation line = parameters[0] as Situation;

				//See if the line was already added (will be the case if launching this from the Call Out button)
				DataGridViewRow row = null;
				foreach (DataGridViewRow r in gridCases.Rows)
				{
					Situation s = r.Tag as Situation;
					if (s == line)
					{
						row = r;
						break;
					}
				}
				if (row == null)
				{
					row = BuildLine(line);
				}
				row.Selected = true;
				gridCases.ClearSelection();
				row.Cells[0].Selected = true;
			}
		}

		private void PopulateLines()
		{
			foreach (Situation line in _editorData.NoteworthySituations)
			{
				BuildLine(line);
			}
		}

		private DataGridViewRow BuildLine(Situation line)
		{
			DataGridViewRow row = gridCases.Rows[gridCases.Rows.Add(line.Name, line.Description, line.GetStageString(), line.Case.ToString())];
			row.Tag = line;
			return row;
		}

		private void gridCases_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			DataGridViewRow row = e.Row;

			Situation line = row.Tag as Situation;
			if (line != null)
			{
				_editorData.NoteworthySituations.Remove(line);
			}
		}

		public override void Save()
		{
			foreach (DataGridViewRow row in gridCases.Rows)
			{
				Situation line = row.Tag as Situation;
				if (line != null)
				{
					line.Name = row.Cells["ColName"].Value?.ToString();
					line.Description = row.Cells["ColDescription"].Value?.ToString();
				}
			}
		}

		private void gridCases_SelectionChanged(object sender, System.EventArgs e)
		{
			if (gridCases.SelectedCells.Count > 0)
			{
				DataGridViewRow row = gridCases.SelectedCells[0].OwningRow;
				Situation line = row.Tag as Situation;
				_selectedCase = line;
				if (line == null) { return; }
				HashSet<int> selectedStages = new HashSet<int>();
				selectedStages.Add(line.MinStage);
				gridLines.SetData(_character, _character.Behavior.Stages[line.MinStage], line.Case, selectedStages, _imageLibrary);
			}
		}

		private void gridLines_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			string image = gridLines.GetImage(index);
			CharacterImage img = null;
			img = _imageLibrary.Find(image);
			if (img == null)
			{
				int stage = _selectedCase.MinStage;
				image = DialogueLine.GetDefaultImage(image);
				img = _imageLibrary.Find(stage + "-" + image);
			}
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, img);
		}
	}
}

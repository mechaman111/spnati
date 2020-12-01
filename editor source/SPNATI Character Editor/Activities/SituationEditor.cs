using Desktop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 305, DelayRun = true, Caption = "Situations")]
	public partial class SituationEditor : Activity
	{
		private Character _character;
		private CharacterEditorData _editorData;
		private Situation _selectedCase;

		private static Dictionary<SituationPriority, string> _priorities;
		private static Dictionary<string, SituationPriority> _prioritiesIndex;

		static SituationEditor()
		{
			_priorities = new Dictionary<SituationPriority, string>();
			_prioritiesIndex = new Dictionary<string, SituationPriority>();
			_priorities.Add(SituationPriority.MustTarget, "Must Target");
			_priorities.Add(SituationPriority.Noteworthy, "Noteworthy");
			_priorities.Add(SituationPriority.FYI, "FYI");
			_priorities.Add(SituationPriority.Prompt, "Prompt");
			foreach (KeyValuePair<SituationPriority, string> kvp in _priorities)
			{
				_prioritiesIndex[kvp.Value] = kvp.Key;
			}
		}

		public SituationEditor()
		{
			InitializeComponent();
			ColJump.Flat = ColDelete.Flat = true;
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

			foreach (SituationPriority priority in Enum.GetValues(typeof(SituationPriority)))
			{
				if (_priorities.ContainsKey(priority))
				{
					ColPriority.Items.Add(_priorities[priority]);
				}
			}
		}

		protected override void OnFirstActivate()
		{
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
				if (parameters[0] is bool)
				{
					_editorData.ReviewedPriorities = true;
				}
				else
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
			DataGridViewRow row = gridCases.Rows[gridCases.Rows.Add(line.Name, line.Description, "", line.GetStageString(), line.LinkedCase?.ToString())];
			SituationPriority priority = line.Priority;
			if (priority == SituationPriority.None)
			{
				priority = SituationPriority.Noteworthy;
			}
			string value;
			if (_priorities.TryGetValue(priority, out value))
			{
				row.Cells[nameof(ColPriority)].Value = value;
			}
			DataGridViewCell jumpButton = row.Cells["ColJump"];
			if (line.Id == 0)
			{
				jumpButton.ToolTipText = "";// "Link Case";
			}
			else
			{
				jumpButton.ToolTipText = "Go to Case";
			}
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
					string priority = row.Cells[ColPriority.Index].Value?.ToString();
					SituationPriority p;
					if (_prioritiesIndex.TryGetValue(priority, out p))
					{
						line.Priority = p;
					}
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
				Stage stage = new Stage(line.MinStage);
				gridLines.SetData(_character, stage, line.LinkedCase, selectedStages);
			}
		}

		private void gridLines_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			PoseMapping image = gridLines.GetImage(index);
			if (image != null)
			{
				int stage = _selectedCase.MinStage;
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, stage));
			}
		}

		private void gridCases_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == ColJump.Index && e.RowIndex >= 0)
			{
				Situation s = gridCases.Rows[e.RowIndex]?.Tag as Situation;
				if (s == null || s.Id == 0)
				{
					return;
				}

				Image img = s.Id > 0 ? Properties.Resources.GoToLine : Properties.Resources.Link;
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				var w = img.Width;
				var h = img.Height;
				var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
				var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

				e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
				e.Handled = true;
			}
			else if (e.ColumnIndex == ColDelete.Index)
			{
				Image img = Properties.Resources.Delete;
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				var w = img.Width;
				var h = img.Height;
				var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
				var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

				e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
				e.Handled = true;
			}
		}

		private void gridCases_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex < 0 || e.ColumnIndex >= gridCases.Columns.Count || e.RowIndex == gridCases.NewRowIndex || e.RowIndex < 0)
			{
				return;
			}
			if (e.ColumnIndex == ColJump.Index)
			{
				Situation situation = gridCases.Rows[e.RowIndex]?.Tag as Situation;
				if (situation != null)
				{
					if (situation.Id == 0)
					{
						//Forms.SituationLinker linker = new Forms.SituationLinker();
						//linker.SetData(_character, situation);
						//linker.ShowDialog();
					}
					else if (situation.LinkedCase != null)
					{
						Shell.Instance.Launch<Character, DialogueEditor>(_character, new ValidationContext(new Stage(situation.LinkedCase.Stages[0]), situation.LinkedCase, null));
					}
				}
			}
			else if (e.ColumnIndex == ColDelete.Index)
			{
				DataGridViewRow row = gridCases.Rows[e.RowIndex];
				Situation line = row.Tag as Situation;
				if (line != null)
				{
					_editorData.NoteworthySituations.Remove(line);
				}
				gridCases.Rows.RemoveAt(e.RowIndex);
			}
		}
	}
}

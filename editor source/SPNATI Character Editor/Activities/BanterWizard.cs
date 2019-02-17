using Desktop;
using Desktop.Messaging;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Spacer]
	[Activity(typeof(Character), 300)]
	public partial class BanterWizard : Activity
	{
		private Character _character;
		private TargetData _selectedData;
		private int _currentLine;
		private Case _workingResponse;
		public bool Modified { get; private set; }
		private Dictionary<Character, List<TargetData>> _lines = new Dictionary<Character, List<TargetData>>();
		private Dictionary<Character, List<TargetData>> _filterLines = new Dictionary<Character, List<TargetData>>();
		private ImageLibrary _imageLibrary;

		public BanterWizard()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get
			{
				return "Banter Wizard";
			}
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			_imageLibrary = ImageLibrary.Get(_character);
		}

		protected override void OnFirstActivate()
		{
			HideResponses();
			lblCharacters.Text = string.Format(lblCharacters.Text, _character);
			lstCharacters.Sorted = true;

			//Scan other characters to see who talks to this character
			foreach (Character other in CharacterDatabase.Characters)
			{
				if (other.FolderName == "human" || other == _character) { continue; }
				lstCharacters.Items.Add(other);
			}
		}

		private void lstCharacters_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowTargetedLines(lstCharacters.SelectedItem as Character, _lines, TargetType.DirectTarget);
			SelectLine(0);
		}

		private void lstTags_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowTargetedLines(lstTags.SelectedItem as Character, _filterLines, TargetType.Filter);
			SelectLine(0);
		}

		private List<TargetData> LoadLines(Character other, TargetType targetType)
		{
			List<TargetData> lines = new List<TargetData>();
			foreach (var stageCase in other.GetWorkingCasesTargetedAtCharacter(_character, targetType))
			{
				//Split the case into sequential ranges, since you can't target non-sequential ranges in a response
				int startStage = stageCase.Stages[0];
				int stage = startStage;
				for (int i = 1; i < stageCase.Stages.Count; i++)
				{
					int nextStage = stageCase.Stages[i];
					if (nextStage - stage > 1)
					{
						//Found a splitting point
						Case subCase = stageCase.Copy();
						for (int s = startStage; s <= stage; s++)
						{
							subCase.Stages.Add(s);
						}
						TargetData subdata = new TargetData(other, subCase);
						lines.Add(subdata);
						startStage = nextStage;
						stage = nextStage;
					}
					else
					{
						stage = nextStage;
					}
				}
				Case lastCase = stageCase.Copy();
				for (int s = startStage; s <= stage; s++)
				{
					lastCase.Stages.Add(s);
				}
				TargetData data = new TargetData(other, lastCase);
				lines.Add(data);
			}
			return lines;
		}

		private void ShowTargetedLines(Character other, Dictionary<Character, List<TargetData>> targetedLines, TargetType type)
		{
			if (other == null)
				return;
			lblLines.Text = "Lines spoken by " + other;
			HideResponses();
			List<TargetData> lines = targetedLines.GetOrAddDefault(other, () =>
			{
				return LoadLines(other, type);
			});
			gridLines.Rows.Clear();
			foreach (var data in lines)
			{
				foreach (var line in data.Case.Lines)
				{
					DataGridViewRow row = gridLines.Rows[gridLines.Rows.Add()];
					row.Tag = data;
					row.Cells["ColText"].Value = line.Text;
					row.Cells["ColText"].Tag = line;
					if (data.Case.Stages.Count == 1)
					{
						row.Cells["ColStage"].Value = data.Case.Stages[0];
					}
					else
					{
						row.Cells["ColStage"].Value = data.Case.Stages[0] + "-" + data.Case.Stages[data.Case.Stages.Count - 1];
					}
					Trigger trigger = TriggerDatabase.GetTrigger(data.Case.Tag);
					row.Cells["ColCase"].Value = GetCaseLabel(data.Case, trigger);
				}
			}

			gridLines.Visible = (lines.Count > 0);
			lblNoMatches.Visible = (lines.Count == 0);
		}

		private string GetCaseLabel(Case targetedCase, Trigger trigger)
		{
			Character target = _character;
			if (targetedCase.Target != null)
			{
				target = CharacterDatabase.Get(targetedCase.Target);
			}

			if (trigger.Tag.Contains("_removing_"))
			{
				int stage;
				if (int.TryParse(targetedCase.TargetStage, out stage))
				{
					//when targeting a specific stage, give a more useful label
					int index = target.Layers - stage - 1;
					if (index < 0 || index >= target.Layers)
						return "!!Invalid case!!";
					Clothing layer = target.Wardrobe[index];
					return "Removing " + layer;
				}
			}
			else if (trigger.Tag.Contains("_removed_"))
			{
				int stage;
				if (int.TryParse(targetedCase.TargetStage, out stage))
				{
					//when targeting a specific stage, give a more useful label
					int index = target.Layers - stage;
					if (index < 0 || index >= target.Layers)
						return "!!Invalid case!!";
					Clothing layer = target.Wardrobe[index];
					return "Removed " + layer;
				}
			}

			return trigger?.ToString();
		}

		private void gridLines_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			_currentLine = e.RowIndex;
			SelectLine(e.RowIndex);
		}

		private void SelectLine(int rowIndex)
		{
			DataGridViewRow row = gridLines.Rows[rowIndex];
			TargetData data = row.Tag as TargetData;
			HideResponses();
			if (data != null)
			{
				_selectedData = data;
				lblCaseInfo.Text = data.Case.ToString();

				lblBaseLine.Text = string.Format("{0} is reacting to these lines from {1}:", _selectedData.Character, _character);

				ImageLibrary library = ImageLibrary.Get(_selectedData.Character);
				DialogueLine line = row.Cells["ColText"].Tag as DialogueLine;
				if (line != null)
				{
					string image = DialogueLine.GetStageImage(data.Case.Stages[0], line.Image);
					CharacterImage img = library.Find(image);
					if (img != null)
					{
						Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, img);
					}
				}


				//See if the character already has a response, and display it if so
				Case sampleResponse = _selectedData.Case.CreateResponse(_selectedData.Character, _character);
				if (sampleResponse == null)
				{
					cmdCreateResponse.Enabled = false; //can't respond to this line
					lstBasicLines.Items.Clear();
				}
				else
				{
					cmdCreateResponse.Enabled = true;
					bool hasResponses = false;
					foreach (Case workingCase in _character.Behavior.GetWorkingCases())
					{
						if (workingCase.MatchesConditions(sampleResponse))
						{
							//Found one
							ShowResponse(workingCase);
							hasResponses = true;
							break;
						}
					}

					if (!hasResponses)
					{
						ShowBasicLines();
					}
				}
			}
			else
			{
				lstBasicLines.Items.Clear();
			}
		}

		private void ShowBasicLines()
		{
			Case speakerCase = _selectedData.Case;

			//show the default lines that the character is probably reacting to
			lstBasicLines.Items.Clear();

			List<Case> possibleCases = Case.GetMatchingCases(speakerCase, _selectedData.Character, _character);
			if (possibleCases.Count == 0) { return; }
			int topPriority = possibleCases[0].GetPriority();
			lblBasicText.Text = possibleCases[0].ToString();
			foreach (var line in possibleCases[0].Lines)
			{
				lstBasicLines.Items.Add(line);
			}
			for (int i = 1; i < possibleCases.Count; i++)
			{
				Case c = possibleCases[i];
				if (c.GetPriority() == topPriority)
				{
					foreach (var line in c.Lines)
					{
						lstBasicLines.Items.Add(line);
					}
				}
				else break;
			}
			return;
		}

		private void gridResponse_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			string image = gridResponse.GetImage(index);
			CharacterImage img = null;
			img = _imageLibrary.Find(image);
			if (img == null)
			{
				int stage = _workingResponse.Stages[0];
				image = DialogueLine.GetDefaultImage(image);
				img = _imageLibrary.Find(stage + "-" + image);
			}
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, img);
		}

		private void cmdCreateResponse_Click(object sender, EventArgs e)
		{
			if (_workingResponse != null)
			{
				DeleteResponse();
			}
			else
			{
				CreateResponse();
			}
		}

		private void DeleteResponse()
		{
			if (_workingResponse == null)
				return;
			Modified = true;
			_character.Behavior.RemoveWorkingCase(_workingResponse);
			HideResponses();
			SelectLine(_currentLine);
		}

		private void CreateResponse()
		{
			if (_selectedData == null)
				return;
			Modified = true;
			Case response = _selectedData.Case.CreateResponse(_selectedData.Character, _character);
			if (response == null)
				return;
			_character.Behavior.AddWorkingCase(response);
			ShowResponse(response);
		}

		private void HideResponses()
		{
			cmdCreateResponse.Text = "Create Response";
			splitContainer3.Panel2Collapsed = true;
			splitContainer3.Panel1Collapsed = false;
			if (_workingResponse != null)
			{
				gridResponse.Save();
				gridResponse.Clear();
				_workingResponse = null;
				_selectedData = null;
			}
		}

		private void ShowResponse(Case response)
		{
			HashSet<int> selectedStages = new HashSet<int>();
			foreach (int stage in response.Stages)
			{
				selectedStages.Add(stage);
			}
			_workingResponse = response;
			gridResponse.SetData(_character, _character.Behavior.Stages.Find(s => s.Id == response.Stages[0]), response, selectedStages, _imageLibrary);
			lblResponse.Text = response.ToString();
			splitContainer3.Panel2Collapsed = false;
			splitContainer3.Panel1Collapsed = true;

			cmdCreateResponse.Text = "Delete Response";
		}

		private class TargetData
		{
			public Character Character;
			public Case Case;

			public TargetData(Character character, Case c)
			{
				Character = character;
				Case = c.Copy();
				Case.Stages.AddRange(c.Stages);
			}
		}

		public override void Save()
		{
			if (_workingResponse != null)
			{
				gridResponse.Save();
			}
		}

		private void cmdLoadTags_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This can take a long time. Proceed?", "Load Tag Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;
				LoadTags();
				Cursor.Current = Cursors.Default;
			}
		}

		private void LoadTags()
		{
			foreach (Character other in CharacterDatabase.Characters)
			{
				List<TargetData> lines = LoadLines(other, TargetType.Filter);
				_filterLines[other] = lines;
				if (lines.Count > 0)
				{
					lstTags.Items.Add(other);
				}
			}

			cmdLoadTags.Visible = false;
			lstTags.Visible = true;
		}
	}
}

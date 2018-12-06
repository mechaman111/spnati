using Desktop;
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
				foreach (var stageCase in other.GetWorkingCasesTargetedAtCharacter(_character, TargetType.All))
				{
					List<TargetData> lines;
					if (!string.IsNullOrEmpty(stageCase.Filter))
					{
						if (!_filterLines.TryGetValue(other, out lines))
						{
							lstTags.Items.Add(other);
							lines = new List<TargetData>();
							_filterLines[other] = lines;
						}
					}
					else
					{
						if (!_lines.TryGetValue(other, out lines))
						{
							lstCharacters.Items.Add(other);
							lines = new List<TargetData>();
							_lines[other] = lines;
						}
					}

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
			}
		}

		private void lstCharacters_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowTargetedLines(lstCharacters.SelectedItem as Character, _lines);
			SelectLine(0);
		}

		private void lstTags_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowTargetedLines(lstTags.SelectedItem as Character, _filterLines);
			SelectLine(0);
		}

		private void ShowTargetedLines(Character other, Dictionary<Character, List<TargetData>> targetedLines)
		{
			if (other == null)
				return;
			lblLines.Text = "Lines spoken by " + other;
			HideResponses();
			List<TargetData> lines = targetedLines[other];
			gridLines.Rows.Clear();
			foreach (var data in lines)
			{
				foreach (var line in data.Case.Lines)
				{
					DataGridViewRow row = gridLines.Rows[gridLines.Rows.Add()];
					row.Tag = data;
					row.Cells["ColText"].Value = line.Text;
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
					foreach (Case workingCase in _character.Behavior.WorkingCases)
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
			_character.Behavior.WorkingCases.Remove(_workingResponse);
			Workspace.SendMessage(WorkspaceMessages.CaseRemoved, _workingResponse);
			_character.Behavior.BuildStageTree(_character);
			_character.Behavior.BuildWorkingCases(_character);
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
			_character.Behavior.WorkingCases.Add(response);
			Workspace.SendMessage(WorkspaceMessages.CaseAdded, _workingResponse);
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
	}
}

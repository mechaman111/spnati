using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class BanterWizard : Form
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

		public void SetCharacter(Character character, ImageLibrary imageLibrary)
		{
			HideResponses();
			_character = character;
			_imageLibrary = imageLibrary;
			lblCharacters.Text = string.Format(lblCharacters.Text, character);
			lstCharacters.Sorted = true;

			//Scan other characters to see who talks to this character
			foreach (Character other in CharacterDatabase.Characters)
			{
				foreach (var stageCase in other.GetWorkingCasesTargetedAtCharacter(character, TargetType.All))
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

		/// <summary>
		/// Creates a CharacterState out of the info in a case
		/// </summary>
		/// <param name="character"></param>
		/// <param name="stateCase"></param>
		/// <returns></returns>
		private CharacterState CreateCharacterState(Character character, Case stateCase)
		{
			CharacterState state = new CharacterState();
			state.Character = character;
			state.Stage = stateCase.Stages[0];
			int min, max;
			Case.ToRange(stateCase.TimeInStage, out min, out max);
			state.TimeInStage = min;
			Case.ToRange(stateCase.ConsecutiveLosses, out min, out max);
			state.Losses = min;
			state.Hand = stateCase.HasHand;
			if (!string.IsNullOrEmpty(stateCase.SaidMarker))
				state.Markers.Add(stateCase.SaidMarker);
			return state;
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
					//foreach (Case workingCase in _character.Behavior.WorkingCases)
					//{
					//	if (workingCase.MatchesConditions(sampleResponse))
					//	{
					//		//Found one
					//		ShowResponse(workingCase);
					//		hasResponses = true;
					//		break;
					//	}
					//}

					if (!hasResponses)
					{
						ShowBasicLines(sampleResponse);
					}
				}
			}
		}

		private Character CreateDummyCharacter()
		{
			Character c = new Character();
			c.FolderName = "dummy";
			c.Wardrobe.Add(new Clothing() { Position = "lower", Type = "important" });
			c.Wardrobe.Add(new Clothing() { Position = "upper", Type = "important" });
			c.Wardrobe.Add(new Clothing() { Position = "upper", Type = "major" });
			c.Wardrobe.Add(new Clothing() { Position = "upper", Type = "minor" });
			c.Wardrobe.Add(new Clothing() { Position = "upper", Type = "extra" });
			return c;
		}

		private void ShowBasicLines(Case sampleResponse)
		{
			Case speakerCase = _selectedData.Case;

			//show the default lines that the character is probably reacting to
			lstBasicLines.Items.Clear();

			//Set up the game state based on the reacting conditions
			GameState state = new GameState();
			CharacterState speaker = CreateCharacterState(_selectedData.Character, speakerCase);
			CharacterState responder = CreateCharacterState(_character, sampleResponse);
			state.Characters.Add(speaker);
			state.Characters.Add(responder);

			int females = 0;
			int males = 0;
			if (_selectedData.Character.Gender == "female")
				females++;
			else males++;
			if (_character.Gender == "female")
				females++;
			else males++;

			Trigger trigger = TriggerDatabase.GetTrigger(speakerCase.Tag);
			string gender = (trigger.Tag.StartsWith("male_") ? "male" : trigger.Tag.StartsWith("female_") ? "female" : null);
			if (trigger.HasTarget)
			{
				if (speakerCase.Target == _character.FolderName || _character.Tags.Contains(speakerCase.Filter))
				{
					if (gender != null && gender != _character.Gender)
						return;
					state.TargetState = responder;
				}
				else
				{
					CharacterState target = new CharacterState();
					if (!string.IsNullOrEmpty(speakerCase.Target))
					{
						target.Character = CharacterDatabase.Get(speakerCase.Target);
					}
					else
					{
						//use some dummy target if none is being targeted specifically
						Character c = CreateDummyCharacter();
						target = new CharacterState();
						target.Character = c;
						if (speakerCase.Tag.StartsWith("male"))
						{
							c.Gender = "male";
						}
						else
						{
							c.Gender = "female";
						}
						if (speakerCase.Tag.Contains("large"))
							c.Size = "large";
						else if (speakerCase.Tag.Contains("large"))
							c.Size = "small";
						else c.Size = "medium";
					}
					state.Characters.Add(target);
					state.TargetState = target;
					if (target.Character.Gender == "female")
						females++;
					else males++;
				}

				if (string.IsNullOrEmpty(speakerCase.TargetStage))
				{
					List<int> targetStages = Case.GetTargetStage(speakerCase.Tag, state.TargetState.Character);
					if (targetStages.Count > 0)
					{
						state.TargetState.Stage = targetStages[0];
					}
				}
				else
				{
					state.TargetState.Stage = speakerCase.TargetStage.ToInt();
				}

				if (!string.IsNullOrEmpty(speakerCase.ConsecutiveLosses))
				{
					state.TargetState.Losses = speakerCase.ConsecutiveLosses.ToInt();
					//make sure they're in a stage where that's possible
					if (state.TargetState.Stage < state.TargetState.Losses)
						state.TargetState.Stage = Math.Min(state.TargetState.Losses, state.TargetState.Character.Layers + Clothing.ExtraStages - 1);
				}
				if (!string.IsNullOrEmpty(speakerCase.TargetTimeInStage))
				{
					state.TargetState.TimeInStage = speakerCase.TargetTimeInStage.ToInt();
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(speakerCase.ConsecutiveLosses))
				{
					speaker.Losses = speakerCase.ConsecutiveLosses.ToInt();
				}
			}

			// Also playing
			if (sampleResponse.AlsoPlaying == _selectedData.Character.FolderName)
			{
				speaker.TimeInStage = sampleResponse.AlsoPlayingTimeInStage.ToInt();
				if (!string.IsNullOrEmpty(sampleResponse.AlsoPlayingSaidMarker))
					speaker.Markers.Add(sampleResponse.AlsoPlayingSaidMarker);
				speaker.Stage = sampleResponse.AlsoPlayingStage.ToInt();
				speaker.Hand = sampleResponse.AlsoPlayingHand;
			}

			// Self
			if (!string.IsNullOrEmpty(speakerCase.TimeInStage))
			{
				speaker.TimeInStage = speakerCase.TimeInStage.ToInt();
			}

			//Fill in the other slots with dummy characters
			int neededFemales = 0;
			int neededMales = 0;
			int max;
			Case.ToRange(speakerCase.TotalMales, out neededMales, out max);
			Case.ToRange(speakerCase.TotalFemales, out neededFemales, out max);
			neededFemales -= females;
			neededMales -= males;
			while (state.Characters.Count < 5)
			{
				Character c = CreateDummyCharacter();
				if (neededMales > 0)
				{
					c.Gender = "male";
					neededMales--;
				}
				else
				{
					c.Gender = "female";
					neededFemales--;
				}
				CharacterState tempState = new CharacterState();
				tempState.Character = c;
				state.Characters.Add(tempState);

				if (trigger.HasTarget && state.Target == null)
				{
					state.TargetState = tempState;
				}
			}

			state.Phase = TriggerDatabase.GetPhase(_selectedData.Case.Tag);

			//set state clothing based on target's stage
			if (state.Target != null)
			{
				int targetStage = state.TargetState.Stage;
				if (state.Phase == GamePhase.AfterLoss)
					targetStage--;
				if (targetStage >= 0 && targetStage < state.TargetState.Character.Layers)
				{
					state.Clothing = state.TargetState.Character.Wardrobe[state.TargetState.Character.Layers - targetStage - 1];
				}
			}

			var availableCases = responder.GetPossibleCases(state);
			if (availableCases.Count > 0)
			{
				foreach (var c in availableCases)
				{
					if (c.MatchesConditions(sampleResponse))
					{
						ShowResponse(c);
						return;
					}
				}
				availableCases.Sort();
				int topPriority = availableCases[0].GetPriority();
				lblBasicText.Text = availableCases[0].ToString();
				foreach (var line in availableCases[0].Lines)
				{
					lstBasicLines.Items.Add(line);
				}
				for (int i = 1; i < availableCases.Count; i++)
				{
					Case c = availableCases[i];
					if (c.GetPriority() == topPriority)
					{
						foreach (var line in c.Lines)
						{
							lstBasicLines.Items.Add(line);
						}
					}
					else break;
				}
			}
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
			if (img != null)
			{
				picPreview.Image = img.Image;
			}
			else picPreview.Image = null;
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

		private void BanterWizard_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_workingResponse != null)
			{
				gridResponse.Save();
				Modified = true;
			}
		}
	}
}

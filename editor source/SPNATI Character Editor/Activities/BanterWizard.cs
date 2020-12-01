using Desktop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Spacer]
	[Activity(typeof(Character), 300, DelayRun = true, Caption = "Banter Wizard")]
	public partial class BanterWizard : Activity
	{
		private Character _character;
		private TargetData _selectedData;
		private Case _workingResponse;
		public bool Modified { get; private set; }
		private Dictionary<Character, Lazy<List<TargetData>>> _lines = new Dictionary<Character, Lazy<List<TargetData>>>();
		private Dictionary<Character, Lazy<List<TargetData>>> _filterLines = new Dictionary<Character, Lazy<List<TargetData>>>();
		private int _oldSplitter;
		private bool _editing;
		private bool _loading;
		private Character _target;

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

		public override bool CanRun()
		{
			return !Config.SafeMode;
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
		}

		protected override void OnFirstActivate()
		{
			ctlResponse.SetCharacter(_character);
			HideResponses();
			lblCharacters.Text = string.Format(lblCharacters.Text, _character);
			lstCharacters.Sorted = true;

			FilterTargets();
		}

		protected override void OnActivate()
		{
			_target = null;
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length > 0 && parameters[0] is Character)
			{
				_target = parameters[0] as Character;
			}
		}


		public override bool CanQuit(CloseArgs args)
		{
			if (_loading)
			{
				return false;
			}
			return base.CanQuit(args);
		}

		public override bool CanDeactivate(DeactivateArgs args)
		{
			if (_loading)
			{
				return false;
			}
			return base.CanDeactivate(args);
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
						subCase.AddStageRange(startStage, stage);
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
				lastCase.AddStageRange(startStage, stage);
				TargetData data = new TargetData(other, lastCase);
				lines.Add(data);
			}
			return lines;
		}

		private void ShowTargetedLines(Character other, Dictionary<Character, Lazy<List<TargetData>>> targetedLines, TargetType type)
		{
			if (other == null)
				return;
			if(!other.IsFullyLoaded)
			{
				other = CharacterDatabase.Load(other.FolderName);
			}
			grpLines.Text = "Lines spoken by " + other;
			HideResponses();
			Lazy<List<TargetData>> lines = targetedLines.GetOrAddDefault(other, () =>
			{
				return new Lazy<List<TargetData>>(() => LoadLines(other, type));
			});
			gridLines.Rows.Clear();
			foreach (var data in lines.Value)
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
					TriggerDefinition trigger = TriggerDatabase.GetTrigger(data.Case.Tag);
					row.Cells["ColCase"].Value = GetCaseLabel(data.Case, trigger);
				}
			}

			gridLines.Visible = (lines.Value.Count > 0);
			lblNoMatches.Visible = (lines.Value.Count == 0);
		}

		private string GetCaseLabel(Case targetedCase, TriggerDefinition trigger)
		{
			Character target = _character;
			if (targetedCase.Target != null)
			{
				target = CharacterDatabase.Get(targetedCase.Target);
				if (target == null) { return "???"; }
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

				grpBaseLine.Text = string.Format("{0} may be reacting to these lines from {1}:", _selectedData.Character, _character);
			
				DialogueLine line = row.Cells["ColText"].Tag as DialogueLine;
				if (line != null)
				{
					int stage = data.Case.Stages[0];
					PoseMapping pose = line.Pose;
					if (pose != null)
					{
						Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_selectedData.Character, pose, stage));
					}
					Workspace.SendMessage(WorkspaceMessages.PreviewLine, line);
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
							ShowResponse(workingCase, false);
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
			PoseMapping image = null;
			if (ctlResponse.Visible)
			{
				image = ctlResponse.GetImage(index);
			}
			else
			{
				image = gridResponse.GetImage(index);
			}
			if (image != null)
			{
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, _workingResponse.Stages[0]));
			}
			Workspace.SendMessage(WorkspaceMessages.PreviewLine, gridResponse.GetLine(index));
		}

		private void cmdCreateResponse_Click(object sender, EventArgs e)
		{
			if (_workingResponse == null)
			{
				CreateResponse();
			}
		}
		
		private void CreateResponse()
		{
			if (_selectedData == null)
				return;
			Modified = true;

			Case caseToRespondTo = _selectedData.Case;

			if (!string.IsNullOrEmpty(_selectedData.Case.Filter) && string.IsNullOrEmpty(_selectedData.Case.Target))
			{
				//If making a response to a line that has a filter but no target, assume they're targeting you directly
				caseToRespondTo = _selectedData.Case.CopyConditions();
				caseToRespondTo.Target = _character.FolderName;
				caseToRespondTo.StageRange = _selectedData.Case.StageRange;
			}

			Case response = caseToRespondTo.CreateResponse(_selectedData.Character, _character);
			if (response == null)
				return;
			ShowResponse(response, true);
		}

		private void HideResponses()
		{
			cmdCreateResponse.Text = "Create Response";
			splitContainer3.Panel2Collapsed = true;
			splitContainer3.Panel1Collapsed = false;
			if (_workingResponse != null)
			{
				if (gridResponse.Visible)
				{
					gridResponse.Save();
					gridResponse.Clear();
				}
				else if (_editing)
				{
					ctlResponse.SetCase(null, null);
					_editing = false;
					gridLines.Enabled = true;
					splitContainer2.SplitterDistance = _oldSplitter;
				}
				_workingResponse = null;
				_selectedData = null;
			}
		}

		private void ShowResponse(Case response, bool editing)
		{
			HashSet<int> selectedStages = new HashSet<int>();
			foreach (int stage in response.Stages)
			{
				selectedStages.Add(stage);
			}
			_workingResponse = response;
			if (editing)
			{
				gridResponse.Visible = false;
				ctlResponse.Visible = true;
				ctlResponse.SetCase(new Stage(response.Stages[0]), response);
			}
			else
			{
				gridResponse.Visible = true;
				ctlResponse.Visible = false;
				Stage stage = new Stage(response.Stages[0]);
				gridResponse.SetData(_character, stage, response, selectedStages);
			}			
			
			grpResponse.Text = $"Response from {_character}";
			splitContainer3.Panel2Collapsed = false;
			splitContainer3.Panel1Collapsed = true;
			cmdAccept.Enabled = editing;
			cmdDiscard.Enabled = editing;

			cmdCreateResponse.Enabled = false;
			if (editing)
			{
				_editing = true;
				_oldSplitter = splitContainer2.SplitterDistance;
				splitContainer2.SplitterDistance = 110;
				if (gridLines.SelectedCells.Count > 0)
				{
					gridLines.FirstDisplayedScrollingRowIndex = gridLines.SelectedCells[0].RowIndex;
				}
				gridLines.Enabled = false;
			}
		}

		private class TargetData
		{
			public Character Character;
			public Case Case;

			public TargetData(Character character, Case c)
			{
				Character = character;
				Case = c.Copy();
				Case.AddStages(c.Stages);
			}
		}

		public override void Save()
		{
			if (_workingResponse != null)
			{
				ctlResponse.Save();
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
			CharacterDatabase.LoadAll();
			foreach (Character other in CharacterDatabase.FilteredCharacters)
			{
				Lazy<List<TargetData>> lines = new Lazy<List<TargetData>>(() => LoadLines(other, TargetType.Filter));
				_filterLines[other] = lines;
				if (lines.Value.Count > 0)
				{
					lstTags.Items.Add(other);
				}
			}

			cmdLoadTags.Visible = false;
			lstTags.Visible = true;
		}

		private void cmdFilter_Click(object sender, EventArgs e)
		{
			FilterTargets();
		}

		private async void FilterTargets()
		{
			_loading = true;
			splitContainer1.Panel1.Enabled = false;
			panelLoad.Visible = true;
			panelLoad.BringToFront();
			int characters = CharacterDatabase.Count;
			Cursor.Current = Cursors.WaitCursor;
			lstCharacters.Items.Clear();
			int count = 0;
			progress.Maximum = characters;
			foreach (Character other in CharacterDatabase.FilteredCharacters)
			{
				progress.Value = count++;
				if (other == _character || other.FolderName == "human")
				{
					continue;
				}
				lblProgress.Text = $"Scanning {other}...";
				bool add = await Task.Run(() =>
				{
					if (_lines.ContainsKey(other))
					{
						if (_lines[other].Value.Count > 0)
						{
							return true;
						}
					}
					else
					{
						CachedCharacter cached = other as CachedCharacter;
						if (cached == null)
						{
							Lazy<List<TargetData>> lines = new Lazy<List<TargetData>>(() => LoadLines(other, TargetType.DirectTarget));
							_lines[other] = lines;
							if (lines.Value.Count > 0)
							{
								return true;
							}
						}
						else
						{
							int targetCount = cached.GetTargetedCountTowards(_character.FolderName);
							if (targetCount > 0)
							{
								_lines[other] = new Lazy<List<TargetData>>(() =>
								{
									Character loaded = CharacterDatabase.Load(other.FolderName);
									return LoadLines(loaded, TargetType.DirectTarget);
								});
								return true;
							}
						}
					}
					return false;
				});
				if (add)
				{
					lstCharacters.Items.Add(other);
				}
			}
			panelLoad.Visible = false;
			int margin = lstCharacters.Top - cmdFilter.Bottom;
			lstCharacters.Height = lstCharacters.Height + cmdFilter.Height + margin;
			lstCharacters.Top = cmdFilter.Top;
			cmdFilter.Visible = false;
			splitContainer1.Panel1.Enabled = true;
			Cursor.Current = Cursors.Default;
			_loading = false;

			if (_target != null)
			{
				lstCharacters.SelectedItem = _target;
				_target = null;
			}
		}

		private void cmdJump_Click(object sender, EventArgs e)
		{
			if (_editing)
			{
				ctlResponse.Save();
				_character.Behavior.AddWorkingCase(_workingResponse);
			}
			else
			{
				gridResponse.Save();
			}
			Shell.Instance.Launch<Character, DialogueEditor>(_character, _workingResponse);
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
			ctlResponse.Save();
			_character.Behavior.AddWorkingCase(_workingResponse);
			HideResponses();
		}

		private void cmdDiscard_Click(object sender, EventArgs e)
		{
			HideResponses();
		}
	}
}

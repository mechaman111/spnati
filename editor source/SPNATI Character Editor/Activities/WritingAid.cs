using Desktop;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	/// <summary>
	/// Activity for finding noteworthy dialogue from other opponents and writing dialogue for it
	/// </summary>
	[Activity(typeof(Character), 310, DelayRun = true, Caption = "Writing Aid")]
	public partial class WritingAid : Activity
	{
		private const string SuggestionPreference = "WritingSuggestions";
		private const string ShowSuggestionSetting = "ShowExisting";
		private const int DefaultSuggestionCount = 10;

		private bool _activated;
		private Character _character;
		private CharacterEditorData _editorData;

		private int _maxSuggestions = 0;

		private bool _showExisting;
		private Character _activeCharacter;
		private Situation _activeSituation;
		private Case _response;

		private int _generationId;

		public WritingAid()
		{
			InitializeComponent();

			recFilter.RecordType = typeof(Character);
			recFilter.RecordFilter = FilterRecords;

			cboPriority.Items.AddRange(new string[] { "- All -", "Must Target", "Noteworthy", "FYI", "Prompt" });
			cboPriority.SelectedIndex = 0;
		}

		public override bool CanRun()
		{
			return !Config.SafeMode;
		}

		private bool FilterRecords(IRecord record)
		{
			if (record == _character || record.Key == "human")
			{
				return false;
			}
			return true;
		}

		public override string Caption
		{
			get { return "Writing Aid"; }
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length >= 1)
			{
				if (parameters[0] is SituationPriority)
				{
					cboPriority.SelectedIndex = (int)parameters[0];
				}
				else if (parameters[0] is Character)
				{
					recFilter.Record = parameters[0] as Character;
				}
			}
			base.OnParametersUpdated(parameters);
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			ColJump.Flat = true;
			_editorData = CharacterDatabase.GetEditorData(_character);
			int suggestions = Config.GetInt(SuggestionPreference);
			if (suggestions == 0)
			{
				suggestions = DefaultSuggestionCount;
			}
			valSuggestions.Value = Math.Max(valSuggestions.Minimum, Math.Min(valSuggestions.Maximum, suggestions));

			chkFilter.Checked = Config.GetBoolean(ShowSuggestionSetting);
			chkFilter.CheckedChanged += ChkFilter_CheckedChanged;
		}

		protected override void OnFirstActivate()
		{
			_activated = true;
			splitContainer1.Panel2Collapsed = true;
			_maxSuggestions = 0;
			foreach (Character c in CharacterDatabase.FilteredCharacters)
			{
				if (c.FolderName == "human" || c == _character)
				{
					continue;
				}

				CharacterEditorData editorData = CharacterDatabase.GetEditorData(c);
				_maxSuggestions += editorData.NoteworthySituations.Count;
			}
			_generationId = Shell.Instance.DelayAction(GenerateSuggestions, 100);
			UpdateResponseCount();
		}

		protected override void OnActivate()
		{
			_character.Behavior.CaseAdded += Behavior_CaseAdded;
			Workspace.SendMessage<DialogueLine>(WorkspaceMessages.PreviewLine, null);
		}

		protected override void OnDeactivate()
		{
			_character.Behavior.CaseAdded -= Behavior_CaseAdded;
		}

		private void cboFilter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!_activated)
			{
				return;
			}
			GenerateSuggestions();
		}

		private void recFilter_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			if (!_activated)
			{
				return;
			}
			GenerateSuggestions();
		}

		private void cmdNew_Click(object sender, EventArgs e)
		{
			GenerateSuggestions();
		}

		private void GenerateSuggestions()
		{
			if (_generationId != 0)
			{
				Shell.Instance.CancelAction(_generationId);
				_generationId = 0;
			}
			Cursor.Current = Cursors.WaitCursor;

			int pri = cboPriority.SelectedIndex;
			if (pri < 0)
			{
				pri = 0;
			}
			SituationPriority priority = (SituationPriority)pri;

			gridSituations.Rows.Clear();
			int suggestionCount = (int)valSuggestions.Value;
			int max = Math.Min(_maxSuggestions, suggestionCount);

			Character filter = recFilter.Record as Character;
			List<Character> possibleCharacters = new List<Character>();
			if (filter != null)
			{
				possibleCharacters.Add(filter);
			}
			else
			{
				possibleCharacters.AddRange(CharacterDatabase.FilteredCharacters);
			}

			List<Tuple<Character, Situation>> suggestions = new List<Tuple<Character, Situation>>();
			foreach (Character c in possibleCharacters)
			{
				CharacterEditorData data = CharacterDatabase.GetEditorData(c);
				if (c != _character && data.NoteworthySituations.Count > 0)
				{
					foreach (Situation s in data.NoteworthySituations)
					{
						SituationPriority sitPriority = s.Priority == SituationPriority.None ? SituationPriority.Noteworthy : s.Priority;
						if (sitPriority == priority || priority == SituationPriority.None)
						{
							suggestions.Add(new Tuple<Character, Situation>(c, s));
						}
					}
				}
			}

			for (int i = 0; i < max && suggestions.Count > 0; i++)
			{
				int index = ExtensionMethods.GetRandom(suggestions.Count);
				Tuple<Character, Situation> item = suggestions[index];
				Character owner = item.Item1;
				Situation situation = item.Item2;
				suggestions.RemoveAt(index);

				if (!_showExisting)
				{
					//see if we've already responded
					bool responded = HasResponded(owner, _character, situation, true);
					if (responded)
					{
						i--;
						continue;
					}
				}

				BuildSuggestionRow(owner, situation);
				if (gridSituations.Rows.Count == 1)
				{
					ShowSituation(owner, situation, gridSituations.Rows[0]);
				}
			}

			Cursor.Current = Cursors.Default;
		}

		public static bool HasResponded(Character speaker, Character character, Situation situation, bool checkLinkedOnly)
		{
			bool responded = false;
			CharacterEditorData editorData = CharacterDatabase.GetEditorData(character);

			if (editorData.HasResponse(speaker, situation.Id))
			{
				return true;
			}
			if (checkLinkedOnly)
			{
				return false;
			}

			if (situation.LinkedCase != null)
			{
				if (situation.LinkedCase.Id > 0)
				{
					//if the case has an ID, then we can potentially speed things up
					responded = editorData.HasResponse(speaker, situation.LinkedCase);
				}
				if (!responded)
				{
					Case response = situation.LinkedCase.CreateResponse(speaker, character);

					if (response != null)
					{
						//See if they already have a response
						Case match = character.Behavior.GetWorkingCases().FirstOrDefault(c => c.MatchesConditions(response) && c.MatchesStages(response, false));
						responded = match != null;
					}
				}
			}
			return responded;
		}

		private void BuildSuggestionRow(Character character, Situation line)
		{
			int index = gridSituations.Rows.Add(character.Name, line.Name, line.Description, line.GetStageString(), line.LinkedCase?.ToString() ?? "...");
			DataGridViewRow row = gridSituations.Rows[index];
			row.Tag = new Tuple<Character, Situation>(character, line);
		}

		private void gridSituations_SelectionChanged(object sender, EventArgs e)
		{
			if (gridSituations.SelectedRows.Count > 0)
			{
				DataGridViewRow row = gridSituations.SelectedRows[0];
				Tuple<Character, Situation> situation = row.Tag as Tuple<Character, Situation>;
				if (situation != null)
				{
					ShowSituation(situation.Item1, situation.Item2, row);
				}
			}
		}

		private void ShowSituation(Character character, Situation situation, DataGridViewRow row)
		{
			Cursor.Current = Cursors.WaitCursor;
			splitSituations.Panel2Collapsed = false;

			if (!character.IsFullyLoaded)
			{
				character = CharacterDatabase.Load(character.FolderName);
				character.PrepareForEdit();
				CharacterEditorData editorData = CharacterDatabase.GetEditorData(character);

				Situation replacement = editorData.NoteworthySituations.Find(s => (situation.Id > 0 && s.Id == situation.Id) || (situation.Id == 0 && situation.Name == s.Name));
				if (replacement != null)
				{
					situation = replacement;
				}

				row.Tag = new Tuple<Character, Situation>(character, situation);
				row.Cells[ColTrigger.Index].Value = situation.LinkedCase?.ToString() ?? "...";
			}
			_activeCharacter = character;
			_activeSituation = situation;
			cmdMarkResponded.Enabled = situation.Id > 0 && !_editorData.HasResponse(_activeCharacter, _activeSituation.Id);
			Stage stage = new Stage(situation.MinStage);
			gridActiveSituation.SetData(character, stage, situation.LinkedCase, new HashSet<int>());
			Cursor.Current = Cursors.Default;
		}

		private void gridActiveSituation_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			PoseMapping image = gridActiveSituation.GetImage(index);
			if (image != null)
			{
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_activeCharacter, image, _activeSituation.MinStage));
			}
		}

		private void gridLines_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			PoseMapping image = gridLines.GetImage(index);
			if (image != null)
			{
				Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, _response.Stages[0]));
			}
		}

		private void cmdRespond_Click(object sender, EventArgs e)
		{
			if (_activeSituation == null) { return; }

			//TODO: If _showExisting is true, we already created the response and checked for matches up front, so save it off and retrieve here
			if (_activeSituation.LinkedCase == null)
			{
				MessageBox.Show("Something's wrong with this situation. You may want to talk to " + _activeCharacter.Metadata.Writer + " about getting it resolved.", "Create Response", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			_response = _activeSituation.LinkedCase.CreateResponse(_activeCharacter, _character);

			if (_response == null || _response.Tag == null)
			{
				MessageBox.Show("This line cannot be responded to (so why was it called out?)", "Create Response", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//See if they already have a response
			Case match = _editorData.GetResponse(_activeCharacter, _activeSituation.LinkedCase);
			if (match == null)
			{
				match = _character.Behavior.GetWorkingCases().FirstOrDefault(c => c.MatchesConditions(_response) && c.MatchesStages(_response, false));
			}
			if (match != null)
			{
				_response = match;
			}
			else
			{
				ResponseSetupForm setup = new ResponseSetupForm();
				setup.SetData(_character, _activeSituation.LinkedCase, _response);
				DialogResult result = setup.ShowDialog();
				if (result == DialogResult.Cancel)
				{
					_response = null;
					return;
				}

				_editorData.MarkResponse(_activeCharacter, _activeSituation.LinkedCase, _response);
			}

			if (_response.Stages.Count == 0)
			{
				_response = null;
				return;
			}

			splitContainer1.Panel1.Enabled = false;
			splitContainer1.Panel2Collapsed = false;
			Stage stage = new Stage(_response.Stages[0]);
			HashSet<int> stages = new HashSet<int>();
			foreach (int i in _response.Stages)
			{
				stages.Add(i);
			}
			gridLines.SetData(_character, stage, _response, stages);
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
			AcceptResponse();
		}

		private void AcceptResponse()
		{
			gridLines.Save();

			//Add this stage
			if (!_character.Behavior.GetWorkingCases().Contains(_response))
			{
				_character.Behavior.AddWorkingCase(_response);
			}
			splitContainer1.Panel2Collapsed = true;
			splitContainer1.Panel1.Enabled = true;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			gridLines.Clear();
			splitContainer1.Panel2Collapsed = true;
			splitContainer1.Panel1.Enabled = true;
		}

		private void valSuggestions_ValueChanged(object sender, EventArgs e)
		{
			if (!_activated) { return; }
			int count = (int)valSuggestions.Value;
			Config.Set(SuggestionPreference, count);
			GenerateSuggestions();
		}

		private void ChkFilter_CheckedChanged(object sender, EventArgs e)
		{
			_showExisting = chkFilter.Checked;
			Config.Set(ShowSuggestionSetting, _showExisting);
			GenerateSuggestions();
		}

		private void cmdJumpToDialogue_Click(object sender, EventArgs e)
		{
			AcceptResponse();
			Shell.Instance.Launch<Character, DialogueEditor>(_character, _response);
		}

		private void chkFilter_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void gridSituations_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == ColJump.Index)
			{
				Tuple<Character, Situation> tuple = gridSituations.Rows[e.RowIndex]?.Tag as Tuple<Character, Situation>;
				if (tuple == null || tuple.Item2.Id == 0)
				{
					return;
				}
				Image img = Properties.Resources.GoToLine;
				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				var w = img.Width;
				var h = img.Height;
				var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
				var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

				e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
				e.Handled = true;
			}
		}

		private void gridSituations_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == ColJump.Index)
			{
				Tuple<Character, Situation> tuple = gridSituations.Rows[e.RowIndex]?.Tag as Tuple<Character, Situation>;
				if (tuple == null || tuple.Item2.Id == 0)
				{
					return;
				}
				Situation s = tuple.Item2;
				Shell.Instance.Launch<Character, DialogueEditor>(tuple.Item1, new ValidationContext(new Stage(s.LinkedCase.Stages[0]), s.LinkedCase, null));
			}
		}

		private void Behavior_CaseAdded(object sender, Case e)
		{
			UpdateResponseCount();
		}

		private void UpdateResponseCount()
		{
			CharacterEditorData editorData = CharacterDatabase.GetEditorData(_character);
			int count = 0;
			if (editorData.Responses != null)
			{
				count = editorData.Responses.Count;
			}
			lblResponseCount.Text = count.ToString();
		}

		private void cmdMarkResponded_Click(object sender, EventArgs e)
		{
			if (_activeSituation == null || _activeSituation.Id == 0) { return; }

			if (_editorData.HasResponse(_activeCharacter, _activeSituation.Id))
			{
				_editorData.MarkResponse(_activeCharacter, _activeSituation.Id);
				UpdateResponseCount();
			}
		}
	}
}

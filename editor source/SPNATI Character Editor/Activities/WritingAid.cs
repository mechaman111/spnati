using Desktop;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	/// <summary>
	/// Activity for finding noteworthy dialogue from other opponents and writing dialogue for it
	/// </summary>
	[Activity(typeof(Character), 310)]
	public partial class WritingAid : Activity
	{
		private const string SuggestionPreference = "WritingSuggestions";
		private const int DefaultSuggestionCount = 10;

		private Character _character;

		private int _maxSuggestions = 0;

		private Character _activeCharacter;
		private Situation _activeSituation;
		private Case _response;

		public WritingAid()
		{
			InitializeComponent();
		}

		public override string Caption
		{
			get { return "Writing Aid"; }
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			int suggestions = Config.GetInt(SuggestionPreference);
			if (suggestions == 0)
			{
				suggestions = DefaultSuggestionCount;
			}
			valSuggestions.Value = Math.Max(valSuggestions.Minimum, Math.Min(valSuggestions.Maximum, suggestions));
		}

		protected override void OnFirstActivate()
		{
			splitContainer1.Panel2Collapsed = true;
			_maxSuggestions = 0;

			cboFilter.Items.Add("- All - ");
			foreach (Character c in CharacterDatabase.Characters)
			{
				if (c == _character)
				{
					continue; //can't respond to yourself
				}
				cboFilter.Items.Add(c);

				CharacterEditorData editorData = CharacterDatabase.GetEditorData(c);
				_maxSuggestions += editorData.NoteworthySituations.Count;
			}
			cboFilter.SelectedIndex = 0;
		}

		private void cboFilter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GenerateSuggestions();
		}

		private void cmdNew_Click(object sender, EventArgs e)
		{
			GenerateSuggestions();
		}

		private void GenerateSuggestions()
		{
			gridSituations.Rows.Clear();
			int suggestionCount = (int)valSuggestions.Value;
			int max = Math.Min(_maxSuggestions, suggestionCount);

			Character filter = cboFilter.SelectedItem as Character;
			Character currentCharacter = filter;
			CharacterEditorData editorData = null;
			if (filter != null)
			{
				editorData = CharacterDatabase.GetEditorData(filter);

				max = Math.Min(suggestionCount, editorData.NoteworthySituations.Count);
			}

			List<Situation> suggestions = new List<Situation>();
			List<Character> noteworthyCharacters = CharacterDatabase.Characters.Where(c => c != _character && CharacterDatabase.GetEditorData(c).NoteworthySituations.Count > 0).ToList();

			while (suggestions.Count < max)
			{
				if (filter == null)
				{
					currentCharacter = noteworthyCharacters.GetRandom();
					editorData = CharacterDatabase.GetEditorData(currentCharacter);
				}

				Situation situation = editorData.NoteworthySituations.GetRandom();
				if (suggestions.Contains(situation))
				{
					continue;
				}

				suggestions.Add(situation);
				BuildSuggestionRow(currentCharacter, situation);
				if (gridSituations.Rows.Count == 1)
				{
					ShowSituation(currentCharacter, situation);
				}
			}
		}

		private void BuildSuggestionRow(Character character, Situation line)
		{
			int index = gridSituations.Rows.Add(character.Name, line.Name, line.Description, line.GetStageString(), line.Case.ToString());
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
					ShowSituation(situation.Item1, situation.Item2);
				}
			}
		}

		private void ShowSituation(Character character, Situation situation)
		{
			splitSituations.Panel2Collapsed = false;

			_activeCharacter = character;
			_activeSituation = situation;
			Stage stage = character.Behavior.Stages[situation.MinStage];
			gridActiveSituation.SetData(character, stage, situation.Case, new HashSet<int>(), ImageLibrary.Get(character));
		}

		private void gridActiveSituation_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			string image = gridActiveSituation.GetImage(index);
			CharacterImage img = null;
			ImageLibrary library = ImageLibrary.Get(_activeCharacter);
			img = library.Find(image);
			if (img == null)
			{
				int stage = _activeSituation.MinStage;
				image = DialogueLine.GetDefaultImage(image);
				img = library.Find(stage + "-" + image);
			}
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, img);
		}

		private void gridLines_HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			string image = gridLines.GetImage(index);
			CharacterImage img = null;
			ImageLibrary library = ImageLibrary.Get(_character);
			img = library.Find(image);
			if (img == null)
			{
				int stage = _response.Stages[0];
				image = DialogueLine.GetDefaultImage(image);
				img = library.Find(stage + "-" + image);
			}
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, img);
		}

		private void cmdRespond_Click(object sender, EventArgs e)
		{
			if (_activeSituation == null) { return; }

			_response = _activeSituation.Case.CreateResponse(_activeCharacter, _character);

			if (_response == null || _response.Tag == null)
			{
				MessageBox.Show("This line cannot be responded to (so why was it called out?)", "Create Response", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//See if they already have a response
			Case match = _character.Behavior.WorkingCases.Find(c => c.MatchesConditions(_response) && c.MatchesStages(_response, false));
			if (match != null)
			{
				_response = match;
			}
			else
			{
				ResponseSetupForm setup = new ResponseSetupForm();
				setup.SetData(_character, _activeSituation.Case, _response);
				DialogResult result = setup.ShowDialog();
				if (result == DialogResult.Cancel)
				{
					_response = null;
					return;
				}
			}

			if (_response.Stages.Count == 0)
			{
				_response = null;
				return;
			}

			splitContainer1.Panel1.Enabled = false;
			splitContainer1.Panel2Collapsed = false;
			Stage stage = _character.Behavior.Stages.Find(s => s.Id == _response.Stages[0]);
			HashSet<int> stages = new HashSet<int>();
			foreach (int i in _response.Stages)
			{
				stages.Add(i);
			}
			gridLines.SetData(_character, stage, _response, stages, ImageLibrary.Get(_character));
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
			AcceptResponse();
		}

		private void AcceptResponse()
		{
			gridLines.Save();

			//Add this stage
			if (!_character.Behavior.WorkingCases.Contains(_response))
			{
				_character.Behavior.WorkingCases.Add(_response);
			}
			Workspace.SendMessage(WorkspaceMessages.CaseAdded, _response);
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
			int count = (int)valSuggestions.Value;
			Config.Set(SuggestionPreference, count);
			GenerateSuggestions();
		}

		private void cmdJumpToDialogue_Click(object sender, EventArgs e)
		{
			AcceptResponse();
			Shell.Instance.Launch<Character, DialogueEditor>(_character, _response);
		}
	}
}

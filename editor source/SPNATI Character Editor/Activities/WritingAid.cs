﻿using Desktop;
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
	[Activity(typeof(Character), 310)]
	public partial class WritingAid : Activity
	{
		private const string SuggestionPreference = "WritingSuggestions";
		private const string ShowSuggestionSetting = "ShowExisting";
		private const int DefaultSuggestionCount = 10;

		private Character _character;
		private CharacterEditorData _editorData;

		private int _maxSuggestions = 0;

		private bool _showExisting;
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
			splitContainer1.Panel2Collapsed = true;
			_maxSuggestions = 0;

			cboFilter.Items.Add("- All - ");
			foreach (Character c in CharacterDatabase.Characters)
			{
				if (c.FolderName == "human" || c == _character)
				{
					continue;
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
			Cursor.Current = Cursors.WaitCursor;
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

			if (filter == null && noteworthyCharacters.Count == 0)
			{
				return;
			}

			int attempts = max + 20; //it's possible to get into an infinite loop if there is a short supply of situations, so take the dumb way out of avoiding this by just trying a maximum number of times
			while (suggestions.Count < max && attempts-- > 0)
			{
				if (filter == null)
				{
					currentCharacter = noteworthyCharacters.GetRandom();
					editorData = CharacterDatabase.GetEditorData(currentCharacter);
				}

				editorData.Initialize();
				Situation situation = editorData.NoteworthySituations.GetRandom();
				if (suggestions.Contains(situation))
				{
					continue;
				}

				if (!_showExisting)
				{
					//see if we've already responded
					bool responded = false;
					if (situation.LinkedCase.Id > 0)
					{
						//if the case has an ID, then we can potentially speed things up
						responded = _editorData.HasResponse(currentCharacter, situation.LinkedCase);
					}
					if (!responded)
					{
						Case response = situation.LinkedCase.CreateResponse(currentCharacter, _character);

						if (response != null)
						{
							//See if they already have a response
							Case match = _character.Behavior.GetWorkingCases().FirstOrDefault(c => c.MatchesConditions(response) && c.MatchesStages(response, false));
							responded = match != null;
						}
					}
					if (responded)
					{
						if (filter != null)
						{
							max--; //avoid an infinite loop of trying the same situation over and over
						}
						continue;
					}
				}

				suggestions.Add(situation);
				BuildSuggestionRow(currentCharacter, situation);
				if (gridSituations.Rows.Count == 1)
				{
					ShowSituation(currentCharacter, situation);
				}
			}
			Cursor.Current = Cursors.Default;
		}

		private void BuildSuggestionRow(Character character, Situation line)
		{
			int index = gridSituations.Rows.Add(character.Name, line.Name, line.Description, line.GetStageString(), line.LinkedCase.ToString());
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
			gridActiveSituation.SetData(character, stage, situation.LinkedCase, new HashSet<int>(), ImageLibrary.Get(character));
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

			//TODO: If _showExisting is true, we already created the response and checked for matches up front, so save it off and retrieve here
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
	}
}

using Desktop;
using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 30)]
	public partial class DialogueEditor : Activity
	{
		private const string FavoriteConditionsSetting = "FavoritedConditions";

		private Character _character;
		private CharacterEditorData _editorData;
		private Stage _selectedStage;
		private Case _selectedCase;
		private FindReplace _findForm;
		private bool _pendingWardrobeChange;
		private bool _exportOnQuit;

		private enum TreeFilterMode
		{
			All = 0,
			NonTargeted = 1,
			Targeted = 2
		}

		public DialogueEditor()
		{
			InitializeComponent();
		}

		#region Activity
		public override string Caption
		{
			get
			{
				return "Dialogue";
			}
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;
			_editorData = CharacterDatabase.GetEditorData(_character);
			SetupMessageHandlers();
			panelCase.Visible = false;
			caseControl.TextUpdated += GridDialogue_TextUpdated;
			caseControl.HighlightRow += HighlightRow;
			caseControl.KeyDown += CaseControl_KeyDown;
		}

		private void CaseControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (_findForm.Visible && e.KeyCode == Keys.Enter)
			{
				//Redirect enter to the Find form
				_findForm.RepeatKeyPress();
				e.Handled = true;
			}
		}

		private void GridDialogue_TextUpdated(object sender, DialogueLine e)
		{
			DisplayText(e);
		}

		/// <summary>
		/// Saves to the character in memory, but doesn't write to disk
		/// </summary>
		public override void Save()
		{
			caseControl.Save();
		}

		protected override void OnFirstActivate()
		{
			Character c = _character;
			caseControl.SetCharacter(c);
			treeDialogue.SetData(c);
			_selectedStage = null;
			_selectedCase = null;

			caseControl.Activate();
			_character = c;

			OnSettingsUpdated();

			SetupFindReplace();
		}

		protected override void OnActivate()
		{
			if (_pendingWardrobeChange)
			{
				_pendingWardrobeChange = false;
				caseControl.UpdateStages();
				treeDialogue.RegenerateTree();
			}
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length > 0)
			{
				ValidationContext context = parameters[0] as ValidationContext;
				if (context != null)
				{
					JumpToLine(context.Stage, context.Case, context.Line);
				}
				else if (parameters[0] is Case)
				{
					Case jumpCase = parameters[0] as Case;
					Stage stage = new Stage(jumpCase.Stages[0]);
					JumpToLine(stage, jumpCase, null);
				}
			}
		}

		protected override void OnDeactivate()
		{
			_findForm.Hide();
		}

		public override bool CanQuit(CloseArgs args)
		{
			caseControl.SaveFavorites();

			return PromptToSave();
		}

		public override void Quit()
		{
			if (_exportOnQuit)
			{
				Export(false);
			}
		}

		#endregion

		#region Messaging
		private void SetupMessageHandlers()
		{
			SubscribeWorkspace<bool>(WorkspaceMessages.Save, OnSaveWorkspace);
			SubscribeWorkspace(WorkspaceMessages.Find, OnFind);
			SubscribeWorkspace(WorkspaceMessages.Replace, OnReplace);
			SubscribeWorkspace(WorkspaceMessages.WardrobeUpdated, OnWardrobeChanged);
			SubscribeWorkspace(WorkspaceMessages.SkinChanged, OnSkinChanged);
			SubscribeDesktop(DesktopMessages.SettingsUpdated, OnSettingsUpdated);
			SubscribeDesktop(DesktopMessages.MacrosUpdated, OnMacrosUpdated);
		}

		private void OnSettingsUpdated()
		{
			caseControl.AutoOpenConditions = Config.AutoOpenConditions;
		}

		private void OnMacrosUpdated()
		{
			caseControl.UpdateMacros();
		}

		private void OnSaveWorkspace(bool auto)
		{
			if (!auto && !Config.SuppressDefaults && _character.Behavior.EnsureDefaults(_character))
			{
				Shell.Instance.SetStatus("Character was missing some required lines, so defaults were automatically pulled in.");
			}
			Export(auto);
		}

		private void OnFind()
		{
			if (!IsActive) { return; }
			_findForm.SetReplaceMode(false);
			_findForm.Show();
		}

		private void OnReplace()
		{
			if (!IsActive) { return; }
			_findForm.SetReplaceMode(true);
			_findForm.Show();
		}

		private void OnWardrobeChanged()
		{
			_pendingWardrobeChange = true;
		}

		private void OnSkinChanged()
		{
			caseControl.Save();
			caseControl.UpdateStages();
		}
		#endregion

		/// <summary>
		/// Prompts the user to export the current character
		/// </summary>
		/// <returns></returns>
		private bool PromptToSave()
		{
			if (_character == null || !_character.IsDirty)
				return true;
			DialogResult result = MessageBox.Show(string.Format("Do you wish to save {0} first?", _character), "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes)
			{
				_exportOnQuit = true;
				return true;
			}
			else if (result == DialogResult.No)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Exports the current character to disk (i.e. updates the meta.xml and behaviour.xml files)
		/// </summary>
		private bool Export(bool auto)
		{
			if (_character == null)
				return true;
			Save();
			if (Serialization.ExportCharacter(_character))
			{
				if (auto)
				{
					Shell.Instance.SetStatus(string.Format("{0} autosaved at {1}.", _character, DateTime.Now.ToShortTimeString()));
				}
				else
				{
					Shell.Instance.SetStatus(string.Format("{0} exported successfully at {1}.", _character, DateTime.Now.ToShortTimeString()));
				}
				return true;
			}
			else
			{
				if (auto)
				{
					Shell.Instance.SetStatus(string.Format("{0} failed to autosave.", _character));
				}
				else
				{
					Shell.Instance.SetStatus(string.Format("{0} failed to export.", _character));
				}
				return false;
			}
		}

		/// <summary>
		/// Updates the preview image to display the selected line of dialogue's pose
		/// </summary>
		/// <param name="index"></param>
		private void HighlightRow(object sender, int index)
		{
			if (index == -1)
				return;
			PoseMapping image = caseControl.GetImage(index);
			if (image != null)
			{
				DisplayImage(image, caseControl.PreviewStage);
			}

			DialogueLine line = caseControl.GetLine(index);
			DisplayText(line);
		}

		private void DisplayText(DialogueLine line)
		{
			Workspace.SendMessage(WorkspaceMessages.PreviewLine, line);
		}

		/// <summary>
		/// Displays an image in the preview box
		/// </summary>
		/// <param name="image">Image to display</param>
		private void DisplayImage(PoseMapping image, int stage)
		{
			if (_selectedCase != null)
			{
				List<string> markers = _selectedCase.GetMarkers();
				Workspace.SendMessage(WorkspaceMessages.UpdateMarkers, markers);
			}
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, new UpdateImageArgs(_character, image, stage));
		}

		private void cmdCallOut_Click(object sender, EventArgs e)
		{
			if (_selectedCase != null)
			{
				if (_editorData.IsCalledOut(_selectedCase)) { return; } //can't call something out twice

				caseControl.Save();
				if (!_selectedCase.HasConditions && !TriggerDatabase.GetTrigger(_selectedCase.Tag).OncePerStage)
				{
					if (MessageBox.Show("This case's triggering conditions are very broad, making it very likely that other characters will respond to this inaccurately. Are you sure you want to call this out for targeting?",
						"Call Out Situation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					{
						return;
					}
				}
				CallOutForm form = new CallOutForm();
				if (form.ShowDialog() == DialogResult.OK)
				{
					Situation line = _editorData.MarkNoteworthy(_selectedCase, form.Priority);
					Shell.Instance.Launch<Character, SituationEditor>(_character, line);
				}
			}
		}

		private void cmdMakeResponse_Click(object sender, EventArgs e)
		{
			if (_selectedCase == null) { return; }
			string last = RecordLookup.GetLastLookup<Character>();
			if (last == _character.FolderName)
			{
				last = "";
			}
			Character responder = RecordLookup.DoLookup(typeof(Character), last, false, CharacterDatabase.FilterHuman, true, null) as Character;
			if (responder != null)
			{
				if (!responder.IsFullyLoaded)
				{
					responder = CharacterDatabase.Load(responder.FolderName);
				}

				CharacterEditorData responderData = CharacterDatabase.GetEditorData(responder);
				Case existing = responderData.GetResponse(_character, _selectedCase);
				Case response = null;
				bool isNew = false;
				if (existing != null)
				{
					//jump to the existing response
					response = existing;
				}
				else
				{
					if (responder == _character)
					{
						if (MessageBox.Show("Do you really want to respond to your own lines?", "Make Response", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
						{
							return;
						}
					}

					response = _selectedCase.CreateResponse(_character, responder);
					if (response == null)
					{
						MessageBox.Show("Couldn't create a response based on this case's conditions.", "Make Response", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					DataConversions.ConvertCase5_2(response);
					//see if there's a response already matching the conditions of this response and just reuse that if possible
					existing = responder.Behavior.GetWorkingCases().FirstOrDefault(c => c.MatchesConditions(response) && c.MatchesStages(response, true));
					if (existing == null)
					{
						isNew = true;
					}
					else
					{
						isNew = false;
						response = existing;
					}
					responder.PrepareForEdit();
				}

				ResponseRecord record = new ResponseRecord(_character, _selectedCase, responder, response, isNew);
				Shell.Instance.LaunchWorkspace(record);
			}
		}

		private void JumpToLine(Stage stage, Case stageCase, DialogueLine line)
		{
			treeDialogue.SelectNode(stage.Id, stageCase);
			if (line != null)
			{
				caseControl.SelectLine(line);
			}
		}

		private void tree_SelectedNodeChanging(object sender, CaseSelectionEventArgs e)
		{
			caseControl.Save();
		}

		private void tree_SelectedCaseChanged(object sender, CaseSelectionEventArgs e)
		{
			_selectedStage = e.Stage;
			_selectedCase = e.Case;
			if (_selectedCase != null)
			{
				splitDialogue.Panel2.Visible = true;
				panelCase.Visible = true;
				cmdCallOut.Enabled = cmdMakeResponse.Enabled = _selectedCase.GetResponseTag(_character, _character) != null;
			}
			else
			{
				panelCase.Visible = false;
				splitDialogue.Panel2.Visible = false;
			}

			caseControl.SetCase(_selectedStage, _selectedCase);
		}

		private void tree_CreatingCase(object sender, CaseCreationEventArgs e)
		{
			caseControl.Save();
		}

		#region Find/Replace
		/// <summary>
		/// Hooks up event handlers for the Find/Replace form
		/// </summary>
		private void SetupFindReplace()
		{
			_findForm = new FindReplace();
			_findForm.Find += DoFindReplace;
			_findForm.Replace += DoFindReplace;
			_findForm.ReplaceAll += DoFindReplace;
			_findForm.RestoreFocus += _findForm_RestoreFocus;
		}

		private void DoFindReplace(object sender, FindArgs args)
		{
			if (_character == null || string.IsNullOrEmpty(args.FindText))
				return;

			List<Case> cases = _character.Behavior.GetWorkingCases().ToList();
			int startCaseIndex = 0;

			//Look at the current screen before doing cases in the data structure
			if (_selectedCase != null)
			{
				startCaseIndex = Math.Max(0, cases.IndexOf(_selectedCase));
				bool found = caseControl.FindReplace(args);
				if (found)
					return;
			}

			//Nothing found, deselect everything
			caseControl.ClearSelection();

			//Now look across all cases
			List<Case> otherCases = new List<Case>();
			for (int i = startCaseIndex + 1; i < cases.Count; i++)
			{
				otherCases.Add(cases[i]);
			}
			for (int i = 0; i < startCaseIndex; i++)
			{
				otherCases.Add(cases[i]);
			}
			for (int i = 0; i < otherCases.Count; i++)
			{
				Case c = otherCases[i];
				for (int l = 0; l < c.Lines.Count; l++)
				{
					if (args.SearchMarkers)
					{
						string marker = c.Lines[l].Marker;
						if (!string.IsNullOrEmpty(marker))
						{
							int index = caseControl.FindText(marker, 0, args);
							if (index >= 0)
							{
								args.Success = true;
								//select the case
								if (treeDialogue.SelectNode(_selectedStage?.Id ?? c.Stages[0], c))
								{
									//Select the line
									caseControl.SelectTextInRow(l, index, args.FindText.Length, true);
								}
								else
								{
									args.Success = false;
								}
								if (args.Success)
								{
									return;
								}
							}
						}
					}
					else
					{
						string text = c.Lines[l].Text;
						if (!string.IsNullOrEmpty(text))
						{
							int index = caseControl.FindText(text, 0, args);
							if (index >= 0)
							{
								args.Success = true;

								if (args.DoReplace)
								{
									text = text.ReplaceAt(index, args.FindText, args.ReplaceText);
									args.ReplaceCount++;
									c.Lines[l].Text = text;
								}
								else
								{
									//Select the case
									if (treeDialogue.SelectNode(_selectedStage?.Id ?? c.Stages[0], c))
									{
										//Select the line
										caseControl.SelectTextInRow(l, index, args.FindText.Length, args.SearchMarkers);
									}
									else
									{
										args.Success = false;
									}
								}

								if (!args.ReplaceAll && args.Success)
									return;
							}
						}
					}					
				}
			}
		}

		private void _findForm_RestoreFocus(object sender, EventArgs e)
		{
			caseControl.Focus();
		}
		#endregion
	}
}

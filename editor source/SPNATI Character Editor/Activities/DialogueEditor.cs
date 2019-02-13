using Desktop;
using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Activity(typeof(Character), 30)]
	public partial class DialogueEditor : Activity
	{
		private const string FavoriteConditionsSetting = "FavoritedConditions";

		private Character _character;
		private CharacterEditorData _editorData;
		private ImageLibrary _imageLibrary = new ImageLibrary();
		private Stage _selectedStage;
		private Case _selectedCase;
		private FindReplace _findForm;
		private bool _populatingCase;
		private bool _pendingWardrobeChange;
		private bool _exportOnQuit;
		private List<DialogueLine> _lineClipboard = new List<DialogueLine>();

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
			tableConditions.RecordFilter = FilterTargets;
			tableConditions.Context = _character;
			SetupMessageHandlers();
			grpCase.Enabled = false;
		}

		/// <summary>
		/// Saves to the character in memory, but doesn't write to disk
		/// </summary>
		public override void Save()
		{
			SaveCase();
		}

		protected override void OnFirstActivate()
		{
			Character c = _character;
			treeDialogue.SetData(c);
			_selectedStage = null;
			_selectedCase = null;

			_character = c;
			_imageLibrary = ImageLibrary.Get(c);

			CreateStageCheckboxes();

			SetupFindReplace();
		}

		protected override void OnActivate()
		{
			if (_pendingWardrobeChange)
			{
				_pendingWardrobeChange = false;
				CreateStageCheckboxes();
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
					Stage stage = _character.Behavior.Stages[jumpCase.Stages[0]];
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
			//Update favorite conditions
			List<string> favorites = tableConditions.GetFavorites();
			Config.Set(FavoriteConditionsSetting, string.Join("|", favorites));

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
		}

		private void OnSaveWorkspace(bool auto)
		{
			if (!auto && _character.Behavior.EnsureDefaults(_character))
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
		#endregion

		#region Event handlers
		/// <summary>
		/// Raised when a Stage checkbox is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Check_CheckedChanged(object sender, EventArgs e)
		{
			if (_populatingCase)
				return;
			_populatingCase = true;
			UpdateCheckAllState();
			gridDialogue.UpdateAvailableImagesForCase(GetSelectedStages(), true);
			_populatingCase = false;
		}
		
		/// <summary>
		/// Checks or unchecks all stages besides the current stage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			if (_populatingCase)
				return;
			int currentStage = _selectedStage == null ? 0 : _selectedStage.Id;
			bool newState = chkSelectAll.Checked;
			_populatingCase = true;
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				if (i == currentStage)
					continue;
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (box != null && box.Enabled)
				{
					box.Checked = newState;
				}
			}
			_populatingCase = false;
			gridDialogue.UpdateAvailableImagesForCase(GetSelectedStages(), true);
		}

		/// <summary>
		/// Copies the current case's lines to the clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdCopyAll_Click(object sender, EventArgs e)
		{
			if (_selectedCase == null)
				return;
			_lineClipboard = gridDialogue.CopyLines();
			Shell.Instance.SetStatus(string.Format("Lines from {0} copied to the clipboard.", _selectedCase));
		}

		/// <summary>
		/// Pastes the lines in the clipboard into the selected case, either replacing or appending to the existing lines
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdPasteAll_Click(object sender, EventArgs e)
		{
			if (_selectedCase == null || _lineClipboard.Count == 0)
				return;

			if (!gridDialogue.IsEmpty)
			{
				DialogResult result = MessageBox.Show("Do you want to overwrite the existing lines?", "Paste Lines", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Cancel)
					return;
				else if (result == DialogResult.Yes)
				{
					gridDialogue.Clear();
				}
			}
			gridDialogue.PasteLines(_lineClipboard);
		}

		private void gridDialogue_HighlightRow(object sender, int index)
		{
			HighlightRow(index);
		}

		private void gridDialogue_KeyDown(object sender, KeyEventArgs e)
		{
			if (_findForm.Visible && e.KeyCode == Keys.Enter)
			{
				//Redirect enter to the Find form
				_findForm.RepeatKeyPress();
				e.Handled = true;
			}
		}

		private void ckbShowAdvanced_CheckedChanged(object sender, EventArgs e)
		{
			this.gridDialogue.ShowAdvancedColumns = ckbShowAdvanced.Checked;
		}
		#endregion

		/// <summary>
		/// Prompts the user to export the current character
		/// </summary>
		/// <returns></returns>
		private bool PromptToSave()
		{
			if (_character == null)
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
				bool found = gridDialogue.FindReplace(args);
				if (found)
					return;
			}

			//Nothing found, deselect everything
			gridDialogue.ClearSelection();

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
					string text = c.Lines[l].Text;
					if (!string.IsNullOrEmpty(text))
					{
						int index = gridDialogue.FindText(text, 0, args);
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
								treeDialogue.SelectNode(_selectedStage?.Id ?? c.Stages[0], c);
								//Select the line
								gridDialogue.SelectTextInRow(l, index, args.FindText.Length);
							}

							if (!args.ReplaceAll)
								return;
						}
					}
				}
			}
		}

		private void _findForm_RestoreFocus(object sender, EventArgs e)
		{
			gridDialogue.SetFocus();
		}

		#endregion

		/// <summary>
		/// Loads the newly selected case into the dialogue fields
		/// </summary>
		private void PopulateCase()
		{
			if (_selectedCase == null)
			{
				grpCase.Visible = false;
				return;
			}
			else
			{
				grpCase.Visible = true;
				grpCase.Enabled = true;
			}

			_populatingCase = true;
			Case stageCase = _selectedCase;

			PopulateStageCheckboxes();

			int stageId = _selectedStage == null ? 0 : _selectedStage.Id;
			Trigger caseTrigger = TriggerDatabase.GetTrigger(stageCase.Tag);

			#region Case-wide settings
			//Tag combo box
			cboCaseTags.Items.Clear();
			if (_selectedStage != null)
			{
				Trigger selection = null;
				foreach (string tag in TriggerDatabase.GetTags())
				{
					if (TriggerDatabase.UsedInStage(tag, _character, stageId))
					{
						Trigger t = new Trigger(tag, TriggerDatabase.GetLabel(tag));
						if (tag == _selectedCase.Tag)
							selection = t;
						cboCaseTags.Items.Add(t);
					}
				}
				cboCaseTags.SelectedItem = selection;
				cboCaseTags.Enabled = true;
			}
			else
			{
				cboCaseTags.Enabled = false;
			}

			//Help text
			lblHelpText.Text = caseTrigger.HelpText;

			//Available variables
			List<string> vars = new List<string>();
			foreach (Variable globalVar in VariableDatabase.GlobalVariables)
			{
				vars.Add($"~{globalVar.Name}~");
			}
			foreach (string variable in caseTrigger.AvailableVariables)
			{
				vars.Add($"~{variable}~");
			}
			toolTip1.SetToolTip(lblAvailableVars, string.Format("Variables: {0}", string.Join(" ", vars)));

			#endregion

			txtNotes.Text = _editorData.GetNote(_selectedCase);

			if (caseTrigger.HasTarget)
			{
				tableConditions.RecordFilter = null;
			}
			else
			{
				tableConditions.RecordFilter = FilterTargets;
			}
			bool firstPopulation = (tableConditions.Data == null);
			tableConditions.Data = _selectedCase;
			AddSpeedButtons();

			if (firstPopulation)
			{
				List<string> favorites = new List<string>();
				string favoritesData = Config.GetString(FavoriteConditionsSetting);
				if (!string.IsNullOrEmpty(favoritesData))
				{
					foreach (string key in favoritesData.Split('|'))
					{
						if (!string.IsNullOrEmpty(key))
						{
							favorites.Add(key);
						}
					}
				}
				tableConditions.SetFavorites(favorites);
			}
			

			GUIHelper.SetNumericBox(valPriority, _selectedCase.CustomPriority);

			#region Dialogue
			var stages = GetSelectedStages();
			gridDialogue.SetData(_character, _selectedStage, _selectedCase, stages, _imageLibrary);
			GetSelectedStages();
			#endregion

			_populatingCase = false;
			HighlightRow(0);
		}

		private void AddSpeedButtons()
		{
			if (_selectedCase == null) { return; }
			tableConditions.AddSpeedButton("Game", "Background", (data) => { return AddVariableTest("~background~", data); });
			tableConditions.AddSpeedButton("Game", "Inside/Outside", (data) => { return AddVariableTest("~background.location~", data); });
			tableConditions.AddSpeedButton("Self", "Costume", (data) => { return AddVariableTest("~self.costume~", data); });
			tableConditions.AddSpeedButton("Self", "Slot", (data) => { return AddVariableTest("~self.slot~", data); });
			Trigger caseTrigger = TriggerDatabase.GetTrigger(_selectedCase.Tag);
			if (caseTrigger.HasTarget)
			{
				if (caseTrigger.AvailableVariables.Contains("clothing"))
				{
					tableConditions.AddSpeedButton("Clothing", "Clothing Position", (data) => { return AddVariableTest("~clothing.position~", data); });
				}
				tableConditions.AddSpeedButton("Target", "Target Costume", (data) => { return AddVariableTest("~target.costume~", data); });
				tableConditions.AddSpeedButton("Target", "Target Position", (data) => { return AddVariableTest("~target.position~", data); });
				tableConditions.AddSpeedButton("Target", "Target Slot", (data) => { return AddVariableTest("~target.slot~", data); });
			}
		}

		private string AddVariableTest(string variable, object data)
		{
			Case theCase = data as Case;
			theCase.Expressions.Add(new ExpressionTest(variable, ""));
			return "Expressions";
		}

		private HashSet<int> GetSelectedStages()
		{
			HashSet<int> selectedStages = new HashSet<int>();
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (box.Checked)
				{
					selectedStages.Add(i);
				}
			}
			return selectedStages;
		}

		/// <summary>
		/// Sets the checked state for each stage for the current case
		/// </summary>
		private void CreateStageCheckboxes()
		{
			//Stage checkmarks
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (box != null)
				{
					box.CheckedChanged -= Check_CheckedChanged;
				}
			}
			flowStageChecks.Controls.Clear();
			int layers = _character.Layers + 3;
			for (int i = 0; i < layers; i++)
			{
				StageName stage = _character.LayerToStageName(i);
				CheckBox check = new CheckBox();
				check.CheckedChanged += Check_CheckedChanged;
				check.Tag = stage;
				check.Text = string.Format("{0} ({1})", stage.DisplayName, stage.Id);
				check.Width = 180;
				check.Margin = new Padding(0);
				flowStageChecks.Controls.Add(check);
			}
		}

		private bool FilterTargets(PropertyRecord record)
		{
			if (record.Group == "Target")
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Puts the data in the fields into the selected case object
		/// </summary>
		/// <param name="switchingCases">True when saving within the context of switching selected cases</param>
		/// <returns>True if cases were changed in such a way that the dialogue tree needs to be regenerated</returns>
		private bool SaveCase()
		{
			if (_selectedCase == null)
				return false;

			SaveNotes();
			bool needRegeneration = false;
			var c = _selectedCase;
			if (c.Tag != Trigger.StartTrigger)
			{
				string newTag = GUIHelper.ReadComboBox(cboCaseTags);
				if (newTag != c.Tag)
					needRegeneration = true;
				c.Tag = newTag;

				//Figure out the stages
				List<int> oldStages = new List<int>();
				oldStages.AddRange(c.Stages);
				c.Stages.Clear();
				for (int i = 0; i < flowStageChecks.Controls.Count; i++)
				{
					CheckBox box = flowStageChecks.Controls[i] as CheckBox;
					if (box.Checked && TriggerDatabase.UsedInStage(newTag, _character, i))
					{
						c.Stages.Add(i);
						if (!oldStages.Contains(i))
							needRegeneration = true;
					}
					else if (oldStages.Contains(i))
						needRegeneration = true;
				}

				tableConditions.Save();
				
				c.CustomPriority = GUIHelper.ReadNumericBox(valPriority);
			}

			//Lines
			gridDialogue.Save();

			_character.Behavior.ApplyChanges(_selectedCase);

			return needRegeneration;
		}

		/// <summary>
		/// Updates the stage checkboxes to match the selected case's stages
		/// </summary>
		private void PopulateStageCheckboxes()
		{
			chkSelectAll.Enabled = (_selectedStage != null);
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (_selectedCase != null)
				{
					box.Enabled = TriggerDatabase.UsedInStage(_selectedCase.Tag, _character, i);
				}
				box.Checked = _selectedCase == null ? false : _selectedCase.Stages.Contains(i);
			}
			UpdateCheckAllState();
		}

		/// <summary>
		/// Updates the Select All checkbox based on the individual stage checkboxes
		/// </summary>
		private void UpdateCheckAllState()
		{
			bool allChecked = true;
			bool noneChecked = true;
			for (int i = 0; i < flowStageChecks.Controls.Count; i++)
			{
				CheckBox box = flowStageChecks.Controls[i] as CheckBox;
				if (_selectedStage != null && _selectedStage.Id != i && box.Enabled)
				{
					if (box.Checked)
						noneChecked = false;
					else allChecked = false;
				}
			}
			if (chkSelectAll.Enabled)
			{
				chkSelectAll.CheckState = allChecked ? CheckState.Checked : noneChecked ? CheckState.Unchecked : CheckState.Indeterminate;
			}
			else
			{
				chkSelectAll.Checked = false;
			}
		}

		/// <summary>
		/// Updates the preview image to display the selected line of dialogue's pose
		/// </summary>
		/// <param name="index"></param>
		private void HighlightRow(int index)
		{
			if (index == -1 || _populatingCase)
				return;
			string image = gridDialogue.GetImage(index);
			CharacterImage img = null;
			img = _imageLibrary.Find(image);
			if (img == null)
			{
				int stage = _selectedStage == null ? 0 : _selectedStage.Id;
				image = DialogueLine.GetStageImage(stage, DialogueLine.GetDefaultImage(image));
				img = _imageLibrary.Find(image);
			}
			DisplayImage(img);
		}

		/// <summary>
		/// Displays an image in the preview box
		/// </summary>
		/// <param name="image">Image to display</param>
		private void DisplayImage(CharacterImage image)
		{
			Workspace.SendMessage(WorkspaceMessages.UpdatePreviewImage, image);
		}

		private void cmdCallOut_Click(object sender, EventArgs e)
		{
			if (_selectedCase != null)
			{
				if (_editorData.IsCalledOut(_selectedCase)) { return; } //can't call something out twice

				SaveCase();
				if (!_selectedCase.HasFilters && !TriggerDatabase.GetTrigger(_selectedCase.Tag).OncePerStage)
				{
					if (MessageBox.Show("This case's triggering conditions are very broad, making it very likely that other characters will respond to this inaccurately. Are you sure you want to call this out for targeting?",
						"Call Out Situation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					{
						return;
					}
				}
				Situation line = _editorData.MarkNoteworthy(_selectedCase);
				Shell.Instance.Launch<Character, SituationEditor>(_character, line);
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
				CharacterEditorData responderData = CharacterDatabase.GetEditorData(responder);
				Case existing = responderData.GetResponse(_character, _selectedCase);
				Case response = null;
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

					responderData.MarkResponse(_character, _selectedCase, response);
					responder.PrepareForEdit();
					responder.Behavior.AddWorkingCase(response);
				}
				Shell.Instance.Launch<Character, DialogueEditor>(responder, response);
			}
		}

		private void JumpToLine(Stage stage, Case stageCase, DialogueLine line)
		{
			treeDialogue.SelectNode(stage.Id, stageCase);
			if (line != null)
			{
				gridDialogue.SelectLine(line);
			}
		}

		private void tree_SelectedNodeChanging(object sender, CaseSelectionEventArgs e)
		{
			SaveCase();
		}

		private void tree_SelectedCaseChanged(object sender, CaseSelectionEventArgs e)
		{
			_selectedStage = e.Stage;
			_selectedCase = e.Case;
			if (_selectedCase != null)
			{
				splitDialogue.Panel2.Visible = true;
				grpConditions.Enabled = true;
				cmdCallOut.Enabled = true;
			}
			else
			{
				splitDialogue.Panel2.Visible = false;
			}
			PopulateCase();
		}

		private void tree_CreatingCase(object sender, CaseCreationEventArgs e)
		{
			SaveCase();
		}

		private void tree_CreatedCase(object sender, CaseCreationEventArgs e)
		{

		}

		private void txtNotes_Validated(object sender, EventArgs e)
		{
			SaveNotes();
		}

		private void SaveNotes()
		{
			if (_selectedCase == null)
			{
				return;
			}
			_editorData.SetNote(_selectedCase, txtNotes.Text);
		}
	}
}

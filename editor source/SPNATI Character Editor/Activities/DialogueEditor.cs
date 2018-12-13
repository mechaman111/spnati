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
		private const string UseOldEditorSetting = "UseOldEditor";
		private bool _usingOldEditor;

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

			_usingOldEditor = Config.GetBoolean(UseOldEditorSetting);
			EnableOldEditor(_usingOldEditor);
		}

		private void EnableOldEditor(bool value)
		{
			tableConditions.Visible = !value;
			tabControlConditions.Visible = value;
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
			ImageCache.Clear();
			treeDialogue.SetData(c);
			_selectedStage = null;
			_selectedCase = null;
			
			_character = c;
			_imageLibrary = ImageLibrary.Get(c);

			CreateStageCheckboxes();

			PopulateListingFields();
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
			return PromptToSave();
		}

		public override void Quit()
		{
			if (_exportOnQuit)
			{
				Export();
			}
		}

		#endregion

		#region Messaging
		private void SetupMessageHandlers()
		{
			SubscribeWorkspace(WorkspaceMessages.Save, OnSaveWorkspace);
			SubscribeWorkspace(WorkspaceMessages.Find, OnFind);
			SubscribeWorkspace(WorkspaceMessages.Replace, OnReplace);
			SubscribeWorkspace(WorkspaceMessages.WardrobeUpdated, OnWardrobeChanged);
		}

		private void OnSaveWorkspace()
		{
			if (_character.Behavior.EnsureDefaults(_character))
			{
				Shell.Instance.SetStatus("Character was missing some required lines, so defaults were automatically pulled in.");
			}
			Export();
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

		private void cboLineTarget_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingCase)
				return;
			string key = cboLineTarget.SelectedItem.ToString();
			Character c = CharacterDatabase.Get(key);
			PopulateStageCombo(cboTargetStage, c, true);
			PopulateStageCombo(cboTargetToStage, c, true);
			markerTarget.SetDataSource(c, false);
			PopulateMarkerCombo(cboTargetNotMarker, c, false);
		}

		private void cboAlsoPlaying_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_populatingCase)
				return;
			string key = cboAlsoPlaying.SelectedItem.ToString();
			Character c = CharacterDatabase.Get(key);
			PopulateStageCombo(cboAlsoPlayingStage, c, false);
			PopulateStageCombo(cboAlsoPlayingMaxStage, c, false);
			markerAlsoPlaying.SetDataSource(c, false);
			PopulateMarkerCombo(cboAlsoPlayingNotMarker, c, false);
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
		
		private void ckbShowSpeechBubbleColumns_CheckedChanged(object sender, EventArgs e)
		{
			this.gridDialogue.ShowSpeechBubbleColumns = ckbShowBubbleColumns.Checked;
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
		private bool Export()
		{
			if (_character == null)
				return true;
			Save();
			if (Serialization.ExportCharacter(_character))
			{
				Shell.Instance.SetStatus(string.Format("{0} exported successfully at {1}.", _character, DateTime.Now.ToShortTimeString()));
				return true;
			}
			else
			{
				Shell.Instance.SetStatus(string.Format("{0} failed to export.", _character));
				return false;
			}
		}

		/// <summary>
		/// Populates fields that list cross-character data (names, tags, etc.)
		/// </summary>
		private void PopulateListingFields()
		{
			List<string> items = new List<string>();
			items.Add("");
			foreach (var character in CharacterDatabase.Characters)
			{
				items.Add(character.FolderName);
			}
			items.Sort();
			cboAlsoPlaying.DataSource = items;
			cboAlsoPlaying.BindingContext = new BindingContext();

			List<object> filters = new List<object>();
			List<string> tags = new List<string>();
			foreach (var tag in TagDatabase.Tags)
			{
				filters.Add(tag);
				tags.Add(tag.Value);
			}
			filters.Add("");
			filters.Add("human");
			filters.Add("human_male");
			filters.Add("human_female");
			filters.Sort((i1, i2) => { return i1.ToString().CompareTo(i2.ToString()); });
			tags.Add("");
			tags.Add("human");
			tags.Add("human_male");
			tags.Add("human_female");
			tags.Sort();
			cboLineFilter.DataSource = filters;
			cboLineFilter.BindingContext = new BindingContext();

			((DataGridViewComboBoxColumn)gridFilters.Columns["ColStatusFilter"]).DataSource = TargetCondition.StatusTypes;
			((DataGridViewComboBoxColumn)gridFilters.Columns["ColStatusFilter"]).ValueMember = "Key";
			((DataGridViewComboBoxColumn)gridFilters.Columns["ColStatusFilter"]).DisplayMember = "Value";
			cboTargetStatus.DataSource = TargetCondition.StatusTypes;
			cboTargetStatus.ValueMember = "Key";
			cboTargetStatus.DisplayMember = "Value";

			DataGridViewComboBoxColumn gridCol = gridFilters.Columns["ColTagFilter"] as DataGridViewComboBoxColumn;
			if (gridCol != null)
			{
				foreach (var tag in tags)
				{
					gridCol.Items.Add(tag);
				}
			}
		}

		/// <summary>
		/// Populates the target field with loaded characters of the appropriate gender for the current case
		/// </summary>
		private void PopulateTargetField()
		{
			cboLineTarget.Items.Clear();
			List<string> items = new List<string>();
			items.Add("");

			Trigger trigger = TriggerDatabase.GetTrigger(_selectedCase?.Tag);
			string gender = trigger?.Gender;
			bool useGender = !string.IsNullOrEmpty(gender);
			string size = trigger?.Size;
			bool useSize = !string.IsNullOrEmpty(size);

			foreach (var character in CharacterDatabase.Characters)
			{
				if ((!useGender || gender == character.Gender) && (!useSize || size == character.Size))
				{
					items.Add(character.FolderName);
				}
			}
			items.Add("human");
			items.Sort();
			cboLineTarget.Items.Clear();
			foreach (string item in items)
			{
				cboLineTarget.Items.Add(item);
			}
		}

		/// <summary>
		/// Updates a marker dropdown to contain only markers used in the given character's dialogue
		/// </summary>
		/// <param name="box"></param>
		/// <param name="character"></param>
		private void PopulateMarkerCombo(ComboBox box, Character character, bool allowPrivate)
		{
			string oldText = box.Text;
			box.Items.Clear();
			box.Text = "";
			if (character == null)
				return;

			foreach (var marker in character.Markers.Values)
			{
				if (allowPrivate || marker.Scope == MarkerScope.Public)
				{
					box.Items.Add(marker.Name);
				}
			}

			if (!string.IsNullOrEmpty(oldText))
			{
				box.Text = oldText;
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
			string minStage, maxStage;
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
			lblAvailableVars.Text = string.Format("Variables: {0}", string.Join(" ", vars));

			#endregion

			if (!_usingOldEditor)
			{
				if (caseTrigger.HasTarget)
				{
					tableConditions.RecordFilter = null;
				}
				else
				{
					tableConditions.RecordFilter = FilterTargets;
				}
				tableConditions.Data = _selectedCase;
			}
			else
			{
				PopulateTargetField();

				#region Target tab
				ClearConditionFields();
				if (caseTrigger.HasTarget)
				{
					((Control)tabTarget).Enabled = true;
					GUIHelper.SetComboBox(cboLineTarget, _selectedCase.Target);
					GUIHelper.SetComboBox(cboTargetHand, _selectedCase.TargetHand);
					GUIHelper.SetComboBox(cboLineFilter, _selectedCase.Filter);
					GUIHelper.SetComboBox(cboTargetStatus, _selectedCase.TargetStatusType);
					ckbTargetStatusNegated.Checked = _selectedCase.NegateTargetStatus;
					Character target = CharacterDatabase.Get(_selectedCase.Target);
					_selectedCase.SplitTargetStage(out minStage, out maxStage);
					PopulateStageCombo(cboTargetStage, target, true);
					SetStageComboBox(cboTargetStage, minStage);
					PopulateStageCombo(cboTargetToStage, target, true);
					SetStageComboBox(cboTargetToStage, maxStage);
					markerTarget.SetDataSource(target, false);
					markerTarget.Value = _selectedCase.TargetSaidMarker;
					PopulateMarkerCombo(cboTargetNotMarker, target, false);
					cboTargetNotMarker.Text = _selectedCase.TargetNotSaidMarker;
					GUIHelper.SetRange(valTimeInStage, valMaxTimeInStage, _selectedCase.TargetTimeInStage);
					GUIHelper.SetRange(valLosses, valMaxLosses, _selectedCase.ConsecutiveLosses);
					GUIHelper.SetRange(valLayers, valMaxLayers, _selectedCase.TargetLayers);
					GUIHelper.SetRange(valStartingLayers, valMaxStartingLayers, _selectedCase.TargetStartingLayers);
					valOwnLosses.Enabled = false;
					valMaxOwnLosses.Enabled = false;
				}
				else
				{
					((Control)tabTarget).Enabled = false;
					GUIHelper.SetComboBox(cboLineTarget, "");
					cboTargetStage.Text = "";
					cboTargetToStage.Text = "";
					markerTarget.Value = null;
					cboTargetNotMarker.Text = "";
					GUIHelper.SetComboBox(cboTargetHand, "");
					GUIHelper.SetComboBox(cboLineFilter, "");
					valOwnLosses.Enabled = true;
					valMaxOwnLosses.Enabled = true;
				}
				#endregion

				#region Also Playing tab
				GUIHelper.SetComboBox(cboAlsoPlaying, _selectedCase.AlsoPlaying);
				GUIHelper.SetComboBox(cboAlsoPlayingHand, _selectedCase.AlsoPlayingHand);
				Character other = CharacterDatabase.Get(_selectedCase.AlsoPlaying);
				cboAlsoPlayingStage.Text = "";
				cboAlsoPlayingMaxStage.Text = "";
				GUIHelper.SetRange(valAlsoTimeInStage, valMaxAlsoTimeInStage, _selectedCase.AlsoPlayingTimeInStage);

				_selectedCase.SplitAlsoPlayingStage(out minStage, out maxStage);
				PopulateStageCombo(cboAlsoPlayingStage, other, false);
				SetStageComboBox(cboAlsoPlayingStage, minStage);
				PopulateStageCombo(cboAlsoPlayingMaxStage, other, false);
				SetStageComboBox(cboAlsoPlayingMaxStage, maxStage);
				markerAlsoPlaying.SetDataSource(other, false);
				markerAlsoPlaying.Value = _selectedCase.AlsoPlayingSaidMarker;
				PopulateMarkerCombo(cboAlsoPlayingNotMarker, other, false);
				cboAlsoPlayingNotMarker.Text = _selectedCase.AlsoPlayingNotSaidMarker;
				#endregion

				#region Self tab
				cboOwnHand.SelectedItem = _selectedCase.HasHand;
				GUIHelper.SetRange(valOwnLosses, valMaxOwnLosses, _selectedCase.ConsecutiveLosses);
				GUIHelper.SetRange(valOwnTimeInStage, valMaxOwnTimeInStage, _selectedCase.TimeInStage);
				markerSelf.SetDataSource(_character, true);
				PopulateMarkerCombo(cboNotMarker, _character, true);
				markerSelf.Value = _selectedCase.SaidMarker;
				cboNotMarker.Text = _selectedCase.NotSaidMarker;
				#endregion

				#region Misc tab
				GUIHelper.SetRange(cboTotalFemales, cboMaxTotalFemales, _selectedCase.TotalFemales);
				GUIHelper.SetRange(cboTotalMales, cboMaxTotalMales, _selectedCase.TotalMales);
				GUIHelper.SetRange(valGameRounds, valMaxGameRounds, _selectedCase.TotalRounds);
				GUIHelper.SetRange(cboTotalPlaying, cboMaxTotalPlaying, _selectedCase.TotalPlaying);
				GUIHelper.SetRange(cboTotalExposed, cboMaxTotalExposed, _selectedCase.TotalExposed);
				GUIHelper.SetRange(cboTotalNaked, cboMaxTotalNaked, _selectedCase.TotalNaked);
				GUIHelper.SetRange(cboTotalFinishing, cboMaxTotalFinishing, _selectedCase.TotalMasturbating);
				GUIHelper.SetRange(cboTotalFinished, cboMaxTotalFinished, _selectedCase.TotalFinished);
				#endregion

				#region Tags tab
				LoadFilterConditions();
				#endregion
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

		private void ClearConditionFields()
		{
			foreach (TabPage page in tabControlConditions.TabPages)
			{
				foreach (Control ctl in page.Controls)
				{
					if (ctl is TextBox || ctl is NumericUpDown)
						ctl.Text = "";
					else if (ctl is ComboBox)
					{
						ComboBox box = ctl as ComboBox;
						box.SelectedIndex = -1;
						box.Text = "";
					}
				}
			}
		}

		/// <summary>
		/// Updates a stage-specific dropdown to have display friendly options specific to the character being targeted
		/// </summary>
		/// <param name="box"></param>
		/// <param name="character"></param>
		/// <param name="useLookForward">If true, for removed/removing cases, only valid stages will be provided</param>
		private void PopulateStageCombo(ComboBox box, Character character, bool filterStages)
		{
			string oldText = box.Text;
			box.Items.Clear();
			box.Text = "";

			string tag = _selectedCase?.Tag;
			string filterType = null;
			bool removing = false;
			bool removed = false;
			bool lookForward = false;
			if (tag != null && filterStages)
			{
				removing = tag.Contains("removing_");
				removed = tag.Contains("removed_");
				lookForward = removing;
				if (removing || removed)
				{
					int index = tag.LastIndexOf('_');
					if (index >= 0 && index < tag.Length - 1)
					{
						filterType = tag.Substring(index + 1);
						if (filterType == "accessory")
							filterType = "extra";
					}
				}
			}

			if (character == null)
			{
				//If the character is not valid, still allow something but there's no way to give a useful name to it
				for (int i = 0; i < 8 + Clothing.ExtraStages; i++)
				{
					box.Items.Add(i);
				}
			}
			else
			{
				for (int i = 0; i < character.Layers + Clothing.ExtraStages; i++)
				{
					if (filterStages)
					{
						if (filterType != null)
						{
							//Filter out stages that will never be valid
							if (i >= 0 && i <= character.Layers)
							{
								int layer = removed ? i - 1 : i;
								if (layer < 0 || layer >= character.Layers)
									continue;

								Clothing clothing = character.Wardrobe[character.Layers - layer - 1];
								string realType = clothing.Type;
								if (filterType != realType.ToLower())
									continue;
							}
							else continue;
						}
					}
					box.Items.Add(character.LayerToStageName(i, lookForward));
				}
				if (!string.IsNullOrEmpty(oldText))
				{
					box.Text = oldText;
				}
			}
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

		/// <summary>
		/// Sets a stage target box to the given stage
		/// </summary>
		/// <param name="box"></param>
		/// <param name="stage"></param>
		private void SetStageComboBox(ComboBox box, string stage)
		{
			for (int i = 0; i < box.Items.Count; i++)
			{
				StageName stageName = box.Items[i] as StageName;
				if (stageName != null && stageName.Id == stage)
				{
					box.SelectedIndex = i;
					return;
				}
			}

			box.Text = stage; //If couldn't set an object, just set the text
		}

		/// <summary>
		/// Reads the value from a stage dropdown
		/// </summary>
		/// <param name="box"></param>
		/// <returns></returns>
		private string ReadStageComboBox(ComboBox box)
		{
			StageName stage = box.SelectedItem as StageName;
			if (stage == null)
			{
				//Must be a generic stage
				return box.Text;
			}

			return stage.Id;
		}

		/// <summary>
		/// Populates the Filters grid
		/// </summary>
		private void LoadFilterConditions()
		{
			if (_selectedCase == null)
				return;
			gridFilters.Rows.Clear();
			DataGridViewComboBoxColumn colTags = gridFilters.Columns["ColTagFilter"] as DataGridViewComboBoxColumn;
			foreach (TargetCondition condition in _selectedCase.Conditions)
			{
				if (string.IsNullOrEmpty(condition.Filter))
					continue;
				DataGridViewRow row = gridFilters.Rows[gridFilters.Rows.Add()];
				if (!colTags.Items.Contains(condition.Filter))
				{
					colTags.Items.Add(condition.Filter);
				}
				try
				{
					row.Cells["ColTagFilter"].Value = condition.Filter;
					row.Cells["ColGenderFilter"].Value = condition.Gender;
					row.Cells["ColStatusFilter"].Value = condition.StatusType;
					row.Cells["ColStatusFilterNegated"].Value = condition.NegateStatus;
					row.Cells["ColFilterCount"].Value = condition.Count;
				}
				catch { }
			}
		}

		/// <summary>
		/// Saves filter conditions into the case
		/// </summary>
		private void SaveFilterConditions()
		{
			if (_selectedCase == null)
				return;
			_selectedCase.Conditions.Clear();
			for (int i = 0; i < gridFilters.Rows.Count; i++)
			{
				DataGridViewRow row = gridFilters.Rows[i];
				string filter = row.Cells["ColTagFilter"].Value?.ToString();
				string countValue = row.Cells["ColFilterCount"].Value?.ToString();
				string gender = row.Cells["ColGenderFilter"].Value?.ToString();
				string status = row.Cells["ColStatusFilter"].Value?.ToString();
				object negStatus = row.Cells["ColStatusFilterNegated"].Value;

				if (string.IsNullOrEmpty(filter) || string.IsNullOrEmpty(countValue))
					continue;
				TargetCondition condition = new TargetCondition(filter, gender, status, negStatus != null && (bool)negStatus, countValue);
				_selectedCase.Conditions.Add(condition);
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

			bool needRegeneration = false;
			var c = _selectedCase;
			if (c.Tag != Trigger.StartTrigger)
			{
				string newTag = GUIHelper.ReadComboBox(cboCaseTags);
				if (newTag != c.Tag)
					needRegeneration = true;
				c.Tag = newTag;
				Trigger trigger = TriggerDatabase.GetTrigger(newTag);

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

				if (!_usingOldEditor)
				{
					tableConditions.Save();
				}
				else
				{
					#region Target tab
					if (trigger.HasTarget)
					{
						c.Target = GUIHelper.ReadComboBox(cboLineTarget);
						c.SetTargetStage(ReadStageComboBox(cboTargetStage), ReadStageComboBox(cboTargetToStage));
						c.TargetHand = GUIHelper.ReadComboBox(cboTargetHand);
						c.Filter = GUIHelper.ReadComboBox(cboLineFilter);
						c.TargetTimeInStage = GUIHelper.ReadRange(valTimeInStage, valMaxTimeInStage);
						c.ConsecutiveLosses = GUIHelper.ReadRange(valLosses, valMaxLosses);
						c.TargetLayers = GUIHelper.ReadRange(valLayers, valMaxLayers);
						c.TargetStartingLayers = GUIHelper.ReadRange(valStartingLayers, valMaxStartingLayers);
						c.TargetStatusType = GUIHelper.ReadComboBox(cboTargetStatus);
						c.NegateTargetStatus = ckbTargetStatusNegated.Checked;
						c.TargetSaidMarker = markerTarget.Value;
						c.TargetNotSaidMarker = GUIHelper.ReadComboBox(cboTargetNotMarker);
					}
					else
					{
						c.Target = null;
						c.TargetStage = null;
						c.TargetLayers = null;
						c.TargetStatus = null;
						c.NegateTargetStatus = false;
						c.TargetHand = null;
						c.Filter = null;
						c.TargetTimeInStage = null;
						c.TargetSaidMarker = null;
						c.TargetNotSaidMarker = null;
					}
					#endregion

					#region Also Playing Tab
					c.AlsoPlaying = GUIHelper.ReadComboBox(cboAlsoPlaying);
					c.AlsoPlayingHand = GUIHelper.ReadComboBox(cboAlsoPlayingHand);
					c.SetAlsoPlayingStage(ReadStageComboBox(cboAlsoPlayingStage), ReadStageComboBox(cboAlsoPlayingMaxStage));
					c.AlsoPlayingTimeInStage = GUIHelper.ReadRange(valAlsoTimeInStage, valMaxAlsoTimeInStage);
					c.AlsoPlayingSaidMarker = markerAlsoPlaying.Value;
					c.AlsoPlayingNotSaidMarker = GUIHelper.ReadComboBox(cboAlsoPlayingNotMarker);
					#endregion

					#region Self tab
					c.SaidMarker = markerSelf.Value;
					c.NotSaidMarker = GUIHelper.ReadComboBox(cboNotMarker);
					c.HasHand = GUIHelper.ReadComboBox(cboOwnHand);
					c.TimeInStage = GUIHelper.ReadRange(valOwnTimeInStage, valMaxOwnTimeInStage);
					if (!trigger.HasTarget)
					{
						c.ConsecutiveLosses = GUIHelper.ReadRange(valOwnLosses, valMaxOwnLosses);
					}
					#endregion

					#region Misc tab
					c.TotalFemales = GUIHelper.ReadRange(cboTotalFemales, cboMaxTotalFemales);
					c.TotalMales = GUIHelper.ReadRange(cboTotalMales, cboMaxTotalMales);
					c.TotalRounds = GUIHelper.ReadRange(valGameRounds, valMaxGameRounds);
					c.TotalPlaying = GUIHelper.ReadRange(cboTotalPlaying, cboMaxTotalPlaying);
					c.TotalExposed = GUIHelper.ReadRange(cboTotalExposed, cboMaxTotalExposed);
					c.TotalNaked = GUIHelper.ReadRange(cboTotalNaked, cboMaxTotalNaked);
					c.TotalMasturbating = GUIHelper.ReadRange(cboTotalFinishing, cboMaxTotalFinishing);
					c.TotalFinished = GUIHelper.ReadRange(cboTotalFinished, cboMaxTotalFinished);
					#endregion

					#region Tags tab
					SaveFilterConditions();
					#endregion
				}

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
				image = DialogueLine.GetDefaultImage(image);
				img = _imageLibrary.Find(stage + "-" + image);
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
				if (responder == _character)
				{
					if (MessageBox.Show("Do you really want to respond to your own lines?", "Make Response", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					{
						return;
					}
				}

				Case response = _selectedCase.CreateResponse(_character, responder);
				if (response == null)
				{
					MessageBox.Show("Couldn't create a response based on this case's conditions.", "Make Response", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				responder.PrepareForEdit();
				responder.Behavior.AddWorkingCase(response);
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

		private void cmdToggleMode_Click(object sender, EventArgs e)
		{
			if (_selectedCase != null)
			{
				SaveCase();
			}
			_usingOldEditor = !_usingOldEditor;
			Config.Set(UseOldEditorSetting, _usingOldEditor);
			EnableOldEditor(_usingOldEditor);
			if (_selectedCase != null)
			{
				PopulateCase();
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
	}
}

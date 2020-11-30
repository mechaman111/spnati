using Desktop;
using Desktop.CommonControls;
using Desktop.Skinning;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CaseControl : UserControl, IMacroEditor, ISkinnedPanel, ISkinControl
	{
		private const string FavoriteConditionsSetting = "FavoritedConditions";

		private Character _character;
		private CharacterEditorData _editorData;
		private Case _selectedCase;
		private Stage _selectedStage;
		private bool _populatingCase;
		private List<DialogueLine> _lineClipboard = new List<DialogueLine>();
		private Case _trackedCase;

		public event EventHandler<DialogueLine> TextUpdated;
		public event EventHandler<int> HighlightRow;

		public CaseControl()
		{
			InitializeComponent();
		}

		private void UpdateAddCaption()
		{
			if (tabsConditions.TabCount > 1)
			{
				stripConditions.AddCaption = "OR";
			}
			else
			{
				stripConditions.AddCaption = "AND";
			}
		}

		public int PreviewStage
		{
			get { return _selectedStage == null ? _selectedCase.Stages[0] : _selectedStage.Id; }
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.Background.GetColor(VisualState.Normal, false, Enabled);
		}

		private void CaseControl_Load(object sender, EventArgs e)
		{
			tableConditions.RecordFilter = FilterTargets;
			lstAddTags.RecordType = typeof(Tag);
			lstRemoveTags.RecordType = typeof(Tag);
			gridStages.CheckedChanged += Check_CheckedChanged;
			gridStages.LayerSelected += GridStages_LayerSelected;
			gridDialogue.TextUpdated += GridDialogue_TextUpdated;
		}

		public void Activate()
		{
			CreateStageCheckboxes();
		}

		public void SetCharacter(Character character)
		{
			_character = character;
			_editorData = CharacterDatabase.GetEditorData(_character);
			tableConditions.Context = character;
			CreateStageCheckboxes();
		}

		public Case GetCase()
		{
			return _selectedCase;
		}

		public void SetCase(Stage stage, Case workingCase)
		{
			if (_selectedCase != null)
			{
				tabsConditions.SelectedIndexChanged -= tabsConditions_SelectedIndexChanged;
				tabsConditions.SelectedIndex = 0;
				for (int i = tabsConditions.TabPages.Count - 1; i > 0; i--)
				{
					tabsConditions.TabPages.RemoveAt(i);
				}
			}
			_selectedStage = stage;
			_selectedCase = workingCase;
			if (_selectedCase != null)
			{
				DataConversions.ConvertCase5_2(_selectedCase);
				DataConversions.ConvertCase5_8(_selectedCase, _character);
			}
			TrackCase(_selectedCase);
			if (_selectedCase != null)
			{
				tabConditions.Enabled = true;
				for (int i = 0; i < _selectedCase.AlternativeConditions.Count; i++)
				{
					AddAlternateTab();
				}
				tabsConditions.SelectedIndexChanged += tabsConditions_SelectedIndexChanged;
			}
			PopulateCase();
		}

		private void CasePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_character.IsDirty = true;
		}

		public void UpdateStages()
		{
			if (_character == null) { return; }
			CreateStageCheckboxes();
			PopulateStageCheckboxes();
		}

		public void UpdateMacros()
		{
			tableConditions.AddMacros();
		}

		public bool AutoOpenConditions
		{
			set { tableConditions.RunInitialAddEvents = value; }
		}

		private void GridDialogue_TextUpdated(object sender, int e)
		{
			TextUpdated?.Invoke(this, gridDialogue.GetLine(e));
		}

		public void SaveFavorites()
		{
			//Update favorite conditions
			List<string> favorites = tableConditions.GetFavorites();
			Config.Set(FavoriteConditionsSetting, string.Join("|", favorites));
		}

		public void Save()
		{
			SaveCase();
		}


		#region Event handlers
		/// <summary>
		/// Raised when a Stage checkbox is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Check_CheckedChanged(object sender, int stage)
		{
			if (_populatingCase)
				return;
			_populatingCase = true;
			HashSet<int> stages = GetSelectedStages();
			_selectedCase.ClearStages();
			_selectedCase.AddStages(stages);
			UpdatePreviewStage(stages, -1);
			_populatingCase = false;
		}

		private void GridStages_LayerSelected(object sender, int layer)
		{
			UpdatePreviewStage(GetSelectedStages(), layer);
		}

		private void UpdatePreviewStage(HashSet<int> stages, int desiredStage)
		{
			if (_selectedStage != null && stages.Count > 0)
			{
				if (desiredStage == -1)
				{
					desiredStage = stages.Min();
				}
				_selectedStage = new Stage(desiredStage);
				gridDialogue.SetStage(_selectedStage, stages);
				gridStages.SetPreviewStage(_selectedStage.Id);
			}
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
					gridDialogue.ClearLines();
				}
			}
			gridDialogue.PasteLines(_lineClipboard);
		}

		private void gridDialogue_HighlightRow(object sender, int index)
		{
			HighlightRow?.Invoke(this, index);
		}

		private void gridDialogue_KeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown(e);
		}
		#endregion

		public bool FindReplace(FindArgs args)
		{
			return gridDialogue.FindReplace(args);
		}

		public PoseMapping GetImage(int index)
		{
			return gridDialogue.GetImage(index);
		}
		public DialogueLine GetLine(int index)
		{
			return gridDialogue.GetLine(index);
		}

		public void ClearSelection()
		{
			gridDialogue.ClearSelection();
		}

		public int FindText(string text, int startIndex, FindArgs args)
		{
			return gridDialogue.FindText(text, startIndex, args);
		}

		public void SelectTextInRow(int rowIndex, int startIndex, int length, bool selectMarker)
		{
			gridDialogue.SelectTextInRow(rowIndex, startIndex, length, selectMarker);
		}

		/// <summary>
		/// Loads the newly selected case into the dialogue fields
		/// </summary>
		private void PopulateCase()
		{
			if (_selectedCase == null)
			{
				return;
			}
			_populatingCase = true;
			Case stageCase = _selectedCase;

			PopulateStageCheckboxes();

			TriggerDefinition caseTrigger = TriggerDatabase.GetTrigger(stageCase.Tag);

			#region Case-wide settings
			//Tag combo box
			cboCaseTags.Items.Clear();
			TriggerDefinition currentTrigger = TriggerDatabase.GetTrigger(_selectedCase.Tag);
			if (_selectedStage != null)
			{
				TriggerDefinition selection = null;
				foreach (string tag in TriggerDatabase.GetTags())
				{
					TriggerDefinition t = TriggerDatabase.GetTrigger(tag);
					if (currentTrigger.HasTarget && currentTrigger.HasTarget != t.HasTarget)
					{
						continue;
					}
					if (tag == _selectedCase.Tag)
						selection = t;
					cboCaseTags.Items.Add(t);
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
			if (!string.IsNullOrEmpty(txtNotes.Text))
			{
				stripTabs.SetHighlight(tabNotes, DataHighlight.Important);
			}
			else
			{
				stripTabs.SetHighlight(tabNotes, DataHighlight.Normal);
			}
			CaseLabel label = _editorData.GetLabel(_selectedCase);
			txtFolder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			txtFolder.AutoCompleteSource = AutoCompleteSource.CustomSource;
			txtFolder.AutoCompleteCustomSource = _editorData.Folders;
			if (label == null)
			{
				txtLabel.Text = null;
				txtFolder.Text = null;
				SetColorButton(null);
			}
			else
			{
				txtLabel.Text = label.Text;
				txtFolder.Text = label.Folder;
				SetColorButton(label.ColorCode);
			}

			if (caseTrigger.HasTarget)
			{
				tableConditions.RecordFilter = null;
			}
			else
			{
				tableConditions.RecordFilter = FilterTargets;
			}
			bool firstPopulation = (tableConditions.Data == null);
			PopulateConditionTable(_selectedCase);

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
			chkBackground.Checked = _selectedCase.Hidden == "1";
			chkPlayOnce.Checked = _selectedCase.OneShotId > 0;

			var stages = GetSelectedStages();
			gridDialogue.SetData(_character, _selectedStage, _selectedCase, stages);
			UpdateAddCaption();

			PopulateTagsTab();

			_populatingCase = false;
		}

		private void PopulateConditionTable(Case workingCase)
		{
			if (tableConditions.Data != workingCase)
			{
				tableConditions.Data = workingCase;
				AddSpeedButtons(tableConditions, workingCase?.Tag);
			}
		}

		public static void AddSpeedButtons(PropertyTable table, string tag)
		{
			if (tag == null) { return; }
			TriggerDefinition caseTrigger = TriggerDatabase.GetTrigger(tag);

			//Self
			table.AddSpeedButton("Self", "Said Marker", (data) => { return AddFilter("self", data, "SaidMarker"); });
			table.AddSpeedButton("Self", "Not Said Marker", (data) => { return AddFilter("self", data, "NotSaidMarker"); });
			table.AddSpeedButton("Self", "Time in Stage", (data) => { return AddFilter("self", data, "TimeInStage"); });
			table.AddSpeedButton("Self", "Has Hand", (data) => { return AddFilter("self", data, "Hand"); });

			//Table
			table.AddSpeedButton("Table", "Total Females", (data) => { return AddGenderFilter(data, "female"); });
			table.AddSpeedButton("Table", "Total Males", (data) => { return AddGenderFilter(data, "male"); });
			table.AddSpeedButton("Table", "# Players Still in Game", (data) => { return AddStatusFilter(data, "alive"); });
			table.AddSpeedButton("Table", "# Players Exposed", (data) => { return AddStatusFilter(data, "exposed"); });
			table.AddSpeedButton("Table", "# Players Naked", (data) => { return AddStatusFilter(data, "naked"); });
			table.AddSpeedButton("Table", "# Players Masturbating", (data) => { return AddStatusFilter(data, "masturbating"); });
			table.AddSpeedButton("Table", "# Players Finished", (data) => { return AddStatusFilter(data, "finished"); });
			table.AddSpeedButton("Table", "Human Name", (data) => { return AddVariableTest("~player~", data); });

			//Also Playing
			table.AddSpeedButton("Also Playing", "Also Playing", (data) => { return AddFilter("other", data); });
			table.AddSpeedButton("Also Playing", "Also Playing Stage", (data) => { return AddFilter("other", data, "Stage"); });
			table.AddSpeedButton("Also Playing", "Also Playing Said Marker", (data) => { return AddFilter("other", data, "SaidMarker"); });
			table.AddSpeedButton("Also Playing", "Also Playing Not Said Marker", (data) => { return AddFilter("other", data, "NotSaidMarker"); });
			table.AddSpeedButton("Also Playing", "Also Playing Saying Marker", (data) => { return AddFilter("other", data, "SayingMarker"); });
			table.AddSpeedButton("Also Playing", "Also Playing Saying Text", (data) => { return AddFilter("other", data, "Saying"); });
			table.AddSpeedButton("Also Playing", "Also Playing Time in Stage", (data) => { return AddFilter("other", data, "TimeInStage"); });
			table.AddSpeedButton("Also Playing", "Also Playing Hand", (data) => { return AddFilter("other", data, "Hand"); });
			table.AddSpeedButton("Also Playing", "Also Playing Pose", (data) => { return AddFilter("other", data, "Pose"); });

			//Game-wide
			table.AddSpeedButton("Game", "Background", (data) => { return AddVariableTest("~background~", data); });
			table.AddSpeedButton("Game", "Month", (data) => { return AddVariableTest("~month.number~", data); });
			table.AddSpeedButton("Game", "Day", (data) => { return AddVariableTest("~day.number~", data); });
			table.AddSpeedButton("Game", "Weekday", (data) => { return AddVariableTest("~weekday.number~", data); });

			//Filters
			table.AddSpeedButton("Filter", "Anyone", (data) => { return AddFilter("", data); });
			if (caseTrigger.HasTarget)
			{
				table.AddSpeedButton("Filter", "Target", (data) => { return AddFilter("target", data); });
			}
			table.AddSpeedButton("Filter", "Self", (data) => { return AddFilter("self", data); });
			table.AddSpeedButton("Filter", "Also Playing", (data) => { return AddFilter("other", data); });
			table.AddSpeedButton("Filter", "Opponent", (data) => { return AddFilter("opp", data); });
			table.AddSpeedButton("Filter", "Winner", (data) => { return AddFilter("winner", data); });
			table.AddSpeedButton("Filter", "Specific Character", (data) =>
			{
				Character character = RecordLookup.DoLookup(typeof(Character), "", false, null) as Character;
				return AddFilter("", data, null, character);
			});

			//Player variables

			table.AddSpeedButton("Player", "Collectible", (data) => { return AddVariableTest("~_.collectible.*~", data); });
			table.AddSpeedButton("Player", "Collectible (Counter)", (data) => { return AddVariableTest("~_.collectible.*.counter~", data); });
			table.AddSpeedButton("Player", "Costume", (data) => { return AddVariableTest("~_.costume~", data); });
			table.AddSpeedButton("Player", "Distance", (data) => { return AddVariableTest("~_.distance~", data); });
			table.AddSpeedButton("Player", "Gender", (data) => { return AddVariableTest("~_.gender~", data); });
			table.AddSpeedButton("Player", "Hand Quality", (data) => { return AddVariableTest("~_.hand.score~", data); });
			table.AddSpeedButton("Player", "Largest Lead", (data) => { return AddVariableTest("~_.biggestlead~", data); });
			table.AddSpeedButton("Player", "Layer Difference", (data) => { return AddVariableTest("~_.diff~", data); });
			table.AddSpeedButton("Player", "Marker", (data) => { return AddVariableTest("~_.marker.*~", data); });
			table.AddSpeedButton("Player", "Marker (Persistent)", (data) => { return AddVariableTest("~_.persistent.*~", data); });
			table.AddSpeedButton("Player", "Place", (data) => { return AddVariableTest("~_.lead~", data); });
			table.AddSpeedButton("Player", "Relative Position", (data) => { return AddVariableTest("~_.position~", data); });
			table.AddSpeedButton("Player", "Size", (data) => { return AddVariableTest("~_.size~", data); });
			table.AddSpeedButton("Player", "Slot", (data) => { return AddVariableTest("~_.slot~", data); });
			table.AddSpeedButton("Player", "Stage", (data) => { return AddVariableTest("~_.stage~", data); });
			table.AddSpeedButton("Player", "Tag", (data) => { return AddVariableTest("~_.tag.*~", data); });

			//Table-wide
			if (caseTrigger.AvailableVariables.Contains("cards"))
			{
				table.AddSpeedButton("Self", "Cards Exchanged", (data) => { return AddVariableTest("~cards~", data); });
			}
			if (caseTrigger.HasTarget)
			{
				//Target
				table.AddSpeedButton("Target", "Target", (data) => { return AddFilter("target", data); });
				table.AddSpeedButton("Target", "Target Tag", (data) => { return AddFilter("target", data, "FilterTag"); });
				table.AddSpeedButton("Target", "Target Stage", (data) => { return AddFilter("target", data, "Stage"); });
				table.AddSpeedButton("Target", "Target Said Marker", (data) => { return AddFilter("target", data, "SaidMarker"); });
				table.AddSpeedButton("Target", "Target Not Said Marker", (data) => { return AddFilter("target", data, "NotSaidMarker"); });
				table.AddSpeedButton("Target", "Target Saying Marker", (data) => { return AddFilter("target", data, "SayingMarker"); });
				table.AddSpeedButton("Target", "Target Saying Text", (data) => { return AddFilter("target", data, "Saying"); });
				table.AddSpeedButton("Target", "Target Time in Stage", (data) => { return AddFilter("target", data, "TimeInStage"); });
				table.AddSpeedButton("Target", "Target Hand", (data) => { return AddFilter("target", data, "Hand"); });
				table.AddSpeedButton("Target", "Target Status", (data) => { return AddFilter("target", data, "Status"); });
				table.AddSpeedButton("Target", "Target Layers", (data) => { return AddFilter("target", data, "Layers"); });
				table.AddSpeedButton("Target", "Target Starting Layers", (data) => { return AddFilter("target", data, "StartingLayers"); });
				table.AddSpeedButton("Target", "Target Pose", (data) => { return AddFilter("target", data, "Pose"); });

				//Clothing
				if (caseTrigger.AvailableVariables.Contains("clothing"))
				{
					table.AddSpeedButton("Clothing", "Clothing Position", (data) => { return AddVariableTest("~clothing.position~", data); });
					table.AddSpeedButton("Clothing", "Clothing Type", (data) => { return AddVariableTest("~clothing.type~", data); });
					table.AddSpeedButton("Clothing", "Clothing Category", (data) => { return AddVariableTest("~clothing.generic~", data); });
				}
			}
		}

		private static SpeedButtonData AddVariableTest(string variable, object data)
		{
			Case theCase = data as Case;
			theCase.Expressions.Add(new ExpressionTest(variable, ""));
			return new SpeedButtonData("Expressions");
		}

		private static SpeedButtonData AddFilter(string role, object data, string subproperty = null, Character character = null)
		{
			Case theCase = data as Case;
			TargetCondition condition = theCase.Conditions.Find(c => c.Role == role && (character == null || c.Character == character.FolderName));
			if (subproperty == null)
			{
				condition = null; //always create new filters for roles
			}
			if (condition == null)
			{
				condition = new TargetCondition();
				condition.Role = role;
				theCase.Conditions.Add(condition);
				if (character != null)
				{
					condition.Character = character.FolderName;
				}
			}
			return new SpeedButtonData("Conditions", subproperty) { ListItem = condition };
		}

		private static SpeedButtonData AddGenderFilter(object data, string gender)
		{
			Case theCase = data as Case;
			TargetCondition condition = new TargetCondition();
			condition.Gender = gender;
			theCase.Conditions.Add(condition);
			return new SpeedButtonData("Conditions") { ListItem = condition };
		}

		private static SpeedButtonData AddStatusFilter(object data, string status)
		{
			Case theCase = data as Case;
			TargetCondition condition = new TargetCondition();
			condition.Status = status;
			theCase.Conditions.Add(condition);
			return new SpeedButtonData("Conditions") { ListItem = condition };
		}

		private void PopulateTagsTab()
		{
			lstAddTags.Clear();
			lstRemoveTags.Clear();
			string[] addTags = (_selectedCase.AddCharacterTags ?? "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			string[] removeTags = (_selectedCase.RemoveCharacterTags ?? "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			lstAddTags.SelectedItems = addTags;
			lstRemoveTags.SelectedItems = removeTags;
		}

		private HashSet<int> GetSelectedStages()
		{
			HashSet<int> selectedStages = new HashSet<int>();
			for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
			{
				if (gridStages.GetChecked(i))
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
			gridStages.SetData(_character, null, -1);
		}

		private bool FilterTargets(PropertyRecord record)
		{
			if (record == null)
			{
				return false;
			}
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
		private void SaveCase()
		{
			if (_selectedCase == null)
			{
				return;
			}

			SaveNotes();
			var c = _selectedCase;
			if (c.Tag != TriggerDefinition.StartTrigger)
			{
				string newTag = "";
				TriggerDefinition trigger = cboCaseTags.SelectedItem as TriggerDefinition;
				if (trigger != null)
				{
					newTag = trigger.Tag;
				}
				c.Tag = newTag;
				foreach (Case alternate in c.AlternativeConditions)
				{
					alternate.Tag = newTag;
				}

				//Figure out the stages
				List<int> oldStages = new List<int>();
				oldStages.AddRange(c.Stages);

				tableConditions.Save();

				c.CustomPriority = GUIHelper.ReadNumericBox(valPriority);
				c.Hidden = (chkBackground.Checked ? "1" : null);
				if (chkPlayOnce.Checked)
				{
					if (c.OneShotId == 0)
					{
						c.OneShotId = ++_character.Behavior.MaxCaseId;
					}
				}
				else
				{
					c.OneShotId = 0;
				}
			}

			//Lines
			gridDialogue.Save();

			SaveTagsTab();

			_character.Behavior.ApplyChanges(_selectedCase);
		}

		/// <summary>
		/// Updates the stage checkboxes to match the selected case's stages
		/// </summary>
		private void PopulateStageCheckboxes()
		{
			int stageId = -1;
			if (_selectedStage != null)
			{
				stageId = _selectedStage.Id;
			}
			gridStages.SetData(_character, _selectedCase, stageId);
		}

		private void SaveTagsTab()
		{
			string[] addTags = lstAddTags.SelectedItems;
			string[] removeTags = lstRemoveTags.SelectedItems;
			if (addTags.Length > 0)
			{
				_selectedCase.AddCharacterTags = string.Join(",", addTags);
			}
			if (removeTags.Length > 0)
			{
				_selectedCase.RemoveCharacterTags = string.Join(",", removeTags);
			}
		}

		public void SelectLine(DialogueLine line)
		{
			if (line != null)
			{
				gridDialogue.SelectLine(line);
			}
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
			_editorData.SetLabel(_selectedCase, txtLabel.Text, cmdColorCode.Tag?.ToString(), txtFolder.Text);
		}

		#region Macro editing
		private void tableConditions_EditingMacro(object sender, MacroArgs args)
		{
			args.SetEditor(this);
		}

		public bool ShowHelp
		{
			get
			{
				return MacroManager.ShowMacroHelp;
			}
		}

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Background; }
		}

		public string GetHelpText()
		{
			return MacroManager.HelpText;
		}

		public object CreateData()
		{
			Case tag = new Case(_selectedCase.Tag);
			tag.AddStages(_selectedCase.Stages);
			return tag;
		}

		public object GetRecordContext()
		{
			return _character;
		}

		public object GetSecondaryRecordContext()
		{
			return _character;
		}

		public Func<PropertyRecord, bool> GetRecordFilter(object data)
		{
			Case tag = data as Case;
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(tag.Tag);
			if (trigger.HasTarget)
			{
				return FilterTargets;
			}
			else
			{
				return null;
			}
		}
		#endregion

		private void tableConditions_MacroChanged(object sender, MacroArgs e)
		{
			Config.SaveMacros("Case");
		}

		private void cboCaseTags_SelectedIndexChanged(object sender, EventArgs e)
		{
			TriggerDefinition tag = cboCaseTags.SelectedItem as TriggerDefinition;
			if (tag != null)
			{
				lblHelpText.Text = tag.HelpText;
			}
		}

		public void AddSpeedButtons(PropertyTable table)
		{
			AddSpeedButtons(table, _selectedCase?.Tag);
		}

		private void cmdColorCode_Click(object sender, EventArgs e)
		{
			ColorCode color = RecordLookup.DoLookup(typeof(ColorCode), "", false, null) as ColorCode;
			if (color != null)
			{
				SetColorButton(color.Key);
			}
		}

		private void SetColorButton(string colorCode)
		{
			ColorCode code = Definitions.Instance.Get<ColorCode>(colorCode);
			if (code == null)
			{
				cmdColorCode.BackColor = SkinManager.Instance.CurrentSkin.Background.Normal;
				cmdColorCode.Tag = null;
			}
			else
			{
				cmdColorCode.BackColor = code.GetColor();
				cmdColorCode.Tag = colorCode;
			}
		}

		private void stripConditions_AddButtonClicked(object sender, EventArgs e)
		{
			if (_selectedCase == null) { return; }
			Case alternate = new Case(_selectedCase.Tag);
			_selectedCase.AlternativeConditions.Add(alternate);
			_selectedCase.NotifyPropertyChanged(nameof(_selectedCase.AlternativeConditions));
			AddAlternateTab();
			tabsConditions.SelectedIndex = _selectedCase.AlternativeConditions.Count;
		}

		private void AddAlternateTab()
		{
			tabsConditions.TabPages.Add($"Set {(tabsConditions.TabPages.Count)}");
			UpdateAddCaption();
		}

		private void stripConditions_CloseButtonClicked(object sender, EventArgs e)
		{
			if (_selectedCase == null) { return; }
			int index = tabsConditions.SelectedIndex - 1;
			if (index >= 0)
			{
				_selectedCase.AlternativeConditions.RemoveAt(index);
				_selectedCase.NotifyPropertyChanged(nameof(_selectedCase.AlternativeConditions));
				tabsConditions.TabPages.RemoveAt(index + 1);
				for (int i = index + 1; i < tabsConditions.TabPages.Count; i++)
				{
					tabsConditions.TabPages[i].Text = "Set " + i;
				}
				tabsConditions.SelectedIndex = index < tabsConditions.TabPages.Count - 1 ? index + 1 : index;
				UpdateAddCaption();
			}
		}

		private void tabsConditions_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_selectedCase == null) { return; }
			int index = tabsConditions.SelectedIndex;
			Case desiredCase = _selectedCase;
			if (index > 0)
			{
				desiredCase = _selectedCase.AlternativeConditions[index - 1];
			}
			TrackCase(desiredCase);
			PopulateConditionTable(desiredCase);
		}

		private void TrackCase(Case c)
		{
			if (_trackedCase != null)
			{
				_trackedCase.PropertyChanged -= CasePropertyChanged;
			}
			_trackedCase = c;
			if (_trackedCase != null)
			{
				_trackedCase.PropertyChanged += CasePropertyChanged;
			}
		}
	}
}

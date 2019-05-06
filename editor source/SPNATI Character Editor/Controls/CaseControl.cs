using Desktop;
using Desktop.CommonControls;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class CaseControl : UserControl, IMacroEditor
	{
		private const string FavoriteConditionsSetting = "FavoritedConditions";

		private Character _character;
		private CharacterEditorData _editorData;
		private ImageLibrary _imageLibrary = new ImageLibrary();
		private Case _selectedCase;
		private Stage _selectedStage;
		private bool _populatingCase;
		private List<DialogueLine> _lineClipboard = new List<DialogueLine>();

		public event EventHandler<DialogueLine> TextUpdated;
		public event EventHandler<int> HighlightRow;

		public CaseControl()
		{
			InitializeComponent();
		}

		private void CaseControl_Load(object sender, EventArgs e)
		{
			tableConditions.RecordFilter = FilterTargets;
			lstAddTags.RecordType = typeof(Tag);
			lstRemoveTags.RecordType = typeof(Tag);
			//SetupMessageHandlers();
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
			_imageLibrary = ImageLibrary.Get(character);
			tableConditions.Context = character;
			CreateStageCheckboxes();
		}

		public void SetCase(Stage stage, Case workingCase)
		{
			_selectedStage = stage;
			_selectedCase = workingCase;
			if (_selectedCase != null)
			{
				grpConditions.Enabled = true;
			}
			PopulateCase();
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

		public string GetImage(int index)
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

		public void SelectTextInRow(int rowIndex, int startIndex, int length)
		{
			gridDialogue.SelectTextInRow(rowIndex, startIndex, length);
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

			var stages = GetSelectedStages();
			gridDialogue.SetData(_character, _selectedStage, _selectedCase, stages, _imageLibrary);
			GetSelectedStages();

			PopulateTagsTab();

			_populatingCase = false;
			//HighlightRow(0);
		}

		private void AddSpeedButtons()
		{
			if (_selectedCase == null) { return; }
			Trigger caseTrigger = TriggerDatabase.GetTrigger(_selectedCase.Tag);
			tableConditions.AddSpeedButton("Game", "Background", (data) => { return AddVariableTest("~background~", data); });
			tableConditions.AddSpeedButton("Game", "Inside/Outside", (data) => { return AddVariableTest("~background.location~", data); });
			if (caseTrigger.AvailableVariables.Contains("cards"))
			{
				tableConditions.AddSpeedButton("Self", "Cards Exchanged", (data) => { return AddVariableTest("~cards~", data); });
			}
			tableConditions.AddSpeedButton("Self", "Collectible", (data) => { return AddVariableTest("~collectible.*~", data); });
			tableConditions.AddSpeedButton("Self", "Collectible (Counter)", (data) => { return AddVariableTest("~collectible.*.counter~", data); });
			tableConditions.AddSpeedButton("Self", "Costume", (data) => { return AddVariableTest("~self.costume~", data); });
			tableConditions.AddSpeedButton("Self", "Marker (Persistent) (+)", (data) => { return AddVariableTest("~persistent.*~", data); });
			tableConditions.AddSpeedButton("Self", "Slot", (data) => { return AddVariableTest("~self.slot~", data); });
			tableConditions.AddSpeedButton("Self", "Tag", (data) => { return AddVariableTest("~self.tag.*~", data); });
			if (caseTrigger.HasTarget)
			{
				if (caseTrigger.AvailableVariables.Contains("clothing"))
				{
					tableConditions.AddSpeedButton("Clothing", "Clothing Position", (data) => { return AddVariableTest("~clothing.position~", data); });
					tableConditions.AddSpeedButton("Clothing", "Clothing Type", (data) => { return AddVariableTest("~clothing.type~", data); });
				}
				tableConditions.AddSpeedButton("Target", "Collectible (+)", (data) => { return AddVariableTest("~target.collectible.*~", data); });
				tableConditions.AddSpeedButton("Target", "Collectible (Counter) (+)", (data) => { return AddVariableTest("~target.collectible.*.counter~", data); });
				tableConditions.AddSpeedButton("Target", "Costume", (data) => { return AddVariableTest("~target.costume~", data); });
				tableConditions.AddSpeedButton("Target", "Gender", (data) => { return AddVariableTest("~target.gender~", data); });
				tableConditions.AddSpeedButton("Target", "Marker (Persistent) (+)", (data) => { return AddVariableTest("~target.persistent.*~", data); });
				tableConditions.AddSpeedButton("Target", "Position", (data) => { return AddVariableTest("~target.position~", data); });
				tableConditions.AddSpeedButton("Target", "Size", (data) => { return AddVariableTest("~target.size~", data); });
				tableConditions.AddSpeedButton("Target", "Slot", (data) => { return AddVariableTest("~target.slot~", data); });
				tableConditions.AddSpeedButton("Target", "Tag (+)", (data) => { return AddVariableTest("~target.tag.*~", data); });
			}
			tableConditions.AddSpeedButton("Also Playing", "Collectible (+)", (data) => { return AddVariableTest("~_.collectible.*~", data); });
			tableConditions.AddSpeedButton("Also Playing", "Collectible (Counter) (+)", (data) => { return AddVariableTest("~_.collectible.*.counter~", data); });
			tableConditions.AddSpeedButton("Also Playing", "Costume", (data) => { return AddVariableTest("~_.costume~", data); });
			tableConditions.AddSpeedButton("Also Playing", "Marker (Persistent) (+)", (data) => { return AddVariableTest("~_.persistent.*~", data); });
			tableConditions.AddSpeedButton("Also Playing", "Position", (data) => { return AddVariableTest("~_.position~", data); });
			tableConditions.AddSpeedButton("Also Playing", "Slot", (data) => { return AddVariableTest("~_.slot~", data); });
			tableConditions.AddSpeedButton("Also Playing", "Tag (+)", (data) => { return AddVariableTest("~_.tag.*~", data); });
		}

		private string AddVariableTest(string variable, object data)
		{
			Case theCase = data as Case;
			theCase.Expressions.Add(new ExpressionTest(variable, ""));
			return "Expressions";
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

			SaveTagsTab();

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

		public string GetHelpText()
		{
			return MacroManager.HelpText;
		}

		public object CreateData()
		{
			Case tag = new Case(_selectedCase.Tag);
			tag.Stages.AddRange(_selectedCase.Stages);
			return tag;
		}

		public object GetRecordContext()
		{
			return _character;
		}

		public Func<PropertyRecord, bool> GetRecordFilter(object data)
		{
			Case tag = data as Case;
			Trigger trigger = TriggerDatabase.GetTrigger(tag.Tag);
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
	}
}

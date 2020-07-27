using Desktop;
using Desktop.CommonControls;
using Desktop.Skinning;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class DialogueTree : UserControl, ISkinControl
	{
		private string LastViewSetting = "TreeView";

		/// <summary>
		/// Raised when a new case (or stage) is about to be selected
		/// </summary>
		public event EventHandler<CaseSelectionEventArgs> SelectedNodeChanging;
		/// <summary>
		/// Raised when a new case (or stage) is selected
		/// </summary>
		public event EventHandler<CaseSelectionEventArgs> SelectedNodeChanged;
		/// <summary>
		/// Raised when a case is being created, to update its properties before it gets added to the tree
		/// </summary>
		public event EventHandler<CaseCreationEventArgs> CreatingCase;
		/// <summary>
		/// Raised when a case has been created
		/// </summary>
		public event EventHandler<CaseCreationEventArgs> CreatedCase;

		private Character _character;
		private CharacterEditorData _editorData;
		private DialogueNode _selectedNode;
		private bool _changingViews;
		private IDialogueTreeView _view;
		private bool _showHidden;

		public DialogueTree()
		{
			InitializeComponent();

			cboView.Items.AddRange(new string[] { "Stage", "Case", "Target", "Folder" });

			Shell.Instance.PostOffice.Subscribe(DesktopMessages.SettingsUpdated, OnSettingsChanged);
		}

		private void DialogueTree_Load(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				recTreeTarget.RecordType = typeof(Character);
				recTag.RecordType = typeof(Tag);

				lstDialogue.FormatRow += LstDialogue_FormatRow;
				lstDialogue.FormatGroup += LstDialogue_FormatGroup;
				lstDialogue.RightClick += LstDialogue_RightClick;
				lstDialogue.MoveItem += LstDialogue_MoveItem;
			}
		}

		private void OnSettingsChanged()
		{
			lstDialogue.Refresh();
		}

		public void SetData(Character character)
		{
			_character = character;
			_editorData = CharacterDatabase.GetEditorData(_character);
			int view = 1;
			if (Config.HasValue(LastViewSetting))
			{
				view = Config.GetInt(LastViewSetting);
			}
			cboView.SelectedIndex = view;
			cboView.SelectedIndexChanged += cboView_SelectedIndexChanged;
			if (_view == null)
			{
				switch (view)
				{
					case 1:
						_view = new CaseView();
						break;
					case 2:
						_view = new TargetView();
						break;
					case 3:
						_view = new FolderView();
						break;
					default:
						_view = new StageView();
						break;
				}
				InitializeView();
			}
			tsbtnSplit.DropDown = _view.GetCopyMenu();
			if (tsbtnSplit.DropDown != null)
			{
				SkinManager.Instance.ReskinControl(tsbtnSplit.DropDown, SkinManager.Instance.CurrentSkin);
			}
			_character.Behavior.CaseAdded += Behavior_CaseAdded;
			_character.Behavior.CaseRemoved += Behavior_CaseRemoved;
			_character.Behavior.CaseModified += Behavior_CaseModified;
			PopulateTriggerMenu();
			_view.BuildTree(_showHidden);
		}

		private void InitializeView()
		{
			_view.SaveNode += _view_SaveNode;
			lstDialogue.DataSource = null;
			lstDialogue.ClearColumns();
			_view.Initialize(lstDialogue, _character);
			lstDialogue.AllowRowReorder = _view.AllowReorder();
		}

		private void PopulateTriggerMenu()
		{
			List<CaseDefinition> definitions = CaseDefinitionDatabase.Definitions;
			definitions.Sort((a, b) =>
			{
				return CaseDefinitionDatabase.Compare(a.Key, b.Key);
			});
			Dictionary<int, ContextMenuStrip> groupItems = new Dictionary<int, ContextMenuStrip>();

			foreach (CaseDefinition def in definitions)
			{
				TriggerDefinition t = def.GetTrigger();
				if (t.StartStage < 0) continue;


				ContextMenuStrip curGroupMenu;
				if (!groupItems.TryGetValue(def.Group, out curGroupMenu))
				{
					ToolStripMenuItem groupMenuItem = new ToolStripMenuItem();
					CaseGroup group = CaseDefinitionDatabase.GetGroup(def.Group);
					groupMenuItem.Text = group.DisplayName;
					curGroupMenu = new ContextMenuStrip();
					curGroupMenu.ShowImageMargin = false;
					groupMenuItem.DropDown = curGroupMenu;
					triggerMenu.Items.Add(groupMenuItem);
					groupItems[def.Group] = curGroupMenu;
				}
				
				string label = def.DisplayName;
				ToolStripMenuItem item = new ToolStripMenuItem(label, null, triggerAddItem_Click, def.Key);
				item.Tag = def;
				curGroupMenu.Items.Add(item);
			}
		}

		/// <summary>
		/// Rebuilds the tree from scratch. Useful when the stages have changed
		/// </summary>
		public void RegenerateTree()
		{
			_view.BuildTree(_showHidden);
		}

		/// <summary>
		/// Adds a new case and selects it in the tree
		/// </summary>
		/// <param name="tag">Tag of case to add</param>
		/// <param name="singleStage">If true, initial stages will only be the current stage. If false, all possible stages will be checked.</param>
		private void AddAndSelectNewCase(Case newCase, string folder, bool singleStage)
		{
			string tag = newCase.Tag;
			int startStage;
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(tag);
			startStage = trigger.StartStage;
			int currentStage = _selectedNode?.Stage?.Id ?? startStage;

			//Add a default line
			if (newCase.Lines.Count == 0)
			{
				Tuple<string, string> template = DialogueDatabase.GetTemplate(tag);
				if (template != null)
				{
					DialogueLine line = new DialogueLine(template.Item1, template.Item2);
					newCase.Lines.Add(line);
				}
			}

			if (singleStage)
			{
				//shift finished stages to the layer-appropriate number
				int offset = trigger.StartStage - Clothing.MaxLayers;
				if (offset >= 0)
				{
					currentStage = _character.Layers + offset;
				}
				newCase.AddStage(currentStage);
			}
			else
			{
				for (int stageIndex = 0; stageIndex < _character.Layers + Clothing.ExtraStages; stageIndex++)
				{
					if (TriggerDatabase.UsedInStage(tag, _character, stageIndex))
					{
						newCase.AddStage(stageIndex);
					}
				}
			}

			if (!string.IsNullOrEmpty(folder) && _editorData != null)
			{
				_editorData.SetLabel(newCase, null, null, folder);
			}

			//Give the host control a chance to setup some properties
			CreatingCase?.Invoke(this, new CaseCreationEventArgs(newCase));

			//Add to the collection, which will trigger the CaseAdded event which will add it to the tree
			_character.Behavior.AddWorkingCase(newCase);

			//Raise an event saying the case has been created
			CreatedCase?.Invoke(this, new CaseCreationEventArgs(newCase));

			//Select it
			SelectNode(currentStage, newCase);
		}

		/// <summary>
		/// Selects a node based on a stage and case
		/// </summary>
		/// <param name="stage">Which stage within the case to select</param>
		/// <param name="stageCase">Case to select</param>
		public bool SelectNode(int stage, Case stageCase)
		{
			return _view.SelectNode(stage, stageCase);
		}

		/// <summary>
		/// Unselects the current node to force a save prior to splitting, duplicating, etc.
		/// </summary>
		private void LeaveNode()
		{
			if (_character == null || _selectedNode?.Case == null)
				return;
			Case selectedCase = _selectedNode.Case;
			CaseSelectionEventArgs args = new CaseSelectionEventArgs(_selectedNode.Stage, selectedCase);
			SelectedNodeChanging?.Invoke(this, args);
			_selectedNode = null;
			lstDialogue.SelectedItem = null;
		}

		/// <summary>
		/// Removes a case from the tree
		/// </summary>
		private void DeleteSelectedCase()
		{
			if (_character == null || _selectedNode?.Case == null)
				return;

			if (MessageBox.Show("Are you sure you want to permanently delete this case from all applicable stages?", "Delete Case", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				Case selectedCase = _selectedNode.Case;
				_character.Behavior.RemoveWorkingCase(selectedCase);
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				CleanupView();
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Cleans up anything related to the current view
		/// </summary>
		private void CleanupView()
		{
			if (_view == null) { return; }
			_view.SaveNode -= _view_SaveNode;
		}

		#region Event handlers
		private void chkHideTargeted_CheckedChanged(object sender, EventArgs e)
		{
			if (_character == null) { return; }
			LeaveNode();
			if (chkHideTargeted.Checked)
			{
				_view.SetFilter((Case workingCase) =>
				{
					return !workingCase.HasTargetedConditions;
				});
			}
			else
			{
				_view.SetFilter(null);
			}
		}

		private void recTreeTarget_RecordChanged(object sender, RecordEventArgs e)
		{
			if (_character == null) { return; }
			LeaveNode();
			string key = recTreeTarget.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				_view.SetFilter(null);
			}
			else
			{
				_view.SetFilter((Case workingCase) =>
				{
					return workingCase.GetTargets().Contains(key);
				});
			}
			lstDialogue.ExpandAll();
		}

		private void recTag_RecordChanged(object sender, RecordEventArgs e)
		{
			if (_character == null) { return; }
			LeaveNode();
			string key = recTag.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				_view.SetFilter(null);
			}
			else
			{
				_view.SetFilter((Case workingCase) =>
				{
					foreach (TargetCondition c in workingCase.Conditions)
					{
						if (c.FilterTag == key)
						{
							return true;
						}
					}
					return workingCase.Filter == key;
				});
			}
			lstDialogue.ExpandAll();
		}

		private void lstDialogue_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_changingViews) { return; }
			if (_selectedNode != null)
			{
				CaseSelectionEventArgs selectionArgs = new CaseSelectionEventArgs();
				selectionArgs.Stage = _selectedNode.Stage;
				selectionArgs.Case = _selectedNode.Case;
				//DialogueNode targetNode = lstDialogue.SelectedItem as DialogueNode;
				SelectedNodeChanging?.Invoke(this, selectionArgs);

				//the above might have shuffled items around, so make sure we actually have the targetNode selected
				//if (targetNode != lstDialogue.SelectedItem)
				//{
				//	_selectedNode = null;
				//	lstDialogue.SelectedItem = targetNode;
				//	return; //another event will be raised
				//}
			}

			DialogueNode node = lstDialogue.SelectedItem as DialogueNode;
			_selectedNode = node;

			CaseSelectionEventArgs args = new CaseSelectionEventArgs();
			if (node != null)
			{
				args.Stage = node.Stage;
				args.Case = node.Case;
			}
			SelectedNodeChanged?.Invoke(this, args);

			if (node == null)
			{
				tsbtnRemoveDialogue.Enabled = false;
				tsbtnSplit.Enabled = false;
				return;
			}

			tsbtnRemoveDialogue.Enabled = true;
			tsbtnSplit.Enabled = true;
		}

		private void Behavior_CaseModified(object sender, Case modifiedCase)
		{
			if (IsDisposed) { return; }
			_view.ModifyCase(modifiedCase);
		}

		private void Behavior_CaseRemoved(object sender, Case removedCase)
		{
			if (IsDisposed) { return; }
			_view.RemoveCase(removedCase);
		}

		private void Behavior_CaseAdded(object sender, Case newCase)
		{
			if (IsDisposed) { return; }
			_view.AddCase(newCase);
		}

		/// <summary>
		/// Adds a new case whose tag matches the currently selected case
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbtnAddDialogue_ButtonClick(object sender, EventArgs e)
		{
			string folder;
			Case newCase = _view.AddingCase(out folder);
			if (newCase == null)
			{
				return;
			}
			else
			{
				AddAndSelectNewCase(newCase, folder, true);
			}
		}

		private void triggerAddItem_Click(object sender, EventArgs e)
		{
			if (_character == null)
				return;

			CaseDefinition def = ((ToolStripMenuItem)sender).Tag as CaseDefinition;
			Case newCase = def.CreateCase();
			_view.BuildCase(newCase);
			AddAndSelectNewCase(newCase, "", false);
		}

		private void tsmiRemove_Click(object sender, EventArgs e)
		{
			DeleteSelectedCase();
		}

		private void lstDialogue_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedCase();
			}
		}

		private void _view_SaveNode(object sender, EventArgs e)
		{
			LeaveNode();
		}

		private void cboView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_character == null) { return; }
			_changingViews = true;
			LeaveNode();
			int index = cboView.SelectedIndex;
			Config.Set(LastViewSetting, index);
			CleanupView();
			switch (index)
			{
				case 1:
					_view = new CaseView();
					break;
				case 2:
					_view = new TargetView();
					break;
				case 3:
					_view = new FolderView();
					break;
				default:
					_view = new StageView();
					break;
			}
			InitializeView();
			tsbtnSplit.DropDown = _view.GetCopyMenu();
			_view.SetFilter(null);
			_view.BuildTree(_showHidden);
			_changingViews = false;
		}
		#endregion

		private void triggerMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DialogueNode node = lstDialogue.SelectedItem as DialogueNode;

			foreach (ToolStripMenuItem group in triggerMenu.Items)
			{
				int visibleCount = 0;
				foreach (ToolStripMenuItem item in group.DropDownItems)
				{
					CaseDefinition t = item.Tag as CaseDefinition;
					if (_view.IsTriggerValid(node, t.GetTrigger()))
					{
						visibleCount++;
						item.Visible = true;
					}
					else
					{
						item.Visible = false;
					}
				}
				group.Visible = (visibleCount > 0);
			}
		}

		private void tsConfig_DropDownOpening(object sender, EventArgs e)
		{
			DialogueNode node = lstDialogue.SelectedItem as DialogueNode;
			if (node == null || node.Case == null)
			{
				tsHide.Enabled = false;
				tsUnhide.Enabled = false;
			}
			else
			{
				Case selectedCase = node.Case;
				bool hidden = _editorData.IsHidden(selectedCase);
				tsHide.Enabled = !hidden;
				tsUnhide.Enabled = hidden;
			}
		}

		private void tsHide_Click(object sender, EventArgs e)
		{
			DialogueNode node = lstDialogue.SelectedItem as DialogueNode;
			Case selectedCase = node?.Case;
			if (selectedCase == null) { return; }

			_editorData.HideCase(selectedCase, true);
			_view.HideCase(selectedCase, true);
		}

		private void tsUnhide_Click(object sender, EventArgs e)
		{
			DialogueNode node = lstDialogue.SelectedItem as DialogueNode;
			Case selectedCase = node?.Case;
			if (selectedCase == null) { return; }

			_editorData.HideCase(selectedCase, false);
			_view.HideCase(selectedCase, false);
		}

		private void tsShowHidden_Click(object sender, EventArgs e)
		{
			_showHidden = !_showHidden;
			RegenerateTree();
		}

		private void LstDialogue_FormatGroup(object sender, FormatGroupEventArgs e)
		{
			_view?.FormatGroup(e);
		}

		private void LstDialogue_FormatRow(object sender, FormatRowEventArgs e)
		{
			_view?.FormatRow(e);
		}

		private void LstDialogue_RightClick(object sender, AccordionListViewEventArgs e)
		{
			ContextMenuStrip strip = _view?.ShowContextMenu(e);
			if (strip != null)
			{
				strip.Show(Cursor.Position);
			}
		}

		private void cmdLegend_Click(object sender, EventArgs e)
		{
			new DialogueLegend().ShowDialog();
		}

		private void tsRecipe_Click(object sender, EventArgs e)
		{
			Recipe recipe = RecordLookup.DoLookup(typeof(Recipe), "", false, _character) as Recipe;
			if (recipe != null)
			{
				CharacterEditorData editorData = CharacterDatabase.GetEditorData(_character);
				Case existing = editorData.GetRecipeUsage(recipe);
				if (existing != null)
				{
					DialogResult result = MessageBox.Show("Do you want to create a new case?", $"Add {recipe.Name}", MessageBoxButtons.YesNoCancel);
					if (result == DialogResult.Cancel)
					{
						return;
					}
					else if (result == DialogResult.No)
					{
						SelectNode(existing.Stages[0], existing);
						return;
					}
				}

				Case instance = recipe.AddToCharacter(_character);
				editorData.AddRecipeUsage(recipe, instance);
				SelectNode(instance.Stages[0], instance);
			}
		}

		private void tsRefresh_Click(object sender, EventArgs e)
		{
			int currentStage = _selectedNode?.Stage?.Id ?? -1;
			Case currentCase = _selectedNode?.Case;
			_view?.Sort();
			Invalidate(true);
			if (currentCase != null && currentStage >= 0)
			{
				_view.SelectNode(currentStage, currentCase);
			}
		}

		public void OnUpdateSkin(Skin skin)
		{
			SkinManager.Instance.ReskinControl(triggerMenu, skin);
			if (tsbtnSplit.DropDown != null)
			{
				SkinManager.Instance.ReskinControl(tsbtnSplit.DropDown, skin);
			}
		}

		private void tsCollapse_Click(object sender, EventArgs e)
		{
			lstDialogue.CollapseAll();
		}

		private void tsExpandAll_Click(object sender, EventArgs e)
		{
			lstDialogue.ExpandAll();
		}


		private void LstDialogue_MoveItem(object sender, AccordionListViewDragEventArgs e)
		{
			_view.MoveItem(e.Source, e.Target, e.Before);
		}

	}

	public class CaseSelectionEventArgs : EventArgs
	{
		public Stage Stage;
		public Case Case;

		public CaseSelectionEventArgs() { }

		public CaseSelectionEventArgs(Stage stage, Case theCase)
		{
			Stage = stage;
			Case = theCase;
		}
	}

	public class CaseCreationEventArgs : EventArgs
	{
		/// <summary>
		/// Case being created
		/// </summary>
		public Case Case;

		public CaseCreationEventArgs(Case c)
		{
			Case = c;
		}
	}
}

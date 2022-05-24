using Desktop.CommonControls;
using Desktop.Skinning;
using SPNATI_Character_Editor.DataStructures;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	/// <summary>
	/// View that orders the dialogue tree by stage where cases shared across stages have nodes in each stage
	/// </summary>
	public class CaseView : IDialogueTreeView
	{
		public event EventHandler SaveNode;

		private GroupedList<DialogueNode> _model = new GroupedList<DialogueNode>();

		private Font _font = new Font("Arial", 9, FontStyle.Bold);

		/// <summary>
		/// Mapping from case to node
		/// </summary>
		private Dictionary<Case, DialogueNode> _caseMap = new Dictionary<Case, DialogueNode>();

		private AccordionListView _listView;
		private ContextMenuStrip splitMenu;
		private Character _character;
		private CharacterEditorData _editorData;
		private Func<Case, bool> _filter;
		private bool _showHidden;

		public GroupedList<DialogueNode> GetModel()
		{
			return _model;
		}

		public void Initialize(AccordionListView listView, Character character)
		{
			_listView = listView;
			_character = character;
			_editorData = CharacterDatabase.GetEditorData(character);

			InitializeColumns();
		}

		private void InitializeColumns()
		{
			AccordionColumn column;
			if (Config.UseSimpleTree)
			{
				column = new AccordionColumn("Case", "Label");
				column.FillWeight = 1;
				_listView.AddColumn(column);
			}
			else
			{
				column = new AccordionColumn("Stage", "StageRange");
				column.Width = 40;
				_listView.AddColumn(column);

				column = new AccordionColumn("Target", "Target");
				column.Width = 55;
				_listView.AddColumn(column);

				column = new AccordionColumn("Conditions", "Conditions");
				column.FillWeight = 1;
				_listView.AddColumn(column);

				column = new AccordionColumn("Pri", "Priority");
				column.Width = 40;
				column.TextAlign = HorizontalAlignment.Right;
				_listView.AddColumn(column);
			}

			_listView.RebuildColumns();
		}

		public ContextMenuStrip GetCopyMenu()
		{
			splitMenu = new ContextMenuStrip();
			splitMenu.Items.Add("Isolate Stage", null, SeparateCaseFromStage);
			splitMenu.Items.Add("Split at Stage", null, SplitCaseAtPoint);
			splitMenu.Items.Add("Split this Case into Individual Stages", null, SplitAllStages);
			splitMenu.Items.Add("Duplicate this Case", null, DuplicateCase);
			splitMenu.Items.Add(new ToolStripSeparator());
			splitMenu.Items.Add("Remove", null, DeleteCase);
			return splitMenu;
		}

		public void Sort()
		{
			_model.SortItems();
		}

		public void BuildTree(bool showHidden)
		{
			_showHidden = showHidden;
			_listView.DataSource = null;

			_caseMap.Clear();

			_model = new GroupedList<DialogueNode>();
			_model.GroupComparer = SortGroups;
			_model.ItemComparer = DialogueNode.CompareCases;

			bool hideEmpty = Config.HideEmptyCases;

			//Make nodes for each case tag
			foreach (CaseDefinition def in CaseDefinitionDatabase.Definitions)
			{
				string key = def.Key;
				if (def.IsCore)
				{
					string tag = def.Case.Tag;
					if (tag == "-") { continue; }

					TriggerDefinition trigger = TriggerDatabase.GetTrigger(tag);
					if (!hideEmpty || !trigger.Optional)
					{
						_model.AddGroup(key);
					}
				}
				else
				{
					if (!hideEmpty)
					{
						_model.AddGroup(key);
					}
				}
			}

			//Add working cases to the right grouper
			foreach (Case workingCase in _character.Behavior.GetWorkingCases())
			{
				if (!showHidden && _editorData.IsHidden(workingCase)) { continue; }

				CreateCaseNode(workingCase);
			}

			_model.Sorted = true;
			_listView.DataSource = _model;
		}

		private void CreateCaseNode(Case workingCase)
		{
			//Exclude cases depending on filters. These are just excluded from the UI. This has no bearing on the actual underlying data
			if (_filter != null && !_filter(workingCase))
			{
				return;
			}

			DialogueNode wrapper = new DialogueNode(_character, new Stage(workingCase.Stages[0]), workingCase);
			wrapper.Mode = NodeMode.Case;
			CreateNode(wrapper);
		}

		/// <summary>
		/// Creates a node in the dialogue tree
		/// </summary>
		/// <param name="wrapper"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		private void CreateNode(DialogueNode wrapper)
		{	
			_model.AddItem(wrapper);
			_caseMap[wrapper.Case] = wrapper;
		}

		public void SetFilter(Func<Case, bool> filter)
		{
			_filter = filter;
			BuildTree(_showHidden);
		}

		public bool SelectNode(int stage, Case stageCase)
		{
			DialogueNode node = _caseMap.Get(stageCase);
			if (node != null)
			{
				_listView.SelectedItem = node;
				return true;
			}
			return false;
		}

		public void BuildCase(Case theCase)
		{
		}

		public Case AddingCase(out string folder)
		{
			string newTag = null;
			folder = "";
			if (_character == null)
			{
				return null;
			}
			DialogueNode node = _listView.SelectedItem as DialogueNode;
			if (node == null)
			{
				GroupedListGrouper group = _listView.SelectedItem as GroupedListGrouper;
				string tag = group?.RootKey ?? group?.Key?.ToString();
				if (!string.IsNullOrEmpty(tag))
				{
					if (tag != group?.Key?.ToString())
					{
						folder = group?.Key?.ToString();
					}
					newTag = tag;
				}
			}
			else
			{
				Case c = node?.Case;
				if (c != null && _editorData != null)
				{
					CaseLabel label = _editorData.GetLabel(c);
					if (label != null)
					{
						folder = label.Folder;
					}
				}

				CaseDefinition def = node.Definition;
				return def.CreateCase();
			}

			if (string.IsNullOrEmpty(newTag))
			{
				newTag = node?.Case?.Tag;
			}
			if (!string.IsNullOrEmpty(newTag))
			{
				CaseDefinition def = CaseDefinitionDatabase.GetDefinition(newTag);
				if (def != null)
				{
					return def.CreateCase();
				}
				return new Case(newTag);
			}
			return null;
		}

		public void ModifyCase(Case modifiedCase)
		{
			//since all stages are in the same node, we don't need to do anything but update that node's appearance!
			DialogueNode node = _caseMap.Get(modifiedCase);
			if (node != null)
			{
				if (modifiedCase.Stages.Count > 0)
				{
					node.Stage = new Stage(modifiedCase.Stages[0]);
				}
			}
		}

		public void AddCase(Case newCase)
		{
			DialogueNode wrapper = new DialogueNode(_character, new Stage(newCase.Stages[0]), newCase);
			wrapper.Mode = NodeMode.Case;
			CreateNode(wrapper);
		}

		public void RemoveCase(Case removedCase)
		{
			DialogueNode node = _caseMap.Get(removedCase);
			if (node == null) { return; }
			_model.RemoveItem(node);
			_caseMap.Remove(removedCase);
		}
		
		private void SeparateCaseFromStage(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			Case selectedCase = selectedNode?.Case;
			StageSelect select = new StageSelect();
			select.SetData(_character, selectedCase, "Isolate Stage", "Choose the stage you want to split out from the others.");
			if (select.ShowDialog() == DialogResult.OK)
			{
				int stage = select.Stage;
				SaveNode?.Invoke(this, EventArgs.Empty);
				_character.Behavior.SplitCaseStage(selectedCase, stage);
				SelectNode(stage, selectedCase);
			}
		}

		private void SplitCaseAtPoint(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			Case selectedCase = selectedNode?.Case;
			StageSelect select = new StageSelect();
			select.SetData(_character, selectedCase, "Split Case at Stage", "Choose the stage to split at. Two cases will be created: one with cases before the split point, and one starting at the split.");
			if (select.ShowDialog() == DialogResult.OK)
			{
				int stage = select.Stage;
				SaveNode?.Invoke(this, EventArgs.Empty);
				_character.Behavior.SplitCaseAtStage(selectedCase, stage);
				SelectNode(stage, selectedCase);
			}
		}

		/// <summary>
		/// Separates the selected case into individual copies for each applicable stage
		/// </summary>
		private void SplitAllStages(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			int stage = selectedNode.Stage.Id;
			Case selectedCase = selectedNode.Case;
			SaveNode?.Invoke(this, EventArgs.Empty);
			_character.Behavior.DivideCaseIntoSeparateStages(selectedCase, stage);
			SelectNode(stage, selectedCase);
		}

		private void DuplicateCase(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			Case selectedCase = selectedNode.Case;
			int stage = selectedNode.Stage.Id;
			SaveNode?.Invoke(this, EventArgs.Empty);
			Case copy = _character.Behavior.DuplicateCase(selectedCase, true);
			SelectNode(stage, copy);
		}

		/// <summary>
		/// Removes a case from the tree
		/// </summary>
		private void DeleteCase(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;

			if (MessageBox.Show("Are you sure you want to permanently delete this case from all applicable stages?", "Delete Case", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				Case selectedCase = selectedNode.Case;
				_character.Behavior.RemoveWorkingCase(selectedCase);
			}
		}

		public bool IsTriggerValid(DialogueNode selectedNode, TriggerDefinition trigger)
		{
			return true;
		}

		public void HideCase(Case c, bool hide)
		{
			if (hide)
			{
				DialogueNode node = _caseMap.Get(c);
				if (node != null)
				{
					if (!_showHidden)
					{
						_model.RemoveItem(node);
						_caseMap.Remove(c);
					}
				}
			}
			else
			{
				if (_showHidden)
				{
					DialogueNode node = _caseMap.Get(c);
					if (node != null)
					{
						node.Dummy++;
					}
				}
				else
				{
					//generate a node
					CreateCaseNode(c);
				}
			}
		}

		public int SortGroups(string key1, string key2)
		{
			return CaseDefinitionDatabase.Compare(key1, key2);
		}

		public void FormatRow(FormatRowEventArgs args)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			DialogueNode node = args.Model as DialogueNode;
			args.Tooltip = node.Case.ToString();

			args.GrouperColor = GetGroupColor(args.Group.Key, args.Group.Index);

			CaseLabel label = _editorData.GetLabel(node.Case);
			if (label != null)
			{
				ColorCode colorCode = Definitions.Instance.Get<ColorCode>(label.ColorCode);
				if (colorCode != null)
				{
					args.ForeColor = colorCode.GetColor();
					return;
				}
			}

			if (_editorData.IsHidden(node.Case))
			{
				args.ForeColor = skin.LightGray;
			}
			else if (node.Case.Disabled == "1")
			{
				args.ForeColor = skin.LightGray;
			}
			else if (node.Case.Hidden == "1")
			{
				args.ForeColor = skin.Gray;
			}
			else if (node.Case.HasCollectible)
			{
				args.ForeColor = skin.Orange;
			}
			else if ((Config.ColorTargetedLines || Config.UseSimpleTree) && node.Case.HasTargetedConditionsIncludeHuman)
			{
				args.ForeColor = skin.Green;
			}
			else
			{
				//Highlight lines that are still using the default
				Tuple<string, string> template = DialogueDatabase.GetTemplate(node.Case.Tag);
				if (template != null)
				{
					foreach (var line in node.Case.Lines)
					{
						if (Path.GetFileNameWithoutExtension(line.Image) == template.Item1 && line.Text?.Trim() == template.Item2)
						{
							args.ForeColor = skin.Blue;
						}
					}
				}
			}
		}

		private Color GetGroupColor(string key, int index)
		{
			CaseDefinition definition = CaseDefinitionDatabase.GetDefinition(key);
			if (definition == null)
			{
				return index % 2 == 0 ? SkinManager.Instance.CurrentSkin.PrimaryForeColor : SkinManager.Instance.CurrentSkin.SecondaryForeColor;
			}
			string tag = definition.Case.Tag;
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(tag);
			if (string.IsNullOrEmpty(trigger.Label))
			{
				return index % 2 == 0 ? SkinManager.Instance.CurrentSkin.PrimaryForeColor : SkinManager.Instance.CurrentSkin.SecondaryForeColor;
			}
			return SkinManager.Instance.CurrentSkin.GetGrouper(trigger.ColorScheme);
		}

		public void FormatGroup(FormatGroupEventArgs args)
		{
			CaseDefinition definition = CaseDefinitionDatabase.GetDefinition(args.Group.Key);
			string tag = definition?.Case.Tag;

			args.Font = _font;
			args.ForeColor = GetGroupColor(tag, args.Group.Index);
			
			string label = definition?.DisplayName;
			if (!string.IsNullOrEmpty(label))
			{
				args.Label = label;
			}
		}

		public ContextMenuStrip ShowContextMenu(AccordionListViewEventArgs args)
		{
			if (args.Model != null)
			{
				return splitMenu;
			}
			return null;
		}

		public bool AllowReorder()
		{
			return false;
		}

		public void MoveItem(object source, object target, bool before)
		{

		}
	}
}

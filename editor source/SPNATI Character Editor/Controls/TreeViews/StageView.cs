using Desktop;
using Desktop.CommonControls;
using Desktop.Skinning;
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
	public class StageView : IDialogueTreeView
	{
		public event EventHandler SaveNode;

		private Font _font = new Font("Arial", 9, FontStyle.Bold);
		private GroupedList<DialogueNode> _model = new GroupedList<DialogueNode>();

		/// <summary>
		/// Mapping from stage+case to node
		/// </summary>
		private DualKeyDictionary<Case, int, DialogueNode> _caseMap = new DualKeyDictionary<Case, int, DialogueNode>();

		private AccordionListView _listView;
		private ContextMenuStrip splitMenu;
		private Character _character;
		private CharacterEditorData _editorData;
		private Func<Case, bool> _filter;
		private bool _showHidden;

		public void Initialize(AccordionListView listView, Character character)
		{
			_listView = listView;
			_character = character;
			_editorData = CharacterDatabase.GetEditorData(_character);

			InitializeColumns();
		}

		public GroupedList<DialogueNode> GetModel()
		{
			return _model;
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
				column = new AccordionColumn("Tag", "Tag");
				column.Width = 160;
				_listView.AddColumn(column);

				column = new AccordionColumn("Tgt", "Target");
				column.Width = 40;
				_listView.AddColumn(column);

				column = new AccordionColumn("Conditions", "Conditions");
				column.FillWeight = 1;
				_listView.AddColumn(column);

				column = new AccordionColumn("P", "Priority");
				column.Width = 31;
				column.TextAlign = HorizontalAlignment.Right;
				_listView.AddColumn(column);
			}

			_listView.RebuildColumns();
		}

		public ContextMenuStrip GetCopyMenu()
		{
			splitMenu = new ContextMenuStrip();
			splitMenu.Items.Add("Separate this Stage into a New Case", null, SeparateCaseFromStage);
			splitMenu.Items.Add("Separate this and Later Stages into a New Case", null, SplitCaseAtPoint);
			splitMenu.Items.Add("Split this Case into Individual Stages", null, SplitAllStages);
			splitMenu.Items.Add("Duplicate this Case", null, DuplicateCase);
			splitMenu.Items.Add("Bulk Replace Tool...", null, BulkReplace);
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

			//Make groups for each stage (in case no lines exist for that stage)
			for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
			{
				_model.AddGroup(i.ToString());
			}

			//Make nodes for cases
			foreach (Case workingCase in _character.Behavior.GetWorkingCases())
			{
				if (!_showHidden && _editorData.IsHidden(workingCase))
				{
					continue;
				}

				//Exclude cases depending on filters. These are just excluded from the UI. This has no bearing on the actual underlying data
				if (_filter != null && !_filter(workingCase))
				{
					continue;
				}

				//create a node for each stage the appears in
				GenerateNodes(workingCase);
			}

			_model.Sorted = true;
			_listView.DataSource = _model;
		}

		private void GenerateNodes(Case workingCase)
		{
			foreach (int stageIndex in workingCase.Stages)
			{
				GenerateNode(workingCase, stageIndex);
			}
		}

		private void GenerateNode(Case workingCase, int stageIndex)
		{
			Stage stage = new Stage(stageIndex);

			DialogueNode node = new DialogueNode(_character, stage, workingCase);
			node.Mode = NodeMode.Stage;
			_caseMap.Set(workingCase, stageIndex, node);
			_model.AddItem(node);
		}

		public void SetFilter(Func<Case, bool> filter)
		{
			_filter = filter;
			BuildTree(_showHidden);
		}

		public bool SelectNode(int stage, Case stageCase)
		{
			DialogueNode node = _caseMap.Get(stageCase, stage);
			if (node != null)
			{
				_listView.SelectedItem = node;
				return true;
			}
			else
			{
				//try the earliest stage
				if (stageCase.Stages.Count > 0 && stage != stageCase.Stages[0])
				{
					return SelectNode(stageCase.Stages[0], stageCase);
				}
			}
			return false;
		}

		public void BuildCase(Case theCase)
		{
		}

		public Case AddingCase(out string folder)
		{
			folder = "";
			DialogueNode node = _listView.SelectedItem as DialogueNode;
			if (_character == null || node == null)
			{
				return null;
			}
			Case c = node?.Case;
			if (c != null && _editorData != null)
			{
				CaseLabel label = _editorData.GetLabel(c);
				if (label != null)
				{
					folder = label.Folder;
				}
			}
			string tag = c?.Tag;
			if (!string.IsNullOrEmpty(tag))
			{
				return new Case(tag);
			}
			return null;
		}

		public void ModifyCase(Case modifiedCase)
		{
			if (!_caseMap.ContainsPrimaryKey(modifiedCase))
			{
				//this could happen when deleting a node
				return;
			}

			//reconcile any stage differences between the real case and what we have cached
			HashSet<int> modifiedStages = new HashSet<int>();
			foreach (int stage in modifiedCase.Stages)
			{
				modifiedStages.Add(stage);
			}

			//Process stages that no longer exist
			List<Tuple<int, DialogueNode>> nodesToRemove = new List<Tuple<int, DialogueNode>>();
			foreach (KeyValuePair<int, DialogueNode> kvp in _caseMap[modifiedCase])
			{
				int stage = kvp.Key;
				modifiedStages.Remove(stage);
				if (!modifiedCase.Stages.Contains(stage))
				{
					nodesToRemove.Add(new Tuple<int, DialogueNode>(kvp.Key, kvp.Value));
				}
			}
			foreach (Tuple<int, DialogueNode> kvp in nodesToRemove)
			{
				_model.RemoveItem(kvp.Item2);
				_caseMap.Remove(modifiedCase, kvp.Item1);
			}

			//Process stages that don't have nodes yet
			foreach (int stage in modifiedStages)
			{
				GenerateNode(modifiedCase, stage);
			}
		}

		public void AddCase(Case newCase)
		{
			GenerateNodes(newCase);
		}

		public void RemoveCase(Case removedCase)
		{
			if (!_caseMap.ContainsPrimaryKey(removedCase)) { return; }
			List<Tuple<int, DialogueNode>> nodesToRemove = new List<Tuple<int, DialogueNode>>();
			foreach (KeyValuePair<int, DialogueNode> kvp in _caseMap[removedCase])
			{
				nodesToRemove.Add(new Tuple<int, DialogueNode>(kvp.Key, kvp.Value));
			}
			foreach (Tuple<int, DialogueNode> kvp in nodesToRemove)
			{
				_model.RemoveItem(kvp.Item2);
			}
			_caseMap.Remove(removedCase);
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

		/// <summary>
		/// Separates the current stage into a new case with the same conditions and dialogue as the selected one
		/// </summary>
		private void SeparateCaseFromStage(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			int stage = selectedNode.Stage.Id;
			Case selectedCase = selectedNode.Case;
			SaveNode?.Invoke(this, EventArgs.Empty);
			_character.Behavior.SplitCaseStage(selectedCase, stage);
			SelectNode(stage, selectedCase);
		}

		/// <summary>
		/// Separates the current stage and later ones from the current case, effectively splitting the case in two
		/// </summary>
		private void SplitCaseAtPoint(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			int stage = selectedNode.Stage.Id;
			Case selectedCase = selectedNode.Case;
			SaveNode?.Invoke(this, EventArgs.Empty);
			_character.Behavior.SplitCaseAtStage(selectedCase, stage);
			SelectNode(stage, selectedCase);
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

		private void BulkReplace(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (selectedNode?.Case == null)
				return;
			Case selectedCase = selectedNode.Case;
			SaveNode?.Invoke(this, EventArgs.Empty);
			BulkReplaceTool replacer = new BulkReplaceTool();
			replacer.SourceTag = selectedCase.Tag;
			if (replacer.ShowDialog() == DialogResult.OK)
			{
				_character.Behavior.BulkReplace(replacer.SourceTag, replacer.DestinationTags);
				Shell.Instance.SetStatus("Dialogue replaced.");
			}
		}

		public bool IsTriggerValid(DialogueNode selectedNode, TriggerDefinition trigger)
		{
			Stage stage = selectedNode?.Stage;
			if (stage == null)
			{
				return true;
			}
			return TriggerDatabase.UsedInStage(trigger.Tag, _character, stage.Id);
		}

		public void HideCase(Case c, bool hide)
		{
			if (hide)
			{
				foreach (int stage in c.Stages)
				{
					DialogueNode node = _caseMap.Get(c, stage);
					if (node != null)
					{
						if (!_showHidden)
						{
							_model.RemoveItem(node);
							_caseMap.Remove(c, stage);
						}
					}
				}
			}
			else
			{
				if (_showHidden)
				{
					foreach (int stage in c.Stages)
					{
						DialogueNode node = _caseMap.Get(c, stage);
						if (node != null)
						{
							node.Dummy++;
						}
					}
				}
			}
		}

		public int SortGroups(string key1, string key2)
		{
			int s1, s2;
			if (int.TryParse(key1, out s1) && int.TryParse(key2, out s2))
			{
				return s1.CompareTo(s2);
			}
			return key1.CompareTo(key2);
		}

		public void FormatRow(FormatRowEventArgs args)
		{
			DialogueNode node = args.Model as DialogueNode;
			Skin skin = SkinManager.Instance.CurrentSkin;
			args.Tooltip = node.Case.ToString();

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
						if ((Path.GetFileNameWithoutExtension(line.Image) == template.Item1 || string.IsNullOrEmpty(line.Image)) && line.Text?.Trim() == template.Item2)
						{
							args.ForeColor = skin.Blue;
						}
					}
				}
			}
		}

		public void FormatGroup(FormatGroupEventArgs args)
		{
			args.Font = _font;
			int stage;
			if (int.TryParse(args.Group.Key, out stage))
			{
				args.Label = _character.LayerToStageName(stage).DisplayName + " (" + stage + ")";
			}
			else
			{
				args.Label = args.Group.Key;
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

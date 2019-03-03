using Desktop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	/// <summary>
	/// View that orders the dialogue tree by stage where cases shared across stages have nodes in each stage
	/// </summary>
	public class StageView : IDialogueTreeView
	{
		public event EventHandler<TreeNode> DeleteNode;
		public event EventHandler SaveNode;

		/// <summary>
		/// Mapping from stage to node
		/// </summary>
		private Dictionary<int, TreeNode> _stageMap = new Dictionary<int, TreeNode>();

		/// <summary>
		/// Mapping from stage+case to node
		/// </summary>
		private DualKeyDictionary<Case, int, TreeNode> _caseMap = new DualKeyDictionary<Case, int, TreeNode>();

		private TreeView treeDialogue;
		private ContextMenuStrip splitMenu;
		private Character _character;
		private CharacterEditorData _editorData;
		private Func<Case, bool> _filter;
		private bool _showHidden;

		public void Initialize(TreeView tree, Character character)
		{
			treeDialogue = tree;
			treeDialogue.TreeViewNodeSorter = null;
			_character = character;
			_editorData = CharacterDatabase.GetEditorData(_character);
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

		public void BuildTree(bool showHidden)
		{
			_showHidden = showHidden;
			treeDialogue.Nodes.Clear();
			treeDialogue.Sorted = false;

			_stageMap = new Dictionary<int, TreeNode>();

			//Make nodes for each stage
			for (int i = 0; i < _character.Layers + Clothing.ExtraStages; i++)
			{
				TreeNode node = CreateNode(new DialogueNode(_character, new Stage(i)), null, false);
				_stageMap[i] = node;
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
				GenerateNodes(workingCase, false);
			}
		}

		private void GenerateNodes(Case workingCase, bool sorted)
		{
			foreach (int stageIndex in workingCase.Stages)
			{
				GenerateNode(workingCase, stageIndex, sorted);
			}
		}

		private void GenerateNode(Case workingCase, int stageIndex, bool sorted)
		{
			TreeNode stageNode = _stageMap.Get(stageIndex);
			if (stageNode == null) { return; }

			Stage stage = (stageNode.Tag as DialogueNode).Stage;

			TreeNode caseNode = CreateNode(new DialogueNode(_character, stage, workingCase), stageNode, sorted);
			_caseMap.Set(workingCase, stageIndex, caseNode);
		}

		/// <summary>
		/// Creates a node in the dialogue tree
		/// </summary>
		/// <param name="wrapper"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		private TreeNode CreateNode(DialogueNode wrapper, TreeNode parent, bool sorted)
		{
			TreeNode node = new TreeNode();
			node.Text = wrapper.ToString();
			node.Tag = wrapper;

			if (wrapper.NodeType == NodeType.Case)
			{
				node.ContextMenuStrip = splitMenu;
				UpdateNodeAppearance(node);
			}

			if (parent == null)
			{
				treeDialogue.Nodes.Add(node);
			}
			else
			{
				if (sorted)
				{
					bool inserted = false;
					for (int i = 0; i < parent.Nodes.Count; i++)
					{
						DialogueNode sibling = parent.Nodes[i].Tag as DialogueNode;
						if (Behaviour.CompareTags(wrapper.Case, sibling.Case) <= 0)
						{
							parent.Nodes.Insert(i, node);
							inserted = true;
							break;
						}
					}
					if (!inserted)
					{
						parent.Nodes.Add(node);
					}
				}
				else
				{
					parent.Nodes.Add(node);
				}
			}
			return node;
		}

		private void UpdateNodeAppearance(TreeNode node)
		{
			DialogueNode wrapper = node.Tag as DialogueNode;
			if (_editorData.IsHidden(wrapper.Case))
			{
				node.ForeColor = System.Drawing.Color.LightGray;
			}
			else if (wrapper.Case.Hidden == "1")
			{
				node.ForeColor = System.Drawing.Color.Gray;
			}
			else if (wrapper.Case.HasFilters)
			{
				//Highlight targeted dialogue
				node.ForeColor = System.Drawing.Color.Green;
			}
			else
			{
				node.ForeColor = System.Drawing.Color.Black;
				//Highlight lines that are still using the default
				Tuple<string, string> template = DialogueDatabase.GetTemplate(wrapper.Case.Tag);
				if (template != null)
				{
					foreach (var line in wrapper.Case.Lines)
					{
						if (Path.GetFileNameWithoutExtension(line.Image) == template.Item1 && line.Text?.Trim() == template.Item2)
						{
							node.ForeColor = System.Drawing.Color.Blue;

							//Color ancestors too
							TreeNode ancestor = node.Parent;
							while (ancestor != null)
							{
								ancestor.ForeColor = System.Drawing.Color.Blue;
								ancestor = ancestor.Parent;
							}
						}
					}
				}
			}

			//update text as well
			node.Text = wrapper.Case.ToString();
		}

		public void SetFilter(Func<Case, bool> filter)
		{
			_filter = filter;
			BuildTree(_showHidden);
		}

		public void SelectNode(int stage, Case stageCase)
		{
			TreeNode node = _caseMap.Get(stageCase, stage);
			if (node != null)
			{
				treeDialogue.SelectedNode = node;
			}
		}

		public string AddingCase()
		{
			DialogueNode node = treeDialogue.SelectedNode?.Tag as DialogueNode;
			if (_character == null || node == null)
			{
				return null;
			}

			if (node.Case == null)
			{
				return "";
			}

			return node?.Case?.Tag;
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
			List<Tuple<int, TreeNode>> nodesToRemove = new List<Tuple<int, TreeNode>>();
			foreach (KeyValuePair<int, TreeNode> kvp in _caseMap[modifiedCase])
			{
				int stage = kvp.Key;
				modifiedStages.Remove(stage);
				if (!modifiedCase.Stages.Contains(stage))
				{
					nodesToRemove.Add(new Tuple<int, TreeNode>(kvp.Key, kvp.Value));
				}
				else
				{
					UpdateNodeAppearance(kvp.Value);
				}
			}
			foreach (Tuple<int, TreeNode> kvp in nodesToRemove)
			{
				//TreeView will crash if trying to delete the node that is being unselected, so delay it to let the stack unwind
				DeleteNode?.Invoke(this, kvp.Item2);
				_caseMap.Remove(modifiedCase, kvp.Item1);
			}

			//Process stages that don't have nodes yet
			foreach (int stage in modifiedStages)
			{
				GenerateNode(modifiedCase, stage, true);
			}
		}

		public void AddCase(Case newCase)
		{
			GenerateNodes(newCase, true);
		}

		public void RemoveCase(Case removedCase)
		{
			if (!_caseMap.ContainsPrimaryKey(removedCase)) { return; }
			List<Tuple<int, TreeNode>> nodesToRemove = new List<Tuple<int, TreeNode>>();
			foreach (KeyValuePair<int, TreeNode> kvp in _caseMap[removedCase])
			{
				nodesToRemove.Add(new Tuple<int, TreeNode>(kvp.Key, kvp.Value));
			}
			foreach (Tuple<int, TreeNode> kvp in nodesToRemove)
			{
				//TreeView will crash if trying to delete the node that is being unselected, so delay it to let the stack unwind
				DeleteNode?.Invoke(this, kvp.Item2);
			}
			_caseMap.Remove(removedCase);
		}

		/// <summary>
		/// Separates the selected case into individual copies for each applicable stage
		/// </summary>
		private void SplitAllStages(object sender, EventArgs e)
		{
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			Case selectedCase = selectedNode.Case;
			int stage = selectedNode.Stage.Id;
			SaveNode?.Invoke(this, EventArgs.Empty);
			Case copy = _character.Behavior.DuplicateCase(selectedCase);
			SelectNode(stage, copy);
		}

		private void BulkReplace(object sender, EventArgs e)
		{
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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

		public bool IsTriggerValid(DialogueNode selectedNode, Trigger trigger)
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
					TreeNode node = _caseMap.Get(c, stage);
					if (node != null)
					{
						if (_showHidden)
						{
							UpdateNodeAppearance(node);
						}
						else
						{
							node.Remove();
							_caseMap.Remove(c, stage);
						}
					}
				}
			}
			else
			{
				foreach (int stage in c.Stages)
				{
					if (_showHidden)
					{
						//just update color since the node already exists
						TreeNode node = _caseMap.Get(c, stage);
						if (node != null)
						{
							UpdateNodeAppearance(node);
						}
					}
					else
					{
						//wait, how would you even unhide a node if it's not in the tree already? just ignore implementing this for now
					}
				}
			}
		}
	}
}

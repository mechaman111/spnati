using SPNATI_Character_Editor.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	/// <summary>
	/// View that orders the dialogue tree by stage where cases shared across stages have nodes in each stage
	/// </summary>
	public class CaseView : IDialogueTreeView
	{
		public event EventHandler<TreeNode> DeleteNode;
		public event EventHandler SaveNode;

		/// <summary>
		/// Mapping from tag to grouper node
		/// </summary>
		private Dictionary<string, TreeNode> _tagMap = new Dictionary<string, TreeNode>();

		/// <summary>
		/// Mapping from case to node
		/// </summary>
		private Dictionary<Case, TreeNode> _caseMap = new Dictionary<Case, TreeNode>();

		private TreeView treeDialogue;
		private ContextMenuStrip splitMenu;
		private Character _character;
		private CharacterEditorData _editorData;
		private Func<Case, bool> _filter;
		private bool _showHidden;

		public void Initialize(TreeView tree, Character character)
		{
			treeDialogue = tree;
			_character = character;
			_editorData = CharacterDatabase.GetEditorData(character);
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

		public void BuildTree(bool showHidden)
		{
			_showHidden = showHidden;
			treeDialogue.Nodes.Clear();

			_tagMap = new Dictionary<string, TreeNode>();

			//Make nodes for each case tag
			foreach (string tag in TriggerDatabase.GetTags())
			{
				if (tag == "-") { continue; }
				Trigger trigger = TriggerDatabase.GetTrigger(tag);
				TreeNode node = new TreeNode(trigger.Label);
				node.Tag = tag;
				treeDialogue.Nodes.Add(node);
				_tagMap[tag] = node;
			}

			//Add working cases to the right grouper
			foreach (Case workingCase in _character.Behavior.GetWorkingCases())
			{
				if (!showHidden && _editorData.IsHidden(workingCase)) { continue; }

				CreateCaseNode(workingCase);
			}

			treeDialogue.TreeViewNodeSorter = new NodeSorter();
			treeDialogue.Sort();
		}

		private void CreateCaseNode(Case workingCase)
		{
			//Exclude cases depending on filters. These are just excluded from the UI. This has no bearing on the actual underlying data
			if (_filter != null && !_filter(workingCase))
			{
				return;
			}

			string tag = workingCase.Tag;
			TreeNode grouper = _tagMap.Get(tag);
			if (grouper == null) { return; } //should we display a warning message here? The dialogue is using an unrecognized tag

			DialogueNode wrapper = new DialogueNode(_character, new Stage(workingCase.Stages[0]), workingCase);
			CreateNode(wrapper, grouper, false);
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
			node.Tag = wrapper;

			node.ContextMenuStrip = splitMenu;
			UpdateNodeAppearance(node);

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
			_caseMap[wrapper.Case] = node;
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

			//Prefix the case with the stage range			
			Case c = wrapper.Case;
			StringBuilder sb = new StringBuilder();
			if (c.Stages.Count == 0)
			{
				sb.Append("???");
			}
			else
			{
				int last = c.Stages[0];
				int startRange = last;
				for (int i = 1; i < c.Stages.Count; i++)
				{
					int stage = c.Stages[i];
					if (stage - 1 > last)
					{
						if (startRange == last)
						{
							sb.Append(startRange.ToString() + ",");
						}
						else
						{
							sb.Append($"{startRange}-{last},");
						}
						startRange = stage;
					}
					last = stage;
				}
				if (startRange == last)
				{
					sb.Append(startRange.ToString());
				}
				else
				{
					sb.Append($"{startRange}-{last}");
				}
				wrapper.Stage = new Stage(c.Stages[0]);
			}

			node.Text = $"{sb.ToString()} - {wrapper.ToString()}";
		}

		public void SetFilter(Func<Case, bool> filter)
		{
			_filter = filter;
			BuildTree(_showHidden);
		}

		public void SelectNode(int stage, Case stageCase)
		{
			TreeNode node = _caseMap.Get(stageCase);
			if(node != null)
			{
				treeDialogue.SelectedNode = node;
			}
		}

		public string AddingCase()
		{
			DialogueNode node = treeDialogue.SelectedNode?.Tag as DialogueNode;
			if (_character == null)
			{
				return null;
			}
			if (node == null)
			{
				string tag = treeDialogue.SelectedNode?.Tag?.ToString();
				if (!string.IsNullOrEmpty(tag))
				{
					return tag;
				}
			}

			return node?.Case?.Tag;
		}

		public void ModifyCase(Case modifiedCase)
		{
			//since all stages are in the same node, we don't need to do anything but update that node's appearance!
			TreeNode node = _caseMap.Get(modifiedCase);
			if (node != null)
			{
				UpdateNodeAppearance(node);
			}
		}

		public void AddCase(Case newCase)
		{
			TreeNode grouper = _tagMap.Get(newCase.Tag);
			DialogueNode wrapper = new DialogueNode(_character, new Stage(newCase.Stages[0]), newCase);
			CreateNode(wrapper, grouper, false);
		}

		public void RemoveCase(Case removedCase)
		{
			TreeNode node = _caseMap.Get(removedCase);
			if (node == null) { return; }
			//TreeView will crash if trying to delete the node that is being unselected, so delay it to let the stack unwind
			DeleteNode?.Invoke(this, node);
			_caseMap.Remove(removedCase);
		}

		private class NodeSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				TreeNode node1 = x as TreeNode;
				TreeNode node2 = y as TreeNode;

				DialogueNode caseNode1 = node1.Tag as DialogueNode;
				DialogueNode caseNode2 = node2.Tag as DialogueNode;
				if (caseNode1 != null)
				{
					//case nodes
					int stage1 = caseNode1.Case.Stages.Count > 0 ? caseNode1.Case.Stages[0] : -1;
					int stage2 = caseNode2.Case.Stages.Count > 0 ? caseNode2.Case.Stages[0] : -1;
					int diff = stage1.CompareTo(stage2);
					if (diff == 0)
					{
						diff = caseNode1.Case.Stages[caseNode1.Case.Stages.Count - 1].CompareTo(caseNode2.Case.Stages[caseNode2.Case.Stages.Count - 1]);
						if (diff == 0)
						{
							diff = caseNode2.Case.GetPriority().CompareTo(caseNode1.Case.GetPriority());
							if (diff == 0)
							{
								diff = node1.Text.CompareTo(node2.Text);
							}
						}
					}
					return diff;
				}
				else
				{
					//groupers
					string t1 = (string)node1.Tag;
					string t2 = (string)node2.Tag;
					return TriggerDatabase.Compare(t1, t2);
				}
			}
		}

		private void SeparateCaseFromStage(object sender, EventArgs e)
		{
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
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
			DialogueNode selectedNode = treeDialogue.SelectedNode?.Tag as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
				return;
			Case selectedCase = selectedNode.Case;
			int stage = selectedNode.Stage.Id;
			SaveNode?.Invoke(this, EventArgs.Empty);
			Case copy = _character.Behavior.DuplicateCase(selectedCase);
			SelectNode(stage, copy);
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

		public bool IsTriggerValid(DialogueNode selectedNode, Trigger trigger)
		{
			return true;
		}

		public void HideCase(Case c, bool hide)
		{
			if (hide)
			{
				TreeNode node = _caseMap.Get(c);
				if (node != null)
				{
					if (_showHidden)
					{
						UpdateNodeAppearance(node);
					}
					else
					{
						node.Remove();
						_caseMap.Remove(c);
					}
				}
			}
			else
			{
				if (_showHidden)
				{
					TreeNode node = _caseMap.Get(c);
					if (node != null)
					{
						UpdateNodeAppearance(node);
					}
				}
				else
				{
					//generate a node
					CreateCaseNode(c);
				}
			}
		}
	}
}

using Desktop.CommonControls;
using Desktop.Skinning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	/// <summary>
	/// Views that lets the user create their own groups and sort order
	/// </summary>
	public class FolderView : IDialogueTreeView
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
				column.Width = 31;
				column.TextAlign = HorizontalAlignment.Right;
				_listView.AddColumn(column);
			}

			_listView.RebuildColumns();
		}

		public ContextMenuStrip GetCopyMenu()
		{
			splitMenu = new ContextMenuStrip();
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
			_model.ItemComparer = SortCases;

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
			wrapper.Mode = NodeMode.Folder;
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
			CaseLabel label = _editorData.GetLabel(wrapper.Case);
			if (label != null && !string.IsNullOrEmpty(label.Folder) && label.SortId == 0)
			{
				//auto-assign a sort ID to foldered nodes
				_editorData.SetSortId(wrapper.Case, label.Folder, _model.GetGroupCount(label.Folder) + 1);
			}
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
					if (tag != "-")
					{
						folder = tag;
					}
					return null;
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
			}

			if (string.IsNullOrEmpty(newTag))
			{
				newTag = node?.Case?.Tag;
			}
			if (!string.IsNullOrEmpty(newTag))
			{
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
			wrapper.Mode = NodeMode.Folder;
			CreateNode(wrapper);
		}

		public void RemoveCase(Case removedCase)
		{
			DialogueNode node = _caseMap.Get(removedCase);
			if (node == null) { return; }
			_model.RemoveItem(node);
			_caseMap.Remove(removedCase);

			CaseLabel label = _editorData.GetLabel(removedCase);
			if (!string.IsNullOrEmpty(label?.Folder))
			{
				Resort(label.Folder);
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

			CaseLabel original = _editorData.GetLabel(selectedCase);
			CaseLabel label = _editorData.GetLabel(copy);
			if (original != null && label != null && !string.IsNullOrEmpty(original.Folder))
			{
				label.SortId = original.SortId + 1;
				Resort(original.Folder);
			}

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
			if (key1 == "-")
			{
				return 1;
			}
			else if (key2 == "-")
			{
				return -1;
			}
			return key1.CompareTo(key2);
		}

		public int SortCases(DialogueNode caseNode1, DialogueNode caseNode2)
		{
			CaseLabel label1 = _editorData.GetLabel(caseNode1.Case);
			CaseLabel label2 = _editorData.GetLabel(caseNode2.Case);
			if (label1 != null && label2 != null && label1.Folder == label2.Folder && label1.SortId > 0 && label2.SortId > 0)
			{
				return label1.SortId.CompareTo(label2.SortId);
			}
			return DialogueNode.CompareCases(caseNode1, caseNode2);
		}

		public void FormatRow(FormatRowEventArgs args)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			DialogueNode node = args.Model as DialogueNode;
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
						if (Path.GetFileNameWithoutExtension(line.Image) == template.Item1 && line.Text?.Trim() == template.Item2)
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

			string folder = args.Group.Key;
			Character c = CharacterDatabase.Get(folder);
			if (c == null)
			{
				if (folder == "-")
				{
					args.Label = "- unsorted -";
				}
			}
			else
			{
				args.Label = c.ToLookupString();
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
			return true;
		}

		public void MoveItem(object source, object target, bool before)
		{
			DialogueNode src = source as DialogueNode;
			if (src == null)
			{
				return;
			}

			CaseLabel oldLabel = _editorData.GetLabel(src.Case);
			string oldFolder = oldLabel?.Folder;

			if (target is GroupedListGrouper)
			{
				//Dragged onto a folder. Give the case that name
				GroupedListGrouper group = target as GroupedListGrouper;
				SaveNode?.Invoke(this, EventArgs.Empty);
				string folder = group.Key == "-" ? null : group.Key;

				if (string.IsNullOrEmpty(folder))
				{
					_editorData.SetSortId(src.Case, null, -1);
				}
				else
				{
					InsertAndResort(src.Case, folder);
				}
				SelectNode(-1, src.Case);
			}
			else
			{
				SaveNode?.Invoke(this, EventArgs.Empty);

				DialogueNode targetNode = target as DialogueNode;
				int sortId = _editorData.GetSortId(targetNode.Case);
				if (before)
				{
					sortId--;
				}
				else
				{
					sortId++;
				}
				CaseLabel targetLabel = _editorData.GetLabel(targetNode.Case);
				string targetFolder = targetLabel?.Folder;

				if (string.IsNullOrEmpty(targetFolder))
				{
					_editorData.SetSortId(src.Case, null, -1);
				}
				else
				{
					_editorData.SetSortId(src.Case, targetFolder, sortId);
					Resort(targetFolder);
				}
				SelectNode(-1, src.Case);
			}

			//Update IDs of the old folder if there is one
			if (!string.IsNullOrEmpty(oldFolder))
			{
				Resort(oldFolder);
			}

			//force the tree to update groups
			SaveNode?.Invoke(this, EventArgs.Empty);
			Sort();
			SelectNode(-1, src.Case);
		}

		private void InsertAndResort(Case workingCase, string folder)
		{
			List<CaseLabel> cases = _editorData.GetCasesInFolder(folder);
			int nextId = cases.Count * 2 + 1;
			CaseLabel existing = cases.Find(c => c.Id == workingCase.Id);
			CaseLabel label = _editorData.SetSortId(workingCase, folder, nextId);
			if (existing == null)
			{
				cases.Add(label);
			}
			cases.Sort((l1, l2) => l1.SortId.CompareTo(l2.SortId));

			//rebuild sort indices
			int id = 2;
			foreach (CaseLabel c in cases)
			{
				c.SortId = id;
				id += 2;
			}
		}

		private void Resort(string folder)
		{
			List<CaseLabel> cases = _editorData.GetCasesInFolder(folder);
			int id = 2;
			foreach (CaseLabel c in cases)
			{
				c.SortId = id;
				id += 2;
			}
		}
	}
}

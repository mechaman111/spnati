using Desktop;
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
	/// View that orders tree by pose names
	/// </summary>
	public class PoseView : IDialogueTreeView
	{
		public event EventHandler SaveNode;

		private Font _font = new Font("Arial", 9, FontStyle.Bold);
		private GroupedList<DialogueNode> _model = new GroupedList<DialogueNode>();

		//Mapping from pose key + case to node
		private DualKeyDictionary<Case, string, DialogueNode> _caseMap = new DualKeyDictionary<Case, string, DialogueNode>();

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
			_model.ItemComparer = DialogueNode.CompareCases;

			foreach (Case workingCase in _character.Behavior.GetWorkingCases())
			{
				if (!_showHidden && _editorData.IsHidden(workingCase))
				{
					continue;
				}

				if (_filter != null && _filter(workingCase))
				{
					continue;
				}

				//create a node for each pose that appears in it
				GenerateNodes(workingCase);
			}

			_model.Sorted = true;
			_listView.DataSource = _model;
		}

		private void GenerateNodes(Case workingCase)
		{
			foreach (DialogueLine line in workingCase.Lines)
			{
				string pose = GetPoseKey(line);
				GenerateNode(workingCase, pose);
			}
		}

		private void GenerateNode(Case workingCase, string poseKey)
		{
			if (_caseMap.ContainsKey(workingCase, poseKey))
			{
				return; //same pose could appear on multiple lines; only include the first
			}

			DialogueNode node = new DialogueNode(_character, new Stage(workingCase.Stages[0]), workingCase);
			node.Mode = NodeMode.Pose;
			node.GroupKey = poseKey;
			_caseMap.Set(workingCase, poseKey, node);
			_model.AddItem(node);
		}

		public void SetFilter(Func<Case, bool> filter)
		{
			_filter = filter;
			BuildTree(_showHidden);
		}

		public bool SelectNode(int stage, Case stageCase)
		{
			foreach (DialogueLine line in stageCase.Lines)
			{
				string poseKey = GetPoseKey(line);
				DialogueNode node = _caseMap.Get(stageCase, poseKey);
				if (node != null)
				{
					_listView.SelectedItem = node;
					return true;
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

			//reconcile any pose differences between the real case and what we have cached
			HashSet<string> modifiedPoses = new HashSet<string>();
			foreach (DialogueLine line in modifiedCase.Lines)
			{
				string pose = GetPoseKey(line);
				modifiedPoses.Add(pose);
			}

			//process poses that no longer exist
			List<Tuple<string, DialogueNode>> nodesToRemove = new List<Tuple<string, DialogueNode>>();
			foreach (KeyValuePair<string, DialogueNode> kvp in _caseMap[modifiedCase])
			{
				string poseKey = kvp.Key;
				modifiedPoses.Remove(poseKey);
				if (modifiedCase.Lines.Find(l => GetPoseKey(l) == poseKey) == null)
				{
					nodesToRemove.Add(new Tuple<string, DialogueNode>(poseKey, kvp.Value));
				}
			}
			foreach (Tuple<string, DialogueNode> kvp in nodesToRemove)
			{
				_model.RemoveItem(kvp.Item2);
				_caseMap.Remove(modifiedCase, kvp.Item1);
			}

			//Process poses that don't have nodes yet
			foreach (string poseKey in modifiedPoses)
			{
				GenerateNode(modifiedCase, poseKey);
			}
		}

		public void AddCase(Case newCase)
		{
			GenerateNodes(newCase);
		}

		public void RemoveCase(Case removedCase)
		{
			if (!_caseMap.ContainsPrimaryKey(removedCase)) { return; }
			List<Tuple<string, DialogueNode>> nodesToRemove = new List<Tuple<string, DialogueNode>>();
			foreach (KeyValuePair<string, DialogueNode> kvp in _caseMap[removedCase])
			{
				nodesToRemove.Add(new Tuple<string, DialogueNode>(kvp.Key, kvp.Value));
			}
			foreach (Tuple<string, DialogueNode> kvp in nodesToRemove)
			{
				_model.RemoveItem(kvp.Item2);
			}
			_caseMap.Remove(removedCase);
		}

		private void DeleteCase(object sender, EventArgs e)
		{
			DialogueNode selectedNode = _listView.SelectedItem as DialogueNode;
			if (_character == null || selectedNode?.Case == null)
			{
				return;
			}

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

		public bool IsTriggerValid(DialogueNode selectedNode, TriggerDefinition trigger)
		{
			return true;
		}

		public void HideCase(Case c, bool hide)
		{
			HashSet<string> visited = new HashSet<string>();
			if (hide)
			{
				if (!_showHidden)
				{
					foreach (DialogueLine line in c.Lines)
					{
						string poseKey = GetPoseKey(line);
						if (visited.Contains(poseKey))
						{
							continue;
						}
						DialogueNode node = _caseMap.Get(c, poseKey);
						if (node != null)
						{
							visited.Add(poseKey);
							_model.RemoveItem(node);
							_caseMap.Remove(c, poseKey);
						}
					}
				}
			}
			else
			{
				if (_showHidden)
				{
					foreach (DialogueLine line in c.Lines)
					{
						string poseKey = GetPoseKey(line);
						if (visited.Contains(poseKey))
						{
							continue;
						}
						DialogueNode node = _caseMap.Get(c, poseKey);
						if (node != null)
						{
							visited.Add(poseKey);
							node.Dummy++;
						}
					}
				}
			}
		}

		public int SortGroups(string key1, string key2)
		{
			return key1.CompareTo(key2);
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

		private static string GetPoseKey(DialogueLine line)
		{
			return line.Pose?.Key ?? "_";
		}

		public void FormatGroup(FormatGroupEventArgs args)
		{
			args.Font = _font;
			args.Label = args.Group.Key == "_" ? "- unassigned -" : args.Group.Key;
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

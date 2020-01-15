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
	/// View that orders the dialogue tree by target
	/// </summary>
	public class TargetView : IDialogueTreeView
	{
		public event EventHandler SaveNode;

		private GroupedList<DialogueNode> _model = new GroupedList<DialogueNode>();

		private Font _font = new Font("Arial", 9, FontStyle.Bold);

		/// <summary>
		/// Mapping from target+case to node
		/// </summary>
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
			_editorData = CharacterDatabase.GetEditorData(character);

			InitializeColumns();
		}

		private void InitializeColumns()
		{
			AccordionColumn column;
			column = new AccordionColumn("Stage", "TargetStage");
			column.Width = 40;
			_listView.AddColumn(column);

			column = new AccordionColumn("Tag", "Tag");
			column.Width = 100;
			_listView.AddColumn(column);

			column = new AccordionColumn("Conditions", "Conditions");
			column.FillWeight = 1;
			_listView.AddColumn(column);

			column = new AccordionColumn("Pri", "Priority");
			column.Width = 31;
			column.TextAlign = HorizontalAlignment.Right;
			_listView.AddColumn(column);

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

				GenerateNodes(workingCase);
			}

			_model.Sorted = true;
			_listView.DataSource = _model;
		}

		private static int SortCases(DialogueNode c1, DialogueNode c2)
		{
			int compare = c1.TargetStage.CompareTo(c2.TargetStage);
			if (compare == 0)
			{
				compare = DialogueNode.CompareCases(c1, c2);
			}
			return compare;
		}

		private void GenerateNodes(Case workingCase)
		{
			bool added = false;
			foreach (string target in workingCase.GetTargets())
			{
				added = true;
				GenerateNode(workingCase, target);
			}
			if (!added)
			{
				GenerateNode(workingCase, "");
			}
		}

		private void GenerateNode(Case workingCase, string target)
		{
			//Exclude cases depending on filters. These are just excluded from the UI. This has no bearing on the actual underlying data
			if (_filter != null && !_filter(workingCase))
			{
				return;
			}

			DialogueNode wrapper = new DialogueNode(_character, new Stage(workingCase.Stages[0]), workingCase);
			wrapper.TargetCharacter = CharacterDatabase.Get(target);
			wrapper.Mode = NodeMode.Target;
			_model.AddItem(wrapper);
			_caseMap.Set(workingCase, target, wrapper);
		}

		public void SetFilter(Func<Case, bool> filter)
		{
			_filter = filter;
			BuildTree(_showHidden);
		}

		public bool SelectNode(int stage, Case stageCase)
		{
			foreach (string target in stageCase.GetTargets())
			{
				DialogueNode node = _caseMap.Get(stageCase, target);
				if (node != null)
				{
					_listView.SelectedItem = node;
					return true;
				}
			}
			DialogueNode untargetedNode = _caseMap.Get(stageCase, "");
			_listView.SelectedItem = untargetedNode;
			return untargetedNode != null;
		}

		public void BuildCase(Case theCase)
		{
			TriggerDefinition def = TriggerDatabase.GetTrigger(theCase.Tag);
			if (def != null)
			{
				DialogueNode node = _listView.SelectedItem as DialogueNode;
				string target = node?.GetGroupKey();
				if (!string.IsNullOrEmpty(target))
				{
					target = target.Split('>')[0];
				}
				if (node == null)
				{
					GroupedListGrouper group = _listView.SelectedItem as GroupedListGrouper;
					target = group?.RootKey ?? group?.Key?.ToString();
				}
				if (!string.IsNullOrEmpty(target))
				{
					if (def.HasTarget)
					{
						theCase.Target = target;
					}
					else
					{
						theCase.AlsoPlaying = target;
					}
				}
			}
		}

		public Case AddingCase(out string folder)
		{
			string newTag = null;
			string groupTarget = null;
			folder = "";
			if (_character == null)
			{
				return null;
			}
			DialogueNode node = _listView.SelectedItem as DialogueNode;
			if (node == null)
			{
				GroupedListGrouper group = _listView.SelectedItem as GroupedListGrouper;
				string target = group?.RootKey ?? group?.Key?.ToString();
				if (!string.IsNullOrEmpty(target))
				{
					if (target != group?.Key?.ToString())
					{
						folder = group?.Key?.ToString();
					}
					newTag = "hand";
					groupTarget = target;
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
				groupTarget = node?.GetGroupKey();
			}
			if (!string.IsNullOrEmpty(newTag))
			{
				Case c = new Case(newTag);
				if (!string.IsNullOrEmpty(groupTarget))
				{
					groupTarget = groupTarget.Split('>')[0];
					if (node != null)
					{
						CopyTarget(node.Case, c, groupTarget);
					}
					else
					{
						c.AlsoPlaying = groupTarget;
					}
				}
				return c;
			}
			return null;
		}

		/// <summary>
		/// Copies any property containing the target from source to dest
		/// </summary>
		/// <param name="source"></param>
		/// <param name="dest"></param>
		/// <param name="target"></param>
		private void CopyTarget(Case source, Case dest, string target)
		{
			if (source.Target == target)
			{
				dest.Target = target;
				dest.TargetStage = source.TargetStage;
			}
			if (source.AlsoPlaying == target)
			{
				dest.AlsoPlaying = target;
				dest.AlsoPlayingStage = source.AlsoPlayingStage;
			}
			string id = CharacterDatabase.GetId(target);
			if (source.Filter == id)
			{
				dest.Filter = id;
			}
			foreach (TargetCondition cond in source.Conditions)
			{
				if (cond.Character == target)
				{
					TargetCondition copy = new TargetCondition();
					copy.Role = cond.Role;
					copy.Character = target;
					copy.Stage = cond.Stage;
					dest.Conditions.Add(copy);
				}
				else if (cond.FilterTag == id)
				{
					TargetCondition copy = new TargetCondition();
					copy.Role = cond.Role;
					copy.FilterTag = id;
					copy.Stage = cond.Stage;
					dest.Conditions.Add(copy);
				}
			}
			foreach (Case alt in source.AlternativeConditions)
			{
				CopyTarget(alt, dest, target);
			}			
		}

		public void ModifyCase(Case modifiedCase)
		{
			if (!_caseMap.ContainsPrimaryKey(modifiedCase))
			{
				//this could happen when deleting a node
				return;
			}

			//reconcile any target differences between the real case and what we have cached
			HashSet<string> modifiedTargets = new HashSet<string>();
			foreach (string target in modifiedCase.GetTargets())
			{
				modifiedTargets.Add(target);
			}
			if (modifiedTargets.Count == 0)
			{
				modifiedTargets.Add("");
			}

			//Process targets that no longer exist
			List<Tuple<string, DialogueNode>> nodesToRemove = new List<Tuple<string, DialogueNode>>();
			foreach (KeyValuePair<string, DialogueNode> kvp in _caseMap[modifiedCase])
			{
				string target = kvp.Key;
				if (!modifiedTargets.Contains(target))
				{
					nodesToRemove.Add(new Tuple<string, DialogueNode>(kvp.Key, kvp.Value));
				}
				modifiedTargets.Remove(target);
			}
			foreach (Tuple<string, DialogueNode> kvp in nodesToRemove)
			{
				_model.RemoveItem(kvp.Item2);
				_caseMap.Remove(modifiedCase, kvp.Item1);
			}

			//process targets that don't have nodes yet
			foreach (string target in modifiedTargets)
			{
				GenerateNode(modifiedCase, target);
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

		public void FormatGroup(FormatGroupEventArgs args)
		{
			args.Font = _font;
			string folder = args.Group.Key;
			Character c = CharacterDatabase.Get(folder);
			if (c == null)
			{
				if (folder == "-")
				{
					args.Label = "- untargeted -";
				}
			}
			else
			{
				args.Label = c.ToLookupString();
			}
		}

		private Color GetGroupColor(string key, int index)
		{
			TriggerDefinition trigger = TriggerDatabase.GetTrigger(key);
			if (string.IsNullOrEmpty(trigger.Label))
			{
				return index % 2 == 0 ? SkinManager.Instance.CurrentSkin.PrimaryForeColor : SkinManager.Instance.CurrentSkin.SecondaryForeColor;
			}
			return SkinManager.Instance.CurrentSkin.GetGrouper(trigger.ColorScheme);
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
			else if (node.Case.Hidden == "1")
			{
				args.ForeColor = skin.Gray;
			}
			else if (node.Case.HasCollectible)
			{
				args.ForeColor = skin.Orange;
			}
			else if ((Config.ColorTargetedLines || Config.UseSimpleTree) && node.Case.HasTargetedConditions)
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

		public void HideCase(Case theCase, bool hide)
		{
			if (hide)
			{
				foreach (string target in theCase.GetTargets())
				{
					DialogueNode node = _caseMap.Get(theCase, target);
					if (node != null)
					{
						if (!_showHidden)
						{
							_model.RemoveItem(node);
							_caseMap.Remove(theCase, target);
						}
					}
				}
			}
			else
			{
				if (_showHidden)
				{
					foreach (string target in theCase.GetTargets())
					{
						DialogueNode node = _caseMap.Get(theCase, target);
						if (node != null)
						{
							node.Dummy++;
						}
					}
				}
			}
		}

		public bool IsTriggerValid(DialogueNode selectedNode, TriggerDefinition trigger)
		{
			return true;
		}

		public ContextMenuStrip ShowContextMenu(AccordionListViewEventArgs args)
		{
			if (args.Model != null)
			{
				return splitMenu;
			}
			return null;
		}

		public int SortGroups(string key1, string key2)
		{
			return key1.CompareTo(key2);
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

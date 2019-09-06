using Desktop.CommonControls;
using Desktop.DataStructures;
using System;
using System.Text;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Helper class for tagging tree nodes to particular cases
	/// </summary>
	public class DialogueNode : BindableObject, IGroupedItem, IComparable<DialogueNode>
	{
		public NodeType NodeType;
		public NodeMode Mode;

		public int Dummy
		{
			get { return Get<int>(); }
			set { Set(value); }
		}

		public Character Character;
		private CharacterEditorData _editorData;

		public Stage Stage;
		public Case Case
		{
			get { return Get<Case>(); }
			set { Set(value); }
		}

		public string Tag
		{
			get
			{
				TriggerDefinition trigger = TriggerDatabase.GetTrigger(Case.Tag);
				return trigger?.Label ?? Case.Tag;
			}
		}

		public string Label
		{
			get
			{
				return $"{StageRange} - {Case.ToString()}";
			}
		}

		public string CustomLabel
		{
			get
			{
				CaseLabel label = _editorData?.GetLabel(Case);
				if (label != null && !string.IsNullOrEmpty(label.Text))
				{
					return label.Text;
				}
				return "";
			}
		}

		public string Conditions
		{
			get
			{
				Case workingCase = Case;
				string label = CustomLabel;
				if (!string.IsNullOrEmpty(label))
				{
					return label;
				}

				string conditions = workingCase.ToConditionsString(true);
				if (string.IsNullOrEmpty(conditions))
				{
					return "-";
				}
				return conditions;
			}
		}

		public string StageRange
		{
			get
			{
				Case c = Case;
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
				}
				return sb.ToString();
			}
		}

		public string Target
		{
			get
			{
				if (!string.IsNullOrEmpty(Case.Target))
				{
					return GetCharacterName(Case.Target);
				}
				if (!string.IsNullOrEmpty(Case.AlsoPlaying))
				{
					return GetCharacterName(Case.AlsoPlaying);
				}
				return "-";
			}
		}

		private string GetCharacterName(string key)
		{
			Character c = CharacterDatabase.Get(key);
			if (c == null)
			{
				return key;
			}
			return c.Label;
		}

		public string Priority
		{
			get
			{
				if (Case.Hidden == "1")
				{
					return "-";
				}
				else if (!string.IsNullOrEmpty(Case.CustomPriority))
				{
					return "*" + Case.CustomPriority;
				}
				return Case.GetPriority().ToString();
			}
		}

		public DialogueNode(Character character, Stage stage, Case stageCase)
		{
			Character = character;
			_editorData = CharacterDatabase.GetEditorData(character);
			Stage = stage;
			Case = stageCase;
		}

		public override string ToString()
		{
			return string.Format("{0}", Case.ToString());
		}

		public string GetGroupKey()
		{
			string key = "";
			if (Mode == NodeMode.Stage)
			{
				key = Stage.Id.ToString();
			}
			else
			{
				key = Case.Tag;
			}
			if (_editorData != null && Case.Id > 0)
			{
				CaseLabel label = _editorData.GetLabel(Case);
				if (label != null && !string.IsNullOrEmpty(label.Folder))
				{
					key += ">" + label.Folder;
				}
			}
			return key;
		}

		public int CompareTo(DialogueNode other)
		{
			return Case.CompareTo(other.Case);
		}

		public static int CompareCases(DialogueNode caseNode1, DialogueNode caseNode2)
		{
			string tag1 = caseNode1.Case.Tag;
			string tag2 = caseNode2.Case.Tag;
			int diff = TriggerDatabase.Compare(tag1, tag2);

			if (diff == 0)
			{
				int stage1 = caseNode1.Case.Stages.Count > 0 ? caseNode1.Case.Stages[0] : -1;
				int stage2 = caseNode2.Case.Stages.Count > 0 ? caseNode2.Case.Stages[0] : -1;
				diff = stage1.CompareTo(stage2);
				if (diff == 0)
				{
					diff = caseNode1.Case.Stages[caseNode1.Case.Stages.Count - 1].CompareTo(caseNode2.Case.Stages[caseNode2.Case.Stages.Count - 1]);
					if (diff == 0)
					{
						diff = caseNode2.Case.GetPriority().CompareTo(caseNode1.Case.GetPriority());
						if (diff == 0)
						{
							diff = caseNode1.Label.CompareTo(caseNode2.Label);
						}
					}
				}
			}
			return diff;
		}
	}

	public enum NodeMode
	{
		Case,
		Stage
	}

	public enum NodeType
	{
		Stage,
		Case,
		Start
	}
}

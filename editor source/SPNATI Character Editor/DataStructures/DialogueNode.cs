using Desktop;
using System;

namespace SPNATI_Character_Editor
{
	/// <summary>
	/// Helper class for tagging tree nodes to particular cases
	/// </summary>
	public class DialogueNode
	{
		public NodeType NodeType;

		public Character Character;

		public Stage Stage;
		public Case Case;

		public DialogueNode(Character character)
		{
			Character = character;
			NodeType = NodeType.Start;
		}

		public DialogueNode(Character character, Stage stage)
		{
			Character = character;
			NodeType = NodeType.Stage;
			Stage = stage;
		}

		public DialogueNode(Character character, Stage stage, Case stageCase)
		{
			Character = character;
			NodeType = NodeType.Case;
			Stage = stage;
			Case = stageCase;
		}

		public override string ToString()
		{
			switch (NodeType)
			{
				case NodeType.Start:
					return "Starting Lines";
				case NodeType.Stage:
					return string.Format("Stage: {0} ({1})", Character.LayerToStageName(Stage.Id), Stage.Id);
				case NodeType.Case:
					return string.Format("{0}", Case.ToString());
				default:
					return "Unknown node";
			}
		}
	}

	public enum NodeType
	{
		Stage,
		Case,
		Start
	}
}

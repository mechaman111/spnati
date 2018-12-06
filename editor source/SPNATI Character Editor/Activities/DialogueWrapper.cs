using Desktop;

namespace SPNATI_Character_Editor.Activities
{
	public partial class DialogueEditor : Activity
	{
		/// <summary>
		/// Helper class for tagging tree nodes to particular cases
		/// </summary>
		private class DialogueWrapper
		{
			public NodeType NodeType;

			public Character Character;

			public Stage Stage;
			public Case Case;

			public DialogueWrapper(Character character)
			{
				Character = character;
				NodeType = NodeType.Start;
			}

			public DialogueWrapper(Character character, Stage stage)
			{
				Character = character;
				NodeType = NodeType.Stage;
				Stage = stage;
			}

			public DialogueWrapper(Character character, Case stageCase)
			{
				Character = character;
				NodeType = NodeType.Case;
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

		private enum NodeType
		{
			Stage,
			Case,
			Line,
			Start
		}
	}
}

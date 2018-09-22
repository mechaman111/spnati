using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	/// <summary>
	/// Character instance for the dialogue tester. Manages game state per individual
	/// </summary>
	public partial class TestCharacter : UserControl
	{
		private ImageLibrary _imageLibrary;
		public CharacterState State = new CharacterState();

		public event EventHandler<CharacterState> StateChanged;
		private Random _random;

		public TestCharacter()
		{
			_random = new Random();
			InitializeComponent();
			PopulateCharacterCombo();
		}

		private void PopulateCharacterCombo()
		{
			cboCharacter.DataSource = CharacterDatabase.Characters;
			cboCharacter.BindingContext = new BindingContext();
		}

		private void cboCharacter_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetCharacterInternal(cboCharacter.SelectedItem as Character);
		}

		public void SetCharacter(Character character)
		{
			cboCharacter.SelectedItem = character;
		}

		private void SetCharacterInternal(Character character)
		{
			State.Character = character;
			if (character == null)
				return;
			character.OnAfterDeserialize();
			_imageLibrary = new ImageLibrary();
			_imageLibrary.Load(character);
			SetImage(character.Metadata.Portrait);
			lblName.Text = character.Label;
			PopulateStageCombo();
			StateChanged?.Invoke(this, State);
		}

		private void SetImage(string name)
		{
			if (!Config.DisplayImages)
				return;
			var image = _imageLibrary.Find(name);
			picPortrait.Image = image?.Image;
		}

		private void PopulateStageCombo()
		{
			if (State.Character == null)
				return;
			cboStage.Items.Clear();
			int layers = State.Character.Layers + Clothing.ExtraStages;
			for (int i = 0; i < layers; i++)
			{
				StageName stage = State.Character.LayerToStageName(i);
				cboStage.Items.Add(stage);
			}
			if (cboStage.Items.Count > 0)
				cboStage.SelectedIndex = 0;
		}

		private void cboStage_SelectedIndexChanged(object sender, EventArgs e)
		{
			var stage = cboStage.SelectedItem as StageName;
			if (stage != null)
			{
				State.Stage = int.Parse(stage.Id);
				StateChanged?.Invoke(this, State);
			}
		}

		private void cboHand_SelectedIndexChanged(object sender, EventArgs e)
		{
			string hand = cboHand.Text;
			if (hand == "")
				hand = null;
			State.Hand = hand;
			StateChanged?.Invoke(this, State);
		}

		private void valTime_ValueChanged(object sender, EventArgs e)
		{
			int time = (int)valTime.Value;
			State.TimeInStage = time;
			StateChanged?.Invoke(this, State);
		}

		private void valLosses_ValueChanged(object sender, EventArgs e)
		{
			int time = (int)valLosses.Value;
			State.Losses = time;
			StateChanged?.Invoke(this, State);
		}

		public void UpdateDisplay(string image, string text)
		{
			SetImage(image);
			lblText.Text = text;
		}
		
		/// <summary>
		/// Updates the image and dialogue based on the game state
		/// </summary>
		/// <param name="trigger"></param>
		/// <param name="state"></param>
		public void UpdateState(GameState state)
		{
			Character character = State.Character;
			if (character == null || state == null)
				return;

			int stageId = state.Phase == GamePhase.AfterLoss && State == state.TargetState ? State.Stage + 1 : State.Stage;

			List<DialogueLine> availableLines = new List<DialogueLine>();
			picPortrait.Visible = true;
			lblText.Visible = true;
			if (state.Phase == GamePhase.AfterLoss)
			{
				state.TargetState.Stage++;
			}
			List<Case> availableCases = State.GetPossibleCases(state);
			if (state.Phase == GamePhase.AfterLoss)
			{
				state.TargetState.Stage--;
			}

			if (availableCases.Count > 0)
			{
				availableCases.Sort();
				int topPriority = availableCases[0].GetPriority();
				availableLines.AddRange(availableCases[0].Lines);
				for (int i = 1; i < availableCases.Count; i++)
				{
					Case c = availableCases[i];
					if (c.GetPriority() == topPriority)
					{
						availableLines.AddRange(c.Lines);
					}
					else break;
				}
			}
			bool usingLegacyStart = false;
			if (availableLines.Count == 0)
			{
				if (state.Phase == GamePhase.Selected || state.Phase == GamePhase.Start)
				{
					stageId = 0;
					availableLines.AddRange(character.StartingLines);
					usingLegacyStart = true;
				}
				else
				{
					lblText.Text = "I have no lines that meet the current criteria!";
					return;
				}
			}
			int index = _random.Next(availableLines.Count);
			DialogueLine line = availableLines[index];
			if (usingLegacyStart) {
				SetImage(line.Image);
			}
			else
			{
				SetImage(Behaviour.CreateStageSpecificLine(line, stageId, character).Image);
			}
			string text = line.Text;
			//Fill in variables
			Regex varRegex = new Regex(@"~\w*~", RegexOptions.IgnoreCase);
			text = varRegex.Replace(text, (match) =>
			{
				return state.GetVariable(match.Value.Substring(1, match.Value.Length - 2));
			});
			lblText.Text = text;
		}
	}
}

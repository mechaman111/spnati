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
		private GameState _gameState;

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
			_imageLibrary.Load(character.FolderName);
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

			Trigger trigger = GetTrigger(state);

			_gameState = state;
			int stageId = state.Phase == GamePhase.AfterLoss && State == state.TargetState ? State.Stage + 1 : State.Stage;

			List<DialogueLine> availableLines = new List<DialogueLine>();
			List<Case> availableCases = new List<Case>();
			if (trigger == null)
			{
				picPortrait.Visible = false;
				lblText.Visible = false;
				return;
			}
			else
			{
				picPortrait.Visible = true;
				lblText.Visible = true;
			}
			if (trigger.Tag == Trigger.StartTrigger)
			{
				stageId = 0;
				availableLines.AddRange(character.StartingLines);
			}
			else
			{
				Stage stage = character.Behavior.Stages.Find(s => s.Id == stageId);
				List<Case> cases = new List<Case>();
				if (stage != null)
				{
					cases = stage.Cases.FindAll(c => c.Tag == trigger.Tag);
				}

				foreach (Case possibleCase in cases)
				{
					//Filters
					string target = state.Target;
					if (trigger.HasTarget)
					{
						if (!string.IsNullOrEmpty(possibleCase.Target) && target != possibleCase.Target)
							continue;   //Target doesn't match

						if (!string.IsNullOrEmpty(possibleCase.TargetStage) && state.TargetState.Stage.ToString() != possibleCase.TargetStage)
							continue; //Target stage doesn't match

						if (!string.IsNullOrEmpty(possibleCase.Filter))
						{
							if (!state.TargetState.Character.Tags.Contains(possibleCase.Filter) && state.Filter != possibleCase.Filter)
								continue; //Filter doesn't match
						}
						if (!string.IsNullOrEmpty(possibleCase.TargetHand))
						{
							if (state.TargetState.Hand != possibleCase.TargetHand)
								continue;
						}
					}
					if (!string.IsNullOrEmpty(possibleCase.AlsoPlaying))
					{
						CharacterState alsoPlaying = state.IsPlaying(possibleCase.AlsoPlaying);
						if (alsoPlaying == null || alsoPlaying == State || possibleCase.AlsoPlaying == target)
							continue;
						if (!string.IsNullOrEmpty(possibleCase.AlsoPlayingStage) && possibleCase.AlsoPlayingStage != alsoPlaying.Stage.ToString())
							continue;
						if (!string.IsNullOrEmpty(possibleCase.AlsoPlayingHand))
						{
							if (alsoPlaying.Hand != possibleCase.AlsoPlayingHand)
								continue;
						}
					}
					if (!string.IsNullOrEmpty(possibleCase.HasHand))
					{
						if (State.Hand != possibleCase.HasHand)
							continue;
					}

					availableCases.Add(possibleCase);
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
				if (availableLines.Count == 0)
				{
					lblText.Text = "I have no lines that meet the current criteria!";
					return;
				}
			}

			int index = _random.Next(availableLines.Count);
			DialogueLine line = availableLines[index];
			if (trigger.Tag == Trigger.StartTrigger)
			{
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
				return _gameState.GetVariable(match.Value.Substring(1, match.Value.Length - 2));
			});
			lblText.Text = text;
		}

		/// <summary>
		/// Gets the appropriate trigger for this phase and target
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		private Trigger GetTrigger(GameState state)
		{
			bool isTarget = (state.TargetState != null && state.TargetState == State);
			string tag = "";
			string gender = state.TargetState?.Character?.Gender;
			int standardStage = 0;
			if (state.TargetState != null)
			{
				standardStage = TriggerDatabase.ToStandardStage(state.TargetState.Character, state.TargetState.Stage);
			}
			else
			{
				standardStage = TriggerDatabase.ToStandardStage(State.Character, State.Stage);
				isTarget = true;
			}
			switch (state.Phase)
			{
				case GamePhase.Start:
					tag = Trigger.StartTrigger;
					break;
				case GamePhase.ExchangingCards:
					if (standardStage < 9)
					{
						tag = "swap_cards";
					}
					else if (standardStage == 9)
					{
						tag = "masturbating";
					}
					else if (standardStage == 10)
					{
						tag = "finished_masturbating";
					}
					break;
				case GamePhase.GoodHand:
					if (standardStage < 9)
					{
						tag = "good_hand";
					}
					else if (standardStage == 9)
					{
						tag = "masturbating";
					}
					else if (standardStage == 10)
					{
						tag = "finished_masturbating";
					}
					break;
				case GamePhase.OkayHand:
					if (standardStage < 9)
					{
						tag = "okay_hand";
					}
					else if (standardStage == 9)
					{
						tag = "masturbating";
					}
					break;
				case GamePhase.BadHand:
					if (standardStage < 9)
					{
						tag = "bad_hand";
					}
					else if (standardStage == 9)
					{
						tag = "masturbating";
					}
					break;
				case GamePhase.BeforeLoss:
					bool winning = true;
					bool losing = true;
					if (standardStage < 8)
					{
						int stage = State.Stage;
						if (stage >= State.Character.Layers)
							stage = 100 + (stage - State.Character.Layers);
						foreach (var character in state.Characters)
						{
							if (character != State)
							{
								int otherStage = character.Stage;
								if (otherStage >= character.Character.Layers)
									otherStage = 100 + (otherStage - character.Character.Layers);
								if (otherStage < stage)
									winning = false;
								else if (otherStage > stage)
									losing = false;
							}
						}

						if (isTarget)
						{
							if (winning)
								tag = "must_strip_winning";
							else if (losing)
								tag = "must_strip_losing";
							else tag = "must_strip_normal";
						}
						else
						{
							tag = string.Format("{0}_must_strip", gender);
						}
					}
					else if (standardStage == 8)
					{
						if (isTarget)
						{
							bool first = true;
							foreach (var character in state.Characters)
							{
								if (character != State)
								{
									if (TriggerDatabase.ToStandardStage(character.Character, character.Stage) >= 9)
									{
										first = false;
										break;
									}
								}
							}
							if (first)
							{
								tag = "must_masturbate_first";
							}
							else
							{
								tag = "must_masturbate";
							}
						}
						else
						{
							tag = string.Format("{0}_must_masturbate", gender);
						}
					}
					break;
				case GamePhase.DuringLoss:
					if (standardStage < 8)
					{
						if (isTarget)
						{
							tag = "stripping";
						}
						else
						{
							bool isFemale = (gender == "female");
							string layerType = state.Clothing.Type;
							if (layerType == "important")
							{
								string position = state.Clothing.Position;
								if (position == "upper")
								{
									tag = string.Format("{0}_chest_will_be_visible", gender);
								}
								else if (position == "lower")
								{
									tag = string.Format("{0}_crotch_will_be_visible", gender);
								}
							}
							else
							{
								if (layerType == "extra")
									layerType = "accessory";
								tag = string.Format("{0}_removing_{1}", gender, layerType);
							}
						}
					}
					else if (standardStage == 8)
					{
						if (isTarget)
						{
							tag = "start_masturbating";
						}
						else
						{
							tag = string.Format("{0}_start_masturbating", gender);
						}
					}
					break;
				case GamePhase.AfterLoss:
					if (standardStage < 8)
					{
						if (isTarget)
						{
							tag = "stripped";
						}
						else
						{
							bool isFemale = (gender == "female");
							string layerType = state.Clothing?.Type;
							if (layerType == "important")
							{
								string position = state.Clothing.Position;
								if (position == "upper")
								{
									if (isFemale)
									{
										string size = state.TargetState.Character.Size;
										tag = string.Format("{0}_{1}_chest_is_visible", gender, size);
									}
									else
									{
										tag = string.Format("{0}_chest_is_visible", gender);
									}
								}
								else if (position == "lower")
								{
									if (isFemale)
									{
										tag = string.Format("{0}_crotch_is_visible", gender);
									}
									else
									{
										string size = state.TargetState.Character.Size;
										tag = string.Format("{0}_{1}_crotch_is_visible", gender, size);
									}
								}
							}
							else
							{
								if (layerType == "extra")
									layerType = "accessory";
								tag = string.Format("{0}_removed_{1}", gender, layerType);
							}
						}
					}
					else if (standardStage == 8)
					{
						if (isTarget)
						{
							tag = "start_masturbating";
						}
						else
						{
							tag = string.Format("{0}_start_masturbating", gender);
						}
					}
					break;
				case GamePhase.HeavyMasturbating:
					if (standardStage == 9)
					{
						if (isTarget)
						{
							tag = "heavy_masturbating";
						}
						else
						{
							tag = string.Format("{0}_masturbating", gender);
						}
					}
					break;
				case GamePhase.Finishing:
					if (standardStage == 9)
					{
						if (isTarget)
						{
							tag = "finishing_masturbating";
						}
					}
					break;
				case GamePhase.Finished:
					if (standardStage == 10)
					{
						if (isTarget)
						{
							tag = "finished_masturbating";
						}
						else
						{
							tag = string.Format("{0}_finished_masturbating", gender);
						}
					}
					break;
				case GamePhase.GameOver:
					if (isTarget)
					{
						tag = "game_over_victory";
					}
					else
					{
						if (TriggerDatabase.ToStandardStage(State.Character, State.Stage) == 10)
							tag = "game_over_defeat";
					}
					break;
			}
			return TriggerDatabase.GetTrigger(tag);
		}
	}
}

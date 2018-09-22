using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class GameSimulator : Form
	{
		private TestCharacter[] _characters;
		private GameState _state = new GameState();

		public GameSimulator()
		{
			InitializeComponent();
			_characters = new TestCharacter[4];
			_characters[0] = char1;
			_characters[1] = char2;
			_characters[2] = char3;
			_characters[3] = char4;

			PopulateComboBoxes();

			//Fill random characters
			Random random = new Random();
			HashSet<Character> usedChars = new HashSet<Character>();
			for (int i = 0; i < 4; i++)
			{
				TestCharacter control = _characters[i];
				Character character = null;
				do
				{
					int index = random.Next(CharacterDatabase.Characters.Count);
					character = CharacterDatabase.Characters[index];
				}
				while (usedChars.Contains(character));
				_characters[i].SetCharacter(character);
				usedChars.Add(character);

				//Hook up events
				control.Tag = i;
				control.StateChanged += Control_CharacterChanged;
			}
			UpdateGameState();
		}

		private void Control_CharacterChanged(object sender, CharacterState e)
		{
			PopulateTargets();
			List<CharacterState> targets = new List<CharacterState>();
			for (int i = 0; i < _characters.Length; i++)
			{
				targets.Add(_characters[i].State);
			}
		}

		private void PopulateComboBoxes()
		{
			cboTrigger.DataSource = Enum.GetValues(typeof(GamePhase));
		}

		private void PopulateTargets()
		{
			for (int i = 0; i < _characters.Length; i++)
			{
				if (cboTarget.Items.Count <= i)
					cboTarget.Items.Add(_characters[i].State);
				else cboTarget.Items[i] = _characters[i].State; //Forces the text to update
			}
			if (cboTarget.Items.Count > 0 && cboTarget.SelectedIndex == -1)
				cboTarget.SelectedIndex = 0;
		}

		public void SetCharacter(int index, Character character)
		{
			_characters[index].SetCharacter(character);
		}

		/// <summary>
		/// Updates variables and all characters' states
		/// </summary>
		private void UpdateGameState()
		{
			Random rand = new Random();
			_state.ClearVariables();

			_state.SetVariable("player", "Mr. Player");

			_state.Characters.Clear();
			_state.Characters.Add(_characters[0].State);
			_state.Characters.Add(_characters[1].State);
			_state.Characters.Add(_characters[2].State);
			_state.Characters.Add(_characters[3].State);

			_state.Phase = (GamePhase)cboTrigger.SelectedItem;
			_state.Filter = txtFilter.Text;
			_state.TotalRounds = (int)valTotalRounds.Value;

			_state.TargetState = null;
			if (_state.Phase == GamePhase.BeforeLoss || _state.Phase == GamePhase.DuringLoss || _state.Phase == GamePhase.AfterLoss ||
				_state.Phase == GamePhase.Finished || _state.Phase == GamePhase.GameOver || 
				_state.Phase == GamePhase.Masturbating || _state.Phase == GamePhase.HeavyMasturbating)
			{
				_state.TargetState = cboTarget.SelectedItem as CharacterState;
				if (_state.TargetState == null)
					return;
				Character target = _state.TargetState.Character;
				if (target != null)
				{
					_state.SetVariable("name", target.Label);
				}
				int state = _state.TargetState.Stage;
				if (state < target.Layers)
				{
					int wardrobeIndex = target.Layers - state - 1;
					if(wardrobeIndex < target.Wardrobe.Count)
						_state.Clothing = target.Wardrobe[wardrobeIndex];
				}
				if (_state.Phase == GamePhase.DuringLoss || _state.Phase == GamePhase.AfterLoss)
				{
					if (_state.Clothing != null)
					{
						_state.SetVariable("clothing", _state.Clothing.Lowercase);
						_state.SetVariable("Clothing", _state.Clothing.Name);
					}
				}
			}
			else if (_state.Phase == GamePhase.ExchangingCards)
			{
				_state.SetVariable("cards", rand.Next(5).ToString());
			}

			//Send to the characters
			for (int i = 0; i < _characters.Length; i++)
			{
				_characters[i].UpdateState(_state);
			}
		}
		
		private void cmdGo_Click(object sender, EventArgs e)
		{
			UpdateGameState();
		}

		private void cboTrigger_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateGameState();
		}

		private void cboTarget_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateGameState();
		}

		private void valTotalRounds_ValueChanged(object sender, EventArgs e)
		{
			UpdateGameState();
		}
	}

	public class GameState
	{
		public GamePhase Phase = GamePhase.Start;
		public Clothing Clothing;
		public string Filter;
		public List<CharacterState> Characters = new List<CharacterState>();
		public CharacterState TargetState;
		public int TotalRounds;
		private Dictionary<string, string> _variables = new Dictionary<string, string>();

		public string Target
		{
			get
			{
				return TargetState?.Character?.FolderName;
			}
		}

		public void SetVariable(string var, string value)
		{
			_variables[var] = value;
		}

		public string GetVariable(string var)
		{
			if (!_variables.ContainsKey(var))
				return var;
			return _variables[var];
		}

		public void ClearVariables()
		{
			_variables.Clear();
		}

		/// <summary>
		/// Gets whether a certain character is playing
		/// </summary>
		/// <param name="character"></param>
		/// <returns></returns>
		public CharacterState IsPlaying(string folderName)
		{
			CharacterState character = Characters.Find(s => s.Character.FolderName == folderName);
			return character;
		}
	}

	public class CharacterState
	{
		public Character Character;
		public int Stage;
		public string Hand;
		public int TimeInStage;
		public int Losses;
		public HashSet<string> Markers = new HashSet<string>();

		public override string ToString()
		{
			string value = Character?.Label;
			if (string.IsNullOrEmpty(value))
				return "???";
			return value;
		}

		public bool IsAlive
		{
			get
			{
				return Stage <= Character.Layers;
			}
		}

		public bool IsExposed
		{
			get
			{
				if (IsNaked)
					return true;
				if (Stage == 0)
					return false;
				for (int i = 0; i < Stage; i++)
				{
					Clothing clothing = Character.Wardrobe[i];
					if (clothing.Type == "important")
						return true;
				}
				return false;
			}
		}

		public bool IsNaked
		{
			get
			{
				return Stage >= Character.Layers;
			}
		}

		public bool IsFinishing
		{
			get
			{
				return Stage == Character.Layers + 1;
			}
		}

		public bool IsFinished
		{
			get
			{
				return Stage > Character.Layers;
			}
		}

		/// <summary>
		/// Gets the appropriate trigger for this phase and target
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public Trigger GetTrigger(GameState state)
		{
			bool isTarget = (state.TargetState != null && state.TargetState == this);
			string tag = "";
			string gender = state.TargetState?.Character?.Gender;
			int standardStage = 0;
			if (state.TargetState != null)
			{
				standardStage = TriggerDatabase.ShiftStage(state.TargetState.Character, state.TargetState.Stage);
			}
			else
			{
				standardStage = TriggerDatabase.ShiftStage(Character, Stage);
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
						int stage = standardStage;
						foreach (var character in state.Characters)
						{
							if (character != this)
							{
								int otherStage = TriggerDatabase.ShiftStage(character.Character, character.Stage);
								if (otherStage <= stage)
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
								if (character != this)
								{
									if (TriggerDatabase.ShiftStage(character.Character, character.Stage) >= 9)
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
					if (standardStage <= 8)
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
				case GamePhase.Masturbating:
					if (standardStage == 9)
					{
						if (isTarget)
						{
							tag = "masturbating";
						}
						else
						{
							tag = string.Format("{0}_masturbating", gender);
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
						if (TriggerDatabase.ShiftStage(Character, Stage) == 10)
							tag = "game_over_defeat";
					}
					break;
			}
			return TriggerDatabase.GetTrigger(tag);
		}

		/// <summary>
		/// Gets whether a value is within a range
		/// </summary>
		/// <param name="range"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool InRange(string range, int value)
		{
			if (string.IsNullOrEmpty(range))
				return true;
			int min = 0;
			int max = 0;
			string[] pieces = range.Split('-');
			if (!int.TryParse(pieces[0], out min))
				return true;
			if (pieces.Length > 1)
			{
				if (!int.TryParse(pieces[1], out max))
					return true;
				return min <= value && value <= max;
			}
			return value == min;
		}

		public List<Case> GetPossibleCases(GameState state)
		{
			List<Case> availableCases = new List<Case>();
			Trigger trigger = GetTrigger(state);
			if (trigger == null)
			{
				return availableCases;
			}
			int stageId = Stage;
			Character character = Character;
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

					if (!InRange(possibleCase.TargetStage, state.TargetState.Stage))
						continue; //Target stage doesn't match

					if (!InRange(possibleCase.TargetTimeInStage, state.TargetState.TimeInStage))
						continue;

					if (!InRange(possibleCase.ConsecutiveLosses, state.TargetState.Losses))
						continue;

					if (!string.IsNullOrEmpty(possibleCase.TargetSaidMarker))
					{
						if (!state.TargetState.Markers.Contains(possibleCase.TargetSaidMarker))
							continue;
					}
					if (!string.IsNullOrEmpty(possibleCase.TargetNotSaidMarker))
					{
						if (state.TargetState.Markers.Contains(possibleCase.TargetNotSaidMarker))
							continue;
					}

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
				else
				{
					if (!InRange(possibleCase.ConsecutiveLosses, Losses))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.AlsoPlaying))
				{
					CharacterState alsoPlaying = state.IsPlaying(possibleCase.AlsoPlaying);
					if (alsoPlaying == null || alsoPlaying == this || possibleCase.AlsoPlaying == target)
						continue;
					if (!string.IsNullOrEmpty(possibleCase.AlsoPlayingStage) && possibleCase.AlsoPlayingStage != alsoPlaying.Stage.ToString())
						continue;
					if (!string.IsNullOrEmpty(possibleCase.AlsoPlayingHand))
					{
						if (alsoPlaying.Hand != possibleCase.AlsoPlayingHand)
							continue;
					}
					if (!InRange(possibleCase.AlsoPlayingTimeInStage, alsoPlaying.TimeInStage))
						continue;
					if (!string.IsNullOrEmpty(possibleCase.AlsoPlayingSaidMarker))
					{
						if (!alsoPlaying.Markers.Contains(possibleCase.AlsoPlayingSaidMarker))
							continue;
					}
					if (!string.IsNullOrEmpty(possibleCase.AlsoPlayingNotSaidMarker))
					{
						if (alsoPlaying.Markers.Contains(possibleCase.AlsoPlayingNotSaidMarker))
							continue;
					}
				}
				if (!string.IsNullOrEmpty(possibleCase.SaidMarker))
				{
					if (!Markers.Contains(possibleCase.SaidMarker))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.NotSaidMarker))
				{
					if (Markers.Contains(possibleCase.NotSaidMarker))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TimeInStage))
				{
					if (!InRange(possibleCase.TimeInStage, TimeInStage))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.HasHand))
				{
					if (Hand != possibleCase.HasHand)
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalMales))
				{
					int total = 0;
					foreach (var c in state.Characters)
					{
						if (c.Character.Gender == "male")
							total++;
					}
					if (!InRange(possibleCase.TotalMales, total))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalFemales))
				{
					int total = 0;
					foreach (var c in state.Characters)
					{
						if (c.Character.Gender == "female")
							total++;
					}
					if (!InRange(possibleCase.TotalFemales, total))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalPlaying))
				{
					int total = 0;
					foreach (var c in state.Characters)
					{
						if (c.IsAlive)
							total++;
					}
					if (!InRange(possibleCase.TotalPlaying, total))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalExposed))
				{
					int total = 0;
					foreach (var c in state.Characters)
					{
						if (c.IsExposed)
							total++;
					}
					if (!InRange(possibleCase.TotalExposed, total))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalNaked))
				{
					int total = 0;
					foreach (var c in state.Characters)
					{
						if (c.IsNaked)
							total++;
					}
					if (!InRange(possibleCase.TotalNaked, total))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalFinishing))
				{
					int total = 0;
					foreach (var c in state.Characters)
					{
						if (c.IsFinishing)
							total++;
					}
					if (!InRange(possibleCase.TotalFinishing, total))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalFinished))
				{
					int total = 0;
					foreach (var c in state.Characters)
					{
						if (c.IsFinished)
							total++;
					}
					if (!InRange(possibleCase.TotalFinished, total))
						continue;
				}
				if (!string.IsNullOrEmpty(possibleCase.TotalRounds))
				{
					if (!InRange(possibleCase.TotalRounds, state.TotalRounds))
						continue;
				}

				availableCases.Add(possibleCase);
			}
			return availableCases;
		}

	}

	public enum GamePhase
	{
		Start,
		ExchangingCards,
		GoodHand,
		OkayHand,
		BadHand,
		BeforeLoss,
		DuringLoss,
		AfterLoss,
		Masturbating,
		HeavyMasturbating,
		Finishing,
		Finished,
		GameOver
	}
}

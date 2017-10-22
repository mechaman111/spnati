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

			_state.TargetState = null;
			if (_state.Phase == GamePhase.BeforeLoss || _state.Phase == GamePhase.DuringLoss || _state.Phase == GamePhase.AfterLoss ||
				_state.Phase == GamePhase.Finished || _state.Phase == GamePhase.GameOver || 
				_state.Phase == GamePhase.HeavyMasturbating)
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
	}

	public class GameState
	{
		public GamePhase Phase = GamePhase.Start;
		public Clothing Clothing;
		public string Filter;
		public List<CharacterState> Characters = new List<CharacterState>();
		public CharacterState TargetState;
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

		public override string ToString()
		{
			string value = Character?.Label;
			if (string.IsNullOrEmpty(value))
				return "???";
			return value;
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

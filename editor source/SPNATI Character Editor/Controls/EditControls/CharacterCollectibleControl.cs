using SPNATI_Character_Editor.DataStructures;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class CharacterCollectibleControl : SubVariableControl
	{
		private ExpressionTest _expression;
		
		public CharacterCollectibleControl()
		{
			InitializeComponent();

			recCharacter.RecordType = typeof(Character);
			recItem.RecordType = typeof(Collectible);
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;
			
			recItem.RecordKey = null;

			string pattern = @"~(.*)\.collectible\.([^~]*)~";
			Match match = Regex.Match(_expression.Expression, pattern);
			if (match.Success)
			{
				string id = match.Groups[1].Value;
				string key = match.Groups[2].Value;

				Character character = CharacterDatabase.GetById(id);
				if (character == null)
				{
					//default to AlsoPlaying
					Case data = Data as Case;
					character = CharacterDatabase.Get(data.AlsoPlaying);
				}

				recItem.RecordContext = character;
				recCharacter.Record = character;

				if (!string.IsNullOrEmpty(key) && key != "*")
				{
					recItem.RecordKey = key;
				}
			}

			radLocked.Checked = _expression.Value == "false";
			radUnlocked.Checked = _expression.Value != "false";
		}
		
		public override void OnAddedToRow()
		{
			OnChangeLabel("Also Playing Collectible");
		}

		protected override void AddHandlers()
		{
			recCharacter.RecordChanged += RecCharacter_RecordChanged;
			recItem.RecordChanged += RecField_RecordChanged;
			radLocked.CheckedChanged += RadLocked_CheckedChanged;
			radUnlocked.CheckedChanged += RadLocked_CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			recCharacter.RecordChanged -= RecCharacter_RecordChanged;
			recItem.RecordChanged -= RecField_RecordChanged;
			radLocked.CheckedChanged -= RadLocked_CheckedChanged;
			radUnlocked.CheckedChanged -= RadLocked_CheckedChanged;
		}

		public override void ApplyMacro(List<string> values)
		{
			//macros should never be applied directly to a subcontrol
		}

		public override void BuildMacro(List<string> values)
		{
			Save();
			values.Add(_expression.Expression);
			values.Add(_expression.Operator);
			values.Add(_expression.Value);
		}

		private void RecCharacter_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Character character = recCharacter.Record as Character;
			recItem.RecordContext = character;
			recItem.RecordKey = null;
			Save();
		}

		private void RecField_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Save();	
		}
		private void RadLocked_CheckedChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		public override void Save()
		{
			string character = recCharacter.RecordKey;
			string id = "_";
			if (!string.IsNullOrEmpty(character))
			{
				id = CharacterDatabase.GetId(character);
			}

			string key = recItem.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				key = "*";
			}
			string expression = $"~{id}.collectible.*~".Replace("*", key);
			_expression.Expression = expression;
			_expression.Operator = "==";
			if (radLocked.Checked)
			{
				_expression.Value = "false";
			}
			else
			{
				_expression.Value = "true";
			}

			base.Save();
		}

		private enum TargetMode
		{
			Self,
			AlsoPlaying,
			Target
		}
	}
}

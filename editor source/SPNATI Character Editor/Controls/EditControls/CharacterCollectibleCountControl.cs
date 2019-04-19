using SPNATI_Character_Editor.DataStructures;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using SPNATI_Character_Editor.Providers;

namespace SPNATI_Character_Editor
{
	public partial class CharacterCollectibleCountControl : SubVariableControl
	{
		private ExpressionTest _expression;

		public CharacterCollectibleCountControl()
		{
			InitializeComponent();

			recCharacter.RecordType = typeof(Character);
			recItem.RecordType = typeof(Collectible);
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;

			recItem.RecordKey = null;

			string pattern = @"~(.*)\.collectible\.([^.~]*).counter~";
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
					if (recItem.RecordKey == null)
					{
						CollectibleProvider provider = new CollectibleProvider();
						provider.Create(key);
						recItem.RecordKey = key;
					}
				}
			}

			try
			{
				cboOperator.SelectedItem = _expression.Operator ?? "==";
			}
			catch
			{
				cboOperator.SelectedItem = "==";
			}
			int count;
			int.TryParse(_expression.Value, out count);
			valCounter.Value = Math.Max(valCounter.Minimum, Math.Min(valCounter.Maximum, count));
			OnAddedToRow();
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Also Playing Collectible (Counter)");
		}

		protected override void AddHandlers()
		{
			recCharacter.RecordChanged += RecCharacter_RecordChanged;
			recItem.RecordChanged += RecField_RecordChanged;
			cboOperator.SelectedIndexChanged += Field_ValueChanged;
			valCounter.ValueChanged += Field_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			recCharacter.RecordChanged -= RecCharacter_RecordChanged;
			recItem.RecordChanged -= RecField_RecordChanged;
			cboOperator.SelectedIndexChanged -= Field_ValueChanged;
			valCounter.ValueChanged -= Field_ValueChanged;
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
		private void Field_ValueChanged(object sender, System.EventArgs e)
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
			string expression = $"~{id}.collectible.*.counter~".Replace("*", key);
			_expression.Expression = expression;
			_expression.Operator = cboOperator.Text;
			_expression.Value = valCounter.Value.ToString();

		}
	}
}

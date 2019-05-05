using Desktop;
using Desktop.CommonControls;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class CharacterPersistentMarkerControl : SubVariableControl
	{
		private ExpressionTest _expression;

		public CharacterPersistentMarkerControl()
		{
			InitializeComponent();

			recCharacter.RecordType = typeof(Character);
			recItem.RecordType = typeof(Marker);
			recItem.RecordFilter = FilterPrivateMarkers;
		}

		public override void BuildMacro(List<string> values)
		{
			Save();
			values.Add(_expression.Expression);
			values.Add(_expression.Operator);
			values.Add(_expression.Value);
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Also Playing Marker (Persistent)");
		}

		private bool FilterPrivateMarkers(IRecord record)
		{
			Marker marker = record as Marker;
			return marker.Scope == MarkerScope.Public;
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;

			recItem.RecordKey = null;

			string pattern = @"~(.*)\.persistent\.([^~]*)~";
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

			cboOperator.Text = _expression.Operator;
			txtValue.Text = _expression.Value;
			OnAddedToRow();
		}

		private void SetTargetContext()
		{
			Case context = Data as Case;
			string target = context.Target;
			if (!string.IsNullOrEmpty(target))
			{
				Character targetChar = CharacterDatabase.Get(target);
				recItem.RecordContext = targetChar;
			}
			else
			{
				recItem.RecordContext = null;
			}
		}

		protected override void RemoveHandlers()
		{
			recCharacter.RecordChanged -= RecCharacter_RecordChanged;
			recItem.RecordChanged -= RecordChanged;
			cboOperator.SelectedIndexChanged -= ValueChanged;
			txtValue.TextChanged -= ValueChanged;
		}

		protected override void AddHandlers()
		{
			recCharacter.RecordChanged += RecCharacter_RecordChanged;
			recItem.RecordChanged += RecordChanged;
			cboOperator.SelectedIndexChanged += ValueChanged;
			txtValue.TextChanged += ValueChanged;
		}

		public override void Clear()
		{
			RemoveHandlers();
			recCharacter.RecordKey = null;
			recItem.RecordKey = null;
			cboOperator.SelectedIndex = 0;
			txtValue.Text = "";
			AddHandlers();
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

			string expression = $"~{id}.persistent.*~".Replace("*", key);
			_expression.Expression = expression;

			string op = cboOperator.SelectedItem?.ToString();
			if (string.IsNullOrEmpty(op))
			{
				op = ">";
			}
			_expression.Operator = op;

			string value = txtValue.Text;
			if (string.IsNullOrEmpty(value))
			{
				value = "0";
			}
			_expression.Value = value;
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}
		private void RecCharacter_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Character character = recCharacter.Record as Character;
			recItem.RecordContext = character;
			recItem.RecordKey = null;
			Save();
		}

		private void RecordChanged(object sender, RecordEventArgs record)
		{
			Save();
		}
	}
}

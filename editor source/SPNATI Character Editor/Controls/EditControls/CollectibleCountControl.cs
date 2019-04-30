using SPNATI_Character_Editor.DataStructures;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using SPNATI_Character_Editor.Providers;

namespace SPNATI_Character_Editor
{
	public partial class CollectibleCountControl : SubVariableControl
	{
		private ExpressionTest _expression;
		private string _wildcardVariable = "~collectible.*.counter~";
		private TargetMode _mode = TargetMode.Self;

		public CollectibleCountControl()
		{
			InitializeComponent();

			recField.RecordType = typeof(Collectible);
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;

			if (_expression.Expression.StartsWith("~collectible."))
			{
				_mode = TargetMode.Self;
				recField.RecordContext = Context;
				_wildcardVariable = "~collectible.*.counter~";
			}
			else if (_expression.Expression.StartsWith("~target.collectible."))
			{
				_mode = TargetMode.Target;
				SetTargetContext();
				_wildcardVariable = "~target.collectible.*.counter~";
			}
			recField.RecordKey = null;

			string pattern = _wildcardVariable.Replace("*", "([^.~]*)");
			Match match = Regex.Match(_expression.Expression, pattern);
			if (match.Success)
			{
				string key = match.Groups[1].Value;
				if (!string.IsNullOrEmpty(key) && key != "*")
				{
					recField.RecordKey = key;
					if (recField.RecordKey == null)
					{
						CollectibleProvider provider = new CollectibleProvider();
						provider.Create(key);
						recField.RecordKey = key;
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

		protected override void OnBindingUpdated(string property)
		{
			if (property == "Target" && _mode == TargetMode.Target)
			{
				SetTargetContext();
			}
		}

		private void SetTargetContext()
		{
			Case context = Data as Case;
			string target = context.Target;
			if (!string.IsNullOrEmpty(target))
			{
				Character targetChar = CharacterDatabase.Get(target);
				recField.RecordContext = targetChar;
			}
			else
			{
				recField.RecordContext = null;
			}
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel(_mode == TargetMode.Self ? "Collectible (Counter)" : _mode == TargetMode.Target ? "Target Collectible (Counter)" : "Other Collectible (Counter)");
		}

		protected override void AddHandlers()
		{
			recField.RecordChanged += RecField_RecordChanged;
			cboOperator.SelectedIndexChanged += Field_ValueChanged;
			valCounter.ValueChanged += Field_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			recField.RecordChanged -= RecField_RecordChanged;
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
			string key = recField.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				key = "*";
			}
			string expression = _wildcardVariable.Replace("*", key);
			_expression.Expression = expression;
			_expression.Operator = cboOperator.Text;
			_expression.Value = valCounter.Value.ToString();

		}

		private enum TargetMode
		{
			Self,
			Target
		}
	}
}

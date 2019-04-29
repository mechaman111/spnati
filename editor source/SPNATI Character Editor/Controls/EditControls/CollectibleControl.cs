using SPNATI_Character_Editor.DataStructures;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class CollectibleControl : SubVariableControl
	{
		private ExpressionTest _expression;
		private string _wildcardVariable = "~collectible.*~";
		private TargetMode _mode = TargetMode.Self;

		public CollectibleControl()
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
				_wildcardVariable = "~collectible.*~";
			}
			else if (_expression.Expression.StartsWith("~target.collectible."))
			{
				_mode = TargetMode.Target;
				SetTargetContext();
				_wildcardVariable = "~target.collectible.*~";
			}
			recField.RecordKey = null;

			string pattern = _wildcardVariable.Replace("*", "([^~]*)");
			Match match = Regex.Match(_expression.Expression, pattern);
			if (match.Success)
			{
				string key = match.Groups[1].Value;
				if (!string.IsNullOrEmpty(key) && key != "*")
				{
					recField.RecordKey = key;
				}
			}

			radLocked.Checked = _expression.Value == "false";
			radUnlocked.Checked = _expression.Value != "false";
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
			OnChangeLabel(_mode == TargetMode.Self ? "Collectible" : _mode == TargetMode.Target ? "Target Collectible" : "Also Playing Collectible");
		}

		protected override void AddHandlers()
		{
			recField.RecordChanged += RecField_RecordChanged;
			radLocked.CheckedChanged += RadLocked_CheckedChanged;
			radUnlocked.CheckedChanged += RadLocked_CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			recField.RecordChanged -= RecField_RecordChanged;
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
			string key = recField.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				key = "*";
			}
			string expression = _wildcardVariable.Replace("*", key);
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
			Target
		}
	}
}

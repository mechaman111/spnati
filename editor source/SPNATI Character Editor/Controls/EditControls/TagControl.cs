using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class TagControl : SubVariableControl
	{
		private ExpressionTest _expression;
		private string _wildcardVariable = "~self.tag.*~";
		private TargetMode _mode = TargetMode.Self;

		public TagControl()
		{
			InitializeComponent();

			recField.RecordType = typeof(Tag);
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;

			if (_expression.Expression.StartsWith("~self.tag."))
			{
				_mode = TargetMode.Self;
				recField.RecordContext = Context;
				_wildcardVariable = "~self.tag.*~";
			}
			else if (_expression.Expression.StartsWith("~target.tag."))
			{
				_mode = TargetMode.Target;
				SetTargetContext();
				_wildcardVariable = "~target.tag.*~";
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

			chkNot.Checked = _expression.Value == "false";
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
			OnChangeLabel(_mode == TargetMode.Self ? "Tag" : _mode == TargetMode.Target ? "Target Tag" : "Also Playing Tag");
		}

		protected override void AddHandlers()
		{
			recField.RecordChanged += RecField_RecordChanged;
			chkNot.CheckedChanged += Field_CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			recField.RecordChanged -= RecField_RecordChanged;
			chkNot.CheckedChanged -= Field_CheckedChanged;
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
		private void Field_CheckedChanged(object sender, System.EventArgs e)
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
			if (chkNot.Checked)
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

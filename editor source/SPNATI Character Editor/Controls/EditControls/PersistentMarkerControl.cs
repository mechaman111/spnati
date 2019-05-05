using Desktop;
using Desktop.CommonControls;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class PersistentMarkerControl : SubVariableControl
	{
		private ExpressionTest _expression;
		private string _wildcardVariable = "~persistent.*~";
		private TargetMode _mode = TargetMode.Self;

		public PersistentMarkerControl()
		{
			InitializeComponent();

			recField.RecordType = typeof(Marker);
		}

		public override void BuildMacro(List<string> values)
		{
			Save();
			values.Add(_expression.Expression);
			values.Add(_expression.Operator);
			values.Add(_expression.Value);
		}

		public override void OnInitialAdd()
		{
			if (recField.RecordContext != null)
			{
				recField.DoSearch();
			}
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel(_mode == TargetMode.Self ? "Marker (Persistent)" : _mode == TargetMode.Target ? "Target Marker (Persistent)" : "Also Playing Marker (Persistent)");
		}

		private bool FilterPrivateMarkers(IRecord record)
		{
			Marker marker = record as Marker;
			return marker.Scope == MarkerScope.Public;
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;

			if (_expression.Expression.StartsWith("~persistent."))
			{
				_mode = TargetMode.Self;
				recField.RecordContext = Context;
				_wildcardVariable = "~persistent.*~";
			}
			else if (_expression.Expression.StartsWith("~target.persistent."))
			{
				_mode = TargetMode.Target;
				recField.RecordFilter = FilterPrivateMarkers;
				SetTargetContext();
				_wildcardVariable = "~target.persistent.*~";
			}

			recField.RecordKey = null;
			recField.UseAutoComplete = true;

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

			cboOperator.Text = _expression.Operator;
			txtValue.Text = _expression.Value;
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

		protected override void RemoveHandlers()
		{
			recField.RecordChanged -= RecordChanged;
			cboOperator.SelectedIndexChanged -= ValueChanged;
			txtValue.TextChanged -= ValueChanged;
		}

		protected override void AddHandlers()
		{
			recField.RecordChanged += RecordChanged;
			cboOperator.SelectedIndexChanged += ValueChanged;
			txtValue.TextChanged += ValueChanged;
		}

		public override void Clear()
		{
			RemoveHandlers();
			recField.RecordKey = null;
			cboOperator.SelectedIndex = 0;
			txtValue.Text = "";
			AddHandlers();
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

		private void RecordChanged(object sender, RecordEventArgs record)
		{
			Save();
		}

		private enum TargetMode
		{
			Self,
			Target
		}
	}
}

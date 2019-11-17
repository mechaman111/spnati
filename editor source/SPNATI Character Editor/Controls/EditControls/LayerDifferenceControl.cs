using System;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	[SubVariable("diff", AllowRHSVariables = true)]
	public partial class LayerDifferenceControl : SubVariableControl
	{
		private ExpressionTest _expression;

		public LayerDifferenceControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;
			recType.RecordType = typeof(TargetId);
			recCharacter.RecordType = typeof(TargetId);

			Bindings.Add("AlsoPlaying");
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;
			string targetType;
			string variable;
			ExtractExpressionPieces(out targetType, out variable);

			recType.RecordKey = targetType;
			variable = variable.TrimEnd(')');
			int start = variable.IndexOf('(');
			if (start >= 0 && start < variable.Length - 1)
			{
				string id = variable.Substring(start + 1);
				recCharacter.RecordKey = id;
				//Character character = CharacterDatabase.GetById(id);
				//recCharacter.Record = character;
			}

			cboOperator.Text = _expression.Operator ?? "==";
			txtValue.Text = _expression.Value;
			OnAddedToRow();
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Layer Difference");
		}

		protected override void OnBindingUpdated(string property)
		{
			if (property == "AlsoPlaying" && (recType.RecordKey == "_" || recType.RecordKey == null))
			{
				string targetType;
				string character;
				ExtractExpressionPieces(out targetType, out character);
				recType.RecordKey = targetType;
			}
		}

		protected override void AddHandlers()
		{
			recType.RecordChanged += RecType_RecordChanged;
			cboOperator.SelectedIndexChanged += ValueChanged;
			recCharacter.RecordChanged += RecType_RecordChanged;
			txtValue.TextChanged += ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			recType.RecordChanged -= RecType_RecordChanged;
			cboOperator.SelectedIndexChanged -= ValueChanged;
			recCharacter.RecordChanged -= RecType_RecordChanged;
			txtValue.TextChanged -= ValueChanged;
		}

		private void RecType_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Save();
		}
		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			string targetType = recType.RecordKey;
			if (string.IsNullOrEmpty(targetType))
			{
				targetType = "_";
			}
			string id = recCharacter.RecordKey ?? "";
			_expression.Expression = $"~{targetType}.diff({id})~";

			string op = cboOperator.SelectedItem?.ToString();
			if (string.IsNullOrEmpty(op))
			{
				op = "==";
			}
			_expression.Operator = op;

			string value = txtValue.Text;
			if (string.IsNullOrEmpty(value))
			{
				value = null;
			}
			_expression.Value = value;
		}
	}
}

using Desktop.CommonControls;
using System;

namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	[SubVariable("background")]
	public partial class BackgroundControl : SubVariableControl
	{
		public BackgroundControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;
			recTag.RecordType = typeof(BackgroundTag);
			cboOperator.SelectedIndex = 0;
		}

		protected virtual Type GetCategoryType()
		{
			return null;
		}

		protected override void OnBoundData()
		{
			_expression = (GetValue() as ExpressionTest);
			string target;
			string variable;
			ExtractExpressionPieces(out target, out variable);
			if (variable.StartsWith("background"))
			{
				_expression.Expression = string.Format("~{0}~", variable);
			}
			ExtractExpressionPieces(out target, out variable);
			if (string.IsNullOrEmpty(variable) || variable == "background")
			{
				variable = "name";
			}
			BackgroundTag tag = Definitions.Instance.Get<BackgroundTag>(variable);
			recTag.Record = tag;
			this.UpdateValues();
			try
			{
				cboOperator.SelectedItem = (_expression.Operator ?? "==");
			}
			catch
			{
				cboOperator.SelectedItem = "==";
			}
			cboValue.Text = _expression.Value;
			if (_expression.Value == "true" && (_expression.Operator == "==" || string.IsNullOrEmpty(_expression.Operator)))
			{
				radTrue.Checked = true;
			}
			else
			{
				radFalse.Checked = true;
			}
			OnChangeLabel("Background");
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Background");
		}

		protected override void AddHandlers()
		{
			recTag.RecordChanged += RecTag_RecordChanged;
			cboOperator.SelectedIndexChanged += Field_ValueChanged;
			cboValue.TextChanged += Field_ValueChanged;
			radTrue.CheckedChanged += Field_ValueChanged;
			radFalse.CheckedChanged += Field_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			recTag.RecordChanged -= RecTag_RecordChanged;
			cboOperator.SelectedIndexChanged -= Field_ValueChanged;
			cboValue.TextChanged -= Field_ValueChanged;
			radTrue.CheckedChanged -= Field_ValueChanged;
			radFalse.CheckedChanged -= Field_ValueChanged;
		}

		private void Field_ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void RecTag_RecordChanged(object sender, RecordEventArgs e)
		{
			RemoveHandlers();
			UpdateValues();
			AddHandlers();
			Save();
		}

		private void UpdateValues()
		{
			BackgroundTag tag = recTag.Record as BackgroundTag;
			if (tag == null)
			{
				cboOperator.Visible = false;
				cboValue.Visible = false;
				panelBoolean.Visible = false;
			}
			else
			{
				if (tag.Values.Count > 0)
				{
					cboOperator.Visible = true;
					cboValue.Visible = true;
					panelBoolean.Visible = false;
					cboOperator.SelectedIndex = 0;
					cboValue.Text = "";
					cboValue.DataSource = tag.Values;
				}
				else
				{
					cboOperator.Visible = false;
					cboValue.Visible = false;
					panelBoolean.Visible = true;
					radTrue.Checked = true;
				}
			}
		}

		protected override void OnSave()
		{
			BackgroundTag tag = recTag.Record as BackgroundTag;
			if (tag != null)
			{
				string expression = string.Format("~background.{0}~", tag.Key);
				if (tag.Key == "name")
				{
					expression = "~background~";
				}
				_expression.Expression = expression;
				if (tag.Values.Count > 0)
				{
					_expression.Operator = cboOperator.Text;
					_expression.Value = cboValue.Text;
				}
				else
				{
					if (radTrue.Checked)
					{
						_expression.Operator = "==";
						_expression.Value = "true";
					}
					else
					{
						_expression.Operator = "!=";
						_expression.Value = "true";
					}
				}
			}
		}
	}
}

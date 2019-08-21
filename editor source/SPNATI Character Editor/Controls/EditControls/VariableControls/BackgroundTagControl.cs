using Desktop.CommonControls;
using System;

namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	[SubVariable("background", "tag")]
	public partial class BackgroundTagControl : SubVariableControl
	{
		public BackgroundTagControl()
		{
			InitializeComponent();
			recTag.RecordType = typeof(BackgroundTagValue);
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

			string[] pieces = variable.Split('.');
			string tagName = "";
			if (pieces.Length > 1)
			{
				tagName = pieces[1];
			}

			BackgroundTagValue tag = Definitions.Instance.Get<BackgroundTagValue>(tagName);
			recTag.Record = tag;
			UpdateValues();
			if (_expression.Value == "true" && (_expression.Value == "==" || string.IsNullOrEmpty(_expression.Value)))
			{
				radTrue.Checked = true;
			}
			else
			{
				radFalse.Checked = true;
			}
			OnAddedToRow();
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Background Tag");
		}

		protected override void AddHandlers()
		{
			recTag.RecordChanged += RecTag_RecordChanged;
			radTrue.CheckedChanged += Field_ValueChanged;
			radFalse.CheckedChanged += Field_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			recTag.RecordChanged -= RecTag_RecordChanged;
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
			BackgroundTagValue tag = recTag.Record as BackgroundTagValue;
			radTrue.Checked = true;
		}

		protected override void OnSave()
		{
			BackgroundTagValue tag = recTag.Record as BackgroundTagValue;
			if (tag != null)
			{
				string expression = string.Format("~background.tag.{0}~", tag.Key);
				_expression.Expression = expression;
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

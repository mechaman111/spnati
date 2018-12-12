using System;
using Desktop;
using Desktop.CommonControls;

namespace SPNATI_Character_Editor
{
	public partial class ExpressionControl : PropertyEditControl
	{
		private ExpressionTest _expression;

		public ExpressionControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;
			txtExpression.Text = _expression.Expression;
			txtValue.Text = _expression.Value;

			txtExpression.TextChanged += TextValueChanged;
			txtValue.TextChanged += TextValueChanged;
		}

		private void TextValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		public override void Clear()
		{
			txtExpression.Text = "";
			txtValue.Text = "";
			Save();
		}

		public override void Save()
		{
			_expression.Expression = txtExpression.Text;
			_expression.Value = txtValue.Text;
		}
	}

	public class ExpressionAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(ExpressionControl); }
		}
	}
}

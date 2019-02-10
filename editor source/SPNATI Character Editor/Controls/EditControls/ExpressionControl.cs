using System;
using Desktop;
using Desktop.CommonControls;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class ExpressionControl : PropertyEditControl
	{
		private ExpressionTest _expression;
		private string _currentVariable;

		public ExpressionControl()
		{
			InitializeComponent();

			cboExpression.Items.AddRange(new string[] {
				"~background~",
				"~background.location~",
				"~clothing~",
				"~clothing.position~",
				"~player~",
				"~weekday~",
			});
			cboExpression.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			cboExpression.AutoCompleteSource = AutoCompleteSource.ListItems;
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;
			cboExpression.Text = _expression.Expression;
			try
			{
				cboOperator.SelectedItem = _expression.Operator ?? "==";
			}
			catch
			{
				cboOperator.SelectedItem = "==";
			}
			cboValue.Text = _expression.Value;

			UpdateAutoComplete();

			cboExpression.TextChanged += TextValueChanged;
			cboOperator.SelectedValueChanged += TextValueChanged;
			cboValue.TextChanged += TextValueChanged;
		}

		private void TextValueChanged(object sender, EventArgs e)
		{
			UpdateAutoComplete();
			Save();
		}

		public override void Clear()
		{
			cboExpression.Text = "";
			cboExpression.SelectedItem = "==";
			cboValue.Text = "";
			Save();
		}

		public override void Save()
		{
			_expression.Expression = cboExpression.Text;
			_expression.Operator = cboOperator.Text;
			_expression.Value = cboValue.Text;
		}

		private void UpdateAutoComplete()
		{
			string variable = cboExpression.Text;
			if (variable == _currentVariable)
			{
				return;
			}
			_currentVariable = variable;

			AutoCompleteStringCollection list = new AutoCompleteStringCollection();

			cboValue.Items.Clear();
			switch (_currentVariable)
			{
				case "~clothing.position~":
					cboValue.Items.AddRange(new string[] {
						"upper",
						"lower",
						"both",
						"head",
						"neck",
						"hands",
						"arms",
						"feet",
						"legs",
						"waist",
						"other",
					});
					break;
				case "~background.location~":
					cboValue.Items.AddRange(new string[] {
						"indoors",
						"outdoors",
					});
					break;
				case "~background~":
					cboValue.Items.AddRange(new string[] {
						"inventory",
						"beach",
						"classroom",
						"brick",
						"night",
						"roof",
						"seasonal",
						"library",
						"bathhouse",
						"poolside",
						"hot spring",
						"mansion",
						"purple room",
						"showers",
						"street",
						"green screen",
						"arcade",
						"club",
						"bedroom",
						"hall",
						"locker room",
						"haunted forest",
						"romantic",
						"classic",
					});
					break;
				case "~weekday~":
					cboValue.Items.AddRange(new string[] {
						"Sunday",
						"Monday",
						"Tuesday",
						"Wednesday",
						"Thursday",
						"Friday",
						"Saturday",
					});
					break;
			}
			cboValue.Sorted = true;
			cboValue.AutoCompleteSource = AutoCompleteSource.ListItems;
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

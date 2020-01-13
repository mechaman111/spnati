using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPNATI_Character_Editor.Categories;

namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	public partial class GlobalCategoryControl : SubVariableControl
	{
		public ExpressionTest Expression { get; private set; }

		public GlobalCategoryControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;
			recCategory.RecordType = GetCategoryType();
		}

		protected virtual Type GetCategoryType()
		{
			return null;
		}

		protected override void OnBoundData()
		{
			Expression = GetValue() as ExpressionTest;
			cboOperator.SelectedItem = Expression.Operator ?? "==";
			recCategory.RecordKey = Expression.Value;
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			recCategory.RecordChanged += RecCategory_RecordChanged;
			cboOperator.SelectedIndexChanged += CboOperator_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			base.AddHandlers();
			recCategory.RecordChanged -= RecCategory_RecordChanged;
			cboOperator.SelectedIndexChanged -= CboOperator_ValueChanged;
		}

		private void RecCategory_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Save();
		}
		private void CboOperator_ValueChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			string v = GetVariable();
			Expression.Expression = $"~{v}~";
			Expression.Operator = cboOperator.Text;
			Expression.Value = recCategory.RecordKey;
		}

		protected virtual string GetVariable()
		{
			return "";
		}
	}


	[SubVariable("month", "number")]
	public class MonthNumericControl : GlobalCategoryControl
	{
		protected override Type GetCategoryType()
		{
			return typeof(MonthNumericCategory);
		}

		protected override string GetVariable()
		{
			return "month.number";
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Month");
		}
	}

	[SubVariable("day", "number")]
	public class DayNumericControl : GlobalCategoryControl
	{
		protected override Type GetCategoryType()
		{
			return typeof(DayNumericCategory);
		}

		protected override string GetVariable()
		{
			return "day.number";
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Day");
		}
	}

	[SubVariable("weekday", "number")]
	public class WeekdayNumericControl : GlobalCategoryControl
	{
		protected override Type GetCategoryType()
		{
			return typeof(WeekdayNumericCategory);
		}

		protected override string GetVariable()
		{
			return "weekday.number";
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Weekday");
		}
	}

	//These are commented out because the number versions do everything they do as far as comparisons go,
	//and exposing them runs the risk of people inadvertently trying to do string comparisons (ex. ~day~ < "Tuesday")

	//[SubVariable("month")]
	//public class MonthControl : GlobalCategoryControl
	//{
	//	protected override Type GetCategoryType()
	//	{
	//		return typeof(MonthCategory);
	//	}

	//	protected override string GetVariable()
	//	{
	//		return "month";
	//	}

	//	public override void OnAddedToRow()
	//	{
	//		OnChangeLabel("Month");
	//	}
	//}

	//[SubVariable("day")]
	//public class DayControl : GlobalCategoryControl
	//{
	//	protected override Type GetCategoryType()
	//	{
	//		return typeof(DayCategory);
	//	}

	//	protected override string GetVariable()
	//	{
	//		return "day";
	//	}

	//	public override void OnAddedToRow()
	//	{
	//		OnChangeLabel("Day");
	//	}
	//}

	//[SubVariable("weekday")]
	//public class WeekdayControl : GlobalCategoryControl
	//{
	//	protected override Type GetCategoryType()
	//	{
	//		return typeof(WeekdayCategory);
	//	}

	//	protected override string GetVariable()
	//	{
	//		return "weekday";
	//	}

	//	public override void OnAddedToRow()
	//	{
	//		OnChangeLabel("Weekday");
	//	}
	//}
}

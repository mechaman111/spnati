using System;

namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	public partial class CategoryControl : PlayerControlBase
	{
		public CategoryControl()
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
			base.OnBoundData();
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
			base.OnSave();
			Expression.Operator = cboOperator.Text;
			Expression.Value = recCategory.RecordKey;
		}
	}

	[SubVariable("slot")]
	public class SlotControl : CategoryControl
	{
		protected override Type GetCategoryType()
		{
			return typeof(Slot);
		}

		protected override string GetVariable()
		{
			return "slot";
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Game Slot");
		}

	}

	[SubVariable("position")]
	public class PositionControl : CategoryControl
	{
		protected override Type GetCategoryType()
		{
			return typeof(RelativePosition);
		}

		protected override string GetVariable()
		{
			return "position";
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Relative Position");
		}
	}

	[SubVariable("size")]
	public class SizeControl : CategoryControl
	{
		protected override Type GetCategoryType()
		{
			return typeof(SizeCategory);
		}

		protected override string GetVariable()
		{
			return "size";
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Size");
		}
	}

	[SubVariable("gender")]
	public class GenderControl : CategoryControl
	{
		protected override Type GetCategoryType()
		{
			return typeof(GenderCategory);
		}

		protected override string GetVariable()
		{
			return "gender";
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Gender");
		}
	}
}

using Desktop;
using Desktop.CommonControls;

namespace SPNATI_Character_Editor.Controls.EditControls.VariableControls
{
	[SubVariable("costume")]
	public partial class CostumeControl : PlayerControlBase
	{
		public CostumeControl()
		{
			InitializeComponent();
			recCostume.RecordType = typeof(Costume);
			recCostume.RecordFilter = FilterCostume;
		}

		private bool FilterCostume(IRecord record)
		{
			Character character = GetTargetCharacter();
			if (character == null)
			{
				return true;
			}
			Costume costume = record as Costume;
			return costume.Character == character || costume.Key == "default";
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			chkNot.Checked = (Expression.Operator == "!=");
			recCostume.RecordKey = Expression.Value;
			OnAddedToRow();
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Costume");
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			chkNot.CheckedChanged += ValueChanged;
			recCostume.RecordChanged += RecCostume_RecordChanged;
		}

		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			chkNot.CheckedChanged -= ValueChanged;
			recCostume.RecordChanged -= RecCostume_RecordChanged;
		}

		private void RecCostume_RecordChanged(object sender, RecordEventArgs e)
		{
			Save();
		}

		private void ValueChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			base.OnSave();
			Expression.Operator = (chkNot.Checked ? "!=" : "==");
			Expression.Value = recCostume.RecordKey;
		}

		protected override string GetVariable()
		{
			return "costume";
		}
	}
}

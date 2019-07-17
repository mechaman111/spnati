using SPNATI_Character_Editor.DataStructures;

namespace SPNATI_Character_Editor
{
	[SubVariable("collectible")]
	public partial class CollectibleControl : PlayerControlBase
	{
		public CollectibleControl()
		{
			InitializeComponent();

			recField.RecordType = typeof(Collectible);
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			radLocked.Checked = Expression.Value == "false";
			radUnlocked.Checked = Expression.Value != "false";
			OnAddedToRow();
		}

		protected override void BindVariable(string variable)
		{
			string[] pieces = variable.Split('.'); ;
			string propName = "";
			if (pieces.Length > 1)
			{
				propName = pieces[1];
			}
			if (!string.IsNullOrEmpty(propName) && propName != "*")
			{
				recField.RecordKey = propName;
			}
		}

		protected override void OnTargetTypeChanged()
		{
			recField.RecordContext = GetTargetCharacter();
		}

		public override void OnAddedToRow()
		{
			OnChangeLabel("Collectible");
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			recField.RecordChanged += RecField_RecordChanged;
			radLocked.CheckedChanged += RadLocked_CheckedChanged;
			radUnlocked.CheckedChanged += RadLocked_CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			recField.RecordChanged -= RecField_RecordChanged;
			radLocked.CheckedChanged -= RadLocked_CheckedChanged;
			radUnlocked.CheckedChanged -= RadLocked_CheckedChanged;
		}

		private void RecField_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Save();	
		}
		private void RadLocked_CheckedChanged(object sender, System.EventArgs e)
		{
			Save();
		}

		protected override string GetVariable()
		{
			string key = recField.RecordKey;
			if (string.IsNullOrEmpty(key))
			{
				key = "*";
			}

			return $"collectible.{key}";
		}

		protected override void OnSave()
		{
			base.OnSave();
			Expression.Operator = "==";
			if (radLocked.Checked)
			{
				Expression.Value = "false";
			}
			else
			{
				Expression.Value = "true";
			}
		}
	}
}

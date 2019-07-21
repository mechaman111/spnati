namespace SPNATI_Character_Editor
{
	[SubVariable("tag")]
	public partial class TagControl : PlayerControlBase
	{
		public TagControl()
		{
			InitializeComponent();

			recField.RecordType = typeof(Tag);
		}

		protected override void OnBoundData()
		{
			base.OnBoundData();
			chkNot.Checked = Expression.Value == "false";
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
			OnChangeLabel("Tag");
		}

		protected override void AddHandlers()
		{
			base.AddHandlers();
			recField.RecordChanged += RecField_RecordChanged;
			chkNot.CheckedChanged += Field_CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			base.RemoveHandlers();
			recField.RecordChanged -= RecField_RecordChanged;
			chkNot.CheckedChanged -= Field_CheckedChanged;
		}

		private void RecField_RecordChanged(object sender, Desktop.CommonControls.RecordEventArgs e)
		{
			Save();	
		}
		private void Field_CheckedChanged(object sender, System.EventArgs e)
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

			return $"tag.{key}";
		}

		protected override void OnSave()
		{
			base.OnSave();
			Expression.Operator = "==";
			if (chkNot.Checked)
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

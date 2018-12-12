using System;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class BooleanControl : PropertyEditControl
	{
		public BooleanControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			Type propType = PropertyType;

			//False values will never even be bound to a control in the PropertyTable, so the only time someone should want to edit a boolean is to mark it true,
			//So, let's just do that up front.
			chkEnabled.Checked = true;
			Save();

			//If we *did* care about false, here it is for posterity's sake:
			//if (propType == typeof(bool))
			//{
			//	chkEnabled.Checked = (bool)GetValue();
			//}
			//else
			//{
			//	string value = GetValue()?.ToString();
			//	if (!string.IsNullOrEmpty(value) && value != "0")
			//	{
			//		chkEnabled.Checked = true;
			//	}
			//}
			
			chkEnabled.CheckedChanged += ChkEnabled_CheckedChanged;
		}

		private void ChkEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Save();
		}

		public override void Clear()
		{
			chkEnabled.Checked = false;
		}

		public override void Save()
		{
			bool enabled = chkEnabled.Checked;
			if (PropertyType == typeof(bool))
			{
				SetValue(enabled);
			}
			else
			{
				SetValue(enabled ? "1" : null);
			}
		}
	}

	public class BooleanAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(BooleanControl); }
		}
	}
}

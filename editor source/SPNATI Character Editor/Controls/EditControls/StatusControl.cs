using Desktop;
using Desktop.CommonControls;
using System;

namespace SPNATI_Character_Editor
{
	public partial class StatusControl : PropertyEditControl
	{
		public StatusControl()
		{
			InitializeComponent();

			cboStatus.DataSource = TargetCondition.StatusTypes;
			cboStatus.ValueMember = "Key";
			cboStatus.DisplayMember = "Value";
		}

		protected override void OnBoundData()
		{
			string value = GetValue()?.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				if (value.StartsWith("not_"))
				{
					value = value.Substring(4, value.Length - 4);
					chkNegate.Checked = true;
				}
				cboStatus.SelectedValue = value;
			}
			chkNegate.CheckedChanged += ChkNegate_CheckedChanged;
			cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;
		}

		public override void Clear()
		{
			cboStatus.SelectedIndex = 0;
		}

		public override void Save()
		{
			bool inverted = chkNegate.Checked;
			string value = (string)cboStatus.SelectedValue;
			if (string.IsNullOrEmpty(value))
			{
				SetValue(null);
			}
			else
			{
				if (inverted)
				{
					value = "not_" + value;
				}
				SetValue(value);
			}
		}

		private void CboStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void ChkNegate_CheckedChanged(object sender, EventArgs e)
		{
			Save();
		}
	}

	public class StatusAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(StatusControl); }
		}
	}
}

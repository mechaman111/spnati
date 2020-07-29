using Desktop;
using Desktop.CommonControls;
using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class StatusControl : PropertyEditControl
	{
		public StatusControl()
		{
			InitializeComponent();

			cboStatus.DataSource = Config.SafeMode ? TargetCondition.SafeStatusTypes : TargetCondition.StatusTypes;
			cboStatus.ValueMember = "Key";
			cboStatus.DisplayMember = "Value";
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count > 0)
			{
				ApplyValue(values[0]);
			}
		}

		public override void BuildMacro(List<string> values)
		{
			values.Add(BuildValue() ?? "");
		}

		protected override void OnBoundData()
		{
			string value = GetValue()?.ToString();
			value = ApplyValue(value);
			chkNegate.CheckedChanged += ChkNegate_CheckedChanged;
			cboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;
		}

		private string ApplyValue(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (value.StartsWith("not_"))
				{
					value = value.Substring(4, value.Length - 4);
					chkNegate.Checked = true;
				}
				cboStatus.SelectedValue = value;
			}

			return value;
		}

		protected override void OnClear()
		{
			chkNegate.Checked = false;
			cboStatus.SelectedIndex = 0;
			Save();
		}

		public string BuildValue()
		{
			bool inverted = chkNegate.Checked;
			string value = (string)cboStatus.SelectedValue;
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			else
			{
				if (inverted)
				{
					value = "not_" + value;
				}
				return value;
			}
		}

		protected override void OnSave()
		{
			string value = BuildValue();
			SetValue(value);
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

using System;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class BooleanControl : PropertyEditControl
	{
		private bool _autoSelect;

		public BooleanControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			BooleanAttribute attrib = parameters as BooleanAttribute;
			_autoSelect = attrib.AutoCheck;
		}

		protected override void OnBoundData()
		{
			Type propType = PropertyType;

			if (_autoSelect)
			{
				chkEnabled.Checked = true;
				Save();
			}
			else
			{
				if (propType == typeof(bool))
				{
					chkEnabled.Checked = (bool)GetValue();
				}
				else
				{
					string value = GetValue()?.ToString();
					if (!string.IsNullOrEmpty(value) && value != "0")
					{
						chkEnabled.Checked = true;
					}
				}
			}

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

		/// <summary>
		/// If true, adding this property will automatically set it true
		/// </summary>
		public bool AutoCheck { get; set; }
	}
}

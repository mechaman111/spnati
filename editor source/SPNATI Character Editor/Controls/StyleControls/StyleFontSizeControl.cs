using System;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor.Controls.StyleControls
{
	[SubAttribute("font-size", "Font size")]
	public partial class StyleFontSizeControl : SubAttributeControl
	{
		public StyleFontSizeControl()
		{
			InitializeComponent();
		}

		private bool _bound = false;

		protected override void OnBoundData()
		{
			if (!_bound)
			{
				_bound = true;
				Attribute.PropertyChanged += Attribute_PropertyChanged;
			}
			string value = Attribute.Value;

			radPct.Checked = true;
			if (value.EndsWith("em"))
			{
				radEm.Checked = true;
			}

			float num;
			value = Regex.Replace(value, @"[^0-9\.]*", "");
			float.TryParse(value, out num);
			if (num == 0)
			{
				num = 100;
			}
			valSize.Value = Math.Min(valSize.Maximum, Math.Max(valSize.Minimum, (int)num));
		}

		protected override void OnDestroy()
		{
			Attribute.PropertyChanged -= Attribute_PropertyChanged;
			base.OnDestroy();
		}

		private void Attribute_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (IsUpdating) { return; }
			RemoveHandlers();
			OnBoundData();
			AddHandlers();
		}

		protected override void AddHandlers()
		{
			valSize.ValueChanged += ValSize_ValueChanged;
			radPct.CheckedChanged += ValSize_ValueChanged;
			radEm.CheckedChanged += ValSize_ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			valSize.ValueChanged -= ValSize_ValueChanged;
			radPct.CheckedChanged -= ValSize_ValueChanged;
			radEm.CheckedChanged -= ValSize_ValueChanged;
		}

		private void ValSize_ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnSave()
		{
			int size = (int)valSize.Value;
			string unit = "%";

			if (radEm.Checked)
			{
				unit = "em";
			}

			Attribute.Value = $"{size}{unit}";
		}
	}
}

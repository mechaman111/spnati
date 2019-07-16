using System;

namespace SPNATI_Character_Editor.Controls.StyleControls
{
	[SubAttribute("font-weight", "Font weight")]
	public partial class StyleFontWeightControl : SubAttributeControl
	{
		public StyleFontWeightControl()
		{
			InitializeComponent();
		}

		private bool _bound;

		protected override void OnBoundData()
		{
			if (!_bound)
			{
				_bound = true;
				Attribute.PropertyChanged += Attribute_PropertyChanged;
			}
			string weight = Attribute.Value;
			if (string.IsNullOrEmpty(weight))
			{
				weight = "400";
			}
			weight = weight.ToLower();
			if (weight == "400" || weight == "normal")
			{
				radNormal.Checked = true;
				valWeight.Value = 400;
			}
			else if (weight == "700" || weight == "bold")
			{
				radBold.Checked = true;
				valWeight.Value = 700;
			}
			else
			{
				int wt;
				radBold.Checked = false;
				radNormal.Checked = false;
				if (int.TryParse(Attribute.Value, out wt))
				{
					valWeight.Value = Math.Min(valWeight.Maximum, Math.Max(valWeight.Minimum, wt));
				}
				else
				{
					valWeight.Value = 400;
				}
			}
		}

		protected override void OnDestroy()
		{
			Attribute.PropertyChanged -= Attribute_PropertyChanged;
			base.OnDestroy();
		}

		protected override void RemoveHandlers()
		{
			radBold.CheckedChanged -= RadBold_CheckedChanged;
			radNormal.CheckedChanged -= RadBold_CheckedChanged;
			valWeight.ValueChanged -= ValWeight_ValueChanged;
		}

		protected override void AddHandlers()
		{
			radBold.CheckedChanged += RadBold_CheckedChanged;
			radNormal.CheckedChanged += RadBold_CheckedChanged;
			valWeight.ValueChanged += ValWeight_ValueChanged;
		}

		private void ValWeight_ValueChanged(object sender, EventArgs e)
		{
			RemoveHandlers();
			if (valWeight.Value == 400)
			{
				radNormal.Checked = true;
			}
			else if (valWeight.Value == 700)
			{
				radBold.Checked = true;
			}
			else
			{
				radBold.Checked = false;
				radNormal.Checked = false;
			}
			Save();
			AddHandlers();
		}

		private void RadBold_CheckedChanged(object sender, EventArgs e)
		{
			RemoveHandlers();
			if (radNormal.Checked)
			{
				valWeight.Value = 400;
			}
			else if (radBold.Checked)
			{
				valWeight.Value = 700;
			}
			AddHandlers();
			Save();
		}

		private void Attribute_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (IsUpdating) { return; }
			RemoveHandlers();
			OnBoundData();
			AddHandlers();
		}

		protected override void OnSave()
		{
			int weight = (int)valWeight.Value;
			Attribute.Value = weight.ToString();
		}
	}
}

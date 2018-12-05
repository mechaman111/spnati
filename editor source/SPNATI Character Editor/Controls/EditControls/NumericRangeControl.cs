using Desktop;
using Desktop.CommonControls;
using Desktop.CommonControls.PropertyControls;
using System;
using System.ComponentModel;

namespace SPNATI_Character_Editor
{
	[ToolboxItem(true)]
	public partial class NumericRangeControl : PropertyEditControl
	{
		public NumericRangeControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			NumericAttribute p = parameters as NumericAttribute;
			valFrom.Minimum = p.Minimum;
			valFrom.Maximum = p.Maximum;
			valTo.Minimum = p.Minimum;
			valTo.Maximum = p.Maximum;
		}

		protected override void OnBoundData()
		{
			string range = GetValue()?.ToString();
			if (range == null)
			{
				valFrom.Value = 0;
				valTo.Text = "";
				return;
			}
			string[] pieces = range.Split('-');
			int from;
			int to;
			if (int.TryParse(pieces[0], out from))
			{
				valFrom.Value = Math.Max(valFrom.Minimum, Math.Min(valFrom.Maximum, from));
			}
			if (pieces.Length > 1)
			{
				if (int.TryParse(pieces[1], out to))
				{
					valTo.Value = Math.Max(valTo.Minimum, Math.Min(valTo.Maximum, to));
				}
			}
			else
			{
				valTo.Text = "";
			}
		}

		public override void Clear()
		{
			valFrom.Text = "";
			valTo.Text = "";
			Save();
		}

		public override void Save()
		{
			int from = (int)valFrom.Value;
			int to = (int)valTo.Value;
			if (valFrom.Text == "")
			{
				from = -1;
			}
			if (valTo.Text == "")
			{
				to = -1;
			}
			if (from == -1)
			{
				SetValue(null);
			}
			else if (to <= from)
			{
				SetValue(from.ToString());
			}
			else
			{
				SetValue($"{from}-{to}");
			}
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}
	}

	public class NumericRangeAttribute : NumericAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(NumericRangeControl); }
		}
	}
}

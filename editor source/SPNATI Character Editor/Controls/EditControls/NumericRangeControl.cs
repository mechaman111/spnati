using Desktop;
using Desktop.CommonControls;
using Desktop.CommonControls.PropertyControls;
using System;
using System.ComponentModel;
using System.Windows.Forms;

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

		private void AddHandlers()
		{
			valFrom.ValueChanged += ValueChanged;
			valTo.ValueChanged += ValueChanged;
			valFrom.TextChanged += Value_TextChanged;
			valTo.TextChanged += Value_TextChanged;
			chkUpper.CheckedChanged += ValueChanged;
		}

		private void RemoveHandlers()
		{
			valFrom.ValueChanged -= ValueChanged;
			valTo.ValueChanged -= ValueChanged;
			valFrom.TextChanged -= Value_TextChanged;
			valTo.TextChanged -= Value_TextChanged;
			chkUpper.CheckedChanged -= ValueChanged;
		}

		protected override void OnBoundData()
		{
			chkUpper.Checked = false;
			string range = GetValue()?.ToString();
			if (range == null)
			{
				valFrom.Text = "";
				valTo.Text = "";
				AddHandlers();
				return;
			}
			string[] pieces = range.Split('-');
			int from;
			int to;
			if (int.TryParse(pieces[0], out from))
			{
				valFrom.Value = Math.Max(valFrom.Minimum, Math.Min(valFrom.Maximum, from));
			}
			else
			{
				//open range
				valFrom.Text = "";
			}
			if (pieces.Length > 1)
			{
				if (int.TryParse(pieces[1], out to))
				{
					valTo.Value = Math.Max(valTo.Minimum, Math.Min(valTo.Maximum, to));
				}
				else
				{
					//open upper range
					valTo.Text = "";
					chkUpper.Checked = true;
				}
			}
			else
			{
				valTo.Text = "";
			}
			AddHandlers();
		}

		public override void Clear()
		{
			RemoveHandlers();
			valFrom.Text = "";
			valTo.Text = "";
			AddHandlers();
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
				to = from;
			}
			if (chkUpper.Checked)
			{
				to = -1;
			}
			SetValue(GUIHelper.ToRange(from, to));
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void Value_TextChanged(object sender, EventArgs e)
		{
			NumericUpDown ctl = sender as NumericUpDown;
			if (ctl?.Text == "")
			{
				Save();
			}
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

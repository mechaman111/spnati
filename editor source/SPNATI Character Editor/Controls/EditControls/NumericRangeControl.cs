using Desktop;
using Desktop.CommonControls;
using Desktop.CommonControls.PropertyControls;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	[ToolboxItem(true)]
	public partial class NumericRangeControl : PropertyEditControl
	{
		public NumericRangeControl()
		{
			InitializeComponent();
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count > 0)
			{
				string value = values[0];
				ApplyValue(value);
			}
		}

		public override void BuildMacro(List<string> values)
		{
			values.Add(BuildValue() ?? "");
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			NumericAttribute p = parameters as NumericAttribute;
			valFrom.Minimum = p.Minimum;
			valFrom.Maximum = p.Maximum;
			valTo.Minimum = p.Minimum;
			valTo.Maximum = p.Maximum;
		}

		protected override void AddHandlers()
		{
			valFrom.TextChanged += Value_TextChanged;
			valTo.TextChanged += Value_TextChanged;
			chkUpper.CheckedChanged += ValueChanged;
		}

		protected override void RemoveHandlers()
		{
			valFrom.TextChanged -= Value_TextChanged;
			valTo.TextChanged -= Value_TextChanged;
			chkUpper.CheckedChanged -= ValueChanged;
		}

		protected override void OnBoundData()
		{
			chkUpper.Checked = false;
			string value = GetValue()?.ToString();
			ApplyValue(value);
		}

		private void ApplyValue(string value)
		{
			string range = value;
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
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			valFrom.Text = "";
			valTo.Text = "";
			AddHandlers();
			Save();
		}

		private string BuildValue()
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
			string value = GUIHelper.ToRange(from, to);
			return value;
		}

		protected override void OnSave()
		{
			string value = BuildValue();	
			SetValue(value);
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void Value_TextChanged(object sender, EventArgs e)
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

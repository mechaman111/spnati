using System;
using Desktop;
using Desktop.CommonControls;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class FilterControl : PropertyEditControl
	{
		private TargetCondition _filter;

		public FilterControl()
		{
			InitializeComponent();

			recTag.RecordType = typeof(Tag);
			cboStatus.DataSource = TargetCondition.StatusTypes;
			cboStatus.ValueMember = "Key";
			cboStatus.DisplayMember = "Value";
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count >= 5)
			{
				string count = values[0];
				string tag = values[1];
				string gender = values[2];
				bool inverted = (values[3] == "1");
				string status = values[4];
				chkNot.Checked = inverted;
				cboGender.SelectedItem = gender;
				if (cboGender.SelectedItem == null)
				{
					cboGender.SelectedIndex = 0;
				}
				recTag.RecordKey = tag;
				cboStatus.SelectedValue = status ?? "";
				SetCount(count);
			}
		}

		public override void BuildMacro(List<string> values)
		{
			string count = GetCount() ?? "0";
			string tag = recTag.RecordKey;
			string gender = cboGender.SelectedItem?.ToString();
			bool inverted = chkNot.Checked;
			string status = (string)cboStatus.SelectedValue;
			values.Add(count);
			values.Add(tag ?? "");
			values.Add(gender ?? "");
			values.Add(inverted ? "1" : "");
			values.Add(status);
		}

		protected override void OnBoundData()
		{
			_filter = GetValue() as TargetCondition;

			SetCount(_filter.Count);
			chkNot.Checked = _filter.NegateStatus;
			cboGender.SelectedItem = _filter.Gender;
			if (cboGender.SelectedItem == null)
			{
				cboGender.SelectedIndex = 0;
			}
			recTag.RecordKey = _filter.Filter;
			chkNot.Checked = _filter.NegateStatus;
			cboStatus.SelectedValue = _filter.StatusType ?? "";

			AddHandlers();
		}

		private void SetCount(string range)
		{
			if (range == null)
			{
				valFrom.Value = 0;
				valTo.Value = 0;
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
					valTo.Text = "";
				}
			}
			else
			{
				valTo.Value = valFrom.Value;
			}
		}

		private void RemoveHandlers()
		{
			valFrom.ValueChanged -= ValueChanged;
			valTo.ValueChanged -= ValueChanged;
			valFrom.TextChanged -= Value_TextChanged;
			valTo.TextChanged -= Value_TextChanged;
			cboStatus.SelectedIndexChanged -= ValueChanged;
			cboGender.SelectedIndexChanged -= ValueChanged;
			recTag.RecordChanged -= RecordChanged;
			chkNot.CheckedChanged -= ValueChanged;
		}

		private void AddHandlers()
		{
			valFrom.ValueChanged += ValueChanged;
			valTo.ValueChanged += ValueChanged;
			valFrom.TextChanged += Value_TextChanged;
			valTo.TextChanged += Value_TextChanged;
			cboStatus.SelectedIndexChanged += ValueChanged;
			cboGender.SelectedIndexChanged += ValueChanged;
			recTag.RecordChanged += RecordChanged;
			chkNot.CheckedChanged += ValueChanged;
		}

		private void RecordChanged(object sender, IRecord record)
		{
			Save();
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

		private string GetCount()
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
			return GUIHelper.ToRange(from, to);
		}

		public override void Clear()
		{
			RemoveHandlers();
			valFrom.Text = "";
			valTo.Text = "";
			cboStatus.SelectedIndex = 0;
			cboGender.SelectedIndex = 0;
			recTag.RecordKey = null;
			chkNot.Checked = false;
			Save();
			AddHandlers();
		}

		public override void Save()
		{
			string count = GetCount() ?? "0";
			string tag = recTag.RecordKey;
			string gender = cboGender.SelectedItem?.ToString();
			bool inverted = chkNot.Checked;
			string status = (string)cboStatus.SelectedValue;
			_filter.Count = count;
			_filter.Gender = gender;
			_filter.NegateStatus = inverted;
			_filter.StatusType = status;
			_filter.Filter = tag;
		}
	}

	public class FilterAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(FilterControl); }
		}
	}
}

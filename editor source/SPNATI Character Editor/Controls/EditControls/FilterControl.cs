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

		private bool _collapsed;

		public FilterControl()
		{
			InitializeComponent();

			recTag.RecordType = typeof(Tag);
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count >= 4)
			{
				string count = values[0];
				string tag = values[1];
				string gender = values[2];
				string status = values[3];
				cboGender.SelectedItem = gender;
				if (cboGender.SelectedItem == null)
				{
					cboGender.SelectedIndex = 0;
				}
				recTag.RecordKey = tag;
				SetCount(count);

				_filter.Status = status;

				RebindTable();

				ToggleCollapsed(!_filter.HasAdvancedConditions);
			}
		}

		public override void BuildMacro(List<string> values)
		{
			string count = GetCount() ?? "0";
			string tag = recTag.RecordKey;
			string gender = cboGender.SelectedItem?.ToString();
			values.Add(count);
			values.Add(tag ?? "");
			values.Add(gender ?? "");
			values.Add(_filter.Status);
		}

		protected override void OnBoundData()
		{
			_filter = GetValue() as TargetCondition;
			SetCount(_filter.Count);
			cboGender.SelectedItem = _filter.Gender;
			if (cboGender.SelectedItem == null)
			{
				cboGender.SelectedIndex = 0;
			}
			recTag.RecordKey = _filter.FilterTag;

			tableAdvanced.Data = _filter;
		}

		public override void OnAddedToRow()
		{
			ToggleCollapsed(!_filter.HasAdvancedConditions);
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

		protected override void RemoveHandlers()
		{
			valFrom.ValueChanged -= ValueChanged;
			valTo.ValueChanged -= ValueChanged;
			valFrom.TextChanged -= Value_TextChanged;
			valTo.TextChanged -= Value_TextChanged;
			cboGender.SelectedIndexChanged -= ValueChanged;
			recTag.RecordChanged -= RecordChanged;
		}

		protected override void AddHandlers()
		{
			valFrom.ValueChanged += ValueChanged;
			valTo.ValueChanged += ValueChanged;
			valFrom.TextChanged += Value_TextChanged;
			valTo.TextChanged += Value_TextChanged;
			cboGender.SelectedIndexChanged += ValueChanged;
			recTag.RecordChanged += RecordChanged;
		}

		private void RecordChanged(object sender, RecordEventArgs e)
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
			cboGender.SelectedIndex = 0;
			recTag.RecordKey = null;
			_filter.Status = null;

			RebindTable();

			Save();
			AddHandlers();
		}

		private void RebindTable()
		{
			//TODO: Once properties serialize properly with SpnatiXmlSerializer, we can switch TargetCondition to use a BindableObject, make
			//the fields properties, and get rid of this method
			tableAdvanced.UpdateProperty("Status");
		}

		public override void Save()
		{
			string count = GetCount() ?? "0";
			string tag = recTag.RecordKey;
			string gender = cboGender.SelectedItem?.ToString();
			_filter.Count = count;
			_filter.Gender = gender;
			_filter.FilterTag = tag;
			tableAdvanced.Save();
		}

		private void cmdExpand_Click(object sender, EventArgs e)
		{
			ToggleCollapsed(!_collapsed);
		}

		/// <summary>
		/// Displays or hides the advanced property table
		/// </summary>
		/// <param name="collapsed"></param>
		private void ToggleCollapsed(bool collapsed)
		{
			_collapsed = collapsed;
			if (_collapsed)
			{
				cmdExpand.Image = Properties.Resources.ChevronDown;
				OnRequireHeight(22);
				
			}
			else
			{
				cmdExpand.Image = Properties.Resources.ChevronUp;
				OnRequireHeight(175);
			}
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

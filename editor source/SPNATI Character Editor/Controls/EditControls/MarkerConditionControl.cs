using Desktop;
using Desktop.CommonControls;
using System;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor
{
	public partial class MarkerConditionControl : PropertyEditControl
	{
		public MarkerConditionControl()
		{
			InitializeComponent();

			recField.RecordType = typeof(Marker);
		}

		protected override void OnBindingUpdated(string property)
		{
			recField.RecordContext = CharacterDatabase.Get(GetBindingValue(property)?.ToString());
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			MarkerConditionAttribute attr = parameters as MarkerConditionAttribute;
			if (!attr.ShowPrivate)
			{
				recField.RecordFilter = FilterPrivateMarkers;
			}
		}

		private bool FilterPrivateMarkers(IRecord record)
		{
			Marker marker = record as Marker;
			return marker.Scope == MarkerScope.Public;
		}

		protected override void OnBoundData()
		{
			recField.UseAutoComplete = true;
			if (Bindings.Count > 0)
			{
				OnBindingUpdated(Bindings[0]);
			}
			else
			{
				recField.RecordContext = Context;
			}
			
			cboOperator.SelectedIndex = 0;

			string pattern = @"^([-\w\.]+)(\*?)(\s*(\<|\>|\<\=|\>\=|\<\=|\=\=?)\s*([-\w]+|~[-\w]+~))?$";
			Regex regex = new Regex(pattern);
			Match match = regex.Match(GetValue()?.ToString() ?? "");
			if (match.Success)
			{
				string name = match.Groups[1].Value;
				string perTarget = match.Groups[2].Value;
				string op = match.Groups[4].Value;
				string value = match.Groups[5].Value;
				recField.RecordKey = name;
				chkPerTarget.Checked = perTarget == "*";
				if (!string.IsNullOrEmpty(op))
				{
					if (op == "=")
					{
						op = "==";
					}
					cboOperator.SelectedItem = op;
				}
				txtValue.Text = value;
			}

			AddHandlers();
		}

		private void RemoveHandlers()
		{
			recField.RecordChanged -= RecordChanged;
			cboOperator.SelectedIndexChanged -= ValueChanged;
			txtValue.TextChanged -= ValueChanged;
			chkPerTarget.CheckedChanged -= ValueChanged;
		}

		private void AddHandlers()
		{
			recField.RecordChanged += RecordChanged;
			cboOperator.SelectedIndexChanged += ValueChanged;
			txtValue.TextChanged += ValueChanged;
			chkPerTarget.CheckedChanged += ValueChanged;
		}

		public override void Clear()
		{
			RemoveHandlers();
			recField.RecordKey = null;
			cboOperator.SelectedIndex = 0;
			txtValue.Text = "";
			chkPerTarget.Checked = false;
			AddHandlers();
			Save();
		}

		public override void Save()
		{
			string record = recField.RecordKey;
			if (string.IsNullOrEmpty(record))
			{
				SetValue(null);
				return;
			}
			string op = cboOperator.SelectedItem?.ToString();
			string value = txtValue.Text;
			bool perTarget = chkPerTarget.Checked;
			if (perTarget)
			{
				record += "*";
			}

			if (!string.IsNullOrEmpty(op) && !string.IsNullOrEmpty(value))
			{
				record += op + value;
			}
			SetValue(record);
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void RecordChanged(object sender, IRecord record)
		{
			Save();
		}
	}

	public class MarkerConditionAttribute : MarkerAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(MarkerConditionControl); }
		}
	}
}

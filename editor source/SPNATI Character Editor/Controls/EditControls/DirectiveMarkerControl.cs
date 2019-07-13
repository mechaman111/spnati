using Desktop;
using Desktop.CommonControls;
using System;
using System.Text.RegularExpressions;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class DirectiveMarkerControl : PropertyEditControl
	{
		public DirectiveMarkerControl()
		{
			InitializeComponent();
			cboOperator.DataSource = ExpressionTest.Operators;
			recField.RecordType = typeof(Marker);
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			DirectiveMarkerAttribute attr = parameters as DirectiveMarkerAttribute;
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
			ICharacterContext context = Context as ICharacterContext;
			if (context != null)
			{
				recField.RecordContext = context.Character;
			}
			recField.UseAutoComplete = true;

			recField.RecordKey = null;
			txtValue.Text = "";
			cboOperator.SelectedIndex = 0;

			string pattern = @"^([-\w\.]+)(\*?)(\s*(\<|\>|\<\=|\>\=|\<\=|\=\=|!\=?)\s*([-\w]+|~[-\w]+~))?$";
			Regex regex = new Regex(pattern);
			Match match = regex.Match(GetValue()?.ToString() ?? "");
			if (match.Success)
			{
				string name = match.Groups[1].Value;
				string op = match.Groups[4].Value;
				string value = match.Groups[5].Value;
				recField.RecordKey = name;
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
		}

		protected override void RemoveHandlers()
		{
			recField.RecordChanged -= RecordChanged;
			cboOperator.SelectedIndexChanged -= ValueChanged;
			txtValue.TextChanged -= ValueChanged;
		}

		protected override void AddHandlers()
		{
			recField.RecordChanged += RecordChanged;
			cboOperator.SelectedIndexChanged += ValueChanged;
			txtValue.TextChanged += ValueChanged;
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			recField.RecordKey = null;
			cboOperator.SelectedIndex = 0;
			txtValue.Text = "";
			AddHandlers();
			Save();
		}

		protected override void OnSave()
		{
			string record = recField.RecordKey;
			if (string.IsNullOrEmpty(record))
			{
				SetValue(null);
				return;
			}
			string op = cboOperator.SelectedItem?.ToString();
			string value = txtValue.Text;

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

		private void RecordChanged(object sender, RecordEventArgs e)
		{
			Save();
		}
	}

	public class DirectiveMarkerAttribute : MarkerAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(DirectiveMarkerControl); }
		}
	}
}

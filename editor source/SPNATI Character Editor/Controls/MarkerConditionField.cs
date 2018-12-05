using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class MarkerConditionField : UserControl
	{
		private Dictionary<MarkerOperator, string> _displayMap = new Dictionary<MarkerOperator, string>();
		private Dictionary<string, MarkerOperator> _operatorMap = new Dictionary<string, MarkerOperator>();

		public MarkerConditionField()
		{
			InitializeComponent();

			foreach (MarkerOperator op in Enum.GetValues(typeof(MarkerOperator)))
			{
				string display = op.Serialize();
				_displayMap[op] = display;
				_operatorMap[display] = op;
				cboOperator.Items.Add(display);
			}
		}

		/// <summary>
		/// Sets the auto-complete list based on a character's markers
		/// </summary>
		/// <param name="character"></param>
		public void SetDataSource(Character character, bool allowPrivate)
		{
			string oldText = cboMarker.Text;
			cboMarker.Items.Clear();
			cboMarker.Text = "";
			if (character == null)
				return;

			foreach (Marker marker in character.Markers.Values)
			{
				if (allowPrivate || marker.Scope == MarkerScope.Public)
				{
					cboMarker.Items.Add(marker.Name);
				}
			}

			if (!string.IsNullOrEmpty(oldText))
			{
				cboMarker.Text = oldText;
			}
		}

		public string Value
		{
			get
			{
				if (string.IsNullOrEmpty(cboMarker.Text))
				{
					return null;
				}
				string marker = cboMarker.Text;
				MarkerOperator op = _operatorMap[cboOperator.SelectedItem?.ToString() ?? "=="];
				string value = txtValue.Text;
				if (!string.IsNullOrEmpty(value))
				{
					marker = $"{marker}{op.Serialize()}{value}";
				}
				return marker;
			}
			set
			{
				if (value == null)
				{
					cboMarker.Text = "";
					cboOperator.SelectedIndex = 0;
					txtValue.Text = "";
				}
				else
				{
					string marker;
					MarkerOperator op;
					string rhs;
					marker = Marker.SplitConditional(value, out op, out rhs);

					cboMarker.Text = marker;
					cboOperator.SelectedItem = _displayMap[op];
					txtValue.Text = rhs;
				}
			}
		}
	}
}

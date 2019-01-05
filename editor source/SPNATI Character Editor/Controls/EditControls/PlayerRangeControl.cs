using System;
using Desktop;
using Desktop.CommonControls;

namespace SPNATI_Character_Editor
{
	public partial class PlayerRangeControl : PropertyEditControl
	{
		public PlayerRangeControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			string range = GetValue()?.ToString() ?? "";
			string[] pieces = range.Split('-');
			cboMin.SelectedItem = pieces[0];
			if (pieces.Length > 1)
			{
				cboMax.SelectedItem = pieces[1];
			}
			else
			{
				cboMax.SelectedItem = pieces[0];
			}
		}

		public override void Clear()
		{
			cboMin.Text = "";
			cboMax.Text = "";
			SetValue(null);
		}

		public override void Save()
		{
			string min = cboMin.Text;
			string max = cboMax.Text;
			if (string.IsNullOrEmpty(min) && string.IsNullOrEmpty(max))
			{
				SetValue(null);
				return;
			}

			int from;
			int to;
			if (!int.TryParse(min, out from))
			{
				from = -1;
			}
			if (!int.TryParse(max, out to))
			{
				to = -1;
			}
			SetValue(GUIHelper.ToRange(from, to));
		}

		private void cboMin_SelectedIndexChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void cboMax_SelectedIndexChanged(object sender, EventArgs e)
		{
			Save();
		}
	}

	public class PlayerRangeAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get	{ return typeof(PlayerRangeControl); }
		}
	}
}

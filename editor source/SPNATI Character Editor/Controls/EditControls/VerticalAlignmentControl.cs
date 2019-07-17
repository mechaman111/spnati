using Desktop;
using Desktop.CommonControls;
using System;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class VerticalAlignmentControl : PropertyEditControl
	{
		public VerticalAlignmentControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			string value = GetValue()?.ToString();
			if (value == "top")
			{
				chkTop.Checked = true;
			}
			else if (value == "center")
			{
				chkMiddle.Checked = true;
			}
			else if (value == "bottom")
			{
				chkBottom.Checked = true;
			}
		}

		protected override void AddHandlers()
		{
			chkTop.CheckedChanged += CheckedChanged;
			chkMiddle.CheckedChanged += CheckedChanged;
			chkBottom.CheckedChanged += CheckedChanged;
		}

		protected override void RemoveHandlers()
		{
			chkTop.CheckedChanged -= CheckedChanged;
			chkMiddle.CheckedChanged -= CheckedChanged;
			chkBottom.CheckedChanged -= CheckedChanged;
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			Save();
		}

		protected override void OnClear()
		{
			RemoveHandlers();
			chkTop.Checked = false;
			chkMiddle.Checked = false;
			chkBottom.Checked = false;
			AddHandlers();
		}

		protected override void OnSave()
		{
			if (chkTop.Checked)
			{
				SetValue("top");
			}
			else if (chkMiddle.Checked)
			{
				SetValue("center");
			}
			else if (chkBottom.Checked)
			{
				SetValue("bottom");
			}
			else
			{
				SetValue(null);
			}
		}
	}

	public class VerticalAlignmentAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get
			{
				return typeof(VerticalAlignmentControl);
			}
		}
	}
}

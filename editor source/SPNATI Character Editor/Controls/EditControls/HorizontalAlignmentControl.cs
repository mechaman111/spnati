using Desktop;
using Desktop.CommonControls;
using System;

namespace SPNATI_Character_Editor.Controls.EditControls
{
	public partial class HorizontalAlignmentControl : PropertyEditControl
	{
		public HorizontalAlignmentControl()
		{
			InitializeComponent();
		}

		protected override void OnBoundData()
		{
			string value = GetValue()?.ToString();
			if (value == "left")
			{
				chkLeft.Checked = true;
			}
			else if (value == "center")
			{
				chkMiddle.Checked = true;
			}
			else if (value == "right")
			{
				chkRight.Checked = true;
			}

			AddHandlers();
		}

		private void AddHandlers()
		{
			chkLeft.CheckedChanged += CheckedChanged;
			chkMiddle.CheckedChanged += CheckedChanged;
			chkRight.CheckedChanged += CheckedChanged;
		}

		private void RemoveHandlers()
		{
			chkLeft.CheckedChanged -= CheckedChanged;
			chkMiddle.CheckedChanged -= CheckedChanged;
			chkRight.CheckedChanged -= CheckedChanged;
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			Save();
		}

		public override void Clear()
		{
			RemoveHandlers();
			chkLeft.Checked = false;
			chkMiddle.Checked = false;
			chkRight.Checked = false;
			AddHandlers();
		}

		public override void Save()
		{
			if (chkLeft.Checked)
			{
				SetValue("left");
			}
			else if (chkMiddle.Checked)
			{
				SetValue("center");
			}
			else if (chkRight.Checked)
			{
				SetValue("right");
			}
			else
			{
				SetValue(null);
			}
		}
	}

	public class HorizontalAlignmentAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get
			{
				return typeof(HorizontalAlignmentControl);
			}
		}
	}
}

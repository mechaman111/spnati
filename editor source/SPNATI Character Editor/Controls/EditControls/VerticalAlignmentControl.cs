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

			AddHandlers();
		}

		private void AddHandlers()
		{
			chkTop.CheckedChanged += CheckedChanged;
			chkMiddle.CheckedChanged += CheckedChanged;
			chkBottom.CheckedChanged += CheckedChanged;
		}

		private void RemoveHandlers()
		{
			chkTop.CheckedChanged -= CheckedChanged;
			chkMiddle.CheckedChanged -= CheckedChanged;
			chkBottom.CheckedChanged -= CheckedChanged;
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			Save();
		}

		public override void Clear()
		{
			RemoveHandlers();
			chkTop.Checked = false;
			chkMiddle.Checked = false;
			chkBottom.Checked = false;
			AddHandlers();
		}

		public override void Save()
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

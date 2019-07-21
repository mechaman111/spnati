using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public class GroupRadioButton : SkinnedRadioButton
	{
		public string GroupName { get; set; }

		public GroupRadioButton() : base()
		{
			CheckedChanged += GroupRadioButton_CheckedChanged;
		}

		private void GroupRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (Checked)
			{
				//limit to two levels high, though there's no reason we couldn't make it cover the whole form
				Control parent = Parent;
				if (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				DisableRadioButtons(parent);
			}
		}

		private void DisableRadioButtons(Control parent)
		{
			foreach (Control ctl in parent.Controls)
			{
				if (ctl is GroupRadioButton)
				{
					GroupRadioButton other = ctl as GroupRadioButton;
					if (other != this && other.GroupName == GroupName)
					{
						other.Checked = false;
					}
					break;
				}
				else
				{
					DisableRadioButtons(ctl);
				}
			}
		}
	}
}

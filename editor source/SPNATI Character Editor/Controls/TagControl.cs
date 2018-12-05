using Desktop;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class TagControl : UserControl
	{
		private const int TitleHeight = 25;

		private Dictionary<string, Control> _controlMap = new Dictionary<string, Control>();

		public TagControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Builds the checkboxes for the group
		/// </summary>
		/// <param name="group"></param>
		public void SetGroup(TagGroup group, Character character)
		{
			string gender = character.Gender;
			grpBox.Text = group.Label;
			int width = (flowPanel.Width - (flowPanel.Padding.Left - flowPanel.Padding.Right) * 2 - 5);
			if (!group.MultiSelect)
			{
				Control none = new RadioButton();
				none.Width = width;
				none.Margin = new Padding(0);
				none.Text = "N/A";
				flowPanel.Controls.Add(none);
			}
			foreach (Tag tag in group.Tags)
			{
				if (string.IsNullOrEmpty(tag.Gender) || gender == tag.Gender)
				{
					Control input = null;
					if (group.MultiSelect)
					{
						input = new CheckBox();
						input.Text = tag.DisplayName;
					}
					else
					{
						input = new RadioButton();
						input.Text = tag.DisplayName;
					}
					toolTip1.SetToolTip(input, tag.Description);
					input.Width = width;
					input.Margin = new Padding(0);
					input.Tag = tag;
					_controlMap[tag.Value] = input;
					flowPanel.Controls.Add(input);
				}
			}

			//size the control to fit its contents
			Height = TitleHeight + flowPanel.Height;
		}

		/// <summary>
		/// Auto-selects any tags from the input list
		/// </summary>
		/// <param name="tags"></param>
		public void CheckTags(List<string> tags)
		{
			for (int i = tags.Count - 1; i >= 0; i--)
			{
				string tag = tags[i];
				Control matchingControl = _controlMap.Get(tag);
				if (matchingControl != null)
				{
					if (matchingControl is CheckBox)
					{
						((CheckBox)matchingControl).Checked = true;
					}
					else if (matchingControl is RadioButton)
					{
						((RadioButton)matchingControl).Checked = true;
					}
					tags.RemoveAt(i); //remove so that whatever's leftover can go to misc
				}
			}
		}

		/// <summary>
		/// Gets a list of tags selected
		/// </summary>
		/// <returns></returns>
		public List<string> GetTags()
		{
			List<string> tags = new List<string>();
			foreach (Control ctl in flowPanel.Controls)
			{
				if ((ctl is RadioButton && (ctl as RadioButton).Checked) ||
					(ctl is CheckBox && (ctl as CheckBox).Checked))
				{
					Tag tag = ctl.Tag as Tag;
					if (tag != null)
					{
						tags.Add(tag.Value);
						if (!string.IsNullOrEmpty(tag.PairedTag))
						{
							tags.Add(tag.PairedTag);
						}
					}
				}
			}
			return tags;
		}
	}
}

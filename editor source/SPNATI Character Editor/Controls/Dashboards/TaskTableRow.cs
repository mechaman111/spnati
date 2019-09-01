using Desktop.Skinning;
using SPNATI_Character_Editor.DataStructures;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class TaskTableRow : UserControl
	{
		public ChecklistTask Task { get; private set; }

		public TaskTableRow()
		{
			InitializeComponent();
		}

		public TaskTableRow(ChecklistTask task) : this()
		{
			Task = task;

			Build();
		}

		private void Build()
		{
			if (Task == null) { return; }
			lblTask.Text = Task.Text;
			link.LaunchParameters = Task.LaunchData;

			if (Task.ProgressBased)
			{
				fill.Minimum = 0;
				fill.Maximum = Task.MaxValue;
				fill.Value = Task.Value;
			}
			else
			{
				table.Controls.Remove(fill);
				if (Task.Value > 0)
				{
					SkinnedLabel valLabel = new SkinnedLabel();
					valLabel.Highlight = SkinnedHighlight.Heading;
					valLabel.Text = $"({Task.Value})";
					table.Controls.Add(valLabel, 2, 0);
					valLabel.Anchor = AnchorStyles.Left;
					valLabel.Location = new Point(valLabel.Location.X, 3);
				}
			}

			if (!string.IsNullOrEmpty(Task.HelpText))
			{
				bubbleTip.SetToolTip(cmdHelp, Task.HelpText);
				cmdHelp.Visible = true;
			}
			link.Visible = Task.LaunchData != null;
		}
	}
}

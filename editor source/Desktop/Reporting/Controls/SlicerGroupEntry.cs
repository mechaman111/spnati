using System;
using System.Windows.Forms;

namespace Desktop.Reporting.Controls
{
	public partial class SlicerGroupEntry : UserControl
	{
		public event EventHandler Delete;
		public event EventHandler Merge;

		public ISlicerGroup Group { get; private set; }
		public bool Removable
		{
			get { return cmdDelete.Visible; }
			set { cmdDelete.Visible = value; }
		}

		public SlicerGroupEntry()
		{
			InitializeComponent();
		}

		public void SetGroup(ISlicerGroup group)
		{
			Group = group;
			Group.PropertyChanged += Group_PropertyChanged;
			lblEntry.Text = group.Label;
			chkActive.Checked = group.Active;
			cmdEdit.Left = lblEntry.Right + 5;
			cmdGroup.Visible = group.Groupable;
		}

		private void Group_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Label" && lblEntry.Text != Group.Label)
			{
				lblEntry.Text = Group.Label;
				RepositionPencil();
			}
			else if (e.PropertyName == "Values")
			{
				if (Group.Values.Count > 1)
				{
					cmdGroup.Image = Properties.Resources.Grouped;
				}
				else
				{
					cmdGroup.Image = Properties.Resources.Ungrouped;
				}
			}
		}

		private void RepositionPencil()
		{
			cmdEdit.Left = lblEntry.Right + 5;
		}

		private void lblEntry_TextChanged(object sender, EventArgs e)
		{
			RepositionPencil();
		}

		private void cmdEdit_Click(object sender, EventArgs e)
		{
			txtGroupName.Text = lblEntry.Text;
			txtGroupName.Visible = true;
			txtGroupName.Focus();
		}

		private void txtGroupName_Leave(object sender, EventArgs e)
		{
			lblEntry.Text = txtGroupName.Text;
			txtGroupName.Visible = false;
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{
			Delete?.Invoke(this, EventArgs.Empty);
		}

		private void chkActive_CheckedChanged(object sender, EventArgs e)
		{
			Group.Active = chkActive.Checked;
		}

		private void txtGroupName_Validated(object sender, EventArgs e)
		{
			Group.Label = txtGroupName.Text;
		}

		private void cmdGroup_Click(object sender, EventArgs e)
		{
			Merge?.Invoke(this, EventArgs.Empty);
		}

		private void txtGroupName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Group.Label = txtGroupName.Text;
				cmdEdit.Focus();
			}
		}
	}
}

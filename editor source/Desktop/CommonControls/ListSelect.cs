using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class ListSelect : Form
	{
		public ListSelect()
		{
			InitializeComponent();
		}

		public void SetItems(string label, List<object> items)
		{
			lblName.Text = label;
			cboItems.Items.Clear();
			cboItems.Items.AddRange(items.ToArray());
		}

		public object SelectedItem
		{
			get
			{
				return cboItems.SelectedItem;
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}

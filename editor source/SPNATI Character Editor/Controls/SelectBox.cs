using System;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class SelectBox : UserControl
	{
		public event EventHandler<object> ItemAdded;
		public event EventHandler<object> ItemRemoved;

		public SelectBox()
		{
			InitializeComponent();
		}

		public string[] SelectableItems
		{
			set
			{
				cboSelectableItems.Items.Clear();
				cboSelectableItems.Items.AddRange(value);
			}
		}

		public Button AddButton
		{
			get
			{
				return cmdAdd;
			}
		}

		public string[] SelectedItems
		{
			get
			{
				return lstSelectedItems.Items.OfType<string>().ToArray();
			}
			set
			{
				lstSelectedItems.Items.Clear();
				if (value == null) return;
				foreach (string item in value)
				{
					lstSelectedItems.Items.Add(item);
					if (cboSelectableItems.Items.Contains(item))
					{
						cboSelectableItems.Items.Remove(item);
					}
				}
			}
		}

		public void Clear()
		{
			lstSelectedItems.Items.Clear();
			cboSelectableItems.Items.Clear();
		}

		private void cboSelectableItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdAdd.Enabled = cboSelectableItems.SelectedIndex >= 0 || !string.IsNullOrWhiteSpace(cboSelectableItems.Text);
		}

		private void cboSelectableItems_TextChanged(object sender, EventArgs e)
		{
			cmdAdd.Enabled = cboSelectableItems.SelectedIndex >= 0 || !string.IsNullOrWhiteSpace(cboSelectableItems.Text);
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
			if (!lstSelectedItems.Items.Contains(cboSelectableItems.Text.Trim()))
			{
				AddItem(cboSelectableItems.Text.Trim());
			}
			if (cboSelectableItems.SelectedIndex >= 0)
			{
				cboSelectableItems.Items.RemoveAt(cboSelectableItems.SelectedIndex);
			}
			cboSelectableItems.Text = null;
		}

		public void AddItem(object item)
		{
			lstSelectedItems.Items.Add(item);
			ItemAdded?.Invoke(this, item);
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{
			cboSelectableItems.Items.Add(lstSelectedItems.Text);
			object item = lstSelectedItems.Items[lstSelectedItems.SelectedIndex];
			lstSelectedItems.Items.RemoveAt(lstSelectedItems.SelectedIndex);
			ItemRemoved?.Invoke(this, item);
		}

		private void lstSelectedItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdRemove.Enabled = lstSelectedItems.SelectedIndex >= 0;
		}

		private void cboSelectableItems_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
			{
				cmdAdd_Click(this, EventArgs.Empty);
			}
		}
	}
}

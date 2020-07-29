using Desktop;
using Desktop.CommonControls;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class RecordSelectBox : UserControl
	{
		public event EventHandler<object> ItemAdded;
		public event EventHandler<object> ItemRemoved;

		public Type RecordType
		{
			get { return recField.RecordType; }
			set { recField.RecordType = value; }
		}

		public object RecordContext
		{
			get { return recField.RecordContext; }
			set { recField.RecordContext = value; }
		}

		public Func<IRecord, bool> RecordFilter
		{
			get { return recField.RecordFilter; }
			set { recField.RecordFilter = value; }
		}

		public RecordSelectBox()
		{
			InitializeComponent();
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
				}
			}
		}

		public void Clear()
		{
			lstSelectedItems.Items.Clear();
			recField.RecordKey = null;
		}

		private void recField_RecordChanged(object sender, RecordEventArgs e)
		{
			cmdAdd.Enabled = !string.IsNullOrEmpty(recField.RecordKey);
			cmdAdd_Click(sender, EventArgs.Empty);
		}
		
		private void cmdAdd_Click(object sender, EventArgs e)
		{
			string key = recField.RecordKey;
			if (string.IsNullOrWhiteSpace(key)) { return; }
			if (!lstSelectedItems.Items.Contains(key))
			{
				AddItem(key);
			}
			recField.RecordKey = null;
		}

		public void AddItem(object item)
		{
			lstSelectedItems.Items.Add(item);
			ItemAdded?.Invoke(this, item);
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{
			object item = lstSelectedItems.Items[lstSelectedItems.SelectedIndex];
			lstSelectedItems.Items.RemoveAt(lstSelectedItems.SelectedIndex);
			ItemRemoved?.Invoke(this, item);
		}

		private void lstSelectedItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdRemove.Enabled = lstSelectedItems.SelectedIndex >= 0;
		}
	}
}

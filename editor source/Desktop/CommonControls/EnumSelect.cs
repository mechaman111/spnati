using System;
using System.Reflection;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public partial class EnumSelect : UserControl
	{
		public EnumSelect()
		{
			InitializeComponent();
		}

		private Type _enumType;
		public Type EnumerationType
		{
			get { return _enumType; }
			set
			{
				_enumType = value;
				SetOptions();
			}
		}

		private void SetOptions()
		{
			if (_enumType == null)
				return;
			lstItems.Items.Clear();

			FlagsAttribute attr = _enumType.GetCustomAttribute<FlagsAttribute>();
			lstItems.MultiSelect = (attr != null);

			foreach (var value in Enum.GetValues(_enumType))
			{
				int v = (int)value;
				if (v == 0 || (v & (v - 1)) > 0)
					continue;
				ListViewItem item = new ListViewItem(value.ToString());
				item.Tag = value;
				lstItems.Items.Add(item);
			}
		}

		public void SetValues(int value)
		{
			foreach (ListViewItem item in lstItems.Items)
			{
				int itemValue = (int)item.Tag;
				if ((value & itemValue) > 0)
					item.Checked = true;
			}
		}

		public int GetValues()
		{			
			int value = 0;
			foreach (ListViewItem item in lstItems.CheckedItems)
			{
				value += (int)item.Tag;
			}
			return value;
		}
	}
}

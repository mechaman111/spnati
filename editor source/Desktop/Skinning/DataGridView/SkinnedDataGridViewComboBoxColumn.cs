using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewComboBoxColumn : DataGridViewColumn
	{
		private SkinnedDataGridViewComboBoxCell _cellTemplate;

		public SkinnedDataGridViewComboBoxColumn()
		{
			CellTemplate = _cellTemplate = new SkinnedDataGridViewComboBoxCell();
		}

		public bool AutoComplete
		{
			get { return _cellTemplate.AutoComplete; }
			set { _cellTemplate.AutoComplete = value; }
		}

		public bool Sorted
		{
			get { return _cellTemplate.Sorted; }
			set { _cellTemplate.Sorted = value; }
		}

		public string DisplayMember
		{
			get { return _cellTemplate.DisplayMember; }
			set { _cellTemplate.DisplayMember = value; }
		}

		public SkinnedComboBox.ObjectCollection Items
		{
			get { return _cellTemplate.Items; }
			set { _cellTemplate.Items = value; }
		}

		public override object Clone()
		{
			SkinnedDataGridViewComboBoxColumn copy = base.Clone() as SkinnedDataGridViewComboBoxColumn;
			copy.AutoComplete = AutoComplete;
			copy.Sorted = Sorted;
			copy.DisplayMember = DisplayMember;
			Items = copy.Items;
			return copy;
		}

	}
}
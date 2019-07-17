using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn
	{
		private SkinnedDataGridViewCheckBoxCell _cellTemplate;

		public SkinnedDataGridViewCheckBoxColumn()
		{
			CellTemplate = _cellTemplate = new SkinnedDataGridViewCheckBoxCell();
		}
	}
}

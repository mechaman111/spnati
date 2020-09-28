using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn
	{
		public SkinnedDataGridViewCheckBoxColumn()
		{
			CellTemplate = new SkinnedDataGridViewCheckBoxCell();
		}
	}
}

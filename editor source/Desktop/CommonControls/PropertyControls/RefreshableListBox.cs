using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public class RefreshableListBox : ListBox
	{
		public RefreshableListBox() : base()
		{
			DoubleBuffered = true;
		}

		public void RefreshListItems()
		{
			this.RefreshItems();
		}
	}
}

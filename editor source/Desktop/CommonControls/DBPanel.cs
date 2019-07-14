using Desktop.Skinning;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	public class DBPanel : SkinnedPanel
	{
		public DBPanel()
		{
			DoubleBuffered = true;
		}

		protected override Point ScrollToControl(Control activeControl)
		{
			return AutoScrollPosition;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			Control ctl = this;
			while (ctl != null)
			{
				SelectablePanel selectable = ctl as SelectablePanel;
				if (selectable != null)
				{
					selectable.Focus();
					break;
				}
				ctl = ctl.Parent;
			}
			base.OnMouseDown(e);
		}
	}
}

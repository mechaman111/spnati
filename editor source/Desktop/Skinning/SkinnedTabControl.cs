using System;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedTabControl : TabControl, ISkinControl
	{
		/// <summary>
		/// Hide the tabs since we'll draw our own
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
			else base.WndProc(ref m);
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			TabPage page = e.Control as TabPage;
			if (page != null)
			{
				page.BackColor = SkinManager.Instance.CurrentSkin.Background.Normal;
			}
			base.OnControlAdded(e);
		}

		public void OnUpdateSkin(Skin skin)
		{
			
		}
	}
}

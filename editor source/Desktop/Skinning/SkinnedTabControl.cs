using System;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedTabControl : TabControl, ISkinControl
	{
		public new event EventHandler TextChanged;

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
				page.TextChanged += Page_TextChanged;
				page.BackColor = SkinManager.Instance.CurrentSkin.Background.Normal;
			}
			base.OnControlAdded(e);
		}

		private void Page_TextChanged(object sender, EventArgs e)
		{
			TextChanged?.Invoke(this, e);
		}

		public void OnUpdateSkin(Skin skin)
		{
			
		}
	}
}

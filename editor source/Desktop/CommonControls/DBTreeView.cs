using Desktop.Skinning;
using System;
using System.Windows.Forms;

namespace Desktop.CommonControls
{
	//Double-buffered TreeView. The DoubleBuffered property doesn't do anything, so we have to take this lower-level route
	public class DBTreeView : TreeView, ISkinControl
	{
		protected override void OnHandleCreated(EventArgs e)
		{
			NativeMethods.SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
			base.OnHandleCreated(e);
		}

		public void OnUpdateSkin(Skin skin)
		{
			BackColor = skin.FieldBackColor;
			ForeColor = skin.Surface.ForeColor;
		}

		// Pinvoke:
		private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
		private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
		private const int TVS_EX_DOUBLEBUFFER = 0x0004;
	}
}

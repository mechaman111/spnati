using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class NativeMethods
{
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

	private const int WM_SETREDRAW = 11;

	public static void Scroll(this Control control)
	{
		System.Drawing.Point pt = control.PointToClient(Cursor.Position);

		if ((pt.Y + 20) > control.Height)
		{
			// scroll down
			SendMessage(control.Handle, 277, (IntPtr)1, (IntPtr)0);
		}
		else if (pt.Y < 20)
		{
			// scroll up
			SendMessage(control.Handle, 277, (IntPtr)0, (IntPtr)0);
		}
	}

	public static void SuspendDrawing(this Control control)
	{
		SendMessage(control.Handle, WM_SETREDRAW, (IntPtr)0, (IntPtr)0);
	}

	public static void ResumeDrawing(this Control control)
	{
		SendMessage(control.Handle, WM_SETREDRAW, (IntPtr)1, (IntPtr)0);
		control.Refresh();
	}
}
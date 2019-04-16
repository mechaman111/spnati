using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class NativeMethods
{
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

	private const int WM_SETREDRAW = 11;
	private const int WM_KEYDOWN = 0x0100;
	private const int WM_CUT = 0x0300;
	private const int WM_COPY = 0x0301;
	private const int WM_PASTE = 0x0302;
	private const int WM_CLEAR = 0x0303;

	private const int VK_DELETE = 0x2E;

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

	/// <summary>
	/// Gets whether a control contains the active control
	/// </summary>
	/// <param name="control"></param>
	/// <param name="child"></param>
	/// <returns></returns>
	public static bool ContainsActiveControl(this Control control)
	{
		Control active = GetActiveControl(control);
		while (active != null)
		{
			if (active == control)
			{
				return true;
			}
			active = active.Parent;
		}
		return false;
	}

	public static Control GetActiveControl(Control control)
	{
		Form form = control.FindForm();
		if (form != null)
		{
			Control active = form.ActiveControl;
			ContainerControl container = active as ContainerControl;
			while (container != null)
			{
				active = container.ActiveControl;
				container = active as ContainerControl;
			}
			return active;
		}
		return null;
	}

	public static void HandleCut(this UserControl control)
	{
		Control active = GetActiveControl(control);
		SendMessage(active.Handle, WM_CUT, (IntPtr)0, (IntPtr)0);
	}

	public static void HandleCopy(this UserControl control)
	{
		Control active = GetActiveControl(control);
		SendMessage(active.Handle, WM_COPY, (IntPtr)0, (IntPtr)0);
	}

	public static void HandlePaste(this UserControl control)
	{
		Control active = GetActiveControl(control);
		SendMessage(active.Handle, WM_PASTE, (IntPtr)0, (IntPtr)0);
	}

	public static void HandleDelete(this UserControl control)
	{
		Control active = GetActiveControl(control);
		SendMessage(active.Handle, WM_KEYDOWN, (IntPtr)VK_DELETE, (IntPtr)0);
		//SendMessage(active.Handle, WM_CLEAR, (IntPtr)0, (IntPtr)0);
	}
}
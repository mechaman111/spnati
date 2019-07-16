using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public class WorkflowTracker : IMessageFilter
	{
		private const int TrackedScreens = 10;

		private LinkedList<Bitmap> _screens = new LinkedList<Bitmap>();

		public const int WM_LBUTTONDOWN = 0x0201;
		public enum enmScreenCaptureMode
		{
			Screen,
			Window
		}

		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

		[StructLayout(LayoutKind.Sequential)]
		private struct Rect
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		public Bitmap Capture(enmScreenCaptureMode screenCaptureMode = enmScreenCaptureMode.Window)
		{
			Rectangle bounds;

			if (screenCaptureMode == enmScreenCaptureMode.Screen)
			{
				bounds = System.Windows.Forms.Screen.GetBounds(Point.Empty);
				CursorPosition = Cursor.Position;
			}
			else
			{
				var foregroundWindowsHandle = GetForegroundWindow();
				var rect = new Rect();
				GetWindowRect(foregroundWindowsHandle, ref rect);
				bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
				CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);
			}

			var result = new Bitmap(bounds.Width, bounds.Height);

			using (var g = Graphics.FromImage(result))
			{
				g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
				g.FillEllipse(Brushes.Red, Cursor.Position.X - 2 - bounds.Left, Cursor.Position.Y - 2 - bounds.Top, 5, 5);
			}

			return result;
		}

		public Point CursorPosition
		{
			get;
			protected set;
		}

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == WM_LBUTTONDOWN)
			{
				if (!Config.DisableWorkflowTracer)
				{
					_screens.AddLast(Capture(enmScreenCaptureMode.Window));
					if (_screens.Count > TrackedScreens)
					{
						_screens.RemoveFirst();
					}
				}
			}
			return false;
		}

		public IEnumerable<Bitmap> GetScreens()
		{
			return _screens;
		}
	}
}

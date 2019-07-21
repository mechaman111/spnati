// This file is a modified form of what was originally MaterialForm from the MaterialSkin project, licensed below.

//The MIT License(MIT)

//Copyright(c) 2014 Ignace Maes

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


using Desktop.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static NativeMethods;

namespace Desktop.Skinning
{
	public class SkinnedForm : Form, ISkinnedPanel
	{
		public bool Sizable { get; set; }
		public bool ShowTitleBar { get; set; } = true;

		private Mailbox _mailbox;
		private int _threadId;

		#region Custom menu bar

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;
		public const int WM_MOUSEMOVE = 0x0200;
		public const int WM_LBUTTONDOWN = 0x0201;
		public const int WM_LBUTTONUP = 0x0202;
		public const int WM_LBUTTONDBLCLK = 0x0203;
		public const int WM_RBUTTONDOWN = 0x0204;
		private const int HTBOTTOMLEFT = 16;
		private const int HTBOTTOMRIGHT = 17;
		private const int HTLEFT = 10;
		private const int HTRIGHT = 11;
		private const int HTBOTTOM = 15;
		private const int HTTOP = 12;
		private const int HTTOPLEFT = 13;
		private const int HTTOPRIGHT = 14;
		private const int BORDER_WIDTH = 7;
		private ResizeDirection _resizeDir;
		private ButtonState _buttonState = ButtonState.None;

		private const int WMSZ_TOP = 3;
		private const int WMSZ_TOPLEFT = 4;
		private const int WMSZ_TOPRIGHT = 5;
		private const int WMSZ_LEFT = 1;
		private const int WMSZ_RIGHT = 2;
		private const int WMSZ_BOTTOM = 6;
		private const int WMSZ_BOTTOMLEFT = 7;
		private const int WMSZ_BOTTOMRIGHT = 8;

		private readonly Dictionary<int, int> _resizingLocationsToCmd = new Dictionary<int, int>
		{
			{HTTOP,         WMSZ_TOP},
			{HTTOPLEFT,     WMSZ_TOPLEFT},
			{HTTOPRIGHT,    WMSZ_TOPRIGHT},
			{HTLEFT,        WMSZ_LEFT},
			{HTRIGHT,       WMSZ_RIGHT},
			{HTBOTTOM,      WMSZ_BOTTOM},
			{HTBOTTOMLEFT,  WMSZ_BOTTOMLEFT},
			{HTBOTTOMRIGHT, WMSZ_BOTTOMRIGHT}
		};

		private const int STATUS_BAR_BUTTON_WIDTH = StatusBarHeight;
		private const int StatusBarHeight = 27;
		private const int IconSize = 16;
		private const int ACTION_BAR_HEIGHT = 40;

		private const uint TPM_LEFTALIGN = 0x0000;
		private const uint TPM_RETURNCMD = 0x0100;

		private const int WM_SYSCOMMAND = 0x0112;
		private const int WS_MINIMIZEBOX = 0x20000;
		private const int WS_SYSMENU = 0x00080000;

		private const int MONITOR_DEFAULTTONEAREST = 2;

		private enum ResizeDirection
		{
			BottomLeft,
			Left,
			Right,
			BottomRight,
			Bottom,
			Top,
			TopLeft,
			TopRight,
			None
		}

		private enum ButtonState
		{
			XOver,
			MaxOver,
			MinOver,
			XDown,
			MaxDown,
			MinDown,
			None
		}

		private readonly Cursor[] _resizeCursors = { Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNS };

		private Rectangle _minButtonBounds;
		private Rectangle _maxButtonBounds;
		private Rectangle _xButtonBounds;
		private Rectangle _actionBarBounds;
		private Rectangle _statusBarBounds;

		private bool _maximized;
		private Size _previousSize;
		private Point _previousLocation;
		private bool _headerMouseDown;

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (DesignMode || IsDisposed) return;

			if (m.Msg == WM_LBUTTONDBLCLK)
			{
				MaximizeWindow(!_maximized);
			}
			else if (m.Msg == WM_MOUSEMOVE && _maximized &&
				(_statusBarBounds.Contains(PointToClient(Cursor.Position)) || _actionBarBounds.Contains(PointToClient(Cursor.Position))) &&
				!(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
			{
				if (_headerMouseDown)
				{
					_maximized = false;
					_headerMouseDown = false;

					var mousePoint = PointToClient(Cursor.Position);
					if (mousePoint.X < Width / 2)
						Location = mousePoint.X < _previousSize.Width / 2 ?
							new Point(Cursor.Position.X - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
							new Point(Cursor.Position.X - _previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);
					else
						Location = Width - mousePoint.X < _previousSize.Width / 2 ?
							new Point(Cursor.Position.X - _previousSize.Width + Width - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
							new Point(Cursor.Position.X - _previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);

					Size = _previousSize;
					ReleaseCapture();
					SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
				}
			}
			else if (m.Msg == WM_LBUTTONDOWN &&
				(_statusBarBounds.Contains(PointToClient(Cursor.Position)) || _actionBarBounds.Contains(PointToClient(Cursor.Position))) &&
				!(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
			{
				if (!_maximized)
				{
					ReleaseCapture();
					SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
				}
				else
				{
					_headerMouseDown = true;
				}
			}
			else if (m.Msg == WM_RBUTTONDOWN)
			{
				Point cursorPos = PointToClient(Cursor.Position);

				if (_statusBarBounds.Contains(cursorPos) && !_minButtonBounds.Contains(cursorPos) &&
					!_maxButtonBounds.Contains(cursorPos) && !_xButtonBounds.Contains(cursorPos))
				{
					// Show default system menu when right clicking titlebar
					var id = TrackPopupMenuEx(GetSystemMenu(Handle, false), TPM_LEFTALIGN | TPM_RETURNCMD, Cursor.Position.X, Cursor.Position.Y, Handle, IntPtr.Zero);

					// Pass the command as a WM_SYSCOMMAND message
					SendMessage(Handle, WM_SYSCOMMAND, (IntPtr)id, (IntPtr)0);
				}
			}
			else if (m.Msg == WM_NCLBUTTONDOWN)
			{
				// This re-enables resizing by letting the application know when the
				// user is trying to resize a side. This is disabled by default when using WS_SYSMENU.
				if (!Sizable) return;

				byte bFlag = 0;

				// Get which side to resize from
				if (_resizingLocationsToCmd.ContainsKey((int)m.WParam))
				{
					bFlag = (byte)_resizingLocationsToCmd[(int)m.WParam];
				}

				if (bFlag != 0)
				{
					SendMessage(Handle, WM_SYSCOMMAND, (IntPtr)(0xF000 | bFlag), m.LParam);
				}
			}
			else if (m.Msg == WM_LBUTTONUP)
			{
				_headerMouseDown = false;
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var par = base.CreateParams;
				// WS_SYSMENU: Trigger the creation of the system menu
				// WS_MINIMIZEBOX: Allow minimizing from taskbar
				par.Style = par.Style | WS_MINIMIZEBOX | WS_SYSMENU; // Turn on the WS_MINIMIZEBOX style flag
				return par;
			}
		}

		public SkinnedBackgroundType PanelType
		{
			get { return SkinnedBackgroundType.Background; }
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (DesignMode) return;
			UpdateButtons(e);

			if (e.Button == MouseButtons.Left && !_maximized && !Modal) //Modal check is a workaround until we figure out what's causing modals to resize when they should be dragged
				ResizeForm(_resizeDir);
			base.OnMouseDown(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (DesignMode) return;
			_buttonState = ButtonState.None;
			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (DesignMode) return;

			if (Sizable && ControlBox)
			{
				//True if the mouse is hovering over a child control
				var isChildUnderMouse = GetChildAtPoint(e.Location) != null;
				//isChildUnderMouse = false;

				if (e.Location.X < BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
				{
					_resizeDir = ResizeDirection.BottomLeft;
					Cursor = Cursors.SizeNESW;
				}
				else if (e.Location.X < BORDER_WIDTH && e.Location.Y < BORDER_WIDTH && !isChildUnderMouse && !_maximized)
				{
					_resizeDir = ResizeDirection.TopLeft;
					Cursor = Cursors.SizeNWSE;
				}
				else if (e.Location.X > Width - BORDER_WIDTH && e.Location.Y < BORDER_WIDTH && !isChildUnderMouse && !_maximized && !_xButtonBounds.Contains(e.Location))
				{
					_resizeDir = ResizeDirection.TopRight;
					Cursor = Cursors.SizeNESW;
				}
				else if (e.Location.X < BORDER_WIDTH && !isChildUnderMouse && !_maximized)
				{
					_resizeDir = ResizeDirection.Left;
					Cursor = Cursors.SizeWE;
				}
				else if (e.Location.X > Width - BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
				{
					_resizeDir = ResizeDirection.BottomRight;
					Cursor = Cursors.SizeNWSE;
				}
				else if (e.Location.X > Width - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
				{
					_resizeDir = ResizeDirection.Right;
					Cursor = Cursors.SizeWE;
				}
				else if (e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
				{
					_resizeDir = ResizeDirection.Bottom;
					Cursor = Cursors.SizeNS;
				}
				else if (e.Location.Y < BORDER_WIDTH && e.Location.X < _minButtonBounds.X && !isChildUnderMouse && !_maximized)
				{
					_resizeDir = ResizeDirection.Top;
					Cursor = Cursors.SizeNS;
				}
				else
				{
					_resizeDir = ResizeDirection.None;

					//Only reset the cursor when needed, this prevents it from flickering when a child control changes the cursor to its own needs
					if (_resizeCursors.Contains(Cursor))
					{
						Cursor = Cursors.Default;
					}
				}
			}

			UpdateButtons(e);
		}

		protected void OnGlobalMouseMove(object sender, MouseEventArgs e)
		{
			if (IsDisposed || System.Threading.Thread.CurrentThread.ManagedThreadId != _threadId) return;
			// Convert to client position and pass to Form.MouseMove
			var clientCursorPos = PointToClient(e.Location);
			var newE = new MouseEventArgs(MouseButtons.None, 0, clientCursorPos.X, clientCursorPos.Y, 0);
			OnMouseMove(newE);
		}

		private void UpdateButtons(MouseEventArgs e, bool up = false)
		{
			if (DesignMode) return;
			var oldState = _buttonState;
			bool showMin = MinimizeBox && ControlBox;
			bool showMax = MaximizeBox && ControlBox;

			if (e.Button == MouseButtons.Left && !up)
			{
				if (showMin && !showMax && _maxButtonBounds.Contains(e.Location))
					_buttonState = ButtonState.MinDown;
				else if (showMin && showMax && _minButtonBounds.Contains(e.Location))
					_buttonState = ButtonState.MinDown;
				else if (showMax && _maxButtonBounds.Contains(e.Location))
					_buttonState = ButtonState.MaxDown;
				else if (ControlBox && _xButtonBounds.Contains(e.Location))
					_buttonState = ButtonState.XDown;
				else
					_buttonState = ButtonState.None;
			}
			else
			{
				if (showMin && !showMax && _maxButtonBounds.Contains(e.Location))
				{
					_buttonState = ButtonState.MinOver;

					if (oldState == ButtonState.MinDown && up)
						WindowState = FormWindowState.Minimized;
				}
				else if (showMin && showMax && _minButtonBounds.Contains(e.Location))
				{
					_buttonState = ButtonState.MinOver;

					if (oldState == ButtonState.MinDown && up)
						WindowState = FormWindowState.Minimized;
				}
				else if (MaximizeBox && ControlBox && _maxButtonBounds.Contains(e.Location))
				{
					_buttonState = ButtonState.MaxOver;

					if (oldState == ButtonState.MaxDown && up)
						MaximizeWindow(!_maximized);

				}
				else if (ControlBox && _xButtonBounds.Contains(e.Location))
				{
					_buttonState = ButtonState.XOver;

					if (oldState == ButtonState.XDown && up)
						Close();
				}
				else _buttonState = ButtonState.None;
			}

			if (oldState != _buttonState) Invalidate();
		}

		private void MaximizeWindow(bool maximize)
		{
			if (!MaximizeBox || !ControlBox) return;

			_maximized = maximize;

			if (maximize)
			{
				IntPtr monitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
				MONITORINFOEX monitorInfo = new MONITORINFOEX();
				GetMonitorInfo(new HandleRef(null, monitorHandle), monitorInfo);
				_previousSize = Size;
				_previousLocation = Location;
				Size = new Size(monitorInfo.rcWork.Width(), monitorInfo.rcWork.Height());
				Location = new Point(monitorInfo.rcWork.left, monitorInfo.rcWork.top);
			}
			else
			{
				Size = _previousSize;
				Location = _previousLocation;
			}

		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (DesignMode) return;
			UpdateButtons(e, true);

			base.OnMouseUp(e);
			ReleaseCapture();
		}

		private void ResizeForm(ResizeDirection direction)
		{
			if (DesignMode) return;
			var dir = -1;
			switch (direction)
			{
				case ResizeDirection.BottomLeft:
					dir = HTBOTTOMLEFT;
					break;
				case ResizeDirection.Left:
					dir = HTLEFT;
					break;
				case ResizeDirection.Right:
					dir = HTRIGHT;
					break;
				case ResizeDirection.BottomRight:
					dir = HTBOTTOMRIGHT;
					break;
				case ResizeDirection.Bottom:
					dir = HTBOTTOM;
					break;
				case ResizeDirection.Top:
					dir = HTTOP;
					break;
				case ResizeDirection.TopLeft:
					dir = HTTOPLEFT;
					break;
				case ResizeDirection.TopRight:
					dir = HTTOPRIGHT;
					break;
			}

			ReleaseCapture();
			if (dir != -1)
			{
				SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)dir, (IntPtr)0);
			}
		}

		private const int FormPadding = 14;

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			_minButtonBounds = new Rectangle((Width - FormPadding / 2) - 3 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, StatusBarHeight);
			_maxButtonBounds = new Rectangle((Width - FormPadding / 2) - 2 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, StatusBarHeight);
			_xButtonBounds = new Rectangle((Width - FormPadding / 2) - STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, StatusBarHeight);
			_statusBarBounds = new Rectangle(1, 1, Width - 2, StatusBarHeight - 1);
			_actionBarBounds = new Rectangle(0, StatusBarHeight, Width, ACTION_BAR_HEIGHT);
			Invalidate(true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;

			Skin skin = SkinManager.Instance.CurrentSkin;

			g.Clear(skin.Background.Normal);
			g.FillRectangle(skin.PrimaryDarkColor.GetBrush(VisualState.Normal), _statusBarBounds);

			//Draw border
			Pen borderPen = skin.PrimaryLightColor.GetPen(VisualState.Normal, false, Enabled);
			g.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);

			// Determine whether or not we even should be drawing the buttons.
			bool showMin = MinimizeBox && ControlBox;
			bool showMax = MaximizeBox && ControlBox;
			SolidBrush hoverBrush = skin.PrimaryDarkColor.GetBrush(VisualState.Hover);
			SolidBrush downBrush = skin.PrimaryDarkColor.GetBrush(VisualState.Pressed);

			// When MaximizeButton == false, the minimize button will be painted in its place
			if (_buttonState == ButtonState.MinOver && showMin)
			{
				g.FillRectangle(hoverBrush, showMax ? _minButtonBounds : _maxButtonBounds);
			}

			if (_buttonState == ButtonState.MinDown && showMin)
			{
				g.FillRectangle(downBrush, showMax ? _minButtonBounds : _maxButtonBounds);
			}

			if (_buttonState == ButtonState.MaxOver && showMax)
			{
				g.FillRectangle(hoverBrush, _maxButtonBounds);
			}

			if (_buttonState == ButtonState.MaxDown && showMax)
			{
				g.FillRectangle(downBrush, _maxButtonBounds);
			}

			if (ControlBox && (_buttonState == ButtonState.XOver || _buttonState == ButtonState.XDown))
			{
				using (SolidBrush br = new SolidBrush(skin.ErrorBackColor))
				{
					if (_buttonState == ButtonState.XOver)
					{
						g.FillRectangle(br, _xButtonBounds);
					}
					else if (_buttonState == ButtonState.XDown)
					{
						g.FillRectangle(br, _xButtonBounds);
					}
				}
			}

			using (var formButtonsPen = new Pen(skin.PrimaryDarkColor.ForeColor, 2))
			{
				// Minimize button.
				if (showMin)
				{
					int x = showMax ? _minButtonBounds.X : _maxButtonBounds.X;
					int y = showMax ? _minButtonBounds.Y : _maxButtonBounds.Y;

					g.DrawLine(
						formButtonsPen,
						x + (int)(_minButtonBounds.Width * 0.33),
						y + (int)(_minButtonBounds.Height * 0.66),
						x + (int)(_minButtonBounds.Width * 0.66),
						y + (int)(_minButtonBounds.Height * 0.66)
				   );
				}

				// Maximize button
				if (showMax)
				{
					if (_maximized)
					{
						g.DrawRectangle(
							formButtonsPen,
							_maxButtonBounds.X + (int)(_maxButtonBounds.Width * 0.33) - 2,
							_maxButtonBounds.Y + (int)(_maxButtonBounds.Height * 0.36) + 2,
							(int)(_maxButtonBounds.Width * 0.39),
							(int)(_maxButtonBounds.Height * 0.31)
					   );
						g.DrawRectangle(
							formButtonsPen,
							_maxButtonBounds.X + (int)(_maxButtonBounds.Width * 0.33) + 2,
							_maxButtonBounds.Y + (int)(_maxButtonBounds.Height * 0.36) - 2,
							(int)(_maxButtonBounds.Width * 0.39),
							(int)(_maxButtonBounds.Height * 0.31)
					   );
					}
					else
					{
						g.DrawRectangle(
							formButtonsPen,
							_maxButtonBounds.X + (int)(_maxButtonBounds.Width * 0.33),
							_maxButtonBounds.Y + (int)(_maxButtonBounds.Height * 0.36),
							(int)(_maxButtonBounds.Width * 0.39),
							(int)(_maxButtonBounds.Height * 0.31)
					   );
					}
				}

				// Close button
				if (ControlBox)
				{
					g.DrawLine(
						formButtonsPen,
						_xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
						_xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
						_xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
						_xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66)
				   );

					g.DrawLine(
						formButtonsPen,
						_xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
						_xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
						_xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
						_xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66));
				}
			}

			//Form title
			bool showIcon = ShowIcon && Icon != null && ControlBox;
			if (showIcon)
			{
				g.DrawIcon(Icon, new Rectangle(6, StatusBarHeight / 2 - IconSize / 2, IconSize, IconSize));
			}
			if (ShowTitleBar)
			{
				using (SolidBrush foreBrush = new SolidBrush(skin.PrimaryDarkColor.ForeColor))
				{
					g.DrawString(Text, Skin.HeaderFont, foreBrush, new Rectangle(FormPadding + (showIcon ? IconSize : 0), 0, Width, StatusBarHeight), new StringFormat { LineAlignment = StringAlignment.Center });
				}
			}
		}
		#endregion

		public SkinnedForm()
		{
			_threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
			FormBorderStyle = FormBorderStyle.None;
			Sizable = true;
			DoubleBuffered = true;
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

			// This enables the form to trigger the MouseMove event even when mouse is over another control
			Application.AddMessageFilter(new MouseMessageFilter());
			MouseMessageFilter.MouseMove += OnGlobalMouseMove;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (DesignMode) { return; }
			_mailbox = Shell.Instance.PostOffice.GetMailbox();
			_mailbox.Subscribe<Skin>(CoreDesktopMessages.SkinChanged, OnSkinChanged);

			OnSkinChanged(SkinManager.Instance.CurrentSkin);
		}

		private void OnSkinChanged(Skin skin)
		{
			Invalidate(true);
			BackColor = skin.Background.Normal;
			ForeColor = skin.Surface.ForeColor;

			foreach (Control ctl in Controls)
			{
				SkinManager.Instance.ReskinControl(ctl, skin);
			}
			OnUpdateSkin(skin);
		}

		protected virtual void OnUpdateSkin(Skin skin)
		{
		}
	}

	public class MouseMessageFilter : IMessageFilter
	{
		private const int WM_MOUSEMOVE = 0x0200;

		public static event MouseEventHandler MouseMove;

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == WM_MOUSEMOVE)
			{
				if (MouseMove != null)
				{
					int x = Control.MousePosition.X, y = Control.MousePosition.Y;

					MouseMove(null, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
				}
			}
			return false;
		}
	}
}

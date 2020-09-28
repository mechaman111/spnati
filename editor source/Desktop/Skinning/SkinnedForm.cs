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
using System.Drawing;
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

		#region Custom menu bar

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;
		public const int WM_MOUSEMOVE = 0x0200;
		public const int WM_LBUTTONDOWN = 0x0201;
		public const int WM_LBUTTONUP = 0x0202;
		public const int WM_LBUTTONDBLCLK = 0x0203;
		public const int WM_RBUTTONDOWN = 0x0204;
		private const int WM_NCHITTEST = 0x84;
		private const int HTBOTTOMLEFT = 16;
		private const int HTBOTTOMRIGHT = 17;
		private const int HTLEFT = 10;
		private const int HTRIGHT = 11;
		private const int HTBOTTOM = 15;
		private const int HTTOP = 12;
		private const int HTTOPLEFT = 13;
		private const int HTTOPRIGHT = 14;
		private ButtonState _buttonState = ButtonState.None;

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
		
		private Rectangle _minButtonBounds;
		private Rectangle _maxButtonBounds;
		private Rectangle _xButtonBounds;
		private Rectangle _actionBarBounds;
		private Rectangle _statusBarBounds;

		private const int ResizeBuffer = 5;
		private Rectangle TopEdge { get { return new Rectangle(0, 0, ClientSize.Width, ResizeBuffer); } }
		private Rectangle LeftEdge { get { return new Rectangle(0, 0, ResizeBuffer, ClientSize.Height); } }
		private Rectangle BottomEdge { get { return new Rectangle(0, ClientSize.Height - ResizeBuffer, ClientSize.Width, ResizeBuffer); } }
		private Rectangle RightEdge { get { return new Rectangle(ClientSize.Width - ResizeBuffer, 0, ResizeBuffer, ClientSize.Height); } }

		private Rectangle TopLeft { get { return new Rectangle(0, 0, ResizeBuffer, ResizeBuffer); } }
		private Rectangle TopRight { get { return new Rectangle(ClientSize.Width - ResizeBuffer, 0, ResizeBuffer, ResizeBuffer); } }
		private Rectangle BottomLeft { get { return new Rectangle(0, ClientSize.Height - ResizeBuffer, ResizeBuffer, ResizeBuffer); } }
		private Rectangle BottomRight { get { return new Rectangle(ClientSize.Width - ResizeBuffer, ClientSize.Height - ResizeBuffer, ResizeBuffer, ResizeBuffer); } }

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
				//maximize toggle when double clicking header
				MaximizeWindow(!_maximized);
			}
			else if (m.Msg == WM_MOUSEMOVE && _maximized &&
				(_statusBarBounds.Contains(PointToClient(Cursor.Position)) || _actionBarBounds.Contains(PointToClient(Cursor.Position))) &&
				!(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
			{
				//dragging header when window is maximized - restore window and start dragging
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
				//Dragging title bar to move window
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
				//System menu when right-clicking header
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
			else if (m.Msg == WM_LBUTTONUP)
			{
				_headerMouseDown = false;
			}
			else if (m.Msg == WM_NCHITTEST && Sizable)
			{
				Point cursor = PointToClient(Cursor.Position);
				if (TopLeft.Contains(cursor))
				{
					m.Result = (IntPtr)HTTOPLEFT;
				}
				else if (TopRight.Contains(cursor))
				{
					m.Result = (IntPtr)HTTOPRIGHT;
				}
				else if (BottomLeft.Contains(cursor))
				{
					m.Result = (IntPtr)HTBOTTOMLEFT;
				}
				else if (BottomRight.Contains(cursor))
				{
					m.Result = (IntPtr)HTBOTTOMRIGHT;
				}
				else if (TopEdge.Contains(cursor))
				{
					m.Result = (IntPtr)HTTOP;
				}
				else if (LeftEdge.Contains(cursor))
				{
					m.Result = (IntPtr)HTLEFT;
				}
				else if (RightEdge.Contains(cursor))
				{
					m.Result = (IntPtr)HTRIGHT;
				}
				else if (BottomEdge.Contains(cursor))
				{
					m.Result = (IntPtr)HTBOTTOM;
				}
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var par = base.CreateParams;
				// WS_SYSMENU: Trigger the creation of the system menu
				// WS_MINIMIZEBOX: Allow minimizing from taskbar
				par.Style = par.Style | WS_MINIMIZEBOX; // Turn on the WS_MINIMIZEBOX style flag
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

			UpdateButtons(e);
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
			FormBorderStyle = FormBorderStyle.None;
			Sizable = true;
			DoubleBuffered = true;
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
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
}

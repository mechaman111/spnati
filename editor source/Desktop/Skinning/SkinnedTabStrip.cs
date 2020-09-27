using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Desktop.Skinning
{
	public class SkinnedTabStrip : Control, ISkinControl, ISkinnedPanel
	{
		private const int IndicatorSize = 5;
		private const int VerticalBarWidth = 5;
		private const int CloseButtonWidth = 20;
		private const int CloseMarkWidth = 10;
		private const int SpacerSize = 15;
		private const int ScrollBarSize = 20;
		private const int ScrollTick = 20;
		private const int ScrollDelay = 500;
		private const int ScrollInterval = 100;

		private static StringFormat CenterFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
		private static StringFormat VerticalFormat = new StringFormat() { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };

		public event EventHandler CloseButtonClicked;
		public event EventHandler AddButtonClicked;

		private SolidBrush _activeBrush = new SolidBrush(Color.Black);
		private SolidBrush _textBrush = new SolidBrush(Color.Black);
		private SolidBrush _decorBrush = new SolidBrush(Color.Black);
		private SolidBrush _highlightBrush = new SolidBrush(Color.Black);

		private ScrollBar _upArrow = new ScrollBar();
		private ScrollBar _downArrow = new ScrollBar();
		private int _scrollPosition = 0;
		private int _maxScroll = 0;
		private Timer _scrollTimer;
		private int _scrollDirection;

		private Dictionary<TabPage, DataHighlight> _highlights = new Dictionary<TabPage, DataHighlight>();

		private int _startMargin = 5;
		public int StartMargin
		{
			get { return _startMargin; }
			set
			{
				_startMargin = value;
				_tabRects = null;
				Invalidate(true);
			}
		}

		private string _decorationText;
		public string DecorationText
		{
			get { return _decorationText; }
			set
			{
				_decorationText = value;
				Invalidate(true);
			}
		}

		private SkinnedBackgroundType _type = SkinnedBackgroundType.PrimaryLight;
		public SkinnedBackgroundType PanelType
		{
			get { return _type; }
			set
			{
				_type = value;
				Invalidate(true);
			}
		}

		private SkinnedBackgroundType _tabType = SkinnedBackgroundType.Background;
		public SkinnedBackgroundType TabType
		{
			get { return _tabType; }
			set
			{
				_tabType = value;
				Invalidate(true);
			}
		}

		private bool _showClose;
		public bool ShowCloseButton
		{
			get { return _showClose; }
			set { _showClose = value; _tabRects = null; Invalidate(); }
		}

		private bool _showAdd;
		public bool ShowAddButton
		{
			get { return _showAdd; }
			set { _showAdd = value; _tabRects = null; Invalidate(); }
		}

		private string _addCaption;
		public string AddCaption
		{
			get { return _addCaption; }
			set { _addCaption = value; _tabRects = null; Invalidate(); }
		}

		private bool _vertical;
		public bool Vertical
		{
			get { return _vertical; }
			set { _vertical = value; Invalidate(); }
		}

		private int _tabSize = 100;
		public int TabSize
		{
			get { return Vertical ? 25 : _tabSize; }
			set { _tabSize = value; _tabRects = null; Invalidate(); }
		}

		private int _tabPadding = 20;
		public int TabPadding
		{
			get { return _tabPadding; }
			set { _tabPadding = value; _tabRects = null; Invalidate(); }
		}

		private int _tabMargin = 5;
		public int TabMargin
		{
			get { return _tabMargin; }
			set { _tabMargin = value; _tabRects = null; Invalidate(); }
		}

		private SkinnedTabControl _tabControl;
		public SkinnedTabControl TabControl
		{
			get { return _tabControl; }
			set
			{
				_tabControl = value;
				if (_tabControl == null)
				{
					return;
				}

				_highlights.Clear();

				_tabControl.SelectedIndexChanged += (sender, args) =>
				{
					Invalidate();
				};
				_tabControl.ControlAdded += delegate
				{
					_tabRects = null;
					OnUpdateSkin(SkinManager.Instance.CurrentSkin);
					Invalidate();
				};
				_tabControl.ControlRemoved += (object sender, ControlEventArgs args) =>
				{
					TabPage page = args.Control as TabPage;
					if (page != null)
					{
						_highlights.Remove(page);
					}
					_tabRects = null;
					OnUpdateSkin(SkinManager.Instance.CurrentSkin);
					Invalidate();
				};
				_tabControl.VisibleChanged += delegate
				{
					OnUpdateSkin(SkinManager.Instance.CurrentSkin);
				};
				_tabControl.TextChanged += delegate
				{
					Invalidate();
				};
			}
		}

		private int _hoveredTabIndex = -1;
		private List<Rectangle> _tabRects = null;

		public SkinnedTabStrip()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
			Margin = new Padding(0);
			_scrollTimer = new Timer();
			_scrollTimer.Interval = ScrollDelay;
			_scrollTimer.Tick += _scrollTimer_Tick;
		}

		public void OnUpdateSkin(Skin skin)
		{
			if (_tabControl != null)
			{
				ColorSet set = skin.GetColorSet(TabType);
				foreach (TabPage page in _tabControl.TabPages)
				{
					page.BackColor = set.GetColor(VisualState.Normal, false, Enabled);
					page.ForeColor = set.ForeColor;
				}
			}
		}

		protected override void OnResize(EventArgs e)
		{
			_tabRects = null;
			Invalidate();
		}

		public bool IsUpArrowVisible
		{
			get { return _scrollPosition > 0; }
		}

		public bool IsDownArrowVisible
		{
			get { return _scrollPosition < _maxScroll; }
		}

		private void SetMaxScroll(int amount)
		{
			_maxScroll = amount;
			_scrollPosition = Math.Min(_maxScroll, _scrollPosition);
		}

		private void CalculateTabRects()
		{
			_tabRects = new List<Rectangle>();
			_maxScroll = 0;

			if (_tabControl == null || _tabControl.TabCount == 0)
			{
				return;
			}

			if (Vertical)
			{
				int y = StartMargin;
				for (int i = 0; i < _tabControl.TabPages.Count; i++)
				{
					Rectangle rect = new Rectangle(0, y, ClientRectangle.Width, TabSize);
					_tabRects.Add(rect);
					if (_tabControl.TabPages[i]?.Tag?.ToString() == "spacer")
					{
						y += SpacerSize;
					}
					else
					{
						y += TabSize + TabMargin;
					}
				}
				if (_showAdd)
				{
					Rectangle rect = new Rectangle(0, y, ClientRectangle.Width, TabSize);
					y += TabSize + TabMargin;
					_tabRects.Add(rect);
				}

				if (y - TabMargin > ClientRectangle.Height)
				{
					SetMaxScroll(y - ClientRectangle.Height);
					_upArrow.Rect = new Rectangle(0, 0, ClientRectangle.Width, ScrollBarSize);
					_downArrow.Rect = new Rectangle(0, ClientRectangle.Height - ScrollBarSize, ClientRectangle.Width, ScrollBarSize);
				}
				else
				{
					_scrollPosition = 0;
				}
			}
			else
			{
				using (Graphics g = CreateGraphics())
				{
					int x = StartMargin;
					for (int i = 0; i < _tabControl.TabPages.Count; i++)
					{
						int width = TabSize;
						if (width == -1)
						{
							width = (int)g.MeasureString(_tabControl.TabPages[i].Text, Skin.TabFont).Width + _tabPadding * 2;
						}

						if (ShowCloseButton || (ShowAddButton && i > 0))
						{
							width += CloseButtonWidth;
						}
						Rectangle rect = new Rectangle(x, 0, width, ClientRectangle.Height - 1);
						_tabRects.Add(rect);
						if (_tabControl.TabPages[i]?.Tag?.ToString() == "spacer")
						{
							x += SpacerSize;
						}
						else
						{
							x += width + TabMargin;
						}
					}
					if (_showAdd)
					{
						int width = TabSize;
						if (width == -1)
						{
							width = (int)g.MeasureString("Add", Skin.TabFont).Width + _tabPadding * 2 + Properties.Resources.Add.Width;
						}
						Rectangle rect = new Rectangle(x, 0, width, ClientRectangle.Height - 1);
						x += width + TabMargin;
						_tabRects.Add(rect);
					}

					if (x - TabMargin > ClientRectangle.Width)
					{
						SetMaxScroll(x - ClientRectangle.Width);
						_upArrow.Rect = new Rectangle(0, 0, ScrollBarSize, ClientRectangle.Height);
						_downArrow.Rect = new Rectangle(ClientRectangle.Width - ScrollBarSize, 0, ScrollBarSize, ClientRectangle.Height);
					}
					else
					{
						_scrollPosition = 0;
					}
				}
			}
		}

		private List<Rectangle> GetTabRects()
		{
			if (_tabRects == null)
			{
				CalculateTabRects();
			}
			return _tabRects;
		}

		private ColorSet GetIndicator()
		{
			if (TabType == SkinnedBackgroundType.Secondary)
			{
				return SkinManager.Instance.CurrentSkin.PrimaryColor;
			}
			else
			{
				return SkinManager.Instance.CurrentSkin.SecondaryColor;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (Vertical)
			{
				PaintVertical(e);
			}
			else
			{
				PaintHorizontal(e);
			}
		}

		private void PaintHorizontal(PaintEventArgs e)
		{
			Skin skin = SkinManager.Instance.CurrentSkin;
			Graphics g = e.Graphics;

			ColorSet set = skin.GetColorSet(PanelType);
			Color color = set.GetColor(VisualState.Normal, false, Enabled);
			g.Clear(color);

			ColorSet tabSet = skin.GetColorSet(TabType);

			if (_tabControl == null) { return; }

			SolidBrush indicatorBrush = GetIndicator().GetBrush(VisualState.Normal, false, Enabled);
			SolidBrush hoverBrush = set.GetBrush(VisualState.Hover, false, Enabled);
			Pen borderPen = set.GetBorderPen(VisualState.Normal, false, Enabled);

			List<Rectangle> rects = GetTabRects();
			g.DrawLine(borderPen, ClientRectangle.X, ClientRectangle.Bottom - 1, ClientRectangle.Right, ClientRectangle.Bottom - 1);
			_activeBrush.Color = _textBrush.Color = Enabled ? set.ForeColor : set.DisabledForeColor;
			if (!string.IsNullOrEmpty(_decorationText))
			{
				using (StringFormat decorFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far })
				{
					_decorBrush.Color = ColorSet.BlendColor(_textBrush.Color, color, 0.2f);
					Rectangle rect = new Rectangle(0, 0, ClientRectangle.Width - 5, ClientRectangle.Height);
					g.DrawString(_decorationText, Skin.DecorationFont, _decorBrush, rect, decorFormat);
				}
			}

			for (int i = 0; i < rects.Count; i++)
			{
				bool showClose = ShowCloseButton || (i > 0 && ShowAddButton && i < rects.Count - 1);
				bool showAdd = (ShowAddButton && i == rects.Count - 1);
				Rectangle rect = rects[i];
				rect.X -= _scrollPosition;
				TabPage page = i < _tabControl.TabPages.Count ? _tabControl.TabPages[i] : null;

				string text = page == null ? AddCaption : page.Text;
				int buttonWidth = showClose ? CloseButtonWidth : 0;
				Rectangle textRect = new Rectangle(rect.X, rect.Y + IndicatorSize + 1, rect.Width - buttonWidth, rect.Height - IndicatorSize - 2);
				if (showAdd)
				{
					textRect = new Rectangle(rect.X + Properties.Resources.Add.Width, rect.Y + IndicatorSize + 1, rect.Width - Properties.Resources.Add.Width, textRect.Height);
				}

				if (_hoveredTabIndex == i)
				{
					g.FillRectangle(hoverBrush, rect);
				}

				DataHighlight highlight = DataHighlight.Normal;
				if (page != null)
				{
					_highlights.TryGetValue(page, out highlight);
					if (highlight != DataHighlight.Normal)
					{
						_highlightBrush.Color = skin.GetHighlightColor(highlight);
						g.FillRectangle(_highlightBrush, new RectangleF(rect.X, 1, rect.Width, IndicatorSize));
					}
				}

				//tab and text
				if (i == _tabControl.SelectedIndex)
				{
					//selected tab

					SolidBrush tabBrush = tabSet.GetBrush(VisualState.Normal, false, Enabled);
					g.FillRectangle(tabBrush, rect.X, rect.Y, rect.Width, rect.Height + 1); //Height + 1 to cover border line
					if (highlight == DataHighlight.Normal)
					{
						g.FillRectangle(indicatorBrush, new RectangleF(rect.X, 1, rect.Width, IndicatorSize));
					}
					else
					{
						g.FillRectangle(_highlightBrush, new RectangleF(rect.X, 1, rect.Width, IndicatorSize));
					}
					g.DrawLine(borderPen, rect.X, rect.Top, rect.Right, rect.Top);
					g.DrawLine(borderPen, rect.X, rect.Y + 1, rect.X, rect.Bottom);
					g.DrawLine(borderPen, rect.Right, rect.Y + 1, rect.Right, rect.Bottom);

					using (SolidBrush foreBrush = new SolidBrush(Enabled ? tabSet.ForeColor : tabSet.DisabledForeColor))
					{
						g.DrawString(text, Skin.ActiveTabFont, foreBrush, textRect, CenterFormat);

						if (showClose && !showAdd)
						{
							//close button
							g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
							Color closeColor = skin.PrimaryForeColor;
							Rectangle closeRect = GetCloseRectangle();
							if (!RectangleToScreen(new Rectangle(closeRect.X - 3, closeRect.Y - 3, closeRect.Width + 6, closeRect.Height + 6)).Contains(MousePosition))
							{
								closeColor = Color.FromArgb(127, closeColor);
							}
							using (Pen closePen = new Pen(closeColor, 2))
							{
								g.DrawLine(closePen, closeRect.X, closeRect.Y, closeRect.Right, closeRect.Bottom);
								g.DrawLine(closePen, closeRect.X, closeRect.Bottom, closeRect.Right, closeRect.Top);
							}
							g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
						}
					}
				}
				else
				{
					if (showAdd)
					{
						//Add buttton
						g.DrawImage(Properties.Resources.Add, textRect.X - Properties.Resources.Add.Width + TabPadding, textRect.Y + 1);
					}

					g.DrawString(text, Skin.TabFont, _textBrush, textRect, CenterFormat);
				}
			}

			//Scroll arrows
			if (IsUpArrowVisible)
			{
				Rectangle rect = _upArrow.Rect;
				SolidBrush brush = skin.PrimaryDarkColor.GetBrush(_upArrow.IsMouseDown ? VisualState.Pressed : _upArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				SolidBrush arrowBrush = skin.SecondaryWidget.GetBrush(_upArrow.IsMouseDown ? VisualState.Pressed : _upArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				g.FillRectangle(brush, rect);
				g.FillPolygon(arrowBrush, new PointF[] { new PointF(rect.X + rect.Width / 2 - ScrollBarSize / 4, rect.Y + rect.Height / 2), new PointF(rect.Right - rect.Width / 2 + ScrollBarSize / 4, rect.Y + rect.Height / 3), new PointF(rect.Right - rect.Width / 2+ ScrollBarSize / 4, rect.Bottom - rect.Height / 3) });
			}
			if (IsDownArrowVisible)
			{
				SolidBrush brush = skin.PrimaryDarkColor.GetBrush(_downArrow.IsMouseDown ? VisualState.Pressed : _downArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				SolidBrush arrowBrush = skin.SecondaryWidget.GetBrush(_downArrow.IsMouseDown ? VisualState.Pressed : _downArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				Rectangle rect = _downArrow.Rect;
				g.FillRectangle(brush, rect);
				g.FillPolygon(arrowBrush, new PointF[] { new PointF(rect.X + rect.Width - ScrollBarSize / 4, rect.Y + rect.Height / 2), new PointF(rect.Right - rect.Width + ScrollBarSize / 4, rect.Y + rect.Height / 3), new PointF(rect.Right - rect.Width + ScrollBarSize / 4, rect.Bottom - rect.Height / 3) });
			}
		}

		private Rectangle GetCloseRectangle()
		{
			Rectangle rect = _tabRects[_tabControl.SelectedIndex];
			Rectangle textRect = new Rectangle(rect.X, rect.Y + IndicatorSize + 1, rect.Width - CloseButtonWidth, rect.Height - IndicatorSize - 2);
			int crossX = rect.Right - CloseButtonWidth;
			int crossY = textRect.Top + textRect.Height / 2 - CloseMarkWidth / 2;
			if (Vertical)
			{
				crossY -= _scrollPosition;
			}
			else
			{
				crossX -= _scrollPosition;
			}
			Rectangle closeRect = new Rectangle(crossX, crossY, CloseMarkWidth, CloseMarkWidth);
			return closeRect;
		}

		private void PaintVertical(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Skin skin = SkinManager.Instance.CurrentSkin;

			ColorSet background = skin.GetColorSet(PanelType);
			ColorSet tabSet = skin.GetColorSet(TabType);

			Color backColor = background.GetColor(VisualState.Normal, false, Enabled);
			g.Clear(backColor);

			if (_tabControl == null) { return; }

			Brush indicatorBrush = tabSet.GetBrush(VisualState.Normal, false, Enabled);
			Brush hoverBrush = background.GetBrush(VisualState.Hover, false, Enabled);
			Pen innerBorderPen = background.GetBorderPen(VisualState.Normal, false, Enabled);
			g.DrawLine(innerBorderPen, e.ClipRectangle.Right - 1, 0, e.ClipRectangle.Right - 1, e.ClipRectangle.Height);

			List<Rectangle> rects = GetTabRects();
			_activeBrush.Color = Enabled ? tabSet.ForeColor : tabSet.DisabledForeColor;
			_textBrush.Color = Enabled ? background.ForeColor : background.DisabledForeColor;
			for (int i = 0; i < _tabControl.TabPages.Count; i++)
			{
				Rectangle rect = rects[i];
				rect.Y -= _scrollPosition;
				TabPage page = _tabControl.TabPages[i];
				string text = page.Text;

				Rectangle textRect = new Rectangle(rect.X + 3, rect.Y, rect.Width - 6, rect.Height);

				if (_tabControl.SelectedIndex == i)
				{
					Rectangle clientRect = ClientRectangle;
					Brush activeBar = skin.SecondaryColor.GetBrush(VisualState.Normal, false, Enabled);
					g.FillRectangle(indicatorBrush, rect);
					g.FillRectangle(activeBar, new RectangleF(rect.X + 1, rect.Y, IndicatorSize, rect.Height));
					g.DrawString(text, Skin.ActiveTabFont, _activeBrush, new Rectangle(textRect.X + IndicatorSize, textRect.Y, textRect.Width - IndicatorSize, textRect.Height), VerticalFormat);
					g.DrawLine(innerBorderPen, clientRect.X, textRect.Y - 1, clientRect.X, textRect.Bottom);
					g.DrawLine(innerBorderPen, clientRect.X, textRect.Y - 1, clientRect.Right - 2, textRect.Y - 1);
					g.DrawLine(innerBorderPen, clientRect.X, textRect.Bottom, clientRect.Right - 2, textRect.Bottom);
				}
				else if (_hoveredTabIndex == i)
				{
					g.FillRectangle(hoverBrush, new RectangleF(rect.X, rect.Y, rect.Width - 1, rect.Height));
					g.DrawString(text, Skin.TabFont, _textBrush, textRect, VerticalFormat);
				}
				else
				{
					g.DrawString(text, Skin.TabFont, _textBrush, textRect, VerticalFormat);
				}
			}

			//Scroll arrows
			if (IsUpArrowVisible)
			{
				Rectangle rect = _upArrow.Rect;
				SolidBrush brush = skin.PrimaryDarkColor.GetBrush(_upArrow.IsMouseDown ? VisualState.Pressed : _upArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				SolidBrush arrowBrush = skin.SecondaryWidget.GetBrush(_upArrow.IsMouseDown ? VisualState.Pressed : _upArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				g.FillRectangle(brush, rect);
				g.FillPolygon(arrowBrush, new PointF[] { new PointF(rect.X + rect.Width / 3, rect.Bottom - rect.Height / 2 + ScrollBarSize / 4), new PointF(rect.Right - rect.Width / 3, rect.Bottom - rect.Height / 2 + ScrollBarSize / 4), new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 -  ScrollBarSize / 4) });
			}
			if (IsDownArrowVisible)
			{
				SolidBrush brush = skin.PrimaryDarkColor.GetBrush(_downArrow.IsMouseDown ? VisualState.Pressed : _downArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				SolidBrush arrowBrush = skin.SecondaryWidget.GetBrush(_downArrow.IsMouseDown ? VisualState.Pressed : _downArrow.IsHover ? VisualState.Hover : VisualState.Normal);
				Rectangle rect = _downArrow.Rect;
				g.FillRectangle(brush, rect);
				g.FillPolygon(arrowBrush, new PointF[] { new PointF(rect.X + rect.Width / 3, rect.Y + rect.Height / 2 - ScrollBarSize / 4), new PointF(rect.Right - rect.Width / 3, rect.Y + rect.Height / 2 - ScrollBarSize / 4), new PointF(rect.X + rect.Width / 2, rect.Bottom - rect.Height / 2 + ScrollBarSize / 4) });
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			_hoveredTabIndex = -1;
			_upArrow.IsHover = false;
			_downArrow.IsHover = false;
			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			List<Rectangle> rects = GetTabRects();
			Point pt = new Point(e.X, e.Y);

			if (IsUpArrowVisible)
			{
				if (_upArrow.Rect.Contains(pt))
				{
					if (_scrollDirection == 0)
					{
						_upArrow.IsHover = true;
						Invalidate();
					}
					return;
				}
				else if (_upArrow.IsHover)
				{
					_upArrow.IsHover = false;
					Invalidate();
				}
			}
			if (IsDownArrowVisible)
			{
				if (_downArrow.Rect.Contains(pt))
				{
					if (_scrollDirection == 0)
					{
						_downArrow.IsHover = true;
						Invalidate();
					}
					return;
				}
				else if (_downArrow.IsHover)
				{
					_downArrow.IsHover = false;
					Invalidate();
				}
			}

			if (_scrollDirection != 0)
			{
				return;
			}

			for (int i = 0; i < rects.Count; i++)
			{
				Rectangle rect = rects[i];
				if (Vertical)
				{
					rect.Y -= _scrollPosition;
				}
				else
				{
					rect.X -= _scrollPosition;
				}
				if (rect.Contains(pt))
				{
					TabPage page = i < _tabControl.TabPages.Count ? _tabControl.TabPages[i] : null;
					if (page?.Tag?.ToString() == "spacer")
					{
						continue;
					}
					_hoveredTabIndex = i;
					Invalidate();
					return;
				}
			}
			if (_hoveredTabIndex != -1)
			{
				_hoveredTabIndex = -1;
				Invalidate();
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (e.Delta < 0)
			{
				DoScroll(1);
			}
			else if (e.Delta > 0)
			{
				DoScroll(-1);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Point pt = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Left)
			{
				if (IsUpArrowVisible && _upArrow.Rect.Contains(pt))
				{
					_upArrow.IsMouseDown = true;
					StartScrolling(-1);
					DoScroll(-1);
					Invalidate();
				}
				else if (IsDownArrowVisible && _downArrow.Rect.Contains(pt))
				{
					_downArrow.IsMouseDown = true;
					StartScrolling(1);
					DoScroll(1);
					Invalidate();
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_upArrow.IsMouseDown = false;
			_downArrow.IsMouseDown = false;
			StopScrolling();
		}

		private void StartScrolling(int direction)
		{
			_scrollDirection = direction;
			_scrollTimer.Interval = ScrollDelay;
			_scrollTimer.Start();
		}

		private void StopScrolling()
		{
			_scrollDirection = 0;
			_scrollTimer.Stop();
		}

		private void DoScroll(int direction)
		{
			_scrollPosition = Math.Max(0, Math.Min(_scrollPosition + direction * ScrollTick, _maxScroll));
			Invalidate();
			Update();
		}

		private void _scrollTimer_Tick(object sender, EventArgs e)
		{
			_scrollTimer.Interval = ScrollInterval;
			_scrollTimer.Start();
			DoScroll(_scrollDirection);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			List<Rectangle> rects = GetTabRects();
			Point pt = new Point(e.X, e.Y);

			if (_scrollDirection != 0)
			{
				return;
			}

			if ((ShowCloseButton || (_showAdd && _tabControl.SelectedIndex > 0)) && _tabControl.SelectedIndex >= 0)
			{
				Rectangle closeRect = GetCloseRectangle();
				closeRect.X -= 3;
				closeRect.Y -= 3;
				closeRect.Height += 6;
				closeRect.Width += 6;
				if (closeRect.Contains(pt))
				{
					CloseButtonClicked?.Invoke(this, EventArgs.Empty);
					return;
				}
			}

			for (int i = 0; i < rects.Count; i++)
			{
				TabPage page = i < _tabControl.TabPages.Count ? _tabControl.TabPages[i] : null;
				if (page?.Tag?.ToString() == "spacer")
				{
					continue;
				}
				Rectangle rect = rects[i];
				if (Vertical)
				{
					rect.Y -= _scrollPosition;
				}
				else
				{
					rect.X -= _scrollPosition;
				}
				if (rect.Contains(pt))
				{
					if (i >= _tabControl.TabPages.Count)
					{
						if (ShowAddButton)
						{
							//add tab clicked
							AddButtonClicked?.Invoke(this, EventArgs.Empty);
						}
					}
					else
					{
						_tabControl.SelectedIndex = i;
					}
					break;
				}
			}
		}

		public void SetHighlight(TabPage tab, DataHighlight highlight)
		{
			_highlights[tab] = highlight;
			Invalidate();
		}

		private class ScrollBar
		{
			public Rectangle Rect;
			public bool IsHover;
			public bool IsMouseDown;
		}
	}
}

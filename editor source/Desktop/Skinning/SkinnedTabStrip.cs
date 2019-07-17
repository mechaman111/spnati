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

		public event EventHandler CloseButtonClicked;
		public event EventHandler AddButtonClicked;

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

				_previousSelectedTabIndex = _tabControl.SelectedIndex;
				_tabControl.Deselected += (sender, args) =>
				{
					_previousSelectedTabIndex = _tabControl.SelectedIndex;
				};
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
				_tabControl.ControlRemoved += delegate
				{
					_tabRects = null;
					OnUpdateSkin(SkinManager.Instance.CurrentSkin);
					Invalidate();
				};
				_tabControl.VisibleChanged += delegate
				{
					OnUpdateSkin(SkinManager.Instance.CurrentSkin);
				};
			}
		}

		private int _previousSelectedTabIndex;
		private int _hoveredTabIndex = -1;
		private List<Rectangle> _tabRects = null;

		public SkinnedTabStrip()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
			Margin = new Padding(0);
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

		private void CalculateTabRects()
		{
			_tabRects = new List<Rectangle>();

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
					_tabRects.Add(rect);
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
						_tabRects.Add(rect);
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
			using (StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
			{
				using (Brush activeBrush = new SolidBrush(Enabled ? set.ForeColor : set.DisabledForeColor))
				{
					using (Brush textBrush = new SolidBrush(Enabled ? set.ForeColor : set.DisabledForeColor))
					{
						for (int i = 0; i < rects.Count; i++)
						{
							bool showClose = ShowCloseButton || (i > 0 && ShowAddButton && i < rects.Count - 1);
							bool showAdd = (ShowAddButton && i == rects.Count - 1);
							Rectangle rect = rects[i];
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

							//tab and text
							if (i == _tabControl.SelectedIndex)
							{
								SolidBrush tabBrush = tabSet.GetBrush(VisualState.Normal, false, Enabled);
								g.FillRectangle(tabBrush, rect.X, rect.Y, rect.Width, rect.Height + 1); //Height + 1 to cover border line
								g.FillRectangle(indicatorBrush, new RectangleF(rect.X, 1, rect.Width, IndicatorSize));
								g.DrawLine(borderPen, rect.X, rect.Top, rect.Right, rect.Top);
								g.DrawLine(borderPen, rect.X, rect.Y + 1, rect.X, rect.Bottom);
								g.DrawLine(borderPen, rect.Right, rect.Y + 1, rect.Right, rect.Bottom);

								using (SolidBrush foreBrush = new SolidBrush(Enabled ? tabSet.ForeColor : tabSet.DisabledForeColor))
								{
									g.DrawString(text, Skin.ActiveTabFont, foreBrush, textRect, sf);

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

								g.DrawString(text, Skin.TabFont, textBrush, textRect, sf);
							}
						}
					}
				}
			}
		}

		private Rectangle GetCloseRectangle()
		{
			Rectangle rect = _tabRects[_tabControl.SelectedIndex];
			Rectangle textRect = new Rectangle(rect.X, rect.Y + IndicatorSize + 1, rect.Width - CloseButtonWidth, rect.Height - IndicatorSize - 2);
			int crossX = rect.Right - CloseButtonWidth;
			int crossY = textRect.Top + textRect.Height / 2 - CloseMarkWidth / 2;
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

			using (StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
			{
				using (Brush activeBrush = new SolidBrush(Enabled ? tabSet.ForeColor : tabSet.DisabledForeColor))
				{
					using (Brush textBrush = new SolidBrush(Enabled ? background.ForeColor : background.DisabledForeColor))
					{
						for (int i = 0; i < _tabControl.TabPages.Count; i++)
						{
							Rectangle rect = rects[i];
							TabPage page = _tabControl.TabPages[i];
							string text = page.Text;

							Rectangle textRect = new Rectangle(rect.X + 3, rect.Y, rect.Width - 6, rect.Height);

							if (_tabControl.SelectedIndex == i)
							{
								Rectangle clientRect = ClientRectangle;
								Brush activeBar = skin.SecondaryColor.GetBrush(VisualState.Normal, false, Enabled);
								g.FillRectangle(indicatorBrush, rect);
								g.FillRectangle(activeBar, new RectangleF(rect.X + 1, rect.Y, IndicatorSize, rect.Height));
								g.DrawString(text, Skin.ActiveTabFont, activeBrush, new Rectangle(textRect.X + IndicatorSize, textRect.Y, textRect.Width - IndicatorSize, textRect.Height), sf);
								g.DrawLine(innerBorderPen, clientRect.X, textRect.Y - 1, clientRect.X, textRect.Bottom);
								g.DrawLine(innerBorderPen, clientRect.X, textRect.Y - 1, clientRect.Right - 2, textRect.Y - 1);
								g.DrawLine(innerBorderPen, clientRect.X, textRect.Bottom, clientRect.Right - 2, textRect.Bottom);
							}
							else if (_hoveredTabIndex == i)
							{
								g.FillRectangle(hoverBrush, new RectangleF(rect.X, rect.Y, rect.Width - 1, rect.Height));
								g.DrawString(text, Skin.TabFont, textBrush, textRect, sf);
							}
							else
							{
								g.DrawString(text, Skin.TabFont, textBrush, textRect, sf);
							}
						}
					}
				}
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			_hoveredTabIndex = -1;
			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			List<Rectangle> rects = GetTabRects();
			Point pt = new Point(e.X, e.Y);
			for (int i = 0; i < rects.Count; i++)
			{
				Rectangle rect = rects[i];
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

		protected override void OnMouseClick(MouseEventArgs e)
		{
			List<Rectangle> rects = GetTabRects();
			Point pt = new Point(e.X, e.Y);

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
				TabPage page = i < _tabControl.TabPages.Count ? _tabControl.TabPages[i]: null;
				if (page?.Tag?.ToString() == "spacer")
				{
					continue;
				}
				Rectangle rect = rects[i];
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
	}
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.Actions;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public partial class Timeline : UserControl
	{
		private const int MajorTickHeight = 10;
		private const int TickHeight = 6;
		private const int MinorTickHeight = 4;
		private const float PixelsPerSecond = 200;
		private const int RowHeight = 20;
		private const int IconSize = 16;
		private const int IconPadding = 2;

		private const float MinZoom = 0.25f;
		private const float MaxZoom = 2.5f;

		private float _tickResolution = 0.10f;
		private int MajorTickFrequency = 10;

		public static Cursor HandOpen = LoadCursor(Properties.Resources.hand_open);
		public static Cursor HandClosed = LoadCursor(Properties.Resources.hand_closed);
		private NumberFormatInfo _timeFormatter;
		private Brush _trackFill;
		private Brush _trackFillAlternate;
		private Brush _trackFillSelected;
		private Brush _brushTimeline;
		private Brush _brushWidgetHeader;
		private Pen _penTickMajor;
		private Pen _penTick;
		private Pen _penTickMinor;
		private Pen _penCanvasTickMajor;
		private Pen _penCanvasTickMinor;
		private Font _fontTimeline;
		private Brush _brushFont;
		private StringFormat _rowHeaderFormat;
		private Brush _widgetSelectedFill;
		private Pen _widgetOutline;
		private Pen _widgetSelectedOutline;
		private Pen _timeLineAxis;
		private Pen _timeLine;
		private Pen _playbackLineAxis;
		private Pen _playbackLine;
		private Brush _iconHoverFill;

		private float _zoom = 1;
		private ITimelineAction _pendingAction = null;
		private ITimelineWidget _pendingWidget = null;
		private ITimelineAction _currentAction = null;
		private DateTime _lastTick;
		private ITimelineWidget _headerHoverWidget = null;
		private int _headerHoverRow = -1;
		private int _headerHoverIconIndex;

		private int _headerX;
		private int _headerY;

		private List<ITimelineWidget> _widgets = new List<ITimelineWidget>();
		private ITimelineWidget _selectedWidget;

		private PlaybackMode _playbackMode = PlaybackMode.Once;

		public event EventHandler<ITimelineWidget> WidgetSelected;
		public event EventHandler<float> TimeChanged;
		public event EventHandler<float> PlaybackTimeChanged;
		public event EventHandler<DataSelectionArgs> DataSelected;
		public event EventHandler<bool> PlaybackChanged;

		private UndoManager _history;
		public UndoManager CommandHistory
		{
			get { return _history; }
			set
			{
				_history = value;
				if (_history != null)
				{
					_history.CommandApplied += UndoManager_CommandApplied;
				}
			}
		}

		public ITimelineWidget SelectedWidget
		{
			get { return _selectedWidget; }
		}

		private float _time = 0;
		public float CurrentTime
		{
			get { return _time; }
			set
			{
				UpdateMarker(value);
				//int x = TimeToX(value);
				//int left = container.HorizontalScroll.Value;
				//int right = left + container.Width;
				//if (x < left || x > right)
				//{
				//	//using (Control child = new Control() { Parent = container, Left = x })
				//	//{
				//	//	container.ScrollControlIntoView(child);
				//	//}
				//	container.HorizontalScroll.Value = Math.Max(container.HorizontalScroll.Minimum, Math.Max(container.HorizontalScroll.Maximum, x - (right - left) / 2));
				//}
			}
		}

		private float _playbackTime = 0;
		public float PlaybackTime
		{
			get { return _playbackTime; }
			set
			{
				if (_playbackTime == value) { return; }
				_playbackTime = value;
				Redraw();
				PlaybackTimeChanged?.Invoke(this, value);
			}
		}

		public ITimelineData Data { get; private set; }

		private static Cursor LoadCursor(byte[] array)
		{
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				return new Cursor(memoryStream);
			}
		}

		public Timeline()
		{
			InitializeComponent();

			_timeFormatter = new NumberFormatInfo();
			_timeFormatter.NumberDecimalSeparator = ":";
			_trackFill = new SolidBrush(Color.FromArgb(200, 200, 200));
			_trackFillAlternate = new SolidBrush(Color.FromArgb(210, 210, 210));
			_trackFillSelected = new SolidBrush(Color.FromArgb(230, 230, 255));
			_brushTimeline = new SolidBrush(Color.FromArgb(230, 230, 230));
			_brushWidgetHeader = new SolidBrush(Color.FromArgb(185, 185, 185));
			_fontTimeline = new Font("Arial", 8);
			_brushFont = new SolidBrush(Color.Black);
			_penTickMajor = new Pen(Color.FromArgb(0, 0, 0));
			_penTick = new Pen(Color.FromArgb(130, 130, 130));
			_penTickMinor = new Pen(Color.FromArgb(170, 170, 170));
			_penCanvasTickMinor = new Pen(Color.FromArgb(170, 170, 170));
			_penCanvasTickMajor = new Pen(Color.FromArgb(150, 150, 150));
			_rowHeaderFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.LineLimit };
			_widgetSelectedFill = new SolidBrush(Color.White);
			_widgetOutline = new Pen(Color.Black);
			_widgetSelectedOutline = new Pen(Color.White);
			_timeLineAxis = new Pen(Color.Red, 3);
			_timeLine = new Pen(Color.Red, 1);
			_playbackLineAxis = new Pen(Color.DarkGreen, 3);
			_playbackLine = new Pen(Color.DarkGreen, 1);
			_iconHoverFill = new SolidBrush(Color.FromArgb(50, 0, 0, 0));

			container.MouseWheel += Panel_MouseWheel;

			UpdateMarker(0);
		}

		private void Timeline_Load(object sender, EventArgs e)
		{
			Clipboards.ClipboardUpdated += Clipboards_ClipboardUpdated;
		}

		private void Clipboards_ClipboardUpdated(object sender, EventArgs e)
		{
			Type type = (Type)sender;
			if (type == typeof(ITimelineWidget))
			{
				tsPaste.Enabled = true;
			}
		}

		private void UndoManager_CommandApplied(object sender, CommandEventArgs args)
		{
			tsUndo.Enabled = CommandHistory.CanUndo();
			tsRedo.Enabled = CommandHistory.CanRedo();
			if (_selectedWidget != null)
			{
				if (!_widgets.Contains(_selectedWidget))
				{
					SelectWidget(null);
				}
				else
				{
					UpdateEditMenu();
				}
			}
			Redraw();
		}

		private void UpdateEditMenu()
		{
			WidgetSelectionArgs args = new WidgetSelectionArgs(this, SelectionType.Select, ModifierKeys);
			_selectedWidget?.UpdateSelection(args);
			UpdateButtons(args);
		}

		public void SetData(ITimelineData data)
		{
			CleanUp();
			if (data == null) { return; }
			Data = data;
			Data.WidgetMoved += Data_WidgetMoved;
			Data.WidgetCreated += Data_WidgetCreated;
			Data.WidgetRemoved += Data_WidgetRemoved;
			foreach (ITimelineWidget widget in data.CreateWidgets())
			{
				AddWidget(widget, _widgets.Count);
			}
			ResizeTimeline();
			UpdateMarker(0);
			SelectWidget(null);
			AutoZoom();
		}

		private void CleanUp()
		{
			EnablePlayback(false);
			if (Data != null)
			{
				Data.WidgetMoved -= Data_WidgetMoved;
				Data.WidgetCreated -= Data_WidgetCreated;
				Data.WidgetRemoved -= Data_WidgetRemoved;
				Data = null;
			}
			_widgets.Clear();
		}

		private void Data_WidgetMoved(object sender, WidgetCreationArgs args)
		{
			MoveWidget(args.Widget, args.Index);
		}

		private void Data_WidgetRemoved(object sender, WidgetCreationArgs args)
		{
			RemoveWidget(args.Widget);
		}

		private void Data_WidgetCreated(object sender, WidgetCreationArgs args)
		{
			AddWidget(args.Widget, args.Index);
		}

		public ITimelineWidget CreateWidget(object context)
		{
			//TODO: This needs to be a command to be undoable
			if (Data == null) { return null; }
			ITimelineWidget widget = Data.CreateWidget(_time, context);
			AddWidget(widget, _widgets.Count);
			return widget;
		}

		public void CreateWidget(object data, int index)
		{
			//TODO: This needs to be a command to be undoable
			if (Data == null) { return; }
			ITimelineWidget widget = Data.CreateWidget(_time, data, index);
			AddWidget(widget, index);
		}

		public void RemoveSelectedWidget()
		{
			if (_selectedWidget == null) { return; }
			DeleteWidgetCommand action = new DeleteWidgetCommand(Data, _selectedWidget);
			_history?.Commit(action);
		}

		private void AddWidget(ITimelineWidget widget, int index)
		{
			if (index == -1)
			{
				_widgets.Add(widget);
			}
			else
			{
				_widgets.Insert(index, widget);
			}
			widget.Invalidated += Widget_Invalidated;
			ResizeTimeline();
			Redraw();
		}

		private void RemoveWidget(ITimelineWidget widget)
		{
			_widgets.Remove(widget);
			widget.Invalidated -= Widget_Invalidated;
			ResizeTimeline();
			Redraw();
		}

		private void MoveWidget(ITimelineWidget widget, int index)
		{
			int track = _widgets.IndexOf(widget);

			_widgets.RemoveAt(track);
			if (index >= _widgets.Count || index == -1)
			{
				_widgets.Add(widget);
			}
			else
			{
				_widgets.Insert(index, widget);
			}
		}

		private void tmrTick_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			float duration = Duration;
			TimeSpan elapsed = now - _lastTick;
			float elapsedSec = (float)elapsed.TotalSeconds;
			_lastTick = now;
			float time = _playbackTime + elapsedSec;
			if (time > duration)
			{
				switch (_playbackMode)
				{
					case PlaybackMode.Once:
						EnablePlayback(false);
						break;
					case PlaybackMode.Looping:
						time -= duration;
						break;
				}
			}
			PlaybackTime = time;// (_playbackTime + elapsedSec) % duration;
			Redraw();
		}

		private void Widget_Invalidated(object sender, EventArgs e)
		{
			ResizeTimeline();
			Redraw();
		}

		private void Redraw()
		{
			panel.Invalidate();
			panelAxis.Invalidate();
			panelHeader.Invalidate();
		}


		/// <summary>
		/// Converts a Y position into a widget's relative row
		/// </summary>
		/// <param name="y"></param>
		private int YToRow(int y, out int trackIndex)
		{
			if (y < 0)
			{
				trackIndex = 0;
				return 0;
			}
			trackIndex = -1;
			int targetRow = y / RowHeight; //absolute row

			int row = 0;
			for (int i = 0; i < _widgets.Count; i++)
			{
				trackIndex = i;
				ITimelineWidget widget = _widgets[i];
				if (row == targetRow)
				{
					return 0;
				}
				if (widget == null)
				{
					row++;
					continue;
				}
				else
				{
					int count = widget.GetRowCount();
					if (targetRow >= row && targetRow < row + count)
					{
						return targetRow - row;
					}
					row += count;
				}
			}
			trackIndex = -1;
			return 0;
		}

		/// <summary>
		/// Converts a time value in seconds to the corresponding X position on the timeline
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public int TimeToX(float time)
		{
			return (int)Math.Round(time * PixelsPerSecond * _zoom);
		}

		/// <summary>
		/// Converts a position on the timeline to the corresponding time in seconds
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public float XToTime(float x)
		{
			return x / (PixelsPerSecond * _zoom);
		}

		private void Timeline_Resize(object sender, EventArgs e)
		{
			//resize the inner canvas to fit the animation or fill the panel
			ResizeTimeline();
		}

		public float Duration
		{
			get
			{
				float duration = 0;
				if (_widgets.Count > 0)
				{
					duration = _widgets.Max(t => t.GetEnd(0));
				}
				return duration;
			}
		}

		private void ResizeTimeline()
		{
			float duration = Duration;

			float visibleTime = XToTime(container.Width);
			duration = Math.Max(visibleTime, duration + 1);
			int width = Math.Max((int)(PixelsPerSecond * _zoom * duration), container.Width - SystemInformation.VerticalScrollBarWidth);
			panel.Width = width;
			int height = Math.Max(TotalRowCount * RowHeight, container.Height - SystemInformation.HorizontalScrollBarHeight);
			panel.Height = height;
			Redraw();
		}

		private int TotalRowCount
		{
			get
			{
				int rows = 1;
				if (_widgets.Count > 0)
				{
					rows = _widgets.Sum(w => w.GetRowCount());
				}
				return rows;
			}
		}

		private void Panel_MouseWheel(object sender, MouseEventArgs e)
		{
			if (ModifierKeys.HasFlag(Keys.Control))
			{
				if (_currentAction == null)
				{
					((HandledMouseEventArgs)e).Handled = true;
					if (e.Delta > 0 && _zoom < MaxZoom)
					{
						ZoomIn();
					}
					else if (e.Delta < 0)
					{
						ZoomOut();
					}
				}
			}
			panelHeader.Invalidate();
		}

		private void panelAxis_Paint(object sender, PaintEventArgs e)
		{
			int height = panelAxis.Height;
			int startX = container.HorizontalScroll.Value;
			Graphics g = e.Graphics;
			g.FillRectangle(_brushTimeline, 0, 0, panelAxis.Width, height);
			g.DrawLine(_penTickMajor, 0, height, panelAxis.Width, height);
			g.DrawLine(_penTickMajor, 0, panelAxis.Height - 1, panelAxis.Width, panelAxis.Height - 1);
			int step = 0;

			float duration = XToTime(panelAxis.Width + startX);

			while (true)
			{
				float time = step * _tickResolution;
				if (time > duration)
				{
					break;
				}
				bool major = step % MajorTickFrequency == 0;
				bool minor = !major && step % 2 == 1;
				int x = TimeToX(time) - startX;
				Pen pen = _penTick;
				int tickHeight = TickHeight;

				if (major || (_zoom > 1.25f && step % 5 == 0))
				{
					major = true;
					g.DrawString(time.ToString("0.00", _timeFormatter), _fontTimeline, _brushFont, x + 1, 0);
				}

				if (major)
				{
					tickHeight = MajorTickHeight;
					pen = _penTickMajor;
				}
				else if (minor)
				{
					tickHeight = MinorTickHeight;
					pen = _penTickMinor;
				}
				g.DrawLine(pen, x, height - 1, x, height - 1 - tickHeight);

				step++;
			}

			int start;
			//playback time marker
			if (tmrTick.Enabled)
			{
				start = TimeToX(_playbackTime) - startX;
				g.DrawLine(_playbackLineAxis, start, 0, start, height);
			}

			//Current time marker
			start = TimeToX(CurrentTime) - startX;
			g.DrawLine(_timeLineAxis, start, 0, start, height);

			start = TimeToX(Duration) - startX + 1;
			g.DrawLine(Pens.White, start, 0, start, height);
		}

		private void panelHeader_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int startY = container.VerticalScroll.Value;
			int width = panelHeader.Width;
			g.FillRectangle(_trackFill, 0, 0, panelHeader.Width, panelHeader.Height);

			int y = -startY;
			for (int t = 0; t < _widgets.Count; t++)
			{
				ITimelineWidget widget = _widgets[t];
				if (widget != null)
				{
					int rowCount = widget.GetRowCount();
					int trackY = y;
					if (widget == _selectedWidget)
					{
						g.FillRectangle(_trackFillSelected, 0, trackY + 1, panelHeader.Width, rowCount * RowHeight - 1);
					}

					for (int row = 0; row < rowCount; row++)
					{
						int rowY = trackY + row * RowHeight;
						if (row == 0 || _selectedWidget == widget)
						{
							bool highlighted = widget.IsRowHighlighted(row);
							if (row == 0 || highlighted)
							{
								g.FillRectangle(_selectedWidget == widget && highlighted ? _widgetSelectedFill : _brushWidgetHeader, 0, rowY + 1, width, RowHeight - 1);
							}
						}
						string label = widget.GetLabel(row);
						if (row == 0 && widget.IsCollapsible)
						{
							g.DrawImage(widget.IsCollapsed ? Properties.Resources.Expand : Properties.Resources.Collapse, 0, rowY + RowHeight / 2 - Properties.Resources.Expand.Height / 2);
						}
						int left = Properties.Resources.Expand.Width;
						Image thumbnail = widget.GetThumbnail();
						if (thumbnail != null && row == 0)
						{
							g.DrawImage(thumbnail, new Rectangle(left + 2, rowY + 1, RowHeight - 2, RowHeight - 2), new Rectangle(0, 0, thumbnail.Width, thumbnail.Height), GraphicsUnit.Pixel);
							left += RowHeight;
						}
						g.DrawString(label, _fontTimeline, _brushFont, new Rectangle(left, rowY, width - left, RowHeight), _rowHeaderFormat);

						int headerIconCount = widget.GetHeaderIconCount(row);
						int iconLeft = panelHeader.Width - IconPadding - IconSize;
						int iconTop = rowY + RowHeight / 2 - IconSize / 2;
						for (int i = 0; i < headerIconCount; i++)
						{
							int index = -1;
							if (widget == _headerHoverWidget && i == _headerHoverIconIndex && row == _headerHoverRow)
							{
								g.FillRectangle(_iconHoverFill, iconLeft, iconTop, IconSize, IconSize);
								index = _headerHoverIconIndex;
							}
							widget.DrawHeaderIcon(g, row, i, iconLeft, iconTop, IconSize, index);
							iconLeft -= IconSize + IconPadding;
						}

						g.DrawLine(_penCanvasTickMinor, 0, rowY + RowHeight, width, rowY + RowHeight);

						if (y > panelHeader.Height)
						{
							return;
						}
					}
					y += RowHeight * rowCount;
				}
				else
				{
					y += RowHeight;
				}
				g.DrawLine(_penCanvasTickMajor, 0, y, panelHeader.Width, y);
			}
			g.DrawLine(_penCanvasTickMajor, panelHeader.Width - 1, 0, panelHeader.Width - 1, panelHeader.Height);
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.FillRectangle(_trackFill, 0, 0, panel.Width, panel.Height);

			float duration = Duration;

			float pps = PixelsPerSecond * _zoom;
			int y = 0;
			for (int i = 0; i < _widgets.Count; i++)
			{
				ITimelineWidget widget = _widgets[i];
				int trackHeight = RowHeight;
				int rowCount = 1;
				if (widget != null)
				{
					rowCount = widget.GetRowCount();
					trackHeight = widget.GetRowCount() * RowHeight;
				}
				int trackY = y;
				if (_selectedWidget == widget)
				{
					g.FillRectangle(_trackFillSelected, 0, y + 1, panel.Width, trackHeight - 1);
				}
				else if (i % 2 == 1)
				{
					g.FillRectangle(_trackFillAlternate, 0, y + 1, panel.Width, trackHeight - 1);
				}

				for (int r = 0; r < rowCount; r++)
				{
					//grid line
					int rowY = y + r * RowHeight + RowHeight;
					g.DrawLine(r < rowCount - 1 ? _penCanvasTickMinor : _penCanvasTickMajor, 0, rowY, panel.Width, rowY);
				}
				DrawTickMarks(g, y, trackHeight);

				if (widget != null)
				{
					int count = rowCount;
					float start = widget.GetStart();
					float length = widget.GetLength(duration);
					int x = TimeToX(start);

					//background
					int top = trackY;
					int widgetWidth = TimeToX(length);
					g.FillRectangle(widget.GetFillBrush(), x, top, widgetWidth, count * RowHeight);

					//outline
					int width = TimeToX(length);
					g.DrawRectangle(_widgetOutline, x, top, width, count * RowHeight - 1);
					if (_selectedWidget == widget)
					{
						g.DrawRectangle(_widgetSelectedOutline, x + 2, top + 2, width - 4, count * RowHeight - 5);
					}

					//contents
					for (int row = 0; row < count; row++)
					{
						int rowY = trackY + row * RowHeight;
						//contents
						widget.DrawContents(g, row, x + 1, rowY + 1, pps, widgetWidth - 1, RowHeight - 2);
					}


					y += rowCount * RowHeight;
				}
				else
				{
					y += RowHeight;
				}
			}

			int timeX;
			if (tmrTick.Enabled)
			{
				timeX = TimeToX(PlaybackTime);
				g.DrawLine(_playbackLine, timeX, 0, timeX, panel.Height);
			}

			//Current time marker
			timeX = TimeToX(CurrentTime);
			g.DrawLine(_timeLine, timeX, 0, timeX, panel.Height);

			timeX = TimeToX(duration) + 1;
			g.DrawLine(Pens.White, timeX, 0, timeX, panel.Height);
		}

		private void DrawTickMarks(Graphics g, int y, int height)
		{
			float end = Duration;
			float step = 0;
			while (true)
			{
				float time = step * _tickResolution;
				if (time > end)
				{
					break;
				}
				bool major = step % MajorTickFrequency == 0;
				int x = TimeToX(time);
				Pen canvasPen = _penCanvasTickMinor;
				if (major)
				{
					canvasPen = _penCanvasTickMajor;
				}
				g.DrawLine(canvasPen, x, y, x, y + height);
				step++;
			}
		}

		private void container_Scroll(object sender, ScrollEventArgs e)
		{
			Redraw();
		}

		private ITimelineWidget GetWidgetUnderCursorInHeader(int y)
		{
			int row = y / RowHeight; //absolute row
			int currentRow = 0;
			foreach (ITimelineWidget widget in _widgets)
			{
				int count = widget == null ? 1 : widget.GetRowCount();
				if (currentRow + count > row)
				{
					return widget;
				}
				currentRow += count;
			}
			return null;
		}

		private void panelHeader_MouseDown(object sender, MouseEventArgs e)
		{
			int startY = container.VerticalScroll.Value;
			int row = (e.Y + startY) / RowHeight; //absolute row
			int currentRow = 0;
			foreach (ITimelineWidget widget in _widgets)
			{
				int count = widget == null ? 1 : widget.GetRowCount();
				if (currentRow + count > row)
				{
					if (currentRow == row)
					{
						//clicking collapsible button on title row
						if (widget != null && e.X <= Properties.Resources.Expand.Width && widget.IsCollapsible)
						{
							widget.IsCollapsed = !widget.IsCollapsed;
							panel.Invalidate();
							ResizeTimeline();
							return;
						}
					}
					//clicking any row within the widget
					if (widget != null)
					{
						SelectWidget(widget);

						int track;
						int r = YToRow(e.Y + startY, out track);
						int icon = GetHeaderIconIndex(e.X, widget, r);
						WidgetActionArgs args = GetWidgetArgs(_time, e.Y + startY);
						if (icon >= 0)
						{
							widget.OnClickHeaderIcon(args, icon);
						}
						widget.OnClickHeader(args);
						WidgetSelectionArgs selectionArgs = new WidgetSelectionArgs(this, SelectionType.Reselect, ModifierKeys);
						widget.UpdateSelection(selectionArgs);
						UpdateButtons(selectionArgs);
					}
					panelHeader.Invalidate();
					return;
				}
				currentRow += count;
			}
		}

		private void panelHeader_MouseLeave(object sender, EventArgs e)
		{
			tmrTooltip.Stop();
		}

		private void panelHeader_MouseMove(object sender, MouseEventArgs e)
		{
			int startY = container.VerticalScroll.Value;
			_headerX = e.X;
			_headerY = e.Y;
			tmrTooltip.Stop();

			int y = e.Y + startY;

			//figure out which icon is being hovered over
			ITimelineWidget widget = GetWidgetUnderCursorInHeader(y);
			int row = _headerHoverRow;
			int icon = _headerHoverIconIndex;
			if (widget != null)
			{
				int track;
				row = YToRow(y, out track);
				icon = GetHeaderIconIndex(e.X, widget, row);
			}
			if (widget != _headerHoverWidget || row != _headerHoverRow || icon != _headerHoverIconIndex)
			{
				_headerHoverWidget = widget;
				_headerHoverRow = row;
				_headerHoverIconIndex = icon;
				panelHeader.Invalidate();
			}

			tooltip.Tag = new Tuple<int, int, bool>(e.X, y, true);
			tmrTooltip.Start();
		}

		private void panel_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.None)
			{
				ITimelineAction old = _pendingAction;
				ITimelineWidget widget = GetWidgetUnderCursor(e.Y);
				if (widget != _pendingWidget && _pendingWidget != null)
				{
					_pendingWidget.OnMouseOut();
				}
				if (widget != null)
				{
					_pendingAction = GetAction(widget, e.X, e.Y);
					if (_pendingAction != null || old != null)
					{
						_pendingWidget = widget;
						panel.Invalidate();
					}
					Cursor = (_pendingAction != null ? _pendingAction.GetHoverCursor() : Cursors.Default);
				}
			}
			else if (_currentAction != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					UpdateAction(XToTime(e.X), e.Y);
				}
			}
		}

		private void panel_MouseLeave(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void panel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int track;
				YToRow(e.Y, out track);
				ITimelineWidget widget = GetWidgetUnderCursor(e.Y);

				if (_selectedWidget != widget)
				{
					SelectWidget(widget);
				}

				if (widget != null && _pendingAction != null)
				{
					_currentAction = _pendingAction;
					_pendingAction = null;
					StartAction(XToTime(e.X), e.Y);
				}
			}
		}

		private void panel_MouseUp(object sender, MouseEventArgs e)
		{
			EndAction();
		}

		private void SelectWidget(ITimelineWidget widget)
		{
			ApplyWidgetSelection(widget);
		}

		public void ApplyWidgetSelection(ITimelineWidget widget)
		{
			WidgetSelectionArgs args = new WidgetSelectionArgs(this, SelectionType.Select, ModifierKeys);
			if (widget != _selectedWidget)
			{
				if (_selectedWidget != null)
				{
					args.IsSelected = SelectionType.Deselect;
					_selectedWidget.OnWidgetSelectionChanged(args);
				}
				_selectedWidget = widget;
				args.IsSelected = SelectionType.Select;
			}
			else
			{
				args.IsSelected = SelectionType.Reselect;
			}
			if (_selectedWidget != null)
			{
				_selectedWidget.OnWidgetSelectionChanged(args);
				tsNext.Enabled = true;
				tsPrevious.Enabled = true;
			}
			else
			{
				Data.UpdateSelection(args);
				tsNext.Enabled = false;
				tsPrevious.Enabled = false;
			}
			UpdateButtons(args);

			if (args.IsSelected == SelectionType.Select)
			{
				WidgetSelected?.Invoke(this, _selectedWidget);
			}

			Redraw();
		}

		private void UpdateButtons(WidgetSelectionArgs args)
		{
			tsCut.Enabled = args.AllowCut;
			tsCopy.Enabled = args.AllowCopy;
			tsPaste.Enabled = args.AllowPaste;
			tsDuplicate.Enabled = args.AllowDuplicate;
			tsDelete.Enabled = args.AllowDelete;
		}

		/// <summary>
		/// Selects the widget with the provided backing data
		/// </summary>
		/// <param name="data"></param>
		public void SelectWidgetWithData(object data)
		{
			ITimelineWidget widget = _widgets.Find(w => w.GetData() == data);
			if (widget != null)
			{
				SelectWidget(widget);
			}
		}

		public void SelectData(object data)
		{
			DataSelected?.Invoke(this, new DataSelectionArgs(data, null));
		}
		public void SelectData(object data, object previewData)
		{
			DataSelected?.Invoke(this, new DataSelectionArgs(data, previewData));
		}

		private ITimelineWidget GetWidgetUnderCursor(int y)
		{
			int track;
			YToRow(y, out track);
			if (track == -1)
			{
				return null;
			}
			ITimelineWidget widget = _widgets[track];
			if (widget != null)
			{
				return widget;
			}
			return null;
		}
		
		private void StartAction(float time, int y)
		{
			WidgetActionArgs args = GetWidgetArgs(time, y);
			_currentAction.Start(args);
			Cursor = _currentAction.GetCursor();
		}

		private WidgetActionArgs GetWidgetArgs(float time, int y)
		{
			int track;
			int row = YToRow(y, out track);
			WidgetActionArgs args = new WidgetActionArgs()
			{
				History = _history,
				Timeline = this,
				Data = Data,
				Widget = _selectedWidget,
				Time = time,
				Row = row,
				SnapIncrement = _tickResolution,
				Track = track,
				Modifiers = ModifierKeys,
				Duration = Duration,
			};

			return args;
		}

		private WidgetOperationArgs GetOperationArgs()
		{
			WidgetOperationArgs args = new WidgetOperationArgs()
			{
				History = _history,
				Timeline = this,
				Time = CurrentTime,
				Data = Data,
			};
			return args;
		}

		private void UpdateAction(float time, int y)
		{
			WidgetActionArgs args = GetWidgetArgs(time, y);

			_currentAction?.Update(args);
			Redraw();
		}

		private void EndAction()
		{
			if (_currentAction != null)
			{
				_currentAction.Finish();
				_currentAction = null;
				Cursor = Cursors.Default;
				if (_selectedWidget != null)
				{
					WidgetSelectionArgs args = new WidgetSelectionArgs(this, SelectionType.Select, ModifierKeys);
					_selectedWidget.UpdateSelection(args);
					UpdateButtons(args);
				}
			}
		}

		private void panelAxis_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				//placing the time marker
				_currentAction = new TimelineDragAction(this);
				float time = XToTime(e.X + container.HorizontalScroll.Value);
				float inverse = 1 / _tickResolution * 2;
				time = (float)Math.Round(Math.Round(time * inverse) / inverse, 2);
				StartAction(time, 0);
			}
		}

		private void panelAxis_MouseUp(object sender, MouseEventArgs e)
		{
			EndAction();
		}

		private void panelAxis_MouseMove(object sender, MouseEventArgs e)
		{
			if (_currentAction != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					float time = XToTime(e.X + container.HorizontalScroll.Value);
					if (!ModifierKeys.HasFlag(Keys.Shift))
					{
						float inverse = 1 / _tickResolution * 2;
						time = (float)Math.Round(Math.Round(time * inverse) / inverse, 2);
					}
					UpdateAction(time, 0);
				}
			}
		}

		private void UpdateMarker(float time)
		{
			time = Math.Max(0, time);
			if (time == _time) { return; }

			_time = time;
			Redraw();
			TimeChanged?.Invoke(this, time);
		}

		public void FinalizeTimeMovement()
		{
			if (_selectedWidget != null)
			{
				_selectedWidget.OnTimeChanged(GetOperationArgs());
			}
		}

		/// <summary>
		/// Gets what action a mousedown will perform when clicking the target point
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private ITimelineAction GetAction(ITimelineWidget widget, int x, int y)
		{
			float pps = PixelsPerSecond * _zoom;
			if (widget != null)
			{
				_pendingWidget = widget;
				float start = widget.GetStart();
				int track;
				int row = YToRow(y, out track);
				return widget.GetAction(x - TimeToX(start), TimeToX(widget.GetLength(Duration)), XToTime(x), row, TimeToX(Duration), pps);
			}
			return _selectedWidget != null ? null : new SelectAction();
		}

		/// <summary>
		/// Pseudo-action to get a cursor. This doesn't actually do anything since selection is performed separately by the timeline
		/// </summary>
		private class SelectAction : ITimelineAction
		{
			public Cursor GetHoverCursor() { return GetCursor(); }
			public Cursor GetCursor() { return Cursors.Hand; }
			public void Finish() { }
			public void Start(WidgetActionArgs args) { }
			public void Update(WidgetActionArgs args) { }
		}

		private void tsPlay_Click(object sender, EventArgs e)
		{
			EnablePlayback(!tmrTick.Enabled);
		}

		private void EnablePlayback(bool enabled)
		{
			if (tmrTick.Enabled == enabled)
			{
				return;
			}
			if (enabled)
			{
				PlaybackTime = 0;
			}
			tmrTick.Enabled = enabled;
			_lastTick = DateTime.Now;
			if (tmrTick.Enabled)
			{
				tsPlay.ToolTipText = "Pause";
				tsPlay.Image = Properties.Resources.TimelinePause;
			}
			else
			{
				tsPlay.ToolTipText = "Play";
				switch (_playbackMode)
				{
					case PlaybackMode.Once:
						tsPlay.Image = Properties.Resources.TimelinePlayOnce;
						break;
					case PlaybackMode.Looping:
						tsPlay.Image = Properties.Resources.TimelinePlay;
						break;
					case PlaybackMode.OnceLooping:
						tsPlay.Image = Properties.Resources.TimelinePlayLoops;
						break;
				}
				
			}
			PlaybackChanged?.Invoke(this, tmrTick.Enabled);
			foreach (ITimelineWidget widget in _widgets)
			{
				widget.OnPlaybackChanged(tmrTick.Enabled);
			}
			Redraw();
		}

		private void tsFirst_Click(object sender, EventArgs e)
		{
			UpdateMarker(0);
			PlaybackTime = 0;
		}

		private void tsLast_Click(object sender, EventArgs e)
		{
			UpdateMarker(Duration);
		}

		private void tsUndo_Click(object sender, EventArgs e)
		{
			if (tmrTick.Enabled) { return; }
			//disabling for now since history with the PropertyTable is a bit buggy and the toolbar actions weren't made with commands
			//_history?.Undo();
		}

		private void tsRedo_Click(object sender, EventArgs e)
		{
			if (tmrTick.Enabled) { return; }
			//_history?.Redo();
		}

		private void tsCut_Click(object sender, EventArgs e)
		{
			if (tmrTick.Enabled) { return; }
			if (this.ContainsActiveControl())
			{
				if (_selectedWidget == null) { return; }
				WidgetOperationArgs args = GetOperationArgs();
				args.IsSilent = true;
				_selectedWidget.OnCopy(args);
				_selectedWidget.OnDelete(args);
				tsPaste.Enabled = true;
			}
			else
			{
				this.HandleCut();
			}

		}

		private void tsCopy_Click(object sender, EventArgs e)
		{
			if (this.ContainsActiveControl())
			{
				if (_selectedWidget == null) { return; }
				WidgetOperationArgs args = GetOperationArgs();
				_selectedWidget.OnCopy(args);

				tsPaste.Enabled = true;
			}
			else
			{
				this.HandleCopy();
			}
		}

		private void tsPaste_Click(object sender, EventArgs e)
		{
			if (this.ContainsActiveControl())
			{
				WidgetOperationArgs args = GetOperationArgs();
				if (_selectedWidget != null && _selectedWidget.OnPaste(args))
				{
					return;
				}
				Data.OnPaste(args);
			}
			else
			{
				this.HandlePaste();
			}
		}

		private void tsDuplicate_Click(object sender, EventArgs e)
		{
			if (this.ContainsActiveControl())
			{
				WidgetOperationArgs args = GetOperationArgs();
				_selectedWidget?.OnDuplicate(args);
			}

			//int track = _selectedWidget != null ? _widgets.IndexOf(_selectedWidget) : -1;
			//DuplicateWidgetAction action = new DuplicateWidgetAction(this, Data, _selectedWidget, _time, track);
			//_history?.Commit(action);
		}

		private void tsDelete_Click(object sender, EventArgs e)
		{
			if (this.ContainsActiveControl())
			{
				if (_selectedWidget == null) { return; }
				WidgetOperationArgs args = GetOperationArgs();
				_selectedWidget.OnDelete(args);
			}
			else
			{
				this.HandleDelete();
			}
		}

		private void panel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				tsPlay_Click(sender, EventArgs.Empty);
			}
		}

		private void UpdateTooltip()
		{
			Tuple<int, int, bool> tuple = tooltip.Tag as Tuple<int, int, bool>;
			if (tuple == null) { return; }
			int x = tuple.Item1;
			int y = tuple.Item2;
			bool inHeader = tuple.Item3;
			ITimelineWidget widget = inHeader ? GetWidgetUnderCursorInHeader(y) : GetWidgetUnderCursor(y);
			if (widget != null)
			{
				int track;
				int row = YToRow(y, out track);
				int iconIndex = GetHeaderIconIndex(x, widget, row);
				string text = widget.GetHeaderTooltip(GetWidgetArgs(_time, y), iconIndex);
				tooltip.SetToolTip(panelHeader, text);
			}
			else
			{
				tooltip.SetToolTip(panelHeader, null);
			}
		}

		private int GetHeaderIconIndex(int x, ITimelineWidget widget, int row)
		{
			int iconCount = widget.GetHeaderIconCount(row);
			int iconIndex = -1;
			if (iconCount > 0)
			{
				int iconStart = panelHeader.Width - IconPadding * iconCount - IconSize * iconCount;
				if (x >= iconStart)
				{
					iconIndex = iconCount - 1 - (x - iconStart) / IconSize;
				}
			}

			return iconIndex;
		}

		private void tmrTooltip_Tick(object sender, EventArgs e)
		{
			UpdateTooltip();
		}

		public void ShowContextMenu(params ContextMenuItem[] items)
		{
			tooltip.Hide(panelHeader);
			contextMenu.Items.Clear();
			foreach (ContextMenuItem item in items)
			{
				ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Text, item.Icon, item.Click);
				menuItem.Checked = item.Checked;
				menuItem.Tag = item.Tag;
				contextMenu.Items.Add(menuItem);
			}
			contextMenu.Show(panelHeader, new Point(_headerX, _headerY));
		}

		private void tsPrevious_Click(object sender, EventArgs e)
		{
			_selectedWidget?.AdvanceSubWidget(false);
		}

		private void tsNext_Click(object sender, EventArgs e)
		{
			_selectedWidget?.AdvanceSubWidget(true);
		}

		private void ZoomOut()
		{
			SetZoom(_zoom - 0.2f);
		}

		private void ZoomIn()
		{
			SetZoom(_zoom + 0.2f);
		}

		private void AutoZoom()
		{
			_zoom = 1;
			float duration = Duration;
			float visibleTime = XToTime(container.Width);
			if (duration > visibleTime)
			{
				SetZoom(0.5f);
			}
			else
			{
				SetZoom(1);
			}
		}

		private void SetZoom(float level)
		{
			_zoom = Math.Max(MinZoom, Math.Min(MaxZoom, level));
			if (_zoom <= 0.5f)
			{
				_tickResolution = 0.25f;
			}
			else if (_zoom > 1.25f)
			{
				_tickResolution = 0.05f;
			}
			else
			{
				_tickResolution = 0.125f;
			}

			MajorTickFrequency = (int)Math.Round(1 / _tickResolution);
			ResizeTimeline();
		}

		private void tsZoomOut_Click(object sender, EventArgs e)
		{
			ZoomOut();
		}

		private void tsZoomIn_Click(object sender, EventArgs e)
		{
			ZoomIn();
		}

		private List<ToolStripMenuItem> _contextMenuItems = new List<ToolStripMenuItem>();
		private void editMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach (ToolStripMenuItem item in _contextMenuItems)
			{
				editMenu.Items.Remove(item);
			}
			_contextMenuItems.Clear();
			ContextMenuArgs args = new ContextMenuArgs();
			_selectedWidget?.OnOpeningContextMenu(args);
			foreach (ContextMenuItem item in args.ItemsToAdd)
			{
				ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Text, item.Icon, item.Click);
				menuItem.Checked = item.Checked;
				menuItem.Tag = item.Tag;
				editMenu.Items.Add(menuItem);
				_contextMenuItems.Add(menuItem);
			}
		}

		private void playOnceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_playbackMode = PlaybackMode.Once;
			EnablePlayback(true);
		}

		private void playLoopingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_playbackMode = PlaybackMode.Looping;
			EnablePlayback(true);
		}

		private void playOnceWithRepeatsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_playbackMode = PlaybackMode.OnceLooping;
			EnablePlayback(true);
		}

		private enum PlaybackMode
		{
			Once,
			Looping,
			OnceLooping,
		}
	}

	public class ContextMenuItem
	{
		public Image Icon;
		public string Text;
		public EventHandler Click;
		public bool Checked;
		public object Tag;

		public ContextMenuItem(string text, Image icon, EventHandler click, object tag, bool selected)
		{
			Text = text;
			Icon = icon;
			Click = click;
			Checked = selected;
			Tag = tag;
		}
	}

	public class ContextMenuArgs
	{
		public List<ContextMenuItem> ItemsToAdd = new List<ContextMenuItem>();
	}

	public class TimelineCommand : ICommand
	{
		private Action _do;
		private Action _undo;

		public TimelineCommand(Action doAction, Action undoAction)
		{
			_do = doAction;
			_undo = undoAction;
		}

		public void Do()
		{
			_do();
		}

		public void Undo()
		{
			_undo();
		}
	}

	public class TimelineDragAction : ITimelineAction
	{
		private Timeline _timeline;
		private float _currentTime;

		public TimelineDragAction(Timeline timeline)
		{
			_timeline = timeline;
		}

		public Cursor GetCursor()
		{
			return Cursors.IBeam;
		}

		public Cursor GetHoverCursor()
		{
			return Cursors.Default;
		}

		public void Start(WidgetActionArgs args)
		{
			Update(args);
		}

		public void Update(WidgetActionArgs args)
		{
			_currentTime = args.Time;
			_currentTime = (float)Math.Round(_currentTime * 100) / 100;
			_timeline.CurrentTime = _currentTime;
		}

		public void Finish()
		{
			_timeline.FinalizeTimeMovement();
		}
	}
}

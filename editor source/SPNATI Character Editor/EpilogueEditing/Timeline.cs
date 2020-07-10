using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.Actions;
using Desktop.Skinning;
using SPNATI_Character_Editor.Actions.TimelineActions;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public partial class Timeline : UserControl, ISkinControl
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
		private SolidBrush _trackFill;
		private SolidBrush _trackFillAlternate;
		private SolidBrush _trackFillSelected;
		private SolidBrush _timelineBack;
		private SolidBrush _widgetHeaderFill;
		private SolidBrush _accentFill;
		private Pen _penTickMajor;
		private Pen _penTick;
		private Pen _penTickMinor;
		private Pen _trackBorder;
		private Pen _trackRowBorder;
		private Font _fontTimeline;
		private SolidBrush _timelineFore;
		private SolidBrush _widgetTitleFore;
		private StringFormat _rowHeaderFormat;
		private SolidBrush _widgetSelectedFill;
		public static Pen WidgetOutline;
		public static Pen WidgetSelectedOutline;
		private Pen _timeLineAxis;
		private Pen _timeLine;
		private Pen _playbackLineAxis;
		private Pen _playbackLine;
		private SolidBrush _iconHoverFill;

		private float _zoom = 1;
		private ITimelineAction _pendingAction = null;
		private ITimelineObject _pendingObject = null;
		private ITimelineAction _currentAction = null;
		private DateTime _lastTick;
		private ITimelineWidget _headerHoverWidget = null;
		private int _headerHoverRow = -1;
		private int _headerHoverIconIndex;

		private int _headerX;
		private int _headerY;
		private DateTime _lastClick = DateTime.Now;

		private List<ITimelineWidget> _widgets = new List<ITimelineWidget>();
		private ITimelineObject _selectedObject;
		private List<ITimelineBreak> _breaks = new List<ITimelineBreak>();

		private PlaybackMode _playbackMode = PlaybackMode.Once;
		public bool PlaybackAwaitingInput { get; set; }
		public bool PauseOnBreaks { get; set; }

		public event EventHandler<ITimelineObject> WidgetSelected;
		public event EventHandler<float> TimeChanged;
		public event EventHandler<float> PlaybackTimeChanged;
		public event EventHandler<float> ElapsedTimeChanged;
		public event EventHandler<DataSelectionArgs> DataSelected;
		public event EventHandler<bool> PlaybackChanged;
		public event EventHandler<object> UIRequested;

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

		public ITimelineObject SelectedObject
		{
			get { return _selectedObject; }
		}

		private float _time = 0;
		public float CurrentTime
		{
			get { return _time; }
			set
			{
				UpdateMarker(value);
			}
		}

		private float _playbackTime = 0;
		public float PlaybackTime
		{
			get { return _playbackTime; }
			set
			{
				if (_playbackTime == value) { return; }
				ITimelineBreak brk = GetBreakBetween(_playbackTime, value);
				if (brk != null && PauseOnBreaks)
				{
					value = brk.Time - 0.001f;
					PlaybackAwaitingInput = true;
				}
				foreach (LiveEvent evt in GetEventsBetween(_playbackTime, value))
				{
					evt.Trigger();
				}
				_playbackTime = value;
				Redraw();
				PlaybackTimeChanged?.Invoke(this, value);
			}
		}

		private float _elapsedTime = 0;
		public float ElapsedTime
		{
			get { return _elapsedTime; }
			set
			{
				if (_elapsedTime == value) { return; }
				_elapsedTime = value;
				ElapsedTimeChanged?.Invoke(this, value);
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
			_timelineBack = new SolidBrush(Color.FromArgb(230, 230, 230));
			_widgetHeaderFill = new SolidBrush(Color.FromArgb(185, 185, 185));
			_accentFill = new SolidBrush(Color.Black);
			_fontTimeline = new Font("Arial", 8);
			_timelineFore = new SolidBrush(Color.Black);
			_penTickMajor = new Pen(Color.FromArgb(0, 0, 0));
			_penTick = new Pen(Color.FromArgb(130, 130, 130));
			_penTickMinor = new Pen(Color.FromArgb(170, 170, 170));
			_trackRowBorder = new Pen(Color.FromArgb(170, 170, 170));
			_trackBorder = new Pen(Color.FromArgb(150, 150, 150));
			_rowHeaderFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.LineLimit };
			_widgetSelectedFill = new SolidBrush(Color.White);
			WidgetOutline = new Pen(Color.Black);
			WidgetSelectedOutline = new Pen(Color.White);
			_widgetTitleFore = new SolidBrush(Color.Black);
			_timeLineAxis = new Pen(Color.Red, 3);
			_timeLine = new Pen(Color.Red, 1);
			_playbackLineAxis = new Pen(Color.DarkGreen, 3);
			_playbackLine = new Pen(Color.DarkGreen, 1);
			_iconHoverFill = new SolidBrush(Color.FromArgb(50, 0, 0, 0));

			container.MouseWheel += Panel_MouseWheel;

			UpdateMarker(0);
			OnUpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		private void SetDefaultColors()
		{
			_trackFill.Color = Color.FromArgb(200, 200, 200);
			_trackFillAlternate.Color = Color.FromArgb(210, 210, 210);
			_trackFillSelected.Color = Color.FromArgb(230, 230, 255);
			_timelineBack.Color = Color.FromArgb(230, 230, 230);
			_widgetHeaderFill.Color = Color.FromArgb(185, 185, 185);
			_timelineFore.Color = Color.Black;
			_penTickMajor.Color = Color.FromArgb(0, 0, 0);
			_penTick.Color = Color.FromArgb(130, 130, 130);
			_penTickMinor.Color = Color.FromArgb(170, 170, 170);
			_trackRowBorder.Color = Color.FromArgb(170, 170, 170);
			_trackBorder.Color = Color.FromArgb(150, 150, 150);
			_widgetSelectedFill.Color = Color.White;
			WidgetOutline.Color = Color.Black;
			WidgetSelectedOutline.Color = Color.White;
			_widgetTitleFore.Color = Color.Black;
			_timeLine.Color = _timeLineAxis.Color = Color.Red;
			_playbackLine.Color = _playbackLineAxis.Color = Color.DarkGreen;
			_iconHoverFill.Color = Color.FromArgb(50, 0, 0, 0);
		}

		public void OnUpdateSkin(Skin skin)
		{
			_timelineBack.Color = skin.Surface.Normal;
			_timelineFore.Color = skin.Surface.ForeColor;
			if (!skin.AppColors.ContainsKey("TimelineFore"))
			{
				SetDefaultColors();
			}
			else
			{
				_trackFill.Color = skin.GetAppColor("TimelineRow");
				_trackFillAlternate.Color = skin.GetAppColor("TimelineRowAlt");
				_trackFillSelected.Color = skin.GetAppColor("TimelineSelected");
				_widgetTitleFore.Color = skin.GetAppColor("TimelineFore");
				_widgetSelectedFill.Color = skin.GetAppColor("WidgetTitleSelected");
				_widgetHeaderFill.Color = skin.GetAppColor("WidgetTitle");
				WidgetSelectedOutline.Color = skin.Group == "Dark" ? Color.Black : Color.White;
				WidgetOutline.Color = skin.GetAppColor("WidgetBorder");
				_playbackLine.Color = _playbackLineAxis.Color = skin.GetAppColor("PlaybackBar");
				_timeLine.Color = _timeLineAxis.Color = skin.GetAppColor("TimelineBar");
				_iconHoverFill.Color = skin.PrimaryColor.Hover;

				_penTickMajor.Color = ColorSet.BlendColor(_timelineBack.Color, _timelineFore.Color, 0.5f);
				_penTickMinor.Color = ColorSet.BlendColor(_timelineBack.Color, _timelineFore.Color, 0.3f);
				_trackBorder.Color = skin.GetAppColor("TimelineRowBorder");
				_trackRowBorder.Color = skin.GetAppColor("TimelineSubRowBorder");
			}

			foreach (ITimelineWidget widget in _widgets)
			{
				widget.UpdateSkin(skin);
			}
			foreach (ITimelineBreak b in _breaks)
			{
				b.UpdateSkin(skin);
			}
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
			if (_selectedObject != null)
			{
				if (!_widgets.Contains(_selectedObject))
				{
					SelectObject(null);
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
			_selectedObject?.UpdateSelection(args);
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
			foreach (ITimelineWidget widget in data.CreateWidgets(this))
			{
				AddObject(widget, _widgets.Count);
			}
			foreach (ITimelineBreak timeBreak in data.CreateBreaks(this))
			{
				AddBreak(timeBreak);
			}
			ResizeTimeline();
			UpdateMarker(0);
			SelectObject(null);
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
			_breaks.Clear();
		}

		private void Data_WidgetMoved(object sender, WidgetCreationArgs args)
		{
			MoveObject(args.Widget, args.Index);
		}

		private void Data_WidgetRemoved(object sender, WidgetCreationArgs args)
		{
			RemoveObject(args.Widget);
		}

		private void Data_WidgetCreated(object sender, WidgetCreationArgs args)
		{
			AddObject(args.Widget, args.Index);
		}

		public ITimelineWidget CreateWidget(object context)
		{
			//TODO: This needs to be a command to be undoable
			if (Data == null) { return null; }
			ITimelineWidget widget = Data.CreateWidget(this, _time, context);
			AddObject(widget, _widgets.Count);
			return widget;
		}

		public void CreateWidget(object data, int index)
		{
			//TODO: This needs to be a command to be undoable
			if (Data == null) { return; }
			ITimelineWidget widget = Data.CreateWidget(this, _time, data, index);
			AddObject(widget, index);
			SelectObject(widget);
			SelectData(data);
		}

		public void RemoveSelectedWidget()
		{
			if (_selectedObject == null) { return; }
			DeleteWidgetCommand action = new DeleteWidgetCommand(Data, _selectedObject);
			_history?.Commit(action);
		}

		public void AddBreak(ITimelineBreak timeBreak)
		{
			AddObject(timeBreak, -1);
		}

		private void AddObject(ITimelineObject obj, int index)
		{
			ITimelineWidget widget = obj as ITimelineWidget;
			if (widget != null)
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
			}
			else if (obj is ITimelineBreak)
			{
				ITimelineBreak brk = obj as ITimelineBreak;
				_breaks.Add(brk);
			}
			Redraw();
		}

		private void RemoveObject(ITimelineObject obj)
		{
			if (obj is ITimelineWidget)
			{
				ITimelineWidget widget = obj as ITimelineWidget;
				_widgets.Remove(widget);
				widget.Invalidated -= Widget_Invalidated;
				ResizeTimeline();
			}
			else if (obj is ITimelineBreak)
			{
				_breaks.Remove(obj as ITimelineBreak);
			}
			Redraw();
		}

		private void MoveObject(ITimelineObject obj, int index)
		{
			if (obj is ITimelineWidget)
			{
				ITimelineWidget widget = obj as ITimelineWidget;
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
			else if (obj is ITimelineBreak)
			{
				ITimelineBreak brk = obj as ITimelineBreak;
				_breaks.RemoveAt(index);
				if (index >= _breaks.Count || index == -1)
				{
					_breaks.Add(brk);
				}
				else
				{
					_breaks.Insert(index, brk);
				}
			}
		}

		private void tmrTick_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			float duration = Duration;
			TimeSpan elapsed = now - _lastTick;
			float elapsedSec = (float)elapsed.TotalSeconds;
			_lastTick = now;

			if (!PlaybackAwaitingInput)
			{
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
				PlaybackTime = time;
			}

			float elapsedTime = _elapsedTime + elapsedSec;
			ElapsedTime = elapsedTime;

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
			g.FillRectangle(_timelineBack, 0, 0, panelAxis.Width, height);
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
					g.DrawString(time.ToString("0.00", _timeFormatter), _fontTimeline, _timelineFore, x + 1, 0);
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
					if (widget == _selectedObject)
					{
						g.FillRectangle(_trackFillSelected, 0, trackY + 1, panelHeader.Width, rowCount * RowHeight - 1);
					}
					_accentFill.Color = widget.GetAccent();

					for (int row = 0; row < rowCount; row++)
					{
						int rowY = trackY + row * RowHeight;
						if (rowY + RowHeight >= 0 && rowY < panelHeader.Height)
						{
							if (row == 0 || _selectedObject == widget)
							{
								bool highlighted = widget.IsRowHighlighted(row);
								if (row == 0 || highlighted)
								{
									g.FillRectangle(_selectedObject == widget && highlighted ? _widgetSelectedFill : _widgetHeaderFill, 0, rowY + 1, width, RowHeight - 1);
								}
							}
							g.FillRectangle(_accentFill, 0, rowY + 1, 3, RowHeight - 1);
							string label = widget.GetLabel(row);
							if (row == 0 && widget.IsCollapsible)
							{
								g.DrawImage(widget.IsCollapsed ? Properties.Resources.Expand : Properties.Resources.Collapse, 2, rowY + RowHeight / 2 - Properties.Resources.Expand.Height / 2);
							}
							int left = Properties.Resources.Expand.Width;
							Image thumbnail = widget.GetThumbnail();
							if (thumbnail != null && row == 0)
							{
								g.DrawImage(thumbnail, new Rectangle(left + 2, rowY + 1, RowHeight - 2, RowHeight - 2), new Rectangle(0, 0, thumbnail.Width, thumbnail.Height), GraphicsUnit.Pixel);
								left += RowHeight;
							}
							int headerIconCount = widget.GetHeaderIconCount(row);
							int iconLeft = panelHeader.Width - IconPadding - IconSize;

							g.DrawString(label, _fontTimeline, _widgetTitleFore, new Rectangle(left, rowY, width - left - (panelHeader.Width - iconLeft), RowHeight), _rowHeaderFormat);

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

							g.DrawLine(_trackRowBorder, 0, rowY + RowHeight, width, rowY + RowHeight);
						}

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
				g.DrawLine(_trackBorder, 0, y, panelHeader.Width, y);
			}
			g.DrawLine(_trackBorder, panelHeader.Width - 1, 0, panelHeader.Width - 1, panelHeader.Height);
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.FillRectangle(_trackFill, 0, 0, panel.Width, panel.Height);
			int scrollOffset = -container.VerticalScroll.Value;
			float duration = Duration;
			int panelHeight = panel.Height;

			float pps = PixelsPerSecond * _zoom;

			//widget backgrounds
			int y = 0;
			for (int i = 0; i < _widgets.Count; i++)
			{
				ITimelineWidget widget = _widgets[i];
				int trackHeight = RowHeight;
				int rowCount = 1;
				if (widget != null)
				{
					rowCount = widget.GetRowCount();
					trackHeight = rowCount * RowHeight;
				}

				if (y + scrollOffset + trackHeight >= 0 && y + scrollOffset < panelHeight)
				{
					if (_selectedObject == widget)
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
						g.DrawLine(r < rowCount - 1 ? _trackRowBorder : _trackBorder, 0, rowY, panel.Width, rowY);
					}
					DrawTickMarks(g, y, trackHeight);
				}

				if (widget != null)
				{
					y += rowCount * RowHeight;
				}
				else
				{
					y += RowHeight;
				}
			}

			//break backgrounds
			for (int i = 0; i < _breaks.Count; i++)
			{
				ITimelineBreak brk = _breaks[i];
				brk.DrawBackground(g, pps, panel.Height, brk == _selectedObject);
			}

			//widgets
			y = 0;
			for (int i = 0; i < _widgets.Count; i++)
			{
				ITimelineWidget widget = _widgets[i];
				int rowCount = 1;
				if (widget != null)
				{
					rowCount = widget.GetRowCount();
				}
				int trackY = y;

				if (widget != null)
				{
					int count = rowCount;
					float start = widget.GetStart();
					int x = TimeToX(start);

					//contents
					for (int row = 0; row < count; row++)
					{
						int rowY = trackY + row * RowHeight;
						if (rowY + RowHeight + scrollOffset >= 0 && rowY + scrollOffset < panelHeight)
						{
							widget.DrawContents(g, row, x + 1, rowY + 1, pps, RowHeight - 2, duration);
						}
					}

					y += rowCount * RowHeight;
				}
				else
				{
					y += RowHeight;
				}
			}

			//breaks
			for (int i = 0; i < _breaks.Count; i++)
			{
				ITimelineBreak brk = _breaks[i];
				brk.Draw(g, pps, panel.Height, brk == _selectedObject);
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
				Pen canvasPen = _trackRowBorder;
				if (major)
				{
					canvasPen = _trackBorder;
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
			if (e.Button == MouseButtons.Left)
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
							bool doubleClick = false;
							DateTime clickTime = DateTime.Now;
							if (_selectedObject == widget && (clickTime - _lastClick).TotalMilliseconds < 200)
							{
								doubleClick = true;
							}
							_lastClick = clickTime;

							SelectObject(widget);

							int track;
							int r = YToRow(e.Y + startY, out track);
							int icon = GetHeaderIconIndex(e.X, widget, r);
							WidgetActionArgs args = GetWidgetArgs(_time, e.Y + startY);
							if (icon >= 0)
							{
								widget.OnClickHeaderIcon(args, icon);
							}
							else
							{
								if (doubleClick)
								{
									widget.OnDoubleClickHeader(args);
								}
								else
								{
									widget.OnClickHeader(args);
								}
							}
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
				ITimelineBreak brk = GetBreakUnderCursor(e.X);
				ITimelineWidget widget = GetWidgetUnderCursor(e.Y);
				ITimelineObject hoverObj = null;
				ITimelineAction hoverAction = null;
				ITimelineAction brkAction = GetAction(brk, e.X, e.Y);
				ITimelineAction widgetAction = GetAction(widget, e.X, e.Y);
				if (brk == null || (_selectedObject != brk && widgetAction != null && !(widgetAction is SelectAction) && !(widgetAction is MoveWidgetTimelineAction)))
				{
					hoverObj = widget;
					hoverAction = widgetAction;
				}
				else
				{
					hoverObj = brk;
					hoverAction = brkAction;
				}
				if (hoverObj != _pendingObject && _pendingObject != null)
				{
					_pendingObject.OnMouseOut();
				}
				if (hoverObj != null)
				{
					_pendingAction = hoverAction;
					if (_pendingAction != null || old != null)
					{
						_pendingObject = hoverObj;
						panel.Invalidate();
					}
					Cursor = (_pendingAction != null ? _pendingAction.GetHoverCursor() : Cursors.Default);
				}
				else
				{
					_pendingAction = null;
					Cursor = Cursors.Default;
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
				ITimelineObject widget = null; // GetBreakUnderCursor(e.X);
				ITimelineWidget overWidget = GetWidgetUnderCursor(e.Y);
				if (widget == null || _pendingObject is ITimelineWidget)
				{
					widget = overWidget;
				}

				bool doubleClick = false;
				DateTime clickTime = DateTime.Now;
				if (_selectedObject == widget && (clickTime - _lastClick).TotalMilliseconds < 200)
				{
					doubleClick = true;
				}
				_lastClick = clickTime;

				if (_selectedObject != widget)
				{
					SelectObject(widget);
				}

				if (widget != null)
				{
					if (doubleClick)
					{
						widget.OnDoubleClick(GetWidgetArgs(CurrentTime, e.Y));
					}
					else if (_pendingAction != null)
					{
						_currentAction = _pendingAction;
						_pendingAction = null;
						StartAction(XToTime(e.X), e.Y);
					}
				}
			}
		}

		private void panel_MouseUp(object sender, MouseEventArgs e)
		{
			EndAction();
		}

		public void SelectObject(ITimelineObject widget)
		{
			ApplyObjectSelection(widget);
		}

		public void ApplyObjectSelection(ITimelineObject widget)
		{
			WidgetSelectionArgs args = new WidgetSelectionArgs(this, SelectionType.Select, ModifierKeys);
			if (widget != _selectedObject)
			{
				if (_selectedObject != null)
				{
					args.IsSelected = SelectionType.Deselect;
					_selectedObject.OnWidgetSelectionChanged(args);
				}
				_selectedObject = widget;
				args.IsSelected = SelectionType.Select;
			}
			else
			{
				args.IsSelected = SelectionType.Reselect;
			}
			if (_selectedObject != null)
			{
				_selectedObject.OnWidgetSelectionChanged(args);
				tsNext.Enabled = true;
				tsPrevious.Enabled = true;
			}
			else
			{
				Data?.UpdateSelection(args);
				tsNext.Enabled = false;
				tsPrevious.Enabled = false;
			}
			UpdateButtons(args);

			if (args.IsSelected == SelectionType.Select)
			{
				WidgetSelected?.Invoke(this, _selectedObject);
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
				SelectObject(widget);
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

		private ITimelineBreak GetBreakUnderCursor(int x)
		{
			for (int i = _breaks.Count - 1; i >= 0; i--)
			{
				ITimelineBreak brk = _breaks[i];
				int brkX = TimeToX(brk.Time);
				if (Math.Abs(brkX - x) <= 5)
				{
					return brk;
				}
			}
			return null;
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
				Widget = _selectedObject,
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
				if (_selectedObject != null)
				{
					WidgetSelectionArgs args = new WidgetSelectionArgs(this, SelectionType.Select, ModifierKeys);
					_selectedObject.UpdateSelection(args);
					UpdateButtons(args);
				}
			}
		}

		private void panelAxis_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				//placing the time marker
				panel.Select();
				_currentAction = new TimelineDragAction(this);
				float time = XToTime(e.X + container.HorizontalScroll.Value);
				float inverse = 1 / _tickResolution * 2;
				time = (float)Math.Round(Math.Round(time * inverse) / inverse, 3);
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
					panelAxis.Update();
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
			if (_selectedObject != null)
			{
				_selectedObject.OnTimeChanged(GetOperationArgs());
			}
		}

		/// <summary>
		/// Gets what action a mousedown will perform when clicking the target point
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private ITimelineAction GetAction(ITimelineObject widget, int x, int y)
		{
			float pps = PixelsPerSecond * _zoom;
			if (widget != null)
			{
				float start = widget.GetStart();
				int track;
				int row = YToRow(y, out track);
				return widget.GetAction(x - TimeToX(start), XToTime(x), row, TimeToX(Duration), pps);
			}
			return _selectedObject != null ? null : new SelectAction();
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
			if (ModifierKeys.HasFlag(Keys.Control)) { return; }
			EnablePlayback(!tmrTick.Enabled);
		}

		public void ResumePlayback()
		{
			if (tmrTick.Enabled)
			{
				if (!PlaybackAwaitingInput)
				{
					//jump to the next break if there is one
					for (int i = 0; i < _breaks.Count; i++)
					{
						if (_breaks[i].Time > _playbackTime + 0.001f)
						{
							_playbackTime = _breaks[i].Time;
							break;
						}
					}
				}
				PlaybackAwaitingInput = false;
				_playbackTime += 0.001f;
			}
		}

		public void EnablePlayback(bool enabled)
		{
			if (tmrTick.Enabled == enabled)
			{
				return;
			}
			if (enabled)
			{
				PlaybackAwaitingInput = false;
				_playbackTime = CurrentTime - 0.01f;
				PlaybackTime = (CurrentTime < Duration ? CurrentTime : 0);
				ElapsedTime = 0;
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
			ElapsedTime = 0;
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
				if (_selectedObject == null) { return; }
				WidgetOperationArgs args = GetOperationArgs();
				args.IsSilent = true;
				_selectedObject.OnCopy(args);
				_selectedObject.OnDelete(args);
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
				if (_selectedObject == null) { return; }
				WidgetOperationArgs args = GetOperationArgs();
				_selectedObject.OnCopy(args);

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
				if (_selectedObject != null && _selectedObject.OnPaste(args))
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
				_selectedObject?.OnDuplicate(args);
			}
		}

		private void tsDelete_Click(object sender, EventArgs e)
		{
			if (this.ContainsActiveControl())
			{
				if (_selectedObject == null) { return; }
				WidgetOperationArgs args = GetOperationArgs();
				_selectedObject.OnDelete(args);
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
			_selectedObject?.AdvanceSubWidget(false);
		}

		private void tsNext_Click(object sender, EventArgs e)
		{
			_selectedObject?.AdvanceSubWidget(true);
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
			_selectedObject?.OnOpeningContextMenu(args);
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

		public void RequestUI(object data)
		{
			UIRequested?.Invoke(this, data);
		}

		private ITimelineBreak GetBreakBetween(float start, float end)
		{
			for (int i = 0; i < _breaks.Count; i++)
			{
				float time = _breaks[i].Time;
				if (time > start && time <= end)
				{
					return _breaks[i];
				}
			}
			return null;
		}

		private IEnumerable<LiveEvent> GetEventsBetween(float start, float end)
		{
			for (int i = 0; i < _widgets.Count; i++)
			{
				LiveEvent evt = _widgets[i].GetEventBetween(start, end);
				if (evt != null)
				{
					yield return evt;
				}
			}
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

using System;
using System.Collections.Generic;
using System.Drawing;
using Desktop.Skinning;
using SPNATI_Character_Editor.Actions;
using SPNATI_Character_Editor.Actions.TimelineActions;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.EpilogueEditing.Widgets
{
	public class PauseWidget : ITimelineWidget
	{
		public LivePauser Data;
		private LiveBreak _selectedBreak = null;
		private float _hoverTime = -1;

		private SolidBrush _fillBrush = new SolidBrush(Color.Red);
		private Pen _outer = new Pen(Brushes.Black);
		private Pen _outerSelected = new Pen(Brushes.Black, 3);
		private Pen _inner = new Pen(Brushes.White);

		public HashSet<float> ValidBreaks = new HashSet<float>();

		private bool _showAll;
		public bool ShowAllBreaks
		{
			get { return _showAll; }
			set
			{
				Invalidated?.Invoke(this, EventArgs.Empty);
				_showAll = value;
				ComputePotentialBreaks();
			}
		}

		public PauseWidget(LivePauser data, Timeline timeline)
		{
			Data = data;
			//data.PropertyChanged += Bubble_PropertyChanged;
			UpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public void UpdateSkin(Skin skin)
		{
			_fillBrush.Color = skin.Critical.Normal;
		}

		public bool IsCollapsible { get { return false; } }

		public bool IsCollapsed { get; set; }

		public event EventHandler Invalidated;

		public void AdvanceSubWidget(bool forward)
		{
			return;
		}

		public Color GetAccent()
		{
			return Color.Transparent;
		}

		private Point[] GetShape(int x, int y)
		{
			return new Point[] {
					new Point(x - 2, y),
					new Point(x + 2, y),
					new Point(x + 6, y + 4),
					new Point(x + 6, y + 8),
					new Point(x + 2, y + 12),
					new Point(x - 2, y + 12),
					new Point(x - 6, y + 8),
					new Point(x - 6, y + 4),
					new Point(x - 2, y),
				};
		}

		public void DrawContents(Graphics g, int rowIndex, int x, int y, float pps, int rowHeight, float dataEndTime)
		{
			y += 3;
			if (_showAll)
			{
				foreach (float time in ValidBreaks)
				{
					Point[] pts = GetShape((int)(time * pps), y);
					g.DrawPolygon(_outer, pts);
				}
			}
			foreach (LiveBreak brk in Data.Pauses)
			{
				float time = brk.Time;
				Point[] pts = GetShape((int)(time * pps), y);
				g.FillPolygon(_fillBrush, pts);
				if (brk == _selectedBreak || time == _hoverTime)
				{
					g.DrawPolygon(_outerSelected, pts);
					g.DrawPolygon(_inner, pts);
				}
				else
				{
					g.DrawPolygon(_outer, pts);
				}
			}
		}

		public void DrawHeaderIcon(Graphics g, int rowIndex, int iconIndex, int x, int y, int size, int highlightedIconIndex)
		{
		}

		public void SelectBreak(LiveBreak brk)
		{
			_selectedBreak = brk;
		}

		public ITimelineAction GetAction(int x, float start, int row, int timelineWidth, float pps)
		{
			for (int i = 0; i < Data.Pauses.Count; i++)
			{
				float timeX = Data.Pauses[i].Time * pps;
				if (x >= timeX - 3 && x <= timeX + 3)
				{
					return new MoveBreakTimelineAction(i);
				}
			}
			return null;
		}

		public object GetData()
		{
			return Data;
		}

		public LiveEvent GetEventBetween(float start, float end)
		{
			return null;
		}

		public int GetHeaderIconCount(int row)
		{
			return 0;
		}

		public string GetHeaderTooltip(WidgetActionArgs args, int iconIndex)
		{
			return null;
		}

		public string GetLabel(int row)
		{
			return "Breaks";
		}

		public float GetLength(float duration)
		{
			return duration;
		}

		public int GetRowCount()
		{
			return 1;
		}

		public float GetStart()
		{
			return 0;
		}

		public Image GetThumbnail()
		{
			return null;
		}

		public bool IsRowHighlighted(int row)
		{
			return true;
		}

		public void OnClickHeader(WidgetActionArgs args)
		{
			args.Timeline.SelectData(Data);
		}

		public void OnClickHeaderIcon(WidgetActionArgs args, int iconIndex)
		{
		}

		public bool OnCopy(WidgetOperationArgs args)
		{
			return false;
		}

		public bool OnDelete(WidgetOperationArgs args)
		{
			if (_selectedBreak != null)
			{
				DeleteWidgetCommand command = new DeleteWidgetCommand(Data.Data, _selectedBreak);
				args.History.Commit(command);
				args.Timeline.SelectData(null);

				_selectedBreak = null;
				return true;
			}
			return false;
		}

		public void OnDoubleClick(WidgetActionArgs args)
		{
		}

		public void OnDoubleClickHeader(WidgetActionArgs args)
		{
		}

		public bool OnDuplicate(WidgetOperationArgs args)
		{
			return false;
		}

		public void OnMouseOut()
		{
			_hoverTime = -1;
			Invalidated?.Invoke(this, EventArgs.Empty);
		}

		public void OnOpeningContextMenu(ContextMenuArgs args)
		{
		}

		public bool OnPaste(WidgetOperationArgs args)
		{
			return false;
		}

		public void OnPlaybackChanged(bool playing)
		{
		}

		public void OnStartMove(WidgetActionArgs args)
		{
		}

		public void OnTimeChanged(WidgetOperationArgs args)
		{
		}

		public void OnWidgetSelectionChanged(WidgetSelectionArgs args)
		{
			if (args.IsSelected == SelectionType.Deselect)
			{
				_selectedBreak = null;
			}
		}

		public void SetStart(float time)
		{
		}

		public void UpdateSelection(WidgetSelectionArgs args)
		{
			args.AllowDelete = true;
		}

		/// <summary>
		/// Based on the current timeline, determines every place a break can fit
		/// </summary>
		private void ComputePotentialBreaks()
		{
			ValidBreaks = Data.GetValidBreaks();
		}

		public override string ToString()
		{
			return "Pause";
		}
	}
}

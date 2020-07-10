using System;
using System.Drawing;
using System.Windows.Forms;
using Desktop;
using Desktop.Skinning;
using SPNATI_Character_Editor.Actions;
using SPNATI_Character_Editor.Actions.TimelineActions;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class TextWidget : ITimelineWidget
	{
		public LiveBubble Data;

		private static SolidBrush _fillBrush;
		private static SolidBrush _fillBrushExtra;
		private static SolidBrush _accentBrush = new SolidBrush(Color.Blue);
		private static Color _accentColor;
		private bool _selected;

		public bool IsCollapsible { get { return false; } }

		public bool IsCollapsed { get; set; }

		public event EventHandler Invalidated;

		static TextWidget()
		{
			_fillBrush = new SolidBrush(Color.Gray);
			_fillBrushExtra = new SolidBrush(Color.Gray);
			SetSkin(SkinManager.Instance.CurrentSkin);
		}
		private static void SetSkin(Skin skin)
		{
			if (!skin.AppColors.ContainsKey("WidgetHeaderRow"))
			{
				SetDefaultColors();
			}
			else
			{
				_fillBrush.Color = skin.GetAppColor("WidgetHeaderRow");
				_accentColor = skin.GetAppColor("TextAccent");
			}
			_fillBrushExtra.Color = Color.FromArgb(100,  _fillBrush.Color.R, _fillBrush.Color.G, _fillBrush.Color.B);
			_accentBrush.Color = _accentColor;
		}
		private static void SetDefaultColors()
		{
			_fillBrush.Color = Color.FromArgb(255, 226, 66);
			_accentColor = Color.Orange;
		}

		public TextWidget(LiveBubble data, Timeline timeline)
		{
			Data = data;
			data.PropertyChanged += Bubble_PropertyChanged;
		}

		public Color GetAccent()
		{
			return _accentColor;
		}

		private void Bubble_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Invalidate();
		}

		private void Invalidate()
		{
			Invalidated?.Invoke(this, EventArgs.Empty);
		}

		public override string ToString()
		{
			return $"Text: {Data.ToString()}";
		}

		public void DrawContents(Graphics g, int rowIndex, int x, int y, float pps, int rowHeight, float dataEndTime)
		{
			float startX = Data.Start * pps;
			float length = Data.Length * pps;

			g.FillRectangle(_fillBrush, startX, y, length, rowHeight + 1);
			g.FillRectangle(_accentBrush, startX, y, length, 2);
			g.DrawLine(Timeline.WidgetOutline, startX, y, startX, y + rowHeight);
			g.DrawLine(Timeline.WidgetOutline, startX + length, y, startX + length, y + rowHeight);
			if (Data.LinkedToEnd && dataEndTime > 0)
			{
				startX = startX + length + 1;
				length = dataEndTime * pps - startX;
				g.FillRectangle(_fillBrushExtra, startX, y + 6, length, rowHeight - 11);
				g.DrawRectangle(Timeline.WidgetOutline, startX - 1, y + 6, length + 1, rowHeight - 12);
			}
		}

		public string GetLabel(int row)
		{
			return Data.Text ?? "Text Bubble";
		}

		public int GetRowCount()
		{
			return 1;
		}

		public Image GetThumbnail()
		{
			return Properties.Resources.SpeechBubble;
		}

		public float GetLength(float duration)
		{
			if (duration != 0)
			{
				return duration - GetStart();
			}
			else
			{
				return Data.Length;
			}
		}

		public bool IsRowHighlighted(int row)
		{
			return true;
		}

		public void OnClickHeader(WidgetActionArgs args)
		{
			args.Timeline.SelectData(Data);
		}

		public void OnDoubleClickHeader(WidgetActionArgs args)
		{

		}

		public void OnClickHeaderIcon(WidgetActionArgs args, int iconIndex)
		{
			switch (iconIndex)
			{
				case 0:
					Data.Hidden = !Data.Hidden;
					break;
				case 1:
					Data.LinkedToEnd = !Data.LinkedToEnd;
					break;
			}
		}

		public string GetHeaderTooltip(WidgetActionArgs args, int iconIndex)
		{
			switch (iconIndex)
			{
				case 0:
					return "Toggle looping";
				case 1:
					return "Toggle fixed length";
			}
			return null;
		}

		public void OnPlaybackChanged(bool playing)
		{

		}

		public int GetHeaderIconCount(int row)
		{
			return 2;
		}

		public void DrawHeaderIcon(Graphics g, int rowIndex, int iconIndex, int x, int y, int size, int highlightedIconIndex)
		{
			Image icon = null;
			switch (iconIndex)
			{
				case 0:
					icon = Data.Hidden ? Properties.Resources.EyeClosed : Properties.Resources.EyeOpen;
					break;
				case 1:
					if (Data.LinkedToEnd)
					{
						icon = Properties.Resources.LinkToEndFill;
					}
					else
					{
						icon = Properties.Resources.LinkToEnd;
					}
					break;
			}
			if (icon != null)
			{
				g.DrawImage(icon, x, y, size, size);
			}
		}

		public LiveEvent GetEventBetween(float start, float end)
		{
			return null;
		}

		public object GetData()
		{
			return Data;
		}

		public float GetStart()
		{
			return Data.Start;
		}
		public void SetStart(float time)
		{
			Data.Start = time;
		}

		public void OnWidgetSelectionChanged(WidgetSelectionArgs args)
		{
			_selected = (args.IsSelected != SelectionType.Deselect);
			if (_selected)
			{
				args.Timeline.SelectData(Data);
			}
		}

		public void UpdateSkin(Skin skin)
		{
			SetSkin(skin);
		}

		public ITimelineAction GetAction(int x, float start, int row, int timelineWidth, float pps)
		{
			float end = start + Data.Length * pps;
			if (x > end - 5 && x <= end + 5)
			{
				return new ModifyWidgetLengthTimelineAction();
			}
			else if (x >= 5 && x <= end - 5)
			{
				return new MoveWidgetTimelineAction(true);
			}
			return null;
		}

		public void UpdateSelection(WidgetSelectionArgs args)
		{
			args.AllowCut = true;
			args.AllowCopy = true;
			args.AllowDelete = true;
			args.AllowDuplicate = true;
		}

		public bool OnCopy(WidgetOperationArgs args)
		{
			LiveBubble sprite = Data.Copy() as LiveBubble;
			Clipboards.Set<KeyframedWidget>(sprite);
			return true;
		}

		public bool OnDelete(WidgetOperationArgs args)
		{
			if (args.IsSilent || MessageBox.Show($"Are you sure you want to completely remove {ToString()}?", "Remove Sprite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				DeleteWidgetCommand command = new DeleteWidgetCommand(Data.Data, this);
				args.History.Commit(command);
				args.Timeline.SelectData(null);
				return true;
			}
			return false;
		}

		public bool OnPaste(WidgetOperationArgs args)
		{
			object clipboardData = Clipboards.Get<KeyframedWidget, object>();
			if (clipboardData is LiveBubble)
			{
				return Data.Data.Paste(args, Data);
			}
			return true;
		}

		public bool OnDuplicate(WidgetOperationArgs args)
		{
			object clipboardData = Clipboards.Get<KeyframedWidget, object>();
			OnCopy(args);
			OnPaste(args);
			Clipboards.Set<KeyframedWidget>(clipboardData);
			return false;
		}

		public void OnOpeningContextMenu(ContextMenuArgs args)
		{

		}

		public void AdvanceSubWidget(bool forward)
		{
			return;
		}

		public void OnTimeChanged(WidgetOperationArgs args)
		{
		}

		public void OnDoubleClick(WidgetActionArgs args)
		{
		}

		public void OnStartMove(WidgetActionArgs args)
		{

		}

		public void OnMouseOut()
		{

		}
	}
}

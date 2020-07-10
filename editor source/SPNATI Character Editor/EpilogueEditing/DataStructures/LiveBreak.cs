using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Desktop.DataStructures;
using Desktop.Skinning;
using SPNATI_Character_Editor.Actions;
using SPNATI_Character_Editor.Actions.TimelineActions;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public class LiveBreak : BindableObject, ITimelineBreak
	{
		public ITimelineData Data;

		public object GetData()
		{
			return this;
		}

		/// <summary>
		/// Background width in seconds
		/// </summary>
		private const float BackgroundWidth = 0.25f;

		private float _lastWidth;
		private LinearGradientBrush _brushBackground;
		private static Pen _penOuter;
		private static Pen _penInner;
		private static Pen _penSelection;

		static LiveBreak()
		{
			_penSelection = new Pen(Color.White);
			_penOuter = new Pen(Color.White, 3);
			_penInner = new Pen(Color.Black);
		}

		public LiveBreak()
		{
			UpdateSkin(SkinManager.Instance.CurrentSkin);
		}

		public static void UpdateColors(Skin skin)
		{
			_penInner.Color = Color.Gray;
			_penOuter.Color = skin.GetAppColor("BreakGradient");
			_penSelection.Color = skin.GetAppColor("BreakGradientSelected");
		}

		public void UpdateSkin(Skin skin)
		{
			_lastWidth = -1;
			UpdateColors(skin);
		}

		public float Time
		{
			get { return Get<float>(); }
			set { Set(value); _lastWidth = -1; }
		}

		public void DrawBackground(Graphics g, float pps, int height, bool selected)
		{
			float x = Time * pps;
			float width = BackgroundWidth * pps;
			float left = x - width;

			if (width != _lastWidth)
			{
				_brushBackground?.Dispose();
				_brushBackground = new LinearGradientBrush(new PointF(left, 0), new PointF(x, 0), Color.Transparent, Color.FromArgb(200, selected ? _penSelection.Color : _penOuter.Color));
				_lastWidth = width;
			}

			g.FillRectangle(_brushBackground, left, 0, width, height);
		}

		public void Draw(Graphics g, float pps, int height, bool selected)
		{
			//float x = Time * pps;
			//g.DrawLine(_penOuter, x, 0, x, height);
			//g.DrawLine(_penInner, x, 0, x, height);
			//if (selected)
			//{
			//	g.DrawRectangle(_penSelection, x - 2, 0, 4, height - 1);
			//}
		}

		public override string ToString()
		{
			return "Wait for input";
		}

		public float GetStart()
		{
			return Time;
		}

		public void SetStart(float time)
		{
			Time = time;
		}

		public void OnWidgetSelectionChanged(WidgetSelectionArgs args)
		{
			_lastWidth = -1;
			if (args.IsSelected == SelectionType.Select)
			{
				args.Timeline.SelectData(this);
			}
		}

		public ITimelineAction GetAction(int x, float start, int row, int timelineWidth, float pps)
		{
			return null;
		}

		public void UpdateSelection(WidgetSelectionArgs args)
		{
			args.AllowDelete = true;
		}

		public bool OnCopy(WidgetOperationArgs args)
		{
			return false;
		}

		public bool OnDelete(WidgetOperationArgs args)
		{
			if (args.IsSilent || MessageBox.Show($"Are you sure you want to completely remove {ToString()}?", "Remove Break", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				DeleteWidgetCommand command = new DeleteWidgetCommand(Data, this);
				args.History.Commit(command);
				args.Timeline.SelectData(null);
			}
			return true;
		}

		public bool OnPaste(WidgetOperationArgs args)
		{
			return false;
		}

		public bool OnDuplicate(WidgetOperationArgs args)
		{
			return false;
		}

		public void OnOpeningContextMenu(ContextMenuArgs args)
		{

		}

		public void AdvanceSubWidget(bool forward)
		{
			//go to the next break
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

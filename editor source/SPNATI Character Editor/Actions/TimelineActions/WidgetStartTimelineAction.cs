using System;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions.TimelineActions
{
	/// <summary>
	/// Dragging the start position of a widget
	/// </summary>
	public class WidgetStartTimelineAction : ITimelineAction, ICommand
	{
		private ITimelineWidget _widget;
		private float _startMax;
		private float _startMin;
		private int _snap;
		private float _endTime;
		private float _oldDelay;
		private float _oldLength;
		private UndoManager _history;

		public float Delay;

		public Cursor GetHoverCursor()
		{
			return GetCursor();
		}

		public Cursor GetCursor()
		{
			return Cursors.SizeWE;
		}

		public void Start(WidgetActionArgs args)
		{
			_history = args.History;
			int track = args.Track;
			_widget = args.Widget;
			_snap = (int)Math.Round(1 / args.SnapIncrement);
			_endTime = _widget.GetEnd(args.Duration);
			_startMin = 0;
			_startMax = _endTime - args.SnapIncrement;
			_oldDelay = _widget.GetStart();
			_oldLength = _widget.GetLength(args.Duration);
			Delay = _oldDelay;
			Do();
		}

		public void Update(WidgetActionArgs args)
		{
			float time = args.Time;
			time = (float)Math.Round(Math.Round(time * _snap) / _snap, 2);
			float delay = Math.Max(_startMin, Math.Min(time, _startMax));
			if (delay != Delay)
			{
				Undo();
				Delay = delay;
				Do();
			}
		}

		public void Finish()
		{
			Undo();
			_history.Commit(this);
		}

		public void Do()
		{
			float start = Math.Min(_endTime, Delay);
			_widget.SetStart(start);
			_widget.SetLength(Math.Max(0, _endTime - start));
		}

		public void Undo()
		{
			_widget.SetStart(_oldDelay);
			_widget.SetLength(_oldLength);
		}
	}
}

using System;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions.TimelineActions
{
	/// <summary>
	/// Moving a widget
	/// </summary>
	public class MoveWidgetTimelineAction : ITimelineAction, ICommand
	{
		private ITimelineObject _widget;

		private bool _moved;
		private float _oldStart;
		private float _startOffset;

		public float Time;
		private int _oldTrack;
		public int Track;
		private ITimelineData _data;
		private UndoManager _history;

		private bool _allowTrackSwitch;

		public MoveWidgetTimelineAction(bool allowTrackSwitch)
		{
			_allowTrackSwitch = allowTrackSwitch;
		}

		public Cursor GetHoverCursor()
		{
			return Cursors.Default;
		}

		public Cursor GetCursor()
		{
			return Timeline.HandClosed;
		}

		public void Start(WidgetActionArgs args)
		{
			_history = args.History;
			_oldTrack = args.Track;
			_data = args.Data;
			_widget = args.Widget;
			Track = args.Track;
			_oldStart = _widget.GetStart();
			_startOffset = args.SnapTime() - _oldStart;
			_widget.OnStartMove(args);
		}

		public void Update(WidgetActionArgs args)
		{
			int track = args.Track;
			float time = args.Modifiers.HasFlag(Keys.Shift) ? args.Time : args.SnapTime();
			float start = Math.Max(0, time - _startOffset);
			if (Math.Abs(start) > 0.001f && !args.Modifiers.HasFlag(Keys.Shift))
			{
				start = args.Snap(start);
			}
			
			if (start != _widget.GetStart() || (_allowTrackSwitch && track != Track))
			{
				if (_moved)
				{
					Undo();
				}
				_moved = true;
				if (_allowTrackSwitch)
				{
					Track = track;
				}
				Time = start;
				Do();
			}
		}

		public void Finish()
		{
			if (_moved)
			{
				Undo();
				_history?.Commit(this);
			}
		}

		public void Do()
		{
			if (_moved)
			{
				_widget.SetStart(Time);
				if (_oldTrack != Track)
				{
					_data.MoveWidget(_widget, Track);
				}
			}
		}

		public void Undo()
		{
			if (_moved)
			{
				if (_oldTrack != Track)
				{
					_data.MoveWidget(_widget, _oldTrack);
				}
				_widget.SetStart(_oldStart);
			}
		}
	}
}

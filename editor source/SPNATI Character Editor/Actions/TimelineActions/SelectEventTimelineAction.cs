using SPNATI_Character_Editor.EpilogueEditor;
using System;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Actions.TimelineActions
{
	public class SelectEventTimelineAction : ITimelineAction
	{
		private Timeline _timeline;
		private KeyframedWidget _widget;
		private ITimeCommand _command;
		private LiveEvent _event;
		private LiveAnimatedObject _data;
		private float _startTime;
		private bool _dragging;

		public SelectEventTimelineAction(KeyframedWidget widget, LiveEvent evt)
		{
			_widget = widget;
			_event = evt;
		}

		public Cursor GetCursor()
		{
			return Cursors.Default;
		}

		public Cursor GetHoverCursor()
		{
			return Cursors.Hand;
		}

		public void Start(WidgetActionArgs args)
		{
			_timeline = args.Timeline;
			_data = args.Widget.GetData() as LiveAnimatedObject;
			_widget.SelectEvent(_event);
			_timeline.CurrentTime = _data.Start + _event.Time;
			_startTime = args.Time;

			_timeline.SelectData(_event, null);
		}

		public void Update(WidgetActionArgs args)
		{
			if (Math.Abs(args.Time - _startTime) >= args.SnapIncrement)
			{
				_dragging = true;
			}
			if (!_dragging) { return; }
			bool snap = !args.Modifiers.HasFlag(Keys.Shift);
			float snappedTime = snap ? args.SnapTime() : args.Time;
			if (_widget.SelectedProperties.Count > 0 || _event.Time > 0)
			{
				snappedTime -= _data.Start;
			}
			if (snap)
			{
				snappedTime = Math.Max(args.SnapIncrement, snappedTime);
			}
			else
			{
				snappedTime = Math.Max(0.01f, snappedTime);
			}
			snappedTime = (float)Math.Round(snappedTime, 2);

			LiveEvent eventAtTime = _data.Events.Find(k => k.Time == snappedTime);
			if (eventAtTime == null)
			{
				if (_event.Time != snappedTime)
				{
					if (_command == null)
					{
						_command = new MoveEventCommand(_data, _event);
						_command.Do();
					}
					else
					{
						_command.Update(snappedTime);
					}
				}
			}
		}

		public void Finish()
		{
			if (_command != null)
			{
				_timeline.CommandHistory.Record(_command);
			}
		}
	}

	public class MoveEventCommand : ITimeCommand
	{
		private LiveAnimatedObject _data;
		private LiveEvent _event;
		private float _oldTime;

		public float Time;

		public LiveKeyframe NewKeyframe { get { return null; } }

		public MoveEventCommand(LiveAnimatedObject data, LiveEvent evt)
		{
			Time = evt.Time;
			_data = data;
			_event = evt;
			_oldTime = _event.Time;
		}

		public void Do()
		{
			_event.Time = Time;
		}

		/// <summary>
		/// Updates the action without needing to do a full redo. Should only be called after Do and before Undo
		/// </summary>
		/// <param name="time"></param>
		public void Update(float time)
		{
			time = _data.LinkedToEnd ? time : Math.Min(_data.Length, time);
			Time = time - _data.Start;
			Time = (float)Math.Round(time, 2);
			_event.Time = Time;
		}

		public void Undo()
		{
			_event.Time = _oldTime;
		}
	}
}

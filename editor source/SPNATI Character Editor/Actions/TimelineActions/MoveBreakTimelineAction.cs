using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditing.Widgets;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions.TimelineActions
{
	/// <summary>
	/// Moving a widget
	/// </summary>
	public class MoveBreakTimelineAction : ITimelineAction, ICommand
	{
		private PauseWidget _widget;

		private int _index;
		private float _startTime;
		private bool _moved;
		private float _oldStart;
		private float _startOffset;

		public float Time;
		private int _oldTrack;
		public int Track;
		private LiveScene _data;
		private UndoManager _history;
		private bool _finished;

		private HashSet<float> _validBreaks = new HashSet<float>();

		public MoveBreakTimelineAction(int breakIndex)
		{
			_index = breakIndex;
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
			_data = args.Data as LiveScene;
			_widget = args.Widget as PauseWidget;
			_widget.ShowAllBreaks = true;
			_validBreaks = _widget.ValidBreaks;
			_startTime = _data.BreakSet.Pauses[_index].Time;
			Track = args.Track;
			_oldStart = _startTime;
			_startOffset = args.SnapTime() - _oldStart;
			_widget.OnStartMove(args);
			_widget.SelectBreak(_data.BreakSet.Pauses[_index]);
		}

		public void Update(WidgetActionArgs args)
		{
			float time = args.Modifiers.HasFlag(Keys.Shift) ? args.Time : args.SnapTime();
			float start = Math.Max(0, time - _startOffset);

			float closest = -1;
			float closestDist = float.MaxValue;
			float max = 0;
			foreach (float potential in _validBreaks)
			{
				float dist = Math.Abs(potential - start);
				if (dist < closestDist)
				{
					closest = potential;
					closestDist = dist;
				}
				max = Math.Max(max, potential);
			}

			start = closest;

			if (start != _widget.GetStart() && start >= 0)
			{
				if (_moved)
				{
					Undo();
				}
				_moved = true;
				Time = start;
				Do();
			}
		}

		public void Finish()
		{
			if (_moved)
			{
				_finished = true;
				Undo();
				_history?.Commit(this);
			}
			_widget.ShowAllBreaks = false;
		}

		public void Do()
		{
			if (_moved)
			{
				_data.BreakSet.MoveBreak(_index, Time, _finished);
			}
		}

		public void Undo()
		{
			if (_moved)
			{
				_data.BreakSet.MoveBreak(_index, _oldStart, _finished);
			}
		}
	}
}

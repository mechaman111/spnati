using System;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions.TimelineActions
{
	/// <summary>
	/// Dragging the end position of a widget
	/// </summary>
	public class WidgetEndTimelineAction : ITimelineAction, ICommand
	{
		private ITimelineWidget _widget;
		private UndoManager _history;
		private float _endMin;
		private float _endMax;
		private int _snap;
		private float _oldLength;

		public float Length;

		public float End;


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
			int track = args.Track;
			_history = args.History;
			_widget = args.Widget;
			_snap = (int)Math.Round(1 / args.SnapIncrement);
			_endMax = float.MaxValue;
			_endMin = _widget.GetStart() + args.SnapIncrement;
			_oldLength = _widget.GetLength(args.Duration);
			Length = _oldLength;
			Do();
		}

		public void Update(WidgetActionArgs args)
		{
			float time = args.Time;
			if (!args.Modifiers.HasFlag(Keys.Shift))
			{
				time = (float)Math.Round((float)Math.Round(time * _snap) / _snap, 2);
			}
			time = Math.Max(_endMin, Math.Min(time, _endMax));
			float length = time - _widget.GetStart();
			if (length != Length)
			{
				Undo();
				Length = length;
				Do();
			}
		}

		public void Finish()
		{
			Undo();
			_history?.Commit(this);
		}

		public void Do()
		{
			_widget.SetLength(Length);
		}

		public void Undo()
		{
			_widget.SetLength(_oldLength);
		}
	}
}

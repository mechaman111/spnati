using System;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions.TimelineActions
{
	/// <summary>
	/// Resizing the end point of a widget that has no keyframes
	/// </summary>
	public class ModifyWidgetLengthTimelineAction : ITimelineAction, ICommand
	{
		private ITimelineWidget _widget;
		private LiveObject _object;
		private IFixedLength _lengthObj;

		private float _oldLength;
		private bool _moved;

		public float Length;
		private UndoManager _history;

		public Cursor GetHoverCursor()
		{
			return Cursors.SizeWE;
		}

		public Cursor GetCursor()
		{
			return Cursors.SizeWE;
		}

		public void Start(WidgetActionArgs args)
		{
			_history = args.History;
			_widget = args.Widget as ITimelineWidget;
			if (_widget == null)
			{
				throw new NotSupportedException();
			}
			_object = _widget.GetData() as LiveObject;
			_lengthObj = _object as IFixedLength;
			_oldLength = _object.Start + _lengthObj.Length;
			_widget.OnStartMove(args);
		}

		public void Update(WidgetActionArgs args)
		{
			bool snap = !args.Modifiers.HasFlag(Keys.Shift);
			float time = !snap ? args.Time : args.SnapTime();
			float length = Math.Max(snap ? args.SnapIncrement : 0.01f, time - _object.Start);
			if (length != _lengthObj.Length)
			{
				if (_moved)
				{
					Undo();
				}
				_moved = true;
				Length = length;
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
				_lengthObj.Length = Length;
			}
		}

		public void Undo()
		{
			if (_moved)
			{
				_lengthObj.Length = _oldLength;
			}
		}
	}
}

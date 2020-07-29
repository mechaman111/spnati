using System;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.Actions.TimelineActions
{
	public class SelectKeyframeTimelineAction : ITimelineAction
	{
		private Timeline _timeline;
		private KeyframedWidget _widget;
		private ITimeCommand _command;
		private LiveKeyframe _keyframe;
		private string _property;
		private LiveAnimatedObject _data;
		private bool _movable;
		private bool _movingStart;
		private float _startTime;
		private bool _dragging;
		private bool _splitOnMove;
		private bool _copyOnMove;
		private LiveKeyframe _originalFrame;

		public SelectKeyframeTimelineAction(KeyframedWidget widget, LiveKeyframe keyframe, string property)
		{
			_widget = widget;
			_movable = (_widget.SelectedProperties.Count == 0 && property == null) || keyframe.Time != 0;
			_keyframe = keyframe;
			_property = property;
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
			_copyOnMove = args.Modifiers.HasFlag(Keys.Control) && _data.Keyframes.IndexOf(_keyframe) == _data.Keyframes.Count - 1;
			_splitOnMove = args.Modifiers.HasFlag(Keys.Control) && !_copyOnMove;
			_widget.SelectKeyframe(_keyframe, _property, args.Modifiers.HasFlag(Keys.Control));
			_timeline.CurrentTime = _data.Start + _keyframe.Time;
			_startTime = args.Time;

			LiveKeyframe interpolation = _data.GetInterpolatedFrame(_keyframe.Time);
			_timeline.SelectData(_keyframe, interpolation);
		}

		public void Update(WidgetActionArgs args)
		{
			if (Math.Abs(args.Time - _startTime) >= args.SnapIncrement)
			{
				_dragging = true;
			}
			if (!_movable || !_dragging) { return; }
			bool snap = !args.Modifiers.HasFlag(Keys.Shift);
			float snappedTime = snap ? args.SnapTime() : args.Time;
			if (_widget.SelectedProperties.Count > 0 || _keyframe.Time > 0)
			{
				snappedTime -= _data.Start;
			}
			if (snap)
			{
				snappedTime = Math.Max(_movingStart ? 0 : args.SnapIncrement, snappedTime);
			}
			else
			{
				snappedTime = Math.Max(0.01f, snappedTime);
			}
			snappedTime = (float)Math.Round(snappedTime, 2);

			if (_originalFrame != null && snappedTime <= _originalFrame.Time)
			{
				return;
			}

			LiveKeyframe frameAtTime = _data.Keyframes.Find(k => k.Time == snappedTime);
			if (_movingStart || frameAtTime == null || (_property != null && !frameAtTime.HasProperty(_property)))
			{
				if (_movingStart || _keyframe.Time != snappedTime)
				{
					if (_command == null)
					{
						if (_widget.SelectedProperties.Count == 0 && _keyframe.Time == 0)
						{
							_command = new MoveSpriteStartCommand(_data, _data.Start);
							_movingStart = true;
						}
						else
						{
							if (_splitOnMove)
							{
								HashSet<string> props = new HashSet<string>();
								foreach (string prop in _widget.SelectedProperties)
								{
									props.Add(prop);
								}
								_originalFrame = _keyframe;
								LiveKeyframe copy = _data.CopyKeyframe(_keyframe, props);
								_data.AddKeyframe(copy);
								_keyframe = copy;
								foreach (string prop in _data.Properties)
								{
									if (copy.HasProperty(prop) && (props.Contains(prop) || props.Count == 0))
									{
										LiveKeyframeMetadata metadata = copy.GetMetadata(prop, true);
										metadata.FrameType = KeyframeType.Begin;
									}
								}
							}
							else if (_copyOnMove && snappedTime > _keyframe.Time)
							{
								HashSet<string> props = new HashSet<string>();
								foreach (string prop in _widget.SelectedProperties)
								{
									props.Add(prop);
								}
								_originalFrame = _keyframe;
								LiveKeyframe copy = _data.CopyKeyframe(_keyframe, props);
								_data.AddKeyframe(copy);
								_keyframe = copy;
							}
							_command = new MoveKeyframeCommand(_data, _keyframe, _widget.SelectedProperties);
							_command.Do();
						}
					}
					else
					{
						_command.Update(snappedTime);
					}
					LiveKeyframe frame = _command.NewKeyframe;
					if (frame != _keyframe)
					{
						_keyframe = frame;
						foreach (string property in _widget.SelectedProperties)
						{
							_widget.SelectKeyframe(_keyframe, property, false);
						}
						_timeline.SelectData(_keyframe);
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

	public interface ITimeCommand : ICommand
	{
		void Update(float time);
		LiveKeyframe NewKeyframe { get; }
	}

	public class MoveKeyframeCommand : ITimeCommand
	{
		private LiveAnimatedObject _data;
		private LiveKeyframe _keyframe;
		private float _oldTime;
		private bool _moveFrame;
		private LiveKeyframe _newFrame;
		public LiveKeyframe NewKeyframe { get { return _newFrame; } }
		private List<string> _properties;

		public float Time;

		public MoveKeyframeCommand(LiveAnimatedObject data, LiveKeyframe keyframe, List<string> properties)
		{
			Time = keyframe.Time;
			_data = data;
			_keyframe = keyframe;
			_oldTime = _keyframe.Time;
			_moveFrame = properties.Count == 0;
			_properties = properties;
		}

		public void Do()
		{
			if (_moveFrame)
			{
				_keyframe.Time = Time;
				_newFrame = _keyframe;
			}
			else
			{
				_newFrame = _data.MoveProperty(_keyframe, _properties, Time, NewKeyframe);
			}
		}

		/// <summary>
		/// Updates the action without needing to do a full redo. Should only be called after Do and before Undo
		/// </summary>
		/// <param name="time"></param>
		public void Update(float time)
		{
			Time = time - _data.Start;
			Time = (float)Math.Round(time, 2);
			if (_moveFrame)
			{
				_keyframe.Time = Time;
			}
			else
			{
				_newFrame = _data.MoveProperty(NewKeyframe, _properties, Time, null);
			}
		}

		public void Undo()
		{
			if (_moveFrame)
			{
				_keyframe.Time = _oldTime;
			}
			else
			{
				_keyframe.Time = _oldTime;
				_data.MoveProperty(NewKeyframe, _properties, _oldTime, _keyframe);
			}
		}
	}
}

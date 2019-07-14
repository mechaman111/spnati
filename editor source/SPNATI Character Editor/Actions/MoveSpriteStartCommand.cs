using System;
using System.Collections.Generic;
using SPNATI_Character_Editor.Actions.TimelineActions;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Moves the starting point of a sprite
	/// </summary>
	public class MoveSpriteStartCommand : ITimeCommand
	{
		private LiveAnimatedObject _data;
		private float _oldStart;
		private float _oldLength;
		public float Time;
		private Dictionary<LiveKeyframe, float> _oldTimes = new Dictionary<LiveKeyframe, float>();
		private Dictionary<LiveKeyframe, DeleteKeyframeCommand> _deletions = new Dictionary<LiveKeyframe, DeleteKeyframeCommand>();

		public LiveKeyframe NewKeyframe
		{
			get { return _data.Keyframes[0]; }
		}

		public MoveSpriteStartCommand(LiveAnimatedObject data, float start)
		{
			_data = data;
			Time = start;
			_oldStart = _data.Start;
			_oldLength = _data.Length;
			foreach (LiveKeyframe kf in data.Keyframes)
			{
				_oldTimes[kf] = kf.Time;
			}
		}

		public void Do()
		{
			Time = (float)Math.Round(Time, 3);
			//delete any keyframes that are moving out of range, or adjust their positions so the absolute time remains the same
			if (Time == _data.Start)
			{
				return;
			}
			float relTime = (float)Math.Round(Time - _data.Start, 3);
			List<LiveKeyframe> frames = new List<LiveKeyframe>();
			for (int i = 1; i < _data.Keyframes.Count; i++)
			{
				LiveKeyframe kf = _data.Keyframes[i];
				if (kf.Time <= relTime)
				{
					frames.Add(kf);
				}
				else
				{
					kf.Time -= relTime;
				}
			}
			foreach (LiveKeyframe kf in frames)
			{
				DeleteKeyframeCommand deletion = new DeleteKeyframeCommand(_data, kf);
				deletion.Do();
				_deletions[kf] = deletion;
			}

			//move the start point
			float duration = _data.Start + _data.Length - Time;
			_data.Start = Time;
			_data.Length = duration;

			//add back any deletions that are no longer deleted
			frames.Clear();
			foreach (KeyValuePair<LiveKeyframe, DeleteKeyframeCommand> kvp in _deletions)
			{
				float oldTime = _oldTimes[kvp.Key];
				if (Time < oldTime + _oldStart)
				{
					frames.Add(kvp.Key);
					kvp.Value.Undo();
				}
			}
			foreach (LiveKeyframe kf in frames)
			{
				_deletions.Remove(kf);
			}
		}

		public void Update(float time)
		{
			Time = time;
			Do();
		}

		public void Undo()
		{
			_data.Start = _oldStart;
			_data.Length = _oldLength;
			foreach (KeyValuePair<LiveKeyframe, float> kvp in _oldTimes)
			{
				LiveKeyframe kf = kvp.Key;
				float time = kvp.Value;
				DeleteKeyframeCommand deletion;
				if (_deletions.TryGetValue(kf, out deletion))
				{
					deletion.Undo();
				}
				kf.Time = time;
			}
		}
	}
}

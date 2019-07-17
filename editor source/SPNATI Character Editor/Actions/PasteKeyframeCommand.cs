using System.Collections.Generic;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	public class PasteKeyframeCommand : ICommand
	{
		private LiveAnimatedObject _object;
		private LiveKeyframe _keyframe;
		private float _time;
		public LiveKeyframe NewKeyframe;
		private LiveKeyframe _original;

		public PasteKeyframeCommand(LiveAnimatedObject obj, LiveKeyframe keyframe, float time)
		{
			_object = obj;
			_keyframe = keyframe;
			_time = time - obj.Start;
			_original = obj.Keyframes.Find(f => f.Time == time);
			if (_original != null)
			{
				_original = obj.CopyKeyframe(_original, new HashSet<string>());
			}
		}

		public void Do()
		{
			NewKeyframe = _object.PasteKeyframe(_keyframe, _time, NewKeyframe);
		}

		public void Undo()
		{
			if (_original == null)
			{
				_object.RemoveKeyframe(NewKeyframe);
			}
			else
			{
				NewKeyframe.Clear();
				_object.PasteKeyframe(_original, _time, NewKeyframe);
			}
		}
	}
}

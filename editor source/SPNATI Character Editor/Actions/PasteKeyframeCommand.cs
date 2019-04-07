using System.Collections.Generic;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	public class PasteKeyframeCommand : ICommand
	{
		private LiveSprite _sprite;
		private LiveKeyframe _keyframe;
		private float _time;
		public LiveKeyframe NewKeyframe;
		private LiveKeyframe _original;

		public PasteKeyframeCommand(LiveSprite sprite, LiveKeyframe keyframe, float time)
		{
			_sprite = sprite;
			_keyframe = keyframe;
			_time = time - sprite.Start;
			_original = sprite.Keyframes.Find(f => f.Time == time);
			if (_original != null)
			{
				_original = sprite.CopyKeyframe(_original, new HashSet<string>());
			}
		}

		public void Do()
		{
			NewKeyframe = _sprite.PasteKeyframe(_keyframe, _time, NewKeyframe);
		}

		public void Undo()
		{
			if (_original == null)
			{
				_sprite.RemoveKeyframe(NewKeyframe);
			}
			else
			{
				NewKeyframe.Clear();
				_sprite.PasteKeyframe(_original, _time, NewKeyframe);
			}
		}
	}
}

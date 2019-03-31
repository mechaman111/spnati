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
		private bool _oldLink;
		private bool _forceFiniteLength;

		public PasteKeyframeCommand(LiveSprite sprite, LiveKeyframe keyframe, float time, bool forceFiniteLength)
		{
			_sprite = sprite;
			_keyframe = keyframe;
			_time = time - sprite.Start;
			_original = sprite.Keyframes.Find(f => f.Time == time);
			_oldLink = sprite.LinkedToEnd;
			_forceFiniteLength = forceFiniteLength;
			if (_original != null)
			{
				_original = sprite.CopyKeyframe(_original, new HashSet<string>());
			}
		}

		public void Do()
		{
			NewKeyframe = _sprite.PasteKeyframe(_keyframe, _time, NewKeyframe);
			if (_forceFiniteLength)
			{
				//_sprite.LinkedToEnd = false;
			}
		}

		public void Undo()
		{
			if (_forceFiniteLength)
			{
				//_sprite.LinkedToEnd = _oldLink;
			}
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

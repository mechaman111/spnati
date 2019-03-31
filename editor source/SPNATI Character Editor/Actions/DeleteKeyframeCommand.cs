using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Deletes an entire keyframe from an animation
	/// </summary>
	public class DeleteKeyframeCommand : ICommand
	{
		private LiveSprite _sprite;
		private LiveKeyframe _keyframe;

		public DeleteKeyframeCommand(LiveSprite sprite, LiveKeyframe keyframe)
		{
			_sprite = sprite;
			_keyframe = keyframe;
		}

		public void Do()
		{
			_sprite.RemoveKeyframe(_keyframe);
		}

		public void Undo()
		{
			_sprite.AddKeyframe(_keyframe);
		}
	}
}

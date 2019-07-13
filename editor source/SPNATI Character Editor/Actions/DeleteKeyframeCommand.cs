using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Deletes an entire keyframe from an animation
	/// </summary>
	public class DeleteKeyframeCommand : ICommand
	{
		private LiveAnimatedObject _data;
		private LiveKeyframe _keyframe;

		public DeleteKeyframeCommand(LiveAnimatedObject data, LiveKeyframe keyframe)
		{
			_data = data;
			_keyframe = keyframe;
		}

		public void Do()
		{
			_data.RemoveKeyframe(_keyframe);
		}

		public void Undo()
		{
			_data.AddKeyframe(_keyframe);
		}
	}
}

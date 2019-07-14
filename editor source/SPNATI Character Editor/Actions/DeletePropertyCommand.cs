using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Deletes a property from a keyframe
	/// </summary>
	public class DeletePropertyCommand : ICommand
	{
		private LiveAnimatedObject _object;
		private LiveKeyframe _keyframe;
		private string _property;
		private object _value;

		public DeletePropertyCommand(LiveAnimatedObject obj, LiveKeyframe keyframe, string property)
		{
			_object = obj;
			_keyframe = keyframe;
			_property = property;
			_value = keyframe.Get<object>(property);
		}

		public void Do()
		{
			_keyframe.Delete(_property);
		}

		public void Undo()
		{
			_keyframe.Set(_value, _property);

			//if deleting the property wiped out the keyframe, add it back
			if (!_object.Keyframes.Contains(_keyframe))
			{
				_object.AddKeyframe(_keyframe);
			}
		}
	}
}

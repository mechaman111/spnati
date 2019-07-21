using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	/// <summary>
	/// Deletes an entire keyframe from an animation
	/// </summary>
	public class DeleteEventCommand : ICommand
	{
		private LiveAnimatedObject _data;
		private LiveEvent _event;

		public DeleteEventCommand(LiveAnimatedObject data, LiveEvent evt)
		{
			_data = data;
			_event = evt;
		}

		public void Do()
		{
			_data.RemoveEvent(_event);
		}

		public void Undo()
		{
			_data.AddEvent(_event);
		}
	}
}

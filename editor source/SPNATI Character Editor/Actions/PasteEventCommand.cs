using System.Collections.Generic;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	public class PasteEventCommand : ICommand
	{
		private LiveAnimatedObject _object;
		private LiveEvent _event;
		private float _time;
		public LiveEvent NewEvent;
		private LiveEvent _original;

		public PasteEventCommand(LiveAnimatedObject obj, LiveEvent evt, float time)
		{
			_object = obj;
			_event = evt;
			_time = time - obj.Start;
			_original = obj.Events.Find(f => f.Time == time);
		}

		public void Do()
		{
			if (_object.GetEventType() != _event.GetType())
			{
				return;
			}
			if (_original != null)
			{
				NewEvent = _original;
			}
			else
			{
				NewEvent = _object.PasteEvent(_event, _time);
			}
		}

		public void Undo()
		{
			if (_original == null && NewEvent != null)
			{
				_object.RemoveEvent(NewEvent);
			}
		}
	}
}

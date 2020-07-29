using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	public class TransferPreviousPropertyCommand : ICommand
	{
		private LiveAnimatedObject _data;
		private string _property;
		private object _oldValue;
		private LiveKeyframe _frame;

		public TransferPreviousPropertyCommand(LiveAnimatedObject data, string property)
		{
			_data = data;
			_property = property;
			LiveKeyframe old = _data.Keyframes.Find(kf => kf.Time == 0);
			_oldValue = old?.Get<object>(_property);
		}

		public void Do()
		{
			object previous = _data.GetPreviousValue(_property, 0, false);
			if (previous != null)
			{
				_frame = _data.AddValue(0, _property, previous, true);
			}
		}

		public void Undo()
		{
			if (_frame == null) { return; }
			if (_oldValue == null)
			{
				_frame.Delete(_property);
			}
			else
			{
				_frame.Set(_oldValue, _property);
			}
		}
	}
}

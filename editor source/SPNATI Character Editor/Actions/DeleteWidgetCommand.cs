using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor.Actions
{
	public class DeleteWidgetCommand : ICommand
	{
		private ITimelineWidget _widget;
		private ITimelineData _data;
		private int _index;

		public DeleteWidgetCommand(ITimelineData data, ITimelineWidget widget)
		{
			_widget = widget;
			_data = data;
		}

		public void Do()
		{
			_index = _data.RemoveWidget(_widget);
		}

		public void Undo()
		{
			if (_index >= 0)
			{
				_data.InsertWidget(_widget, _widget.GetStart(), _index);
			}
		}
	}
}

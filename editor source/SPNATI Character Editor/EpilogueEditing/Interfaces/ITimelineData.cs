using System;
using System.Collections.Generic;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public interface ITimelineData
	{
		event EventHandler<WidgetCreationArgs> WidgetMoved;
		event EventHandler<WidgetCreationArgs> WidgetCreated;
		event EventHandler<WidgetCreationArgs> WidgetRemoved;
		List<ITimelineWidget> CreateWidgets(Timeline timeline);
		ITimelineWidget CreateWidget(Timeline timeline, float time, object context);
		ITimelineWidget CreateWidget(Timeline timeline, float time, object data, int index);
		void InsertWidget(ITimelineWidget widget, float time, int index);
		int RemoveWidget(ITimelineWidget widget);
		void MoveWidget(ITimelineWidget widget, int newTrack);
		bool OnPaste(WidgetOperationArgs args);
		///Called when no widget is selcted
		void UpdateSelection(WidgetSelectionArgs args);
	}

	public class WidgetCreationArgs : EventArgs
	{
		public ITimelineWidget Widget;
		public int Index;

		public WidgetCreationArgs(ITimelineWidget widget, int index)
		{
			Widget = widget;
			Index = index;
		}
	}
}

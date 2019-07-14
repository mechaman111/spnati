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
		void InsertWidget(ITimelineObject widget, float time, int index);
		int RemoveWidget(ITimelineObject widget);
		void MoveWidget(ITimelineObject widget, int newTrack);
		bool OnPaste(WidgetOperationArgs args);
		///Called when no widget is selcted
		void UpdateSelection(WidgetSelectionArgs args);
		List<ITimelineBreak> CreateBreaks(Timeline timeline);
		ITimelineBreak AddBreak(float time);
	}

	public class WidgetCreationArgs : EventArgs
	{
		public ITimelineObject Widget;
		public int Index;

		public WidgetCreationArgs(ITimelineObject widget, int index)
		{
			Widget = widget;
			Index = index;
		}
	}
}

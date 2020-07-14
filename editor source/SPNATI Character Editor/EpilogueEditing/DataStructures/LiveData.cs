using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public abstract class LiveData : BindableObject, ITimelineData
	{
		public abstract event EventHandler<WidgetCreationArgs> WidgetMoved;
		public abstract event EventHandler<WidgetCreationArgs> WidgetCreated;
		public abstract event EventHandler<WidgetCreationArgs> WidgetRemoved;

		public abstract LiveObject Find(string id);
		public abstract LiveObject GetObjectAtPoint(int x, int y, Matrix sceneTransform, bool ignoreMarkers, List<string> markers);
		public abstract void UpdateTime(float time, float elapsedTime, bool inPlayback);
		public abstract bool UpdateRealTime(float deltaTime, bool inPlayback);
		public abstract void Draw(Graphics g, Matrix sceneTransform, List<string> markers, LiveObject selectedObject, LiveObject selectedPreview, bool inPlayback);
		public abstract void FitScene(int windowWidth, int windowHeight, ref Point offset, ref float zoom);
		public abstract Matrix GetSceneTransform(int width, int height, Point offset, float zoom);
		public abstract int BaseHeight { get; set; }
		public abstract List<LiveObject> GetAvailableParents(LiveObject child);
		public abstract float GetDuration();

		public abstract bool Paste(WidgetOperationArgs args, LiveObject after);

		//ITimelineData
		public abstract List<ITimelineWidget> CreateWidgets(Timeline timeline);
		public abstract ITimelineWidget CreateWidget(Timeline timeline, float time, object context);
		public abstract ITimelineWidget CreateWidget(Timeline timeline, float time, object data, int index);
		public abstract void InsertWidget(ITimelineObject widget, float time, int index);
		public abstract int RemoveWidget(ITimelineObject widget);
		public abstract void MoveWidget(ITimelineObject widget, int newTrack);
		public abstract bool OnPaste(WidgetOperationArgs args);
		public abstract void UpdateSelection(WidgetSelectionArgs args);
		public abstract List<ITimelineBreak> CreateBreaks(Timeline timeline);
		public abstract ITimelineBreak AddBreak(float time);
	}
}

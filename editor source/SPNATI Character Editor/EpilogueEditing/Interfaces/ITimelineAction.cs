using System;
using System.Windows.Forms;
using Desktop;
using SPNATI_Character_Editor.EpilogueEditor;

namespace SPNATI_Character_Editor
{
	public interface ITimelineAction
	{
		Cursor GetHoverCursor();
		Cursor GetCursor();
		void Start(WidgetActionArgs args);
		void Update(WidgetActionArgs args);
		void Finish();
	}

	public class WidgetActionArgs
	{
		/// <summary>
		/// Undo history manager
		/// </summary>
		public UndoManager History;
		/// <summary>
		/// Timeline control
		/// </summary>
		public Timeline Timeline;
		/// <summary>
		/// Containing data object
		/// </summary>
		public ITimelineData Data;
		/// <summary>
		/// Track the widget is on
		/// </summary>
		public int Track;
		/// <summary>
		/// Selected widget
		/// </summary>
		public ITimelineObject Widget;
		/// <summary>
		/// Selected time
		/// </summary>
		public float Time;
		/// <summary>
		/// Which row is selected
		/// </summary>
		public int Row;
		/// <summary>
		/// Timeline tick snapping
		/// </summary>
		public float SnapIncrement;
		/// <summary>
		/// Modifier keys being held down
		/// </summary>
		public Keys Modifiers;
		/// <summary>
		/// Duration of entire animation
		/// </summary>
		public float Duration;

		/// <summary>
		/// Snaps the Time value to the grid
		/// </summary>
		/// <returns></returns>
		public float SnapTime()
		{
			return Snap(Time);
		}

		/// <summary>
		/// Snaps a time to the grid
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public float Snap(float time)
		{
			float inverse = 1 / SnapIncrement;
			return (float)Math.Round(Math.Round(time * inverse) / inverse, 2);
		}
	}

	public class DataSelectionArgs : EventArgs
	{
		public object Data { get; private set; }
		public object PreviewData { get; private set; }

		public DataSelectionArgs(object data, object previewData)
		{
			Data = data;
			PreviewData = previewData;
		}
	}
}

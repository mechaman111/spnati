using Desktop;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public interface ITimelineWidget : ITimelineObject
	{
		event EventHandler Invalidated;
		void DrawContents(Graphics g, int rowIndex, int x, int y, float pps, int rowHeight, float dataEndTime);
		string GetLabel(int row);
		int GetRowCount();
		Image GetThumbnail();
		/// <summary>
		/// Gets widget's length in seconds
		/// </summary>
		/// <param name="duration">Duration of the entire animation</param>
		/// <returns></returns>
		float GetLength(float duration);
		bool IsCollapsible { get; }
		bool IsCollapsed { get; set; }
		/// <summary>
		/// Gets whether a row should be highlighted on the selected widget
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		bool IsRowHighlighted(int row);
		/// <summary>
		/// Called on a widget when its header is clicked
		/// </summary>
		/// <param name="args"></param>
		void OnClickHeader(WidgetActionArgs args);
		/// <summary>
		/// Called on a widget when its header is double-clicked
		/// </summary>
		/// <param name="args"></param>
		void OnDoubleClickHeader(WidgetActionArgs args);
		/// <summary>
		/// Called when clicking a header icon. OnClickHeader will still be called immediately after this
		/// </summary>
		/// <param name="args"></param>
		void OnClickHeaderIcon(WidgetActionArgs args, int iconIndex);
		/// <summary>
		/// Called when hovering over a row in the header to get a tooltip to display
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		string GetHeaderTooltip(WidgetActionArgs args, int iconIndex);
		void OnPlaybackChanged(bool playing);
		/// <summary>
		/// Gets the number of icons that can display in a row header
		/// </summary>
		/// <returns></returns>
		int GetHeaderIconCount(int row);
		/// <summary>
		/// Draws an icon on the row header
		/// </summary>
		/// <param name="row"></param>
		/// <param name="index"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="rowHeight"></param>
		void DrawHeaderIcon(Graphics g, int rowIndex, int iconIndex, int x, int y, int size, int highlightedIconIndex);
		LiveEvent GetEventBetween(float start, float end);
		Color GetAccent();
	}

	public class WidgetOperationArgs
	{
		public UndoManager History;
		public Timeline Timeline;
		public float Time;
		public ITimelineData Data;
		public bool Handled;
		public bool IsSilent;
	}

	public class WidgetSelectionArgs
	{
		public Timeline Timeline;
		public SelectionType IsSelected;
		public bool AllowCut = false;
		public bool AllowCopy = false;
		public bool AllowPaste = false;
		public bool AllowDuplicate = false;
		public bool AllowDelete = false;
		public Keys Modifiers;

		public WidgetSelectionArgs(Timeline timeline, SelectionType selected, Keys modifiers)
		{
			Timeline = timeline;
			IsSelected = selected;
			Modifiers = modifiers;
		}
	}

	public static class ITimelineWidgetExtensions
	{
		public static float GetEnd(this ITimelineWidget widget, float duration)
		{
			return widget.GetStart() + widget.GetLength(duration);
		}
	}

	public enum SelectionType
	{
		Select,
		Deselect,
		Reselect,
	}
}

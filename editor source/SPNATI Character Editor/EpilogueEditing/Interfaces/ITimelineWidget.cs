using Desktop;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public interface ITimelineWidget
	{
		object GetData();
		void OnWidgetSelectionChanged(WidgetSelectionArgs args);
		event EventHandler Invalidated;
		void DrawContents(Graphics g, int rowIndex, int x, int y, float pps, int widgetWidth, int rowHeight);
		string GetLabel(int row);
		int GetRowCount();
		/// <summary>
		/// Gets widget's starting point in seconds
		/// </summary>
		/// <returns></returns>
		float GetStart();
		void SetStart(float time);
		Brush GetFillBrush();
		Image GetThumbnail();
		/// <summary>
		/// Gets widget's length in seconds
		/// </summary>
		/// <param name="duration">Duration of the entire animation</param>
		/// <returns></returns>
		float GetLength(float duration);
		/// <summary>
		/// Sets the widget's length in seconds
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		void SetLength(float time);
		bool IsCollapsible { get; }
		bool IsCollapsed { get; set; }
		bool IsResizable { get; }
		/// <summary>
		/// Gets whether a row should be highlighted on the selected widget
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		bool IsRowHighlighted(int row);
		/// <summary>
		/// Gets or sets whether the widget's length is tied to the end of the whole timeline's duration
		/// </summary>
		bool LinkedToEnd { get; }
		/// <summary>
		/// Gets the action that should occur if the mouse is click on what it's currently hovering over
		/// </summary>
		/// <param name="x"></param>
		/// <param name="width"></param>
		/// <param name="start"></param>
		/// <param name="row"></param>
		/// <param name="timelineWidth"></param>
		/// <param name="pps"></param>
		/// <returns></returns>
		ITimelineAction GetAction(int x, int width, float start, int row, int timelineWidth, float pps);
		/// <summary>
		/// Called on a widget when its header is clicked
		/// </summary>
		/// <param name="args"></param>
		void OnClickHeader(WidgetActionArgs args);
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
		/// <summary>
		/// Called on the selected widget after an undo or redo operation to clear the selection properly if it got deleted
		/// </summary>
		void UpdateSelection(WidgetSelectionArgs args);
		/// <summary>
		/// Called when performing a copy operation while the widget is selected
		/// </summary>
		/// <returns></returns>
		bool OnCopy(WidgetOperationArgs args);
		/// <summary>
		/// Called when performing a delete operation while the widget is selected
		/// </summary>
		/// <returns></returns>
		bool OnDelete(WidgetOperationArgs args);
		/// <summary>
		/// Called when performing a paste operation while the widget is selected
		/// </summary>
		/// <returns></returns>
		bool OnPaste(WidgetOperationArgs args);
		/// <summary>
		/// Called when performing a duplicate operation while the widget is selected
		/// </summary>
		/// <returns></returns>
		bool OnDuplicate(WidgetOperationArgs args);
		/// <summary>
		/// Called when clicking on a widget to move it instead of something inside of it
		/// </summary>
		/// <param name="args"></param>
		void OnStartMove(WidgetActionArgs args);
		/// <summary>
		/// Called when the current time is manually changed
		/// </summary>
		/// <param name="time"></param>
		void OnTimeChanged(WidgetOperationArgs args);
		void OnMouseOut();
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
		/// <summary>
		/// Advances the selection forward or backwards within the widget
		/// </summary>
		/// <param name="forward"></param>
		void AdvanceSubWidget(bool forward);
		/// <summary>
		/// Called on the selected widget when displaying the right-click menu
		/// </summary>
		/// <param name="args"></param>
		void OnOpeningContextMenu(ContextMenuArgs args);
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

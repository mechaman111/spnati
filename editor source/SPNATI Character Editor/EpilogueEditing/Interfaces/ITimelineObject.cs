using Desktop.Skinning;

namespace SPNATI_Character_Editor.EpilogueEditor
{
	public interface ITimelineObject
	{
		object GetData();
		/// <summary>
		/// Gets widget's starting point in seconds
		/// </summary>
		/// <returns></returns>
		float GetStart();
		void SetStart(float time);
		void OnWidgetSelectionChanged(WidgetSelectionArgs args);
		void UpdateSkin(Skin skin);
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
		ITimelineAction GetAction(int x, float start, int row, int timelineWidth, float pps);
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
		/// Called on the selected widget when displaying the right-click menu
		/// </summary>
		/// <param name="args"></param>
		void OnOpeningContextMenu(ContextMenuArgs args);
		/// <summary>
		/// Advances the selection forward or backwards within the widget
		/// </summary>
		/// <param name="forward"></param>
		void AdvanceSubWidget(bool forward);
		/// <summary>
		/// Called when the current time is manually changed
		/// </summary>
		/// <param name="time"></param>
		void OnTimeChanged(WidgetOperationArgs args);
		/// <summary>
		/// Called when double-clicking the main widget area
		/// </summary>
		/// <param name="args"></param>
		void OnDoubleClick(WidgetActionArgs args);
		/// <summary>
		/// Called when clicking on a widget to move it instead of something inside of it
		/// </summary>
		/// <param name="args"></param>
		void OnStartMove(WidgetActionArgs args);
		void OnMouseOut();
	}
}

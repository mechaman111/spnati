using System;

namespace Desktop
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	/// <summary>
	/// Designates metadata about an activity to place in a workspace
	/// </summary>
	public class ActivityAttribute : Attribute
	{
		public Type RecordType { get; set; }
		public int Order { get; set; }
		/// <summary>
		/// Which pane to place the activity in
		/// </summary>
		public WorkspacePane Pane = WorkspacePane.Main;
		/// <summary>
		/// Width when used in a sidebar
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// If true, the activity's tab will be added on auto-start, but the underlying activity won't be created until manually launched
		/// </summary>
		public bool DelayRun { get; set; }
		/// <summary>
		/// Activity's caption, used when delay running
		/// </summary>
		public string Caption { get; set; }

		public ActivityAttribute(Type type, int order)
		{
			RecordType = type;
			Order = order;
		}
	}

	/// <summary>
	/// Designates that an activity should get a spacer before it in the tabs sequence
	/// </summary>
	public class SpacerAttribute : Attribute
	{
	}
}

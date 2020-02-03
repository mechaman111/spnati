using System;
using System.Drawing;

namespace Desktop.CommonControls
{
	public class FormatRowEventArgs : EventArgs
	{
		public object Model { get; private set; }
		public Color ForeColor { get; set; }
		public string Tooltip { get; set; }
		public Color GrouperColor { get; set; }
		public GroupedListGrouper Group { get; private set; }

		public FormatRowEventArgs(object model, GroupedListGrouper grouper)
		{
			Model = model;
			Group = grouper;
		}
	}

	public class FormatGroupEventArgs : EventArgs
	{
		public GroupedListGrouper Group { get; private set; }
		public string Label { get; set; }
		public Color ForeColor { get; set; }
		public Font Font { get; set; }

		public FormatGroupEventArgs(GroupedListGrouper group, Font font)
		{
			Group = group;
			Label = group.Key;
			ForeColor = Color.RoyalBlue;
			Font = font;
		}
	}

	public class AccordionListViewEventArgs : EventArgs
	{
		public GroupedListGrouper Group { get; private set; }
		public object Model { get; private set; }

		public AccordionListViewEventArgs(object item)
		{
			if (item is GroupedListGrouper)
			{
				Group = item as GroupedListGrouper;
			}
			else
			{
				Model = item;
			}
		}
	}

	public class AccordionListViewDragEventArgs : EventArgs
	{
		public object Source;
		public object Target;
		public bool Before;

		public AccordionListViewDragEventArgs(object source, object target, bool before)
		{
			Source = source;
			Target = target;
			Before = before;
		}
	}
}

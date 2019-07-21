using System;
using System.Windows.Forms;

namespace Desktop.Reporting.Controls
{
	[DataSlicerControl(typeof(ComboSlicer))]
	public partial class ComboSlicerControl : UserControl, ISlicerControl
	{
		private IDataSlicer _slicer;

		public ComboSlicerControl()
		{
			InitializeComponent();
		}

		public void SetSlicer(IDataSlicer slicer)
		{
			_slicer = slicer;
			_slicer.Groups.CollectionChanged += Groups_CollectionChanged;
			foreach (ISlicerGroup group in _slicer.Groups)
			{
				AddEntry(group);
			}
		}

		private void Groups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					ISlicerGroup group = e.NewItems[0] as ISlicerGroup;
					AddEntry(group);
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					group = e.OldItems[0] as ISlicerGroup;
					foreach (Control ctl in flowPanel.Controls)
					{
						SlicerGroupEntry entry = ctl as SlicerGroupEntry;
						if (entry != null && entry.Group == group)
						{
							DeleteEntry(entry);
							break;
						}
					}
					break;
			}
		}

		private void AddEntry(ISlicerGroup group)
		{
			SlicerGroupEntry entry = new SlicerGroupEntry();
			entry.Delete += Entry_Delete;
			entry.Merge += Entry_Merge;
			entry.Removable = false;
			entry.SetGroup(group);
			entry.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			entry.Width = flowPanel.Width - flowPanel.Padding.Left - flowPanel.Padding.Right;
			flowPanel.Controls.Add(entry);
		}

		private void Entry_Delete(object sender, EventArgs e)
		{
			SlicerGroupEntry entry = sender as SlicerGroupEntry;
			entry.Group.Active = false;
			_slicer.RemoveGroup(entry.Group);
		}

		private void DeleteEntry(SlicerGroupEntry entry)
		{
			entry.Delete -= Entry_Delete;
			entry.Merge -= Entry_Merge;
			flowPanel.Controls.Remove(entry);
			_slicer.RemoveGroup(entry.Group);
		}

		private void Entry_Merge(object sender, EventArgs e)
		{
			mnuMerge.Items.Clear();
			SlicerGroupEntry entry = sender as SlicerGroupEntry;
			if (entry.Group.Values.Count > 1)
			{
				ToolStripItem item = mnuMerge.Items.Add("Split apart");
				item.Tag = entry;
				item.Click += Item_Split;
			}
			else
			{
				foreach (ISlicerGroup group in _slicer.Groups)
				{
					if (entry.Group != group && group.Groupable)
					{
						ToolStripItem item = mnuMerge.Items.Add($"Merge into {group.Label}");
						item.Tag = new Tuple<SlicerGroupEntry, ISlicerGroup>(entry, group);
						item.Click += Item_Click;
					}
				}
			}
			mnuMerge.Show(MousePosition);
		}

		private void Item_Split(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			SlicerGroupEntry entry = item.Tag as SlicerGroupEntry;
			ISlicerGroup group = entry.Group;
			_slicer.Split(group);
		}

		private void Item_Click(object sender, EventArgs e)
		{
			ToolStripItem item = sender as ToolStripItem;
			Tuple<SlicerGroupEntry, ISlicerGroup> data = item.Tag as Tuple<SlicerGroupEntry, ISlicerGroup>;
			SlicerGroupEntry sourceEntry = data.Item1;
			ISlicerGroup sourceGroup = sourceEntry.Group;
			ISlicerGroup destGroup = data.Item2;
			_slicer.Merge(sourceGroup, destGroup);
			DeleteEntry(sourceEntry);
		}
	}
}

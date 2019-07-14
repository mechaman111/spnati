using Desktop.Reporting;
using Desktop.Reporting.Controls;
using SPNATI_Character_Editor.DataSlicers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls.SlicerControls
{
	[DataSlicerControl(typeof(IntervalSlicer))]
	public partial class IntervalSlicerControl : UserControl, ISlicerControl
	{
		private IntervalSlicer _slicer;

		public IntervalSlicerControl()
		{
			InitializeComponent();
		}

		public void SetSlicer(IDataSlicer slicer)
		{
			_slicer = slicer as IntervalSlicer;
			sliderSplits.Minimum = _slicer.Min;
			sliderSplits.Maximum = _slicer.Max;
			CreateGroupControls(true);
			if (sliderSplits.Splits.Count > 0)
			{
				int smallestSplit = sliderSplits.Splits.Min();
				sliderSplits.Splits.Remove(smallestSplit);
			}
			sliderSplits.Splits.CollectionChanged += Splits_CollectionChanged;
		}

		private void CreateGroupControls(bool createSplits)
		{
			flowPanel.Controls.Clear();
			foreach (ISlicerGroup group in _slicer.Groups)
			{
				if (group.Key != "-" && createSplits)
				{
					sliderSplits.Splits.Add(group.Values.Max(v => ((Range)v).Min));
				}
				AddEntry(group);
			}
		}

		private void Splits_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			RebuildGroups();
		}

		private void RebuildGroups()
		{
			sliderSplits.Splits.CollectionChanged -= Splits_CollectionChanged;
			ObservableCollection<ISlicerGroup> groups = new ObservableCollection<ISlicerGroup>();
			groups.Add(_slicer.NullGroup);
			int last = sliderSplits.Minimum;
			for (int i = 0; i < sliderSplits.Splits.Count; i++)
			{
				SlicerGroup<Range> group = new SlicerGroup<Range>(new Range(last, sliderSplits.Splits[i] - 1));
				groups.Add(group);
				last = sliderSplits.Splits[i];
				if (i == sliderSplits.Splits.Count - 1)
				{
					group = new SlicerGroup<Range>(new Range(last, sliderSplits.Maximum));
					groups.Add(group);
				}
			}
			_slicer.Groups = groups;
			CreateGroupControls(false);
			sliderSplits.Splits.CollectionChanged += Splits_CollectionChanged;
		}

		private void AddEntry(ISlicerGroup group)
		{
			SlicerGroupEntry entry = new SlicerGroupEntry();
			entry.Removable = false;
			group.Groupable = false;
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

using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Desktop.Reporting
{
	public abstract class BaseSlicer<T> : BindableObject, IDataSlicer
	{
		public object Context
		{
			get { return Get<object>(); }
			set { Set(value); }
		}

		public string DisplayName
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public ObservableCollection<ISlicerGroup> Groups
		{
			get { return Get<ObservableCollection<ISlicerGroup>>(); }
			set { Set(value); }
		}

		public BaseSlicer()
		{
			Groups = new ObservableCollection<ISlicerGroup>();
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public ISlicerGroup AddGroup(object value)
		{
			SlicerGroup<T> group = new SlicerGroup<T>(value);
			Groups.Add(group);
			return group;
		}

		public void RemoveGroup(ISlicerGroup group)
		{
			Groups.Remove(group);
		}

		public void ClearGroups()
		{
			Groups.Clear();
		}

		public void Split(ISlicerGroup group)
		{
			if (group.Values.Count <= 1 || !group.Groupable)
			{
				return;
			}
			foreach (object value in group.Values)
			{
				AddGroup(value);
			}
			RemoveGroup(group);
		}

		public void Merge(ISlicerGroup source, ISlicerGroup dest)
		{
			foreach (object value in source.Values)
			{
				dest.Values.Add(value);
			}
			source.Values.Clear();
			RemoveGroup(source);
			int max = 0;
			Regex regex = new Regex(@"Group (\d+)");
			foreach (ISlicerGroup group in Groups)
			{
				Match match = regex.Match(group.Label);
				if (match.Success)
				{
					int groupNum;
					if (int.TryParse(match.Groups[2].Value, out groupNum))
					{
						max = Math.Max(max, groupNum);
					}
				}
			}
			dest.Label = $"Group {max + 1}";
		}

		public void SetContext(object context)
		{
			Context = context;
		}

		public abstract List<DataBucket> Slice(IEnumerable<ISliceable> dataSet);
	}
}

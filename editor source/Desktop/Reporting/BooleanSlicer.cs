using Desktop.CommonControls;
using Desktop.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Desktop.Reporting
{
	public class BooleanSlicer : BindableObject, IDataSlicer
	{
		public string Property;
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

		public ISlicerGroup YesGroup { get; private set; }
		public ISlicerGroup NoGroup { get; private set; }

		public BooleanSlicer(string property, string displayName)
		{
			Groups = new ObservableCollection<ISlicerGroup>();
			SlicerGroup<IRecord> groupTrue = new SlicerGroup<IRecord>();
			groupTrue.Label = "Yes";
			groupTrue.Key = "Yes";
			groupTrue.Values.Add(true);
			YesGroup = groupTrue;
			Groups.Add(groupTrue);
			SlicerGroup<IRecord> groupFalse = new SlicerGroup<IRecord>();
			groupFalse.Label = "No";
			groupFalse.Key = "No";
			groupFalse.Values.Add(false);
			NoGroup = groupFalse;
			Groups.Add(groupFalse);
			Property = property;
			DisplayName = displayName;
		}

		public void SetContext(object context) { }

		public override string ToString()
		{
			return DisplayName;
		}

		public ISlicerGroup AddGroup(object value)
		{
			throw new NotImplementedException();
		}
		public void RemoveGroup(ISlicerGroup group)
		{
			throw new NotImplementedException();
		}
		public void Split(ISlicerGroup group)
		{
			throw new NotImplementedException();
		}
		public void Merge(ISlicerGroup source, ISlicerGroup dest)
		{
			throw new NotImplementedException();
		}

		public List<DataBucket> Slice(IEnumerable<ISliceable> dataSet)
		{
			Dictionary<bool, DataBucket> buckets = new Dictionary<bool, DataBucket>();
			List<DataBucket> list = new List<DataBucket>();
			foreach (SlicerGroup<IRecord> group in Groups)
			{
				if (!group.Active) { continue; }
				DataBucket bucket = new DataBucket(group.Label);
				foreach (bool record in group.Values)
				{
					buckets[record] = bucket;
				}
				list.Add(bucket);
			}
			list.Sort();

			foreach (ISliceable obj in dataSet)
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(obj.GetType(), Property);
				object value = mi.GetValue(obj);
				bool result = false;
				if (value is bool)
				{
					result = (bool)value;
				}
				else if (value is string)
				{
					result = (value?.ToString() == "1");
				}
				DataBucket bucket = null;

				if (buckets.ContainsKey(result))
				{
					bucket = buckets[result];
				}
				
				if (bucket != null)
				{
					bucket.Count += obj.GetSliceCount();
					bucket.Data.Add(obj);
				}
			}

			return list;
		}
	}
}

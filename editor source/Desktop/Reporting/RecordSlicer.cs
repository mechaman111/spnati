using Desktop.CommonControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Desktop.Reporting
{
	public class RecordSlicer : BaseSlicer<IRecord>
	{
		public string Property;
		public Type RecordType;
		
		public RecordSlicer(Type recordType, string property, string displayName, bool includeOther, bool includeNull)
		{
			Groups = new ObservableCollection<ISlicerGroup>();
			if (includeOther)
			{
				SlicerGroup<IRecord> catchAll = new SlicerGroup<IRecord>();
				catchAll.Label = "Other";
				catchAll.Key = "*";
				Groups.Add(catchAll);
			}
			if (includeNull)
			{
				SlicerGroup<IRecord> catchNull = new SlicerGroup<IRecord>();
				catchNull.Label = "None";
				catchNull.Key = "-";
				Groups.Add(catchNull);
			}
			Property = property;
			RecordType = recordType;
			DisplayName = displayName;
		}

		public SlicerGroup<IRecord> AddGroup(string key)
		{
			IRecord record = RecordLookup.Get(RecordType, key, false, null);
			if (record != null)
			{
				SlicerGroup<IRecord> group = AddGroup(record) as SlicerGroup<IRecord>;
				Groups.Add(group);
				return group;
			}
			return null;
		}

		public override List<DataBucket> Slice(IEnumerable<ISliceable> dataSet)
		{
			Dictionary<string, DataBucket> buckets = new Dictionary<string, DataBucket>();
			List<DataBucket> list = new List<DataBucket>();
			DataBucket catchAll = null;
			DataBucket catchNull = null;
			foreach (SlicerGroup<IRecord> group in Groups)
			{
				if (!group.Active) { continue; }
				DataBucket bucket = new DataBucket(group.Label);
				if (group.Key == "*")
				{
					catchAll = bucket;
					continue;
				}
				else if (group.Key == "-")
				{
					catchNull = bucket;
					continue;
				}
				foreach (IRecord record in group.Values)
				{
					buckets[record.Key] = bucket;
				}
				list.Add(bucket);
			}
			list.Sort();
			if (catchAll != null)
			{
				//put the catch all at the end
				list.Add(catchAll);
			}
			if (catchNull != null)
			{
				list.Add(catchNull);
			}

			foreach (ISliceable obj in dataSet)
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(obj.GetType(), Property);
				object value = mi.GetValue(obj);
				IRecord record = RecordLookup.Get(RecordType, value?.ToString(), false, Context);
				DataBucket bucket = null;
				if (record != null)
				{
					if (buckets.ContainsKey(record.Key))
					{
						bucket = buckets[record.Key];
					}
					else if (catchAll != null)
					{
						bucket = catchAll;
					}
				}
				else
				{
					bucket = catchNull;
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

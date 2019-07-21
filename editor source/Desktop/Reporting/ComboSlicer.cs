using Desktop.CommonControls;
using Desktop.CommonControls.PropertyControls;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Desktop.Reporting
{
	public class ComboSlicer : BaseSlicer<string>
	{
		public ISlicerGroup AnyGroup { get; private set; }
		public string Property;

		public ComboSlicer(Type sourceType, string property, string displayName, KeyValuePair<string, string>[] options = null)
		{
			DisplayName = displayName;
			Property = property;
			if (options == null)
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(sourceType, property);
				ComboBoxAttribute attrib = mi.GetCustomAttribute<ComboBoxAttribute>();
				if (attrib != null)
				{
					foreach (string value in attrib.Options)
					{
						if (!string.IsNullOrEmpty(value))
						{
							ISlicerGroup group = AddGroup(new KeyValuePair<string, string>(value, value));
							group.Label = value;
							group.Active = false;
						}
					}
				}
			}
			else
			{
				foreach (KeyValuePair<string, string> kvp in options)
				{
					if (!string.IsNullOrEmpty(kvp.Key))
					{
						ISlicerGroup group = AddGroup(kvp);
						group.Label = kvp.Value;
						group.Active = false;
					}
				}
			}
			AnyGroup = AddGroup("-");
			AnyGroup.Label = "Not specified";
		}

		public override List<DataBucket> Slice(IEnumerable<ISliceable> dataSet)
		{
			Dictionary<string, DataBucket> buckets = new Dictionary<string, DataBucket>();
			List<DataBucket> list = new List<DataBucket>();
			DataBucket catchNull = null;
			foreach (ISlicerGroup group in Groups)
			{
				if (!group.Active) { continue; }
				DataBucket bucket = new DataBucket(group.Label);
				if (group.Key == "-")
				{
					catchNull = bucket;
					continue;
				}
				foreach (KeyValuePair<string, string> value in group.Values)
				{
					if (string.IsNullOrEmpty(value.Key)) { continue; }
					buckets[value.Key] = bucket;
				}
				list.Add(bucket);
			}
			list.Sort();
			if (catchNull != null)
			{
				list.Add(catchNull);
			}

			foreach (ISliceable obj in dataSet)
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(obj.GetType(), Property);
				string value = mi.GetValue(obj)?.ToString();
				DataBucket bucket = null;
				if (!string.IsNullOrEmpty(value))
				{
					if (buckets.ContainsKey(value))
					{
						bucket = buckets[value];
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

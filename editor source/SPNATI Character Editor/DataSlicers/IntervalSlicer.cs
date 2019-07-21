using Desktop;
using Desktop.CommonControls;
using Desktop.Reporting;
using System.Collections.Generic;
using System.Reflection;

namespace SPNATI_Character_Editor.DataSlicers
{
	public class IntervalSlicer : BaseSlicer<Range>
	{
		public string Property;

		public ISlicerGroup NullGroup { get; private set; }

		public int Min;
		public int Max;

		public IntervalSlicer(string property, string displayName, int min, int max)
		{
			Property = property;
			DisplayName = displayName;
			Min = min;
			Max = max;

			NullGroup = AddGroup(-1);
			NullGroup.Key = "-";
			NullGroup.Label = "None";
			NullGroup.Groupable = false;

			ISlicerGroup grp1 = AddGroup(new Range(min, max));
			grp1.Groupable = false;
		}

		public override List<DataBucket> Slice(IEnumerable<ISliceable> dataSet)
		{
			Dictionary<int, DataBucket> buckets = new Dictionary<int, DataBucket>();
			List<DataBucket> list = new List<DataBucket>();
			foreach (SlicerGroup<Range> group in Groups)
			{
				if (!group.Active || group.Key == "-") { continue; }
				DataBucket bucket = new DataBucket(group.Label);
				foreach (Range value in group.Values)
				{
					for (int i = value.Min; i <= value.Max; i++)
					{
						buckets[i] = bucket;
					}
				}
				list.Add(bucket);
			}
			list.Sort();
			DataBucket nullBucket = null;
			if (NullGroup.Active)
			{
				nullBucket = new DataBucket(NullGroup.Label);
				list.Add(nullBucket);
			}

			foreach (ISliceable obj in dataSet)
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(obj.GetType(), Property);
				string value = mi.GetValue(obj)?.ToString() ?? "";
				if (!string.IsNullOrEmpty(value))
				{
					int min, max;
					string minStr, maxStr;
					string[] pieces = value.Split('-');
					minStr = pieces[0];
					if (pieces.Length > 1)
					{
						maxStr = pieces[1];
					}
					else
					{
						maxStr = minStr;
					}
					if (!int.TryParse(minStr, out min))
					{
						min = 0;
					}
					if (!int.TryParse(maxStr, out max))
					{
						max = int.MaxValue;
					}

					HashSet<DataBucket> usedBuckets = new HashSet<DataBucket>();
					foreach (int bucketKey in buckets.Keys)
					{
						if (bucketKey >= min && bucketKey <= max)
						{
							DataBucket bucket = buckets[bucketKey];
							if (!usedBuckets.Contains(bucket))
							{
								bucket.Count += obj.GetSliceCount();
								bucket.Data.Add(obj);
								usedBuckets.Add(bucket);
							}
						}
					}
				}
				else if (nullBucket != null)
				{
					nullBucket.Count += obj.GetSliceCount();
					nullBucket.Data.Add(obj);
				}
			}

			return list;
		}
	}

	public class Range
	{
		public int Min;
		public int Max;

		public Range(int min, int max)
		{
			Min = min;
			Max = max;
		}

		public Range Split(int pt)
		{
			int oldMax = Max;
			Max = pt - 1;
			return new Range(pt, oldMax);
		}

		public override string ToString()
		{
			return $"{Min}-{Max}";
		}
	}
}

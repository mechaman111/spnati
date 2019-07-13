using Desktop;
using Desktop.CommonControls;
using Desktop.Reporting;
using System.Collections.Generic;
using System.Reflection;

namespace SPNATI_Character_Editor.DataSlicers
{
	public class OneShotSlicer : BaseSlicer<int>
	{
		public ISlicerGroup YesGroup { get; private set; }
		public ISlicerGroup NoGroup { get; private set; }

		public OneShotSlicer() : base()
		{
			DisplayName = "Play Once";
			NoGroup = AddGroup(0);
			NoGroup.Label = "No";
			YesGroup = AddGroup(1);
			YesGroup.Label = "Yes";
		}

		public override List<DataBucket> Slice(IEnumerable<ISliceable> dataSet)
		{
			DataBucket yesBucket = new DataBucket(YesGroup.Label);
			DataBucket noBucket = new DataBucket(NoGroup.Label);
			foreach (ISliceable obj in dataSet)
			{
				MemberInfo mi = PropertyTypeInfo.GetMemberInfo(obj.GetType(), "OneShotId");
				int value = (int)mi.GetValue(obj);
				DataBucket bucket = noBucket;
				if (value > 0)
				{
					bucket = yesBucket;
				}
				bucket.Count += obj.GetSliceCount();
				bucket.Data.Add(obj);
			}

			List<DataBucket> list = new List<DataBucket>();
			if (YesGroup.Active)
			{
				list.Add(yesBucket);
			}
			if (NoGroup.Active)
			{
				list.Add(noBucket);
			}
			return list;
		}
	}
}

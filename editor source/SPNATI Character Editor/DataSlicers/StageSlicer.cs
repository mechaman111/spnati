using Desktop.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPNATI_Character_Editor.DataSlicers
{
	public class StageSlicer : BaseSlicer<int>
	{
		public StageSlicer() : base()
		{
			DisplayName = "Case Stage";
		}

		protected override void OnPropertyChanged(string propName)
		{
			if (propName == "Context")
			{
				Character character = Context as Character;
				ClearGroups();
				for (int i = 0; i < character.Layers + Clothing.ExtraStages; i++)
				{
					StageName stage = character.LayerToStageName(i);
					ISlicerGroup group = AddGroup(i);
					group.Label = stage.DisplayName;
				}
				
			}
		}

		public override List<DataBucket> Slice(IEnumerable<ISliceable> dataSet)
		{
			Dictionary<int, DataBucket> buckets = new Dictionary<int, DataBucket>();
			List<DataBucket> list = new List<DataBucket>();
			foreach (ISlicerGroup group in Groups)
			{
				if (!group.Active) { continue; }
				DataBucket bucket = new DataBucket(group.Label);
				foreach (int value in group.Values)
				{
					buckets[value] = bucket;
				}
				list.Add(bucket);
			}

			foreach (ISliceable obj in dataSet)
			{
				Case c = obj as Case;
				foreach (int stage in c.Stages)
				{
					DataBucket stageBucket;
					if (buckets.TryGetValue(stage, out stageBucket))
					{
						stageBucket.Count += obj.GetSliceCount();
						stageBucket.Data.Add(obj);
					}
				}
			}

			return list;
		}
	}
}

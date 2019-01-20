using SPNATI_Character_Editor.Controls;
using SPNATI_Character_Editor.Controls.EditControls;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class CrossStagePose : IHookSerialization
	{
		[FileSelect(DisplayName = "Path")]
		[XmlAttribute("src")]
		public string FileName;

		[XmlAttribute("stage")]
		public string Stage;

		[PoseStage(DisplayName = "Stages", RowHeight = 300)]
		public HashSet<int> Stages = new HashSet<int>();

		public override string ToString()
		{
			return FileName ?? "Orphaned pose";
		}

		private void ParseStageString()
		{
			Stages.Clear();
			if (!string.IsNullOrEmpty(Stage))
			{
				//convert range string into a set of stages
				string[] ranges = Stage.Split(',');
				foreach (string range in ranges)
				{
					string[] minMax = range.Split('-');
					int min, max;
					int.TryParse(minMax[0], out min);
					if (minMax.Length == 1 || !int.TryParse(minMax[1], out max))
					{
						max = min;
					}
					for (int i = min; i <= max; i++)
					{
						Stages.Add(i);
					}
				}
			}
		}

		private string StageRangeToString()
		{
			//turn the stage map into a string of ranges
			List<int> stages = new List<int>();
			if (Stages.Count == 0)
			{
				return "";
			}
			stages.AddRange(Stages);
			stages.Sort();
			int last = stages[0];
			int startRange = last;

			StringBuilder sb = new StringBuilder();

			for (int i = 1; i < stages.Count; i++)
			{
				int stage = stages[i];
				if (stage - 1 > last)
				{
					if (startRange == last)
					{
						sb.Append(startRange.ToString() + ",");
					}
					else
					{
						sb.Append($"{startRange}-{last},");
					}
					startRange = stage;
				}
				last = stage;
			}
			if (startRange == last)
			{
				sb.Append(startRange.ToString());
			}
			else
			{
				sb.Append($"{startRange}-{last}");
			}

			return sb.ToString();
		}

		public void OnAfterDeserialize()
		{
			ParseStageString();
		}

		public void OnBeforeSerialize()
		{

			Stage = StageRangeToString();
		}
	}
}

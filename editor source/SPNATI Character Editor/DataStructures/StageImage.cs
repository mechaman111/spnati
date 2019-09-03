using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class StageImage
	{
		[XmlText]
		public string Image;

		/// <summary>
		/// Stages this case appears in
		/// </summary>
		[XmlIgnore]
		public List<int> Stages = new List<int>();

		[DefaultValue("")]
		[XmlAttribute("stage")]
		public string StageRange
		{
			get
			{
				return GUIHelper.ListToString(Stages);
			}
			set
			{
				Stages = GUIHelper.StringToList(value);
			}
		}

		[XmlIgnore]
		public PoseMapping Pose { get; set; }

		public StageImage() { }
		public StageImage(int stage, PoseMapping pose)
		{
			Stages.Add(stage);
			Pose = pose;
			Image = pose?.Key;
		}

		public StageImage Copy()
		{
			StageImage copy = new StageImage();
			copy.Pose = Pose;
			copy.Image = Image;
			copy.StageRange = StageRange;
			return copy;
		}
	}
}

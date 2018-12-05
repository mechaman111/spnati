using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	public class KisekaePose : KisekaeComponent
	{
		[KisekaeSubCode("aa")]
		public KisekaeArm Arms
		{
			get { return GetSubCode<KisekaeArm>("aa"); }
			set { SetSubCode("aa", value); }
		}
		[KisekaeSubCode("ab")]
		public KisekaeHandProp RightProp
		{
			get { return GetSubCode<KisekaeHandProp>("ab"); }
			set { SetSubCode("ab", value); }
		}
		[KisekaeSubCode("ac")]
		public KisekaeHandProp LeftProp
		{
			get { return GetSubCode<KisekaeHandProp>("ac"); }
			set { SetSubCode("ac", value); }
		}

		[KisekaeSubCode("bb")]
		public KisekaeLeg RightLeg
		{
			get { return GetSubCode<KisekaeLeg>("bb"); }
			set { SetSubCode("bb", value); }
		}
		[KisekaeSubCode("bd")]
		public KisekaeLeg LeftLeg
		{
			get { return GetSubCode<KisekaeLeg>("bd"); }
			set { SetSubCode("bd", value); }
		}

		[KisekaeSubCode("ba")]
		public KisekaeHeadRotation Head
		{
			get { return GetSubCode<KisekaeHeadRotation>("ba"); }
			set { SetSubCode("ba", value); }
		}
		[KisekaeSubCode("be")]
		public KisekaeBodyRotation Body
		{
			get { return GetSubCode<KisekaeBodyRotation>("be"); }
			set { SetSubCode("be", value); }
		}

		[KisekaeSubCode("bc")]
		public KisekaePlacement Placement
		{
			get { return GetSubCode<KisekaePlacement>("bc"); }
			set { SetSubCode("bc", value); }
		}

		[KisekaeSubCode("ad")]
		public KisekaeMassage Massage
		{
			get { return GetSubCode<KisekaeMassage>("ad"); }
			set { SetSubCode("ad", value); }
		}

		[KisekaeSubCode("ae")]
		public KisekaeBreastPosition BreastPosition
		{
			get { return GetSubCode<KisekaeBreastPosition>("ae"); }
			set { SetSubCode("ae", value); }
		}
	}
}

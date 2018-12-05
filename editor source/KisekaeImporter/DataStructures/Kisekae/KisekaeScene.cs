using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	[KisekaeSubCodeList("u", typeof(KisekaeTextBox))]
	[KisekaeSubCodeList("v", typeof(KisekaeGlobalImage))]
	public class KisekaeScene : KisekaeComponent
	{
		public KisekaeScene() : base()
		{
			Group = ComponentGroup.Scene;
		}

		[KisekaeSubCode("ua")]
		public KisekaeBackground Background
		{
			get { return GetSubCode<KisekaeBackground>("ua"); }
			set { SetSubCode("ua", value); }
		}

		[KisekaeSubCode("uf")]
		public KisekaeFloor Floor
		{
			get { return GetSubCode<KisekaeFloor>("uf"); }
			set { SetSubCode("uf", value); }
		}

		[KisekaeSubCode("ue")]
		public KisekaeStage Stage
		{
			get { return GetSubCode<KisekaeStage>("ue"); }
			set { SetSubCode("ue", value); }
		}

		[KisekaeSubCode("ub")]
		public KisekaeStage Crowd
		{
			get { return GetSubCode<KisekaeStage>("ub"); }
			set { SetSubCode("ub", value); }
		}

		public KisekaeTextBox GetTextBox(int index)
		{
			return GetSubCode<KisekaeTextBox>("u" + index.ToString("0"));
		}

		public KisekaeTextBox GetImage(int index)
		{
			return GetSubCode<KisekaeTextBox>("v" + index.ToString("00"));
		}

		[KisekaeSubCode("ud")]
		public KisekaeCensor Censor
		{
			get { return GetSubCode<KisekaeCensor>("ud"); }
			set { SetSubCode("ud", value); }
		}

		[KisekaeSubCode("uc")]
		public KisekaeCamera Camera
		{
			get { return GetSubCode<KisekaeCamera>("uc"); }
			set { SetSubCode("uc", value); }
		}
	}
}

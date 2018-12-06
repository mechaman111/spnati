using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	[KisekaeSubCodeList("a", typeof(KisekaeGlobalArm))]
	[KisekaeSubCodeList("b", typeof(KisekaeClothes))]
	[KisekaeSubCodeList("c", typeof(KisekaeClothes))]
	[KisekaeSubCodeList("d", typeof(KisekaeClothes))]
	[KisekaeSubCodeList("e", typeof(KisekaeGlobalDecoration))]
	[KisekaeSubCodeList("w", typeof(KisekaeGlobalHairpiece))]
	[KisekaeSubCodeList("x", typeof(KisekaeGlobalBelt))]
	[KisekaeSubCodeList("y", typeof(KisekaeGlobalFlag))]
	[KisekaeSubCodeList("z", typeof(KisekaeGlobalSpeech))]
	public class KisekaeGlobalParts : KisekaeComponent
	{
		public KisekaeGlobalParts() : base()
		{
			Group = ComponentGroup.Scene;
		}

		public KisekaeGlobalArm GetArm(int index)
		{
			return GetSubCode<KisekaeGlobalArm>("a" + index.ToString("00"));
		}

		public KisekaeClothes GetProp(int index)
		{
			return GetSubCode<KisekaeClothes>("b" + index.ToString("00"));
		}

		public KisekaeClothes GetSleeve(int index)
		{
			return GetSubCode<KisekaeClothes>("c" + index.ToString("00"));
		}

		public KisekaeClothes GetBracelet(int index)
		{
			return GetSubCode<KisekaeClothes>("d" + index.ToString("00"));
		}

		public KisekaeGlobalHairpiece GetHairpiece(int index)
		{
			return GetSubCode<KisekaeGlobalHairpiece>("w" + index.ToString("00"));
		}

		public KisekaeGlobalBelt GetBelt(int index)
		{
			return GetSubCode<KisekaeGlobalBelt>("x" + index.ToString("00"));
		}

		public KisekaeGlobalDecoration GetDecoration(int index)
		{
			return GetSubCode<KisekaeGlobalDecoration>("e" + index.ToString("00"));
		}

		public KisekaeGlobalFlag GetFlag(int index)
		{
			return GetSubCode<KisekaeGlobalFlag>("y" + index.ToString("00"));
		}

		public KisekaeGlobalSpeech GetSpeechBubble(int index)
		{
			return GetSubCode<KisekaeGlobalSpeech>("z" + index.ToString("00"));
		}
	}
}

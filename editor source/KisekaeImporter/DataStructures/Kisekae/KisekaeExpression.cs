
using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	public class KisekaeExpression : KisekaeComponent
	{
		[KisekaeSubCode("ga")]
		public KisekaeSubCode Unused1
		{
			get { return GetSubCode<KisekaeSubCode>("ga"); }
			set { SetSubCode("ga", value); }
		}
		[KisekaeSubCode("gb")]
		public KisekaeSubCode Unused2
		{
			get { return GetSubCode<KisekaeSubCode>("gb"); }
			set { SetSubCode("gb", value); }
		}
		[KisekaeSubCode("gh")]
		public KisekaeSubCode Unused3
		{
			get { return GetSubCode<KisekaeSubCode>("gh"); }
			set { SetSubCode("gh", value); }
		}
		[KisekaeSubCode("gc")]
		public KisekaeBlush Blush
		{
			get { return GetSubCode<KisekaeBlush>("gc"); }
			set { SetSubCode("gc", value); }
		}
		[KisekaeSubCode("gd")]
		public KisekaeBitField Tears
		{
			get { return GetSubCode<KisekaeBitField>("gd"); }
			set { SetSubCode("gd", value); }
		}
		[KisekaeSubCode("ge")]
		public KisekaeBitField EmotionIcon
		{
			get { return GetSubCode<KisekaeBitField>("ge"); }
			set { SetSubCode("ge", value); }
		}
		[KisekaeSubCode("gf")]
		public KisekaeShape SpeechBubble
		{
			get { return GetSubCode<KisekaeShape>("gf"); }
			set { SetSubCode("gf", value); }
		}
		[KisekaeSubCode("gg")]
		public KisekaeSpecialEyes Eyes
		{
			get { return GetSubCode<KisekaeSpecialEyes>("gg"); }
			set { SetSubCode("gg", value); }
		}
		[KisekaeSubCode("ha")]
		public KisekaeBlink Blink
		{
			get { return GetSubCode<KisekaeBlink>("ha"); }
			set { SetSubCode("ha", value); }
		}
		[KisekaeSubCode("hb")]
		public KisekaeLook Look
		{
			get { return GetSubCode<KisekaeLook>("hb"); }
			set { SetSubCode("hb", value); }
		}
		[KisekaeSubCode("hc")]
		public KisekaeExpressionBrow Brows
		{
			get { return GetSubCode<KisekaeExpressionBrow>("hc"); }
			set { SetSubCode("hc", value); }
		}
		[KisekaeSubCode("hd")]
		public KisekaeExpressionMouth Mouth
		{
			get { return GetSubCode<KisekaeExpressionMouth>("hd"); }
			set { SetSubCode("hd", value); }
		}
	}
}

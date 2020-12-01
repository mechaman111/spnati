
using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	public class KisekaeShader : KisekaeComponent
	{
		[KisekaeSubCode("bf")]
		public KisekaeShadow Shadow
		{
			get { return GetSubCode<KisekaeShadow>("bf"); }
			set { SetSubCode("bf", value); }
		}

		[KisekaeSubCode("bg")]
		public KisekaeBlur Blur
		{
			get { return GetSubCode<KisekaeBlur>("bg"); }
			set { SetSubCode("bg", value); }
		}

		[KisekaeSubCode("bh")]
		public KisekaeBlurExpression Expression
		{
			get { return GetSubCode<KisekaeBlurExpression>("bh"); }
			set	{ SetSubCode("bh", value); }
		}

		[KisekaeSubCode("bi")]
		public KisekaeShaderEffect Effect
		{
			get { return GetSubCode<KisekaeShaderEffect>("bi"); }
			set { SetSubCode("bi", value); }
		}
	}
}
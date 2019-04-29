using KisekaeImporter.SubCodes;

namespace KisekaeImporter
{
	[KisekaeSubCodeList("t", typeof(KisekaeFacePaint))]
	public class KisekaeFace : KisekaeComponent
	{
		[KisekaeSubCode("fa")]
		public KisekaeEyes Eyes
		{
			get { return GetSubCode<KisekaeEyes>("fa"); }
			set { SetSubCode("fa", value); }
		}
		[KisekaeSubCode("fb")]
		public KisekaeEyelids Eyelids
		{
			get { return GetSubCode<KisekaeEyelids>("fb"); }
			set { SetSubCode("fb", value); }
		}
		[KisekaeSubCode("fh")]
		public KisekaeEyelidBottom EyeBottoms
		{
			get { return GetSubCode<KisekaeEyelidBottom>("fh"); }
			set { SetSubCode("fh", value); }
		}
		[KisekaeSubCode("fk")]
		public KisekaeEyelash Eyelashes
		{
			get { return GetSubCode<KisekaeEyelash>("fk"); }
			set { SetSubCode("fk", value); }
		}
		[KisekaeSubCode("fc")]
		public KisekaeIris Iris
		{
			get { return GetSubCode<KisekaeIris>("fc"); }
			set { SetSubCode("fc", value); }
		}
		[KisekaeSubCode("fj")]
		public KisekaePupil Pupils
		{
			get { return GetSubCode<KisekaePupil>("fj"); }
			set { SetSubCode("fj", value); }
		}
		[KisekaeSubCode("fd")]
		public KisekaeEyebrows Eyebrows
		{
			get { return GetSubCode<KisekaeEyebrows>("fd"); }
			set { SetSubCode("fd", value); }
		}
		[KisekaeSubCode("fe")]
		public KisekaeMouth Mouth
		{
			get { return GetSubCode<KisekaeMouth>("fe"); }
			set { SetSubCode("fe", value); }
		}
		[KisekaeSubCode("ff")]
		public KisekaeBitField Freckles
		{
			get { return GetSubCode<KisekaeBitField>("ff"); }
			set { SetSubCode("ff", value); }
		}
		[KisekaeSubCode("fg")]
		public KisekaeNose Nose
		{
			get { return GetSubCode<KisekaeNose>("fg"); }
			set { SetSubCode("fg", value); }
		}
		[KisekaeSubCode("fi")]
		public KisekaeNoseHilite NoseHighlight
		{
			get { return GetSubCode<KisekaeNoseHilite>("fi"); }
			set { SetSubCode("fi", value); }
		}
		[KisekaeSubCode("pa")]
		public KisekaeEar Ears
		{
			get { return GetSubCode<KisekaeEar>("pa"); }
			set { SetSubCode("pa", value); }
		}

		public KisekaeFacePaint GetFacePaint(int index)
		{
			return GetSubCode<KisekaeFacePaint>("t" + index.ToString("00"));
		}
	}
}


using KisekaeImporter.SubCodes;
using System.Collections.Generic;

namespace KisekaeImporter
{
	[KisekaeSubCodeList("m", typeof(KisekaeHairpiece))]
	[KisekaeSubCodeList("n", typeof(KisekaeHairclip))]
	[KisekaeSubCodeList("s", typeof(KisekaeBelt))]
	public class KisekaeClothing : KisekaeComponent
	{
		[KisekaeSubCode("ia")]
		public KisekaeJacket Jacket
		{
			get { return GetSubCode<KisekaeJacket>("ia"); }
			set { SetSubCode("ia", value); }
		}
		[KisekaeSubCode("if")]
		public KisekaeSweater Sweater
		{
			get { return GetSubCode<KisekaeSweater>("if"); }
			set { SetSubCode("if", value); }
		}
		[KisekaeSubCode("ib")]
		public KisekaeShirt Shirt
		{
			get { return GetSubCode<KisekaeShirt>("ib"); }
			set { SetSubCode("ib", value); }
		}
		[KisekaeSubCode("id")]
		public KisekaeUndershirt Undershirt
		{
			get { return GetSubCode<KisekaeUndershirt>("id"); }
			set { SetSubCode("id", value); }
		}
		[KisekaeSubCode("ic")]
		public KisekaeSkirt Skirt
		{
			get { return GetSubCode<KisekaeSkirt>("ic"); }
			set { SetSubCode("ic", value); }
		}
		[KisekaeSubCode("jc")]
		public KisekaePantyhose Pantyhose
		{
			get { return GetSubCode<KisekaePantyhose>("jc"); }
			set { SetSubCode("jc", value); }
		}
		[KisekaeSubCode("ie")]
		public KisekaePants Pants
		{
			get { return GetSubCode<KisekaePants>("ie"); }
			set { SetSubCode("ie", value); }
		}
		[KisekaeSubCode("ja")]
		public KisekaeClothes RightSock
		{
			get { return GetSubCode<KisekaeClothes>("ja"); }
			set { SetSubCode("ja", value); }
		}
		[KisekaeSubCode("jb")]
		public KisekaeClothes LeftSock
		{
			get { return GetSubCode<KisekaeClothes>("jb"); }
			set { SetSubCode("jb", value); }
		}
		[KisekaeSubCode("jd")]
		public KisekaeShoe RightShoe
		{
			get { return GetSubCode<KisekaeShoe>("jd"); }
			set { SetSubCode("jd", value); }
		}
		[KisekaeSubCode("je")]
		public KisekaeShoe LeftShoe
		{
			get { return GetSubCode<KisekaeShoe>("je"); }
			set { SetSubCode("je", value); }
		}
		[KisekaeSubCode("jf")]
		public KisekaeClothes RightAnklet
		{
			get { return GetSubCode<KisekaeClothes>("jf"); }
			set { SetSubCode("jf", value); }
		}
		[KisekaeSubCode("jg")]
		public KisekaeClothes LeftAnklet
		{
			get { return GetSubCode<KisekaeClothes>("jg"); }
			set { SetSubCode("jg", value); }
		}
		[KisekaeSubCode("ka")]
		public KisekaeSwimsuit Bra
		{
			get { return GetSubCode<KisekaeSwimsuit>("ka"); }
			set { SetSubCode("ka", value); }
		}
		public KisekaeSwimsuit Swimsuit
		{
			get { return GetSubCode<KisekaeSwimsuit>("ka"); }
			set { SetSubCode("ka", value); }
		}
			[KisekaeSubCode("kb")]
		public KisekaeClothes Panties
		{
			get { return GetSubCode<KisekaeClothes>("kb"); }
			set { SetSubCode("kb", value); }
		}
		[KisekaeSubCode("kc")]
		public KisekaePasty RightPasty
		{
			get { return GetSubCode<KisekaePasty>("kc"); }
			set { SetSubCode("kc", value); }
		}
		[KisekaeSubCode("kd")]
		public KisekaePasty LeftPasty
		{
			get { return GetSubCode<KisekaePasty>("kd"); }
			set { SetSubCode("kd", value); }
		}
		[KisekaeSubCode("ke")]
		public KisekaePasty VagPasty
		{
			get { return GetSubCode<KisekaePasty>("ke"); }
			set { SetSubCode("ke", value); }
		}
		[KisekaeSubCode("kf")]
		public KisekaeClothes Bondage
		{
			get { return GetSubCode<KisekaeClothes>("kf"); }
			set { SetSubCode("kf", value); }
		}
		[KisekaeSubCode("kg")]
		public KisekaeToy Toy
		{
			get { return GetSubCode<KisekaeToy>("kg"); }
			set { SetSubCode("kg", value); }
		}
		[KisekaeSubCode("la")]
		public KisekaeHat Hat
		{
			get { return GetSubCode<KisekaeHat>("la"); }
			set { SetSubCode("la", value); }
		}
		[KisekaeSubCode("lb")]
		public KisekaeGlasses Glasses
		{
			get { return GetSubCode<KisekaeGlasses>("lb"); }
			set { SetSubCode("lb", value); }
		}
		[KisekaeSubCode("lc")]
		public KisekaeHeadband Headband
		{
			get { return GetSubCode<KisekaeHeadband>("lc"); }
			set { SetSubCode("lc", value); }
		}
		[KisekaeSubCode("oa")]
		public KisekaeClothes Mask
		{
			get { return GetSubCode<KisekaeClothes>("oa"); }
			set { SetSubCode("oa", value); }
		}
		[KisekaeSubCode("os")]
		public KisekaeClothes Headphones
		{
			get { return GetSubCode<KisekaeClothes>("os"); }
			set { SetSubCode("os", value); }
		}
		[KisekaeSubCode("ob")]
		public KisekaeClothes RightEarring
		{
			get { return GetSubCode<KisekaeClothes>("ob"); }
			set { SetSubCode("ob", value); }
		}
		[KisekaeSubCode("oc")]
		public KisekaeClothes LeftEarring
		{
			get { return GetSubCode<KisekaeClothes>("oc"); }
			set { SetSubCode("oc", value); }
		}
		[KisekaeSubCode("od")]
		public KisekaeChoker Choker
		{
			get { return GetSubCode<KisekaeChoker>("od"); }
			set { SetSubCode("od", value); }
		}
		[KisekaeSubCode("oe")]
		public KisekaeNecklace Necklace
		{
			get { return GetSubCode<KisekaeNecklace>("oe"); }
			set { SetSubCode("oe", value); }
		}
		[KisekaeSubCode("of")]
		public KisekaeTie Tie
		{
			get { return GetSubCode<KisekaeTie>("of"); }
			set { SetSubCode("of", value); }
		}
		[KisekaeSubCode("og")]
		public KisekaeClothes RightBracelet
		{
			get { return GetSubCode<KisekaeClothes>("og"); }
			set { SetSubCode("og", value); }
		}
		[KisekaeSubCode("oh")]
		public KisekaeClothes LeftBracelet
		{
			get { return GetSubCode<KisekaeClothes>("oh"); }
			set { SetSubCode("oh", value); }
		}
		[KisekaeSubCode("oo")]
		public KisekaeClothes RightArmlet
		{
			get { return GetSubCode<KisekaeClothes>("oo"); }
			set { SetSubCode("oo", value); }
		}
		[KisekaeSubCode("op")]
		public KisekaeClothes LeftArmlet
		{
			get { return GetSubCode<KisekaeClothes>("op"); }
			set { SetSubCode("op", value); }
		}
		[KisekaeSubCode("oq")]
		public KisekaeClothes RightArmband
		{
			get { return GetSubCode<KisekaeClothes>("oq"); }
			set { SetSubCode("oq", value); }
		}
		[KisekaeSubCode("or")]
		public KisekaeClothes LeftArmband
		{
			get { return GetSubCode<KisekaeClothes>("or"); }
			set { SetSubCode("or", value); }
		}
		[KisekaeSubCode("oi")]
		public KisekaeGlove RightGlove
		{
			get { return GetSubCode<KisekaeGlove>("oi"); }
			set { SetSubCode("oi", value); }
		}
		[KisekaeSubCode("oj")]
		public KisekaeGlove LeftGlove
		{
			get { return GetSubCode<KisekaeGlove>("oj"); }
			set { SetSubCode("oj", value); }
		}
		[KisekaeSubCode("ok")]
		public KisekaeClothes RightElbow
		{
			get { return GetSubCode<KisekaeClothes>("ok"); }
			set { SetSubCode("ok", value); }
		}
		[KisekaeSubCode("ol")]
		public KisekaeClothes LeftElbow
		{
			get { return GetSubCode<KisekaeClothes>("ol"); }
			set { SetSubCode("ol", value); }
		}
		[KisekaeSubCode("om")]
		public KisekaeClothes RightBicep
		{
			get { return GetSubCode<KisekaeClothes>("om"); }
			set { SetSubCode("om", value); }
		}
		[KisekaeSubCode("on")]
		public KisekaeClothes LeftBicep
		{
			get { return GetSubCode<KisekaeClothes>("on"); }
			set { SetSubCode("on", value); }
		}

		public KisekaeHairpiece GetHairpiece(int index)
		{
			return GetSubCode<KisekaeHairpiece>("m" + index.ToString("00"));
		}

		public KisekaeHairclip GetHairclip(int index)
		{
			return GetSubCode<KisekaeHairclip>("n" + index.ToString("00"));
		}

		public KisekaeBelt GetBelt(int index)
		{
			return GetSubCode<KisekaeBelt>("s" + index.ToString("00"));
		}

		public void SetColors(params KisekaeColor[] colors)
		{
			if (colors.Length == 0)
				return;
			foreach (var subcode in _subcodes.Values)
			{
				if (subcode is IColorable)
				{
					IColorable colorable = subcode as IColorable;
					colorable.Color1 = colors[0];
					if (colors.Length > 1)
						colorable.Color2 = colors[1];
					if (colors.Length > 2)
						colorable.Color3 = colors[2];
				}
			}
		}
	}
}

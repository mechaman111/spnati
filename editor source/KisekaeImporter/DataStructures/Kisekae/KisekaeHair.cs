
using KisekaeImporter.SubCodes;
using System.Collections.Generic;

namespace KisekaeImporter
{
	[KisekaeSubCodeList("r", typeof(KisekaeAhoge))]
	public class KisekaeHair : KisekaeComponent
	{
		[KisekaeSubCode("ea")]
		public KisekaeMainHair Main
		{
			get { return GetSubCode<KisekaeMainHair>("ea"); }
			set { SetSubCode("ea", value); }
		}
		[KisekaeSubCode("ec")]
		public KisekaeBackHair Back
		{
			get { return GetSubCode<KisekaeBackHair>("ec"); }
			set { SetSubCode("ec", value); }
		}
		[KisekaeSubCode("ed")]
		public KisekaeBangs Bangs
		{
			get { return GetSubCode<KisekaeBangs>("ed"); }
			set { SetSubCode("ed", value); }
		}
		[KisekaeSubCode("ef")]
		public KisekaeHairSide RightSide
		{
			get { return GetSubCode<KisekaeHairSide>("ef"); }
			set { SetSubCode("ef", value); }
		}
		[KisekaeSubCode("eg")]
		public KisekaeHairSide LeftSide
		{
			get { return GetSubCode<KisekaeHairSide>("eg"); }
			set { SetSubCode("eg", value); }
		}

		public void SetHairColor(int hairId)
		{
			KisekaeColor color1 = new KisekaeColor(System.Drawing.Color.SaddleBrown);
			KisekaeColor color2 = new KisekaeColor(System.Drawing.Color.RosyBrown);
			KisekaeColor color3 = new KisekaeColor(System.Drawing.Color.Black);
			foreach (var subcode in _subcodes.Values)
			{
				if (subcode.IsEmpty)
					continue;
				if (subcode is IColorable)
				{
					IColorable hair = subcode as IColorable;
					hair.Color1 = color1;
					hair.Color2 = color2;
					hair.Color3 = color3;
				}
			}
		}

		public KisekaeAhoge GetAhoge(int index)
		{
			return GetSubCode<KisekaeAhoge>("r" + index.ToString("00"));
		}

		public void SetAhoge(int index, KisekaeAhoge ahoge)
		{
			SetSubCode("r" + index.ToString("00"), ahoge);
		}
	}
}

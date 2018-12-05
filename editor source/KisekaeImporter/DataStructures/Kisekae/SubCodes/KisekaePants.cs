namespace KisekaeImporter.SubCodes
{
	public class KisekaePants : KisekaeClothes
	{
		public override void SetColors(KisekaeColor color1, KisekaeColor color2, KisekaeColor color3)
		{
			base.SetColors(color1, color2, color3);
			RightLegColor1 = color1;
			RightLegColor2 = color2;
			RightLegColor3 = color3;
			LeftLegColor1 = color1;
			LeftLegColor2 = color2;
			LeftLegColor3 = color3;
		}

		public int RightLegLength
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public KisekaeColor RightLegColor1
		{
			get { return new KisekaeColor(GetString(5)); }
			set { Set(5, value.ToString()); }
		}

		public KisekaeColor RightLegColor2
		{
			get { return new KisekaeColor(GetString(6)); }
			set { Set(6, value.ToString()); }
		}

		public KisekaeColor RightLegColor3
		{
			get { return new KisekaeColor(GetString(7)); }
			set { Set(7, value.ToString()); }
		}

		public int LeftLegLength
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public KisekaeColor LeftLegColor1
		{
			get { return new KisekaeColor(GetString(9)); }
			set { Set(9, value.ToString()); }
		}

		public KisekaeColor LeftLegColor2
		{
			get { return new KisekaeColor(GetString(10)); }
			set { Set(10, value.ToString()); }
		}

		public KisekaeColor LeftLegColor3
		{
			get { return new KisekaeColor(GetString(11)); }
			set { Set(11, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(12); }
			set { Set(12, value.ToString()); }
		}
	}
}

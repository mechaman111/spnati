namespace KisekaeImporter.SubCodes
{
	public class KisekaeBlurExpression : KisekaeSubCode
	{
		public KisekaeBlurExpression() : base("bh") { }

		public bool Blur
		{
			get { return GetBool(0); }
			set { Set(0, value.ToString()); }
		}
	}

	public class KisekaeShaderEffect : KisekaeSubCode
	{
		public KisekaeShaderEffect() : base("bi") { }

		public int Effect
		{
			get { return GetInt(0); }
			set { Set(0, value); }
		}

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public int Opacity
		{
			get { return GetInt(2); }
			set { Set(2, value); }
		}

		public int Fill
		{
			get { return GetInt(3); }
			set { Set(3, value); }
		}
	}
}

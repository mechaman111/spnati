namespace KisekaeImporter.SubCodes
{
	public class KisekaeShadow : KisekaeSubCode
	{
		public KisekaeShadow() : base("bf") { }

		public int Blur
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public bool Inset
		{
			get { return GetBool(2); }
			set { Set(2, value); }
		}

		public bool Cutout
		{
			get { return GetBool(3); }
			set { Set(3, value); }
		}

		public bool ModelVisible
		{
			get { return GetBool(4); }
			set { Set(4, value); }
		}

		public int Opacity
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int Thickness
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int Spread
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}
	}
}

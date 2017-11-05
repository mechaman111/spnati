namespace KisekaeImporter.SubCodes
{
	public class KisekaeSpecialEyes : KisekaeSubCode
	{
		public KisekaeSpecialEyes() : base("gg") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public int ScaleX
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int OffsetY
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int Side
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}
	}
}

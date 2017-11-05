namespace KisekaeImporter.SubCodes
{
	public class KisekaeTail : KisekaeSubCode 
	{
		public KisekaeTail() : base("pe") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public KisekaeColor Color1
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public KisekaeColor Color2
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public KisekaeColor Color3
		{
			get { return new KisekaeColor(GetString(3)); }
			set { Set(3, value.ToString()); }
		}

		public int Side
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int Scale
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}
	}
}

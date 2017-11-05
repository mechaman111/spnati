namespace KisekaeImporter.SubCodes
{
	public class KisekaeTan : KisekaeSubCode
	{
		public KisekaeTan() : base("db") { }

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
	}
}

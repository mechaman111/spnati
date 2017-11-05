namespace KisekaeImporter.SubCodes
{
	public class KisekaeSkin : KisekaeSubCode
	{
		public KisekaeSkin() : base("da") { }

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(0)); }
			set { Set(0, value.ToString()); }
		}

		public int Opacity
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}
	}
}

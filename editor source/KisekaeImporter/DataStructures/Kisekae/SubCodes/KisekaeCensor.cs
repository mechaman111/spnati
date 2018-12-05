namespace KisekaeImporter.SubCodes
{
	public class KisekaeCensor : KisekaeSubCode
	{
		public int Type
		{
			get { return GetInt(0); }
			set { Set(0, value); }
		}

		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}
	}
}

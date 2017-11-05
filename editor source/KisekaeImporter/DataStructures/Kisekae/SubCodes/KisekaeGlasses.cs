namespace KisekaeImporter.SubCodes
{
	public class KisekaeGlasses : KisekaeSubCode
	{
		public KisekaeGlasses() : base("lb") { }

		public int Side
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}
	}
}

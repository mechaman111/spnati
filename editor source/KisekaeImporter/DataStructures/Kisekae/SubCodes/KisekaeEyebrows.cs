namespace KisekaeImporter.SubCodes
{
	public class KisekaeEyebrows : KisekaeSubCode
	{
		public KisekaeEyebrows() : base("fd") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public KisekaeColor Color1
		{
			get { return new KisekaeColor(GetString(3)); }
			set { Set(3, value.ToString()); }
		}

		public KisekaeColor Color2
		{
			get { return new KisekaeColor(GetString(4)); }
			set { Set(4, value.ToString()); }
		}
	}
}

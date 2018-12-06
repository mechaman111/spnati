namespace KisekaeImporter.SubCodes
{
	public class KisekaeFloor : KisekaeSubCode
	{
		public int Image
		{
			get { return GetInt(0); }
			set { Set(0, value); }
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
	}
}

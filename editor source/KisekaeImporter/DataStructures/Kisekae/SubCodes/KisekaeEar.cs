namespace KisekaeImporter.SubCodes
{
	public class KisekaeEar : KisekaeSubCode 
	{
		public KisekaeEar() : base("pa") { }

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

		public int ScaleX
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int OffsetY
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}
	}
}

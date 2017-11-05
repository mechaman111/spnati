namespace KisekaeImporter.SubCodes
{
	public class KisekaeIris : KisekaeSubCode
	{
		public KisekaeIris() : base("fc") { }

		public int RightShape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public KisekaeColor RightColorIris
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public KisekaeColor RightColorWhites
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public int LeftShape
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public KisekaeColor LeftColorIris
		{
			get { return new KisekaeColor(GetString(4)); }
			set { Set(4, value.ToString()); }
		}

		public KisekaeColor LeftColorWhites
		{
			get { return new KisekaeColor(GetString(5)); }
			set { Set(5, value.ToString()); }
		}

		public int Width
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public KisekaeColor RightColorPupil
		{
			get { return new KisekaeColor(GetString(7)); }
			set { Set(7, value.ToString()); }
		}

		public KisekaeColor LeftColorPupil
		{
			get { return new KisekaeColor(GetString(8)); }
			set { Set(8, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int XOffset
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public int YOffset
		{
			get { return GetInt(11); }
			set { Set(11, value.ToString()); }
		}
	}
}

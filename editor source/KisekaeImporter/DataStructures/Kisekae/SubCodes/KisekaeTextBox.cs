namespace KisekaeImporter.SubCodes
{
	//This isn't really supported yet since the array is in the format u# instead of u## but everything assumes the latter
	public class KisekaeTextBox : KisekaeSubCode
	{
		public KisekaeColor Color
		{
			get { return new KisekaeColor(GetString(0)); }
			set { Set(0, value.ToString()); }
		}

		public int Scale
		{
			get { return GetInt(1); }
			set { Set(1, value); }
		}

		public int X
		{
			get { return GetInt(2); }
			set { Set(2, value); }
		}

		public int Y
		{
			get { return GetInt(3); }
			set { Set(3, value); }
		}

		public int Height
		{
			get { return GetInt(4); }
			set { Set(4, value); }
		}

		public int Width
		{
			get { return GetInt(5); }
			set { Set(5, value); }
		}
	}
}

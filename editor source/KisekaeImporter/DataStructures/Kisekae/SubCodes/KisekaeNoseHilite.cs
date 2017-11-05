namespace KisekaeImporter.SubCodes
{
	public class KisekaeNoseHilite : KisekaeSubCode 
	{
		public KisekaeNoseHilite() : base("fi") { }

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Opacity
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int XScale
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int YScale
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

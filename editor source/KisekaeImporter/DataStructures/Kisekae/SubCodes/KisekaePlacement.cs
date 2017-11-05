namespace KisekaeImporter.SubCodes
{
	public class KisekaePlacement : KisekaeSubCode
	{
		public KisekaePlacement() : base("bc") { }

		public int X
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Y
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int Z
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int Shadow
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

namespace KisekaeImporter.SubCodes
{
	public class KisekaeMouth : KisekaeSubCode 
	{
		public KisekaeMouth() : base("fe") { }

		public int Width
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}
	}
}

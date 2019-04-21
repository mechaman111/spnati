namespace KisekaeImporter.SubCodes
{
	public class KisekaeGlasses : KisekaeClothes
	{
		public KisekaeGlasses() : base() { }

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

		public int Layer
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}
	}
}

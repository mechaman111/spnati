namespace KisekaeImporter.SubCodes
{
	public class KisekaePupil : KisekaeSubCode
	{
		public KisekaePupil() : base("fj") { }

		public int Highlight
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Side
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(2); }
			set { Set(2, value.ToString()); }
		}
	}
}

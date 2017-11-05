namespace KisekaeImporter.SubCodes
{
	public class KisekaeBlink : KisekaeSubCode
	{
		public KisekaeBlink() : base("ha") { }

		public int RightEye
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int LeftEye
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}
	}
}

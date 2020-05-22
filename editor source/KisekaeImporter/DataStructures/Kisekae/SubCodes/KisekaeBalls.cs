namespace KisekaeImporter.SubCodes
{
	public class KisekaeBalls : KisekaeSubCode, IPoseable
	{
		public KisekaeBalls() : base("qb") { }

		public void Pose(IPoseable pose)
		{
		}

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Scale
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}
	}
}

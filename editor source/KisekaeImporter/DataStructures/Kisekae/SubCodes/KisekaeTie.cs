namespace KisekaeImporter.SubCodes
{
	public class KisekaeTie : KisekaeClothes
	{
		public int Length
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

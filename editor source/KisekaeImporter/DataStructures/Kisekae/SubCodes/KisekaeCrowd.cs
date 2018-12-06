namespace KisekaeImporter.SubCodes
{
	public class KisekaeCrowd : KisekaeSubCode
	{
		public int Type
		{
			get { return GetInt(0); }
			set { Set(0, value); }
		}
	}
}

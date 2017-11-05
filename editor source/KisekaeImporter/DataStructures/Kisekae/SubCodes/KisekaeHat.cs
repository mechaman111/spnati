namespace KisekaeImporter.SubCodes
{
	public class KisekaeHat : KisekaeClothes
	{
		public int Side
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

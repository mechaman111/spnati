namespace KisekaeImporter.SubCodes
{
	public class KisekaeNecklace : KisekaeClothes
	{
		public int Layer
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

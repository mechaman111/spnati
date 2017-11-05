namespace KisekaeImporter.SubCodes
{
	public class KisekaeChoker : KisekaeClothes
	{
		public int Layer
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}
	}
}

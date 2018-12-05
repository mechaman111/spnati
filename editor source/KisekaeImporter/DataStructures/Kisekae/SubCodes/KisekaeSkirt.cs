namespace KisekaeImporter.SubCodes
{
	public class KisekaeSkirt : KisekaeClothes
	{
		public int Layer
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

namespace KisekaeImporter.SubCodes
{
	public class KisekaeSwimsuit : KisekaeClothes
	{
		public int Piece
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

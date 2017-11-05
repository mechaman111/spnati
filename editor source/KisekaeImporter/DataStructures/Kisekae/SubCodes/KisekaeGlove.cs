using KisekaeImporter;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeGlove : KisekaeClothes
	{
		public int Length
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}
	}
}

using KisekaeImporter;

namespace KisekaeImporter.SubCodes
{
	public class KisekaeLeg : KisekaeSubCode
	{
		public KisekaeLeg() : base("bb") { }

		public int Position
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}
	}
}

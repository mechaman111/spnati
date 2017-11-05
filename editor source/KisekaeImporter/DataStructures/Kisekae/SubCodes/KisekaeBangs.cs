
namespace KisekaeImporter.SubCodes
{
	public class KisekaeBangs : KisekaeSubCode, IHair
	{
		public KisekaeBangs() : base("ed") { }

		public int Shape
		{
			get { return GetInt(0); }
			set
			{
				Set(0, value.ToString());
			}
		}

		public KisekaeColor Color1
		{
			get { return new KisekaeColor(GetString(4)); }
			set { Set(4, value.ToString()); }
		}

		public KisekaeColor Color2
		{
			get { return new KisekaeColor(GetString(5)); }
			set { Set(5, value.ToString()); }
		}

		public KisekaeColor Color3
		{
			get { return new KisekaeColor(GetString(5)); }
			set { Set(5, value.ToString()); }
		}
	}
}

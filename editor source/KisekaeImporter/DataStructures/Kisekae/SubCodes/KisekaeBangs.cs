
namespace KisekaeImporter.SubCodes
{
	public class KisekaeBangs : KisekaeSubCode, IColorable, IPoseable
	{
		public KisekaeBangs() : base("ed") { }

		public void Pose(IPoseable pose)
		{
		}

		public int Shape
		{
			get { return GetInt(0); }
			set
			{
				Set(0, value.ToString());
			}
		}

		public void SetColors(KisekaeColor color1, KisekaeColor color2, KisekaeColor color3)
		{
			Color1 = color1;
			Color2 = color2;
			Color3 = color3;
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

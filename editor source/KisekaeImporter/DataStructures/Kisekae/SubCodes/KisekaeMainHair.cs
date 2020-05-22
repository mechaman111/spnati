
namespace KisekaeImporter.SubCodes
{
	public class KisekaeMainHair : KisekaeSubCode, IColorable, IPoseable
	{
		public KisekaeMainHair() : base("ea") { }

		public void Pose(IPoseable pose)
		{
			KisekaeMainHair other = pose as KisekaeMainHair;
			if (other == null)
			{
				return;
			}
		}

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public void SetColors(KisekaeColor color1, KisekaeColor color2, KisekaeColor color3)
		{
			Color1 = color1;
			Color2 = color2;
			Color3 = color3;
		}

		public KisekaeColor Color1
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public KisekaeColor Color2
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public KisekaeColor Color3
		{
			get { return new KisekaeColor(GetString(3)); }
			set { Set(3, value.ToString()); }
		}
	}
}

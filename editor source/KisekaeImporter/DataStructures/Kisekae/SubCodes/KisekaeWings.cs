namespace KisekaeImporter.SubCodes
{
	public class KisekaeWings : KisekaeSubCode, IPoseable
	{
		public KisekaeWings() : base("pd") { }

		public void Pose(IPoseable pose)
		{
			KisekaeWings other = pose as KisekaeWings;
			if (other == null)
			{
				return;
			}
			ScaleX = other.ScaleX;
			Side = other.Side;
			OffsetY = other.OffsetY;
		}

		public int Shape
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
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

		public int ScaleX
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int Side
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int OffsetY
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}
	}
}

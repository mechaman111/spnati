namespace KisekaeImporter.SubCodes
{
	public class KisekaePenis : KisekaeSubCode, IPoseable
	{
		public KisekaePenis() : base("qa") { }

		public void Pose(IPoseable pose)
		{
			KisekaePenis other = pose as KisekaePenis;
			if (other == null)
			{
				return;
			}
			Scale = other.Scale;
			ErectionSize = other.Erect;
			Layer = other.Layer;
			Erect = other.Erect;
			Angle = other.Angle;
		}

		public int Size
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public KisekaeColor ShaftColor
		{
			get { return new KisekaeColor(GetString(1)); }
			set { Set(1, value.ToString()); }
		}

		public KisekaeColor HeadColor
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public int Scale
		{
			get { return GetInt(3); }
			set { Set(3, value.ToString()); }
		}

		public int ErectionSize
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public KisekaeColor TipColor
		{
			get { return new KisekaeColor(GetString(5)); }
			set { Set(5, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int Erect
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int Angle
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int Foreskin
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}
	}
}

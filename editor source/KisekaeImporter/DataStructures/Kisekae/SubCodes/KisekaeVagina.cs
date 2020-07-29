namespace KisekaeImporter.SubCodes
{
	public class KisekaeVagina : KisekaeSubCode, IPoseable
	{
		public KisekaeVagina() : base("dc") { }

		public void Pose(IPoseable pose)
		{
			KisekaeVagina other = pose as KisekaeVagina;
			if (other == null)
			{
				return;
			}
			Juice = other.Juice;
			Openness = other.Openness;
		}

		public int Juice
		{
			get { return GetInt(0); }
			set { Set(0, value.ToString()); }
		}

		public int Shape
		{
			get { return GetInt(1); }
			set { Set(1, value.ToString()); }
		}
		
		public KisekaeColor Color1
		{
			get { return new KisekaeColor(GetString(2)); }
			set { Set(2, value.ToString()); }
		}

		public KisekaeColor Color2
		{
			get { return new KisekaeColor(GetString(3)); }
			set { Set(3, value.ToString()); }
		}

		public KisekaeColor Color3
		{
			get { return new KisekaeColor(GetString(4)); }
			set { Set(4, value.ToString()); }
		}

		public int Openness
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}
	}
}

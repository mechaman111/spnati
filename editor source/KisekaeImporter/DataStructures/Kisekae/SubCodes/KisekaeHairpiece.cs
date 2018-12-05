namespace KisekaeImporter.SubCodes
{
	public class KisekaeHairpiece : KisekaeClothes, IPoseable
	{
		public int Side
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int Layer
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int ScaleX
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}

		public int Outline
		{
			get { return GetInt(10); }
			set { Set(10, value.ToString()); }
		}

		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(11)); }
			set { Set(11, value.ToString()); }
		}

		public int ScaleZ
		{
			get { return GetInt(12); }
			set { Set(12, value.ToString()); }
		}

		public int RotationZ
		{
			get { return GetInt(13); }
			set { Set(13, value.ToString()); }
		}
	}
}

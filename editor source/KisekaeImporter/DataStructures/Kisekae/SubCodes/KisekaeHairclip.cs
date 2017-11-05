namespace KisekaeImporter.SubCodes
{
	public class KisekaeHairclip : KisekaeClothes
	{
		public int Side
		{
			get { return GetInt(4); }
			set { Set(4, value.ToString()); }
		}

		public int Scale
		{
			get { return GetInt(5); }
			set { Set(5, value.ToString()); }
		}

		public int Rotation
		{
			get { return GetInt(6); }
			set { Set(6, value.ToString()); }
		}

		public int OffsetX
		{
			get { return GetInt(7); }
			set { Set(7, value.ToString()); }
		}

		public int Height
		{
			get { return GetInt(8); }
			set { Set(8, value.ToString()); }
		}

		public int Outline
		{
			get { return GetInt(9); }
			set { Set(9, value.ToString()); }
		}
		
		public KisekaeColor OutlineColor
		{
			get { return new KisekaeColor(GetString(10)); }
			set { Set(10, value.ToString()); }
		}
	}
}
